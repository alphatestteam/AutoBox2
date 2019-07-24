namespace AutoTest
{
    partial class FormTabControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTabControl));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.MainSettingBtn = new System.Windows.Forms.Button();
            this.ScheduleSettingBtn = new System.Windows.Forms.Button();
            this.MailSettingBtn = new System.Windows.Forms.Button();
            this.LogSettingBtn = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_MainSetting = new System.Windows.Forms.TabPage();
            this.tabPage_MultiSchedule = new System.Windows.Forms.TabPage();
            this.tabPage_Mail = new System.Windows.Forms.TabPage();
            this.tabPage_KeywordSearch = new System.Windows.Forms.TabPage();
            this.buttonMonkeyTest = new System.Windows.Forms.Button();
            this.ClosePicBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ClosePicBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(6, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(682, 22);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShowShortcutKeys = false;
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 18);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShowShortcutKeys = false;
            this.toolStripMenuItem2.Size = new System.Drawing.Size(133, 18);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.ShowShortcutKeys = false;
            this.toolStripMenuItem3.Size = new System.Drawing.Size(133, 18);
            this.toolStripMenuItem3.Text = "toolStripMenuItem3";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(133, 18);
            this.toolStripMenuItem4.Text = "toolStripMenuItem4";
            // 
            // MainSettingBtn
            // 
            this.MainSettingBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MainSettingBtn.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MainSettingBtn.Location = new System.Drawing.Point(9, 101);
            this.MainSettingBtn.Margin = new System.Windows.Forms.Padding(2);
            this.MainSettingBtn.Name = "MainSettingBtn";
            this.MainSettingBtn.Size = new System.Drawing.Size(128, 31);
            this.MainSettingBtn.TabIndex = 2;
            this.MainSettingBtn.Text = "Main Setting";
            this.MainSettingBtn.UseVisualStyleBackColor = true;
            this.MainSettingBtn.Click += new System.EventHandler(this.MainSettingBtn_Click);
            // 
            // ScheduleSettingBtn
            // 
            this.ScheduleSettingBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ScheduleSettingBtn.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ScheduleSettingBtn.Location = new System.Drawing.Point(9, 135);
            this.ScheduleSettingBtn.Margin = new System.Windows.Forms.Padding(2);
            this.ScheduleSettingBtn.Name = "ScheduleSettingBtn";
            this.ScheduleSettingBtn.Size = new System.Drawing.Size(128, 31);
            this.ScheduleSettingBtn.TabIndex = 3;
            this.ScheduleSettingBtn.Text = "Multi Schedule";
            this.ScheduleSettingBtn.UseVisualStyleBackColor = true;
            this.ScheduleSettingBtn.Click += new System.EventHandler(this.ScheduleBtn_Click);
            // 
            // MailSettingBtn
            // 
            this.MailSettingBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.MailSettingBtn.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.MailSettingBtn.Location = new System.Drawing.Point(9, 170);
            this.MailSettingBtn.Margin = new System.Windows.Forms.Padding(2);
            this.MailSettingBtn.Name = "MailSettingBtn";
            this.MailSettingBtn.Size = new System.Drawing.Size(128, 31);
            this.MailSettingBtn.TabIndex = 62;
            this.MailSettingBtn.Text = "Mail Setting";
            this.MailSettingBtn.UseVisualStyleBackColor = true;
            this.MailSettingBtn.Click += new System.EventHandler(this.MailSettingBtn_Click);
            // 
            // LogSettingBtn
            // 
            this.LogSettingBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.LogSettingBtn.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LogSettingBtn.Location = new System.Drawing.Point(9, 204);
            this.LogSettingBtn.Margin = new System.Windows.Forms.Padding(2);
            this.LogSettingBtn.Name = "LogSettingBtn";
            this.LogSettingBtn.Size = new System.Drawing.Size(128, 31);
            this.LogSettingBtn.TabIndex = 63;
            this.LogSettingBtn.Text = "Log Setting";
            this.LogSettingBtn.UseVisualStyleBackColor = true;
            this.LogSettingBtn.Click += new System.EventHandler(this.LogSettingBtn_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_MainSetting);
            this.tabControl.Controls.Add(this.tabPage_MultiSchedule);
            this.tabControl.Controls.Add(this.tabPage_Mail);
            this.tabControl.Controls.Add(this.tabPage_KeywordSearch);
            this.tabControl.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tabControl.Location = new System.Drawing.Point(9, 9);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(50, 3);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(664, 541);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl.TabIndex = 1;
            // 
            // tabPage_MainSetting
            // 
            this.tabPage_MainSetting.Location = new System.Drawing.Point(4, 27);
            this.tabPage_MainSetting.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_MainSetting.Name = "tabPage_MainSetting";
            this.tabPage_MainSetting.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_MainSetting.Size = new System.Drawing.Size(656, 510);
            this.tabPage_MainSetting.TabIndex = 0;
            this.tabPage_MainSetting.Text = "Main Setting";
            this.tabPage_MainSetting.UseVisualStyleBackColor = true;
            // 
            // tabPage_MultiSchedule
            // 
            this.tabPage_MultiSchedule.Location = new System.Drawing.Point(4, 27);
            this.tabPage_MultiSchedule.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_MultiSchedule.Name = "tabPage_MultiSchedule";
            this.tabPage_MultiSchedule.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_MultiSchedule.Size = new System.Drawing.Size(656, 510);
            this.tabPage_MultiSchedule.TabIndex = 1;
            this.tabPage_MultiSchedule.Text = "Schedule";
            this.tabPage_MultiSchedule.UseVisualStyleBackColor = true;
            // 
            // tabPage_Mail
            // 
            this.tabPage_Mail.Location = new System.Drawing.Point(4, 27);
            this.tabPage_Mail.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_Mail.Name = "tabPage_Mail";
            this.tabPage_Mail.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_Mail.Size = new System.Drawing.Size(656, 510);
            this.tabPage_Mail.TabIndex = 2;
            this.tabPage_Mail.Text = "Mail";
            this.tabPage_Mail.UseVisualStyleBackColor = true;
            // 
            // tabPage_KeywordSearch
            // 
            this.tabPage_KeywordSearch.Location = new System.Drawing.Point(4, 27);
            this.tabPage_KeywordSearch.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage_KeywordSearch.Name = "tabPage_KeywordSearch";
            this.tabPage_KeywordSearch.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage_KeywordSearch.Size = new System.Drawing.Size(656, 510);
            this.tabPage_KeywordSearch.TabIndex = 3;
            this.tabPage_KeywordSearch.Text = "Keyword";
            this.tabPage_KeywordSearch.UseVisualStyleBackColor = true;
            // 
            // buttonMonkeyTest
            // 
            this.buttonMonkeyTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonMonkeyTest.Font = new System.Drawing.Font("Microsoft JhengHei UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.buttonMonkeyTest.Location = new System.Drawing.Point(9, 238);
            this.buttonMonkeyTest.Margin = new System.Windows.Forms.Padding(2);
            this.buttonMonkeyTest.Name = "buttonMonkeyTest";
            this.buttonMonkeyTest.Size = new System.Drawing.Size(128, 31);
            this.buttonMonkeyTest.TabIndex = 64;
            this.buttonMonkeyTest.Text = "MonkeyTest";
            this.buttonMonkeyTest.UseVisualStyleBackColor = true;
            this.buttonMonkeyTest.Visible = false;
            this.buttonMonkeyTest.Click += new System.EventHandler(this.buttonMonkeyTest_Click);
            // 
            // ClosePicBox
            // 
            this.ClosePicBox.BackColor = System.Drawing.Color.Transparent;
            this.ClosePicBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClosePicBox.Image = global::AutoTest.Properties.Resources.close1;
            this.ClosePicBox.Location = new System.Drawing.Point(699, -7);
            this.ClosePicBox.Margin = new System.Windows.Forms.Padding(2);
            this.ClosePicBox.Name = "ClosePicBox";
            this.ClosePicBox.Size = new System.Drawing.Size(34, 26);
            this.ClosePicBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ClosePicBox.TabIndex = 61;
            this.ClosePicBox.TabStop = false;
            this.ClosePicBox.Click += new System.EventHandler(this.ClosePicBox_Click);
            this.ClosePicBox.MouseEnter += new System.EventHandler(this.ClosePicBox_Enter);
            this.ClosePicBox.MouseLeave += new System.EventHandler(this.ClosePicBox_Leave);
            // 
            // FormTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(682, 558);
            this.Controls.Add(this.ClosePicBox);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.MailSettingBtn);
            this.Controls.Add(this.ScheduleSettingBtn);
            this.Controls.Add(this.MainSettingBtn);
            this.Controls.Add(this.LogSettingBtn);
            this.Controls.Add(this.buttonMonkeyTest);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTabControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SETTING";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTabControl_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTabControl_FormClosed);
            this.Load += new System.EventHandler(this.FormTabControl_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gPanelTitleBack_MouseDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ClosePicBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.Button MainSettingBtn;
        private System.Windows.Forms.Button ScheduleSettingBtn;
        private System.Windows.Forms.PictureBox ClosePicBox;
        private System.Windows.Forms.Button MailSettingBtn;
        private System.Windows.Forms.Button LogSettingBtn;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button buttonMonkeyTest;
        private System.Windows.Forms.TabPage tabPage_MainSetting;
        private System.Windows.Forms.TabPage tabPage_MultiSchedule;
        private System.Windows.Forms.TabPage tabPage_Mail;
        private System.Windows.Forms.TabPage tabPage_KeywordSearch;
    }
}