using jini;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AutoTest
{
    public partial class FormSchedule : Form
    {
        public FormSchedule()
        {
            InitializeComponent();
        }

        private void FormSchedule_Load(object sender, EventArgs e)
        {
            textBox_Schedule1.Text = ini12.INIRead(Global.MainSettingPath, "Schedule1", "Path", "");
            textBox_Schedule1Loop.Text = ini12.INIRead(Global.MainSettingPath, "Schedule1", "Loop", "");

            if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "Exist", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule2", "Exist", "")) == 1)
                {
                    checkBox_Schedule2.Checked = true;
                    textBox_Schedule2.Text = ini12.INIRead(Global.MainSettingPath, "Schedule2", "Path", "");
                    textBox_Schedule2Loop.Text = ini12.INIRead(Global.MainSettingPath, "Schedule2", "Loop", "");
                }
                else
                {
                    checkBox_Schedule2.Checked = false;
                    button_Schedule2.Enabled = false;
                    textBox_Schedule2.Enabled = false;
                    textBox_Schedule2Loop.Enabled = false;
                    checkBox_Timer2.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule2", "Exist", "0");
                ini12.INIWrite(Global.MainSettingPath, "Schedule2", "Loop", "0");
                checkBox_Schedule2.Checked = false;
                button_Schedule2.Enabled = false;
                textBox_Schedule2.Enabled = false;
                textBox_Schedule2Loop.Enabled = false;
                checkBox_Timer2.Enabled = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Schedule3", "Exist", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule3", "Exist", "")) == 1)
                {
                    checkBox_Schedule3.Checked = true;
                    textBox_Schedule3.Text = ini12.INIRead(Global.MainSettingPath, "Schedule3", "Path", "");
                    textBox_Schedule3Loop.Text = ini12.INIRead(Global.MainSettingPath, "Schedule3", "Loop", "");
                }
                else
                {
                    checkBox_Schedule3.Checked = false;
                    button_Schedule3.Enabled = false;
                    textBox_Schedule3.Enabled = false;
                    textBox_Schedule3Loop.Enabled = false;
                    checkBox_Timer3.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Exist", "0");
                ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Loop", "0");
                checkBox_Schedule3.Checked = false;
                button_Schedule3.Enabled = false;
                textBox_Schedule3.Enabled = false;
                textBox_Schedule3Loop.Enabled = false;
                checkBox_Timer3.Enabled = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Schedule4", "Exist", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule4", "Exist", "")) == 1)
                {
                    checkBox_Schedule4.Checked = true;
                    textBox_Schedule4.Text = ini12.INIRead(Global.MainSettingPath, "Schedule4", "Path", "");
                    textBox_Schedule4Loop.Text = ini12.INIRead(Global.MainSettingPath, "Schedule4", "Loop", "");
                }
                else
                {
                    checkBox_Schedule4.Checked = false;
                    button_Schedule4.Enabled = false;
                    textBox_Schedule4.Enabled = false;
                    textBox_Schedule4Loop.Enabled = false;
                    checkBox_Timer4.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Exist", "0");
                ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Loop", "0");
                checkBox_Schedule4.Checked = false;
                button_Schedule4.Enabled = false;
                textBox_Schedule4.Enabled = false;
                textBox_Schedule4Loop.Enabled = false;
                checkBox_Timer4.Enabled = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Schedule5", "Exist", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule5", "Exist", "")) == 1)
                {
                    checkBox_Schedule5.Checked = true;
                    textBox_Schedule5.Text = ini12.INIRead(Global.MainSettingPath, "Schedule5", "Path", "");
                    textBox_Schedule5Loop.Text = ini12.INIRead(Global.MainSettingPath, "Schedule5", "Loop", "");
                }
                else
                {
                    checkBox_Schedule5.Checked = false;
                    button_Schedule5.Enabled = false;
                    textBox_Schedule5.Enabled = false;
                    textBox_Schedule5Loop.Enabled = false;
                    checkBox_Timer5.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Exist", "0");
                ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Loop", "0");
                checkBox_Schedule5.Checked = false;
                button_Schedule5.Enabled = false;
                textBox_Schedule5.Enabled = false;
                textBox_Schedule5Loop.Enabled = false;
                checkBox_Timer5.Enabled = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Record", "CompareChoose", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Record", "CompareChoose", "")) == 1)
                {
                    checkBox_Similarity.Checked = true;
                    comboBox_Similarity.Enabled = true;
                    comboBox_Similarity.Text = (100 - int.Parse(ini12.INIRead(Global.MainSettingPath, "Record", "CompareDifferent", ""))).ToString() + "%";
                }
                else
                {
                    checkBox_Similarity.Checked = false;
                    comboBox_Similarity.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "CompareChoose", "0");
                checkBox_Similarity.Checked = false;
                comboBox_Similarity.Enabled = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Record", "Footprint Mode", "") == "1")
            {
                checkBox_FootprintMode.Checked = true;
            }
            else
            {
                checkBox_FootprintMode.Checked = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Record", "EachVideo", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Record", "EachVideo", "")) == 1)
                    checkBox_VideoRecord.Checked = true;
                else
                    checkBox_VideoRecord.Checked = false;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "EachVideo", "0");
            }

            if (ini12.INIRead(Global.MainSettingPath, "Device", "RunAfterStartUp", "") == "1")
            {
                checkBox_ScheduleAutoStart.Checked = true;
            }
            else
            {
                checkBox_ScheduleAutoStart.Checked = false;
            }

            #region Timer
            if (ini12.INIRead(Global.MainSettingPath, "Schedule1", "OnTimeStart", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule1", "OnTimeStart", "")) == 1)
                {
                    dateTimePicker_Sch1.Text = ini12.INIRead(Global.MainSettingPath, "Schedule1", "Timer", "");       //Schedule1 Timer
                    checkBox_Timer1.Checked = true;
                    dateTimePicker_Sch1.Enabled = true;
                }
                else
                {
                    checkBox_Timer1.Checked = false;
                    dateTimePicker_Sch1.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule1", "OnTimeStart", "0");
                checkBox_Timer1.Checked = false;
                dateTimePicker_Sch1.Enabled = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "")) == 1)
                {
                    dateTimePicker_Sch2.Text = ini12.INIRead(Global.MainSettingPath, "Schedule2", "Timer", "");       //Schedule2 Timer
                    checkBox_Timer2.Checked = true;
                    dateTimePicker_Sch2.Enabled = true;
                }
                else
                {
                    checkBox_Timer2.Checked = false;
                    dateTimePicker_Sch2.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule2", "OnTimeStart", "0");
                checkBox_Timer2.Checked = false;
                dateTimePicker_Sch2.Enabled = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Schedule3", "OnTimeStart", "") != "")
            {

                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule3", "OnTimeStart", "")) == 1)
                {
                    dateTimePicker_Sch3.Text = ini12.INIRead(Global.MainSettingPath, "Schedule3", "Timer", "");       //Schedule3 Timer
                    checkBox_Timer3.Checked = true;
                    dateTimePicker_Sch3.Enabled = true;
                }
                else
                {
                    checkBox_Timer3.Checked = false;
                    dateTimePicker_Sch3.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule3", "OnTimeStart", "0");
                checkBox_Timer3.Checked = false;
                dateTimePicker_Sch3.Enabled = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Schedule4", "OnTimeStart", "") != "")
            {

                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule4", "OnTimeStart", "")) == 1)
                {
                    dateTimePicker_Sch4.Text = ini12.INIRead(Global.MainSettingPath, "Schedule4", "Timer", "");       //Schedule4 Timer
                    checkBox_Timer4.Checked = true;
                    dateTimePicker_Sch4.Enabled = true;
                }
                else
                {
                    checkBox_Timer4.Checked = false;
                    dateTimePicker_Sch4.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule4", "OnTimeStart", "0");
                checkBox_Timer4.Checked = false;
                dateTimePicker_Sch4.Enabled = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Schedule5", "OnTimeStart", "") != "")
            {

                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule5", "OnTimeStart", "")) == 1)
                {
                    dateTimePicker_Sch5.Text = ini12.INIRead(Global.MainSettingPath, "Schedule5", "Timer", "");       //Schedule5 Timer
                    checkBox_Timer5.Checked = true;
                    dateTimePicker_Sch5.Enabled = true;
                }
                else
                {
                    checkBox_Timer5.Checked = false;
                    dateTimePicker_Sch5.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule5", "OnTimeStart", "0");
                checkBox_Timer5.Checked = false;
                dateTimePicker_Sch5.Enabled = false;
            }
            #endregion
        }

        #region LoadSchBtn
        private void LoadSchBtn1_Click(object sender, EventArgs e)      // Load Schedule1 Path
        {
            SchOpen1.Filter = "CSV files (*.csv)|*.CSV";
            SchOpen1.ShowDialog();
            if (SchOpen1.FileName == "SchOpen1")
                textBox_Schedule1.Text = textBox_Schedule1.Text;
            else
                textBox_Schedule1.Text = SchOpen1.FileName;
        }
        private void LoadSchBtn2_Click(object sender, EventArgs e)      // Load Schedule2 Path
        {
            SchOpen2.Filter = "CSV files (*.csv)|*.CSV";
            SchOpen2.ShowDialog();
            if (SchOpen2.FileName == "SchOpen2")
                textBox_Schedule2.Text = textBox_Schedule2.Text;
            else
                textBox_Schedule2.Text = SchOpen2.FileName;
        }
        private void LoadSchBtn3_Click(object sender, EventArgs e)      // Load Schedule3 Path
        {
            SchOpen3.Filter = "CSV files (*.csv)|*.CSV";
            SchOpen3.ShowDialog();
            if (SchOpen3.FileName == "SchOpen3")
                textBox_Schedule3.Text = textBox_Schedule3.Text;
            else
                textBox_Schedule3.Text = SchOpen3.FileName;
        }
        private void LoadSchBtn4_Click(object sender, EventArgs e)      // Load Schedule4 Path
        {
            SchOpen4.Filter = "CSV files (*.csv)|*.CSV";
            SchOpen4.ShowDialog();
            if (SchOpen4.FileName == "SchOpen4")
                textBox_Schedule4.Text = textBox_Schedule4.Text;
            else
                textBox_Schedule4.Text = SchOpen4.FileName;
        }
        private void LoadSchBtn5_Click(object sender, EventArgs e)      // Load Schedule5 Path
        {
            SchOpen5.Filter = "CSV files (*.csv)|*.CSV";
            SchOpen5.ShowDialog();
            if (SchOpen5.FileName == "SchOpen5")
                textBox_Schedule5.Text = textBox_Schedule5.Text;
            else
                textBox_Schedule5.Text = SchOpen5.FileName;
        }
        #endregion
        
        public void SaveSchBtn_Click(object sender, EventArgs e)
        {
            if (checkBox_Schedule3.Checked == true)
            {
                if (System.IO.File.Exists(textBox_Schedule3.Text.Trim()) == true)
                {
                    ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Path", textBox_Schedule3.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase3", Path.GetFileNameWithoutExtension(textBox_Schedule3.Text.Trim()));
                    ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Exist", "1");
                    Global.Schedule_3_Exist = 1;
                }
                else
                {
                    label_ErrorMessage.Text = "Schedule3 csv file not exist !";
                }

                if (string.IsNullOrEmpty(textBox_Schedule3Loop.Text) || textBox_Schedule3Loop.Text == "0")
                {
                    label_ErrorMessage.Text = "Schedule3 Loop error !";
                }
                else
                {
                    ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Loop", textBox_Schedule3Loop.Text.Trim());
                }
            }

            if (checkBox_Schedule4.Checked == true)
            {
                if (System.IO.File.Exists(textBox_Schedule4.Text.Trim()) == true)
                {
                    ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Path", textBox_Schedule4.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase4", Path.GetFileNameWithoutExtension(textBox_Schedule4.Text.Trim()));
                    ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Exist", "1");
                    Global.Schedule_4_Exist = 1;
                }
                else
                {
                    label_ErrorMessage.Text = "Schedule4 csv file not exist !";
                }

                if (string.IsNullOrEmpty(textBox_Schedule4Loop.Text) || textBox_Schedule4Loop.Text == "0")
                {
                    label_ErrorMessage.Text = "Schedule4 Loop error !";
                }
                else
                {
                    ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Loop", textBox_Schedule4Loop.Text.Trim());
                }
            }

            if (checkBox_Schedule5.Checked == true)
            {
                if (System.IO.File.Exists(textBox_Schedule5.Text.Trim()) == true)
                {
                    ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Path", textBox_Schedule5.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase5", Path.GetFileNameWithoutExtension(textBox_Schedule5.Text.Trim()));
                    ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Exist", "1");
                    Global.Schedule_5_Exist = 1;
                }
                else
                {
                    label_ErrorMessage.Text = "Schedule5 csv file not exist !";
                }

                if (string.IsNullOrEmpty(textBox_Schedule5Loop.Text) || textBox_Schedule5Loop.Text == "0")
                {
                    label_ErrorMessage.Text = "Schedule5 Loop error !";
                }
                else
                {
                    ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Loop", textBox_Schedule5Loop.Text.Trim());
                }
            }

            if (Global.FormSchedule == false)
            {
            }
            else
            {
                for (int i = 1; i < 6; i++)
                    Global.Total_Loop += int.Parse(ini12.INIRead(Global.MainSettingPath, "Schedule" + i, "Loop", ""));
            }

            ini12.INIWrite(Global.MainSettingPath, "Schedule1", "Timer", dateTimePicker_Sch1.Text.Trim() + ":00");
            ini12.INIWrite(Global.MainSettingPath, "Schedule2", "Timer", dateTimePicker_Sch2.Text.Trim() + ":00");
            ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Timer", dateTimePicker_Sch3.Text.Trim() + ":00");
            ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Timer", dateTimePicker_Sch4.Text.Trim() + ":00");
            ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Timer", dateTimePicker_Sch5.Text.Trim() + ":00");

            DateTime dt1 = Convert.ToDateTime(dateTimePicker_Sch1.Text);
            DateTime dt2 = Convert.ToDateTime(dateTimePicker_Sch2.Text);
            DateTime dt3 = Convert.ToDateTime(dateTimePicker_Sch3.Text);
            DateTime dt4 = Convert.ToDateTime(dateTimePicker_Sch4.Text);
            DateTime dt5 = Convert.ToDateTime(dateTimePicker_Sch5.Text);

            #region Schedule2偵錯
            if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "") == "1")
            {
                if (ini12.INIRead(Global.MainSettingPath, "Schedule1", "OnTimeStart", "") == "1" && DateTime.Compare(dt1, dt2) > 0)
                {
                    label_ErrorMessage.Text = "Schedule2 Timer Error !";
                }
            }
            #endregion

            #region Schedule3偵錯
            if (ini12.INIRead(Global.MainSettingPath, "Schedule3", "OnTimeStart", "") == "1")
            {
                if (ini12.INIRead(Global.MainSettingPath, "Schedule1", "OnTimeStart", "") == "1" && DateTime.Compare(dt1, dt3) > 0)
                {
                    label_ErrorMessage.Text = "Schedule3 Timer Error !";
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "") == "1" && DateTime.Compare(dt2, dt3) > 0)
                {
                    label_ErrorMessage.Text = "Schedule3 Timer Error !";
                }
            }
            #endregion

            #region Schedule4偵錯
            if (ini12.INIRead(Global.MainSettingPath, "Schedule4", "OnTimeStart", "") == "1")
            {
                if (ini12.INIRead(Global.MainSettingPath, "Schedule1", "OnTimeStart", "") == "1" && DateTime.Compare(dt1, dt4) > 0)
                {
                    label_ErrorMessage.Text = "Schedule4 Timer Error !";
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "") == "1" && DateTime.Compare(dt2, dt4) > 0)
                {
                    label_ErrorMessage.Text = "Schedule4 Timer Error !";
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Schedule3", "OnTimeStart", "") == "1" && DateTime.Compare(dt3, dt4) > 0)
                {
                    label_ErrorMessage.Text = "Schedule4 Timer Error !";
                }
            }
            #endregion

            #region Schedule5偵錯
            if (ini12.INIRead(Global.MainSettingPath, "Schedule5", "OnTimeStart", "") == "1")
            {
                if (ini12.INIRead(Global.MainSettingPath, "Schedule1", "OnTimeStart", "") == "1" && DateTime.Compare(dt1, dt5) > 0)
                {
                    label_ErrorMessage.Text = "Schedule5 Timer Error !";
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "") == "1" && DateTime.Compare(dt2, dt5) > 0)
                {
                    label_ErrorMessage.Text = "Schedule5 Timer Error !";
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Schedule3", "OnTimeStart", "") == "1" && DateTime.Compare(dt3, dt5) > 0)
                {
                    label_ErrorMessage.Text = "Schedule5 Timer Error !";
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Schedule4", "OnTimeStart", "") == "1" && DateTime.Compare(dt4, dt5) > 0)
                {
                    label_ErrorMessage.Text = "Schedule5 Timer Error !";
                }
            }
            #endregion
        }

        

        #region checkBoxTimer
        private void checkBoxTimer1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Timer1.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule1", "OnTimeStart", "1");
                dateTimePicker_Sch1.Enabled = true;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule1", "OnTimeStart", "0");
                dateTimePicker_Sch1.Enabled = false;
            }
        }
        private void checkBoxTimer2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Timer2.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule2", "OnTimeStart", "1");
                dateTimePicker_Sch2.Enabled = true;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule2", "OnTimeStart", "0");
                dateTimePicker_Sch2.Enabled = false;
            }
        }
        private void checkBoxTimer3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Timer3.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule3", "OnTimeStart", "1");
                dateTimePicker_Sch3.Enabled = true;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule3", "OnTimeStart", "0");
                dateTimePicker_Sch3.Enabled = false;
            }
        }
        private void checkBoxTimer4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Timer4.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule4", "OnTimeStart", "1");
                dateTimePicker_Sch4.Enabled = true;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule4", "OnTimeStart", "0");
                dateTimePicker_Sch4.Enabled = false;
            }
        }
        private void checkBoxTimer5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Timer5.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule5", "OnTimeStart", "1");
                dateTimePicker_Sch5.Enabled = true;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule5", "OnTimeStart", "0");
                dateTimePicker_Sch5.Enabled = false;
            }
        }
        #endregion

        

        

        private void textBox_Schedule1_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox_Schedule1.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule1", "Path", textBox_Schedule1.Text.Trim());
                ini12.INIWrite(Global.MainSettingPath, "Schedule1", "Exist", "1");
                ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase1", Path.GetFileNameWithoutExtension(textBox_Schedule1.Text.Trim()));

                label_ErrorMessage.Text = "";
                pictureBox_Schedule1.Image = null;
            }
            else
            {
                label_ErrorMessage.Text = "Schedule1 .csv file not exist";
                pictureBox_Schedule1.Image = Properties.Resources.ERROR;
            }
        }

        private void textBox_Schedule1Loop_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Schedule1Loop.Text) || textBox_Schedule1Loop.Text == "0")
            {
                label_ErrorMessage.Text = "Schedule1 loop is empty";
                pictureBox_Schedule1Loop.Image = Properties.Resources.ERROR;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule1", "Loop", textBox_Schedule1Loop.Text.Trim());

                label_ErrorMessage.Text = "";
                pictureBox_Schedule1Loop.Image = null;
            }
        }

        private void SchCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Schedule2.Checked == false)
            {
                button_Schedule2.Enabled = false;
                textBox_Schedule2.Enabled = false;
                textBox_Schedule2Loop.Enabled = false;

                ini12.INIWrite(Global.MainSettingPath, "Schedule2", "Exist", "0");
                Global.Schedule_2_Exist = 0;

                ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase2", "");

                checkBox_Timer2.Checked = false;
                checkBox_Timer2.Enabled = false;

                label_ErrorMessage.Text = "";
                pictureBox_Schedule2.Image = null;
                pictureBox_Schedule2Loop.Image = null;
            }
            else
            {
                textBox_Schedule2_TextChanged(new TextBox(), new EventArgs());
                textBox_Schedule2Loop_TextChanged(new TextBox(), new EventArgs());

                button_Schedule2.Enabled = true;
                textBox_Schedule2.Enabled = true;
                textBox_Schedule2Loop.Enabled = true;
                checkBox_Timer2.Enabled = true;
            }
        }

        private void textBox_Schedule2_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox_Schedule2.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule2", "Path", textBox_Schedule2.Text.Trim());
                ini12.INIWrite(Global.MainSettingPath, "Schedule2", "Exist", "1");
                Global.Schedule_2_Exist = 1;
                ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase2", Path.GetFileNameWithoutExtension(textBox_Schedule2.Text.Trim()));

                label_ErrorMessage.Text = "";
                pictureBox_Schedule2.Image = null;
            }
            else
            {
                label_ErrorMessage.Text = "Schedule2 .csv file not exist";
                pictureBox_Schedule2.Image = Properties.Resources.ERROR;
            }
        }

        private void textBox_Schedule2Loop_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Schedule2Loop.Text) || textBox_Schedule2Loop.Text == "0")
            {
                label_ErrorMessage.Text = "Schedule2 loop is empty";
                pictureBox_Schedule2Loop.Image = Properties.Resources.ERROR;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule2", "Loop", textBox_Schedule2Loop.Text.Trim());
                pictureBox_Schedule2Loop.Image = null;
            }
        }

        private void SchCheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Schedule3.Checked == false)
            {
                button_Schedule3.Enabled = false;
                textBox_Schedule3.Enabled = false;
                textBox_Schedule3Loop.Enabled = false;

                ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Exist", "0");
                Global.Schedule_3_Exist = 0;

                ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase3", "");

                checkBox_Timer3.Checked = false;
                checkBox_Timer3.Enabled = false;

                label_ErrorMessage.Text = "";
                pictureBox_Schedule3.Image = null;
                pictureBox_Schedule3Loop.Image = null;
            }
            else
            {
                textBox_Schedule3_TextChanged(new TextBox(), new EventArgs());
                textBox_Schedule3Loop_TextChanged(new TextBox(), new EventArgs());

                button_Schedule3.Enabled = true;
                textBox_Schedule3.Enabled = true;
                textBox_Schedule3Loop.Enabled = true;
                checkBox_Timer3.Enabled = true;
            }
        }

        private void textBox_Schedule3_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox_Schedule3.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Path", textBox_Schedule3.Text.Trim());
                ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Exist", "1");
                Global.Schedule_3_Exist = 1;
                ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase3", Path.GetFileNameWithoutExtension(textBox_Schedule3.Text.Trim()));

                label_ErrorMessage.Text = "";
                pictureBox_Schedule3.Image = null;
            }
            else
            {
                label_ErrorMessage.Text = "Schedule3 .csv file not exist";
                pictureBox_Schedule3.Image = Properties.Resources.ERROR;
            }
        }

        private void textBox_Schedule3Loop_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Schedule3Loop.Text) || textBox_Schedule3Loop.Text == "0")
            {
                label_ErrorMessage.Text = "Schedule3 loop is empty";
                pictureBox_Schedule3Loop.Image = Properties.Resources.ERROR;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule3", "Loop", textBox_Schedule3Loop.Text.Trim());
                pictureBox_Schedule3Loop.Image = null;
            }
        }

        private void SchCheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Schedule4.Checked == false)
            {
                button_Schedule4.Enabled = false;
                textBox_Schedule4.Enabled = false;
                textBox_Schedule4Loop.Enabled = false;

                ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Exist", "0");
                Global.Schedule_4_Exist = 0;

                ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase4", "");

                checkBox_Timer4.Checked = false;
                checkBox_Timer4.Enabled = false;

                label_ErrorMessage.Text = "";
                pictureBox_Schedule4.Image = null;
                pictureBox_Schedule4Loop.Image = null;
            }
            else
            {
                textBox_Schedule4_TextChanged(new TextBox(), new EventArgs());
                textBox_Schedule4Loop_TextChanged(new TextBox(), new EventArgs());

                button_Schedule4.Enabled = true;
                textBox_Schedule4.Enabled = true;
                textBox_Schedule4Loop.Enabled = true;
                checkBox_Timer4.Enabled = true;
            }
        }

        private void textBox_Schedule4_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox_Schedule4.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Path", textBox_Schedule4.Text.Trim());
                ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Exist", "1");
                Global.Schedule_4_Exist = 1;
                ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase4", Path.GetFileNameWithoutExtension(textBox_Schedule4.Text.Trim()));

                label_ErrorMessage.Text = "";
                pictureBox_Schedule4.Image = null;
            }
            else
            {
                label_ErrorMessage.Text = "Schedule4 .csv file not exist";
                pictureBox_Schedule4.Image = Properties.Resources.ERROR;
            }
        }

        private void textBox_Schedule4Loop_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Schedule4Loop.Text) || textBox_Schedule4Loop.Text == "0")
            {
                label_ErrorMessage.Text = "Schedule4 loop is empty";
                pictureBox_Schedule4Loop.Image = Properties.Resources.ERROR;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule4", "Loop", textBox_Schedule4Loop.Text.Trim());
                pictureBox_Schedule4Loop.Image = null;
            }
        }

        private void SchCheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Schedule5.Checked == false)
            {
                button_Schedule5.Enabled = false;
                textBox_Schedule5.Enabled = false;
                textBox_Schedule5Loop.Enabled = false;

                ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Exist", "0");
                Global.Schedule_5_Exist = 0;

                ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase5", "");

                checkBox_Timer5.Checked = false;
                checkBox_Timer5.Enabled = false;

                label_ErrorMessage.Text = "";
                pictureBox_Schedule5.Image = null;
                pictureBox_Schedule5Loop.Image = null;
            }
            else
            {
                textBox_Schedule5_TextChanged(new TextBox(), new EventArgs());
                textBox_Schedule5Loop_TextChanged(new TextBox(), new EventArgs());

                button_Schedule5.Enabled = true;
                textBox_Schedule5.Enabled = true;
                textBox_Schedule5Loop.Enabled = true;
                checkBox_Timer5.Enabled = true;
            }
        }

        private void textBox_Schedule5_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox_Schedule5.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Path", textBox_Schedule5.Text.Trim());
                ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Exist", "1");
                Global.Schedule_5_Exist = 1;
                ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase5", Path.GetFileNameWithoutExtension(textBox_Schedule5.Text.Trim()));

                label_ErrorMessage.Text = "";
                pictureBox_Schedule5.Image = null;
            }
            else
            {
                label_ErrorMessage.Text = "Schedule5 .csv file not exist";
                pictureBox_Schedule5.Image = Properties.Resources.ERROR;
            }
        }

        private void textBox_Schedule5Loop_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_Schedule5Loop.Text) || textBox_Schedule5Loop.Text == "0")
            {
                label_ErrorMessage.Text = "Schedule5 loop is empty";
                pictureBox_Schedule5Loop.Image = Properties.Resources.ERROR;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Schedule5", "Loop", textBox_Schedule5Loop.Text.Trim());
                pictureBox_Schedule5Loop.Image = null;
            }
        }

        private void CompareBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Similarity.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "CompareChoose", "1");

                comboBox_Similarity.Enabled = true;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "CompareChoose", "0");

                comboBox_Similarity.Enabled = false;
            }
        }

        private void DifferenceBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MainSettingPath, "Record", "CompareDifferent", (100 - int.Parse(comboBox_Similarity.Text.Replace("%", ""))).ToString());
        }

        private void checkBoxFootprintMode_CheckedChanged(object sender, EventArgs e)
        {
            //足跡模式//
            if (checkBox_FootprintMode.Checked == true)
            {
                
                ini12.INIWrite(Global.MainSettingPath, "Record", "Footprint Mode", "1");
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "Footprint Mode", "0");
            }
        }

        private void RecVideo_CheckedChanged(object sender, EventArgs e)
        {
            //測試完成開始錄影//
            if (checkBox_VideoRecord.Checked == true)
            {
                
                ini12.INIWrite(Global.MainSettingPath, "Record", "EachVideo", "1");
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "EachVideo", "0");
            }
        }

        private void checkBox_ScheduleAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            //程式啟動自動跑shchedule//
            if (checkBox_ScheduleAutoStart.Checked == true)
            {
                
                ini12.INIWrite(Global.MainSettingPath, "Device", "RunAfterStartUp", "1");
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Device", "RunAfterStartUp", "0");
            }
        }
    }
}
