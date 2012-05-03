namespace Power_Supply_Read
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status_bar = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.btnIdentify = new System.Windows.Forms.Button();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lblVoltage = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLog = new System.Windows.Forms.Button();
            this.cbPollDelay = new System.Windows.Forms.ComboBox();
            this.cbPollTimeFormat = new System.Windows.Forms.ComboBox();
            this.ckBaudErrorFix = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnChannel1 = new System.Windows.Forms.Button();
            this.btnChannel2 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.lblVoltageLimit = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCurrentLimit = new System.Windows.Forms.Label();
            this.btnSetPollFrequency = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnStopStart = new System.Windows.Forms.Button();
            this.lblOVP = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnOutputOn = new System.Windows.Forms.Button();
            this.btnOutputOff = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cbModes = new System.Windows.Forms.ComboBox();
            this.btnSetMode = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nmOverVoltageInput = new System.Windows.Forms.NumericUpDown();
            this.btnSetOverVoltageLimit = new System.Windows.Forms.Button();
            this.nmVoltageInput = new System.Windows.Forms.NumericUpDown();
            this.btnSetVoltage = new System.Windows.Forms.Button();
            this.nmVoltageLimitInput = new System.Windows.Forms.NumericUpDown();
            this.btnSetVoltageLimit = new System.Windows.Forms.Button();
            this.nmCurrentInput = new System.Windows.Forms.NumericUpDown();
            this.btnSetCurrent = new System.Windows.Forms.Button();
            this.nmCurrentLimitInput = new System.Windows.Forms.NumericUpDown();
            this.btnSetCurrentLimit = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDisplayText = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtErrors = new System.Windows.Forms.TextBox();
            this.txtSCPIVersion = new System.Windows.Forms.TextBox();
            this.button11 = new System.Windows.Forms.Button();
            this.btnClearDisplayText = new System.Windows.Forms.Button();
            this.btnDisplayText = new System.Windows.Forms.Button();
            this.btnRS232RWLock = new System.Windows.Forms.Button();
            this.btnRS232Remote = new System.Windows.Forms.Button();
            this.btnRS232Local = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSelfTest = new System.Windows.Forms.Button();
            this.btnID = new System.Windows.Forms.Button();
            this.btnClearErrors = new System.Windows.Forms.Button();
            this.btnGetOldestError = new System.Windows.Forms.Button();
            this.btnSCPIVersion = new System.Windows.Forms.Button();
            this.btnBeep = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pbPollCountdown = new Power_Supply_Read.VerticalProgressBar();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel_bottom.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmOverVoltageInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmVoltageInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmVoltageLimitInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmCurrentInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmCurrentLimitInput)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_bar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 448);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.statusStrip1.Size = new System.Drawing.Size(732, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status_bar
            // 
            this.status_bar.Name = "status_bar";
            this.status_bar.Size = new System.Drawing.Size(38, 17);
            this.status_bar.Text = "Ready";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbDataBits);
            this.groupBox1.Controls.Add(this.btnDisconnect);
            this.groupBox1.Controls.Add(this.cbStopBits);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbParity);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbPort);
            this.groupBox1.Controls.Add(this.cbBaudRate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 52);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Serial Controls";
            // 
            // cbDataBits
            // 
            this.cbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.cbDataBits.Location = new System.Drawing.Point(533, 19);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(39, 21);
            this.cbDataBits.TabIndex = 11;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(650, 15);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(76, 26);
            this.btnDisconnect.TabIndex = 13;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cbStopBits.Location = new System.Drawing.Point(432, 19);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(39, 21);
            this.cbStopBits.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(477, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Data Bits:";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(583, 15);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(61, 26);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(374, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Stop Bits:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port:";
            // 
            // cbParity
            // 
            this.cbParity.DisplayMember = "None";
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cbParity.Location = new System.Drawing.Point(295, 19);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(73, 21);
            this.cbParity.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Parity:";
            // 
            // cbPort
            // 
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(41, 19);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(66, 21);
            this.cbPort.Sorted = true;
            this.cbPort.TabIndex = 7;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "115200",
            "57600",
            "38400",
            "19200",
            "14400",
            "9600",
            "4800",
            "2400",
            "1200",
            "600",
            "300"});
            this.cbBaudRate.Location = new System.Drawing.Point(180, 19);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(67, 21);
            this.cbBaudRate.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(113, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Buad Rate:";
            // 
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.groupBox1);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_bottom.Location = new System.Drawing.Point(0, 0);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.panel_bottom.Size = new System.Drawing.Size(732, 58);
            this.panel_bottom.TabIndex = 18;
            // 
            // btnIdentify
            // 
            this.btnIdentify.Location = new System.Drawing.Point(9, 75);
            this.btnIdentify.Name = "btnIdentify";
            this.btnIdentify.Size = new System.Drawing.Size(56, 26);
            this.btnIdentify.TabIndex = 19;
            this.btnIdentify.Text = "Identify";
            this.btnIdentify.UseVisualStyleBackColor = true;
            this.btnIdentify.Click += new System.EventHandler(this.btnIdentify_Click);
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrent.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrent.Location = new System.Drawing.Point(6, 149);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(173, 41);
            this.lblCurrent.TabIndex = 21;
            this.lblCurrent.Text = "-.-----";
            // 
            // lblVoltage
            // 
            this.lblVoltage.AutoSize = true;
            this.lblVoltage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblVoltage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVoltage.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoltage.Location = new System.Drawing.Point(239, 149);
            this.lblVoltage.Name = "lblVoltage";
            this.lblVoltage.Size = new System.Drawing.Size(195, 41);
            this.lblVoltage.TabIndex = 21;
            this.lblVoltage.Text = "--.-----";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(10, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 20);
            this.label6.TabIndex = 22;
            this.label6.Text = "Current (A)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(235, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 20);
            this.label7.TabIndex = 22;
            this.label7.Text = "Voltage (V)";
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(71, 75);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(94, 26);
            this.btnLog.TabIndex = 23;
            this.btnLog.Text = "Start Logging";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // cbPollDelay
            // 
            this.cbPollDelay.FormatString = "N0";
            this.cbPollDelay.FormattingEnabled = true;
            this.cbPollDelay.Items.AddRange(new object[] {
            "1",
            "2",
            "5",
            "10",
            "20",
            "50",
            "100",
            "200",
            "500",
            "1000"});
            this.cbPollDelay.Location = new System.Drawing.Point(21, 19);
            this.cbPollDelay.Name = "cbPollDelay";
            this.cbPollDelay.Size = new System.Drawing.Size(69, 21);
            this.cbPollDelay.TabIndex = 24;
            // 
            // cbPollTimeFormat
            // 
            this.cbPollTimeFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPollTimeFormat.FormattingEnabled = true;
            this.cbPollTimeFormat.Items.AddRange(new object[] {
            "mS",
            "Sec",
            "Min"});
            this.cbPollTimeFormat.Location = new System.Drawing.Point(96, 20);
            this.cbPollTimeFormat.Name = "cbPollTimeFormat";
            this.cbPollTimeFormat.Size = new System.Drawing.Size(52, 21);
            this.cbPollTimeFormat.TabIndex = 26;
            // 
            // ckBaudErrorFix
            // 
            this.ckBaudErrorFix.AutoSize = true;
            this.ckBaudErrorFix.Location = new System.Drawing.Point(200, 21);
            this.ckBaudErrorFix.Name = "ckBaudErrorFix";
            this.ckBaudErrorFix.Size = new System.Drawing.Size(136, 17);
            this.ckBaudErrorFix.TabIndex = 27;
            this.ckBaudErrorFix.Text = "Attempt error correcting";
            this.ckBaudErrorFix.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(485, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 20);
            this.label9.TabIndex = 22;
            this.label9.Text = "Power (W)";
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblPower.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPower.Font = new System.Drawing.Font("Courier New", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPower.Location = new System.Drawing.Point(489, 149);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(195, 41);
            this.lblPower.TabIndex = 21;
            this.lblPower.Text = "--.-----";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(671, 76);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(55, 25);
            this.btnAbout.TabIndex = 28;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnChannel1);
            this.groupBox2.Controls.Add(this.btnChannel2);
            this.groupBox2.Location = new System.Drawing.Point(87, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(93, 47);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Channel select";
            // 
            // btnChannel1
            // 
            this.btnChannel1.BackColor = System.Drawing.SystemColors.Control;
            this.btnChannel1.Location = new System.Drawing.Point(6, 21);
            this.btnChannel1.Name = "btnChannel1";
            this.btnChannel1.Size = new System.Drawing.Size(31, 19);
            this.btnChannel1.TabIndex = 32;
            this.btnChannel1.Text = "1";
            this.btnChannel1.UseVisualStyleBackColor = false;
            this.btnChannel1.Click += new System.EventHandler(this.btnChannel1_Click);
            // 
            // btnChannel2
            // 
            this.btnChannel2.Location = new System.Drawing.Point(56, 21);
            this.btnChannel2.Name = "btnChannel2";
            this.btnChannel2.Size = new System.Drawing.Size(31, 19);
            this.btnChannel2.TabIndex = 32;
            this.btnChannel2.Text = "2";
            this.btnChannel2.UseVisualStyleBackColor = true;
            this.btnChannel2.Click += new System.EventHandler(this.btnChannel2_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(235, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 16);
            this.label11.TabIndex = 22;
            this.label11.Text = "Voltage Limit";
            // 
            // lblVoltageLimit
            // 
            this.lblVoltageLimit.AutoSize = true;
            this.lblVoltageLimit.BackColor = System.Drawing.SystemColors.Control;
            this.lblVoltageLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblVoltageLimit.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVoltageLimit.Location = new System.Drawing.Point(239, 32);
            this.lblVoltageLimit.Name = "lblVoltageLimit";
            this.lblVoltageLimit.Size = new System.Drawing.Size(153, 34);
            this.lblVoltageLimit.TabIndex = 21;
            this.lblVoltageLimit.Text = "--.-----";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 10);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 16);
            this.label10.TabIndex = 22;
            this.label10.Text = "Current limit";
            // 
            // lblCurrentLimit
            // 
            this.lblCurrentLimit.AutoSize = true;
            this.lblCurrentLimit.BackColor = System.Drawing.SystemColors.Control;
            this.lblCurrentLimit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentLimit.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentLimit.Location = new System.Drawing.Point(6, 32);
            this.lblCurrentLimit.Name = "lblCurrentLimit";
            this.lblCurrentLimit.Size = new System.Drawing.Size(136, 34);
            this.lblCurrentLimit.TabIndex = 21;
            this.lblCurrentLimit.Text = "-.-----";
            // 
            // btnSetPollFrequency
            // 
            this.btnSetPollFrequency.Location = new System.Drawing.Point(157, 21);
            this.btnSetPollFrequency.Name = "btnSetPollFrequency";
            this.btnSetPollFrequency.Size = new System.Drawing.Size(37, 19);
            this.btnSetPollFrequency.TabIndex = 32;
            this.btnSetPollFrequency.Text = "set";
            this.btnSetPollFrequency.UseVisualStyleBackColor = true;
            this.btnSetPollFrequency.Click += new System.EventHandler(this.btnSetPollFrequency_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnStopStart);
            this.groupBox4.Controls.Add(this.pbPollCountdown);
            this.groupBox4.Controls.Add(this.cbPollDelay);
            this.groupBox4.Controls.Add(this.btnSetPollFrequency);
            this.groupBox4.Controls.Add(this.cbPollTimeFormat);
            this.groupBox4.Controls.Add(this.ckBaudErrorFix);
            this.groupBox4.Location = new System.Drawing.Point(336, 107);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(396, 47);
            this.groupBox4.TabIndex = 33;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Update frequency -- setting this too low can cause read errors!";
            // 
            // btnStopStart
            // 
            this.btnStopStart.Location = new System.Drawing.Point(339, 21);
            this.btnStopStart.Name = "btnStopStart";
            this.btnStopStart.Size = new System.Drawing.Size(51, 19);
            this.btnStopStart.TabIndex = 36;
            this.btnStopStart.Text = "Stop";
            this.btnStopStart.UseVisualStyleBackColor = true;
            this.btnStopStart.Click += new System.EventHandler(this.btnStopStart_Click);
            // 
            // lblOVP
            // 
            this.lblOVP.AutoSize = true;
            this.lblOVP.BackColor = System.Drawing.SystemColors.Control;
            this.lblOVP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOVP.Font = new System.Drawing.Font("Courier New", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOVP.Location = new System.Drawing.Point(489, 32);
            this.lblOVP.Name = "lblOVP";
            this.lblOVP.Size = new System.Drawing.Size(102, 34);
            this.lblOVP.TabIndex = 21;
            this.lblOVP.Text = "--.--";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(486, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(150, 16);
            this.label8.TabIndex = 22;
            this.label8.Text = "Over Voltage Protection";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnOutputOn);
            this.groupBox5.Controls.Add(this.btnOutputOff);
            this.groupBox5.Location = new System.Drawing.Point(0, 107);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(81, 47);
            this.groupBox5.TabIndex = 30;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Output";
            // 
            // btnOutputOn
            // 
            this.btnOutputOn.BackColor = System.Drawing.SystemColors.Control;
            this.btnOutputOn.Location = new System.Drawing.Point(4, 21);
            this.btnOutputOn.Name = "btnOutputOn";
            this.btnOutputOn.Size = new System.Drawing.Size(31, 19);
            this.btnOutputOn.TabIndex = 32;
            this.btnOutputOn.Text = "On";
            this.btnOutputOn.UseVisualStyleBackColor = false;
            this.btnOutputOn.Click += new System.EventHandler(this.btnOutputOn_Click);
            // 
            // btnOutputOff
            // 
            this.btnOutputOff.Location = new System.Drawing.Point(44, 21);
            this.btnOutputOff.Name = "btnOutputOff";
            this.btnOutputOff.Size = new System.Drawing.Size(31, 19);
            this.btnOutputOff.TabIndex = 32;
            this.btnOutputOff.Text = "Off";
            this.btnOutputOff.UseVisualStyleBackColor = true;
            this.btnOutputOff.Click += new System.EventHandler(this.btnOutputOff_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cbModes);
            this.groupBox6.Controls.Add(this.btnSetMode);
            this.groupBox6.Location = new System.Drawing.Point(188, 107);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(142, 47);
            this.groupBox6.TabIndex = 30;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Mode select";
            // 
            // cbModes
            // 
            this.cbModes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModes.FormattingEnabled = true;
            this.cbModes.Location = new System.Drawing.Point(5, 19);
            this.cbModes.Name = "cbModes";
            this.cbModes.Size = new System.Drawing.Size(82, 21);
            this.cbModes.TabIndex = 33;
            // 
            // btnSetMode
            // 
            this.btnSetMode.BackColor = System.Drawing.SystemColors.Control;
            this.btnSetMode.Location = new System.Drawing.Point(93, 20);
            this.btnSetMode.Name = "btnSetMode";
            this.btnSetMode.Size = new System.Drawing.Size(37, 19);
            this.btnSetMode.TabIndex = 32;
            this.btnSetMode.Text = "set";
            this.btnSetMode.UseVisualStyleBackColor = false;
            this.btnSetMode.Click += new System.EventHandler(this.btnSetMode_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 160);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(732, 277);
            this.tabControl1.TabIndex = 34;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.nmOverVoltageInput);
            this.tabPage1.Controls.Add(this.btnSetOverVoltageLimit);
            this.tabPage1.Controls.Add(this.nmVoltageInput);
            this.tabPage1.Controls.Add(this.btnSetVoltage);
            this.tabPage1.Controls.Add(this.nmVoltageLimitInput);
            this.tabPage1.Controls.Add(this.btnSetVoltageLimit);
            this.tabPage1.Controls.Add(this.nmCurrentInput);
            this.tabPage1.Controls.Add(this.btnSetCurrent);
            this.tabPage1.Controls.Add(this.nmCurrentLimitInput);
            this.tabPage1.Controls.Add(this.btnSetCurrentLimit);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.lblCurrent);
            this.tabPage1.Controls.Add(this.lblPower);
            this.tabPage1.Controls.Add(this.lblCurrentLimit);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.lblVoltage);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.lblVoltageLimit);
            this.tabPage1.Controls.Add(this.lblOVP);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(724, 251);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // nmOverVoltageInput
            // 
            this.nmOverVoltageInput.DecimalPlaces = 1;
            this.nmOverVoltageInput.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmOverVoltageInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nmOverVoltageInput.Location = new System.Drawing.Point(489, 73);
            this.nmOverVoltageInput.Maximum = new decimal(new int[] {
            22,
            0,
            0,
            0});
            this.nmOverVoltageInput.Name = "nmOverVoltageInput";
            this.nmOverVoltageInput.Size = new System.Drawing.Size(102, 31);
            this.nmOverVoltageInput.TabIndex = 25;
            this.nmOverVoltageInput.Visible = false;
            // 
            // btnSetOverVoltageLimit
            // 
            this.btnSetOverVoltageLimit.Location = new System.Drawing.Point(597, 77);
            this.btnSetOverVoltageLimit.Name = "btnSetOverVoltageLimit";
            this.btnSetOverVoltageLimit.Size = new System.Drawing.Size(43, 21);
            this.btnSetOverVoltageLimit.TabIndex = 24;
            this.btnSetOverVoltageLimit.Text = "set";
            this.btnSetOverVoltageLimit.UseVisualStyleBackColor = true;
            this.btnSetOverVoltageLimit.Visible = false;
            this.btnSetOverVoltageLimit.Click += new System.EventHandler(this.btnSetOverVoltageLimit_Click);
            // 
            // nmVoltageInput
            // 
            this.nmVoltageInput.DecimalPlaces = 2;
            this.nmVoltageInput.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmVoltageInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmVoltageInput.Location = new System.Drawing.Point(239, 193);
            this.nmVoltageInput.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nmVoltageInput.Name = "nmVoltageInput";
            this.nmVoltageInput.Size = new System.Drawing.Size(104, 31);
            this.nmVoltageInput.TabIndex = 25;
            this.nmVoltageInput.Visible = false;
            // 
            // btnSetVoltage
            // 
            this.btnSetVoltage.Location = new System.Drawing.Point(349, 197);
            this.btnSetVoltage.Name = "btnSetVoltage";
            this.btnSetVoltage.Size = new System.Drawing.Size(43, 21);
            this.btnSetVoltage.TabIndex = 24;
            this.btnSetVoltage.Text = "set";
            this.btnSetVoltage.UseVisualStyleBackColor = true;
            this.btnSetVoltage.Visible = false;
            // 
            // nmVoltageLimitInput
            // 
            this.nmVoltageLimitInput.DecimalPlaces = 2;
            this.nmVoltageLimitInput.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmVoltageLimitInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.nmVoltageLimitInput.Location = new System.Drawing.Point(238, 69);
            this.nmVoltageLimitInput.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nmVoltageLimitInput.Name = "nmVoltageLimitInput";
            this.nmVoltageLimitInput.Size = new System.Drawing.Size(104, 31);
            this.nmVoltageLimitInput.TabIndex = 25;
            // 
            // btnSetVoltageLimit
            // 
            this.btnSetVoltageLimit.Location = new System.Drawing.Point(348, 73);
            this.btnSetVoltageLimit.Name = "btnSetVoltageLimit";
            this.btnSetVoltageLimit.Size = new System.Drawing.Size(43, 21);
            this.btnSetVoltageLimit.TabIndex = 24;
            this.btnSetVoltageLimit.Text = "set";
            this.btnSetVoltageLimit.UseVisualStyleBackColor = true;
            this.btnSetVoltageLimit.Click += new System.EventHandler(this.btnSetVoltageLimit_Click);
            // 
            // nmCurrentInput
            // 
            this.nmCurrentInput.DecimalPlaces = 3;
            this.nmCurrentInput.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmCurrentInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nmCurrentInput.Location = new System.Drawing.Point(6, 193);
            this.nmCurrentInput.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmCurrentInput.Name = "nmCurrentInput";
            this.nmCurrentInput.Size = new System.Drawing.Size(104, 31);
            this.nmCurrentInput.TabIndex = 25;
            this.nmCurrentInput.Visible = false;
            // 
            // btnSetCurrent
            // 
            this.btnSetCurrent.Location = new System.Drawing.Point(116, 197);
            this.btnSetCurrent.Name = "btnSetCurrent";
            this.btnSetCurrent.Size = new System.Drawing.Size(43, 21);
            this.btnSetCurrent.TabIndex = 24;
            this.btnSetCurrent.Text = "set";
            this.btnSetCurrent.UseVisualStyleBackColor = true;
            this.btnSetCurrent.Visible = false;
            // 
            // nmCurrentLimitInput
            // 
            this.nmCurrentLimitInput.DecimalPlaces = 3;
            this.nmCurrentLimitInput.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmCurrentLimitInput.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.nmCurrentLimitInput.Location = new System.Drawing.Point(6, 69);
            this.nmCurrentLimitInput.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nmCurrentLimitInput.Name = "nmCurrentLimitInput";
            this.nmCurrentLimitInput.Size = new System.Drawing.Size(104, 31);
            this.nmCurrentLimitInput.TabIndex = 25;
            // 
            // btnSetCurrentLimit
            // 
            this.btnSetCurrentLimit.Location = new System.Drawing.Point(116, 73);
            this.btnSetCurrentLimit.Name = "btnSetCurrentLimit";
            this.btnSetCurrentLimit.Size = new System.Drawing.Size(43, 21);
            this.btnSetCurrentLimit.TabIndex = 24;
            this.btnSetCurrentLimit.Text = "set";
            this.btnSetCurrentLimit.UseVisualStyleBackColor = true;
            this.btnSetCurrentLimit.Click += new System.EventHandler(this.btnSetCurrentLimit_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Controls.Add(this.txtDisplayText);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.txtID);
            this.tabPage2.Controls.Add(this.txtErrors);
            this.tabPage2.Controls.Add(this.txtSCPIVersion);
            this.tabPage2.Controls.Add(this.button11);
            this.tabPage2.Controls.Add(this.btnClearDisplayText);
            this.tabPage2.Controls.Add(this.btnDisplayText);
            this.tabPage2.Controls.Add(this.btnRS232RWLock);
            this.tabPage2.Controls.Add(this.btnRS232Remote);
            this.tabPage2.Controls.Add(this.btnRS232Local);
            this.tabPage2.Controls.Add(this.btnReset);
            this.tabPage2.Controls.Add(this.btnSelfTest);
            this.tabPage2.Controls.Add(this.btnID);
            this.tabPage2.Controls.Add(this.btnClearErrors);
            this.tabPage2.Controls.Add(this.btnGetOldestError);
            this.tabPage2.Controls.Add(this.btnSCPIVersion);
            this.tabPage2.Controls.Add(this.btnBeep);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(724, 251);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "System";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(460, 128);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(203, 13);
            this.label12.TabIndex = 5;
            this.label12.Text = "*Remote or WRLock mode; blank to read";
            // 
            // txtDisplayText
            // 
            this.txtDisplayText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplayText.Location = new System.Drawing.Point(460, 98);
            this.txtDisplayText.MaxLength = 21;
            this.txtDisplayText.Name = "txtDisplayText";
            this.txtDisplayText.Size = new System.Drawing.Size(126, 20);
            this.txtDisplayText.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(460, 16);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(256, 84);
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(127, 69);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(210, 20);
            this.txtID.TabIndex = 2;
            // 
            // txtErrors
            // 
            this.txtErrors.Location = new System.Drawing.Point(127, 97);
            this.txtErrors.Multiline = true;
            this.txtErrors.Name = "txtErrors";
            this.txtErrors.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtErrors.Size = new System.Drawing.Size(210, 129);
            this.txtErrors.TabIndex = 2;
            // 
            // txtSCPIVersion
            // 
            this.txtSCPIVersion.BackColor = System.Drawing.SystemColors.Control;
            this.txtSCPIVersion.Location = new System.Drawing.Point(127, 43);
            this.txtSCPIVersion.Name = "txtSCPIVersion";
            this.txtSCPIVersion.ReadOnly = true;
            this.txtSCPIVersion.Size = new System.Drawing.Size(101, 20);
            this.txtSCPIVersion.TabIndex = 1;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(14, 151);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(69, 21);
            this.button11.TabIndex = 0;
            this.button11.Text = "<CTRL-C>";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // btnClearDisplayText
            // 
            this.btnClearDisplayText.Location = new System.Drawing.Point(353, 124);
            this.btnClearDisplayText.Name = "btnClearDisplayText";
            this.btnClearDisplayText.Size = new System.Drawing.Size(101, 21);
            this.btnClearDisplayText.TabIndex = 0;
            this.btnClearDisplayText.Text = "Clear Display text";
            this.btnClearDisplayText.UseVisualStyleBackColor = true;
            this.btnClearDisplayText.Click += new System.EventHandler(this.btnClearDisplayText_Click);
            // 
            // btnDisplayText
            // 
            this.btnDisplayText.Location = new System.Drawing.Point(353, 97);
            this.btnDisplayText.Name = "btnDisplayText";
            this.btnDisplayText.Size = new System.Drawing.Size(101, 21);
            this.btnDisplayText.TabIndex = 0;
            this.btnDisplayText.Text = "Display text*";
            this.btnDisplayText.UseVisualStyleBackColor = true;
            this.btnDisplayText.Click += new System.EventHandler(this.btnDisplayText_Click);
            // 
            // btnRS232RWLock
            // 
            this.btnRS232RWLock.Location = new System.Drawing.Point(353, 70);
            this.btnRS232RWLock.Name = "btnRS232RWLock";
            this.btnRS232RWLock.Size = new System.Drawing.Size(101, 21);
            this.btnRS232RWLock.TabIndex = 0;
            this.btnRS232RWLock.Text = "RS-232 RW Lock";
            this.btnRS232RWLock.UseVisualStyleBackColor = true;
            this.btnRS232RWLock.Click += new System.EventHandler(this.btnRS232RWLock_Click);
            // 
            // btnRS232Remote
            // 
            this.btnRS232Remote.Location = new System.Drawing.Point(353, 43);
            this.btnRS232Remote.Name = "btnRS232Remote";
            this.btnRS232Remote.Size = new System.Drawing.Size(101, 21);
            this.btnRS232Remote.TabIndex = 0;
            this.btnRS232Remote.Text = "RS-232 Remote";
            this.btnRS232Remote.UseVisualStyleBackColor = true;
            this.btnRS232Remote.Click += new System.EventHandler(this.btnRS232Remote_Click);
            // 
            // btnRS232Local
            // 
            this.btnRS232Local.Location = new System.Drawing.Point(353, 16);
            this.btnRS232Local.Name = "btnRS232Local";
            this.btnRS232Local.Size = new System.Drawing.Size(101, 21);
            this.btnRS232Local.TabIndex = 0;
            this.btnRS232Local.Text = "RS-232 Local";
            this.btnRS232Local.UseVisualStyleBackColor = true;
            this.btnRS232Local.Click += new System.EventHandler(this.btnRS232Local_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(14, 178);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(63, 21);
            this.btnReset.TabIndex = 0;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSelfTest
            // 
            this.btnSelfTest.Location = new System.Drawing.Point(14, 205);
            this.btnSelfTest.Name = "btnSelfTest";
            this.btnSelfTest.Size = new System.Drawing.Size(63, 21);
            this.btnSelfTest.TabIndex = 0;
            this.btnSelfTest.Text = "Self test";
            this.btnSelfTest.UseVisualStyleBackColor = true;
            this.btnSelfTest.Click += new System.EventHandler(this.btnSelfTest_Click);
            // 
            // btnID
            // 
            this.btnID.Location = new System.Drawing.Point(14, 70);
            this.btnID.Name = "btnID";
            this.btnID.Size = new System.Drawing.Size(33, 21);
            this.btnID.TabIndex = 0;
            this.btnID.Text = "ID";
            this.btnID.UseVisualStyleBackColor = true;
            this.btnID.Click += new System.EventHandler(this.btnID_Click);
            // 
            // btnClearErrors
            // 
            this.btnClearErrors.Location = new System.Drawing.Point(14, 124);
            this.btnClearErrors.Name = "btnClearErrors";
            this.btnClearErrors.Size = new System.Drawing.Size(89, 21);
            this.btnClearErrors.TabIndex = 0;
            this.btnClearErrors.Text = "Clear errors";
            this.btnClearErrors.UseVisualStyleBackColor = true;
            this.btnClearErrors.Click += new System.EventHandler(this.btnClearErrors_Click);
            // 
            // btnGetOldestError
            // 
            this.btnGetOldestError.Location = new System.Drawing.Point(14, 97);
            this.btnGetOldestError.Name = "btnGetOldestError";
            this.btnGetOldestError.Size = new System.Drawing.Size(89, 21);
            this.btnGetOldestError.TabIndex = 0;
            this.btnGetOldestError.Text = "Get oldest error";
            this.btnGetOldestError.UseVisualStyleBackColor = true;
            this.btnGetOldestError.Click += new System.EventHandler(this.btnGetOldestError_Click);
            // 
            // btnSCPIVersion
            // 
            this.btnSCPIVersion.Location = new System.Drawing.Point(14, 43);
            this.btnSCPIVersion.Name = "btnSCPIVersion";
            this.btnSCPIVersion.Size = new System.Drawing.Size(89, 21);
            this.btnSCPIVersion.TabIndex = 0;
            this.btnSCPIVersion.Text = "SCPI Version";
            this.btnSCPIVersion.UseVisualStyleBackColor = true;
            this.btnSCPIVersion.Click += new System.EventHandler(this.btnSCPIVersion_Click);
            // 
            // btnBeep
            // 
            this.btnBeep.Location = new System.Drawing.Point(14, 16);
            this.btnBeep.Name = "btnBeep";
            this.btnBeep.Size = new System.Drawing.Size(63, 21);
            this.btnBeep.TabIndex = 0;
            this.btnBeep.Text = "Beep";
            this.btnBeep.UseVisualStyleBackColor = true;
            this.btnBeep.Click += new System.EventHandler(this.btnBeep_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(599, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 19);
            this.button1.TabIndex = 26;
            this.button1.Text = "On";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(634, 32);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 19);
            this.button2.TabIndex = 26;
            this.button2.Text = "Off";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(667, 32);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(45, 19);
            this.button3.TabIndex = 26;
            this.button3.Text = "Clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            // 
            // pbPollCountdown
            // 
            this.pbPollCountdown.Location = new System.Drawing.Point(5, 14);
            this.pbPollCountdown.Name = "pbPollCountdown";
            this.pbPollCountdown.Size = new System.Drawing.Size(10, 27);
            this.pbPollCountdown.Step = 1;
            this.pbPollCountdown.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbPollCountdown.TabIndex = 35;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 470);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.btnIdentify);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.statusStrip1);
            this.MinimumSize = new System.Drawing.Size(740, 27);
            this.Name = "Form1";
            this.Text = "Power Supply Read";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_bottom.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmOverVoltageInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmVoltageInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmVoltageLimitInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmCurrentInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmCurrentLimitInput)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status_bar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Button btnIdentify;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lblVoltage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.ComboBox cbPollDelay;
        private System.Windows.Forms.ComboBox cbPollTimeFormat;
        private System.Windows.Forms.CheckBox ckBaudErrorFix;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnChannel1;
        private System.Windows.Forms.Button btnChannel2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblVoltageLimit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCurrentLimit;
        private System.Windows.Forms.Button btnSetPollFrequency;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblOVP;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnOutputOn;
        private System.Windows.Forms.Button btnOutputOff;
        private VerticalProgressBar pbPollCountdown;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnSetMode;
        private System.Windows.Forms.ComboBox cbModes;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button btnRS232RWLock;
        private System.Windows.Forms.Button btnRS232Remote;
        private System.Windows.Forms.Button btnRS232Local;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSelfTest;
        private System.Windows.Forms.Button btnID;
        private System.Windows.Forms.Button btnClearErrors;
        private System.Windows.Forms.Button btnGetOldestError;
        private System.Windows.Forms.Button btnSCPIVersion;
        private System.Windows.Forms.Button btnBeep;
        private System.Windows.Forms.Button btnStopStart;
        private System.Windows.Forms.TextBox txtSCPIVersion;
        private System.Windows.Forms.TextBox txtErrors;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnClearDisplayText;
        private System.Windows.Forms.Button btnDisplayText;
        private System.Windows.Forms.TextBox txtDisplayText;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnSetCurrentLimit;
        private System.Windows.Forms.NumericUpDown nmCurrentLimitInput;
        private System.Windows.Forms.NumericUpDown nmVoltageLimitInput;
        private System.Windows.Forms.Button btnSetVoltageLimit;
        private System.Windows.Forms.NumericUpDown nmOverVoltageInput;
        private System.Windows.Forms.Button btnSetOverVoltageLimit;
        private System.Windows.Forms.NumericUpDown nmVoltageInput;
        private System.Windows.Forms.Button btnSetVoltage;
        private System.Windows.Forms.NumericUpDown nmCurrentInput;
        private System.Windows.Forms.Button btnSetCurrent;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;


    }
}

