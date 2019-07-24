using jini;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoTest
{
    public partial class FormTabControl : Form
    {
        public FormTabControl()
        {
            InitializeComponent();
        }

        //拖動無窗體的控件>>>>>>>>>>>>>>
        [DllImport("user32.dll")]       
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;        
        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        private void Add_TabPage(string str, Form myForm)//載入Form到tab
        {
            if (tabControlCheckHave(tabControl, str))
            {
                return;
            }
            else
            {
                tabControl.TabPages.Add(str);
                tabControl.SelectTab(tabControl.TabPages.Count - 1);

                myForm.FormBorderStyle = FormBorderStyle.None;
                myForm.Dock = DockStyle.Fill;
                myForm.TopLevel = false;
                myForm.Show();
                myForm.Parent = tabControl.SelectedTab;
            }
        }

        private bool tabControlCheckHave(System.Windows.Forms.TabControl tab, String tabName)//設定tab索引編號
        {
            for (int i = 0; i < tab.TabCount; i++)
            {
                if (tab.TabPages[i].Text == tabName)
                {
                    tab.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        private void MainSettingBtn_Click(object sender, EventArgs e)
        {
            Setting FormSetting = new Setting();

            MainSettingBtn.Enabled = false;
            FormSetting.Dock = DockStyle.Fill;
            Add_TabPage("Main Setting", FormSetting);
        }

        private void ScheduleBtn_Click(object sender, EventArgs e)
        {
            FormSchedule FormSchedule = new FormSchedule();

            ScheduleSettingBtn.Enabled = false;
            FormSchedule.Dock = DockStyle.Fill;
            Add_TabPage("Multi Schedule Setting", FormSchedule);
        }

        private void MailSettingBtn_Click(object sender, EventArgs e)
        {
            FormMail FormMail = new FormMail();
            
            MailSettingBtn.Enabled = false;
            FormMail.Dock = DockStyle.Fill;
            Add_TabPage("Mail Setting", FormMail);
        }

        private void LogSettingBtn_Click(object sender, EventArgs e)
        {
            FormLog FormLog = new FormLog();

            LogSettingBtn.Enabled = false;
            FormLog.Dock = DockStyle.Fill;
            Add_TabPage("Log Setting", FormLog);
        }

        private void buttonMonkeyTest_Click(object sender, EventArgs e)
        {
            FormMonkeyTest FormMonkeyTest = new FormMonkeyTest();

            buttonMonkeyTest.Enabled = false;
            FormMonkeyTest.Dock = DockStyle.Fill;
            Add_TabPage("Monket Test", FormMonkeyTest);
        }

        #region 滑鼠拖曳視窗
        private void gPanelTitleBack_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);//*********************調用移動無窗體控件函數
        }
        #endregion

        #region 關閉按鈕
        private void ClosePicBox_Enter(object sender, EventArgs e)
        {
            ClosePicBox.Image = Properties.Resources.close2;
        }

        private void ClosePicBox_Leave(object sender, EventArgs e)
        {
            ClosePicBox.Image = Properties.Resources.close1;
        }
        
        private void ClosePicBox_Click(object sender, EventArgs e)
        {
            if (Global.FormSetting == true && Global.FormSchedule == true && Global.FormMail == true && Global.FormLog == true)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                if(Global.FormSetting == false)
                    MessageBox.Show("Main Setting Error !");

                if (Global.FormSchedule == false)
                    MessageBox.Show("Schedule Setting Error !");

                if (Global.FormMail == false)
                    MessageBox.Show("Mail Setting Error !");

                if (Global.FormLog == false)
                    MessageBox.Show("Log Setting Error !");
            }
        }
        #endregion

        Setting FormSetting = new Setting
        {
            TopLevel = false,
            Dock = DockStyle.Fill
        };

        FormSchedule FormSchedule = new FormSchedule
        {
            TopLevel = false,
            Dock = DockStyle.Fill
        };

        FormMail FormMail = new FormMail
        {
            TopLevel = false,
            Dock = DockStyle.Fill
        };

        FormLog FormLog = new FormLog
        {
            TopLevel = false,
            Dock = DockStyle.Fill
        };

        private void FormTabControl_Load(object sender, EventArgs e)
        {
            FormSetting.Show();
            tabControl.TabPages[0].Controls.Add(FormSetting);
            
            FormSchedule.Show();
            tabControl.TabPages[1].Controls.Add(FormSchedule);
            
            FormMail.Show();
            tabControl.TabPages[2].Controls.Add(FormMail);
            
            FormLog.Show();
            tabControl.TabPages[3].Controls.Add(FormLog);
        }

        private void FormTabControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Global.FormSetting == true && 
                Global.FormSchedule == true && 
                Global.FormMail == true && 
                Global.FormLog == true)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                if (Global.FormSetting == false)
                {
                    MessageBox.Show("Settings do not save", "Main Setting", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    tabControl.SelectedTab = tabPage_MainSetting;
                }
                    

                if (Global.FormSchedule == false)
                {
                    MessageBox.Show("Settings do not save", "Schedule", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    tabControl.SelectedTab = tabPage_MultiSchedule;
                }
                    
                if (Global.FormMail == false)
                {
                    MessageBox.Show("Settings do not save", "Mail", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    tabControl.SelectedTab = tabPage_Mail;
                }
                    

                if (Global.FormLog == false)
                {
                    MessageBox.Show("Settings do not save", "Keyword", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    tabControl.SelectedTab = tabPage_KeywordSearch;
                }
                    
                e.Cancel = true;
            }
        }

        private void FormTabControl_FormClosed(object sender, FormClosedEventArgs e)
        {
            ini12.INIWrite(Global.MainSettingPath, "Comport", "PortName", FormSetting.comboBox_SerialPort1_PortName_Value.Text);
            ini12.INIWrite(Global.MainSettingPath, "ExtComport", "PortName", FormSetting.comboBox_SerialPort2_PortName_Value.Text);
        }
    }
}
