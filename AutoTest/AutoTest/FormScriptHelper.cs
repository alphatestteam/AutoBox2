using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AutoTest
{
    public partial class FormScriptHelper : Form
    {
        public FormScriptHelper()
        {
            InitializeComponent();
        }

        private string RCKey;
        public string RCKeyForm1
        {
            set
            {
                RCKey = value;
            }
        }

        Button[] Buttons;
        public void SetValue(string HeaderText)
        {
            List<string> CmdList = new List<string> { };

            switch (RCKey + " + " + HeaderText)
            {
                case "_cmd + Picture":
                    CmdList.Add("_shot");
                    break;

                case "_cmd + >Video Recording\r\n>Dektec":
                    CmdList.Add("_start");
                    CmdList.Add("_stop");
                    break;

                case "_cmd + >AC/USB Switch\r\n>Stream Name":
                    CmdList.Add("_off");
                    CmdList.Add("_on");
                    CmdList.Add("_AC1_ON");
                    CmdList.Add("_AC1_OFF");
                    CmdList.Add("_AC2_ON");
                    CmdList.Add("_AC2_OFF");
                    CmdList.Add("_USB1_TV");
                    CmdList.Add("_USB1_PC");
                    CmdList.Add("_USB2_TV");
                    CmdList.Add("_USB2_PC");
                    break;

                case "_log1 + >SerialPort\r\n>IO & Keyword":
                    CmdList.Add("_save");
                    CmdList.Add("_clear");
                    break;

                case "_log2 + >SerialPort\r\n>IO & Keyword":
                    CmdList.Add("_save");
                    CmdList.Add("_clear");
                    break;

                case "_PA + >SerialPort\r\n>IO & Keyword":
                    CmdList.Add("_pause");
                    CmdList.Add("_stop");
                    CmdList.Add("_restart");
                    CmdList.Add("_shot");
                    CmdList.Add("_accumulate");
                    CmdList.Add("_mail");
                    CmdList.Add("_rc_");
                    CmdList.Add("_logcmd");
                    break;

                case "_PB + >SerialPort\r\n>IO & Keyword":
                    CmdList.Add("_pause");
                    CmdList.Add("_stop");
                    CmdList.Add("_restart");
                    CmdList.Add("_shot");
                    CmdList.Add("_accumulate");
                    CmdList.Add("_mail");
                    CmdList.Add("_rc_");
                    CmdList.Add("_logcmd");
                    break;

                case "_keyword + >SerialPort\r\n>IO & Keyword":
                    CmdList.Add("_pause");
                    CmdList.Add("_stop");
                    CmdList.Add("_restart");
                    CmdList.Add("_shot");
                    CmdList.Add("_mail");
                    CmdList.Add("_savelog1");
                    CmdList.Add("_savelog2");
                    CmdList.Add("_rc_");
                    CmdList.Add("_logcmd");
                    break;

                default:
                    CmdList.Add("");
                    break;
            }

            Graphics graphics = CreateGraphics();
            float dpiX = graphics.DpiX;
            float dpiY = graphics.DpiY;
            int width, height;
            if (dpiX == 120 && dpiY == 120)
            {
                this.Width = 520;
                this.Height = 90;
                width = 100;
                height = 30;
            }
            else
            {
                this.Width = 415;
                this.Height = 75;
                width = 75;
                height = 25;
            }

            Buttons = new Button[CmdList.Count];

            for (int i = 0; i < Buttons.Length; i++)
            {
                Buttons[i] = new Button
                {
                    Size = new Size(width, height),
                    Text = CmdList[i],
                    AutoSize = false,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    FlatStyle = FlatStyle.System
                };

                if (i <= 4)
                {
                    Buttons[i].Location = new Point(10 + (i * width), 10);
                }
                else if (i > 4 && i <= 9)
                {
                    Buttons[i].Location = new Point(10 + ((i - 5) * width), 50);
                }

                int index = i;
                Buttons[i].Click += (sender1, ex) => Sand_Key(index + 1);
                this.Controls.Add(Buttons[i]);
            }
        }

        private void Sand_Key(int i)
        {
            Form1 lForm1 = (Form1)this.Owner;
            lForm1.StrValue = Buttons[i - 1].Text;
            Close();
        }

        private void FormScriptHelper_Load(object sender, EventArgs e)
        {
            var _point = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            Top = _point.Y + 10;
            Left = _point.X - 50;
        }
    }
}
