using DirectX.Capture;
using jini;
using RedRat.RedRat3;
using RedRat.RedRat3.USB;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AutoTest
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void loadxml()
        {
            // Redrat Database
            if (System.IO.File.Exists(textBox_RcDbPath.Text))
            {
                XDocument myDoc = XDocument.Load(textBox_RcDbPath.Text);
                var AVDevices = from pn in myDoc.Descendants("AVDevice")
                                where pn.Element("Name") != null
                                select pn.Element("Name").Value;

                foreach (var c in AVDevices)
                {
                    comboBox_TvBrands.Items.Add(c);
                    if (comboBox_TvBrands.Text == "")
                        comboBox_TvBrands.Text = c;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // RedRat3 Command Path
            openFileDialog2.Filter = "XML files (*.xml)|*.xml";
            openFileDialog2.ShowDialog();
            if (openFileDialog2.FileName == "")
            {
                textBox_RcDbPath.Text = textBox_RcDbPath.Text;
            }
            else
            {
                textBox_RcDbPath.Text = openFileDialog2.FileName;
                comboBox_TvBrands.Items.Clear();
                loadxml();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Generator Command Path
            openFileDialog1.Filter = "XML files (*.xml)|*.xml";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == "")
            {
                textBox_GeneratorPath.Text = textBox_GeneratorPath.Text;
            }
            else
            {
                textBox_GeneratorPath.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Save Video Path
            folderBrowserDialog1.ShowDialog();
            if (folderBrowserDialog1.SelectedPath == "")
            {
                textBox_ImagePath.Text = textBox_ImagePath.Text;
            }
            else
            {
                textBox_ImagePath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //log file save path
            folderBrowserDialog2.ShowDialog();
            if (folderBrowserDialog2.SelectedPath == "")
            {
                textBox_LogPath.Text = textBox_LogPath.Text;
            }
            else
            {
                textBox_LogPath.Text = folderBrowserDialog2.SelectedPath;
            }
        }

        private void buttonDosWorkingDirectory_Click(object sender, EventArgs e)
        {
            folderBrowserDialog3.ShowDialog();
            if (folderBrowserDialog3.SelectedPath == "")
            {
                textBox_DosPath.Text = textBox_DosPath.Text;
            }
            else
            {
                textBox_DosPath.Text = folderBrowserDialog3.SelectedPath;
            }
        }

        public void OkBtn_Click(object sender, EventArgs e)
        {
            //Image Path//
            ini12.INIWrite(Global.MainSettingPath, "Record", "VideoPath", textBox_ImagePath.Text.Trim());

            //Log Path//
            ini12.INIWrite(Global.MainSettingPath, "Record", "LogPath", textBox_LogPath.Text.Trim());

            //RedRat Path//
            ini12.INIWrite(Global.MainSettingPath, "RedRat", "DBFile", textBox_RcDbPath.Text.Trim());

            //Generator Path//
            ini12.INIWrite(Global.MainSettingPath, "Record", "Generator", textBox_GeneratorPath.Text.Trim());

            //DOS Path//
            ini12.INIWrite(Global.MainSettingPath, "Device", "DOS", textBox_DosPath.Text.Trim());

            //Camera Device, Audio//
            ini12.INIWrite(Global.MainSettingPath, "Camera", "VideoIndex", comboBox_CameraDevice.SelectedIndex.ToString());
            ini12.INIWrite(Global.MainSettingPath, "Camera", "VideoName", comboBox_CameraDevice.Text);
            ini12.INIWrite(Global.MainSettingPath, "Camera", "AudioIndex", comboBox_CameraAudio.SelectedIndex.ToString());
            ini12.INIWrite(Global.MainSettingPath, "Camera", "AudioName", comboBox_CameraAudio.Text);

            //RedRat Brands, Select RC//
            ini12.INIWrite(Global.MainSettingPath, "RedRat", "Brands", comboBox_TvBrands.Text.Trim());
            ini12.INIWrite(Global.MainSettingPath, "RedRat", "RedRatIndex", (comboBox__SelectRedrat.SelectedIndex).ToString());
            ini12.INIWrite(Global.MainSettingPath, "RedRat", "SerialNumber", comboBox__SelectRedrat.Text);

            //SerialPort1//
            if (checkBox_SerialPort1.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Comport", "BaudRate", comboBox_SerialPort1_BaudRate_Value.Text.Trim());
                ini12.INIWrite(Global.MainSettingPath, "Comport", "DataBit", "8");
                ini12.INIWrite(Global.MainSettingPath, "Comport", "StopBits", "One");
                ini12.INIWrite(Global.MainSettingPath, "Comport", "PortName", comboBox_SerialPort1_PortName_Value.Text);
            }

            //SerialPort2//
            if (checkBox_SerialPort2.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "ExtComport", "BaudRate", comboBox_SerialPort2_BaudRate_Value.Text.Trim());
                ini12.INIWrite(Global.MainSettingPath, "ExtComport", "DataBit", "8");
                ini12.INIWrite(Global.MainSettingPath, "ExtComport", "StopBits", "One");
                ini12.INIWrite(Global.MainSettingPath, "ExtComport", "PortName", comboBox_SerialPort2_PortName_Value.Text);
            }
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            //Image欄位//
            if (Directory.Exists(ini12.INIRead(Global.MainSettingPath, "Record", "VideoPath", "")))
            {
                textBox_ImagePath.Text = ini12.INIRead(Global.MainSettingPath, "Record", "VideoPath", "");
            }
            else
            {
                textBox_ImagePath.Text = "";
                pictureBox_ImagePath.Image = Properties.Resources.ERROR;
            }
            
            //Log欄位//
            if (Directory.Exists(ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "")))
            {
                textBox_LogPath.Text = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
            }
            else
            {
                textBox_LogPath.Text = "";
                pictureBox_LogPath.Image = Properties.Resources.ERROR;
            }

            //RC DB欄位//
            if (File.Exists(ini12.INIRead(Global.MainSettingPath, "RedRat", "DBFile", "")))
            {
                textBox_RcDbPath.Text = ini12.INIRead(Global.MainSettingPath, "RedRat", "DBFile", "");
            }
            else
            {
                textBox_RcDbPath.Text = "";
                pictureBox_RcDbPath.Image = Properties.Resources.ERROR;
            }

            //Generator欄位//
            if (File.Exists(ini12.INIRead(Global.MainSettingPath, "Record", "Generator", "")))
            {
                textBox_GeneratorPath.Text = ini12.INIRead(Global.MainSettingPath, "Record", "Generator", "");
            }
            else
            {
                textBox_GeneratorPath.Text = "";
                pictureBox_GeneratorPath.Image = Properties.Resources.ERROR;
            }

            //DOS欄位//
            if (Directory.Exists(ini12.INIRead(Global.MainSettingPath, "Device", "DOS", "")))
            {
                textBox_DosPath.Text = ini12.INIRead(Global.MainSettingPath, "Device", "DOS", "");
            }
            else
            {
                textBox_DosPath.Text = "";
                pictureBox_DosPath.Image = Properties.Resources.ERROR;
            }

            #region -- SerialPort --
            if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
            {
                GroupBox_Rs232.Text = ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxPort", "") + " IS USING";

                comboBox_SerialPort1_PortName_Value.DataSource = System.IO.Ports.SerialPort.GetPortNames();
                comboBox_SerialPort2_PortName_Value.DataSource = System.IO.Ports.SerialPort.GetPortNames();

                if (ini12.INIRead(Global.MainSettingPath, "Comport", "Checked", "") == "1")
                {
                    checkBox_SerialPort1.Checked = true;
                    comboBox_SerialPort1_BaudRate_Value.Enabled = true;
                    comboBox_SerialPort1_PortName_Value.Enabled = true;
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Comport", "Checked", "") == "0")
                {
                    checkBox_SerialPort1.Checked = false;
                    comboBox_SerialPort1_BaudRate_Value.Enabled = false;
                    comboBox_SerialPort1_PortName_Value.Enabled = false;
                }

                if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1")
                {
                    checkBox_SerialPort2.Checked = true;
                    comboBox_SerialPort2_BaudRate_Value.Enabled = true;
                    comboBox_SerialPort2_PortName_Value.Enabled = true;
                }
                else if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "0")
                {
                    checkBox_SerialPort2.Checked = false;
                    comboBox_SerialPort2_BaudRate_Value.Enabled = false;
                    comboBox_SerialPort2_PortName_Value.Enabled = false;
                }
            }
            else if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "0")
            {
                checkBox_SerialPort1.Checked = false;
                checkBox_SerialPort1.Enabled = false;
                comboBox_SerialPort1_BaudRate_Value.Enabled = false;
                comboBox_SerialPort1_PortName_Value.Enabled = false;

                checkBox_SerialPort2.Checked = false;
                checkBox_SerialPort2.Enabled = false;
                comboBox_SerialPort2_BaudRate_Value.Enabled = false;
                comboBox_SerialPort2_PortName_Value.Enabled = false;
            }
            
            comboBox_SerialPort1_BaudRate_Value.Text = ini12.INIRead(Global.MainSettingPath, "Comport", "BaudRate", "");
            comboBox_SerialPort1_PortName_Value.Text = ini12.INIRead(Global.MainSettingPath, "Comport", "PortName", "");
            comboBox_SerialPort2_BaudRate_Value.Text = ini12.INIRead(Global.MainSettingPath, "ExtComport", "BaudRate", "");
            comboBox_SerialPort2_PortName_Value.Text = ini12.INIRead(Global.MainSettingPath, "ExtComport", "PortName", "");
            #endregion

            #region -- Redrat --
            IRedRat3 redRat3;
            if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1")//Redrat存在//
            {
                for (int i = 0; i < RedRat3USBImpl.FindDevices().Count; i++)
                {
                    redRat3 = (IRedRat3)RedRat3USBImpl.FindDevices()[i].GetRedRat();
                    comboBox__SelectRedrat.Items.Add(redRat3.DeviceInformation.ProductName + " - " + 
                                                     redRat3.DeviceInformation.SerialNumber.ToString());

                    if (ini12.INIRead(Global.MainSettingPath, "RedRat", "SerialNumber", "") == "")
                    {
                        comboBox__SelectRedrat.Text = comboBox__SelectRedrat.Items[0].ToString();
                    }

                    if (redRat3.DeviceInformation.ProductName + " - " + 
                        redRat3.DeviceInformation.SerialNumber.ToString() ==
                        ini12.INIRead(Global.MainSettingPath, "RedRat", "SerialNumber", ""))
                    {
                        comboBox__SelectRedrat.Text = redRat3.DeviceInformation.ProductName + " - " + 
                                                      redRat3.DeviceInformation.SerialNumber.ToString();
                    }
                }
                comboBox_TvBrands.Enabled = true;
                comboBox__SelectRedrat.Enabled = true;
            }
            else
            {
                comboBox_TvBrands.Enabled = false;
                comboBox__SelectRedrat.Enabled = false;
            }
            comboBox_TvBrands.Text = ini12.INIRead(Global.MainSettingPath, "RedRat", "Brands", "");
            loadxml();
            #endregion
            
            #region -- Camera --
            if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")//Camera存在//
            {
                comboBox_CameraDevice.Enabled = true;
                comboBox_CameraAudio.Enabled = true;

                Filters filters = new Filters();
                Filter f;

                ini12.INIWrite(Global.MainSettingPath, "Camera", "VideoNumber", filters.VideoInputDevices.Count.ToString());
                ini12.INIWrite(Global.MainSettingPath, "Camera", "AudioNumber", filters.AudioInputDevices.Count.ToString());
                
                for (int c = 0; c < filters.VideoInputDevices.Count; c++)
                {
                    f = filters.VideoInputDevices[c];
                    comboBox_CameraDevice.Items.Add(f.Name);
                    if (f.Name == ini12.INIRead(Global.MainSettingPath, "Camera", "VideoName", ""))
                    {
                        comboBox_CameraDevice.Text = ini12.INIRead(Global.MainSettingPath, "Camera", "VideoName", "");
                    }
                }

                if (comboBox_CameraDevice.Text == "" && filters.VideoInputDevices.Count > 0)
                {
                    comboBox_CameraDevice.SelectedIndex = filters.VideoInputDevices.Count - 1;
                    ini12.INIWrite(Global.MainSettingPath, "Camera", "VideoIndex", comboBox_CameraDevice.SelectedIndex.ToString());
                    ini12.INIWrite(Global.MainSettingPath, "Camera", "VideoName", comboBox_CameraDevice.Text);
                }

                for (int j = 0; j < filters.AudioInputDevices.Count; j++)
                {
                    f = filters.AudioInputDevices[j];
                    comboBox_CameraAudio.Items.Add(f.Name);
                    if (f.Name == ini12.INIRead(Global.MainSettingPath, "Camera", "AudioName", ""))
                    {
                        comboBox_CameraAudio.Text = ini12.INIRead(Global.MainSettingPath, "Camera", "AudioName", "");
                    }
                }

                if (comboBox_CameraAudio.Text == "" && filters.AudioInputDevices.Count > 0)
                {
                    comboBox_CameraAudio.SelectedIndex = filters.AudioInputDevices.Count - 1;
                    ini12.INIWrite(Global.MainSettingPath, "Camera", "AudioIndex", comboBox_CameraAudio.SelectedIndex.ToString());
                    ini12.INIWrite(Global.MainSettingPath, "Camera", "AudioName", comboBox_CameraAudio.Text);
                }
            }
            else
            {
                comboBox_CameraDevice.Enabled = false;
                comboBox_CameraAudio.Enabled = false;
            }
            #endregion

            if (ini12.INIRead(Global.MainSettingPath, "LogSearch", "TextNum", "") == "")
            {
                ini12.INIWrite(Global.MainSettingPath, "LogSearch", "TextNum", "0");
            }
        }
        
        //自動調整ComboBox寬度
        private void AdjustWidthComboBox_DropDown(object sender, System.EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (string s in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }

        private void checkBox_SerialPort1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SerialPort1.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Comport", "Checked", "1");
                comboBox_SerialPort1_BaudRate_Value.Enabled = true;
                comboBox_SerialPort1_PortName_Value.Enabled = true;
                SerialPortCheck();
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Comport", "Checked", "0");
                comboBox_SerialPort1_BaudRate_Value.Enabled = false;
                comboBox_SerialPort1_PortName_Value.Enabled = false;
                SerialPortCheck();
            }
        }

        private void checkBox_SerialPort2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SerialPort2.Checked == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "ExtComport", "Checked", "1");
                comboBox_SerialPort2_BaudRate_Value.Enabled = true;
                comboBox_SerialPort2_PortName_Value.Enabled = true;
                SerialPortCheck();
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "ExtComport", "Checked", "0");
                comboBox_SerialPort2_BaudRate_Value.Enabled = false;
                comboBox_SerialPort2_PortName_Value.Enabled = false;
                SerialPortCheck();
            }
        }

        private void SerialPortCheck()
        {
            if (checkBox_SerialPort1.Checked == true)
            {
                if (comboBox_SerialPort1_PortName_Value.Text == ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxPort", ""))
                {
                    label_ErrorMessage.Text = "SerialPort don't select " + ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxPort", "");
                    pictureBox_SerialPort1.Image = Properties.Resources.ERROR;
                }
                else if (checkBox_SerialPort2.Checked == true &&
                        (comboBox_SerialPort1_PortName_Value.Text == comboBox_SerialPort2_PortName_Value.Text))
                {
                    label_ErrorMessage.Text = "SerialPort duplicate";
                    pictureBox_SerialPort1.Image = Properties.Resources.ERROR;
                }
                else
                {
                    pictureBox_SerialPort1.Image = null;
                }
            }
            else if (checkBox_SerialPort1.Checked == false)
            {
                pictureBox_SerialPort1.Image = null;
            }

            if (checkBox_SerialPort2.Checked == true)
            {
                if (comboBox_SerialPort2_PortName_Value.Text == ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxPort", ""))
                {
                    label_ErrorMessage.Text = "SerialPort don't select " + ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxPort", "");
                    pictureBox_SerialPort2.Image = Properties.Resources.ERROR;
                }
                else if (checkBox_SerialPort1.Checked == true &&
                        (comboBox_SerialPort2_PortName_Value.Text == comboBox_SerialPort1_PortName_Value.Text))
                {
                    label_ErrorMessage.Text = "SerialPort duplicate";
                    pictureBox_SerialPort2.Image = Properties.Resources.ERROR;
                }
                else
                {
                    pictureBox_SerialPort2.Image = null;
                }
            }
            else if (checkBox_SerialPort2.Checked == false)
            {
                pictureBox_SerialPort2.Image = null;
            }

            if (pictureBox_SerialPort1.Image == null &&
                pictureBox_SerialPort2.Image == null)
            {
                label_ErrorMessage.Text = "";
            }
        }

        private void textBox_ImagePath_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox_ImagePath.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "VideoPath", textBox_ImagePath.Text.Trim());
                pictureBox_ImagePath.Image = null;
            }
            else
            {
                pictureBox_ImagePath.Image = Properties.Resources.ERROR;
            }
        }

        private void textBox_LogPath_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox_LogPath.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "LogPath", textBox_LogPath.Text.Trim());
                pictureBox_LogPath.Image = null;
            }
            else
            {
                pictureBox_LogPath.Image = Properties.Resources.ERROR;
            }
        }

        private void textBox_RcDbPath_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox_RcDbPath.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "RedRat", "DBFile", textBox_RcDbPath.Text.Trim());
                pictureBox_RcDbPath.Image = null;
            }
            else
            {
                pictureBox_RcDbPath.Image = Properties.Resources.ERROR;
            }
        }

        private void textBox_GeneratorPath_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(textBox_GeneratorPath.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "Generator", textBox_GeneratorPath.Text.Trim());
                pictureBox_GeneratorPath.Image = null;
            }
            else
            {
                pictureBox_GeneratorPath.Image = Properties.Resources.ERROR;
            }
        }

        private void textBox_DosPath_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox_DosPath.Text.Trim()) == true)
            {
                ini12.INIWrite(Global.MainSettingPath, "Device", "DOS", textBox_DosPath.Text.Trim());
                pictureBox_DosPath.Image = null;
            }
            else
            {
                pictureBox_DosPath.Image = Properties.Resources.ERROR;
            }
        }

        private void comboBox_CameraDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MainSettingPath, "Camera", "VideoIndex", comboBox_CameraDevice.SelectedIndex.ToString());
            ini12.INIWrite(Global.MainSettingPath, "Camera", "VideoName", comboBox_CameraDevice.Text);
        }

        private void comboBox_CameraAudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MainSettingPath, "Camera", "AudioIndex", comboBox_CameraAudio.SelectedIndex.ToString());
            ini12.INIWrite(Global.MainSettingPath, "Camera", "AudioName", comboBox_CameraAudio.Text);
        }

        private void comboBox_TvBrands_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MainSettingPath, "RedRat", "Brands", comboBox_TvBrands.Text.Trim());
            
        }

        private void comboBox__SelectRedrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MainSettingPath, "RedRat", "RedRatIndex", (comboBox__SelectRedrat.SelectedIndex).ToString());
            ini12.INIWrite(Global.MainSettingPath, "RedRat", "SerialNumber", comboBox__SelectRedrat.Text);
        }

        private void comboBox_SerialPort1_PortName_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialPortCheck();
        }

        private void comboBox_SerialPort1_BaudRate_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MainSettingPath, "Comport", "BaudRate", comboBox_SerialPort1_BaudRate_Value.Text.Trim());
        }

        private void comboBox_SerialPort2_PortName_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            SerialPortCheck();
        }

        private void comboBox_SerialPort2_BaudRate_Value_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MainSettingPath, "ExtComport", "BaudRate", comboBox_SerialPort2_BaudRate_Value.Text.Trim());
        }
    }
}
