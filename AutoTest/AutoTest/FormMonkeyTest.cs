using jini;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTest
{
    public partial class FormMonkeyTest : Form
    {
        public FormMonkeyTest()
        {
            InitializeComponent();
        }

        string MonkeyTestPath = Application.StartupPath + "\\Monkey_Test.ini";

        private void FormMonkeyTest_Load(object sender, EventArgs e)
        {
            if (ini12.INIRead(MonkeyTestPath, "Monket Test", "Connection Mode", "") == "Ethernet")
            {
                checkBoxEthernet.Checked = true;
                textBoxTVIP.Text = ini12.INIRead(MonkeyTestPath, "Monket Test", "TV IP", "");
            }

            if (ini12.INIRead(MonkeyTestPath, "Monket Test", "Connection Mode", "") == "USB")
                checkBoxUSBDebbug.Checked = true;

            if (ini12.INIRead(MonkeyTestPath, "Monket Test", "Basic Monkey", "") == "1")    //Basic Monkey
                checkBoxBasic.Checked = true;
            if (ini12.INIRead(MonkeyTestPath, "Monket Test", "Basic Monkey", "") == "0")
                checkBoxBasic.Checked = false;

            if (ini12.INIRead(MonkeyTestPath, "Monket Test", "Specified Monkey", "") == "1")    //Specified Monkey
            {
                checkBoxSpecified.Checked = true;
                buttonLoadApps.Enabled = true;
                comboxQcProName.Enabled = true;
            }
            if (ini12.INIRead(MonkeyTestPath, "Monket Test", "Specified Monkey", "") == "0")
            {
                checkBoxSpecified.Checked = false;
                buttonLoadApps.Enabled = false;
                comboxQcProName.Enabled = false;
            }

            if (ini12.INIRead(MonkeyTestPath, "Monket Test", "MonkeyRunner", "") == "1")    //MonkeyRunner
            {
                checkBoxMonkeyRunner.Checked = true;
                textBoxSDKTools.Enabled = true;
                buttonSDKTools.Enabled = true;
                textBoxMunkeyRunnerScheduler.Enabled = true;
                buttonMunkeyRunnerScheduler.Enabled = true;
            }
            if (ini12.INIRead(MonkeyTestPath, "Monket Test", "MonkeyRunner", "") == "0")
            {
                checkBoxMonkeyRunner.Checked = false;
                textBoxSDKTools.Enabled = false;
                buttonSDKTools.Enabled = false;
                textBoxMunkeyRunnerScheduler.Enabled = false;
                buttonMunkeyRunnerScheduler.Enabled = false;
            }
            
            textBoxMonkeyPath.Text = ini12.INIRead(MonkeyTestPath, "Monket Test", "Path", "");
            textBoxSDKTools.Text = ini12.INIRead(MonkeyTestPath, "Monket Test", "SDK Tools", "");
            textBoxMunkeyRunnerScheduler.Text = ini12.INIRead(MonkeyTestPath, "Monket Test", "MonkeyRunner Scheduler", "");
            comboxQcProName.Text = ini12.INIRead(MonkeyTestPath, "Monket Test", "Package", "");
        }

        private void buttonRunAll_Click(object sender, EventArgs e)
        {
            Add_ons MonkeyTest_All = new Add_ons();
            MonkeyTest_All.MonkeyTest();
        }

        private void buttonLoadApps_Click(object sender, EventArgs e)
        {
            ini12.INIWrite(MonkeyTestPath, "Monket Test", "Package", "");

            Add_ons MonkeyTest = new Add_ons();
            MonkeyTest.MonkeyTest();

            string ReadLine;
            string[] array;
            string Path = Application.StartupPath + "\\app.txt";
            //string Path = "D:\\app.txt";
            
            this.comboxQcProName.Items.Clear();
            StreamReader reader = new StreamReader(Path, System.Text.Encoding.GetEncoding("GB2312"));
            while (reader.Peek() >= 0)
            {
                try
                {
                    ReadLine = reader.ReadLine();
                    if (ReadLine != "")
                    {
                        ReadLine = ReadLine.Replace("\"", "").Substring(8);
                        array = ReadLine.Split(',');
                        if (array.Length == 0)
                        {
                            MessageBox.Show("您选择的导入数据类型有误，请重试！");
                            return;
                        }
                        this.comboxQcProName.Items.Add(array[0]);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                comboxQcProName.Text = "Select App";
            }
            
            SavedLabel.Text = "";
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            ini12.INIWrite(MonkeyTestPath, "Monket Test", "TV IP", textBoxTVIP.Text.Trim());
            ini12.INIWrite(MonkeyTestPath, "Monket Test", "Package", comboxQcProName.Text.Trim());
            ini12.INIWrite(MonkeyTestPath, "Monket Test", "Path", textBoxMonkeyPath.Text.Trim());
            ini12.INIWrite(MonkeyTestPath, "Monket Test", "SDK Tools", textBoxSDKTools.Text.Trim());
            ini12.INIWrite(MonkeyTestPath, "Monket Test", "MonkeyRunner Scheduler", textBoxMunkeyRunnerScheduler.Text.Trim());

            SavedLabel.Text = "Save Successfully !";
        }

        private void checkBoxBasic_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBasic.Checked == true)
            {
                checkBoxSpecified.Checked = false;
                checkBoxMonkeyRunner.Checked = false;

                ini12.INIWrite(MonkeyTestPath, "Monket Test", "Basic Monkey", "1");
            }
            if (checkBoxBasic.Checked == false)
                ini12.INIWrite(MonkeyTestPath, "Monket Test", "Basic Monkey", "0");

            SavedLabel.Text = "";
        }

        private void checkBoxSpecified_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxSpecified.Checked == true)
            {
                checkBoxBasic.Checked = false;
                checkBoxMonkeyRunner.Checked = false;

                ini12.INIWrite(MonkeyTestPath, "Monket Test", "Specified Monkey", "1");
                buttonLoadApps.Enabled = true;
                comboxQcProName.Enabled = true;
            }
            if (checkBoxSpecified.Checked == false)
            {
                ini12.INIWrite(MonkeyTestPath, "Monket Test", "Specified Monkey", "0");
                buttonLoadApps.Enabled = false;
                comboxQcProName.Enabled = false;
            }

            SavedLabel.Text = "";
        }

        private void checkBoxMonkeyRunner_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMonkeyRunner.Checked == true)
            {
                checkBoxBasic.Checked = false;
                checkBoxSpecified.Checked = false;

                ini12.INIWrite(MonkeyTestPath, "Monket Test", "MonkeyRunner", "1");
                textBoxSDKTools.Enabled = true;
                buttonSDKTools.Enabled = true;

                textBoxMunkeyRunnerScheduler.Enabled = true;
                buttonMunkeyRunnerScheduler.Enabled = true;
            }
            if (checkBoxMonkeyRunner.Checked == false)
            {
                ini12.INIWrite(MonkeyTestPath, "Monket Test", "MonkeyRunner", "0");
                textBoxSDKTools.Enabled = false;
                buttonSDKTools.Enabled = false;

                textBoxMunkeyRunnerScheduler.Enabled = false;
                buttonMunkeyRunnerScheduler.Enabled = false;
            }

            SavedLabel.Text = "";
        }

        private void checkBoxEthernet_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEthernet.Checked == true)
            {
                ini12.INIWrite(MonkeyTestPath, "Monket Test", "Connection Mode", "Ethernet");
                textBoxTVIP.Enabled = true;
                checkBoxUSBDebbug.Checked = false;
            }

            if (checkBoxEthernet.Checked == false)
            {
                textBoxTVIP.Enabled = false;
            }
            
            SavedLabel.Text = "";
        }

        private void checkBoxUSBDebbug_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUSBDebbug.Checked == true)
            {
                ini12.INIWrite(MonkeyTestPath, "Monket Test", "Connection Mode", "USB");
                checkBoxEthernet.Checked = false;
            }

            SavedLabel.Text = "";
        }

        private void buttonMonkeyPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBoxMonkeyPath.Text = folderBrowserDialog1.SelectedPath;

            SavedLabel.Text = "";
        }

        private void buttonSDKTools_Click(object sender, EventArgs e)
        {
            folderBrowserDialog2.ShowDialog();
            textBoxSDKTools.Text = folderBrowserDialog2.SelectedPath;

            SavedLabel.Text = "";
        }

        private void buttonMunkeyRunnerScheduler_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            textBoxMunkeyRunnerScheduler.Text = openFileDialog1.SafeFileName;

            SavedLabel.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;

            p.Start();
            
            p.StandardInput.WriteLine("cd D:\\android-sdk-windows\\tools");
            p.StandardInput.WriteLine("monkeyrunner test.py");
            //p.StandardInput.WriteLine("from com.android.monkeyrunner import MonkeyRunner, MonkeyDevice, MonkeyImage");
            //p.StandardInput.WriteLine("device = MonkeyRunner.waitForConnection()");
            //p.StandardInput.WriteLine("device.press('KEYCODE_HOME', 'DOWN_AND_UP')");
            //p.StandardInput.WriteLine("exit");
            string result = p.StandardOutput.ReadToEnd();
            Console.WriteLine(result);
            //p.StandardInput.WriteLine("cd C:\\Users\\Remi.Lin\\AppData\\Local\\Android\\android-sdk\\tools\\");
            //p.StandardInput.WriteLine("monkeyrunner");

            //p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");  //不加exit便不會輸出到console
            //strOutput = p.StandardOutput.ReadToEnd();

            //p.WaitForExit();
            //p.Close();
        }
    }
}
