using System.Drawing;

namespace AutoTest
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        
        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改這個方法的內容。
        ///
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button_Setting = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_Start = new System.Windows.Forms.Button();
            this.label_Power = new System.Windows.Forms.Label();
            this.label_Camera = new System.Windows.Forms.Label();
            this.label_RedRat = new System.Windows.Forms.Label();
            this.labellabel_LoopTimes_Value = new System.Windows.Forms.Label();
            this.label_LoopNumber_Value = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.serialPort2 = new System.IO.Ports.SerialPort(this.components);
            this.TimeLabel = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.label_FwVersion = new System.Windows.Forms.Label();
            this.button_SerialPort1 = new System.Windows.Forms.Button();
            this.button_SerialPort2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button_Schedule1 = new System.Windows.Forms.Button();
            this.button_Schedule2 = new System.Windows.Forms.Button();
            this.SchOpen1 = new System.Windows.Forms.OpenFileDialog();
            this.SchOpen2 = new System.Windows.Forms.OpenFileDialog();
            this.button_Schedule3 = new System.Windows.Forms.Button();
            this.SchOpen3 = new System.Windows.Forms.OpenFileDialog();
            this.button_Schedule4 = new System.Windows.Forms.Button();
            this.button_Schedule5 = new System.Windows.Forms.Button();
            this.SchOpen4 = new System.Windows.Forms.OpenFileDialog();
            this.SchOpen5 = new System.Windows.Forms.OpenFileDialog();
            this.DataBtn = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.button_VirtualRC = new System.Windows.Forms.Button();
            this.TimeLabel2 = new System.Windows.Forms.Label();
            this.labelSch1Timer = new System.Windows.Forms.Label();
            this.labelSch2Timer = new System.Windows.Forms.Label();
            this.labelSch5Timer = new System.Windows.Forms.Label();
            this.labelSch4Timer = new System.Windows.Forms.Label();
            this.labelSch3Timer = new System.Windows.Forms.Label();
            this.button_Pause = new System.Windows.Forms.Button();
            this.label_ScheduleTime_Value = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.SegLabel4 = new System.Windows.Forms.Label();
            this.SegLabel3 = new System.Windows.Forms.Label();
            this.SegLabel2 = new System.Windows.Forms.Label();
            this.SegLabel1 = new System.Windows.Forms.Label();
            this.button_TimeLine = new System.Windows.Forms.Button();
            this.button_SaveSchedule = new System.Windows.Forms.Button();
            this.button_Schedule = new System.Windows.Forms.Button();
            this.button_Camera = new System.Windows.Forms.Button();
            this.label_LoopTimes = new System.Windows.Forms.Label();
            this.label_LoopNumber = new System.Windows.Forms.Label();
            this.label_ScheduleTime = new System.Windows.Forms.Label();
            this.label_AutoBox = new System.Windows.Forms.Label();
            this.serialPort3 = new System.IO.Ports.SerialPort(this.components);
            this.button_Input = new System.Windows.Forms.Button();
            this.button_Output = new System.Windows.Forms.Button();
            this.label_Command = new System.Windows.Forms.Label();
            this.labelGPIO_Input = new System.Windows.Forms.Label();
            this.label_BoxVersion = new System.Windows.Forms.Label();
            this.label_TestTime = new System.Windows.Forms.Label();
            this.label_TestTime_Value = new System.Windows.Forms.Label();
            this.button_InsertRow = new System.Windows.Forms.Button();
            this.panel_AcUsb = new System.Windows.Forms.Panel();
            this.groupBox_Usb = new System.Windows.Forms.GroupBox();
            this.label_Usb2 = new System.Windows.Forms.Label();
            this.label_Usb1 = new System.Windows.Forms.Label();
            this.pictureBox_Usb1 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Usb2 = new System.Windows.Forms.PictureBox();
            this.groupBox_Ac = new System.Windows.Forms.GroupBox();
            this.label_Ac2 = new System.Windows.Forms.Label();
            this.label_Ac1 = new System.Windows.Forms.Label();
            this.pictureBox_Ac2 = new System.Windows.Forms.PictureBox();
            this.pictureBox_Ac1 = new System.Windows.Forms.PictureBox();
            this.button_AcUsb = new System.Windows.Forms.Button();
            this.pictureBox_BlueRat = new System.Windows.Forms.PictureBox();
            this.MiniPicBox = new System.Windows.Forms.PictureBox();
            this.ClosePicBox = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox_AcPower = new System.Windows.Forms.PictureBox();
            this.pictureBox_Camera = new System.Windows.Forms.PictureBox();
            this.pictureBox_RedRat = new System.Windows.Forms.PictureBox();
            this.panelVideo = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.comboBox_CameraDevice = new System.Windows.Forms.ComboBox();
            this.DataGridView_Schedule = new AutoTest.SafeDataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel_AcUsb.SuspendLayout();
            this.groupBox_Usb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Usb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Usb2)).BeginInit();
            this.groupBox_Ac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Ac2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Ac1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_BlueRat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MiniPicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClosePicBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AcPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Camera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RedRat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Schedule)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Setting
            // 
            this.button_Setting.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_Setting.BackColor = System.Drawing.Color.Transparent;
            this.button_Setting.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Setting.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Setting.Location = new System.Drawing.Point(141, 10);
            this.button_Setting.Margin = new System.Windows.Forms.Padding(2);
            this.button_Setting.Name = "button_Setting";
            this.button_Setting.Size = new System.Drawing.Size(70, 25);
            this.button_Setting.TabIndex = 25;
            this.button_Setting.Text = "SETTING";
            this.button_Setting.UseVisualStyleBackColor = false;
            this.button_Setting.Click += new System.EventHandler(this.SettingBtn_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(144, 40);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(950, 400);
            this.textBox1.TabIndex = 24;
            // 
            // button_Start
            // 
            this.button_Start.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_Start.BackColor = System.Drawing.Color.Transparent;
            this.button_Start.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Start.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Start.Location = new System.Drawing.Point(10, 10);
            this.button_Start.Margin = new System.Windows.Forms.Padding(2, 2, 1, 2);
            this.button_Start.Name = "button_Start";
            this.button_Start.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Start.Size = new System.Drawing.Size(60, 25);
            this.button_Start.TabIndex = 22;
            this.button_Start.Text = "START";
            this.button_Start.UseVisualStyleBackColor = false;
            this.button_Start.Click += new System.EventHandler(this.StartBtn_Click);
            // 
            // label_Power
            // 
            this.label_Power.AutoSize = true;
            this.label_Power.BackColor = System.Drawing.Color.Transparent;
            this.label_Power.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Power.ForeColor = System.Drawing.Color.Black;
            this.label_Power.Location = new System.Drawing.Point(854, 17);
            this.label_Power.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Power.Name = "label_Power";
            this.label_Power.Size = new System.Drawing.Size(49, 18);
            this.label_Power.TabIndex = 36;
            this.label_Power.Text = "Power";
            // 
            // label_Camera
            // 
            this.label_Camera.AutoSize = true;
            this.label_Camera.BackColor = System.Drawing.Color.Transparent;
            this.label_Camera.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Camera.ForeColor = System.Drawing.Color.Black;
            this.label_Camera.Location = new System.Drawing.Point(766, 17);
            this.label_Camera.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Camera.Name = "label_Camera";
            this.label_Camera.Size = new System.Drawing.Size(59, 18);
            this.label_Camera.TabIndex = 34;
            this.label_Camera.Text = "Camera";
            // 
            // label_RedRat
            // 
            this.label_RedRat.AutoSize = true;
            this.label_RedRat.BackColor = System.Drawing.Color.Transparent;
            this.label_RedRat.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_RedRat.ForeColor = System.Drawing.Color.Black;
            this.label_RedRat.Location = new System.Drawing.Point(678, 17);
            this.label_RedRat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_RedRat.Name = "label_RedRat";
            this.label_RedRat.Size = new System.Drawing.Size(56, 18);
            this.label_RedRat.TabIndex = 32;
            this.label_RedRat.Text = "RedRat";
            // 
            // labellabel_LoopTimes_Value
            // 
            this.labellabel_LoopTimes_Value.AutoSize = true;
            this.labellabel_LoopTimes_Value.BackColor = System.Drawing.Color.Transparent;
            this.labellabel_LoopTimes_Value.Font = new System.Drawing.Font("Microsoft JhengHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labellabel_LoopTimes_Value.ForeColor = System.Drawing.Color.Black;
            this.labellabel_LoopTimes_Value.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labellabel_LoopTimes_Value.Location = new System.Drawing.Point(140, 470);
            this.labellabel_LoopTimes_Value.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labellabel_LoopTimes_Value.Name = "labellabel_LoopTimes_Value";
            this.labellabel_LoopTimes_Value.Size = new System.Drawing.Size(65, 24);
            this.labellabel_LoopTimes_Value.TabIndex = 30;
            this.labellabel_LoopTimes_Value.Text = "99999";
            this.labellabel_LoopTimes_Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_LoopNumber_Value
            // 
            this.label_LoopNumber_Value.AutoSize = true;
            this.label_LoopNumber_Value.BackColor = System.Drawing.Color.Transparent;
            this.label_LoopNumber_Value.Font = new System.Drawing.Font("Microsoft JhengHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_LoopNumber_Value.ForeColor = System.Drawing.Color.Black;
            this.label_LoopNumber_Value.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_LoopNumber_Value.Location = new System.Drawing.Point(141, 521);
            this.label_LoopNumber_Value.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_LoopNumber_Value.Name = "label_LoopNumber_Value";
            this.label_LoopNumber_Value.Size = new System.Drawing.Size(21, 24);
            this.label_LoopNumber_Value.TabIndex = 29;
            this.label_LoopNumber_Value.Text = "0";
            this.label_LoopNumber_Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.BackColor = System.Drawing.Color.DarkOrange;
            this.TimeLabel.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.TimeLabel.ForeColor = System.Drawing.Color.White;
            this.TimeLabel.Location = new System.Drawing.Point(318, 703);
            this.TimeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(184, 18);
            this.TimeLabel.TabIndex = 44;
            this.TimeLabel.Text = "xxx, xx xxx xxxx xx:xx:xx xxx";
            this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // label_FwVersion
            // 
            this.label_FwVersion.AutoSize = true;
            this.label_FwVersion.BackColor = System.Drawing.Color.Transparent;
            this.label_FwVersion.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_FwVersion.ForeColor = System.Drawing.Color.Crimson;
            this.label_FwVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_FwVersion.Location = new System.Drawing.Point(961, 17);
            this.label_FwVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_FwVersion.Name = "label_FwVersion";
            this.label_FwVersion.Size = new System.Drawing.Size(75, 18);
            this.label_FwVersion.TabIndex = 46;
            this.label_FwVersion.Text = "Ver. 2.17.2";
            this.label_FwVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_FwVersion.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.LabelVersion_MouseClick);
            // 
            // button_SerialPort1
            // 
            this.button_SerialPort1.BackColor = System.Drawing.Color.Transparent;
            this.button_SerialPort1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_SerialPort1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_SerialPort1.Location = new System.Drawing.Point(10, 315);
            this.button_SerialPort1.Margin = new System.Windows.Forms.Padding(2);
            this.button_SerialPort1.Name = "button_SerialPort1";
            this.button_SerialPort1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_SerialPort1.Size = new System.Drawing.Size(112, 60);
            this.button_SerialPort1.TabIndex = 48;
            this.button_SerialPort1.Text = "Serial Port 1";
            this.button_SerialPort1.UseVisualStyleBackColor = false;
            this.button_SerialPort1.Click += new System.EventHandler(this.Com1Btn_Click);
            // 
            // button_SerialPort2
            // 
            this.button_SerialPort2.BackColor = System.Drawing.Color.Transparent;
            this.button_SerialPort2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_SerialPort2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_SerialPort2.Location = new System.Drawing.Point(10, 380);
            this.button_SerialPort2.Margin = new System.Windows.Forms.Padding(2);
            this.button_SerialPort2.Name = "button_SerialPort2";
            this.button_SerialPort2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_SerialPort2.Size = new System.Drawing.Size(112, 60);
            this.button_SerialPort2.TabIndex = 49;
            this.button_SerialPort2.Text = "Serial Port 2";
            this.button_SerialPort2.UseVisualStyleBackColor = false;
            this.button_SerialPort2.Click += new System.EventHandler(this.Com2Btn_Click);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(144, 40);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(950, 400);
            this.textBox2.TabIndex = 50;
            // 
            // button_Schedule1
            // 
            this.button_Schedule1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Schedule1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Schedule1.Location = new System.Drawing.Point(72, 126);
            this.button_Schedule1.Margin = new System.Windows.Forms.Padding(2);
            this.button_Schedule1.Name = "button_Schedule1";
            this.button_Schedule1.Size = new System.Drawing.Size(50, 20);
            this.button_Schedule1.TabIndex = 51;
            this.button_Schedule1.Text = "1";
            this.button_Schedule1.UseVisualStyleBackColor = true;
            this.button_Schedule1.Click += new System.EventHandler(this.SchBtn1_Click);
            // 
            // button_Schedule2
            // 
            this.button_Schedule2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Schedule2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Schedule2.Location = new System.Drawing.Point(72, 151);
            this.button_Schedule2.Margin = new System.Windows.Forms.Padding(2);
            this.button_Schedule2.Name = "button_Schedule2";
            this.button_Schedule2.Size = new System.Drawing.Size(50, 20);
            this.button_Schedule2.TabIndex = 52;
            this.button_Schedule2.Text = "2";
            this.button_Schedule2.UseVisualStyleBackColor = true;
            this.button_Schedule2.Click += new System.EventHandler(this.SchBtn2_Click);
            // 
            // button_Schedule3
            // 
            this.button_Schedule3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Schedule3.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Schedule3.Location = new System.Drawing.Point(72, 176);
            this.button_Schedule3.Margin = new System.Windows.Forms.Padding(2);
            this.button_Schedule3.Name = "button_Schedule3";
            this.button_Schedule3.Size = new System.Drawing.Size(50, 20);
            this.button_Schedule3.TabIndex = 55;
            this.button_Schedule3.Text = "3";
            this.button_Schedule3.UseVisualStyleBackColor = true;
            this.button_Schedule3.Click += new System.EventHandler(this.SchBtn3_Click);
            // 
            // button_Schedule4
            // 
            this.button_Schedule4.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Schedule4.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Schedule4.Location = new System.Drawing.Point(72, 201);
            this.button_Schedule4.Margin = new System.Windows.Forms.Padding(2);
            this.button_Schedule4.Name = "button_Schedule4";
            this.button_Schedule4.Size = new System.Drawing.Size(50, 20);
            this.button_Schedule4.TabIndex = 56;
            this.button_Schedule4.Text = "4";
            this.button_Schedule4.UseVisualStyleBackColor = true;
            this.button_Schedule4.Click += new System.EventHandler(this.SchBtn4_Click);
            // 
            // button_Schedule5
            // 
            this.button_Schedule5.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Schedule5.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Schedule5.Location = new System.Drawing.Point(72, 226);
            this.button_Schedule5.Margin = new System.Windows.Forms.Padding(2);
            this.button_Schedule5.Name = "button_Schedule5";
            this.button_Schedule5.Size = new System.Drawing.Size(50, 20);
            this.button_Schedule5.TabIndex = 57;
            this.button_Schedule5.Text = "5";
            this.button_Schedule5.UseVisualStyleBackColor = true;
            this.button_Schedule5.Click += new System.EventHandler(this.SchBtn5_Click);
            // 
            // DataBtn
            // 
            this.DataBtn.Location = new System.Drawing.Point(226, 702);
            this.DataBtn.Margin = new System.Windows.Forms.Padding(2);
            this.DataBtn.Name = "DataBtn";
            this.DataBtn.Size = new System.Drawing.Size(51, 22);
            this.DataBtn.TabIndex = 62;
            this.DataBtn.Text = "Data";
            this.DataBtn.UseVisualStyleBackColor = true;
            this.DataBtn.Visible = false;
            this.DataBtn.Click += new System.EventHandler(this.DataBtn_Click);
            // 
            // button_VirtualRC
            // 
            this.button_VirtualRC.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_VirtualRC.BackColor = System.Drawing.Color.Transparent;
            this.button_VirtualRC.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_VirtualRC.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_VirtualRC.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_VirtualRC.Location = new System.Drawing.Point(250, 10);
            this.button_VirtualRC.Margin = new System.Windows.Forms.Padding(2, 2, 1, 2);
            this.button_VirtualRC.Name = "button_VirtualRC";
            this.button_VirtualRC.Size = new System.Drawing.Size(95, 25);
            this.button_VirtualRC.TabIndex = 63;
            this.button_VirtualRC.Text = "VIRTUAL RC";
            this.button_VirtualRC.UseVisualStyleBackColor = false;
            this.button_VirtualRC.Click += new System.EventHandler(this.button_VirtualRC_Click);
            // 
            // TimeLabel2
            // 
            this.TimeLabel2.BackColor = System.Drawing.Color.Transparent;
            this.TimeLabel2.Font = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel2.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.TimeLabel2.Location = new System.Drawing.Point(10, 692);
            this.TimeLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TimeLabel2.Name = "TimeLabel2";
            this.TimeLabel2.Size = new System.Drawing.Size(246, 41);
            this.TimeLabel2.TabIndex = 64;
            this.TimeLabel2.Text = "yyyy-MM-dd  HH:mm:ss";
            this.TimeLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSch1Timer
            // 
            this.labelSch1Timer.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.labelSch1Timer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSch1Timer.AutoSize = true;
            this.labelSch1Timer.BackColor = System.Drawing.Color.Transparent;
            this.labelSch1Timer.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelSch1Timer.ForeColor = System.Drawing.Color.Black;
            this.labelSch1Timer.Location = new System.Drawing.Point(14, 14);
            this.labelSch1Timer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSch1Timer.Name = "labelSch1Timer";
            this.labelSch1Timer.Size = new System.Drawing.Size(146, 18);
            this.labelSch1Timer.TabIndex = 65;
            this.labelSch1Timer.Text = "2014-12-11  12:23:00";
            // 
            // labelSch2Timer
            // 
            this.labelSch2Timer.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.labelSch2Timer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSch2Timer.AutoSize = true;
            this.labelSch2Timer.BackColor = System.Drawing.Color.Transparent;
            this.labelSch2Timer.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelSch2Timer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSch2Timer.Location = new System.Drawing.Point(14, 62);
            this.labelSch2Timer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSch2Timer.Name = "labelSch2Timer";
            this.labelSch2Timer.Size = new System.Drawing.Size(146, 18);
            this.labelSch2Timer.TabIndex = 66;
            this.labelSch2Timer.Text = "2014-12-11  12:23:00";
            // 
            // labelSch5Timer
            // 
            this.labelSch5Timer.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.labelSch5Timer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSch5Timer.AutoSize = true;
            this.labelSch5Timer.BackColor = System.Drawing.Color.Transparent;
            this.labelSch5Timer.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelSch5Timer.ForeColor = System.Drawing.Color.Black;
            this.labelSch5Timer.Location = new System.Drawing.Point(14, 206);
            this.labelSch5Timer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSch5Timer.Name = "labelSch5Timer";
            this.labelSch5Timer.Size = new System.Drawing.Size(146, 18);
            this.labelSch5Timer.TabIndex = 67;
            this.labelSch5Timer.Text = "2014-12-11  12:23:00";
            // 
            // labelSch4Timer
            // 
            this.labelSch4Timer.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.labelSch4Timer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSch4Timer.AutoSize = true;
            this.labelSch4Timer.BackColor = System.Drawing.Color.Transparent;
            this.labelSch4Timer.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelSch4Timer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSch4Timer.Location = new System.Drawing.Point(14, 158);
            this.labelSch4Timer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSch4Timer.Name = "labelSch4Timer";
            this.labelSch4Timer.Size = new System.Drawing.Size(146, 18);
            this.labelSch4Timer.TabIndex = 68;
            this.labelSch4Timer.Text = "2014-12-11  12:23:00";
            // 
            // labelSch3Timer
            // 
            this.labelSch3Timer.AccessibleRole = System.Windows.Forms.AccessibleRole.ScrollBar;
            this.labelSch3Timer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelSch3Timer.AutoSize = true;
            this.labelSch3Timer.BackColor = System.Drawing.Color.Transparent;
            this.labelSch3Timer.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelSch3Timer.ForeColor = System.Drawing.Color.Gray;
            this.labelSch3Timer.Location = new System.Drawing.Point(14, 110);
            this.labelSch3Timer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSch3Timer.Name = "labelSch3Timer";
            this.labelSch3Timer.Size = new System.Drawing.Size(146, 18);
            this.labelSch3Timer.TabIndex = 69;
            this.labelSch3Timer.Text = "2014-12-11  12:23:00";
            // 
            // button_Pause
            // 
            this.button_Pause.BackColor = System.Drawing.Color.Transparent;
            this.button_Pause.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Pause.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Pause.Location = new System.Drawing.Point(73, 10);
            this.button_Pause.Margin = new System.Windows.Forms.Padding(2, 2, 1, 2);
            this.button_Pause.Name = "button_Pause";
            this.button_Pause.Size = new System.Drawing.Size(65, 25);
            this.button_Pause.TabIndex = 73;
            this.button_Pause.Text = "PAUSE";
            this.button_Pause.UseVisualStyleBackColor = false;
            this.button_Pause.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // label_ScheduleTime_Value
            // 
            this.label_ScheduleTime_Value.AutoSize = true;
            this.label_ScheduleTime_Value.BackColor = System.Drawing.Color.Transparent;
            this.label_ScheduleTime_Value.Font = new System.Drawing.Font("Microsoft JhengHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ScheduleTime_Value.ForeColor = System.Drawing.Color.Black;
            this.label_ScheduleTime_Value.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_ScheduleTime_Value.Location = new System.Drawing.Point(312, 470);
            this.label_ScheduleTime_Value.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ScheduleTime_Value.Name = "label_ScheduleTime_Value";
            this.label_ScheduleTime_Value.Size = new System.Drawing.Size(120, 24);
            this.label_ScheduleTime_Value.TabIndex = 75;
            this.label_ScheduleTime_Value.Text = "0d 0h 0m 0s";
            this.label_ScheduleTime_Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick_1);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SegLabel4);
            this.panel1.Controls.Add(this.SegLabel3);
            this.panel1.Controls.Add(this.SegLabel2);
            this.panel1.Controls.Add(this.SegLabel1);
            this.panel1.Controls.Add(this.labelSch1Timer);
            this.panel1.Controls.Add(this.labelSch2Timer);
            this.panel1.Controls.Add(this.labelSch3Timer);
            this.panel1.Controls.Add(this.labelSch5Timer);
            this.panel1.Controls.Add(this.labelSch4Timer);
            this.panel1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(904, 190);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(176, 236);
            this.panel1.TabIndex = 76;
            // 
            // SegLabel4
            // 
            this.SegLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SegLabel4.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F);
            this.SegLabel4.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.SegLabel4.Location = new System.Drawing.Point(9, 176);
            this.SegLabel4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SegLabel4.Name = "SegLabel4";
            this.SegLabel4.Size = new System.Drawing.Size(162, 12);
            this.SegLabel4.TabIndex = 73;
            this.SegLabel4.Text = "---------------------------------------";
            // 
            // SegLabel3
            // 
            this.SegLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SegLabel3.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F);
            this.SegLabel3.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.SegLabel3.Location = new System.Drawing.Point(9, 128);
            this.SegLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SegLabel3.Name = "SegLabel3";
            this.SegLabel3.Size = new System.Drawing.Size(162, 12);
            this.SegLabel3.TabIndex = 72;
            this.SegLabel3.Text = "---------------------------------------";
            // 
            // SegLabel2
            // 
            this.SegLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SegLabel2.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F);
            this.SegLabel2.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.SegLabel2.Location = new System.Drawing.Point(9, 80);
            this.SegLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SegLabel2.Name = "SegLabel2";
            this.SegLabel2.Size = new System.Drawing.Size(162, 12);
            this.SegLabel2.TabIndex = 71;
            this.SegLabel2.Text = "---------------------------------------";
            // 
            // SegLabel1
            // 
            this.SegLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SegLabel1.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SegLabel1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.SegLabel1.Location = new System.Drawing.Point(9, 32);
            this.SegLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SegLabel1.Name = "SegLabel1";
            this.SegLabel1.Size = new System.Drawing.Size(162, 12);
            this.SegLabel1.TabIndex = 70;
            this.SegLabel1.Text = "---------------------------------------";
            // 
            // button_TimeLine
            // 
            this.button_TimeLine.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_TimeLine.BackColor = System.Drawing.Color.Transparent;
            this.button_TimeLine.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_TimeLine.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_TimeLine.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_TimeLine.Location = new System.Drawing.Point(771, 444);
            this.button_TimeLine.Margin = new System.Windows.Forms.Padding(2);
            this.button_TimeLine.Name = "button_TimeLine";
            this.button_TimeLine.Size = new System.Drawing.Size(81, 25);
            this.button_TimeLine.TabIndex = 77;
            this.button_TimeLine.Text = "TIMELINE";
            this.button_TimeLine.UseVisualStyleBackColor = false;
            this.button_TimeLine.Visible = false;
            this.button_TimeLine.Click += new System.EventHandler(this.TimerPanelbutton_Click);
            // 
            // button_SaveSchedule
            // 
            this.button_SaveSchedule.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_SaveSchedule.BackColor = System.Drawing.Color.Transparent;
            this.button_SaveSchedule.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_SaveSchedule.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_SaveSchedule.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SaveSchedule.Location = new System.Drawing.Point(975, 444);
            this.button_SaveSchedule.Margin = new System.Windows.Forms.Padding(2);
            this.button_SaveSchedule.Name = "button_SaveSchedule";
            this.button_SaveSchedule.Size = new System.Drawing.Size(120, 25);
            this.button_SaveSchedule.TabIndex = 47;
            this.button_SaveSchedule.Text = "SAVE SCHEDULE";
            this.button_SaveSchedule.UseVisualStyleBackColor = false;
            this.button_SaveSchedule.Click += new System.EventHandler(this.WriteBtn_Click);
            // 
            // button_Schedule
            // 
            this.button_Schedule.BackColor = System.Drawing.Color.Transparent;
            this.button_Schedule.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Schedule.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Schedule.Location = new System.Drawing.Point(10, 62);
            this.button_Schedule.Margin = new System.Windows.Forms.Padding(2);
            this.button_Schedule.Name = "button_Schedule";
            this.button_Schedule.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Schedule.Size = new System.Drawing.Size(112, 60);
            this.button_Schedule.TabIndex = 78;
            this.button_Schedule.Text = "Schedule";
            this.button_Schedule.UseVisualStyleBackColor = false;
            this.button_Schedule.Click += new System.EventHandler(this.Button_TabScheduler_Click);
            // 
            // button_Camera
            // 
            this.button_Camera.BackColor = System.Drawing.Color.Transparent;
            this.button_Camera.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Camera.Font = new System.Drawing.Font("Microsoft JhengHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Camera.Location = new System.Drawing.Point(10, 250);
            this.button_Camera.Margin = new System.Windows.Forms.Padding(2);
            this.button_Camera.Name = "button_Camera";
            this.button_Camera.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.button_Camera.Size = new System.Drawing.Size(112, 60);
            this.button_Camera.TabIndex = 79;
            this.button_Camera.Text = "Camera";
            this.button_Camera.UseVisualStyleBackColor = false;
            this.button_Camera.Click += new System.EventHandler(this.Button_TabCamera_Click);
            // 
            // label_LoopTimes
            // 
            this.label_LoopTimes.AutoSize = true;
            this.label_LoopTimes.BackColor = System.Drawing.Color.Transparent;
            this.label_LoopTimes.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_LoopTimes.ForeColor = System.Drawing.Color.DarkCyan;
            this.label_LoopTimes.Location = new System.Drawing.Point(142, 452);
            this.label_LoopTimes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_LoopTimes.Name = "label_LoopTimes";
            this.label_LoopTimes.Size = new System.Drawing.Size(73, 14);
            this.label_LoopTimes.TabIndex = 80;
            this.label_LoopTimes.Text = "LOOP TIMES";
            // 
            // label_LoopNumber
            // 
            this.label_LoopNumber.AutoSize = true;
            this.label_LoopNumber.BackColor = System.Drawing.Color.Transparent;
            this.label_LoopNumber.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_LoopNumber.ForeColor = System.Drawing.Color.DarkCyan;
            this.label_LoopNumber.Location = new System.Drawing.Point(142, 503);
            this.label_LoopNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_LoopNumber.Name = "label_LoopNumber";
            this.label_LoopNumber.Size = new System.Drawing.Size(89, 14);
            this.label_LoopNumber.TabIndex = 81;
            this.label_LoopNumber.Text = "LOOP NUMBER";
            // 
            // label_ScheduleTime
            // 
            this.label_ScheduleTime.AutoSize = true;
            this.label_ScheduleTime.BackColor = System.Drawing.Color.Transparent;
            this.label_ScheduleTime.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_ScheduleTime.ForeColor = System.Drawing.Color.DarkCyan;
            this.label_ScheduleTime.Location = new System.Drawing.Point(314, 452);
            this.label_ScheduleTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_ScheduleTime.Name = "label_ScheduleTime";
            this.label_ScheduleTime.Size = new System.Drawing.Size(91, 14);
            this.label_ScheduleTime.TabIndex = 82;
            this.label_ScheduleTime.Text = "SCHEDULE TIME";
            // 
            // label_AutoBox
            // 
            this.label_AutoBox.AutoSize = true;
            this.label_AutoBox.BackColor = System.Drawing.Color.Transparent;
            this.label_AutoBox.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_AutoBox.ForeColor = System.Drawing.Color.Black;
            this.label_AutoBox.Location = new System.Drawing.Point(590, 17);
            this.label_AutoBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_AutoBox.Name = "label_AutoBox";
            this.label_AutoBox.Size = new System.Drawing.Size(66, 18);
            this.label_AutoBox.TabIndex = 83;
            this.label_AutoBox.Text = "AutoBox";
            // 
            // button_Input
            // 
            this.button_Input.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_Input.BackColor = System.Drawing.Color.Transparent;
            this.button_Input.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Input.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Input.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Input.Location = new System.Drawing.Point(10, 452);
            this.button_Input.Margin = new System.Windows.Forms.Padding(2);
            this.button_Input.Name = "button_Input";
            this.button_Input.Size = new System.Drawing.Size(80, 25);
            this.button_Input.TabIndex = 85;
            this.button_Input.Text = "INPUT";
            this.button_Input.UseVisualStyleBackColor = false;
            this.button_Input.Visible = false;
            this.button_Input.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_Output
            // 
            this.button_Output.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_Output.BackColor = System.Drawing.Color.Transparent;
            this.button_Output.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_Output.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Output.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_Output.Location = new System.Drawing.Point(10, 480);
            this.button_Output.Margin = new System.Windows.Forms.Padding(2);
            this.button_Output.Name = "button_Output";
            this.button_Output.Size = new System.Drawing.Size(80, 25);
            this.button_Output.TabIndex = 86;
            this.button_Output.Text = "OUTPUT";
            this.button_Output.UseVisualStyleBackColor = false;
            this.button_Output.Visible = false;
            this.button_Output.Click += new System.EventHandler(this.button2_Click);
            // 
            // label_Command
            // 
            this.label_Command.BackColor = System.Drawing.Color.Transparent;
            this.label_Command.Font = new System.Drawing.Font("Microsoft JhengHei UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Command.ForeColor = System.Drawing.Color.Tomato;
            this.label_Command.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_Command.Location = new System.Drawing.Point(665, 482);
            this.label_Command.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Command.Name = "label_Command";
            this.label_Command.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Command.Size = new System.Drawing.Size(428, 53);
            this.label_Command.TabIndex = 45;
            this.label_Command.Text = "~~~~~";
            this.label_Command.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelGPIO_Input
            // 
            this.labelGPIO_Input.BackColor = System.Drawing.Color.Transparent;
            this.labelGPIO_Input.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.labelGPIO_Input.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.labelGPIO_Input.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelGPIO_Input.Location = new System.Drawing.Point(590, 444);
            this.labelGPIO_Input.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelGPIO_Input.Name = "labelGPIO_Input";
            this.labelGPIO_Input.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelGPIO_Input.Size = new System.Drawing.Size(177, 25);
            this.labelGPIO_Input.TabIndex = 87;
            this.labelGPIO_Input.Text = "---------------";
            this.labelGPIO_Input.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelGPIO_Input.Visible = false;
            // 
            // label_BoxVersion
            // 
            this.label_BoxVersion.AutoSize = true;
            this.label_BoxVersion.BackColor = System.Drawing.Color.Transparent;
            this.label_BoxVersion.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_BoxVersion.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label_BoxVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_BoxVersion.Location = new System.Drawing.Point(1040, 17);
            this.label_BoxVersion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_BoxVersion.Name = "label_BoxVersion";
            this.label_BoxVersion.Size = new System.Drawing.Size(41, 18);
            this.label_BoxVersion.TabIndex = 88;
            this.label_BoxVersion.Text = "_1.02";
            this.label_BoxVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_TestTime
            // 
            this.label_TestTime.AutoSize = true;
            this.label_TestTime.BackColor = System.Drawing.Color.Transparent;
            this.label_TestTime.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_TestTime.ForeColor = System.Drawing.Color.DarkCyan;
            this.label_TestTime.Location = new System.Drawing.Point(314, 503);
            this.label_TestTime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_TestTime.Name = "label_TestTime";
            this.label_TestTime.Size = new System.Drawing.Size(60, 14);
            this.label_TestTime.TabIndex = 90;
            this.label_TestTime.Text = "TEST TIME";
            // 
            // label_TestTime_Value
            // 
            this.label_TestTime_Value.AutoSize = true;
            this.label_TestTime_Value.BackColor = System.Drawing.Color.Transparent;
            this.label_TestTime_Value.Font = new System.Drawing.Font("Microsoft JhengHei UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_TestTime_Value.ForeColor = System.Drawing.Color.Black;
            this.label_TestTime_Value.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_TestTime_Value.Location = new System.Drawing.Point(312, 521);
            this.label_TestTime_Value.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_TestTime_Value.Name = "label_TestTime_Value";
            this.label_TestTime_Value.Size = new System.Drawing.Size(120, 24);
            this.label_TestTime_Value.TabIndex = 89;
            this.label_TestTime_Value.Text = "0d 0h 0m 0s";
            this.label_TestTime_Value.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_InsertRow
            // 
            this.button_InsertRow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_InsertRow.BackColor = System.Drawing.Color.Transparent;
            this.button_InsertRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_InsertRow.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_InsertRow.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_InsertRow.Location = new System.Drawing.Point(856, 444);
            this.button_InsertRow.Margin = new System.Windows.Forms.Padding(2);
            this.button_InsertRow.Name = "button_InsertRow";
            this.button_InsertRow.Size = new System.Drawing.Size(115, 25);
            this.button_InsertRow.TabIndex = 91;
            this.button_InsertRow.Text = "INSERT A ROW";
            this.button_InsertRow.UseVisualStyleBackColor = false;
            this.button_InsertRow.Click += new System.EventHandler(this.button_insert_a_row_Click);
            // 
            // panel_AcUsb
            // 
            this.panel_AcUsb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_AcUsb.Controls.Add(this.groupBox_Usb);
            this.panel_AcUsb.Controls.Add(this.groupBox_Ac);
            this.panel_AcUsb.Location = new System.Drawing.Point(144, 280);
            this.panel_AcUsb.Margin = new System.Windows.Forms.Padding(2);
            this.panel_AcUsb.Name = "panel_AcUsb";
            this.panel_AcUsb.Size = new System.Drawing.Size(200, 160);
            this.panel_AcUsb.TabIndex = 93;
            // 
            // groupBox_Usb
            // 
            this.groupBox_Usb.Controls.Add(this.label_Usb2);
            this.groupBox_Usb.Controls.Add(this.label_Usb1);
            this.groupBox_Usb.Controls.Add(this.pictureBox_Usb1);
            this.groupBox_Usb.Controls.Add(this.pictureBox_Usb2);
            this.groupBox_Usb.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.groupBox_Usb.Location = new System.Drawing.Point(14, 79);
            this.groupBox_Usb.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Usb.Name = "groupBox_Usb";
            this.groupBox_Usb.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Usb.Size = new System.Drawing.Size(170, 60);
            this.groupBox_Usb.TabIndex = 1;
            this.groupBox_Usb.TabStop = false;
            this.groupBox_Usb.Text = "USB";
            // 
            // label_Usb2
            // 
            this.label_Usb2.BackColor = System.Drawing.Color.Transparent;
            this.label_Usb2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Usb2.ForeColor = System.Drawing.Color.DarkCyan;
            this.label_Usb2.Location = new System.Drawing.Point(76, 16);
            this.label_Usb2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Usb2.Name = "label_Usb2";
            this.label_Usb2.Size = new System.Drawing.Size(32, 32);
            this.label_Usb2.TabIndex = 100;
            this.label_Usb2.Text = "2";
            this.label_Usb2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_Usb1
            // 
            this.label_Usb1.BackColor = System.Drawing.Color.Transparent;
            this.label_Usb1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Usb1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label_Usb1.Location = new System.Drawing.Point(4, 16);
            this.label_Usb1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Usb1.Name = "label_Usb1";
            this.label_Usb1.Size = new System.Drawing.Size(32, 32);
            this.label_Usb1.TabIndex = 99;
            this.label_Usb1.Text = "1";
            this.label_Usb1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox_Usb1
            // 
            this.pictureBox_Usb1.Image = global::AutoTest.Properties.Resources.Switch_to_PC;
            this.pictureBox_Usb1.Location = new System.Drawing.Point(40, 16);
            this.pictureBox_Usb1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Usb1.Name = "pictureBox_Usb1";
            this.pictureBox_Usb1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Usb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Usb1.TabIndex = 97;
            this.pictureBox_Usb1.TabStop = false;
            this.pictureBox_Usb1.Click += new System.EventHandler(this.pictureBox_Usb1_Click);
            // 
            // pictureBox_Usb2
            // 
            this.pictureBox_Usb2.Image = global::AutoTest.Properties.Resources.Switch_to_PC;
            this.pictureBox_Usb2.Location = new System.Drawing.Point(112, 16);
            this.pictureBox_Usb2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Usb2.Name = "pictureBox_Usb2";
            this.pictureBox_Usb2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Usb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Usb2.TabIndex = 98;
            this.pictureBox_Usb2.TabStop = false;
            this.pictureBox_Usb2.Click += new System.EventHandler(this.pictureBox_Usb2_Click);
            // 
            // groupBox_Ac
            // 
            this.groupBox_Ac.Controls.Add(this.label_Ac2);
            this.groupBox_Ac.Controls.Add(this.label_Ac1);
            this.groupBox_Ac.Controls.Add(this.pictureBox_Ac2);
            this.groupBox_Ac.Controls.Add(this.pictureBox_Ac1);
            this.groupBox_Ac.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.groupBox_Ac.Location = new System.Drawing.Point(14, 15);
            this.groupBox_Ac.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox_Ac.Name = "groupBox_Ac";
            this.groupBox_Ac.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox_Ac.Size = new System.Drawing.Size(170, 60);
            this.groupBox_Ac.TabIndex = 0;
            this.groupBox_Ac.TabStop = false;
            this.groupBox_Ac.Text = "AC";
            // 
            // label_Ac2
            // 
            this.label_Ac2.BackColor = System.Drawing.Color.Transparent;
            this.label_Ac2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Ac2.ForeColor = System.Drawing.Color.DarkCyan;
            this.label_Ac2.Location = new System.Drawing.Point(76, 16);
            this.label_Ac2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Ac2.Name = "label_Ac2";
            this.label_Ac2.Size = new System.Drawing.Size(32, 32);
            this.label_Ac2.TabIndex = 96;
            this.label_Ac2.Text = "2";
            this.label_Ac2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label_Ac1
            // 
            this.label_Ac1.BackColor = System.Drawing.Color.Transparent;
            this.label_Ac1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label_Ac1.ForeColor = System.Drawing.Color.DarkCyan;
            this.label_Ac1.Location = new System.Drawing.Point(4, 16);
            this.label_Ac1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_Ac1.Name = "label_Ac1";
            this.label_Ac1.Size = new System.Drawing.Size(32, 32);
            this.label_Ac1.TabIndex = 95;
            this.label_Ac1.Text = "1";
            this.label_Ac1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox_Ac2
            // 
            this.pictureBox_Ac2.Image = global::AutoTest.Properties.Resources.Switch_On_AC;
            this.pictureBox_Ac2.Location = new System.Drawing.Point(112, 16);
            this.pictureBox_Ac2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Ac2.Name = "pictureBox_Ac2";
            this.pictureBox_Ac2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Ac2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Ac2.TabIndex = 1;
            this.pictureBox_Ac2.TabStop = false;
            this.pictureBox_Ac2.Click += new System.EventHandler(this.pictureBox_Ac2_Click);
            // 
            // pictureBox_Ac1
            // 
            this.pictureBox_Ac1.Image = global::AutoTest.Properties.Resources.Switch_On_AC;
            this.pictureBox_Ac1.Location = new System.Drawing.Point(40, 16);
            this.pictureBox_Ac1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Ac1.Name = "pictureBox_Ac1";
            this.pictureBox_Ac1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox_Ac1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Ac1.TabIndex = 0;
            this.pictureBox_Ac1.TabStop = false;
            this.pictureBox_Ac1.Click += new System.EventHandler(this.pictureBox_Ac1_Click);
            // 
            // button_AcUsb
            // 
            this.button_AcUsb.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button_AcUsb.BackColor = System.Drawing.Color.Transparent;
            this.button_AcUsb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button_AcUsb.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_AcUsb.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.button_AcUsb.Location = new System.Drawing.Point(348, 10);
            this.button_AcUsb.Margin = new System.Windows.Forms.Padding(2);
            this.button_AcUsb.Name = "button_AcUsb";
            this.button_AcUsb.Size = new System.Drawing.Size(130, 25);
            this.button_AcUsb.TabIndex = 94;
            this.button_AcUsb.Text = "AC / USB SWITCH";
            this.button_AcUsb.UseVisualStyleBackColor = false;
            this.button_AcUsb.Click += new System.EventHandler(this.button_AcUsb_Click);
            // 
            // pictureBox_BlueRat
            // 
            this.pictureBox_BlueRat.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_BlueRat.Location = new System.Drawing.Point(577, 18);
            this.pictureBox_BlueRat.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_BlueRat.Name = "pictureBox_BlueRat";
            this.pictureBox_BlueRat.Size = new System.Drawing.Size(13, 13);
            this.pictureBox_BlueRat.TabIndex = 84;
            this.pictureBox_BlueRat.TabStop = false;
            // 
            // MiniPicBox
            // 
            this.MiniPicBox.Image = global::AutoTest.Properties.Resources.mini1;
            this.MiniPicBox.Location = new System.Drawing.Point(1404, 8);
            this.MiniPicBox.Margin = new System.Windows.Forms.Padding(2);
            this.MiniPicBox.Name = "MiniPicBox";
            this.MiniPicBox.Size = new System.Drawing.Size(34, 26);
            this.MiniPicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.MiniPicBox.TabIndex = 61;
            this.MiniPicBox.TabStop = false;
            this.MiniPicBox.Visible = false;
            this.MiniPicBox.Click += new System.EventHandler(this.MiniPicBox_Click);
            this.MiniPicBox.MouseEnter += new System.EventHandler(this.MiniPicBox_Enter);
            this.MiniPicBox.MouseLeave += new System.EventHandler(this.MiniPicBox_Leave);
            // 
            // ClosePicBox
            // 
            this.ClosePicBox.Image = global::AutoTest.Properties.Resources.close1;
            this.ClosePicBox.Location = new System.Drawing.Point(1404, 8);
            this.ClosePicBox.Margin = new System.Windows.Forms.Padding(2);
            this.ClosePicBox.Name = "ClosePicBox";
            this.ClosePicBox.Size = new System.Drawing.Size(34, 26);
            this.ClosePicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ClosePicBox.TabIndex = 60;
            this.ClosePicBox.TabStop = false;
            this.ClosePicBox.Visible = false;
            this.ClosePicBox.Click += new System.EventHandler(this.ClosePicBox_Click);
            this.ClosePicBox.MouseEnter += new System.EventHandler(this.ClosePicBox_Enter);
            this.ClosePicBox.MouseLeave += new System.EventHandler(this.ClosePicBox_Leave);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(1404, 34);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(585, 435);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 38;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox_AcPower
            // 
            this.pictureBox_AcPower.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_AcPower.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_AcPower.Image = global::AutoTest.Properties.Resources.OFF;
            this.pictureBox_AcPower.Location = new System.Drawing.Point(841, 18);
            this.pictureBox_AcPower.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_AcPower.Name = "pictureBox_AcPower";
            this.pictureBox_AcPower.Size = new System.Drawing.Size(13, 13);
            this.pictureBox_AcPower.TabIndex = 35;
            this.pictureBox_AcPower.TabStop = false;
            // 
            // pictureBox_Camera
            // 
            this.pictureBox_Camera.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_Camera.Location = new System.Drawing.Point(753, 18);
            this.pictureBox_Camera.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_Camera.Name = "pictureBox_Camera";
            this.pictureBox_Camera.Size = new System.Drawing.Size(13, 13);
            this.pictureBox_Camera.TabIndex = 33;
            this.pictureBox_Camera.TabStop = false;
            // 
            // pictureBox_RedRat
            // 
            this.pictureBox_RedRat.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_RedRat.Location = new System.Drawing.Point(665, 18);
            this.pictureBox_RedRat.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox_RedRat.Name = "pictureBox_RedRat";
            this.pictureBox_RedRat.Size = new System.Drawing.Size(13, 13);
            this.pictureBox_RedRat.TabIndex = 31;
            this.pictureBox_RedRat.TabStop = false;
            // 
            // panelVideo
            // 
            this.panelVideo.BackColor = System.Drawing.Color.Black;
            this.panelVideo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelVideo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVideo.Image = global::AutoTest.Properties.Resources.TV_Screen;
            this.panelVideo.Location = new System.Drawing.Point(144, 40);
            this.panelVideo.Margin = new System.Windows.Forms.Padding(2);
            this.panelVideo.Name = "panelVideo";
            this.panelVideo.Size = new System.Drawing.Size(950, 400);
            this.panelVideo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.panelVideo.TabIndex = 28;
            this.panelVideo.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Location = new System.Drawing.Point(523, 131);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(522, 390);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 38;
            this.pictureBox5.TabStop = false;
            // 
            // comboBox_CameraDevice
            // 
            this.comboBox_CameraDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_CameraDevice.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox_CameraDevice.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.comboBox_CameraDevice.FormattingEnabled = true;
            this.comboBox_CameraDevice.Location = new System.Drawing.Point(844, 40);
            this.comboBox_CameraDevice.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox_CameraDevice.Name = "comboBox_CameraDevice";
            this.comboBox_CameraDevice.Size = new System.Drawing.Size(250, 23);
            this.comboBox_CameraDevice.TabIndex = 95;
            this.comboBox_CameraDevice.SelectedIndexChanged += new System.EventHandler(this.comboBox_CameraDevice_SelectedIndexChanged);
            // 
            // DataGridView_Schedule
            // 
            this.DataGridView_Schedule.AllowUserToResizeColumns = false;
            this.DataGridView_Schedule.AllowUserToResizeRows = false;
            this.DataGridView_Schedule.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DataGridView_Schedule.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.DataGridView_Schedule.BackgroundColor = System.Drawing.Color.White;
            this.DataGridView_Schedule.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DataGridView_Schedule.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.DataGridView_Schedule.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridView_Schedule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridView_Schedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView_Schedule.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.DataGridView_Schedule.GridColor = System.Drawing.SystemColors.ControlLight;
            this.DataGridView_Schedule.Location = new System.Drawing.Point(144, 40);
            this.DataGridView_Schedule.Margin = new System.Windows.Forms.Padding(2);
            this.DataGridView_Schedule.Name = "DataGridView_Schedule";
            this.DataGridView_Schedule.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGridView_Schedule.RowHeadersWidth = 20;
            this.DataGridView_Schedule.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.PeachPuff;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.DataGridView_Schedule.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridView_Schedule.RowTemplate.Height = 24;
            this.DataGridView_Schedule.Size = new System.Drawing.Size(950, 400);
            this.DataGridView_Schedule.TabIndex = 23;
            this.DataGridView_Schedule.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_Schedule_CellMouseDoubleClick);
            this.DataGridView_Schedule.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView1_DataError);
            this.DataGridView_Schedule.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DataGridView1_EditingControlShowing);
            this.DataGridView_Schedule.RowHeaderMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridView_Schedule_RowHeaderMouseDoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.FillWeight = 100.4199F;
            this.Column1.HeaderText = "RC Key";
            this.Column1.MaxDropDownItems = 20;
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Column1.ToolTipText = "_cmd, _log1, _log2, _astro, _quantum, _dektec";
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.FillWeight = 50.18447F;
            this.Column2.HeaderText = "Times";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 50.18447F;
            this.Column3.HeaderText = "Delay";
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.FillWeight = 55.97045F;
            this.Column4.HeaderText = "Picture";
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column4.ToolTipText = "_shot";
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.FillWeight = 130.2147F;
            this.Column5.HeaderText = ">Video Recording\r\n>Dektec";
            this.Column5.Name = "Column5";
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.ToolTipText = "_start, _stop";
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.FillWeight = 120.2417F;
            this.Column6.HeaderText = ">SerialPort\r\n>IO & Keyword";
            this.Column6.Name = "Column6";
            this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column6.ToolTipText = "SerialPort: _save, _clear\r\nIO: _stop, _pause, _ac_restart, _accumulate, _mail, _s" +
    "hot, _rc_, _logcmd\r\nKeyword: _stop, _pause, _ac_restart, _mail, _shot, _rc_, _sa" +
    "velog1, _savelog2, _logcmd";
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.FillWeight = 130.8559F;
            this.Column7.HeaderText = ">AC/USB Switch\r\n>Stream Name";
            this.Column7.Name = "Column7";
            this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column7.ToolTipText = "_on, _off\r\n_AC1_ON, _AC1_OFF, _AC2_ON, _AC2_OFF\r\n_USB1_TV, _USB1_PC, _USB2_TV, _U" +
    "SB2_PC";
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column8.FillWeight = 105.9228F;
            this.Column8.HeaderText = ">Timing\r\n>TV System";
            this.Column8.Name = "Column8";
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column8.ToolTipText = "ATSC, CMMB, DAB, DTMB\r\nDVBT, DVBS, DVBS2_16APSK, DVBS2_32APSK, DVBS2_8PSK, DVBS2_" +
    "QPSK, DVBS2_L3\r\nDVBT, ISDBS, ISDBT, IQ\r\nQAM4, QAM16, QAM32, QAM64, QAM128, QAM25" +
    "6\r\nT2MI";
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column9.FillWeight = 160.2744F;
            this.Column9.HeaderText = ">Quantum Color Space\r\n>Frequency";
            this.Column9.Name = "Column9";
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column10.FillWeight = 47.6081F;
            this.Column10.HeaderText = "Delay";
            this.Column10.Name = "Column10";
            this.Column10.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1106, 554);
            this.Controls.Add(this.button_AcUsb);
            this.Controls.Add(this.panel_AcUsb);
            this.Controls.Add(this.button_InsertRow);
            this.Controls.Add(this.label_TestTime);
            this.Controls.Add(this.label_TestTime_Value);
            this.Controls.Add(this.label_BoxVersion);
            this.Controls.Add(this.labelGPIO_Input);
            this.Controls.Add(this.button_Output);
            this.Controls.Add(this.button_Input);
            this.Controls.Add(this.pictureBox_BlueRat);
            this.Controls.Add(this.label_AutoBox);
            this.Controls.Add(this.label_ScheduleTime);
            this.Controls.Add(this.label_LoopNumber);
            this.Controls.Add(this.label_LoopTimes);
            this.Controls.Add(this.label_FwVersion);
            this.Controls.Add(this.button_Camera);
            this.Controls.Add(this.button_Schedule);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.label_LoopNumber_Value);
            this.Controls.Add(this.label_Camera);
            this.Controls.Add(this.label_RedRat);
            this.Controls.Add(this.button_TimeLine);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label_ScheduleTime_Value);
            this.Controls.Add(this.button_Schedule5);
            this.Controls.Add(this.button_Schedule4);
            this.Controls.Add(this.button_Schedule3);
            this.Controls.Add(this.button_Schedule2);
            this.Controls.Add(this.button_Schedule1);
            this.Controls.Add(this.button_SaveSchedule);
            this.Controls.Add(this.DataBtn);
            this.Controls.Add(this.button_SerialPort2);
            this.Controls.Add(this.button_SerialPort1);
            this.Controls.Add(this.MiniPicBox);
            this.Controls.Add(this.ClosePicBox);
            this.Controls.Add(this.labellabel_LoopTimes_Value);
            this.Controls.Add(this.label_Command);
            this.Controls.Add(this.label_Power);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox_AcPower);
            this.Controls.Add(this.pictureBox_Camera);
            this.Controls.Add(this.pictureBox_RedRat);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.TimeLabel2);
            this.Controls.Add(this.button_Pause);
            this.Controls.Add(this.button_Setting);
            this.Controls.Add(this.button_VirtualRC);
            this.Controls.Add(this.comboBox_CameraDevice);
            this.Controls.Add(this.DataGridView_Schedule);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.panelVideo);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AUTO TEST ASSISTANT";
            this.MinimumSizeChanged += new System.EventHandler(this.MiniPicBox_Click);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GPanelTitleBack_MouseDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel_AcUsb.ResumeLayout(false);
            this.groupBox_Usb.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Usb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Usb2)).EndInit();
            this.groupBox_Ac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Ac2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Ac1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_BlueRat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MiniPicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClosePicBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_AcPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Camera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_RedRat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView_Schedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Setting;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.PictureBox panelVideo;
        private System.Windows.Forms.Label label_Power;
        private System.Windows.Forms.PictureBox pictureBox_AcPower;
        private System.Windows.Forms.Label label_Camera;
        private System.Windows.Forms.PictureBox pictureBox_Camera;
        private System.Windows.Forms.Label label_RedRat;
        private System.Windows.Forms.PictureBox pictureBox_RedRat;
        private System.Windows.Forms.Label labellabel_LoopTimes_Value;
        private System.Windows.Forms.Label label_LoopNumber_Value;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.IO.Ports.SerialPort serialPort2;
        private USBClassLibrary.USBClass USBPort;
        private USBClassLibrary.USBClass.DeviceProperties USBDeviceProperties;
        //bool MyUSBBoxDeviceConnected;
        bool MyUSBRedratDeviceConnected;
        bool MyUSBCameraDeviceConnected;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label_FwVersion;
        private System.Windows.Forms.Button button_SerialPort1;
        private System.Windows.Forms.Button button_SerialPort2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.OpenFileDialog SchOpen1;
        private System.Windows.Forms.OpenFileDialog SchOpen2;
        private System.Windows.Forms.OpenFileDialog SchOpen3;
        private System.Windows.Forms.OpenFileDialog SchOpen4;
        private System.Windows.Forms.OpenFileDialog SchOpen5;
        private System.Windows.Forms.PictureBox ClosePicBox;
        private System.Windows.Forms.PictureBox MiniPicBox;
        protected internal System.Windows.Forms.Button button_SaveSchedule;
        protected internal System.Windows.Forms.Button button_Schedule1;
        protected internal System.Windows.Forms.Button button_Schedule2;
        protected internal System.Windows.Forms.Button button_Schedule3;
        protected internal System.Windows.Forms.Button button_Schedule4;
        protected internal System.Windows.Forms.Button button_Schedule5;
        private System.Windows.Forms.Button DataBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button button_VirtualRC;
        private System.Windows.Forms.Label TimeLabel2;
        private System.Windows.Forms.Label labelSch1Timer;
        private System.Windows.Forms.Label labelSch2Timer;
        private System.Windows.Forms.Label labelSch5Timer;
        private System.Windows.Forms.Label labelSch4Timer;
        private System.Windows.Forms.Label labelSch3Timer;
        private System.Windows.Forms.Button button_Pause;
        private System.Windows.Forms.Label label_ScheduleTime_Value;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label SegLabel4;
        private System.Windows.Forms.Label SegLabel3;
        private System.Windows.Forms.Label SegLabel2;
        private System.Windows.Forms.Label SegLabel1;
        private System.Windows.Forms.Button button_TimeLine;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button button_Schedule;
        private System.Windows.Forms.Button button_Camera;
        private System.Windows.Forms.Label label_LoopTimes;
        private System.Windows.Forms.Label label_LoopNumber;
        private System.Windows.Forms.Label label_ScheduleTime;
        private System.Windows.Forms.Label label_AutoBox;
        private System.Windows.Forms.PictureBox pictureBox_BlueRat;
        private System.IO.Ports.SerialPort serialPort3;
        private System.Windows.Forms.Button button_Input;
        private System.Windows.Forms.Button button_Output;
        private System.Windows.Forms.Label label_Command;
        private System.Windows.Forms.Label labelGPIO_Input;
        private System.Windows.Forms.Label label_BoxVersion;
        private System.Windows.Forms.Label label_TestTime;
        private System.Windows.Forms.Label label_TestTime_Value;
        protected internal System.Windows.Forms.Button button_InsertRow;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.Panel panel_AcUsb;
        private System.Windows.Forms.GroupBox groupBox_Ac;
        private System.Windows.Forms.GroupBox groupBox_Usb;
        private System.Windows.Forms.PictureBox pictureBox_Ac1;
        private System.Windows.Forms.Button button_AcUsb;
        private System.Windows.Forms.PictureBox pictureBox_Ac2;
        private System.Windows.Forms.Label label_Usb2;
        private System.Windows.Forms.Label label_Usb1;
        private System.Windows.Forms.PictureBox pictureBox_Usb1;
        private System.Windows.Forms.PictureBox pictureBox_Usb2;
        private System.Windows.Forms.Label label_Ac2;
        private System.Windows.Forms.Label label_Ac1;
        private System.Windows.Forms.ComboBox comboBox_CameraDevice;
        public SafeDataGridView DataGridView_Schedule;
        public System.Windows.Forms.Button button_Start;
    }
}

