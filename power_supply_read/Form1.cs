using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Globalization;


namespace Power_Supply_Read
{
    public enum connection_status
    {
        undefined = 0,
        unconnected = 10,
        connected = 20,
        identified = 30,
        polling = 40,
        done = 99
    }

    public enum channel_sel
    {
        undefined = 0,
        one = 1,
        two = 2
    }

    public enum power_mode
    {
        undefined = 0,
        P8V,
        P20V,
        P35V,
        P60V
    }

    public partial class Form1 : Form
    {
        #region Members
        /// <summary>
        /// Serial port for communication.
        /// </summary>
        /// 

        private const string UNKNOWN_CURRENT_TEXT = "-.-----";
        private const string CURRENT_TEXT_FORMAT  = "0.00000";
        private const string UNKNOWN_VOLTAGE_TEXT = "--.-----";
        private const string VOLTAGE_TEXT_FORMAT  = "00.00000";
        private const string UNKNOWN_POWER_TEXT = "--.-----";
        private const string POWER_TEXT_FORMAT  = "00.00000";
        private const string UNKNOWN_OVP_TEXT = "--.--";
        private const string OVP_TEXT_FORMAT  = "00.00";

        private const int DEFAULT_POLL_TIME_IN_MS = 2000; // on init, and if something goes wrong, set the poll time to this
        private const int THREAD_WAIT_TIMEOUT = 1500; // time to wait to take control of the serial port
        private const int THREAD_AFTER_INTERRUPT_WAIT = 400; // after taking control of the polling thread, this is the time to wait for any incoming response
        private const int SERIAL_RESPONSE_WAIT_TIME = 400; // time in ms to wait for a response before doing anything

        private SerialPort m_pSerialPort = null;
        private char[] received_buffer;         // incoming text from serial port goes here
        private int received_count;             // current received_buffer size
        static string current_device;          // text for device response from query over serial line
        static Semaphore safe_to_access_buffer;// semaphore to control access to serial buffer
        static Semaphore safe_to_access_status;// semaphore to control access to state (state machine)
        static connection_status current_status;// current state of the program, basically the polling thread
        //static channel_sel current_channel;    // which channel output the power supply is currently using
        static power_mode current_mode;        // which output the power supplys is in -- 8v/3a, 20v/1.5a, etc

        private int poll_time_in_ms;            // time between asking the power supply for a new value

        private bool log_to_file_now;           // will save to a file when polling when this is true
        private System.IO.FileStream file_to_save_to; // the file to log to

        private Thread thready = null;          // the thread that polls the connected device
        private object monitor_lock;            // polling thread will sleep for a timeout, or until the lock is pinged

        private System.Threading.Timer progress_bar_tick = null;    // timer for the status countdown bar

        //public delegate void InvokeDelegate(string input);

        #endregion

        #region init
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // mostly just initialization is happening here in form_load

            log_to_file_now = false;

            m_pSerialPort = new SerialPort();
            m_pSerialPort.DataReceived += new SerialDataReceivedEventHandler(m_pSerialPort_DataReceived);
            foreach (string port in System.IO.Ports.SerialPort.GetPortNames())
            {
                cbPort.Items.Add(port);
            }

            // select default UI items
            cbParity.SelectedIndex = 0;
            cbStopBits.SelectedIndex = 0;
            cbDataBits.SelectedIndex = 0;
            cbPort.SelectedIndex = 0;
            cbBaudRate.Text = "9600";
            cbPollDelay.SelectedText = DEFAULT_POLL_TIME_IN_MS.ToString();
            cbPollTimeFormat.SelectedIndex = 0;
            // default polling time delay
            poll_time_in_ms = DEFAULT_POLL_TIME_IN_MS;
            // background var for the channel
            //current_channel = channel_sel.undefined;
            
            // make sure the right things are disabled and enabled
            change_ui_to_disconnected();

            // the lock to synchronize cross thread communications
            monitor_lock = new object();

            thready = new Thread(new ThreadStart(thread_invoke));
            thready.Name = "th";
            thready.IsBackground = true;
            thready.Start();

            progress_bar_tick = new System.Threading.Timer(progress_bar_tick_run, null, 0, 100);

            // serial buffer
            received_buffer = new char[1024];

            // semaphore is owned by parent
            safe_to_access_buffer = new Semaphore(0, 1);
            safe_to_access_status = new Semaphore(0, 1);
            // allow anyone access to the semaphore
            safe_to_access_buffer.Release();
            safe_to_access_status.Release();

            safe_to_access_status.WaitOne();
            current_status = connection_status.unconnected;
            safe_to_access_status.Release();

            clear_buffer();

            pbPollCountdown.Value = 100;

        }

        
        #endregion

        #region serial port & buffer

        // removes all data in the buffer that holds incoming serial data
        private void clear_buffer()
        {
            safe_to_access_buffer.WaitOne();

            for (int i = 0; i < 1024; i++)
            {
                received_buffer[i] = '\0';
            }
            received_count = 0;

            safe_to_access_buffer.Release();
        }

        // to keep track of the incoming serial data size
        private void inc_received_count()
        {
            received_count += 1;
        }

        private void m_pSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            safe_to_access_buffer.WaitOne();

            int length = m_pSerialPort.BytesToRead;
            while (length-- > 0)
            {
                m_pSerialPort.Read(received_buffer, received_count, 1);
                inc_received_count();
            }

            safe_to_access_buffer.Release();

        }

        #endregion

        #region low level communication helpers/parsers

        private int quick_identify()
        {
            int ret_val = 0;
            try
            {
                string s = quick_clear_write_read("*IDN?\n");

                // check to make sure this is [probably] the power supply device
                if (s.IndexOf("Agilent Technologies") >= 0)
                {
                    current_device = s;

                    if (s.Length > 3)
                        s = s.Substring(0, s.Length - 2);

                    status_bar.Text = "Found device: " + s;

                    // don't enable logging until a device is found
                    change_ui_to_identified();

                    ret_val = 1;
                }
                else
                {
                    current_device = "";

                    ret_val = -1;
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong getting device id");
                safe_to_access_status.WaitOne();
                current_status = connection_status.undefined;
                safe_to_access_status.Release();

                ret_val = -2;
            }

            return ret_val;
        }

        private int quick_limit_read()
        {
            int ret_val = 0;
            string s;

            try
            {
                // read the current and voltage limits.
                // the response should be in the format "--.-----,--.-----"\n
                s = quick_clear_write_read("APPL?\n");
                string s_volt_limit = s.Substring(1, s.IndexOf(',') - 1);
                string s_curr_limit = s.Substring(s.IndexOf(',') + 1, s.Length - s.IndexOf(',') - 4);

                Console.WriteLine("current_limit: [{0}] voltage_limit: [{1}]", s_curr_limit, s_volt_limit);

                double voltage_limit;
                double current_limit;

                try
                {
                    voltage_limit = double.Parse(s_volt_limit);
                }
                catch
                {
                    voltage_limit = double.NaN;
                }

                try
                {
                    current_limit = double.Parse(s_curr_limit);
                }
                catch
                {
                    current_limit = double.NaN;
                }

                // update the text ui
                if (!Double.IsNaN(voltage_limit))
                {
                    thread_safe_voltage_limit_text_update(voltage_limit.ToString(VOLTAGE_TEXT_FORMAT));
                }
                else
                {
                    thread_safe_voltage_limit_text_update(UNKNOWN_VOLTAGE_TEXT);
                }
                if (!Double.IsNaN(current_limit))
                {
                    thread_safe_current_limit_text_update(current_limit.ToString(CURRENT_TEXT_FORMAT));
                }
                else
                {
                    thread_safe_current_limit_text_update(UNKNOWN_CURRENT_TEXT);
                }

                ret_val = 1;
            }
            catch
            {
                Console.WriteLine("Something went wrong getting the limit values");
                ret_val = -1;
            }

            return ret_val;
        }

        // reads the currently selected channel and returns:
        // 1 on channel 1
        // 2 on channel 2
        // -10 on bad parsing or bad response
        // -20 on some other exception
        // -1 on "nothing happened"
        private int quick_channel_read()
        {
            int ret_val = -1;
            string s;

            try
            {

                // after finding a device, check to see what channel is currently being used
                s = quick_clear_write_read("INST:SEL?\n");

                // check to see what output it is
                if (s.Contains("OUTP1"))
                {
                    //current_channel = channel_sel.one;
                    ui_to_channel_1();
                    ret_val = 1;
                }
                else if (s.Contains("OUTP2"))
                {
                    //current_channel = channel_sel.two;
                    ui_to_channel_2();
                    ret_val = 2;
                }
                else
                {
                    //current_channel = channel_sel.undefined;
                    ret_val = -10;
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong getting the currently selected channel");
                ret_val = -20;
            }

            return ret_val;
        }

        // reads whether the output is on or off and returns:
        // 1 on output on
        // 0 on output off
        // -10 on bad parsing of response or bad response
        // -20 on some other error
        // -1 on "nothing happened"
        private int quick_output_onoff_read()
        {
            int ret_val = -1;
            string s;

            try
            {

                // after finding a device, check to see what channel is currently being used
                s = quick_clear_write_read("OUTP:STATE?\n");

                // check to see what output it is
                if (s.Contains("1"))
                {
                    ui_to_output_on();
                    ret_val = 1;
                }
                else if (s.Contains("0"))
                {
                    ui_to_output_off();
                    ret_val = 0;
                }
                else
                {
                    ui_to_output_undefined();
                    ret_val = -10;
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong reading whether the output is on or off");
                ret_val = -20;
            }

            return ret_val;
        }

        private int quick_ovp_read()
        {
            int ret_val = 0;
            string s;

            try
            {
                // read the over voltage protection limit.
                // the response should be in the format scientific double format: [+|-]-.--------E--
                s = quick_clear_write_read("VOLT:PROT?\n");

                double ovp_limit;

                try
                {
                    ovp_limit = double.Parse(s);
                }
                catch
                {
                    ovp_limit = double.NaN;
                }

                // update the text ui
                if (!Double.IsNaN(ovp_limit))
                {
                    thread_safe_ovp_limit_text_update(ovp_limit.ToString(OVP_TEXT_FORMAT));
                }
                else
                {
                    thread_safe_ovp_limit_text_update(UNKNOWN_OVP_TEXT);
                }

                ret_val = 1;
            }
            catch
            {
                Console.WriteLine("Something went wrong getting the over voltage protection value");
                ret_val = -1;
            }

            return ret_val;
        }

        // reads the current power mode and returns:
        // 1 on 8V/3A
        // 2 on 20V/1.5A
        // 3 on 35V
        // 4 on 60V
        // -10 on bad parsing of response or bad response
        // -20 on some other error
        // -1 on "nothing happened"
        private int quick_power_mode_read()
        {
            int ret_val = -1;
            try
            {
                string s = quick_clear_write_read("VOLT:RANG?\n");

                if (s.Contains(power_mode_to_raw_text(power_mode.P8V)))
                {
                    current_mode = power_mode.P8V;
                    ret_val = 1;
                }
                else if (s.Contains(power_mode_to_raw_text(power_mode.P20V)))
                {
                    current_mode = power_mode.P20V;
                    ret_val = 2;
                }
                else if (s.Contains(power_mode_to_raw_text(power_mode.P35V)))
                {
                    current_mode = power_mode.P35V;
                    ret_val = 4;
                }
                else if (s.Contains(power_mode_to_raw_text(power_mode.P60V)))
                {
                    current_mode = power_mode.P60V;
                    ret_val = 5;
                }
                else
                {
                    current_mode = power_mode.undefined;
                    ret_val = -10;
                }

                cbModes.Text = power_mode_to_ui_text(current_mode);
                return ret_val;
            }
            catch
            {
                MessageBox.Show("Something went wrong reading the current power mode");
                ret_val = -20;
            }

            return ret_val;
        }

        // hey! watch the new line characters, because none are added here!
        private string quick_clear_write_read(string what_to_write)
        {
            if (!m_pSerialPort.IsOpen)
            {
                Console.WriteLine("Warning! Attempting to write to closed serial port!");
                return "";
            }

            try
            {

                string response = "";

                if (what_to_write == "")
                {
                    Console.WriteLine("Attempting to send null string to device, aborting.");
                    return "";
                }

                Console.WriteLine("About to send the following to device: \n{0}", what_to_write);

                clear_buffer();

                m_pSerialPort.Write(what_to_write);

                // wait for a response -- note that this time is static, this may need to be adjusted based on baud
                Thread.Sleep(SERIAL_RESPONSE_WAIT_TIME);

                // received buffer is filled in the event handler for the serial port device
                safe_to_access_buffer.WaitOne();
                int len = 0;
                while (received_buffer[len] != '\0')
                    len++;
                response = new string(received_buffer, 0, len);
                safe_to_access_buffer.Release();

                if (len < 1 || response == "")
                {
                    Console.WriteLine("Received no response. (this might be normal)");
                }
                else
                {
                    Console.WriteLine("Received response: " + response);
                }

                return response;
            }
            catch
            {
                Console.WriteLine("Error! Something went wrong in quick_clear_write_read! what_to_write={0}", what_to_write);
                return "";
            }
        }

        // will change the output of the power supply
        // 0=off
        // 1=on
        private void change_output(int state)
        {
            if (state != 0 && state != 1)
            {
                Console.WriteLine("Attempting to change to unknown state! (value={0})", state.ToString());
                return;
            }

            connection_status t = pause_polling_thread();

            thread_safe_update_status_bar("Changing output.");

            // send the change output to x command
            string s = "";
            s = quick_clear_write_read("OUTP:STAT " + (state == 0 ? "OFF" : "ON") + "\n");

            current_status = t;

            if (state == 0)
            {
                ui_to_output_off();

                // stop the polling thread (pulse below)
                safe_to_access_status.WaitOne();
                current_status = connection_status.connected;
                safe_to_access_status.Release();
            }
            else if (state == 1)
            {
                ui_to_output_on();

                // resume the polling thread
                safe_to_access_status.WaitOne();
                current_status = connection_status.identified;
                safe_to_access_status.Release();
            }
            else
            {
                ui_to_output_undefined();
            }

            // clear existing output
            // the limits, current channel, and ovp can stay
            thread_safe_current_text_update(UNKNOWN_CURRENT_TEXT);
            thread_safe_voltage_text_update(UNKNOWN_VOLTAGE_TEXT);
            thread_safe_power_text_update(UNKNOWN_POWER_TEXT);

            resume_polling_thread(t);

        }

        private void change_voltage_limit(decimal volt_limit)
        {
            if (volt_limit < 0)
            {
                Console.WriteLine("Attempting to change to a negative voltage! v={0}", volt_limit);
                return;
            }

            connection_status t = pause_polling_thread();

            thread_safe_update_status_bar("Changing voltage.");

            // send the change voltage to x command
            string s = "";
            s = quick_clear_write_read("VOLT " +
                volt_limit.ToString() + "\n");

            resume_polling_thread(t);
        }

        private void change_current_limit(decimal curr_limit)
        {
            if (curr_limit < 0)
            {
                Console.WriteLine("Attempting to change to a negative current! v={0}", curr_limit);
                return;
            }

            connection_status t = pause_polling_thread();

            thread_safe_update_status_bar("Changing current.");

            // send the change voltage to x command
            string s = "";
            s = quick_clear_write_read("CURR " +
                curr_limit.ToString() + "\n");

            resume_polling_thread(t);
        }

        private void change_power_mode(power_mode pm)
        {
            if (pm != power_mode.P8V && pm != power_mode.P20V && pm != power_mode.P35V && pm != power_mode.P60V)
            {
                Console.WriteLine("Attempting to change to bad power mode! mode={0}", power_mode_to_raw_text(pm));
                return;
            }

            connection_status t = pause_polling_thread();

            thread_safe_update_status_bar("Changing power mode.");

            // send the change power mode to x command
            string s = "";
            // note that there are no asterisks regardless of what the manual would have you believe
            s = quick_clear_write_read("VOLT:RANG " +
                power_mode_to_raw_text(pm) + "\n");

            current_mode = pm;

            resume_polling_thread(t);
        }

        private void change_channel(int chan)
        {
            if (chan != 1 && chan != 2)
            {
                //current_channel = channel_sel.undefined;
                Console.WriteLine("Attempting to change to bad channel, aborting!");
                return;
            }

            // clear existing output
            thread_safe_current_limit_text_update(UNKNOWN_CURRENT_TEXT);
            thread_safe_voltage_limit_text_update(UNKNOWN_VOLTAGE_TEXT);
            thread_safe_ovp_limit_text_update(UNKNOWN_OVP_TEXT);
            thread_safe_current_text_update(UNKNOWN_CURRENT_TEXT);
            thread_safe_voltage_text_update(UNKNOWN_VOLTAGE_TEXT);
            thread_safe_power_text_update(UNKNOWN_POWER_TEXT);

            connection_status t = pause_polling_thread();

            thread_safe_update_status_bar("Changing channel");

            // send the switch to channel x command
            string s = "";
            s = quick_clear_write_read("INST:NSEL " + chan.ToString() + "\n");

            // it seems changing the selected output takes just a little while for the power supply to process
            // before it will respond to commands, so wait just a bit
            Thread.Sleep(700);

            // set the current channel
            if (chan == 1)
            {
                //current_channel = channel_sel.one;
                ui_to_channel_1();
            }
            else if (chan == 2)
            {
                //current_channel = channel_sel.two;
                ui_to_channel_2();
            }
            else
            {
                //current_channel = channel_sel.undefined;
            }

            thread_safe_update_status_bar("Reading limit values");

            // don't forget to update the limits!
            quick_limit_read();
            quick_ovp_read();

            resume_polling_thread(t);
        }

        private connection_status pause_polling_thread()
        {
            thread_safe_update_status_bar("Stopping pending communications.");

            connection_status t = connection_status.undefined;

            // first, stop the polling thread
            lock (monitor_lock)
            {
                // attempt to wait, but if it's already stopped, then timeout
                if (current_status == connection_status.polling)
                {
                    Monitor.Wait(monitor_lock);

                    // wait for any pending commands to finish
                    Thread.Sleep(THREAD_AFTER_INTERRUPT_WAIT);

                    //Console.WriteLine("pausing polling thread (connection_status=polling)");
                    t = connection_status.polling;
                }
                else
                {
                    Monitor.Wait(monitor_lock, THREAD_WAIT_TIMEOUT);
                    safe_to_access_status.WaitOne();
                    t = current_status;
                    safe_to_access_status.Release();
                    //Console.WriteLine("pausing polling thread (connection_status={0})", connection_status_to_string(t));
                }

                safe_to_access_status.WaitOne();
                current_status = connection_status.unconnected;
                safe_to_access_status.Release();
            }

            return t;

        }

        private void resume_polling_thread(connection_status c)
        {
            //  don't let the polling thread read in the response from the previous commands
            clear_buffer();

            // release the polling thread, if it was stopped earlier
            lock (monitor_lock)
            {
                safe_to_access_status.WaitOne();
                current_status = c;
                safe_to_access_status.Release();

                //Console.WriteLine("Resuming polling thread, status={0}", connection_status_to_string(c));

                if (current_status == connection_status.polling)
                    Monitor.Pulse(monitor_lock);
            }

            thread_safe_update_status_bar("Ready");
        }

        #endregion

        #region main background thread

        // this is the main part of the application, and it happens mostly in the background.
        // after connecting, the device needs to be ID'd. After that succeeds, polling will
        // commence to continuously retrieve the current and voltage values. The user can 
        // choose to "start logging" which will save these polled values to a file in realtime;
        // no values are stored for playback or anything.
        void thread_invoke()
        {
            int ok_to_go = 0;
            int do_it_twice = 0;
            double volt;
            double curr;
            bool monitor_response = true;

            // run this thread until the program ends
            while (true)
            {
                // wait for a device to be found...
                ok_to_go = 0;

                lock (monitor_lock)
                {
                    while (ok_to_go == 0)
                    {
                        //Console.WriteLine("Checking current_status...");

                        if (current_status == connection_status.polling)
                        {
                            //Console.WriteLine("current_status=polling");
                            ok_to_go = 1;

                            // true on interrupt, false on timeout
                            if (poll_time_in_ms - SERIAL_RESPONSE_WAIT_TIME > 10)
                                monitor_response = Monitor.Wait(monitor_lock, poll_time_in_ms - SERIAL_RESPONSE_WAIT_TIME);
                            else
                                monitor_response = Monitor.Wait(monitor_lock, poll_time_in_ms);
                        }
                        else if (current_status == connection_status.identified)
                        {
                            //Console.WriteLine("current_status=identified");
                            // if it's identified, we're about to begin polling
                            safe_to_access_status.WaitOne();
                            current_status = connection_status.polling;
                            safe_to_access_status.Release();

                            // true on interrupt, false on timeout
                            if (poll_time_in_ms - SERIAL_RESPONSE_WAIT_TIME > 10)
                                monitor_response = Monitor.Wait(monitor_lock, poll_time_in_ms - SERIAL_RESPONSE_WAIT_TIME);
                            else
                                monitor_response = Monitor.Wait(monitor_lock, poll_time_in_ms);
                        }
                        else
                        {
                            //Console.WriteLine("current_status=else");
                            // all other cases need to pause this thread indefinitely
                            ok_to_go = 0;

                            Monitor.Wait(monitor_lock);
                            monitor_response = false;
                        }

                        // if this was woken up early, then actually don't do anything
                        if (monitor_response == true)
                        {
                            ok_to_go = 0;
                        }

                        // it's possible that the status changed while inside Monitor.Wait, so make
                        // sure that this thread is still polling
                        safe_to_access_status.WaitOne();
                        if (current_status != connection_status.polling)
                        {
                            ok_to_go = 0;
                        }
                        safe_to_access_status.Release();
                    }
                }

                //Console.WriteLine("Done with lock -- ok_to_go={0}, monitor_response={1}, current_status={2}", ok_to_go, (monitor_response ? "true" : "false"), connection_status_to_string(current_status));

                volt = 0.0;
                curr = 0.0;


                // the voltage and current code are practically the same, so those two
                // are going to be done in a loop. The first time is current, the second
                // is voltage
                    
                clear_buffer();

                //Console.WriteLine("Sending curr/volt command.");

                // disconnecting at the wrong time can cause this thread to 
                // attempt to write to a com port that doesn't exist
                try
                {
                    if (do_it_twice == 0)
                    {   
                        m_pSerialPort.Write("MEAS:CURR?\n");
                    }
                    else if (do_it_twice == 1)
                    {
                        m_pSerialPort.Write("MEAS:VOLT?\n");
                    }
                }
                catch
                {
                }

                // wait for a response from the device. This can probably be fine tuned a little
                // based on the baud rate and other factors.
                Thread.Sleep(SERIAL_RESPONSE_WAIT_TIME);

                // received buffer is filled in the event handler for the serial port device
                string s;
                safe_to_access_buffer.WaitOne();
                int len = 0;
                while (received_buffer[len] != '\0')
                    len++;
                s = new string(received_buffer, 0, len);
                safe_to_access_buffer.Release();

                // the response is going to be a number in scientific notation
                double d = new double();
                try
                {
                    d = double.Parse(s);
                }
                catch
                {
                    d = double.NaN;
                    if (ckBaudErrorFix.Checked == true)
                    {
                        // scale the error correcting time based on the baud rate.
                        // lower bauds have to move in larger amounts than higher baud rates
                        int baud = thread_safe_get_baud_rate();
                        if (baud == 0)
                            baud = 1;
                        int move_factor = (int)(336000/baud);
                        if (move_factor == 0)
                            move_factor = 1;

                        poll_time_in_ms += move_factor;
                        thread_safe_poll_delay_text_update(poll_time_in_ms.ToString());
                        // static wait period. The power supply needs some time to recover
                        Thread.Sleep(250);
                    }
                }

                // update the UI, and set the current and voltage values that will
                // be written to the log file.

                if (do_it_twice == 0 && current_status == connection_status.polling)
                {
                    curr = d;
                    Console.WriteLine("received current response: {0}", d);
                    if (System.Double.IsNaN(d))
                    {
                        thread_safe_current_text_update(UNKNOWN_CURRENT_TEXT);
                    }
                    else
                    {
                        thread_safe_current_text_update(d.ToString(CURRENT_TEXT_FORMAT));
                    }
                }
                else if (do_it_twice == 1 && current_status == connection_status.polling)
                {
                    volt = d;
                    Console.WriteLine("received voltage response: {0}", d);
                    if (System.Double.IsNaN(d))
                    {
                        thread_safe_voltage_text_update(UNKNOWN_VOLTAGE_TEXT);
                    }
                    else
                    {
                        thread_safe_voltage_text_update(d.ToString(VOLTAGE_TEXT_FORMAT));
                    }
                }

                do_it_twice++;
                if (do_it_twice == 2)
                    do_it_twice = 0;

                // update power usage
                if (!Double.IsNaN(curr) && !Double.IsNaN(volt) && current_status == connection_status.polling)
                {
                    thread_safe_power_text_update((curr * volt).ToString(POWER_TEXT_FORMAT));
                }
                else
                {
                    thread_safe_power_text_update(UNKNOWN_POWER_TEXT);
                }


                // done with both voltage and current
                // check to see if they need to be saved to a file
                if (log_to_file_now == true)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(DateTime.Now.ToString());
                    sb.Append(", ");
                    sb.Append(volt.ToString());
                    sb.Append(", ");
                    sb.AppendLine(curr.ToString());

                    byte[] ba = Encoding.ASCII.GetBytes(sb.ToString());
                    file_to_save_to.Write(ba, 0, ba.Length);
                }

                // if someone else is trying to halt this thread, allow that to happen now.
                lock (monitor_lock)
                {
                    //Console.WriteLine("pulsing monitor_lock. (end of main loop)");
                    Monitor.Pulse(monitor_lock);
                }

                // also, attempt to re-sync the status bar
                pbPollCountdown.BeginInvoke((ThreadStart)delegate
                {
                    lock (pbPollCountdown)
                    {
                        pbPollCountdown.Value = 0;
                    }
                });

            } // end of while(true)


        } // end of polling thread

        // timer for countdown bar
        // note that this gets out of sync after a while
        private void progress_bar_tick_run(object data)
        {
            // first of all, find out if the polling thread is running
            connection_status c;
            safe_to_access_status.WaitOne();
            c = current_status;
            safe_to_access_status.Release();

            if (c != connection_status.polling)
            {
                pbPollCountdown.BeginInvoke((ThreadStart)delegate
                {
                    lock (pbPollCountdown)
                    {
                        pbPollCountdown.Value = 100;
                    }
                });

                // check back in 100ms
                progress_bar_tick.Change(100, 100);
                return;
            }

            // getting here means the polling thread is running.
            // change the tick time to something reasonable
            if (poll_time_in_ms > 0)
            {
                progress_bar_tick.Change(poll_time_in_ms / 10, poll_time_in_ms / 10);
            }

            int p_value = 0;
            // update the countdown status bar
            pbPollCountdown.BeginInvoke((ThreadStart)delegate
            {
                lock (pbPollCountdown)
                {
                    try
                    {
                        p_value = pbPollCountdown.Value;
                    }
                    catch
                    {
                        p_value = 0;
                    }

                    p_value -= 10;

                    if (p_value <= 0)
                        p_value += 100;

                    pbPollCountdown.Value = p_value;
                }
            });

        }

        #endregion

        #region direct ui interaction (button clicks, etc)

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                #region Set Serial Settings

                m_pSerialPort.BaudRate = Convert.ToInt32(cbBaudRate.Text);
                m_pSerialPort.DataBits = Convert.ToInt32(cbDataBits.Text);
                switch (cbParity.Text)
                {
                    case "None": m_pSerialPort.Parity = Parity.None; break;
                    case "Odd": m_pSerialPort.Parity = Parity.Odd; break;
                    case "Even": m_pSerialPort.Parity = Parity.Even; break;
                    default:
                        break;
                }
                switch (cbStopBits.Text)
                {
                    case "1": m_pSerialPort.StopBits = StopBits.One; break;
                    case "2": m_pSerialPort.StopBits = StopBits.Two; break;
                    default:
                        break;
                }
                m_pSerialPort.PortName = cbPort.Text;


                m_pSerialPort.DtrEnable = true;
                m_pSerialPort.RtsEnable = true;
                

                #endregion

                m_pSerialPort.Open();

                status_bar.Text = "Connected to " + m_pSerialPort.PortName.ToString() + " at " + m_pSerialPort.BaudRate.ToString();
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;

                safe_to_access_status.WaitOne();
                current_status = connection_status.connected;
                safe_to_access_status.Release();

                change_ui_to_connected();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "COM Port Connection Problem");
                safe_to_access_status.WaitOne();
                current_status = connection_status.unconnected;
                safe_to_access_status.Release();
                return;
            }

        }

        /// <summary>
        /// Happens when the disconnect button is clicked to disconnect
        /// the serial port from the device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            safe_to_access_status.WaitOne();
            current_status = connection_status.unconnected;
            safe_to_access_status.Release();
            
            btnDisconnect.Enabled = false;
            btnConnect.Enabled = true;
            status_bar.Text = "Disconnected";

            // wait just a bit to see if any current communications will finish
            Thread.Sleep(300);

            // I believe closing the port will always trigger an error
            m_pSerialPort.Close();

            change_ui_to_disconnected();
        }

        private void btnStopStart_Click(object sender, EventArgs e)
        {
            UInt64 i = lock_connected_ui();

            // the thread is running and needs to be stopped
            if (btnStopStart.Text == "Stop")
            {

                thread_safe_update_status_bar("Stopping pending communications.");

                // first, stop the polling thread
                lock (monitor_lock)
                {
                    // attempt to wait, but if it's already stopped, then timeout
                    if (current_status == connection_status.polling)
                    {
                        Monitor.Wait(monitor_lock);

                        // wait for any pending commands to finish
                        Thread.Sleep(THREAD_AFTER_INTERRUPT_WAIT);

                    }
                    else
                        Monitor.Wait(monitor_lock, THREAD_WAIT_TIMEOUT);

                    safe_to_access_status.WaitOne();
                    current_status = connection_status.connected;
                    safe_to_access_status.Release();
                }
                thread_safe_update_status_bar("Ready.");
                btnStopStart.Text = "Start";
            }
            // the thread is stopped and needs to be resumed
            else if (btnStopStart.Text == "Start")
            {
                lock (monitor_lock)
                {
                    safe_to_access_status.WaitOne();
                    current_status = connection_status.identified;
                    safe_to_access_status.Release();

                    Monitor.Pulse(monitor_lock);

                }
                btnStopStart.Text = "Stop";
            }

            unlock_connected_ui(i);
        }

        private void btnChannel1_Click(object sender, EventArgs e)
        {
            UInt64 i = lock_connected_ui();
            change_channel(1);
            unlock_connected_ui(i);
        }

        private void btnChannel2_Click(object sender, EventArgs e)
        {
            UInt64 i = lock_connected_ui();
            change_channel(2);
            unlock_connected_ui(i);
        }

        private void btnOutputOn_Click(object sender, EventArgs e)
        {
            UInt64 i = lock_connected_ui();
            change_output(1);
            unlock_connected_ui(i);
            btnStopStart.Text = "Stop";
        }

        private void btnOutputOff_Click(object sender, EventArgs e)
        {
            UInt64 i = lock_connected_ui();
            change_output(0);
            unlock_connected_ui(i);
            btnStopStart.Text = "Start";
        }

        private void btnSetMode_Click(object sender, EventArgs e)
        {
            UInt64 i = lock_connected_ui();

            change_power_mode(ui_text_to_power_mode(cbModes.Text));

            // make sure the limits haven't changed
            // note: the big functions change the status bar text, but these quick functions don't
            thread_safe_update_status_bar("Reading limit values");
            quick_limit_read();
            thread_safe_update_status_bar("Ready");

            update_max_values();

            unlock_connected_ui(i);
        }


        // changes state from connected to ID'd, will start polling continuously on success
        private void btnIdentify_Click(object sender, EventArgs e)
        {
            // return if not connected
            if (btnConnect.Enabled == true)
                return;

            // going to do a couple things before letting the user proceed,
            // but all of these checks need to be successful
            int init_device = 0;

            init_device = quick_identify();
            if (init_device <= 0)
            {
                return;
            }

            // failing a limit read isn't fatal, so the return value can be ignored
            quick_limit_read();

            // failling to identify the current channel is not fatal, so the return value can be ignored
            quick_channel_read();

            // failing to read the over voltage protection value is not fatal, so the return value can be ignored
            quick_ovp_read();

            // If the output is currently off, there's no need to start the polling thread yet
            int output = quick_output_onoff_read();
            btnStopStart.Text = (output==0?"Start":"Stop");

            // now that the device is known, list the available power modes
            populate_modes(); // adds available items
            quick_power_mode_read(); // reads current power mode and selects it in the combo box

            // and update the max values in the 'set' boxes
            update_max_values();
            
            // start the polling if things are good to go
            if (init_device > 0 && output == 1)
            {
                safe_to_access_status.WaitOne();
                current_status = connection_status.identified;
                safe_to_access_status.Release();

                lock (monitor_lock)
                {
                    Monitor.Pulse(monitor_lock);
                }
            }
            else
            {
                safe_to_access_status.WaitOne();
                current_status = connection_status.undefined;
                safe_to_access_status.Release();
            }

            return;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                if (log_to_file_now)
                    file_to_save_to.Close();
            }
            catch
            {
                Console.WriteLine("ERROR -- Attempted to close logging file but failed!");
            }

            try
            {
                // make sure to kill child threads
                if (thready.IsAlive)
                {
                    thready.Abort();
                }
            }
            catch { }

            try
            {
                progress_bar_tick.Dispose();
            }
            catch { }
        }

        // creates file to save to, turns on logging
        private void btnLog_Click(object sender, EventArgs e)
        {
            if (btnLog.Text == "Start Logging")
            {
                SaveFileDialog my_save = new SaveFileDialog();
                my_save.Title = "Save file";
                my_save.ShowDialog();
                if (my_save.FileName != "")
                {
                    file_to_save_to = (System.IO.FileStream)my_save.OpenFile();

                    // save a "first line" to indicate start of logging
                    string sb = "About to start logging.\n";
                    byte[] ba = Encoding.ASCII.GetBytes(sb.ToString());
                    file_to_save_to.Write(ba, 0, ba.Length);
                    sb = DateTime.Now.ToString() + ", Voltage, Current\n";
                    ba = Encoding.ASCII.GetBytes(sb.ToString());
                    file_to_save_to.Write(ba, 0, ba.Length);
                    // done with that

                    log_to_file_now = true;
                    btnLog.Text = "Stop Logging";
                }
            }
            else
            {
                log_to_file_now = false;
                file_to_save_to.Close();
                btnLog.Text = "Start Logging";
            }

        }

        private void btnSetPollFrequency_Click(object sender, EventArgs e)
        {
            UInt64 i = lock_connected_ui();

            try
            {
                poll_time_in_ms = int.Parse(cbPollDelay.Text);
            }
            catch
            {
                poll_time_in_ms = DEFAULT_POLL_TIME_IN_MS;
                cbPollDelay.Text = poll_time_in_ms.ToString();
            }

            if (cbPollTimeFormat.SelectedIndex == 1)
            {
                poll_time_in_ms *= 1000;
            }
            else if (cbPollTimeFormat.SelectedIndex == 2)
            {
                poll_time_in_ms *= (60 * 1000);
            }
            else
            {
                cbPollTimeFormat.SelectedIndex = 0;
            }

            // if the polling thread is sleeping, it should be reset.
            lock (monitor_lock)
            {
                if (current_status == connection_status.polling)
                    Monitor.Pulse(monitor_lock);
            }

            unlock_connected_ui(i);

        }

        private void btnBeep_Click(object sender, EventArgs e)
        {
            quick_clear_write_read("SYST:BEEP\n");
        }

        private void btnSCPIVersion_Click(object sender, EventArgs e)
        {
            string s;
            s = quick_clear_write_read("SYST:VERS?\n");
            txtSCPIVersion.Text = s;
        }

        private void btnGetOldestError_Click(object sender, EventArgs e)
        {
            string s;
            s = quick_clear_write_read("SYST:ERR?\n");
            txtErrors.Text = s;
        }

        private void btnID_Click(object sender, EventArgs e)
        {
            string s;
            s = quick_clear_write_read("*IDN?\n");
            txtID.Text = s;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!m_pSerialPort.IsOpen)
                return;
            byte[] b = { 0x03 };
            m_pSerialPort.Write(b, 0, 1);

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            quick_clear_write_read("*RST\n");
        }

        private void btnSelfTest_Click(object sender, EventArgs e)
        {
            quick_clear_write_read("*TST?\n");
        }

        private void btnClearErrors_Click(object sender, EventArgs e)
        {
            status_bar.Text = "Clearing errors. This could take a moment";
            string s;
            txtErrors.Text += "\n";

            for (int i = 0; i < 20; i++)
            {
                s = quick_clear_write_read("SYST:ERR?\n");
                if (s.Contains("+0,\"No error\""))
                    break;
                txtErrors.Text += s;
            }

            status_bar.Text = "Ready";
        }

        private void btnRS232Local_Click(object sender, EventArgs e)
        {
            quick_clear_write_read("SYST:LOC\n");
        }

        private void btnRS232Remote_Click(object sender, EventArgs e)
        {
            quick_clear_write_read("SYST:REM\n");
        }

        private void btnRS232RWLock_Click(object sender, EventArgs e)
        {
            quick_clear_write_read("SYST:RWL\n");
        }

        private void btnClearDisplayText_Click(object sender, EventArgs e)
        {
            quick_clear_write_read("DISP:TEXT:CLE\n");
            txtDisplayText.Text = "";
        }

        private void btnDisplayText_Click(object sender, EventArgs e)
        {
            string working_string = txtDisplayText.Text;
            working_string = working_string.ToUpper();

            // laeve the text box blank to read...
            if (working_string.Length == 0)
            {
                string s = quick_clear_write_read("DISP:TEXT?\n");
                txtDisplayText.Text = s;
                return;
            }

            // acceptable character to display are A-Z, comma and decimal.

            string to_send = "";
            char c;
            int char_count = 0; // max character is 11, but commas and decimals don't count

            for (int i = 0; i < working_string.Length; i++)
            {
                c = working_string[i];
                if (c == ',' || c == '.' || c == ';')
                {
                    to_send += c;
                }
                else if ((c >= (char)'A' && c <= (char)'Z') ||
                    (c >= (char)'0' && c <= (char)'9'))
                {
                    to_send += c;
                    char_count++;
                }

                if (char_count == 11)
                    break;
            }

            if (char_count > 0)
            {
                quick_clear_write_read("DISP:TEXT \"" + to_send + "\"\n");
            }

            txtDisplayText.Text = to_send;
        }

        private void btnSetVoltageLimit_Click(object sender, EventArgs e)
        {
            UInt64 i = lock_connected_ui();

            change_voltage_limit(nmVoltageLimitInput.Value);

            quick_limit_read();

            unlock_connected_ui(i);
        }

        private void btnSetCurrentLimit_Click(object sender, EventArgs e)
        {
            UInt64 i = lock_connected_ui();

            change_current_limit(nmCurrentLimitInput.Value);

            quick_limit_read();

            unlock_connected_ui(i);
        }


        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This program is designed to be used with an Agilent power supply, specifically the E3646A (though it should work with any E364xA device). It will periodically poll the power supply over the serial port to collect voltage and current; this can be logged to a text file in real time.\n\n" +
                "Ben Burns\n\nApril 12, 2012");
        }

        // because the user can change the power supply to a different channel on the hardware and there is no update to 
        // any connected serial devices, this software must be able to change to any channel at any time.
        // For example, the software could be on channel 1, but if the user changes to channel 2 via hardware, the software
        // must accept "change to channel 1" command because it's valid

        #endregion

        #region cross thread ui update functions

        
        private void thread_safe_update_status_bar(string new_text)
        {
            // ha, nothing special!
            status_bar.Text = new_text;
        }

        private void thread_safe_poll_delay_text_update(string new_text)
        {
            // using begininvoke because it's alright to be asynchronous
            cbPollDelay.BeginInvoke((ThreadStart)delegate
            {
                lock (cbPollDelay)
                {
                    cbPollDelay.Text = new_text;
                }
            });
        }

        private void thread_safe_power_text_update(string new_text)
        {
            // using begininvoke because it's alright to be asynchronous
            lblPower.BeginInvoke((ThreadStart)delegate
            {
                lock (lblPower)
                {
                    lblPower.Text = new_text;
                }
            });
        }

        private void thread_safe_voltage_text_update(string new_text)
        {
            // using begininvoke because it's alright to be asynchronous
            lblVoltage.BeginInvoke((ThreadStart)delegate
            {
                lock (lblVoltage)
                {
                    lblVoltage.Text = new_text;
                }
            });
        }

        private void thread_safe_current_text_update(string new_text)
        {
            // using begininvoke because it's alright to be asynchronous
            lblCurrent.BeginInvoke((ThreadStart)delegate
            {
                lock (lblCurrent)
                {
                    lblCurrent.Text = new_text;
                }
            });
        }

        private void thread_safe_current_limit_text_update(string new_text)
        {
            // using begininvoke because it's alright to be asynchronous
            lblCurrentLimit.BeginInvoke((ThreadStart)delegate
            {
                lock (lblCurrentLimit)
                {
                    lblCurrentLimit.Text = new_text;
                }
            });
        }

        private void thread_safe_voltage_limit_text_update(string new_text)
        {
            // using begininvoke because it's alright to be asynchronous
            lblVoltageLimit.BeginInvoke((ThreadStart)delegate
            {
                lock (lblVoltageLimit)
                {
                    lblVoltageLimit.Text = new_text;
                }
            });
        }

        private void thread_safe_ovp_limit_text_update(string new_text)
        {
            // using begininvoke because it's alright to be asynchronous
            lblOVP.BeginInvoke((ThreadStart)delegate
            {
                lock (lblOVP)
                {
                    lblOVP.Text = new_text;
                }
            });
        }

        private int thread_safe_get_baud_rate()
        {
            int baud = 0;
            // using invoke because this must execute in order
            cbBaudRate.Invoke((ThreadStart)delegate
            {
                lock (cbPollDelay)
                {
                    try
                    {
                        baud = int.Parse(cbBaudRate.Text);
                    }
                    catch
                    {
                        baud = 0;
                    }
                }
            });

            return baud;
        }

        #endregion

        #region UI helper functions

        // for parsing a response from the power supply
        private string power_mode_to_raw_text(power_mode pm)
        {
            switch(pm)
            {
                case power_mode.P8V: return "P8V";
                case power_mode.P20V: return "P20V";
                case power_mode.P35V: return "P35V";
                case power_mode.P60V: return "P60V";
                case power_mode.undefined: 
                default:
                    return "undefined";
            }
        }

        // for displaying text to the user
        private string power_mode_to_ui_text(power_mode pm)
        {
            switch (pm)
            {
                case power_mode.P8V: return "8V/3A";
                case power_mode.P20V: return "20V/1.5A";
                case power_mode.P35V: return "35V";
                case power_mode.P60V: return "60V";
                case power_mode.undefined:
                default:
                    return "undefined";
            }
        }

        // for converting the ui text back to a powermode
        private power_mode ui_text_to_power_mode(string text)
        {
            switch (text)
            {
                case "8V/3A": return power_mode.P8V;
                case "20V/1.5A": return power_mode.P20V;
                case "35V": return power_mode.P35V;
                case "60V": return power_mode.P60V;
                case "undefined":
                default:
                    return power_mode.undefined;
            }
        }

        private string connection_status_to_string(connection_status c)
        {
            switch (c)
            {
                case connection_status.connected: return "connected";
                case connection_status.done: return "done";
                case connection_status.identified: return "identified";
                case connection_status.polling: return "polling";
                case connection_status.unconnected: return "unconnected";

                case connection_status.undefined: 
                default:
                    return "undefined";
            }
        }

        private void populate_modes()
        {
            if (current_device.Contains("E3646A"))
            {
                if (!cbModes.Items.Contains(power_mode_to_ui_text(power_mode.P8V)))
                    cbModes.Items.Add(power_mode_to_ui_text(power_mode.P8V));
                if (!cbModes.Items.Contains(power_mode_to_ui_text(power_mode.P20V)))
                    cbModes.Items.Add(power_mode_to_ui_text(power_mode.P20V));
            }

            // make sure this is enabled
            if (cbModes.Items.Count > 0)
            {
                cbModes.Enabled = true;
                btnSetMode.Enabled = true;
            }
        }

        private void change_ui_to_connected()
        {
            cbBaudRate.Enabled = false;
            cbPort.Enabled = false;
            cbParity.Enabled = false;
            cbStopBits.Enabled = false;
            cbDataBits.Enabled = false;

            btnIdentify.Enabled = true;
            btnLog.Enabled = false;
            cbPollDelay.Enabled = true;
            cbPollTimeFormat.Enabled = true;
            ckBaudErrorFix.Enabled = true;
            btnSetPollFrequency.Enabled = true;

            btnOutputOn.Enabled = false;
            btnOutputOff.Enabled = false;

            cbModes.Enabled = false;
            btnSetMode.Enabled = false;

            btnStopStart.Enabled = false;

            nmCurrentLimitInput.Enabled = false;
            btnSetCurrentLimit.Enabled = false;
            nmCurrentInput.Enabled = false;
            btnSetCurrent.Enabled = false;
            nmVoltageLimitInput.Enabled = false;
            btnSetVoltageLimit.Enabled = false;
            nmVoltageInput.Enabled = false;
            btnSetVoltage.Enabled = false;
            nmOverVoltageInput.Enabled = false;
            btnSetOverVoltageLimit.Enabled = false;

            thread_safe_voltage_limit_text_update(UNKNOWN_VOLTAGE_TEXT);
            thread_safe_current_text_update(UNKNOWN_CURRENT_TEXT);
            thread_safe_ovp_limit_text_update(UNKNOWN_OVP_TEXT);
        }

        private void change_ui_to_disconnected()
        {
            cbBaudRate.Enabled = true;
            cbPort.Enabled = true;
            cbParity.Enabled = true;
            cbStopBits.Enabled = true;
            cbDataBits.Enabled = true;

            btnIdentify.Enabled = false;
            cbPollDelay.Enabled = false;
            cbPollTimeFormat.Enabled = false;
            ckBaudErrorFix.Enabled = false;
            btnSetPollFrequency.Enabled = false;

            btnOutputOn.Enabled = false;
            btnOutputOff.Enabled = false;

            cbModes.Enabled = false;
            btnSetMode.Enabled = false;

            btnStopStart.Enabled = false;

            nmCurrentLimitInput.Enabled = false;
            btnSetCurrentLimit.Enabled = false;
            nmCurrentInput.Enabled = false;
            btnSetCurrent.Enabled = false;
            nmVoltageLimitInput.Enabled = false;
            btnSetVoltageLimit.Enabled = false;
            nmVoltageInput.Enabled = false;
            btnSetVoltage.Enabled = false;
            nmOverVoltageInput.Enabled = false;
            btnSetOverVoltageLimit.Enabled = false;

            thread_safe_voltage_limit_text_update(UNKNOWN_VOLTAGE_TEXT);
            thread_safe_current_text_update(UNKNOWN_CURRENT_TEXT);
            thread_safe_ovp_limit_text_update(UNKNOWN_OVP_TEXT);

            change_ui_to_unidentified();
            ui_to_output_undefined();
        }

        private void change_ui_to_identified()
        {
            // don't allow logging until a device is found
            btnLog.Enabled = true;
            btnChannel1.Enabled = true;
            btnChannel2.Enabled = true;

            btnOutputOn.Enabled = true;
            btnOutputOff.Enabled = true;

            if (cbModes.Items.Count > 0)
            {
                cbModes.Enabled = true;
                btnSetMode.Enabled = true;
            }

            btnStopStart.Enabled = true;

            nmCurrentLimitInput.Enabled = true;
            btnSetCurrentLimit.Enabled = true;
            nmCurrentInput.Enabled = true;
            btnSetCurrent.Enabled = true;
            nmVoltageLimitInput.Enabled = true;
            btnSetVoltageLimit.Enabled = true;
            nmVoltageInput.Enabled = true;
            btnSetVoltage.Enabled = true;
            nmOverVoltageInput.Enabled = true;
            btnSetOverVoltageLimit.Enabled = true;
        }

        private void change_ui_to_unidentified()
        {
            btnLog.Enabled = false;
            btnChannel1.Enabled = false;
            btnChannel2.Enabled = false;

            ui_to_channel_undefined();
        }

        private void update_max_values()
        {
            power_mode pm = current_mode;

            if (pm == power_mode.P8V)
            {
                nmCurrentLimitInput.Maximum = 3;
                nmCurrentInput.Maximum = 3;
                nmVoltageLimitInput.Maximum = 8;
                nmVoltageInput.Maximum = 8;
            }
            else if (pm == power_mode.P20V)
            {
                nmCurrentLimitInput.Maximum = (decimal)1.5;
                nmCurrentInput.Maximum = (decimal)1.5;
                nmVoltageLimitInput.Maximum = 20;
                nmVoltageInput.Maximum = 20;
            }
        }

        private void ui_to_channel_1()
        {
            btnChannel1.BackColor = Color.FromArgb(192, 255, 192);
            btnChannel2.BackColor = System.Drawing.SystemColors.Control;
        }

        private void ui_to_channel_2()
        {
            btnChannel1.BackColor = System.Drawing.SystemColors.Control;
            btnChannel2.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void ui_to_channel_undefined()
        {
            btnChannel1.BackColor = System.Drawing.SystemColors.Control;
            btnChannel2.BackColor = System.Drawing.SystemColors.Control;
        }

        private void ui_to_output_off()
        {
            btnOutputOff.BackColor = Color.FromArgb(192, 255, 192);
            btnOutputOn.BackColor = System.Drawing.SystemColors.Control;
        }

        private void ui_to_output_on()
        {
            btnOutputOff.BackColor = System.Drawing.SystemColors.Control;
            btnOutputOn.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void ui_to_output_undefined()
        {
            btnOutputOff.BackColor = System.Drawing.SystemColors.Control;
            btnOutputOn.BackColor = System.Drawing.SystemColors.Control;
        }

        private UInt64 b_to_uint64(bool b)
        {
            return b ? (UInt64)1 : 0;
        }

        private bool uint64_to_b(UInt64 i)
        {
            return i == 0 ? false : true;
        }

        // takes all the controls that are active after identifying and disables them.
        // Additionally, it saves the state of all the controls so that calling unlock will restore the previous state,
        // as opposed to simply enabling them.
        private UInt64 lock_connected_ui()
        {
            UInt64 flags = 0;

            flags |= b_to_uint64(btnOutputOn.Enabled) << 1;
            flags |= b_to_uint64(btnOutputOff.Enabled) << 2;
            flags |= b_to_uint64(btnChannel1.Enabled) << 3;
            flags |= b_to_uint64(btnChannel2.Enabled) << 4;
            flags |= b_to_uint64(cbModes.Enabled) << 5;
            flags |= b_to_uint64(btnSetMode.Enabled) << 6;
            flags |= b_to_uint64(btnSetPollFrequency.Enabled) << 7;
            flags |= b_to_uint64(btnStopStart.Enabled) << 8;

            flags |= b_to_uint64(nmCurrentLimitInput.Enabled) << 9;
            flags |= b_to_uint64(btnSetCurrentLimit.Enabled) << 10;
            flags |= b_to_uint64(nmCurrentInput.Enabled) << 11;
            flags |= b_to_uint64(btnSetCurrent.Enabled) << 12;
            flags |= b_to_uint64(nmVoltageLimitInput.Enabled) << 13;
            flags |= b_to_uint64(btnSetVoltageLimit.Enabled) << 14;
            flags |= b_to_uint64(nmVoltageInput.Enabled) << 15;
            flags |= b_to_uint64(btnSetVoltage.Enabled) << 16;
            flags |= b_to_uint64(nmOverVoltageInput.Enabled) << 17;
            flags |= b_to_uint64(btnSetOverVoltageLimit.Enabled) << 18;


            btnOutputOn.Enabled = false;
            btnOutputOff.Enabled = false;
            btnChannel1.Enabled = false;
            btnChannel2.Enabled = false;
            cbModes.Enabled = false;
            btnSetMode.Enabled = false;
            btnSetPollFrequency.Enabled = false;
            btnStopStart.Enabled = false;

            nmCurrentLimitInput.Enabled = false;
            btnSetCurrentLimit.Enabled = false;
            nmCurrentInput.Enabled = false;
            btnSetCurrent.Enabled = false;
            nmVoltageLimitInput.Enabled = false;
            btnSetVoltageLimit.Enabled = false;
            nmVoltageInput.Enabled = false;
            btnSetVoltage.Enabled = false;
            nmOverVoltageInput.Enabled = false;
            btnSetOverVoltageLimit.Enabled = false;

            return flags;
        }

        // restores the controls to a previous state. call with (uint64)-1 to enable all.
        private void unlock_connected_ui(UInt64 flags)
        {
            btnOutputOn.Enabled = uint64_to_b(flags & (1 << 1));
            btnOutputOff.Enabled = uint64_to_b(flags & (1 << 2));
            btnChannel1.Enabled = uint64_to_b(flags & (1 << 3));
            btnChannel2.Enabled = uint64_to_b(flags & (1 << 4));
            cbModes.Enabled = uint64_to_b(flags & (1 << 5));
            btnSetMode.Enabled = uint64_to_b(flags & (1 << 6));
            btnSetPollFrequency.Enabled = uint64_to_b(flags & (1 << 7));
            btnStopStart.Enabled = uint64_to_b(flags & (1 << 8));

            nmCurrentLimitInput.Enabled = uint64_to_b(flags & (1 << 9));
            btnSetCurrentLimit.Enabled = uint64_to_b(flags & (1 << 10));
            nmCurrentInput.Enabled = uint64_to_b(flags & (1 << 11));
            btnSetCurrent.Enabled = uint64_to_b(flags & (1 << 12));
            nmVoltageLimitInput.Enabled = uint64_to_b(flags & (1 << 13));
            btnSetVoltageLimit.Enabled = uint64_to_b(flags & (1 << 14));
            nmVoltageInput.Enabled = uint64_to_b(flags & (1 << 15));
            btnSetVoltage.Enabled = uint64_to_b(flags & (1 << 16));
            nmOverVoltageInput.Enabled = uint64_to_b(flags & (1 << 17));
            btnSetOverVoltageLimit.Enabled = uint64_to_b(flags & (1 << 18));
        }

        #endregion



        private void btnSetOverVoltageLimit_Click(object sender, EventArgs e)
        {

        }














    }
}
