namespace AutoTest
{
    partial class FormMonkeyTest
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
            this.buttonRunAll = new System.Windows.Forms.Button();
            this.buttonLoadApps = new System.Windows.Forms.Button();
            this.comboxQcProName = new System.Windows.Forms.ComboBox();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.checkBoxBasic = new System.Windows.Forms.CheckBox();
            this.checkBoxSpecified = new System.Windows.Forms.CheckBox();
            this.SavedLabel = new System.Windows.Forms.Label();
            this.buttonMonkeyPath = new System.Windows.Forms.Button();
            this.textBoxMonkeyPath = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.labelMonkeyPath = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxMonkeyRunner = new System.Windows.Forms.CheckBox();
            this.labelSDKTools = new System.Windows.Forms.Label();
            this.textBoxSDKTools = new System.Windows.Forms.TextBox();
            this.buttonSDKTools = new System.Windows.Forms.Button();
            this.labelMunkeyRunnerScheduler = new System.Windows.Forms.Label();
            this.textBoxMunkeyRunnerScheduler = new System.Windows.Forms.TextBox();
            this.buttonMunkeyRunnerScheduler = new System.Windows.Forms.Button();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxConnectMode = new System.Windows.Forms.GroupBox();
            this.checkBoxUSBDebbug = new System.Windows.Forms.CheckBox();
            this.textBoxTVIP = new System.Windows.Forms.TextBox();
            this.checkBoxEthernet = new System.Windows.Forms.CheckBox();
            this.groupBoxConnectMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRunAll
            // 
            this.buttonRunAll.Location = new System.Drawing.Point(197, 519);
            this.buttonRunAll.Name = "buttonRunAll";
            this.buttonRunAll.Size = new System.Drawing.Size(238, 37);
            this.buttonRunAll.TabIndex = 0;
            this.buttonRunAll.Text = "Monkey Test (All)";
            this.buttonRunAll.UseVisualStyleBackColor = true;
            this.buttonRunAll.Visible = false;
            this.buttonRunAll.Click += new System.EventHandler(this.buttonRunAll_Click);
            // 
            // buttonLoadApps
            // 
            this.buttonLoadApps.Location = new System.Drawing.Point(333, 306);
            this.buttonLoadApps.Name = "buttonLoadApps";
            this.buttonLoadApps.Size = new System.Drawing.Size(90, 25);
            this.buttonLoadApps.TabIndex = 1;
            this.buttonLoadApps.Text = "Load Apps";
            this.buttonLoadApps.UseVisualStyleBackColor = true;
            this.buttonLoadApps.Click += new System.EventHandler(this.buttonLoadApps_Click);
            // 
            // comboxQcProName
            // 
            this.comboxQcProName.FormattingEnabled = true;
            this.comboxQcProName.Location = new System.Drawing.Point(40, 306);
            this.comboxQcProName.Name = "comboxQcProName";
            this.comboxQcProName.Size = new System.Drawing.Size(287, 25);
            this.comboxQcProName.TabIndex = 2;
            // 
            // SaveBtn
            // 
            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SaveBtn.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SaveBtn.Location = new System.Drawing.Point(350, 590);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(85, 30);
            this.SaveBtn.TabIndex = 32;
            this.SaveBtn.Text = "SAVE";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // checkBoxBasic
            // 
            this.checkBoxBasic.AutoSize = true;
            this.checkBoxBasic.Location = new System.Drawing.Point(40, 239);
            this.checkBoxBasic.Name = "checkBoxBasic";
            this.checkBoxBasic.Size = new System.Drawing.Size(127, 21);
            this.checkBoxBasic.TabIndex = 33;
            this.checkBoxBasic.Text = "Basic MokeyTest";
            this.checkBoxBasic.UseVisualStyleBackColor = true;
            this.checkBoxBasic.CheckedChanged += new System.EventHandler(this.checkBoxBasic_CheckedChanged);
            // 
            // checkBoxSpecified
            // 
            this.checkBoxSpecified.AutoSize = true;
            this.checkBoxSpecified.Location = new System.Drawing.Point(40, 279);
            this.checkBoxSpecified.Name = "checkBoxSpecified";
            this.checkBoxSpecified.Size = new System.Drawing.Size(246, 21);
            this.checkBoxSpecified.TabIndex = 35;
            this.checkBoxSpecified.Text = "Select one of the specified categories";
            this.checkBoxSpecified.UseVisualStyleBackColor = true;
            this.checkBoxSpecified.CheckedChanged += new System.EventHandler(this.checkBoxSpecified_CheckedChanged);
            // 
            // SavedLabel
            // 
            this.SavedLabel.Font = new System.Drawing.Font("微軟正黑體", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SavedLabel.ForeColor = System.Drawing.Color.Red;
            this.SavedLabel.Location = new System.Drawing.Point(12, 592);
            this.SavedLabel.Name = "SavedLabel";
            this.SavedLabel.Size = new System.Drawing.Size(316, 24);
            this.SavedLabel.TabIndex = 77;
            // 
            // buttonMonkeyPath
            // 
            this.buttonMonkeyPath.Location = new System.Drawing.Point(398, 164);
            this.buttonMonkeyPath.Name = "buttonMonkeyPath";
            this.buttonMonkeyPath.Size = new System.Drawing.Size(25, 25);
            this.buttonMonkeyPath.TabIndex = 78;
            this.buttonMonkeyPath.Text = "...";
            this.buttonMonkeyPath.UseVisualStyleBackColor = true;
            this.buttonMonkeyPath.Click += new System.EventHandler(this.buttonMonkeyPath_Click);
            // 
            // textBoxMonkeyPath
            // 
            this.textBoxMonkeyPath.ForeColor = System.Drawing.Color.Black;
            this.textBoxMonkeyPath.Location = new System.Drawing.Point(40, 164);
            this.textBoxMonkeyPath.Name = "textBoxMonkeyPath";
            this.textBoxMonkeyPath.Size = new System.Drawing.Size(352, 25);
            this.textBoxMonkeyPath.TabIndex = 79;
            // 
            // labelMonkeyPath
            // 
            this.labelMonkeyPath.AutoSize = true;
            this.labelMonkeyPath.BackColor = System.Drawing.Color.Transparent;
            this.labelMonkeyPath.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.labelMonkeyPath.Location = new System.Drawing.Point(37, 192);
            this.labelMonkeyPath.Name = "labelMonkeyPath";
            this.labelMonkeyPath.Size = new System.Drawing.Size(291, 17);
            this.labelMonkeyPath.TabIndex = 80;
            this.labelMonkeyPath.Text = "Browse the path to save report for MonkeyTest";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(309, 562);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 22);
            this.button1.TabIndex = 81;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxMonkeyRunner
            // 
            this.checkBoxMonkeyRunner.AutoSize = true;
            this.checkBoxMonkeyRunner.Location = new System.Drawing.Point(40, 373);
            this.checkBoxMonkeyRunner.Name = "checkBoxMonkeyRunner";
            this.checkBoxMonkeyRunner.Size = new System.Drawing.Size(121, 21);
            this.checkBoxMonkeyRunner.TabIndex = 82;
            this.checkBoxMonkeyRunner.Text = "MonkeyRunner";
            this.checkBoxMonkeyRunner.UseVisualStyleBackColor = true;
            this.checkBoxMonkeyRunner.CheckedChanged += new System.EventHandler(this.checkBoxMonkeyRunner_CheckedChanged);
            // 
            // labelSDKTools
            // 
            this.labelSDKTools.AutoSize = true;
            this.labelSDKTools.BackColor = System.Drawing.Color.Transparent;
            this.labelSDKTools.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.labelSDKTools.Location = new System.Drawing.Point(37, 428);
            this.labelSDKTools.Name = "labelSDKTools";
            this.labelSDKTools.Size = new System.Drawing.Size(99, 17);
            this.labelSDKTools.TabIndex = 85;
            this.labelSDKTools.Text = "SDK Tools Path";
            // 
            // textBoxSDKTools
            // 
            this.textBoxSDKTools.ForeColor = System.Drawing.Color.Black;
            this.textBoxSDKTools.Location = new System.Drawing.Point(40, 400);
            this.textBoxSDKTools.Name = "textBoxSDKTools";
            this.textBoxSDKTools.Size = new System.Drawing.Size(352, 25);
            this.textBoxSDKTools.TabIndex = 84;
            // 
            // buttonSDKTools
            // 
            this.buttonSDKTools.Location = new System.Drawing.Point(398, 400);
            this.buttonSDKTools.Name = "buttonSDKTools";
            this.buttonSDKTools.Size = new System.Drawing.Size(25, 25);
            this.buttonSDKTools.TabIndex = 83;
            this.buttonSDKTools.Text = "...";
            this.buttonSDKTools.UseVisualStyleBackColor = true;
            this.buttonSDKTools.Click += new System.EventHandler(this.buttonSDKTools_Click);
            // 
            // labelMunkeyRunnerScheduler
            // 
            this.labelMunkeyRunnerScheduler.AutoSize = true;
            this.labelMunkeyRunnerScheduler.BackColor = System.Drawing.Color.Transparent;
            this.labelMunkeyRunnerScheduler.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.labelMunkeyRunnerScheduler.Location = new System.Drawing.Point(37, 486);
            this.labelMunkeyRunnerScheduler.Name = "labelMunkeyRunnerScheduler";
            this.labelMunkeyRunnerScheduler.Size = new System.Drawing.Size(164, 17);
            this.labelMunkeyRunnerScheduler.TabIndex = 88;
            this.labelMunkeyRunnerScheduler.Text = "MunkeyRunner Scheduler";
            // 
            // textBoxMunkeyRunnerScheduler
            // 
            this.textBoxMunkeyRunnerScheduler.ForeColor = System.Drawing.Color.Black;
            this.textBoxMunkeyRunnerScheduler.Location = new System.Drawing.Point(40, 458);
            this.textBoxMunkeyRunnerScheduler.Name = "textBoxMunkeyRunnerScheduler";
            this.textBoxMunkeyRunnerScheduler.Size = new System.Drawing.Size(352, 25);
            this.textBoxMunkeyRunnerScheduler.TabIndex = 87;
            // 
            // buttonMunkeyRunnerScheduler
            // 
            this.buttonMunkeyRunnerScheduler.Location = new System.Drawing.Point(398, 458);
            this.buttonMunkeyRunnerScheduler.Name = "buttonMunkeyRunnerScheduler";
            this.buttonMunkeyRunnerScheduler.Size = new System.Drawing.Size(25, 25);
            this.buttonMunkeyRunnerScheduler.TabIndex = 86;
            this.buttonMunkeyRunnerScheduler.Text = "...";
            this.buttonMunkeyRunnerScheduler.UseVisualStyleBackColor = true;
            this.buttonMunkeyRunnerScheduler.Click += new System.EventHandler(this.buttonMunkeyRunnerScheduler_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBoxConnectMode
            // 
            this.groupBoxConnectMode.Controls.Add(this.checkBoxUSBDebbug);
            this.groupBoxConnectMode.Controls.Add(this.textBoxTVIP);
            this.groupBoxConnectMode.Controls.Add(this.checkBoxEthernet);
            this.groupBoxConnectMode.ForeColor = System.Drawing.Color.DarkOrange;
            this.groupBoxConnectMode.Location = new System.Drawing.Point(40, 23);
            this.groupBoxConnectMode.Name = "groupBoxConnectMode";
            this.groupBoxConnectMode.Size = new System.Drawing.Size(383, 102);
            this.groupBoxConnectMode.TabIndex = 89;
            this.groupBoxConnectMode.TabStop = false;
            this.groupBoxConnectMode.Text = "Select Connection Mode";
            // 
            // checkBoxUSBDebbug
            // 
            this.checkBoxUSBDebbug.AutoSize = true;
            this.checkBoxUSBDebbug.ForeColor = System.Drawing.Color.Black;
            this.checkBoxUSBDebbug.Location = new System.Drawing.Point(32, 66);
            this.checkBoxUSBDebbug.Name = "checkBoxUSBDebbug";
            this.checkBoxUSBDebbug.Size = new System.Drawing.Size(104, 21);
            this.checkBoxUSBDebbug.TabIndex = 2;
            this.checkBoxUSBDebbug.Text = "USB Debbug";
            this.checkBoxUSBDebbug.UseVisualStyleBackColor = true;
            this.checkBoxUSBDebbug.CheckedChanged += new System.EventHandler(this.checkBoxUSBDebbug_CheckedChanged);
            // 
            // textBoxTVIP
            // 
            this.textBoxTVIP.ForeColor = System.Drawing.Color.Crimson;
            this.textBoxTVIP.Location = new System.Drawing.Point(117, 28);
            this.textBoxTVIP.Name = "textBoxTVIP";
            this.textBoxTVIP.Size = new System.Drawing.Size(129, 25);
            this.textBoxTVIP.TabIndex = 1;
            this.textBoxTVIP.Text = "Enter TV IP";
            // 
            // checkBoxEthernet
            // 
            this.checkBoxEthernet.AutoSize = true;
            this.checkBoxEthernet.ForeColor = System.Drawing.Color.Black;
            this.checkBoxEthernet.Location = new System.Drawing.Point(32, 30);
            this.checkBoxEthernet.Name = "checkBoxEthernet";
            this.checkBoxEthernet.Size = new System.Drawing.Size(79, 21);
            this.checkBoxEthernet.TabIndex = 0;
            this.checkBoxEthernet.Text = "Ethernet";
            this.checkBoxEthernet.UseVisualStyleBackColor = true;
            this.checkBoxEthernet.CheckedChanged += new System.EventHandler(this.checkBoxEthernet_CheckedChanged);
            // 
            // FormMonkeyTest
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(455, 665);
            this.Controls.Add(this.groupBoxConnectMode);
            this.Controls.Add(this.labelMunkeyRunnerScheduler);
            this.Controls.Add(this.textBoxMunkeyRunnerScheduler);
            this.Controls.Add(this.buttonMunkeyRunnerScheduler);
            this.Controls.Add(this.labelSDKTools);
            this.Controls.Add(this.textBoxSDKTools);
            this.Controls.Add(this.buttonSDKTools);
            this.Controls.Add(this.checkBoxMonkeyRunner);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelMonkeyPath);
            this.Controls.Add(this.textBoxMonkeyPath);
            this.Controls.Add(this.buttonMonkeyPath);
            this.Controls.Add(this.SavedLabel);
            this.Controls.Add(this.checkBoxSpecified);
            this.Controls.Add(this.checkBoxBasic);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.comboxQcProName);
            this.Controls.Add(this.buttonLoadApps);
            this.Controls.Add(this.buttonRunAll);
            this.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMonkeyTest";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.FormMonkeyTest_Load);
            this.groupBoxConnectMode.ResumeLayout(false);
            this.groupBoxConnectMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRunAll;
        private System.Windows.Forms.Button buttonLoadApps;
        private System.Windows.Forms.ComboBox comboxQcProName;
        private System.Windows.Forms.Button SaveBtn;
        private System.Windows.Forms.CheckBox checkBoxBasic;
        private System.Windows.Forms.CheckBox checkBoxSpecified;
        private System.Windows.Forms.Label SavedLabel;
        private System.Windows.Forms.Button buttonMonkeyPath;
        private System.Windows.Forms.TextBox textBoxMonkeyPath;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label labelMonkeyPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxMonkeyRunner;
        private System.Windows.Forms.Label labelSDKTools;
        private System.Windows.Forms.TextBox textBoxSDKTools;
        private System.Windows.Forms.Button buttonSDKTools;
        private System.Windows.Forms.Label labelMunkeyRunnerScheduler;
        private System.Windows.Forms.TextBox textBoxMunkeyRunnerScheduler;
        private System.Windows.Forms.Button buttonMunkeyRunnerScheduler;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBoxConnectMode;
        private System.Windows.Forms.CheckBox checkBoxUSBDebbug;
        private System.Windows.Forms.TextBox textBoxTVIP;
        private System.Windows.Forms.CheckBox checkBoxEthernet;
    }
}