using BlueRatLibrary;
using jini;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AutoTest
{
    public partial class FormRC : Form
    {
        public FormRC()
        {
            InitializeComponent();
        }
        
        private string RcConfigFolder = Application.StartupPath + "\\RcConfig\\";
        private RedRatDBParser RedRatData = new RedRatDBParser();
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        private void Sand_Key(string RcKey)
        {
            Form1 lForm1 = (Form1)this.Owner;

            if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
            {
                if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1")
                {
                    lForm1.Autocommand_RedRat("FormRc", RcKey);
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxVerson", "") == "2")
                {
                    lForm1.Autocommand_BlueRat("FormRc", RcKey);
                }
            }
            else if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1")
            {
                lForm1.Autocommand_RedRat("FormRc", RcKey);
            }

            if (StartRecord == true)
            {
                lForm1.button_Start.Enabled = false;
                if (RcKey == "up" || RcKey == "down" || RcKey == "left" || RcKey == "right")
                {
                    string keyname = "";
                    for (int i = 0; i < Global.Rc_List.Count; i++)
                    {
                        if (Global.Rc_List[i].ToUpper() == RcKey.ToUpper())
                        {
                            keyname = Global.Rc_List[i];
                        }
                    }
                    lForm1.DataGridView_Schedule.Rows.Add(keyname);
                    lForm1.DataGridView_Schedule.FirstDisplayedScrollingRowIndex = lForm1.DataGridView_Schedule.Rows.Count - 1;
                }
                else
                {
                    lForm1.DataGridView_Schedule.Rows.Add(RcKey);
                    lForm1.DataGridView_Schedule.FirstDisplayedScrollingRowIndex = lForm1.DataGridView_Schedule.Rows.Count - 1;
                }

                if (sw.IsRunning == true)
                {
                    lForm1.DataGridView_Schedule.Rows[lForm1.DataGridView_Schedule.RowCount - 3].Cells[9].Value = sw.ElapsedMilliseconds.ToString();
                    sw.Restart();
                }
                else
                {
                    sw.Start();
                }
            }
        }

        private void LoadRcDB()
        {
            string RcDbPath = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "DBFile", "");
            Console.WriteLine(RcConfigFolder + comboBoxRcNumber.SelectedItem);
            // Redrat Database
            if (System.IO.File.Exists(RcDbPath))
            {
                XDocument myDoc = XDocument.Load(RcDbPath);
                var AVDevices = from pn in myDoc.Descendants("AVDevice")
                                where pn.Element("Name") != null
                                select pn.Element("Name").Value;

                foreach (var c in AVDevices)
                {
                    ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Brands", c);
                    Text = c;
                }

                foreach (Control cl in Controls)
                {
                    if (cl is ComboBox)
                    {
                        ComboBox cob = cl as ComboBox;
                        cob.Items.Clear();
                    }
                }

                RedRatData.RedRatLoadSignalDB(ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "DBFile", ""));
                RedRatData.RedRatSelectDevice(ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Brands", ""));
                string devicename = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Brands", "");
                if (RedRatData.RedRatSelectDevice(devicename))
                {
                    foreach (Control item in Controls)
                    {
                        if (item is ComboBox)
                        {
                            ComboBox comboBox = item as ComboBox;
                            comboBox.Items.Add("");
                            comboBox.Items.AddRange(RedRatData.RedRatGetRCNameList().ToArray());
                        }
                    }
                    Global.Rc_List = RedRatData.RedRatGetRCNameList();
                    Global.Rc_Number = RedRatData.RedRatGetRCNameList().Count;
                }
                else
                {
                    Console.WriteLine("Select Device Error: " + devicename);
                }

                //ComboBox AutoSize//
                int maxSize = 0;
                System.Drawing.Graphics g = CreateGraphics();

                for (int i = 0; i < comboBox17.Items.Count; i++)
                {
                    comboBox17.SelectedIndex = i;
                    SizeF size = g.MeasureString(comboBox17.Text, comboBox17.Font);
                    if (maxSize < (int)size.Width)
                    {
                        maxSize = (int)size.Width + 50;
                    }
                }

                comboBox17.DropDownWidth = comboBox17.Width;

                if (comboBox17.DropDownWidth < maxSize)
                {
                    foreach (Control item in Controls)
                    {
                        if (item is ComboBox)
                        {
                            ComboBox comboBox = item as ComboBox;
                            comboBox.DropDownWidth = maxSize + 50;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("RC DB is not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRcKeys()
        {
            comboBox1.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button1", "");
            comboBox2.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button2", "");
            comboBox3.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button3", "");
            comboBox4.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button4", "");
            comboBox5.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button5", "");
            comboBox6.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button6", "");
            comboBox7.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button7", "");
            comboBox8.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button8", "");
            comboBox9.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button9", "");
            comboBox10.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button10", "");
            comboBox11.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button11", "");
            comboBox12.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button12", "");
            comboBox13.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button13", "");
            comboBox14.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button14", "");
            comboBox15.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button15", "");
            comboBox16.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button16", "");
            comboBox17.Text = ini12.INIRead(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button17", "");
        }

        private void button_Edit_Click(object sender, EventArgs e)
        {
            if (button_Edit.Text == "EDIT")
            {
                comboBox1.Visible = true;
                comboBox2.Visible = true;
                comboBox3.Visible = true;
                comboBox4.Visible = true;
                comboBox5.Visible = true;
                comboBox6.Visible = true;
                comboBox7.Visible = true;
                comboBox8.Visible = true;
                comboBox9.Visible = true;
                comboBox10.Visible = true;
                comboBox11.Visible = true;
                comboBox12.Visible = true;
                comboBox13.Visible = true;
                comboBox14.Visible = true;
                comboBox15.Visible = true;
                comboBox16.Visible = true;
                comboBox17.Visible = true;

                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                button9.Enabled = false;
                button10.Enabled = false;
                button11.Enabled = false;
                button12.Enabled = false;
                button13.Enabled = false;
                button14.Enabled = false;
                button15.Enabled = false;
                button16.Enabled = false;
                button17.Enabled = false;

                button_Edit.Text = "SAVE";
            }
            else if (button_Edit.Text == "SAVE")
            {
                comboBox1.Visible = false;
                comboBox2.Visible = false;
                comboBox3.Visible = false;
                comboBox4.Visible = false;
                comboBox5.Visible = false;
                comboBox6.Visible = false;
                comboBox7.Visible = false;
                comboBox8.Visible = false;
                comboBox9.Visible = false;
                comboBox10.Visible = false;
                comboBox11.Visible = false;
                comboBox12.Visible = false;
                comboBox13.Visible = false;
                comboBox14.Visible = false;
                comboBox15.Visible = false;
                comboBox16.Visible = false;
                comboBox17.Visible = false;

                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
                button8.Enabled = true;
                button9.Enabled = true;
                button10.Enabled = true;
                button11.Enabled = true;
                button12.Enabled = true;
                button13.Enabled = true;
                button14.Enabled = true;
                button15.Enabled = true;
                button16.Enabled = true;
                button17.Enabled = true;

                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button1", button1.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button2", button2.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button3", button3.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button4", button4.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button5", button5.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button6", button6.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button7", button7.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button8", button8.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button9", button9.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button10", button10.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button11", button11.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button12", button12.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button13", button13.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button14", button14.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button15", button15.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button16", button16.Text);
                ini12.INIWrite(RcConfigFolder + comboBoxRcNumber.SelectedItem, "Info", "Button17", button17.Text);

                button_Edit.Text = "EDIT";
            }
        }

        private void FormRC_Load(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(RcConfigFolder);
            foreach (var item in dirInfo.GetFiles("*.ini"))
            {
                comboBoxRcNumber.Items.Add(item.Name);
            }

            comboBoxRcNumber.SelectedIndex =
                comboBoxRcNumber.Items.IndexOf(ini12.INIRead(Global.RcSettingPath, "Setting", "SelectRcLastTime", ""));
        }

        #region -- comboBox_SelectedIndexChanged(object sender, EventArgs e) --
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Text = comboBox1.Text;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button2.Text = comboBox2.Text;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Text = comboBox3.Text;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4.Text = comboBox4.Text;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            button5.Text = comboBox5.Text;
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            button6.Text = comboBox6.Text;
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            button7.Text = comboBox7.Text;
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            button8.Text = comboBox8.Text;
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            button9.Text = comboBox9.Text;
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            button10.Text = comboBox10.Text;
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            button11.Text = comboBox11.Text;
        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            button12.Text = comboBox12.Text;
        }

        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            button13.Text = comboBox13.Text;
        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            button14.Text = comboBox14.Text;
        }

        private void comboBox15_SelectedIndexChanged(object sender, EventArgs e)
        {
            button15.Text = comboBox15.Text;
        }
        private void comboBox16_SelectedIndexChanged(object sender, EventArgs e)
        {
            button16.Text = comboBox16.Text;

        }

        private void comboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            button17.Text = comboBox17.Text;
        }
        #endregion

        #region -- button_Click(object sender, EventArgs e) --
        private void button1_Click(object sender, EventArgs e)
        {
            Sand_Key(button1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sand_Key(button2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sand_Key(button3.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sand_Key(button4.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sand_Key(button5.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Sand_Key(button6.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Sand_Key(button7.Text);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Sand_Key(button8.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Sand_Key(button9.Text);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Sand_Key(button10.Text);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Sand_Key(button11.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Sand_Key(button12.Text);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Sand_Key(button13.Text);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Sand_Key(button14.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Sand_Key(button15.Text);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Sand_Key(button16.Text);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Sand_Key(button17.Text);
        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
            Sand_Key("up");
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            Sand_Key("down");
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            Sand_Key("left");
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            Sand_Key("right");
        }

        private void ButtonDigital1_Click(object sender, EventArgs e)
        {
            Sand_Key("1");
        }

        private void ButtonDigital2_Click(object sender, EventArgs e)
        {
            Sand_Key("2");
        }

        private void ButtonDigital3_Click(object sender, EventArgs e)
        {
            Sand_Key("3");
        }

        private void ButtonDigital4_Click(object sender, EventArgs e)
        {
            Sand_Key("4");
        }

        private void ButtonDigital5_Click(object sender, EventArgs e)
        {
            Sand_Key("5");
        }

        private void ButtonDigital6_Click(object sender, EventArgs e)
        {
            Sand_Key("6");
        }

        private void ButtonDigital7_Click(object sender, EventArgs e)
        {
            Sand_Key("7");
        }

        private void ButtonDigital8_Click(object sender, EventArgs e)
        {
            Sand_Key("8");
        }

        private void ButtonDigital9_Click(object sender, EventArgs e)
        {
            Sand_Key("9");
        }

        private void ButtonDigital0_Click(object sender, EventArgs e)
        {
            Sand_Key("0");
        }

        private void ButtonDigitalDot_Click(object sender, EventArgs e)
        {
            Sand_Key(".");
        }
        #endregion

        private void FormRC_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.FormRC = false;
        }

        private void FormRC_Shown(object sender, EventArgs e)
        {
            Global.FormRC = true;
        }

        private void comboBoxRcNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRcDB();
            LoadRcKeys();
            ini12.INIWrite(Global.RcSettingPath, "Setting", "SelectRcLastTime", comboBoxRcNumber.SelectedItem.ToString());
            ini12.INIWrite(Global.RcSettingPath, "Setting", "SelectRcLastTimePath", RcConfigFolder + comboBoxRcNumber.SelectedItem.ToString());
        }

        private void AddRcDb(string DBFile)
        {
            string path = RcConfigFolder + Path.GetFileNameWithoutExtension(DBFile) + ".ini";
            ini12.INIWrite(path, "Info", "DBFile", DBFile);
            ini12.INIWrite(path, "Info", "Brands", "");
            for (int i = 1; i < 18; i++)
            {
                if (i == 17)
                {
                    ini12.INIWrite(path, "Info", "Button" + i.ToString(), "");
                }
                else
                {
                    ini12.INIWrite(path, "Info", "Button" + i.ToString(), "");
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XML files (*.xml)|*.xml";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                AddRcDb(openFileDialog1.FileName);
                
                //清除並重新撈檔案//
                comboBoxRcNumber.Items.Clear();
                DirectoryInfo dirInfo = new DirectoryInfo(RcConfigFolder);
                foreach (var item in dirInfo.GetFiles("*.ini"))
                {
                    comboBoxRcNumber.Items.Add(item.Name);
                }

                comboBoxRcNumber.SelectedIndex = 
                    comboBoxRcNumber.Items.IndexOf(openFileDialog1.SafeFileName.Substring(0, openFileDialog1.SafeFileName.Length - 4) + ".ini");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (comboBoxRcNumber.Items.Count == 1)
            {
                MessageBox.Show("Press + to add new RC.ini", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                //刪除指定檔案//
                string RcConfigPath = RcConfigFolder + comboBoxRcNumber.SelectedItem;
                File.Delete(RcConfigPath);

                //清除並重新撈檔案//
                comboBoxRcNumber.Items.Clear();
                DirectoryInfo dirInfo = new DirectoryInfo(RcConfigFolder);
                foreach (var item in dirInfo.GetFiles("*.ini"))
                {
                    comboBoxRcNumber.Items.Add(item.Name);
                }
                comboBoxRcNumber.SelectedIndex = 0;
            }
        }

        bool StartRecord = false;
        private void pictureBox_Record_Click(object sender, EventArgs e)
        {
            if (StartRecord == true)
            {
                sw.Stop();
                pictureBox_Record.Image = Properties.Resources.record_off;
                StartRecord = false;
                MessageBox.Show("Save the script before press START button.", "Hint", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (StartRecord == false)
            {
                pictureBox_Record.Image = Properties.Resources.record_on;
                StartRecord = true;
            }
        }
    }
}
