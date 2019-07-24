namespace AutoTest
{
    partial class FormSurp
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
            this.pictureHW = new System.Windows.Forms.PictureBox();
            this.ImportDB = new System.Windows.Forms.CheckBox();
            this.CloseBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureHW)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureHW
            // 
            this.pictureHW.Image = global::AutoTest.Properties.Resources.Surprise;
            this.pictureHW.Location = new System.Drawing.Point(19, 17);
            this.pictureHW.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureHW.Name = "pictureHW";
            this.pictureHW.Size = new System.Drawing.Size(600, 519);
            this.pictureHW.TabIndex = 0;
            this.pictureHW.TabStop = false;
            // 
            // ImportDB
            // 
            this.ImportDB.AutoSize = true;
            this.ImportDB.Location = new System.Drawing.Point(19, 543);
            this.ImportDB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ImportDB.Name = "ImportDB";
            this.ImportDB.Size = new System.Drawing.Size(122, 19);
            this.ImportDB.TabIndex = 58;
            this.ImportDB.Text = "Import Database";
            this.ImportDB.UseVisualStyleBackColor = true;
            this.ImportDB.CheckedChanged += new System.EventHandler(this.ImportDB_CheckedChanged);
            // 
            // CloseBtn
            // 
            this.CloseBtn.Location = new System.Drawing.Point(511, 543);
            this.CloseBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(107, 31);
            this.CloseBtn.TabIndex = 59;
            this.CloseBtn.Text = "Close";
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormSurp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 587);
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.ImportDB);
            this.Controls.Add(this.pictureHW);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FormSurp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormSurp";
            this.Load += new System.EventHandler(this.Setting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureHW)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureHW;
        private System.Windows.Forms.CheckBox ImportDB;
        private System.Windows.Forms.Button CloseBtn;

    }
}