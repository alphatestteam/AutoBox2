using BlueRatLibrary;
using DirectX.Capture;
using jini;
using Microsoft.Win32.SafeHandles;
using RedRat.IR;
using RedRat.RedRat3;
using RedRat.RedRat3.USB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Xml.Linq;
using USBClassLibrary;

namespace AutoTest
{
    public partial class Form1 : Form
    {
        //private BackgroundWorker BackgroundWorker = new BackgroundWorker();
        //private Form_DGV_Autobox Form_DGV_Autobox = new Form_DGV_Autobox();

        private IRedRat3 redRat3 = null;
        private Add_ons Add_ons = new Add_ons();
        private RedRatDBParser RedRatData = new RedRatDBParser();
        private BlueRat MyBlueRat = new BlueRat();
        private static bool BlueRat_UART_Exception_status = false;

        private static void BlueRat_UARTException(Object sender, EventArgs e)
        {
            BlueRat_UART_Exception_status = true;
        }

        private bool FormIsClosing = false;
        private Capture capture = null;
        private Filters filters = null;
        private bool _captureInProgress;
        private bool StartButtonPressed = false;//true = 按下START//false = 按下STOP//
        //private bool excelstat = false;
        private bool VideoRecording = false;//是否正在錄影//
        private bool TimerPanel = false;
        //private bool VirtualRcPanel = false;
        private bool AcUsbPanel = false;
        private long timeCount = 0;
        private long TestTime = 0;
        private string videostring = "";
        private string srtstring = "";


        //宣告於keyword使用
        private Queue<byte> LogQueue1 = new Queue<byte>();
        private Queue<byte> LogQueue2 = new Queue<byte>();
        private char Keyword_SerialPort_1_temp_char;
        private byte Keyword_SerialPort_1_temp_byte;
        private char Keyword_SerialPort_2_temp_char;
        private byte Keyword_SerialPort_2_temp_byte;

        //Schedule暫停用的參數
        private bool Pause = false;
        private ManualResetEvent SchedulePause = new ManualResetEvent(true);
        private ManualResetEvent ScheduleWait = new ManualResetEvent(true);

        private SafeDataGridView portos_online;
        private int Breakpoint;
        private int Nowpoint;
        private bool Breakfunction = false;
        //private const int CS_DROPSHADOW = 0x20000;      //宣告陰影參數

        //拖動無窗體的控件>>>>>>>>>>>>>>>>>>>>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();

            //USB Connection//
            USBPort = new USBClass();
            USBDeviceProperties = new USBClass.DeviceProperties();
            USBPort.USBDeviceAttached += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceAttached);
            USBPort.USBDeviceRemoved += new USBClass.USBDeviceEventHandler(USBPort_USBDeviceRemoved);
            USBPort.RegisterForDeviceChange(true, this);
            //USBTryBoxConnection();
            USBTryRedratConnection();
            USBTryCameraConnection();
            //MyUSBBoxDeviceConnected = false;
            MyUSBRedratDeviceConnected = false;
            MyUSBCameraDeviceConnected = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //根據dpi調整視窗尺寸
            Graphics graphics = CreateGraphics();
            float dpiX = graphics.DpiX;
            float dpiY = graphics.DpiY;
            if (dpiX == 96 && dpiY == 96)
            {
                this.Height = 600;
                this.Width = 1120;
            }
            
            if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
            {
                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxVerson", "") == "1")
                {
                    ConnectAutoBox1();
                }

                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxVerson", "") == "2")
                {
                    ConnectAutoBox2();
                }

                pictureBox_BlueRat.Image = Properties.Resources.ON;
                GP0_GP1_AC_ON();
                GP2_GP3_USB_PC();
            }
            else
            {
                pictureBox_BlueRat.Image = Properties.Resources.OFF;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1" &&
                ini12.INIRead(Global.MainSettingPath, "RedRat", "RedRatIndex", "") != "")
            {
                OpenRedRat3();
            }
            else
            {
                pictureBox_RedRat.Image = Properties.Resources.OFF;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")
            {
                pictureBox_Camera.Image = Properties.Resources.ON;
                filters = new Filters();
                Filter f;

                comboBox_CameraDevice.Enabled = true;
                ini12.INIWrite(Global.MainSettingPath, "Camera", "VideoNumber", filters.VideoInputDevices.Count.ToString());

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
                comboBox_CameraDevice.Enabled = false;
            }
            else
            {
                pictureBox_Camera.Image = Properties.Resources.OFF;
            }

            if (ini12.INIRead(Global.MainSettingPath, "Comport", "Checked", "") == "1")
            {
                button_SerialPort1.Visible = true;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Comport", "Checked", "0");
                button_SerialPort1.Visible = false;
            }

            if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1")
            {
                button_SerialPort2.Visible = true;
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "ExtComport", "Checked", "0");
                button_SerialPort2.Visible = false;
            }

            LoadRCDB();

            List<string> SchExist = new List<string> { };
            for (int i = 2; i < 6; i++)
            {
                SchExist.Add(ini12.INIRead(Global.MainSettingPath, "Schedule" + i, "Exist", ""));
            }

            if (SchExist[0] != "")
            {
                if (SchExist[0] == "0")
                    button_Schedule2.Visible = false;
                else
                    button_Schedule2.Visible = true;
            }
            else
            {
                SchExist[0] = "0";
                button_Schedule2.Visible = false;
            }

            if (SchExist[1] != "")
            {
                if (SchExist[1] == "0")
                    button_Schedule3.Visible = false;
                else
                    button_Schedule3.Visible = true;
            }
            else
            {
                SchExist[1] = "0";
                button_Schedule3.Visible = false;
            }

            if (SchExist[2] != "")
            {
                if (SchExist[2] == "0")
                    button_Schedule4.Visible = false;
                else
                    button_Schedule4.Visible = true;
            }
            else
            {
                SchExist[2] = "0";
                button_Schedule4.Visible = false;
            }

            if (SchExist[3] != "")
            {
                if (SchExist[3] == "0")
                    button_Schedule5.Visible = false;
                else
                    button_Schedule5.Visible = true;
            }
            else
            {
                SchExist[3] = "0";
                button_Schedule5.Visible = false;
            }

            Global.Schedule_2_Exist = int.Parse(SchExist[0]);
            Global.Schedule_3_Exist = int.Parse(SchExist[1]);
            Global.Schedule_4_Exist = int.Parse(SchExist[2]);
            Global.Schedule_5_Exist = int.Parse(SchExist[3]);

            button_Pause.Enabled = false;
            button_Schedule.PerformClick();
            button_Schedule1.PerformClick();
            CheckForIllegalCrossThreadCalls = false;
            TopMost = true;
            TopMost = false;
        }

        #region -- USB Detect --
        //暫時移除有關盒子的插拔偵測，因為有其他無相關裝置運用到相同的VID和PID
        private bool USBTryBoxConnection()
        {
            if (Global.AutoBoxComport.Count != 0)
            {
                for (int i = 0; i < Global.AutoBoxComport.Count; i++)
                {
                    if (USBClass.GetUSBDevice(
                        uint.Parse("067B", System.Globalization.NumberStyles.AllowHexSpecifier),
                        uint.Parse("2303", System.Globalization.NumberStyles.AllowHexSpecifier),
                        ref USBDeviceProperties,
                        true))
                    {
                        if (Global.AutoBoxComport[i] == "COM15")
                        {
                            BoxConnect();
                        }
                    }
                }
                return true;
            }
            else
            {
                BoxDisconnect();
                return false;
            }
        }

        private bool USBTryRedratConnection()
        {
            if (USBClass.GetUSBDevice(uint.Parse("112A", System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse("0005", System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false))
            {
                //My Device is attached
                RedratConnect();
                return true;
            }
            else
            {
                RedratDisconnect();
                return false;
            }
        }
        /*
        private bool USBTryCameraConnection()
        {
            int DeviceNumber = Global.VID.Count;
            int VidCount = Global.VID.Count - 1;
            int PidCount = Global.PID.Count - 1;
            
            if (DeviceNumber != 0)
            {
                for (int i = 0; i < DeviceNumber; i++)
                {
                    if (USBClass.GetUSBDevice(uint.Parse(Global.VID[i], style: System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[i], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false))
                    {
                        CameraConnect();
                        return true;
                    }
                }
                return true;
            }
            else
            {
                CameraDisconnect();
                return false;
            }
        }
        */
        private bool USBTryCameraConnection()
        {
            int DeviceNumber = Global.VID.Count;
            int VidCount = Global.VID.Count - 1;
            int PidCount = Global.PID.Count - 1;

            if (DeviceNumber != 0)
            {
                for (int i = 0; i < DeviceNumber; i++)
                {
                    if (USBClass.GetUSBDevice(uint.Parse(Global.VID[i], style: System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[i], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false))
                    {
                        CameraConnect();
                    }
                }
                return true;
            }
            else
            {
                CameraDisconnect();
                return false;
            }
            /*
            if (DeviceNumber == 0)
            {
                CameraDisconnect();
                return false;
            }
            
            switch (DeviceNumber)
            {
                case 1:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                case 2:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                case 3:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                case 4:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                case 5:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                case 6:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                case 7:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 7], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 7], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                case 8:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 7], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 7], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 8], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 8], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                case 9:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 7], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 7], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 8], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 8], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 9], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 9], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                case 10:
                    if (
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 1], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 2], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 3], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 4], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 5], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 6], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 7], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 7], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 8], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 8], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 9], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 9], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                        USBClass.GetUSBDevice(uint.Parse(Global.VID[VidCount - 10], System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[PidCount - 10], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false)
                        )
                    {
                        CameraConnect();
                        return true;
                    }
                    else
                    {
                        CameraDisconnect();
                        return false;
                    }
                default:
                    CameraDisconnect();
                    return false;
            }
            */
        }

        private void USBPort_USBDeviceAttached(object sender, USBClass.USBDeviceEventArgs e)
        {
            /*
            if (!MyUSBBoxDeviceConnected)
            {
                Console.WriteLine("USBPort_USBDeviceAttached = " + MyUSBBoxDeviceConnected);
                if (USBTryBoxConnection())
                {
                    MyUSBBoxDeviceConnected = true;
                }
            }
            */

            if (!MyUSBRedratDeviceConnected)
            {
                if (USBTryRedratConnection())
                {
                    MyUSBRedratDeviceConnected = true;
                }
            }

            if (!MyUSBCameraDeviceConnected)
            {
                if (USBTryCameraConnection() == true)
                {
                    MyUSBCameraDeviceConnected = true;
                }
            }
        }

        private void USBPort_USBDeviceRemoved(object sender, USBClass.USBDeviceEventArgs e)
        {
            /*
            if (!USBClass.GetUSBDevice(uint.Parse("067B", System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse("2303", System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false))
            {
                Console.WriteLine("USBPort_USBDeviceRemoved = " + MyUSBBoxDeviceConnected);
                //My Device is removed
                MyUSBBoxDeviceConnected = false;
                USBTryBoxConnection();
            }
            */

            if (!USBClass.GetUSBDevice(uint.Parse("112A", System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse("0005", System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false))
            {
                //My Redrat is removed
                MyUSBRedratDeviceConnected = false;
                USBTryRedratConnection();
            }
            /*
            if (!USBClass.GetUSBDevice(uint.Parse("045E", System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse("0766", System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false) ||
                !USBClass.GetUSBDevice(uint.Parse("114D", System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse("8C00", System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false))
            {
                //My Camera is removed
                MyUSBCameraDeviceConnected = false;
                USBTryCameraConnection();
            }
            */
            int DeviceNumber = Global.VID.Count;

            if (DeviceNumber != 0)
            {
                for (int i = 0; i < DeviceNumber; i++)
                {
                    if (!USBClass.GetUSBDevice(uint.Parse(Global.VID[i], style: System.Globalization.NumberStyles.AllowHexSpecifier), uint.Parse(Global.PID[i], System.Globalization.NumberStyles.AllowHexSpecifier), ref USBDeviceProperties, false))
                    {
                        MyUSBCameraDeviceConnected = false;
                        USBTryCameraConnection();
                    }
                }
            }
        }

        

        private void BoxConnect()       //TO DO: Inset your connection code here
        {
            pictureBox_BlueRat.Image = Properties.Resources.ON;
        }

        private void BoxDisconnect()        //TO DO: Insert your disconnection code here
        {
            pictureBox_BlueRat.Image = Properties.Resources.OFF;
        }

        private void RedratConnect()        //TO DO: Inset your connection code here
        {
            ini12.INIWrite(Global.MainSettingPath, "Device", "RedRatExist", "1");
            pictureBox_RedRat.Image = Properties.Resources.ON;
        }

        private void RedratDisconnect()     //TO DO: Insert your disconnection code here
        {
            ini12.INIWrite(Global.MainSettingPath, "Device", "RedRatExist", "0");
            pictureBox_RedRat.Image = Properties.Resources.OFF;
        }

        private void CameraConnect()        //TO DO: Inset your connection code here
        {
            if (ini12.INIRead(Global.MainSettingPath, "Device", "Name", "") != "")
            {
                ini12.INIWrite(Global.MainSettingPath, "Device", "CameraExist", "1");
                pictureBox_Camera.Image = Properties.Resources.ON;
                if (StartButtonPressed == false)
                    button_Camera.Enabled = true;
            }
        }

        private void CameraDisconnect()     //TO DO: Insert your disconnection code here
        {
            ini12.INIWrite(Global.MainSettingPath, "Device", "CameraExist", "0");
            pictureBox_Camera.Image = Properties.Resources.OFF;
            if (StartButtonPressed == false)
                button_Camera.Enabled = false;
        }

        protected override void WndProc(ref Message m)
        {
            USBPort.ProcessWindowsMessage(ref m);
            base.WndProc(ref m);
        }
        #endregion
        
        private void OnCaptureComplete(object sender, EventArgs e)
        {
            // Demonstrate the Capture.CaptureComplete event.
            Debug.WriteLine("Capture complete.");
        }

        //執行緒控制label.text
        private delegate void UpdateUICallBack(string value, Control ctl);
        private void UpdateUI(string value, Control ctl)
        {
            if (InvokeRequired)
            {
                UpdateUICallBack uu = new UpdateUICallBack(UpdateUI);
                Invoke(uu, value, ctl);
            }
            else
            {
                ctl.Text = value;
            }
        }

        //執行緒控制 datagriveiew
        private delegate void UpdateUICallBack1(string value, DataGridView ctl);
        private void GridUI(string i, DataGridView gv)
        {
            if (InvokeRequired)
            {
                UpdateUICallBack1 uu = new UpdateUICallBack1(GridUI);
                Invoke(uu, i, gv);
            }
            else
            {
                DataGridView_Schedule.ClearSelection();
                gv.Rows[int.Parse(i)].Selected = true;
            }
        }

        // 執行緒控制 datagriverew的scorllingbar
        private delegate void UpdateUICallBack3(string value, DataGridView ctl);
        private void Gridscroll(string i, DataGridView gv)
        {
            if (InvokeRequired)
            {
                UpdateUICallBack3 uu = new UpdateUICallBack3(Gridscroll);
                Invoke(uu, i, gv);
            }
            else
            {
                //DataGridView1.ClearSelection();
                //gv.Rows[int.Parse(i)].Selected = true;
                gv.FirstDisplayedScrollingRowIndex = int.Parse(i);
            }
        }

        //執行緒控制 txtbox1
        private delegate void UpdateUICallBack2(string value, Control ctl);
        private void Txtbox1(string value, Control ctl)
        {
            if (InvokeRequired)
            {
                UpdateUICallBack2 uu = new UpdateUICallBack2(Txtbox1);
                Invoke(uu, value, ctl);
            }
            else
            {
                ctl.Text = value;
            }
        }

        //執行緒控制 txtbox2
        private delegate void UpdateUICallBack4(string value, Control ctl);
        private void Txtbox2(string value, Control ctl)
        {
            if (InvokeRequired)
            {
                UpdateUICallBack4 uu = new UpdateUICallBack4(Txtbox2);
                Invoke(uu, value, ctl);
            }
            else
            {
                ctl.Text = value;
            }
        }

        #region -- 拍照 --
        private void Jes() => Invoke(new EventHandler(delegate{Myshot();}));

        private void Myshot()
        {
            button_Start.Enabled = false;
            capture.FrameEvent2 += new Capture.HeFrame(CaptureDone);
            capture.GrapImg();
        }

        // 複製原始圖片
        protected Bitmap CloneBitmap(Bitmap source)
        {
            return new Bitmap(source);
        }

        private void CaptureDone(System.Drawing.Bitmap e)
        {
            capture.FrameEvent2 -= new Capture.HeFrame(CaptureDone);
            string fName = ini12.INIRead(Global.MainSettingPath, "Record", "VideoPath", "");
            //string ngFolder = "Schedule" + Global.Schedule_Num + "_NG";

            //圖片印字>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            Bitmap newBitmap = CloneBitmap(e);
            newBitmap = CloneBitmap(e);
            pictureBox4.Image = newBitmap;

            if (ini12.INIRead(Global.MainSettingPath, "Record", "CompareChoose", "") == "1")
            {
                // Create Compare folder
                string comparePath = ini12.INIRead(Global.MainSettingPath, "Record", "ComparePath", "");
                //string ngPath = fName + "\\" + ngFolder;
                string compareFile = comparePath + "\\" + "cf-" + Global.Loop_Number + "_" + Global.caption_Num + ".png";
                if (Global.caption_Num == 0)
                    Global.caption_Num++;
                /*
                if (Directory.Exists(ngPath))
                {

                }
                else
                {
                    Directory.CreateDirectory(ngPath);
                }
                */
                // 圖片比較

                /*
                newBitmap = CloneBitmap(e);
                newBitmap = RGB2Gray(newBitmap);
                newBitmap = ConvertTo1Bpp2(newBitmap);
                newBitmap = SobelEdgeDetect(newBitmap);                
                this.pictureBox4.Image = newBitmap;
                */
                pictureBox4.Image.Save(compareFile);
                if (Global.Loop_Number < 2)
                {

                }
                else
                {
                    Thread MyCompareThread = new Thread(new ThreadStart(MyCompareCamd));
                    MyCompareThread.Start();
                }
            }

            Graphics bitMap_g = Graphics.FromImage(pictureBox4.Image);//底圖
            Font Font = new Font("Microsoft JhengHei Light", 16, FontStyle.Bold);
            Brush FontColor = new SolidBrush(Color.Red);

            //照片印上現在步驟//
            if (DataGridView_Schedule.Rows[Global.Schedule_Step].Cells[0].Value.ToString() == "_cmd")
            {
                if (Global.Schedule_Step == 0)
                {
                    bitMap_g.DrawString("  ( " + label_Command.Text + " )",
                                    Font,
                                    FontColor,
                                    new PointF(5, 400));
                }
                else
                {
                    bitMap_g.DrawString(DataGridView_Schedule.Rows[Global.Schedule_Step - 1].Cells[0].Value.ToString() + "  ( " + label_Command.Text + " )",
                                    Font,
                                    FontColor,
                                    new PointF(5, 400));
                }
            }
            else
            {
                bitMap_g.DrawString(DataGridView_Schedule.Rows[Global.Schedule_Step].Cells[0].Value.ToString() + "  ( " + label_Command.Text + " )",
                                    Font,
                                    FontColor,
                                    new PointF(5, 400));
            }
            
            //照片印上現在時間//
            bitMap_g.DrawString(TimeLabel.Text, 
                                Font, 
                                FontColor, 
                                new PointF(5, 440));

            Font.Dispose();
            FontColor.Dispose();
            bitMap_g.Dispose();

            string t = fName + "\\" + "pic-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "(" + label_LoopNumber_Value.Text + "-" + Global.caption_Num + ").png";
            pictureBox4.Image.Save(t);
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            button_Start.Enabled = true;
        }
        #endregion

        #region -- 圖片比對 --
        // 內存法
        public static Bitmap RGB2Gray(Bitmap srcBitmap)
        {
            Rectangle rect = new Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height);
            System.Drawing.Imaging.BitmapData bmpdata = srcBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            IntPtr ptr = bmpdata.Scan0;

            int bytes = srcBitmap.Width * srcBitmap.Height * 3;
            byte[] rgbvalues = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbvalues, 0, bytes);

            double colortemp = 0;
            for (int i = 0; i < rgbvalues.Length; i += 3)
            {
                colortemp = rgbvalues[i + 2] * 0.299 + rgbvalues[i + 1] * 0.587 + rgbvalues[i] * 0.114;
                rgbvalues[i] = rgbvalues[i + 1] = rgbvalues[i + 2] = (byte)colortemp;
            }

            System.Runtime.InteropServices.Marshal.Copy(rgbvalues, 0, ptr, bytes);

            srcBitmap.UnlockBits(bmpdata);
            return (srcBitmap);
        }

        // Sobel法 
        private Bitmap SobelEdgeDetect(Bitmap original)
        {
            Bitmap b = original;
            Bitmap bb = original;
            int width = b.Width;
            int height = b.Height;
            int[,] gx = new int[,] { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
            int[,] gy = new int[,] { { 1, 2, 1 }, { 0, 0, 0 }, { -1, -2, -1 } };

            int[,] allPixR = new int[width, height];
            int[,] allPixG = new int[width, height];
            int[,] allPixB = new int[width, height];

            int limit = 128 * 128;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    allPixR[i, j] = b.GetPixel(i, j).R;
                    allPixG[i, j] = b.GetPixel(i, j).G;
                    allPixB[i, j] = b.GetPixel(i, j).B;
                }
            }

            int new_rx = 0, new_ry = 0;
            int new_gx = 0, new_gy = 0;
            int new_bx = 0, new_by = 0;
            int rc, gc, bc;
            for (int i = 1; i < b.Width - 1; i++)
            {
                for (int j = 1; j < b.Height - 1; j++)
                {

                    new_rx = 0;
                    new_ry = 0;
                    new_gx = 0;
                    new_gy = 0;
                    new_bx = 0;
                    new_by = 0;
                    rc = 0;
                    gc = 0;
                    bc = 0;

                    for (int wi = -1; wi < 2; wi++)
                    {
                        for (int hw = -1; hw < 2; hw++)
                        {
                            rc = allPixR[i + hw, j + wi];
                            new_rx += gx[wi + 1, hw + 1] * rc;
                            new_ry += gy[wi + 1, hw + 1] * rc;

                            gc = allPixG[i + hw, j + wi];
                            new_gx += gx[wi + 1, hw + 1] * gc;
                            new_gy += gy[wi + 1, hw + 1] * gc;

                            bc = allPixB[i + hw, j + wi];
                            new_bx += gx[wi + 1, hw + 1] * bc;
                            new_by += gy[wi + 1, hw + 1] * bc;
                        }
                    }
                    if (new_rx * new_rx + new_ry * new_ry > limit || new_gx * new_gx + new_gy * new_gy > limit || new_bx * new_bx + new_by * new_by > limit)
                        bb.SetPixel(i, j, Color.Black);

                    //bb.SetPixel (i, j, Color.FromArgb(allPixR[i,j],allPixG[i,j],allPixB[i,j]));
                    else
                        bb.SetPixel(i, j, Color.Transparent);
                }
            }
            return bb;
        }

        public static bool ImageCompareString(Bitmap firstImage, Bitmap secondImage)
        {
            MemoryStream ms = new MemoryStream();
            firstImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            String firstBitmap = Convert.ToBase64String(ms.ToArray());
            ms.Position = 0;
            secondImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            String secondBitmap = Convert.ToBase64String(ms.ToArray());
            if (firstBitmap.Equals(secondBitmap))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// 圖片內容比較1
        /// Refer: http://www.programmer-club.com.tw/ShowSameTitleN/csharp/9880.html
        public float Similarity(System.Drawing.Bitmap img1, System.Drawing.Bitmap img2)
        {
            int rc, bc, gc;
            float cc = 0, hc = 0;

            for (int i = 0; i < img1.Size.Width; i++)
            {
                for (int j = 0; j < img1.Size.Height; j++)
                {
                    System.Drawing.Color c1 = img1.GetPixel(i, j);
                    System.Drawing.Color c2 = img2.GetPixel(i, j);

                    rc = Math.Abs(c1.R - c2.R);
                    bc = Math.Abs(c1.B - c2.B);
                    gc = Math.Abs(c1.G - c2.G);
                    cc = (float)(rc + bc + gc);

                    float f1 = (float)(255 * 3 * img1.Size.Width * img1.Size.Height);
                    hc += cc / f1;
                }
            }
            hc = hc * 100;
            return hc;
        }

        // GetHisogram 取long
        public long[] GetHistogram(System.Drawing.Bitmap picture)
        {
            long[] myHistogram = new long[256];

            for (int i = 0; i < picture.Size.Width; i++)
                for (int j = 0; j < picture.Size.Height; j++)
                {
                    System.Drawing.Color c = picture.GetPixel(i, j);

                    long Temp = 0;
                    Temp += c.R;
                    Temp += c.G;
                    Temp += c.B;

                    Temp = (int)Temp / 3;
                    myHistogram[Temp]++;
                }

            return myHistogram;
        }

        // GetHisogram 取int
        public int[] GetHisogram(Bitmap img)
        {
            BitmapData data = img.LockBits(new System.Drawing.Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int[] histogram = new int[256];
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int remain = data.Stride - data.Width * 3;
                for (int i = 0; i < histogram.Length; i++)
                    histogram[i] = 0;
                for (int i = 0; i < data.Height; i++)
                {
                    for (int j = 0; j < data.Width; j++)
                    {
                        int mean = ptr[0] + ptr[1] + ptr[2];
                        mean /= 3;
                        histogram[mean]++;
                        ptr += 3;
                    }
                    ptr += remain;
                }
            }
            img.UnlockBits(data);
            return histogram;
        }

        //計算相減後的絕對值
        private float GetAbs(int firstNum, int secondNum)
        {
            float abs = Math.Abs((float)firstNum - (float)secondNum);
            float result = Math.Max(firstNum, secondNum);
            if (result == 0)
                result = 1;
            return abs / result;
        }

        //最終計算結果
        public float GetResult(int[] firstNum, int[] scondNum)
        {
            if (firstNum.Length != scondNum.Length)
            {
                return 0;
            }
            else
            {
                float result = 0;
                int j = firstNum.Length;
                for (int i = 0; i < j; i++)
                {
                    result += 1 - GetAbs(firstNum[i], scondNum[i]);
                }
                return result / j;
            }
        }

        /// <summary>
        /// 判断图形里是否存在另外一个图形 并返回所在位置
        /// </summary>
        /// <param name=”p_SourceBitmap”>原始图形</param>
        /// <param name=”p_PartBitmap”>小图形</param>
        /// <param name=”p_Float”>溶差</param>
        /// <returns>坐标</returns>
        public Point GetImageContains(Bitmap p_SourceBitmap, Bitmap p_PartBitmap, int p_Float)
        {
            int _SourceWidth = p_SourceBitmap.Width;
            int _SourceHeight = p_SourceBitmap.Height;
            int _PartWidth = p_PartBitmap.Width;
            int _PartHeight = p_PartBitmap.Height;
            Bitmap _SourceBitmap = new Bitmap(_SourceWidth, _SourceHeight);
            Graphics _Graphics = Graphics.FromImage(_SourceBitmap);
            _Graphics.DrawImage(p_SourceBitmap, new Rectangle(0, 0, _SourceWidth, _SourceHeight));
            _Graphics.Dispose();
            BitmapData _SourceData = _SourceBitmap.LockBits(new Rectangle(0, 0, _SourceWidth, _SourceHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte[] _SourceByte = new byte[_SourceData.Stride * _SourceHeight];
            Marshal.Copy(_SourceData.Scan0, _SourceByte, 0, _SourceByte.Length);  //复制出p_SourceBitmap的相素信息
            _SourceBitmap.UnlockBits(_SourceData);
            Bitmap _PartBitmap = new Bitmap(_PartWidth, _PartHeight);
            _Graphics = Graphics.FromImage(_PartBitmap);
            _Graphics.DrawImage(p_PartBitmap, new Rectangle(0, 0, _PartWidth, _PartHeight));
            _Graphics.Dispose();
            BitmapData _PartData = _PartBitmap.LockBits(new Rectangle(0, 0, _PartWidth, _PartHeight), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            byte[] _PartByte = new byte[_PartData.Stride * _PartHeight];
            Marshal.Copy(_PartData.Scan0, _PartByte, 0, _PartByte.Length);   //复制出p_PartBitmap的相素信息
            _PartBitmap.UnlockBits(_PartData);
            for (int i = 0; i != _SourceHeight; i++)
            {
                if (_SourceHeight - i < _PartHeight) return new Point(-1, -1);  //如果 剩余的高 比需要比较的高 还要小 就直接返回
                int _PointX = -1;    //临时存放坐标 需要包正找到的是在一个X点上
                bool _SacnOver = true;   //是否都比配的上
                for (int z = 0; z != _PartHeight - 1; z++)       //循环目标进行比较
                {
                    int _TrueX = GetImageContains(_SourceByte, _PartByte, (i + z) * _SourceData.Stride, z * _PartData.Stride, _SourceWidth, _PartWidth, p_Float);
                    if (_TrueX == -1)   //如果没找到
                    {
                        _PointX = -1;    //设置坐标为没找到
                        _SacnOver = false;   //设置不进行返回
                        break;
                    }
                    else
                    {
                        if (z == 0) _PointX = _TrueX;
                        if (_PointX != _TrueX)   //如果找到了 也的保证坐标和上一行的坐标一样 否则也返回
                        {
                            _PointX = -1;//设置坐标为没找到
                            _SacnOver = false;  //设置不进行返回
                            break;
                        }
                    }
                }
                if (_SacnOver) return new Point(_PointX, i);
            }
            return new Point(-1, -1);
        }

        /// <summary>
        /// 判断图形里是否存在另外一个图形 所在行的索引
        /// </summary>
        /// <param name=”p_Source”>原始图形数据</param>
        /// <param name=”p_Part”>小图形数据</param>
        /// <param name=”p_SourceIndex”>开始位置</param>
        /// <param name=”p_SourceWidth”>原始图形宽</param>
        /// <param name=”p_PartWidth”>小图宽</param>
        /// <param name=”p_Float”>溶差</param>
        /// <returns>所在行的索引 如果找不到返回-1</returns>
        private int GetImageContains(byte[] p_Source, byte[] p_Part, int p_SourceIndex, int p_PartIndex, int p_SourceWidth, int p_PartWidth, int p_Float)
        {
            int _PartIndex = p_PartIndex;//
            int _PartRVA = _PartIndex;//p_PartX轴起点
            int _SourceIndex = p_SourceIndex;//p_SourceX轴起点
            for (int i = 0; i < p_SourceWidth; i++)
            {
                if (p_SourceWidth - i < p_PartWidth) return -1;
                Color _CurrentlyColor = Color.FromArgb((int)p_Source[_SourceIndex + 3], (int)p_Source[_SourceIndex + 2], (int)p_Source[_SourceIndex + 1], (int)p_Source[_SourceIndex]);
                Color _CompareColoe = Color.FromArgb((int)p_Part[_PartRVA + 3], (int)p_Part[_PartRVA + 2], (int)p_Part[_PartRVA + 1], (int)p_Part[_PartRVA]);
                _SourceIndex += 4;//成功，p_SourceX轴加4
                bool _ScanColor = ScanColor(_CurrentlyColor, _CompareColoe, p_Float);
                if (_ScanColor)
                {
                    _PartRVA += 4;//成功，p_PartX轴加4
                    int _SourceRVA = _SourceIndex;
                    bool _Equals = true;
                    for (int z = 0; z != p_PartWidth - 1; z++)
                    {
                        _CurrentlyColor = Color.FromArgb((int)p_Source[_SourceRVA + 3], (int)p_Source[_SourceRVA + 2], (int)p_Source[_SourceRVA + 1], (int)p_Source[_SourceRVA]);
                        _CompareColoe = Color.FromArgb((int)p_Part[_PartRVA + 3], (int)p_Part[_PartRVA + 2], (int)p_Part[_PartRVA + 1], (int)p_Part[_PartRVA]);
                        if (!ScanColor(_CurrentlyColor, _CompareColoe, p_Float))
                        {
                            _PartRVA = _PartIndex;//失败，重置p_PartX轴开始
                            _Equals = false;
                            break;
                        }
                        _PartRVA += 4;//成功，p_PartX轴加4
                        _SourceRVA += 4;//成功，p_SourceX轴加4
                    }
                    if (_Equals) return i;
                }
                else
                {
                    _PartRVA = _PartIndex;//失败，重置p_PartX轴开始
                }
            }
            return -1;
        }

        /// <summary>
        /// 检查色彩(可以根据这个更改比较方式
        /// </summary>
        /// <param name=”p_CurrentlyColor”>当前色彩</param>
        /// <param name=”p_CompareColor”>比较色彩</param>
        /// <param name=”p_Float”>溶差</param>
        /// <returns></returns>
        private bool ScanColor(Color p_CurrentlyColor, Color p_CompareColor, int p_Float)
        {
            int _R = p_CurrentlyColor.R;
            int _G = p_CurrentlyColor.G;
            int _B = p_CurrentlyColor.B;
            return (_R <= p_CompareColor.R + p_Float && _R >= p_CompareColor.R - p_Float) && (_G <= p_CompareColor.G + p_Float && _G >= p_CompareColor.G - p_Float) && (_B <= p_CompareColor.B + p_Float && _B >= p_CompareColor.B - p_Float);
        }

        /// <summary>
        /// 图像二值化1：取图片的平均灰度作为阈值，低于该值的全都为0，高于该值的全都为255
        /// </summary>
        /// <param name="bmp"></param>
        /// <returns></returns>
        public static Bitmap ConvertTo1Bpp1(Bitmap bmp)
        {
            int average = 0;
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    average += color.B;
                }
            }
            average = (int)average / (bmp.Width * bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    //获取该点的像素的RGB的颜色
                    Color color = bmp.GetPixel(i, j);
                    int value = 255 - color.B;
                    Color newColor = value > average ? Color.FromArgb(0, 0, 0) : Color.FromArgb(255, 255, 255);
                    bmp.SetPixel(i, j, newColor);
                }
            }
            return bmp;
        }

        /// <summary>
        /// 图像二值化2
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Bitmap ConvertTo1Bpp2(Bitmap img)
        {
            int w = img.Width;
            int h = img.Height;
            Bitmap bmp = new Bitmap(w, h, PixelFormat.Format1bppIndexed);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format1bppIndexed);
            for (int y = 0; y < h; y++)
            {
                byte[] scan = new byte[(w + 7) / 8];
                for (int x = 0; x < w; x++)
                {
                    Color c = img.GetPixel(x, y);
                    if (c.GetBrightness() >= 0.5) scan[x / 8] |= (byte)(0x80 >> (x % 8));
                }
                Marshal.Copy(scan, 0, (IntPtr)((int)data.Scan0 + data.Stride * y), scan.Length);
            }
            return bmp;
        }

        /// <summary>
        /// 圖片內容比較2-1
        /// Refer: http://fecbob.pixnet.net/blog/post/38125033-c%23-%E5%9C%96%E7%89%87%E5%85%A7%E5%AE%B9%E6%AF%94%E8%BC%83
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public struct RGBdata
        {
            public int r;
            public int g;
            public int b;

            public int GetLargest()
            {
                if (r > b)
                {
                    if (r > g)
                    {
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }
                }
                else
                {
                    return 3;
                }
            }
        }

        /// <summary>
        /// 圖片內容比較2-2
        /// Refer: http://fecbob.pixnet.net/blog/post/38125033-c%23-%E5%9C%96%E7%89%87%E5%85%A7%E5%AE%B9%E6%AF%94%E8%BC%83
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        private RGBdata ProcessBitmap(Bitmap a)
        {
            BitmapData bmpData = a.LockBits(new Rectangle(0, 0, a.Width, a.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            IntPtr ptr = bmpData.Scan0;
            RGBdata data = new RGBdata();

            unsafe
            {
                byte* p = (byte*)(void*)ptr;
                int offset = bmpData.Stride - a.Width * 3;
                int width = a.Width * 3;
                for (int y = 0; y < a.Height; ++y)
                {
                    for (int x = 0; x < width; ++x)
                    {
                        data.r += p[0];             //gets red values
                        data.g += p[1];             //gets green values
                        data.b += p[2];             //gets blue values
                        ++p;
                    }
                    p += offset;
                }
            }
            a.UnlockBits(bmpData);
            return data;
        }

        /// <summary>
        /// 圖片內容比較2-3
        /// Refer: http://fecbob.pixnet.net/blog/post/38125033-c%23-%E5%9C%96%E7%89%87%E5%85%A7%E5%AE%B9%E6%AF%94%E8%BC%83
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public double GetSimilarity(Bitmap a, Bitmap b)
        {
            RGBdata dataA = ProcessBitmap(a);
            RGBdata dataB = ProcessBitmap(b);
            double result = 0;
            int averageA = 0;
            int averageB = 0;
            int maxA = 0;
            int maxB = 0;
            maxA = ((a.Width * 3) * a.Height);
            maxB = ((b.Width * 3) * b.Height);

            switch (dataA.GetLargest())            //Find dominant color to compare
            {
                case 1:
                    {
                        averageA = Math.Abs(dataA.r / maxA);
                        averageB = Math.Abs(dataB.r / maxB);
                        result = (averageA - averageB) / 2;
                        break;
                    }
                case 2:
                    {
                        averageA = Math.Abs(dataA.g / maxA);
                        averageB = Math.Abs(dataB.g / maxB);
                        result = (averageA - averageB) / 2;
                        break;
                    }
                case 3:
                    {
                        averageA = Math.Abs(dataA.b / maxA);
                        averageB = Math.Abs(dataB.b / maxB);
                        result = (averageA - averageB) / 2;
                        break;
                    }
            }

            result = Math.Abs((result + 100) / 100);
            if (result > 1.0)
            {
                result -= 1.0;
            }

            return result;
        }
        #endregion

        protected void OpenRedRat3()
        {
            int dev = 0;
            string intdev = ini12.INIRead(Global.MainSettingPath, "RedRat", "RedRatIndex", ""); ;

            if (intdev != "-1")
                dev = int.Parse(intdev);
            
            var devices = RedRat3USBImpl.FindDevices();

            // 假若設定值大於目前device個數，直接更改為目前device個數
            if (dev >= devices.Count)
                dev = devices.Count - 1;

            if (devices.Count > 0)
            {
                //RedRat已連線
                redRat3 = (IRedRat3)devices[dev].GetRedRat();

                //pictureBox1綠燈
                pictureBox_RedRat.Image = Properties.Resources.ON;
            }
            else
                pictureBox_RedRat.Image = Properties.Resources.OFF;
        }

        private void ConnectAutoBox1()
        {   // RS232 Setting
            serialPort3.StopBits = System.IO.Ports.StopBits.One;
            serialPort3.PortName = ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxPort", "");
            //serialPort3.BaudRate = int.Parse(ini12.INIRead(sPath, "SerialPort", "Baudrate", ""));
            if (serialPort3.IsOpen == false)
            {
                serialPort3.Open();
                object stream = typeof(SerialPort).GetField("internalSerialStream", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(serialPort3);
                hCOM = (SafeFileHandle)stream.GetType().GetField("_handle", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(stream);
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt") + " - Cannot connect to AutoBox.\n");
            }
        }

        private void ConnectAutoBox2()
        {
            uint temp_version;
            string curItem = ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxPort", "");
            if (MyBlueRat.Connect(curItem) == true)
            {
                temp_version = MyBlueRat.FW_VER;
                float v = temp_version;
                label_BoxVersion.Text = "_" + (v / 100).ToString();

                hCOM = MyBlueRat.ReturnSafeFileHandle();
                BlueRat_UART_Exception_status = false;
                UpdateRCFunctionButtonAfterConnection();
            }
            else
            {
                Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt") + " - Cannot connect to BlueRat.\n");
            }
        }

        private void DisconnectAutoBox1()
        {
            serialPort3.Close();
        }

        private void DisconnectAutoBox2()
        {
            if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
            {
                if (MyBlueRat.Disconnect() == true)
                {
                    if (BlueRat_UART_Exception_status)
                    {
                        //Serial_UpdatePortName(); 
                    }
                    BlueRat_UART_Exception_status = false;
                }
                else
                {
                    Console.WriteLine(DateTime.Now.ToString("h:mm:ss tt") + " - Cannot disconnect from RS232.\n");
                }
            }
        }

        public void Autocommand_RedRat(string Caller,string SigData)
        {
            string redcon = "";

            //讀取設備//
            if (Caller == "Form1")
            {
                RedRatData.RedRatLoadSignalDB(ini12.INIRead(Global.MainSettingPath, "RedRat", "DBFile", ""));
                redcon = ini12.INIRead(Global.MainSettingPath, "RedRat", "Brands", "");
            }
            else if (Caller == "FormRc")
            {
                string SelectRcLastTimePath = ini12.INIRead(Global.RcSettingPath, "Setting", "SelectRcLastTimePath", "");
                RedRatData.RedRatLoadSignalDB(ini12.INIRead(SelectRcLastTimePath, "Info", "DBFile", ""));
                redcon = ini12.INIRead(SelectRcLastTimePath, "Info", "Brands", "");
            }

            try
            {
                if (RedRatData.SignalDB.GetIRPacket(redcon, SigData).ToString() == "RedRat.IR.DoubleSignal")
                {
                    DoubleSignal sig = (DoubleSignal)RedRatData.SignalDB.GetIRPacket(redcon, SigData);
                    if (redRat3 != null)
                        redRat3.OutputModulatedSignal(sig);
                }
                else
                {
                    ModulatedSignal sig2 = (ModulatedSignal)RedRatData.SignalDB.GetIRPacket(redcon, SigData);
                    if (redRat3 != null)
                        redRat3.OutputModulatedSignal(sig2);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                MessageBox.Show("Transmit RC signal fail !", Ex.Message.ToString());
            }
        }

        private Boolean D = false;
        public void Autocommand_BlueRat(string Caller, string SigData)
        {
            try
            {
                if (Caller == "Form1")
                {
                    RedRatData.RedRatLoadSignalDB(ini12.INIRead(Global.MainSettingPath, "RedRat", "DBFile", ""));
                    RedRatData.RedRatSelectDevice(ini12.INIRead(Global.MainSettingPath, "RedRat", "Brands", ""));
                }
                else if (Caller == "FormRc")
                {
                    string SelectRcLastTimePath = ini12.INIRead(Global.RcSettingPath, "Setting", "SelectRcLastTimePath", "");
                    RedRatData.RedRatLoadSignalDB(ini12.INIRead(SelectRcLastTimePath, "Info", "DBFile", ""));
                    RedRatData.RedRatSelectDevice(ini12.INIRead(SelectRcLastTimePath, "Info", "Brands", ""));
                }

                RedRatData.RedRatSelectRCSignal(SigData, D);

                if (RedRatData.Signal_Type_Supported != true)
                {
                    return;
                }

                // Use UART to transmit RC signal
                int rc_duration = MyBlueRat.SendOneRC(RedRatData) / 1000 + 1;
                RedRatDBViewer_Delay(rc_duration);
                /*
                int SysDelay = int.Parse(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[9].Value.ToString());
                if (SysDelay <= rc_duration)
                {
                    RedRatDBViewer_Delay(rc_duration);
                }
                */
                if ((RedRatData.RedRatSelectedSignalType() == (typeof(DoubleSignal))) || (RedRatData.RC_ToggleData_Length_Value() > 0))
                {
                    RedRatData.RedRatSelectRCSignal(SigData, D);
                    D = !D;
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex);
                MessageBox.Show("Transmit RC signal fail !", Ex.Message.ToString());
            }
        }

        private void UpdateRCFunctionButtonAfterConnection()
        {
            if ((MyBlueRat.CheckConnection() == true))
            {
                if ((RedRatData != null) && (RedRatData.SignalDB != null) && (RedRatData.SelectedDevice != null) && (RedRatData.SelectedSignal != null))
                {
                    button_Start.Enabled = true;
                }
                else
                {
                    button_Start.Enabled = false;
                }
            }
        }

        // 這個主程式專用的delay的內部資料與function
        static bool RedRatDBViewer_Delay_TimeOutIndicator = false;
        private static void RedRatDBViewer_Delay_OnTimedEvent(object source, ElapsedEventArgs e)
        {
            RedRatDBViewer_Delay_TimeOutIndicator = true;
        }

        private void RedRatDBViewer_Delay(int delay_ms)
        {
            if (delay_ms <= 0) return;
            System.Timers.Timer aTimer = new System.Timers.Timer(delay_ms);
            aTimer.Elapsed += new ElapsedEventHandler(RedRatDBViewer_Delay_OnTimedEvent);
            RedRatDBViewer_Delay_TimeOutIndicator = false;
            aTimer.Enabled = true;
            while ((FormIsClosing == false) && (RedRatDBViewer_Delay_TimeOutIndicator == false))
            {
                Application.DoEvents();
                System.Threading.Thread.Sleep(1);//釋放CPU//
                
                if (Global.Break_Out_MyRunCamd == 1)//強制讓schedule直接停止//
                {
                    Global.Break_Out_MyRunCamd = 0;
                    break;
                }
            }
            aTimer.Stop();
            aTimer.Dispose();
        }

        private void Log(string msg)
        {
            textBox1.Invoke(new EventHandler(delegate
            {
                textBox1.Text = msg.Trim();
                serialPort1.WriteLine(msg.Trim());
            }));
        }

        private void ExtLog(string msg)
        {
            textBox2.Invoke(new EventHandler(delegate
            {
                textBox2.Text = msg.Trim();
                serialPort2.WriteLine(msg.Trim());
            }));
        }

        public static string ByteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        #region -- SerialPort1 Setup --
        protected void OpenSerialPort1()
        {
            try
            {
                if (serialPort1.IsOpen == false)
                {
                    string stopbit = ini12.INIRead(Global.MainSettingPath, "Comport", "StopBits", "");
                    switch (stopbit)
                    {
                        case "One":
                            serialPort1.StopBits = StopBits.One;
                            break;
                        case "Two":
                            serialPort1.StopBits = StopBits.Two;
                            break;
                    }
                    serialPort1.PortName = ini12.INIRead(Global.MainSettingPath, "Comport", "PortName", "");
                    serialPort1.BaudRate = int.Parse(ini12.INIRead(Global.MainSettingPath, "Comport", "BaudRate", ""));
                    serialPort1.DataBits = 8;
                    serialPort1.Parity = (Parity)0;
                    serialPort1.ReceivedBytesThreshold = 1;
                    // serialPort1.Encoding = System.Text.Encoding.GetEncoding(1252);

                    serialPort1.DataReceived += new SerialDataReceivedEventHandler(SerialPort1_DataReceived);       // DataReceived呼叫函式
                    serialPort1.Open();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "SerialPort1 Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void CloseSerialPort1()
        {
            serialPort1.Dispose();
            serialPort1.Close();
        }
        #endregion

        #region -- SerialPort2 Setup --
        protected void OpenSerialPort2()
        {
            try
            {
                if (serialPort2.IsOpen == false)
                {
                    string stopbit = ini12.INIRead(Global.MainSettingPath, "ExtComport", "StopBits", "");
                    switch (stopbit)
                    {
                        case "One":
                            serialPort2.StopBits = System.IO.Ports.StopBits.One;
                            break;
                        case "Two":
                            serialPort2.StopBits = System.IO.Ports.StopBits.Two;
                            break;
                    }
                    serialPort2.PortName = ini12.INIRead(Global.MainSettingPath, "ExtComport", "PortName", "");
                    serialPort2.BaudRate = int.Parse(ini12.INIRead(Global.MainSettingPath, "ExtComport", "BaudRate", ""));
                    // serialPort2.Encoding = System.Text.Encoding.GetEncoding(1252);

                    serialPort2.DataReceived += new SerialDataReceivedEventHandler(SerialPort2_DataReceived);       // DataReceived呼叫函式
                    serialPort2.Open();
                    object stream = typeof(SerialPort).GetField("internalSerialStream", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(serialPort2);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message.ToString(), "SerialPort2 Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void CloseSerialPort2()        // 關閉RS232 Port2
        {
            serialPort2.Dispose();
            serialPort2.Close();
        }
        #endregion

        #region -- 接受SerialPort1資料 --
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int data_to_read = serialPort1.BytesToRead;

            if (data_to_read > 0)
            {
                byte[] dataset = new byte[data_to_read];
                serialPort1.Read(dataset, 0, data_to_read);
                int index = 0;

                while (data_to_read > 0)
                {
                    LogQueue1.Enqueue(dataset[index]);
                    index++;
                    data_to_read--;
                }

                string text = Encoding.ASCII.GetString(dataset);
                
                DateTime.Now.ToShortTimeString();
                DateTime dt = DateTime.Now;
                text = text.Replace("\r\n", "\r").Replace("\n\r", "\r").Replace("\n", "\r").Replace("\r", "\r\n" + "[" + dt.ToString("yyyy/MM/dd HH:mm:ss") + "]  ");
                textBox1.AppendText(text);

                //string hex = ByteToHexStr(dataset);
                //File.AppendAllText(@"C:\WriteText.txt", text);
                /*
                //serialPort1.DiscardInBuffer();
                //serialPort1.DiscardOutBuffer();
                ///////////////////////////////////////////////先暫時拿掉此功能，因為_save可能會導致程式死當
                string hex = ByteToHexStr(dataset);
                if (DataGridView1.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_SXP")
                {
                    if (hex.Substring(0, 2) == "0E")
                    {
                        textBox1.AppendText("RX: " + hex);
                        Console.WriteLine("1---" + "RX: " + hex);

                        if (hex.Substring(hex.Length - 2) == "A5")
                        {
                            if (hex.Length >= 4 && hex.Substring(hex.Length - 4) == "A5A5")
                            {
                                textBox1.AppendText("\r\n");
                                Console.WriteLine("\r\n");
                                textBox1.AppendText("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                                Console.WriteLine("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                                textBox1.AppendText("\r\n");
                                Console.WriteLine("\r\n");
                            }
                            else
                            {
                                textBox1.AppendText(hex);
                                Console.WriteLine("2---" + hex);
                            }
                        }
                    }
                    else
                    {
                        textBox1.AppendText(hex);
                        Console.WriteLine("3---" + hex);
                        if(hex.Substring(hex.Length - 2) == "A5")
                        if (hex.Length >= 4 && hex.Substring(hex.Length - 4) == "A5A5")
                        {
                            textBox1.AppendText("\r\n");
                            Console.WriteLine("\r\n");
                            textBox1.AppendText("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                            Console.WriteLine("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                            textBox1.AppendText("\r\n");
                            Console.WriteLine("\r\n");
                        }

                        if (hex == "A5")
                        {
                            textBox1.AppendText("\r\n");
                            Console.WriteLine("\r\n");
                            textBox1.AppendText("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                            Console.WriteLine("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                            textBox1.AppendText("\r\n");
                            Console.WriteLine("\r\n");
                        }
                    }
                }
                else
                    textBox1.AppendText(text);
                */
            }
        }
        #endregion

        #region -- 接受SerialPort2資料 --
        private void SerialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int data_to_read = serialPort2.BytesToRead;
            if (data_to_read > 0)
            {
                byte[] dataset = new byte[data_to_read];

                serialPort2.Read(dataset, 0, data_to_read);
                int index = 0;
                while (data_to_read > 0)
                {
                    LogQueue2.Enqueue(dataset[index]);
                    index++;
                    data_to_read--;
                }

                string text = Encoding.ASCII.GetString(dataset);
                DateTime.Now.ToShortTimeString();
                DateTime dt = DateTime.Now;
                text = text.Replace("\r\n", "\r").Replace("\n\r", "\r").Replace("\n", "\r").Replace("\r", "\r\n" + "[" + dt.ToString("yyyy/MM/dd HH:mm:ss") + "]  ");
                textBox2.AppendText(text);

                //string hex = ByteToHexStr(dataset);
                //serialPort2.DiscardInBuffer();
                //serialPort2.DiscardOutBuffer();
                /*///////////////////////////////////////////////先暫時拿掉此功能，因為_save可能會導致程式死當
                if (DataGridView1.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_SXP")
                {
                    if (hex.Substring(0, 2) == "0E")
                    {
                        textBox2.AppendText("RX: " + hex);
                        Console.WriteLine("1---" + "RX: " + hex);

                        if (hex.Substring(hex.Length - 2) == "A5")
                        {
                            if (hex.Length >= 4 && hex.Substring(hex.Length - 4) == "A5A5")
                            {
                                textBox2.AppendText("\r\n");
                                Console.WriteLine("\r\n");
                                textBox2.AppendText("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                                Console.WriteLine("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                                textBox2.AppendText("\r\n");
                                Console.WriteLine("\r\n");
                            }
                            else
                            {
                                textBox2.AppendText(hex);
                                Console.WriteLine("2---" + hex);
                            }
                        }
                    }
                    else
                    {
                        textBox2.AppendText(hex);
                        Console.WriteLine("3---" + hex);
                        if (hex.Substring(hex.Length - 2) == "A5")
                            if (hex.Length >= 4 && hex.Substring(hex.Length - 4) == "A5A5")
                            {
                                textBox2.AppendText("\r\n");
                                Console.WriteLine("\r\n");
                                textBox2.AppendText("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                                Console.WriteLine("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                                textBox2.AppendText("\r\n");
                                Console.WriteLine("\r\n");
                            }

                        if (hex == "A5")
                        {
                            textBox2.AppendText("\r\n");
                            Console.WriteLine("\r\n");
                            textBox2.AppendText("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                            Console.WriteLine("TX: " + DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() + "\r\n");
                            textBox2.AppendText("\r\n");
                            Console.WriteLine("\r\n");
                        }
                    }
                }
                else
                {
                    textBox2.AppendText(text);

                    //textBox2.AppendText(text.Replace(Environment.NewLine, "<br>"));
                    /*
                    if (text.Length >= 2)
                    {
                        //Console.WriteLine(text.Substring(text.Length - 2));
                        if (text.Replace(Environment.NewLine, "\n\r").Substring(text.Length - 2) == "\n\r")
                        {
                            textBox2.AppendText("[" + DateTime.Now.ToString("ddd MMM d hh:mm:ss yyyy", new CultureInfo("en-US")) + "] ");
                            textBox2.AppendText(text);
                            Console.WriteLine("=================================");
                        }
                        else
                            textBox2.AppendText(text);
                    }
                    else
                    {
                        textBox2.AppendText(text);
                    }
                    //
                }
                */
            }
        }
        #endregion

        #region -- 儲存SerialPort1的log --
        private void Rs232save()
        {
            string fName = "";

            // 讀取ini中的路徑
            fName = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
            string t = fName + "\\_Log1_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + label_LoopNumber_Value.Text + ".txt";

            StreamWriter MYFILE = new StreamWriter(t, false, Encoding.ASCII);
            MYFILE.Write(textBox1.Text);
            /*
            Console.WriteLine("Save Log By Queue");
            while (LogQueue1.Count > 0)
            {
                char temp_char;
                byte temp_byte;

                temp_byte = LogQueue1.Dequeue();
                temp_char = (char)temp_byte;

                MYFILE.Write(temp_char);
            }
            */
            MYFILE.Close();
            Txtbox1("", textBox1);
        }
        #endregion

        #region -- 儲存SerialPort2的log --
        private void ExtRs232save()
        {
            string fName = "";

            // 讀取ini中的路徑
            fName = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
            string t = fName + "\\_Log2_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + label_LoopNumber_Value.Text + ".txt";

            StreamWriter MYFILE = new StreamWriter(t, false, Encoding.ASCII);
            MYFILE.Write(textBox2.Text);
            /*
            Console.WriteLine("Save Log By Queue");
            while (LogQueue2.Count > 0)
            {
                char temp_char;
                byte temp_byte;

                temp_byte = LogQueue2.Dequeue();
                temp_char = (char)temp_byte;

                MYFILE.Write(temp_char);
            }
            */
            MYFILE.Close();
            Txtbox2("", textBox2);
        }
        #endregion

        #region -- 關鍵字比對 - serialport_1 --
        private void MyLog1Camd()
        {
            string my_string = "";
            string csvFile = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "") + "\\Log1_keyword.csv";
            int[] compare_number = new int[10];
            bool[] send_status = new bool[10];
            int compare_paremeter = Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "TextNum", ""));

            while (StartButtonPressed == true)
            {
                while (LogQueue1.Count > 0)
                {
                    Keyword_SerialPort_1_temp_byte = LogQueue1.Dequeue();
                    Keyword_SerialPort_1_temp_char = (char)Keyword_SerialPort_1_temp_byte;

                    if (Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "Comport1", "")) == 1 && 
                        Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "TextNum", "")) > 0)
                    {
                        #region \n
                        if ((Keyword_SerialPort_1_temp_char == '\n'))
                        {
                            for (int i = 0; i < compare_paremeter; i++)
                            {
                                string compare_string = ini12.INIRead(Global.MainSettingPath, "LogSearch", "Text" + i, "");
                                int compare_num = Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "Times" + i, ""));
                                string[] ewords = my_string.Split(new string[] { compare_string }, StringSplitOptions.None);
                                if (Convert.ToInt32(ewords.Length - 1) >= 1)
                                {
                                    compare_number[i] = compare_number[i] + (ewords.Length - 1);
                                    //Console.WriteLine(compare_string + ": " + compare_number[i]);
                                    if (System.IO.File.Exists(csvFile) == false)
                                    {
                                        StreamWriter sw1 = new StreamWriter(csvFile, false, Encoding.UTF8);
                                        sw1.WriteLine("Key words, Setting times, Search times, Time");
                                        sw1.Dispose();
                                    }
                                    StreamWriter sw2 = new StreamWriter(csvFile, true);
                                    sw2.Write(compare_string + ",");
                                    sw2.Write(compare_num + ",");
                                    sw2.Write(compare_number[i] + ",");
                                    sw2.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                    sw2.Close();

                                    ////////////////////////////////////////////////////////////////////////////////////////////////MAIL//////////////////
                                    if (compare_number[i] > compare_num && send_status[i] == false)
                                    {
                                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", "Nowvalue", i.ToString());
                                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", "Display" + i, compare_number[i].ToString());
                                        if (ini12.INIRead(Global.MailSettingPath, "Mail Info", "From", "") != "" 
                                            && ini12.INIRead(Global.MailSettingPath, "Mail Info", "To", "") != "" 
                                            && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Sendmail", "") == "1")
                                        {
                                            FormMail FormMail = new FormMail();
                                            FormMail.logsend();
                                            send_status[i] = true;
                                        }
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////AC OFF ON//////////////////
                                    if (compare_number[i] % compare_num == 0 
                                        && ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1" 
                                        && ini12.INIRead(Global.MainSettingPath, "LogSearch", "ACcontrol", "") == "1")
                                    {
                                        byte[] val1;
                                        val1 = new byte[2];
                                        val1[0] = 0;

                                        bool jSuccess = PL2303_GP0_Enable(hCOM, 1);
                                        if (!jSuccess)
                                        {
                                            Log("GP0 output enable FAILED.");
                                        }
                                        else
                                        {
                                            uint val;
                                            val = (uint)int.Parse("0");
                                            bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                            if (bSuccess)
                                            {
                                                {
                                                    PowerState = false;
                                                    pictureBox_AcPower.Image = Properties.Resources.OFF;
                                                }
                                            }
                                        }

                                        System.Threading.Thread.Sleep(5000);

                                        jSuccess = PL2303_GP0_Enable(hCOM, 1);
                                        if (!jSuccess)
                                        {
                                            Log("GP0 output enable FAILED.");
                                        }
                                        else
                                        {
                                            uint val;
                                            val = (uint)int.Parse("1");
                                            bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                            if (bSuccess)
                                            {
                                                {
                                                    PowerState = true;
                                                    pictureBox_AcPower.Image = Properties.Resources.ON;
                                                }
                                            }
                                        }
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////AC OFF//////////////////
                                    if (compare_number[i] % compare_num == 0 
                                        && ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1" 
                                        && ini12.INIRead(Global.MainSettingPath, "LogSearch", "AC OFF", "") == "1")
                                    {
                                        byte[] val1 = new byte[2];
                                        val1[0] = 0;
                                        uint val = (uint)int.Parse("0");

                                        bool Success_GP0_Enable = PL2303_GP0_Enable(hCOM, 1);
                                        bool Success_GP0_SetValue = PL2303_GP0_SetValue(hCOM, val);

                                        bool Success_GP1_Enable = PL2303_GP1_Enable(hCOM, 1);
                                        bool Success_GP1_SetValue = PL2303_GP1_SetValue(hCOM, val);

                                        PowerState = false;

                                        pictureBox_AcPower.Image = Properties.Resources.OFF;
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////SAVE LOG//////////////////
                                    if (compare_number[i] % compare_num == 0 && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Savelog", "") == "1")
                                    {
                                        string fName = "";

                                        // 讀取ini中的路徑
                                        fName = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
                                        string t = fName + "\\_SaveLog1_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + label_LoopNumber_Value.Text + ".txt";

                                        StreamWriter MYFILE = new StreamWriter(t, false, Encoding.ASCII);
                                        MYFILE.Write(textBox1.Text);
                                        MYFILE.Close();
                                        Txtbox1("", textBox1);
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////STOP//////////////////
                                    if (compare_number[i] % compare_num == 0 && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Stop", "") == "1")
                                    {
                                        button_Start.PerformClick();
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////SCHEDULE//////////////////
                                    if (compare_number[i] % compare_num == 0)
                                    {
                                        int keyword_numer = i + 1;
                                        switch (keyword_numer)
                                        {
                                            case 1:
                                                Global.keyword_1 = "true";
                                                break;

                                            case 2:
                                                Global.keyword_2 = "true";
                                                break;

                                            case 3:
                                                Global.keyword_3 = "true";
                                                break;

                                            case 4:
                                                Global.keyword_4 = "true";
                                                break;

                                            case 5:
                                                Global.keyword_5 = "true";
                                                break;

                                            case 6:
                                                Global.keyword_6 = "true";
                                                break;

                                            case 7:
                                                Global.keyword_7 = "true";
                                                break;

                                            case 8:
                                                Global.keyword_8 = "true";
                                                break;

                                            case 9:
                                                Global.keyword_9 = "true";
                                                break;

                                            case 10:
                                                Global.keyword_10 = "true";
                                                break;
                                        }
                                    }
                                }
                            }
                            //textBox1.AppendText(my_string + '\n');
                            my_string = "";
                        }
                        #endregion

                        #region \r
                        else if ((Keyword_SerialPort_1_temp_char == '\r'))
                        {
                            for (int i = 0; i < compare_paremeter; i++)
                            {
                                string compare_string = ini12.INIRead(Global.MainSettingPath, "LogSearch", "Text" + i, "");
                                int compare_num = Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "Times" + i, ""));
                                string[] ewords = my_string.Split(new string[] { compare_string }, StringSplitOptions.None);
                                
                                if (Convert.ToInt32(ewords.Length - 1) >= 1)
                                {
                                    compare_number[i] = compare_number[i] + (ewords.Length - 1);
                                    //Console.WriteLine(compare_string + ": " + compare_number[i]);

                                    //////////////////////////////////////////////////////////////////////Create the compare csv file////////////////////
                                    if (System.IO.File.Exists(csvFile) == false)
                                    {
                                        StreamWriter sw1 = new StreamWriter(csvFile, false, Encoding.UTF8);
                                        sw1.WriteLine("Key words, Setting times, Search times, Time");
                                        sw1.Dispose();
                                    }
                                    StreamWriter sw2 = new StreamWriter(csvFile, true);
                                    sw2.Write(compare_string + ",");
                                    sw2.Write(compare_num + ",");
                                    sw2.Write(compare_number[i] + ",");
                                    sw2.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                    sw2.Close();

                                    ////////////////////////////////////////////////////////////////////////////////////////////////MAIL//////////////////
                                    if (compare_number[i] > compare_num && send_status[i] == false)
                                    {
                                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", "Nowvalue", i.ToString());
                                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", "Display" + i, compare_number[i].ToString());
                                        if (ini12.INIRead(Global.MailSettingPath, "Mail Info", "From", "") != ""
                                            && ini12.INIRead(Global.MailSettingPath, "Mail Info", "To", "") != ""
                                            && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Sendmail", "") == "1")
                                        {
                                            FormMail FormMail = new FormMail();
                                            FormMail.logsend();
                                            send_status[i] = true;
                                        }
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////AC OFF ON//////////////////
                                    if (compare_number[i] % compare_num == 0 
                                        && ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1" 
                                        && ini12.INIRead(Global.MainSettingPath, "LogSearch", "ACcontrol", "") == "1")
                                    {
                                        byte[] val1;
                                        val1 = new byte[2];
                                        val1[0] = 0;

                                        bool jSuccess = PL2303_GP0_Enable(hCOM, 1);
                                        if (!jSuccess)
                                        {
                                            Log("GP0 output enable FAILED.");
                                        }
                                        else
                                        {
                                            uint val;
                                            val = (uint)int.Parse("0");
                                            bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                            if (bSuccess)
                                            {
                                                {
                                                    PowerState = false;
                                                    pictureBox_AcPower.Image = Properties.Resources.OFF;
                                                }
                                            }
                                        }

                                        System.Threading.Thread.Sleep(5000);

                                        jSuccess = PL2303_GP0_Enable(hCOM, 1);
                                        if (!jSuccess)
                                        {
                                            Log("GP0 output enable FAILED.");
                                        }
                                        else
                                        {
                                            uint val;
                                            val = (uint)int.Parse("1");
                                            bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                            if (bSuccess)
                                            {
                                                {
                                                    PowerState = true;
                                                    pictureBox_AcPower.Image = Properties.Resources.ON;
                                                }
                                            }
                                        }
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////AC OFF//////////////////
                                    if (compare_number[i] % compare_num == 0
                                        && ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1"
                                        && ini12.INIRead(Global.MainSettingPath, "LogSearch", "AC OFF", "") == "1")
                                    {
                                        byte[] val1 = new byte[2];
                                        val1[0] = 0;
                                        uint val = (uint)int.Parse("0");

                                        bool Success_GP0_Enable = PL2303_GP0_Enable(hCOM, 1);
                                        bool Success_GP0_SetValue = PL2303_GP0_SetValue(hCOM, val);

                                        bool Success_GP1_Enable = PL2303_GP1_Enable(hCOM, 1);
                                        bool Success_GP1_SetValue = PL2303_GP1_SetValue(hCOM, val);

                                        PowerState = false;

                                        pictureBox_AcPower.Image = Properties.Resources.OFF;
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////SAVE LOG//////////////////
                                    if (compare_number[i] % compare_num == 0 && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Savelog", "") == "1")
                                    {
                                        string fName = "";

                                        // 讀取ini中的路徑
                                        fName = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
                                        string t = fName + "\\_SaveLog1_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + label_LoopNumber_Value.Text + ".txt";

                                        StreamWriter MYFILE = new StreamWriter(t, false, Encoding.ASCII);
                                        MYFILE.Write(textBox1.Text);
                                        MYFILE.Close();
                                        Txtbox1("", textBox1);
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////STOP//////////////////
                                    if (compare_number[i] % compare_num == 0 && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Stop", "") == "1")
                                    {
                                        button_Start.PerformClick();
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////SCHEDULE//////////////////
                                    if (compare_number[i] % compare_num == 0)
                                    {
                                        int keyword_numer = i + 1;
                                        switch (keyword_numer)
                                        {
                                            case 1:
                                                Global.keyword_1 = "true";
                                                break;

                                            case 2:
                                                Global.keyword_2 = "true";
                                                break;

                                            case 3:
                                                Global.keyword_3 = "true";
                                                break;

                                            case 4:
                                                Global.keyword_4 = "true";
                                                break;

                                            case 5:
                                                Global.keyword_5 = "true";
                                                break;

                                            case 6:
                                                Global.keyword_6 = "true";
                                                break;

                                            case 7:
                                                Global.keyword_7 = "true";
                                                break;

                                            case 8:
                                                Global.keyword_8 = "true";
                                                break;

                                            case 9:
                                                Global.keyword_9 = "true";
                                                break;

                                            case 10:
                                                Global.keyword_10 = "true";
                                                break;
                                        }
                                    }
                                }
                            }
                            //textBox1.AppendText(my_string + '\r');
                            my_string = "";
                        }
                        #endregion

                        else
                        {
                            my_string = my_string + Keyword_SerialPort_1_temp_char;
                        }
                    }
                    else
                    {
                        if ((Keyword_SerialPort_1_temp_char == '\n'))
                        {
                            //textBox1.AppendText(my_string + '\n');
                            my_string = "";
                        }
                        else if ((Keyword_SerialPort_1_temp_char == '\r'))
                        {
                            //textBox1.AppendText(my_string + '\r');
                            my_string = "";
                        }
                        else
                        {
                            my_string = my_string + Keyword_SerialPort_1_temp_char;
                        }
                    }
                }
                Thread.Sleep(500);
            }
        }
        #endregion

        #region -- 關鍵字比對 - serialport_2 --
        private void MyLog2Camd()
        {
            string my_string = "";
            string csvFile = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "") + "\\Log2_keyword.csv";
            int[] compare_number = new int[10];
            bool[] send_status = new bool[10];
            int compare_paremeter = Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "TextNum", ""));

            while (StartButtonPressed == true)
            {
                while (LogQueue2.Count > 0)
                {
                    Keyword_SerialPort_2_temp_byte = LogQueue2.Dequeue();
                    Keyword_SerialPort_2_temp_char = (char)Keyword_SerialPort_2_temp_byte;

                    if (Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "Comport2", "")) == 1 && 
                        Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "TextNum", "")) > 0)
                    {
                        #region \n
                        if ((Keyword_SerialPort_2_temp_char == '\n'))
                        {
                            for (int i = 0; i < compare_paremeter; i++)
                            {
                                string compare_string = ini12.INIRead(Global.MainSettingPath, "LogSearch", "Text" + i, "");
                                int compare_num = Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "Times" + i, ""));
                                string[] ewords = my_string.Split(new string[] { compare_string }, StringSplitOptions.None);
                                if (Convert.ToInt32(ewords.Length - 1) >= 1)
                                {
                                    compare_number[i] = compare_number[i] + (ewords.Length - 1);
                                    //Console.WriteLine(compare_string + ": " + compare_number[i]);
                                    if (System.IO.File.Exists(csvFile) == false)
                                    {
                                        StreamWriter sw1 = new StreamWriter(csvFile, false, Encoding.UTF8);
                                        sw1.WriteLine("Key words, Setting times, Search times, Time");
                                        sw1.Dispose();
                                    }
                                    StreamWriter sw2 = new StreamWriter(csvFile, true);
                                    sw2.Write(compare_string + ",");
                                    sw2.Write(compare_num + ",");
                                    sw2.Write(compare_number[i] + ",");
                                    sw2.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                    sw2.Close();

                                    ////////////////////////////////////////////////////////////////////////////////////////////////MAIL//////////////////
                                    if (compare_number[i] > compare_num && send_status[i] == false)
                                    {
                                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", "Nowvalue", i.ToString());
                                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", "Display" + i, compare_number[i].ToString());
                                        if (ini12.INIRead(Global.MailSettingPath, "Mail Info", "From", "") != ""
                                            && ini12.INIRead(Global.MailSettingPath, "Mail Info", "To", "") != ""
                                            && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Sendmail", "") == "1")
                                        {
                                            FormMail FormMail = new FormMail();
                                            FormMail.logsend();
                                            send_status[i] = true;
                                        }
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////AC OFF ON//////////////////
                                    if (compare_number[i] % compare_num == 0
                                        && ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1"
                                        && ini12.INIRead(Global.MainSettingPath, "LogSearch", "ACcontrol", "") == "1")
                                    {
                                        byte[] val1;
                                        val1 = new byte[2];
                                        val1[0] = 0;

                                        bool jSuccess = PL2303_GP0_Enable(hCOM, 1);
                                        if (!jSuccess)
                                        {
                                            Log("GP0 output enable FAILED.");
                                        }
                                        else
                                        {
                                            uint val;
                                            val = (uint)int.Parse("0");
                                            bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                            if (bSuccess)
                                            {
                                                {
                                                    PowerState = false;
                                                    pictureBox_AcPower.Image = Properties.Resources.OFF;
                                                }
                                            }
                                        }

                                        System.Threading.Thread.Sleep(5000);

                                        jSuccess = PL2303_GP0_Enable(hCOM, 1);
                                        if (!jSuccess)
                                        {
                                            Log("GP0 output enable FAILED.");
                                        }
                                        else
                                        {
                                            uint val;
                                            val = (uint)int.Parse("1");
                                            bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                            if (bSuccess)
                                            {
                                                {
                                                    PowerState = true;
                                                    pictureBox_AcPower.Image = Properties.Resources.ON;
                                                }
                                            }
                                        }
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////AC OFF//////////////////
                                    if (compare_number[i] % compare_num == 0
                                        && ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1"
                                        && ini12.INIRead(Global.MainSettingPath, "LogSearch", "AC OFF", "") == "1")
                                    {
                                        byte[] val1 = new byte[2];
                                        val1[0] = 0;
                                        uint val = (uint)int.Parse("0");

                                        bool Success_GP0_Enable = PL2303_GP0_Enable(hCOM, 1);
                                        bool Success_GP0_SetValue = PL2303_GP0_SetValue(hCOM, val);

                                        bool Success_GP1_Enable = PL2303_GP1_Enable(hCOM, 1);
                                        bool Success_GP1_SetValue = PL2303_GP1_SetValue(hCOM, val);

                                        PowerState = false;

                                        pictureBox_AcPower.Image = Properties.Resources.OFF;
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////SAVE LOG//////////////////
                                    if (compare_number[i] % compare_num == 0 && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Savelog", "") == "1")
                                    {
                                        string fName = "";

                                        // 讀取ini中的路徑
                                        fName = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
                                        string t = fName + "\\_SaveLog2_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + label_LoopNumber_Value.Text + ".txt";

                                        StreamWriter MYFILE = new StreamWriter(t, false, Encoding.ASCII);
                                        MYFILE.Write(textBox2.Text);
                                        MYFILE.Close();
                                        Txtbox2("", textBox2);
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////SCHEDULE//////////////////
                                    if (compare_number[i] % compare_num == 0 && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Stop", "") == "1")
                                    {
                                        button_Start.PerformClick();
                                    }

                                    if (compare_number[i] % compare_num == 0)
                                    {
                                        int keyword_numer = i + 1;
                                        switch (keyword_numer)
                                        {
                                            case 1:
                                                Global.keyword_1 = "true";
                                                break;

                                            case 2:
                                                Global.keyword_2 = "true";
                                                break;

                                            case 3:
                                                Global.keyword_3 = "true";
                                                break;

                                            case 4:
                                                Global.keyword_4 = "true";
                                                break;

                                            case 5:
                                                Global.keyword_5 = "true";
                                                break;

                                            case 6:
                                                Global.keyword_6 = "true";
                                                break;

                                            case 7:
                                                Global.keyword_7 = "true";
                                                break;

                                            case 8:
                                                Global.keyword_8 = "true";
                                                break;

                                            case 9:
                                                Global.keyword_9 = "true";
                                                break;

                                            case 10:
                                                Global.keyword_10 = "true";
                                                break;
                                        }
                                    }
                                }
                            }
                            //textBox2.AppendText(my_string + '\n');
                            my_string = "";
                        }
                        #endregion

                        #region \r
                        else if ((Keyword_SerialPort_2_temp_char == '\r'))
                        {
                            for (int i = 0; i < compare_paremeter; i++)
                            {
                                string compare_string = ini12.INIRead(Global.MainSettingPath, "LogSearch", "Text" + i, "");
                                int compare_num = Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "LogSearch", "Times" + i, ""));
                                string[] ewords = my_string.Split(new string[] { compare_string }, StringSplitOptions.None);
                                if (Convert.ToInt32(ewords.Length - 1) >= 1)
                                {
                                    compare_number[i] = compare_number[i] + (ewords.Length - 1);
                                    //Console.WriteLine(compare_string + ": " + compare_number[i]);

                                    //////////////////////////////////////////////////////////////////////Create the compare csv file////////////////////
                                    if (System.IO.File.Exists(csvFile) == false)
                                    {
                                        StreamWriter sw1 = new StreamWriter(csvFile, false, Encoding.UTF8);
                                        sw1.WriteLine("Key words, Setting times, Search times, Time");
                                        sw1.Dispose();
                                    }
                                    StreamWriter sw2 = new StreamWriter(csvFile, true);
                                    sw2.Write(compare_string + ",");
                                    sw2.Write(compare_num + ",");
                                    sw2.Write(compare_number[i] + ",");
                                    sw2.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                                    sw2.Close();

                                    ////////////////////////////////////////////////////////////////////////////////////////////////MAIL//////////////////
                                    if (compare_number[i] > compare_num && send_status[i] == false)
                                    {
                                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", "Nowvalue", i.ToString());
                                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", "Display" + i, compare_number[i].ToString());
                                        if (ini12.INIRead(Global.MailSettingPath, "Mail Info", "From", "") != "" 
                                            && ini12.INIRead(Global.MailSettingPath, "Mail Info", "To", "") != "" 
                                            && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Sendmail", "") == "1")
                                        {
                                            FormMail FormMail = new FormMail();
                                            FormMail.logsend();
                                            send_status[i] = true;
                                        }
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////AC OFF ON//////////////////
                                    if (compare_number[i] % compare_num == 0 
                                        && ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1" 
                                        && ini12.INIRead(Global.MainSettingPath, "LogSearch", "ACcontrol", "") == "1")
                                    {
                                        byte[] val1;
                                        val1 = new byte[2];
                                        val1[0] = 0;

                                        bool jSuccess = PL2303_GP0_Enable(hCOM, 1);
                                        if (!jSuccess)
                                        {
                                            Log("GP0 output enable FAILED.");
                                        }
                                        else
                                        {
                                            uint val;
                                            val = (uint)int.Parse("0");
                                            bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                            if (bSuccess)
                                            {
                                                {
                                                    PowerState = false;
                                                    pictureBox_AcPower.Image = Properties.Resources.OFF;
                                                }
                                            }
                                        }

                                        System.Threading.Thread.Sleep(5000);

                                        jSuccess = PL2303_GP0_Enable(hCOM, 1);
                                        if (!jSuccess)
                                        {
                                            Log("GP0 output enable FAILED.");
                                        }
                                        else
                                        {
                                            uint val;
                                            val = (uint)int.Parse("1");
                                            bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                            if (bSuccess)
                                            {
                                                {
                                                    PowerState = true;
                                                    pictureBox_AcPower.Image = Properties.Resources.ON;
                                                }
                                            }
                                        }
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////AC OFF//////////////////
                                    if (compare_number[i] % compare_num == 0
                                        && ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1"
                                        && ini12.INIRead(Global.MainSettingPath, "LogSearch", "AC OFF", "") == "1")
                                    {
                                        byte[] val1 = new byte[2];
                                        val1[0] = 0;
                                        uint val = (uint)int.Parse("0");

                                        bool Success_GP0_Enable = PL2303_GP0_Enable(hCOM, 1);
                                        bool Success_GP0_SetValue = PL2303_GP0_SetValue(hCOM, val);

                                        bool Success_GP1_Enable = PL2303_GP1_Enable(hCOM, 1);
                                        bool Success_GP1_SetValue = PL2303_GP1_SetValue(hCOM, val);

                                        PowerState = false;

                                        pictureBox_AcPower.Image = Properties.Resources.OFF;
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////SAVE LOG//////////////////
                                    if (compare_number[i] % compare_num == 0 && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Savelog", "") == "1")
                                    {
                                        string fName = "";

                                        // 讀取ini中的路徑
                                        fName = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
                                        string t = fName + "\\_SaveLog2_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + label_LoopNumber_Value.Text + ".txt";

                                        StreamWriter MYFILE = new StreamWriter(t, false, Encoding.ASCII);
                                        MYFILE.Write(textBox2.Text);
                                        MYFILE.Close();
                                        Txtbox2("", textBox2);
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////STOP//////////////////
                                    if (compare_number[i] % compare_num == 0 && ini12.INIRead(Global.MainSettingPath, "LogSearch", "Stop", "") == "1")
                                    {
                                        button_Start.PerformClick();
                                    }
                                    ////////////////////////////////////////////////////////////////////////////////////////////////SCHEDULE//////////////////
                                    if (compare_number[i] % compare_num == 0)
                                    {
                                        int keyword_numer = i + 1;
                                        switch (keyword_numer)
                                        {
                                            case 1:
                                                Global.keyword_1 = "true";
                                                break;

                                            case 2:
                                                Global.keyword_2 = "true";
                                                break;

                                            case 3:
                                                Global.keyword_3 = "true";
                                                break;

                                            case 4:
                                                Global.keyword_4 = "true";
                                                break;

                                            case 5:
                                                Global.keyword_5 = "true";
                                                break;

                                            case 6:
                                                Global.keyword_6 = "true";
                                                break;

                                            case 7:
                                                Global.keyword_7 = "true";
                                                break;

                                            case 8:
                                                Global.keyword_8 = "true";
                                                break;

                                            case 9:
                                                Global.keyword_9 = "true";
                                                break;

                                            case 10:
                                                Global.keyword_10 = "true";
                                                break;
                                        }
                                    }
                                }
                            }
                            //textBox2.AppendText(my_string + '\r');
                            my_string = "";
                        }
                        #endregion

                        else
                        {
                            my_string = my_string + Keyword_SerialPort_2_temp_char;
                        }
                    }
                    else
                    {

                        if ((Keyword_SerialPort_2_temp_char == '\n'))
                        {
                            //textBox2.AppendText(my_string + '\n');
                            my_string = "";
                        }
                        else if ((Keyword_SerialPort_2_temp_char == '\r'))
                        {
                            //textBox2.AppendText(my_string + '\r');
                            my_string = "";
                        }
                        else
                        {
                            my_string = my_string + Keyword_SerialPort_2_temp_char;
                        }
                    }
                }
                Thread.Sleep(500);
            }
        }
        #endregion

        #region -- 跑Schedule的指令集 --
        private void MyRunCamd()
        {
            int sRepeat = 0, stime = 0, SysDelay = 0;

            Global.Loop_Number = 1;
            Global.Break_Out_Schedule = 0;
            Global.Pass_Or_Fail = "PASS";

            label_TestTime_Value.Text = "0d 0h 0m 0s";
            TestTime = 0;

            for (int l = 0; l <= Global.Schedule_Loop; l++)
            {
                Global.NGValue[l] = 0;
                Global.NGRateValue[l] = 0;
            }

            #region -- 匯出比對結果到CSV & EXCEL --
            if (ini12.INIRead(Global.MainSettingPath, "Record", "CompareChoose", "") == "1" && StartButtonPressed == true)
            {
                string compareFolder = ini12.INIRead(Global.MainSettingPath, "Record", "VideoPath", "") + "\\" + "Schedule" + Global.Schedule_Number + "_Original_" + DateTime.Now.ToString("yyyyMMddHHmmss");

                if (Directory.Exists(compareFolder))
                {

                }
                else
                {
                    Directory.CreateDirectory(compareFolder);
                    ini12.INIWrite(Global.MainSettingPath, "Record", "ComparePath", compareFolder);
                }
                // 匯出csv記錄檔
                string csvFile = ini12.INIRead(Global.MainSettingPath, "Record", "ComparePath", "") + "\\SimilarityReport_" + Global.Schedule_Number + ".csv";
                StreamWriter sw = new StreamWriter(csvFile, false, Encoding.UTF8);
                sw.WriteLine("Target, Source, Similarity, Sub-NG count, NGRate, Result");

                sw.Dispose();
                /*
                                #region Excel function
                                // 匯出excel記錄檔
                                Global.excel_Num = 1;
                                string excelFile = ini12.INIRead(sPath, "Record", "ComparePath", "") + "\\SimilarityReport_" + Global.Schedule_Num;

                                excelApp = new Excel.Application();
                                //excelApp.Visible = true;
                                excelApp.DisplayAlerts = false;
                                excelApp.Workbooks.Add(Type.Missing);
                                wBook = excelApp.Workbooks[1];
                                wBook.Activate();
                                excelstat = true;

                                try
                                {
                                    // 引用第一個工作表
                                    wSheet = (Excel._Worksheet)wBook.Worksheets[1];

                                    // 命名工作表的名稱
                                    wSheet.Name = "全部測試資料";

                                    // 設定工作表焦點
                                    wSheet.Activate();

                                    excelApp.Cells[1, 1] = "All Data";

                                    // 設定第1列資料
                                    excelApp.Cells[1, 1] = "Target";
                                    excelApp.Cells[1, 2] = "Source";
                                    excelApp.Cells[1, 3] = "Similarity";
                                    excelApp.Cells[1, 4] = "Sub-NG count";
                                    excelApp.Cells[1, 5] = "NGRate";
                                    excelApp.Cells[1, 6] = "Result";
                                    // 設定第1列顏色
                                    wRange = wSheet.Range[wSheet.Cells[1, 1], wSheet.Cells[1, 6]];
                                    wRange.Select();
                                    wRange.Font.Color = ColorTranslator.ToOle(Color.White);
                                    wRange.Interior.Color = ColorTranslator.ToOle(Color.DimGray);
                                    wRange.AutoFilter(1, Type.Missing, Microsoft.Office.Interop.Excel.XlAutoFilterOperator.xlAnd, Type.Missing);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("產生報表時出錯！" + Environment.NewLine + ex.Message);
                                }
                                #endregion
                */
            }
            #endregion

            for (int j = 1; j < Global.Schedule_Loop + 1; j++)
            {
                Global.caption_Num = 0;
                UpdateUI(j.ToString(), label_LoopNumber_Value);
                ini12.INIWrite(Global.MailSettingPath, "Data Info", "CreateTime", string.Format("{0:R}", DateTime.Now));

                lock (this)
                {
                    for (Global.Scheduler_Row = 0; Global.Scheduler_Row < DataGridView_Schedule.Rows.Count - 1; Global.Scheduler_Row++)
                    {
                        IO_INPUT();//先讀取IO值，避免schedule第一行放IO CMD會出錯//

                        Global.Schedule_Step = Global.Scheduler_Row;

                        if (StartButtonPressed == false)
                        {
                            j = Global.Schedule_Loop;
                            UpdateUI(j.ToString(), label_LoopNumber_Value);
                            break;
                        }

                        GridUI(Global.Scheduler_Row.ToString(), DataGridView_Schedule);//控制Datagridview highlight//
                        Gridscroll(Global.Scheduler_Row.ToString(), DataGridView_Schedule);//控制Datagridview scollbar//

                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[1].Value.ToString() != "")
                            stime = int.Parse(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[1].Value.ToString()); // 次數
                        else
                            stime = 1;

                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[2].Value.ToString() != "")
                            sRepeat = int.Parse(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[2].Value.ToString()); // 停止時間
                        else
                            sRepeat = 0;

                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[9].Value.ToString() != "")
                            SysDelay = int.Parse(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[9].Value.ToString()); // 指令停止時間
                        else
                            SysDelay = 0;

                        #region -- _cmd --
                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_cmd")
                        {
                            #region -- 拍照 --
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[3].Value.ToString() == "_shot")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")
                                {
                                    Global.caption_Num++;
                                    if (Global.Loop_Number == 1)
                                        Global.caption_Sum = Global.caption_Num;
                                    Jes();
                                    label_Command.Text = "Take Picture";
                                }
                                else
                                {
                                    button_Start.PerformClick();
                                }
                            }
                            #endregion

                            #region -- 錄影 --
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[4].Value.ToString() == "_start")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")
                                {
                                    if (VideoRecording == false)
                                    {
                                        Mysvideo(); // 開新檔
                                        VideoRecording = true;
                                        Thread oThreadC = new Thread(new ThreadStart(MySrtCamd));
                                        oThreadC.Start();
                                    }
                                    label_Command.Text = "Start Recording";
                                }
                                else
                                {
                                    MessageBox.Show("Camera not exist", "Camera Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    button_Start.PerformClick();
                                }
                            }

                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[4].Value.ToString() == "_stop")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")
                                {
                                    if (VideoRecording == true)       //判斷是不是正在錄影
                                    {
                                        VideoRecording = false;
                                        Mysstop();      //先將先前的關掉
                                    }
                                    label_Command.Text = "Stop Recording";
                                }
                                else
                                {
                                    MessageBox.Show("Camera not exist", "Camera Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    button_Start.PerformClick();
                                }
                            }
                            #endregion

                            #region -- AC SWITCH OLD --
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_on")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP0_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("1");
                                        bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                        if (bSuccess)
                                        {
                                            {
                                                PowerState = true;
                                                pictureBox_AcPower.Image = Properties.Resources.ON;
                                                label_Command.Text = "AC ON";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Autobox Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_off")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP0_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("0");
                                        bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                        if (bSuccess)
                                        {
                                            {
                                                PowerState = false;
                                                pictureBox_AcPower.Image = Properties.Resources.OFF;
                                                label_Command.Text = "AC OFF";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Autobox Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            #endregion

                            #region -- AC SWITCH --
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_AC1_ON")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP0_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("1");
                                        bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                        if (bSuccess)
                                        {
                                            {
                                                PowerState = true;
                                                pictureBox_AcPower.Image = Properties.Resources.ON;
                                                label_Command.Text = "AC1 => POWER ON";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Autobox Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_AC1_OFF")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP0_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("0");
                                        bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                                        if (bSuccess)
                                        {
                                            {
                                                PowerState = false;
                                                pictureBox_AcPower.Image = Properties.Resources.OFF;
                                                label_Command.Text = "AC1 => POWER OFF";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Autobox Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_AC2_ON")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP1_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("1");
                                        bool bSuccess = PL2303_GP1_SetValue(hCOM, val);
                                        if (bSuccess)
                                        {
                                            {
                                                PowerState = true;
                                                pictureBox_AcPower.Image = Properties.Resources.ON;
                                                label_Command.Text = "AC2 => POWER ON";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Autobox Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_AC2_OFF")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP1_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("0");
                                        bool bSuccess = PL2303_GP1_SetValue(hCOM, val);
                                        if (bSuccess)
                                        {
                                            {
                                                PowerState = false;
                                                pictureBox_AcPower.Image = Properties.Resources.OFF;
                                                label_Command.Text = "AC2 => POWER OFF";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Autobox Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            #endregion

                            #region -- USB SWITCH --
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_USB1_TV")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP2_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("1");
                                        bool bSuccess = PL2303_GP2_SetValue(hCOM, val);
                                        if (bSuccess == true)
                                        {
                                            {
                                                USBState = false;
                                                label_Command.Text = "USB1 => TV";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_USB1_PC")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP2_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("0");
                                        bool bSuccess = PL2303_GP2_SetValue(hCOM, val);
                                        if (bSuccess == true)
                                        {
                                            {
                                                USBState = true;
                                                label_Command.Text = "USB1 => PC";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_USB2_TV")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP3_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("1");
                                        bool bSuccess = PL2303_GP3_SetValue(hCOM, val);
                                        if (bSuccess == true)
                                        {
                                            {
                                                USBState = false;
                                                label_Command.Text = "USB2 => TV";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString() == "_USB2_PC")
                            {
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    if (PL2303_GP3_Enable(hCOM, 1) == true)
                                    {
                                        uint val = (uint)int.Parse("0");
                                        bool bSuccess = PL2303_GP3_SetValue(hCOM, val);
                                        if (bSuccess == true)
                                        {
                                            {
                                                USBState = true;
                                                label_Command.Text = "USB2 => PC";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region -- COM PORT --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_log1")
                        {
                            if (ini12.INIRead(Global.MainSettingPath, "Comport", "Checked", "") == "1")
                            {
                                switch (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString())
                                {
                                    case "_clear":
                                        textBox1.Text = ""; //清除textbox1
                                        break;

                                    case "_save":
                                        Rs232save(); //存檔rs232
                                        break;

                                    default:
                                        //byte[] data = Encoding.Unicode.GetBytes(DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString());
                                        // string str = Convert.ToString(data);
                                        serialPort1.WriteLine(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString()); //發送數據 Rs232
                                        break;
                                }
                                label_Command.Text = "(" + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() + ") " + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString();
                            }
                        }

                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_log2")
                        {
                            if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1")
                            {
                                switch (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString())
                                {
                                    case "_clear":
                                        textBox2.Text = ""; //清除textbox2
                                        break;

                                    case "_save":
                                        ExtRs232save(); //存檔rs232
                                        break;

                                    default:
                                        //byte[] data = Encoding.Unicode.GetBytes(DataGridView1.Rows[Global.Scheduler_Row].Cells[5].Value.ToString());
                                        // string str = Convert.ToString(data);
                                        serialPort2.WriteLine(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString()); //發送數據 Rs232
                                        break;
                                }
                                label_Command.Text = "(" + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() + ") " + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString();
                            }
                        }
                        #endregion

                        #region -- Astro Timing --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_astro")
                        {
                            try
                            {
                                // Astro指令
                                byte[] startbit = new byte[7] { 0x05, 0x24, 0x20, 0x02, 0xfd, 0x24, 0x20 };
                                serialPort1.Write(startbit, 0, 7);

                                // Astro指令檔案匯入
                                string xmlfile = ini12.INIRead(Global.MainSettingPath, "Record", "Generator", "");
                                if (System.IO.File.Exists(xmlfile) == true)
                                {
                                    var allTiming = XDocument.Load(xmlfile).Root.Element("Generator").Elements("Device");
                                    foreach (var generator in allTiming)
                                    {
                                        if (generator.Attribute("Name").Value == "_astro")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[7].Value.ToString() == generator.Element("Timing").Value)
                                            {
                                                string[] timestrs = generator.Element("Signal").Value.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                                                byte[] timebit1 = Encoding.ASCII.GetBytes(timestrs[0]);
                                                byte[] timebit2 = Encoding.ASCII.GetBytes(timestrs[1]);
                                                byte[] timebit3 = Encoding.ASCII.GetBytes(timestrs[2]);
                                                byte[] timebit4 = Encoding.ASCII.GetBytes(timestrs[3]);
                                                byte[] timebit = new byte[4] { timebit1[1], timebit2[1], timebit3[1], timebit4[1] };
                                                serialPort1.Write(timebit, 0, 4);
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Content include other signal", "Astro Signal Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Signal Generator not exist", "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                byte[] endbit = new byte[3] { 0x2c, 0x31, 0x03 };
                                serialPort1.Write(endbit, 0, 3);
                                label_Command.Text = "(" + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() + ") " + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[7].Value.ToString();
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show("Transmit the Astro command fail ! \nPlease check the serialPort1 setting and voltage equal 3.3V.", Ex.Message.ToString());
                            }
                        }
                        #endregion

                        #region -- Quantum Timing --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_quantum")
                        {
                            try
                            {
                                // Quantum指令檔案匯入
                                string xmlfile = ini12.INIRead(Global.MainSettingPath, "Record", "Generator", "");
                                if (System.IO.File.Exists(xmlfile) == true)
                                {
                                    var allTiming = XDocument.Load(xmlfile).Root.Element("Generator").Elements("Device");
                                    foreach (var generator in allTiming)
                                    {
                                        if (generator.Attribute("Name").Value == "_quantum")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[7].Value.ToString() == generator.Element("Timing").Value)
                                            {
                                                serialPort1.WriteLine(generator.Element("Signal").Value + "\r");
                                                serialPort1.WriteLine("ALLU" + "\r");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Content include other signal", "Quantum Signal Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Signal Generator not exist", "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                                switch (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[8].Value.ToString())
                                {
                                    case "RGB":
                                        // RGB mode
                                        serialPort1.WriteLine("AVST 0" + "\r");
                                        serialPort1.WriteLine("DVST 10" + "\r");
                                        serialPort1.WriteLine("FMTU" + "\r");
                                        break;
                                    case "YCbCr":
                                        // YCbCr mode
                                        serialPort1.WriteLine("AVST 0" + "\r");
                                        serialPort1.WriteLine("DVST 14" + "\r");
                                        serialPort1.WriteLine("FMTU" + "\r");
                                        break;
                                    case "xvYCC":
                                        // xvYCC mode
                                        serialPort1.WriteLine("AVST 0" + "\r");
                                        serialPort1.WriteLine("DVST 17" + "\r");
                                        serialPort1.WriteLine("FMTU" + "\r");
                                        break;
                                    case "4:4:4":
                                        // 4:4:4
                                        serialPort1.WriteLine("DVSM 4" + "\r");
                                        serialPort1.WriteLine("FMTU" + "\r");
                                        break;
                                    case "4:2:2":
                                        // 4:2:2
                                        serialPort1.WriteLine("DVSM 2" + "\r");
                                        serialPort1.WriteLine("FMTU" + "\r");
                                        break;
                                    case "8bits":
                                        // 8bits
                                        serialPort1.WriteLine("NBPC 8" + "\r");
                                        serialPort1.WriteLine("FMTU" + "\r");
                                        break;
                                    case "10bits":
                                        // 10bits
                                        serialPort1.WriteLine("NBPC 10" + "\r");
                                        serialPort1.WriteLine("FMTU" + "\r");
                                        break;
                                    case "12bits":
                                        // 12bits
                                        serialPort1.WriteLine("NBPC 12" + "\r");
                                        serialPort1.WriteLine("FMTU" + "\r");
                                        break;
                                    default:
                                        break;
                                }
                                label_Command.Text = "(" + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() + ") " + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[7].Value.ToString() + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[8].Value.ToString();
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show("Transmit the Quantum command fail ! \nPlease check the serialPort1 setting and voltage equal 3.3V.", Ex.Message.ToString());
                            }
                        }
                        #endregion

                        #region -- Dektec --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_dektec")
                        {
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[4].Value.ToString() == "_start")
                            {
                                string StreamName = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString();
                                string TvSystem = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[7].Value.ToString();
                                string Freq = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[8].Value.ToString();
                                string arguments = Application.StartupPath + @"\\DektecPlayer\\" + StreamName + " " +
                                                   "-mt " + TvSystem + " " +
                                                   "-mf " + Freq + " " +
                                                   "-r 0 " +
                                                   "-l 0";

                                Console.WriteLine(arguments);
                                System.Diagnostics.Process Dektec = new System.Diagnostics.Process();
                                Dektec.StartInfo.FileName = Application.StartupPath + @"\\DektecPlayer\\DtPlay.exe";
                                Dektec.StartInfo.UseShellExecute = false;
                                Dektec.StartInfo.RedirectStandardInput = true;
                                Dektec.StartInfo.RedirectStandardOutput = true;
                                Dektec.StartInfo.RedirectStandardError = true;
                                Dektec.StartInfo.CreateNoWindow = true;

                                Dektec.StartInfo.Arguments = arguments;
                                Dektec.Start();
                                label_Command.Text = "(" + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() + ") " + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[6].Value.ToString();
                            }

                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[4].Value.ToString() == "_stop")
                            {
                                CloseDtplay();
                            }
                        }
                        #endregion

                        #region -- 命令提示 --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_DOS")
                        {
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() != "")
                            {
                                string Command = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString();

                                System.Diagnostics.Process p = new Process();
                                p.StartInfo.FileName = "cmd.exe";
                                p.StartInfo.WorkingDirectory = ini12.INIRead(Global.MainSettingPath, "Device", "DOS", "");
                                p.StartInfo.UseShellExecute = false;
                                p.StartInfo.RedirectStandardInput = true;
                                p.StartInfo.RedirectStandardOutput = true;
                                p.StartInfo.RedirectStandardError = true;
                                p.StartInfo.CreateNoWindow = true; //不跳出cmd視窗
                                string strOutput = null;

                                try
                                {
                                    p.Start();
                                    p.StandardInput.WriteLine(Command);
                                    label_Command.Text = "DOS CMD_" + DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString();
                                    //p.StandardInput.WriteLine("exit");
                                    //strOutput = p.StandardOutput.ReadToEnd();//匯出整個執行過程
                                    //p.WaitForExit();
                                    //p.Close();
                                }
                                catch (Exception e)
                                {
                                    strOutput = e.Message;
                                }
                            }
                        }
                        #endregion

                        #region -- GPIO_INPUT_OUTPUT --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_IO_Input")
                        {
                            IO_INPUT();
                        }

                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_IO_Output")
                        {
                            //string GPIO = "01010101";
                            string GPIO = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[1].Value.ToString();
                            byte GPIO_B = Convert.ToByte(GPIO, 2);
                            MyBlueRat.Set_GPIO_Output(GPIO_B);
                        }
                        #endregion

                        #region -- MonkeyTest --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_MonkeyTest")
                        {
                            Add_ons MonkeyTest = new Add_ons();
                            MonkeyTest.MonkeyTest();
                            MonkeyTest.CreateExcelFile();
                        }
                        #endregion

                        #region -- Factory Command 控制 --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_SXP")
                        {
                            if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1" &&
                                DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_save")
                            {
                                string fName = "";

                                fName = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
                                string t = fName + "\\_Log2_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + label_LoopNumber_Value.Text + ".txt";

                                StreamWriter MYFILE = new StreamWriter(t, false, Encoding.ASCII);
                                MYFILE.WriteLine(textBox2.Text);
                                MYFILE.Close();

                                Txtbox2("", textBox2);
                            }

                            if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1" &&
                                DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() != "_save")
                            {
                                try
                                {
                                    string str = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString();
                                    byte[] bytes = str.Split(' ').Select(s => Convert.ToByte(s, 16)).ToArray();
                                    label_Command.Text = "SXP CMD";
                                    serialPort2.Write(bytes, 0, bytes.Length);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Check your SerialPort2 setting.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
                                Global.Break_Out_Schedule = 1;
                            }
                        }
                        #endregion

                        #region -- IO CMD --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Length >= 7 && DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(0, 3) == "_PA" ||
                             DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Length >= 7 && DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(0, 3) == "_PB")
                        {
                            {
                                switch (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(3, 2))
                                {
                                    #region -- PA10 --
                                    case "10":
                                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "0" &&
                                            Global.IO_INPUT.Substring(10, 1) == "0")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PA10_0_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                            {
                                                IO_CMD();
                                            }     
                                        }
                                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "1" &&
                                            Global.IO_INPUT.Substring(10, 1) == "1")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PA10_1_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                            {
                                                IO_CMD();
                                            }
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        break;
                                    #endregion

                                    #region -- PA11 --
                                    case "11":
                                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "0" &&
                                            Global.IO_INPUT.Substring(8, 1) == "0")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PA11_0_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                                IO_CMD();
                                        }
                                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "1" &&
                                            Global.IO_INPUT.Substring(8, 1) == "1")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PA11_1_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                                IO_CMD();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        break;
                                    #endregion

                                    #region -- PA14 --
                                    case "14":
                                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "0" &&
                                            Global.IO_INPUT.Substring(6, 1) == "0")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PA14_0_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                                IO_CMD();
                                        }
                                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "1" &&
                                            Global.IO_INPUT.Substring(6, 1) == "1")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PA14_1_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                                IO_CMD();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        break;
                                    #endregion

                                    #region -- PA15 --
                                    case "15":
                                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "0" &&
                                            Global.IO_INPUT.Substring(4, 1) == "0")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PA15_0_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                                IO_CMD();
                                        }
                                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "1" &&
                                            Global.IO_INPUT.Substring(4, 1) == "1")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PA15_1_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                                IO_CMD();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        break;
                                    #endregion
                                        
                                    #region -- PB01 --
                                    case "01":
                                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "0" &&
                                            Global.IO_INPUT.Substring(2, 1) == "0")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PB1_0_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }

                                            else
                                                IO_CMD();
                                        }
                                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "1" &&
                                            Global.IO_INPUT.Substring(2, 1) == "1")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PB1_1_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                                IO_CMD();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        break;
                                    #endregion

                                    #region -- PB07 --
                                    case "07":
                                        if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "0" &&
                                            Global.IO_INPUT.Substring(0, 1) == "0")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PB7_0_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                                IO_CMD();
                                        }
                                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(6, 1) == "1" &&
                                            Global.IO_INPUT.Substring(0, 1) == "1")
                                        {
                                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_accumulate")
                                            {
                                                Global.IO_PB7_1_COUNT++;
                                                label_Command.Text = "IO CMD_ACCUMULATE";
                                            }
                                            else
                                                IO_CMD();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        break;
                                        #endregion
                                }
                            }
                        }
                        #endregion

                        #region -- Audio Debounce --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString() == "_audio_debounce")
                        {
                            bool Debounce_Time_PB1, Debounce_Time_PB7;
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[2].Value.ToString() != "" )
                            {
                                MyBlueRat.Set_Input_GPIO_Low_Debounce_Time_PB1(Convert.ToUInt16(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[2].Value.ToString()));
                                MyBlueRat.Set_Input_GPIO_Low_Debounce_Time_PB7(Convert.ToUInt16(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[2].Value.ToString()));
                                Debounce_Time_PB1 = MyBlueRat.Set_Input_GPIO_Low_Debounce_Time_PB1(Convert.ToUInt16(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[2].Value.ToString()));
                                Debounce_Time_PB7 = MyBlueRat.Set_Input_GPIO_Low_Debounce_Time_PB7(Convert.ToUInt16(DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[2].Value.ToString()));
                                
                            }
                            else
                            {
                                MyBlueRat.Set_Input_GPIO_Low_Debounce_Time_PB1();
                                MyBlueRat.Set_Input_GPIO_Low_Debounce_Time_PB7();
                                Debounce_Time_PB1 = MyBlueRat.Set_Input_GPIO_Low_Debounce_Time_PB1();
                                Debounce_Time_PB7 = MyBlueRat.Set_Input_GPIO_Low_Debounce_Time_PB7();
                                
                            }
                        }
                        #endregion

                        #region -- Keyword Search --
                        else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Length >= 9 && 
                                 DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(0, 9) == "_keyword_")
                        {
                            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Length == 11)
                            {
                                if (Global.keyword_10 == "true")
                                {
                                    KeywordCommand();
                                }
                                else
                                {
                                    SysDelay = 0;
                                }
                                Global.keyword_10 = "false";
                            }
                            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Length == 10)
                            {
                                switch (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString().Substring(9, 1))
                                {
                                    case "1":
                                        if (Global.keyword_1 == "true")
                                        {
                                            KeywordCommand();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        Global.keyword_1 = "false";
                                        break;

                                    case "2":
                                        if (Global.keyword_2 == "true")
                                        {
                                            KeywordCommand();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        Global.keyword_2 = "false";
                                        break;

                                    case "3":
                                        if (Global.keyword_3 == "true")
                                        {
                                            KeywordCommand();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        Global.keyword_3 = "false";
                                        break;

                                    case "4":
                                        if (Global.keyword_4 == "true")
                                        {
                                            KeywordCommand();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        Global.keyword_4 = "false";
                                        break;

                                    case "5":
                                        if (Global.keyword_5 == "true")
                                        {
                                            KeywordCommand();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        Global.keyword_5 = "false";
                                        break;

                                    case "6":
                                        if (Global.keyword_6 == "true")
                                        {
                                            KeywordCommand();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        Global.keyword_6 = "false";
                                        break;

                                    case "7":
                                        if (Global.keyword_7 == "true")
                                        {
                                            KeywordCommand();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        Global.keyword_7 = "false";
                                        break;

                                    case "8":
                                        if (Global.keyword_8 == "true")
                                        {
                                            KeywordCommand();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        Global.keyword_8 = "false";
                                        break;

                                    case "9":
                                        if (Global.keyword_9 == "true")
                                        {
                                            KeywordCommand();
                                        }
                                        else
                                        {
                                            SysDelay = 0;
                                        }
                                        Global.keyword_9 = "false";
                                        break;

                                    default:
                                        Console.WriteLine("keyword not found_schedule");
                                        break;
                                }
                            }
                        }
                        #endregion

                        #region -- 遙控器指令 --
                        else
                        {
                            for (int k = 0; k < stime; k++)
                            {
                                label_Command.Text = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString();
                                if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1")
                                {
                                    //執行小紅鼠指令
                                    Autocommand_RedRat("Form1", DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString());
                                }
                                else if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                                {
                                    //執行小藍鼠指令
                                    Autocommand_BlueRat("Form1", DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString());
                                }
                                else
                                {
                                    MessageBox.Show("Please connect AutoBox or RedRat!", "Redrat Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    button_Start.PerformClick();
                                }
                                videostring = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[0].Value.ToString();
                                RedRatDBViewer_Delay(sRepeat);
                            }
                        }
                        #endregion
                        
                        //Thread MyExportText = new Thread(new ThreadStart(MyExportCamd));
                        //MyExportText.Start();
                         
                        ini12.INIWrite(Global.MailSettingPath, "Data Info", "CloseTime", string.Format("{0:R}", DateTime.Now));

                        if (Global.Break_Out_Schedule == 1)//定時器時間到跳出迴圈//
                        {
                            j = Global.Schedule_Loop;
                            UpdateUI(j.ToString(), label_LoopNumber_Value);
                            break;
                        }

                        Nowpoint = DataGridView_Schedule.Rows[Global.Scheduler_Row].Index;
                        if (Breakfunction == true)
                        {
                            if (Breakpoint == Nowpoint)
                            {
                                button_Pause.PerformClick();
                            }
                        }

                        if (Pause == true)//如果按下暫停鈕//
                        {
                            timer1.Stop();
                            SchedulePause.WaitOne();
                        }
                        else
                        {
                            RedRatDBViewer_Delay(SysDelay);
                        }

                        #region -- 足跡模式 --
                        //假如足跡模式打開則會append足跡上去
                        if (ini12.INIRead(Global.MainSettingPath, "Record", "Footprint Mode", "") == "1" && SysDelay != 0)
                        {
                            //檔案不存在則加入標題
                            if (File.Exists(Application.StartupPath + @"\StepRecord.csv") == false)
                            {
                                File.AppendAllText(Application.StartupPath + @"\StepRecord.csv", "LOOP,TIME,COMMAND,bit_0,bit_1,bit_2,bit_3,bit_4,bit_5," +
                                    "PA10_0,PA10_1," +
                                    "PA11_0,PA11_1," +
                                    "PA14_0,PA14_1," +
                                    "PA15_0,PA15_1," +
                                    "PB1_0,PB1_1," +
                                    "PB7_0,PB7_1" + 
                                    Environment.NewLine);

                                File.AppendAllText(Application.StartupPath + @"\StepRecord.csv",
                                Global.Loop_Number + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + label_Command.Text + "," + Global.IO_INPUT +
                                "," + Global.IO_PA10_0_COUNT + "," + Global.IO_PA10_1_COUNT +
                                "," + Global.IO_PA11_0_COUNT + "," + Global.IO_PA11_1_COUNT +
                                "," + Global.IO_PA14_0_COUNT + "," + Global.IO_PA14_1_COUNT +
                                "," + Global.IO_PA15_0_COUNT + "," + Global.IO_PA15_1_COUNT +
                                "," + Global.IO_PB1_0_COUNT + "," + Global.IO_PB1_1_COUNT +
                                "," + Global.IO_PB7_0_COUNT + "," + Global.IO_PB7_1_COUNT + Environment.NewLine);
                            }
                            else
                            {
                                File.AppendAllText(Application.StartupPath + @"\StepRecord.csv",
                                Global.Loop_Number + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "," + label_Command.Text + "," + Global.IO_INPUT + 
                                "," + Global.IO_PA10_0_COUNT + "," + Global.IO_PA10_1_COUNT +
                                "," + Global.IO_PA11_0_COUNT + "," + Global.IO_PA11_1_COUNT +
                                "," + Global.IO_PA14_0_COUNT + "," + Global.IO_PA14_1_COUNT +
                                "," + Global.IO_PA15_0_COUNT + "," + Global.IO_PA15_1_COUNT +
                                "," + Global.IO_PB1_0_COUNT + "," + Global.IO_PB1_1_COUNT +
                                "," + Global.IO_PB7_0_COUNT + "," + Global.IO_PB7_1_COUNT + Environment.NewLine);
                            }
                        }
                        #endregion
                    }

                    #region -- Import database --
                    if (ini12.INIRead(Global.MainSettingPath, "Record", "ImportDB", "") == "1")
                    {
                        string SQLServerURL = "server=192.168.56.2\\ATMS;database=Autobox;uid=AS;pwd=AS";

                        SqlConnection conn = new SqlConnection(SQLServerURL);
                        conn.Open();
                        SqlCommand s_com = new SqlCommand
                        {
                            //s_com.CommandText = "select * from Autobox.dbo.testresult";
                            CommandText = "insert into Autobox.dbo.testresult (ab_p_id, ab_result, ab_create, ab_time, ab_loop, ab_loop_time, ab_loop_step, ab_root, ab_user) values ('" + label_LoopNumber_Value.Text + "', 'Pass', '" + DateTime.Now.ToString("HH:mm:ss") + "', '" + label_LoopNumber_Value.Text + "', 1, 21000, 2, 0, 'Joseph')",
                            //s_com.CommandText = "update Autobox.dbo.testresult (ab_result, ab_close, ab_time, ab_loop, ab_root, ab_user) values ('Pass', '" + DateTime.Now.ToString("HH:mm:ss") + "', '" + label1.Text + "', 1, 21000, 'Joseph')";
                            //s_com.CommandText = "Update Autobox.dbo.testresult set ab_result='Pass', ab_close='2014/5/21 15:49:35', ab_time=600000, ab_loop=25, ab_root=0 where ab_num=2";
                            //s_com.CommandText = "Update Autobox.dbo.testresult set ab_result='NG', ab_close='2014/5/21 15:59:35', ab_time=1200000, ab_loop=50, ab_root=1 where ab_num=3";

                            Connection = conn
                        };

                        SqlDataReader s_read = s_com.ExecuteReader();
                        try
                        {
                            while (s_read.Read())
                            {
                                Console.WriteLine("Log> Find {0}", s_read["ab_p_id"].ToString());
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        s_read.Close();

                        conn.Close();
                    }
                    #endregion
                }
                Global.Loop_Number++;
            }

            #region -- Video Record --
            if (ini12.INIRead(Global.MainSettingPath, "Record", "EachVideo", "") == "1")
            {
                if (StartButtonPressed == true)
                {
                    if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")
                    {
                        if (VideoRecording == false)
                        {
                            label_Command.Text = "Record Video...";
                            Thread.Sleep(1500);
                            Mysvideo(); // 開新檔
                            VideoRecording = true;
                            Thread oThreadC = new Thread(new ThreadStart(MySrtCamd));
                            oThreadC.Start();
                            Thread.Sleep(60000); // 錄影60秒

                            VideoRecording = false;
                            Mysstop();
                            oThreadC.Abort();
                            Thread.Sleep(1500);
                            label_Command.Text = "Vdieo recording completely.";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Camera not exist", "Camera Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            #endregion
            
            /*
            #region Excel function
            if (ini12.INIRead(sPath, "Record", "CompareChoose", "") == "1" && excelstat == true)
            {
                string excelFile = ini12.INIRead(sPath, "Record", "ComparePath", "") + "\\SimilarityReport_" + Global.Schedule_Num;

                try
                {
                    //另存活頁簿
                    wBook.SaveAs(excelFile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    Console.WriteLine("儲存文件於 " + Environment.NewLine + excelFile);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("儲存檔案出錯，檔案可能正在使用" + Environment.NewLine + ex.Message);
                }

                //關閉活頁簿
                //wBook.Close(false, Type.Missing, Type.Missing);

                //關閉Excel
                excelApp.Quit();

                //釋放Excel資源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                excelApp = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wBook);
                wBook = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wSheet);
                wSheet = null;
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wRange);
                wRange = null;

                GC.Collect();
                excelstat = false;

                //Console.Read();

                CloseExcel();
            }
            #endregion

            if (Global.loop_Num < 3)
            {
            }
            else
            {
                if (StartBtnShow_STOP == false)
                    Global.loop_Num--;
                Thread MyCompareThread = new Thread(new ThreadStart(MyCompareCamd));
                MyCompareThread.Start();
                RedratLable.Text = "Start Compare Picture...";
                Thread.Sleep(Global.loop_Num * Global.caption_Sum * 2000);
            }
            */

            #region -- schedule 切換 --
            if (StartButtonPressed != false)
            {
                if (Global.Schedule_2_Exist == 1 && Global.Schedule_Number == 1)
                {
                    if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "") == "1" && StartButtonPressed == true)       //定時器時間未到進入等待<<<<<<<<<<<<<<
                    {
                        if (Global.Break_Out_Schedule == 0)
                        {
                            while (ini12.INIRead(Global.MainSettingPath, "Schedule2", "Timer", "") != TimeLabel2.Text)
                            {
                                ScheduleWait.WaitOne();
                            }
                            ScheduleWait.Set();
                        }
                    }       //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    ini12.INIWrite(Global.MainSettingPath, "Schedule1", "OnTimeStart", "0");
                    button_Schedule2.PerformClick();
                    MyRunCamd();
                }
                else if (
                    Global.Schedule_3_Exist == 1 && Global.Schedule_Number == 1 ||
                    Global.Schedule_3_Exist == 1 && Global.Schedule_Number == 2)
                {
                    if (ini12.INIRead(Global.MainSettingPath, "Schedule3", "OnTimeStart", "") == "1" && StartButtonPressed == true)
                    {
                        if (Global.Break_Out_Schedule == 0)
                        {
                            while (ini12.INIRead(Global.MainSettingPath, "Schedule3", "Timer", "") != TimeLabel2.Text)
                            {
                                ScheduleWait.WaitOne();
                            }
                            ScheduleWait.Set();
                        }
                    }
                    ini12.INIWrite(Global.MainSettingPath, "Schedule2", "OnTimeStart", "0");
                    button_Schedule3.PerformClick();
                    MyRunCamd();
                }
                else if (
                    Global.Schedule_4_Exist == 1 && Global.Schedule_Number == 1 ||
                    Global.Schedule_4_Exist == 1 && Global.Schedule_Number == 2 ||
                    Global.Schedule_4_Exist == 1 && Global.Schedule_Number == 3)
                {
                    if (ini12.INIRead(Global.MainSettingPath, "Schedule4", "OnTimeStart", "") == "1" && StartButtonPressed == true)
                    {
                        if (Global.Break_Out_Schedule == 0)
                        {
                            while (ini12.INIRead(Global.MainSettingPath, "Schedule4", "Timer", "") != TimeLabel2.Text)
                            {
                                ScheduleWait.WaitOne();
                            }
                            ScheduleWait.Set();
                        }
                    }
                    ini12.INIWrite(Global.MainSettingPath, "Schedule3", "OnTimeStart", "0");
                    button_Schedule4.PerformClick();
                    MyRunCamd();
                }
                else if (
                    Global.Schedule_5_Exist == 1 && Global.Schedule_Number == 1 ||
                    Global.Schedule_5_Exist == 1 && Global.Schedule_Number == 2 ||
                    Global.Schedule_5_Exist == 1 && Global.Schedule_Number == 3 ||
                    Global.Schedule_5_Exist == 1 && Global.Schedule_Number == 4)
                {
                    if (ini12.INIRead(Global.MainSettingPath, "Schedule5", "OnTimeStart", "") == "1" && StartButtonPressed == true)
                    {
                        if (Global.Break_Out_Schedule == 0)
                        {
                            while (ini12.INIRead(Global.MainSettingPath, "Schedule5", "Timer", "") != TimeLabel2.Text)
                            {
                                ScheduleWait.WaitOne();
                            }
                            ScheduleWait.Set();
                        }
                    }
                    ini12.INIWrite(Global.MainSettingPath, "Schedule4", "OnTimeStart", "0");
                    button_Schedule5.PerformClick();
                    MyRunCamd();
                }
            }
            #endregion

            //全部schedule跑完或是按下stop鍵以後會跑以下這段/////////////////////////////////////////
            if (StartButtonPressed == false)//按下STOP讓schedule結束//
            {
                Global.Break_Out_MyRunCamd = 1;
                ini12.INIWrite(Global.MailSettingPath, "Data Info", "CloseTime", string.Format("{0:R}", DateTime.Now));
                UpdateUI("START", button_Start);
                button_Start.Enabled = true;
                button_Setting.Enabled = true;
                button_Pause.Enabled = false;
                button_SaveSchedule.Enabled = true;

                if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")
                {
                    _captureInProgress = false;
                    OnOffCamera();
                    //button_VirtualRC.Enabled = true;
                }

                /*
                if (Directory.Exists(ini12.INIRead(sPath, "Record", "VideoPath", "") + "\\" + "Schedule" + Global.Schedule_Num + "_Original") == true)
                {
                    DirectoryInfo DIFO = new DirectoryInfo(ini12.INIRead(sPath, "Record", "VideoPath", "") + "\\" + "Schedule" + Global.Schedule_Num + "_Original");
                    DIFO.Delete(true);
                }
                */
            }
            else//schedule自動跑完//
            {
                StartButtonPressed = false;       

                UpdateUI("START", button_Start);
                button_Setting.Enabled = true;
                button_Pause.Enabled = false;
                button_SaveSchedule.Enabled = true;

                if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")
                {
                    _captureInProgress = false;
                    OnOffCamera();
                }

                Global.Total_Test_Time = Global.Schedule_1_TestTime + Global.Schedule_2_TestTime + Global.Schedule_3_TestTime + Global.Schedule_4_TestTime + Global.Schedule_5_TestTime;
                ConvertToRealTime(Global.Total_Test_Time);
                if (ini12.INIRead(Global.MailSettingPath, "Send Mail", "value", "") == "1")
                {
                    Global.Loop_Number = Global.Loop_Number - 1;
                    FormMail FormMail = new FormMail();
                    FormMail.send();
                }
            }

            label_Command.Text = "Completed!";
            ini12.INIWrite(Global.MainSettingPath, "Schedule" + Global.Schedule_Number, "OnTimeStart", "0");
            button_Schedule1.PerformClick();
            timer1.Stop();
            CloseDtplay();
            timeCount = Global.Schedule_1_TestTime;
            ConvertToRealTime(timeCount);
        }
        #endregion

        #region -- IO CMD 指令集 --
        private void IO_CMD()
        {
            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_pause")
            {
                button_Pause.PerformClick();
                label_Command.Text = "IO CMD_PAUSE";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_stop")
            {
                button_Start.PerformClick();
                label_Command.Text = "IO CMD_STOP";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_ac_restart")
            {
                GP0_GP1_AC_OFF_ON();
                Thread.Sleep(10);
                GP0_GP1_AC_OFF_ON();
                label_Command.Text = "IO CMD_AC_RESTART";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_shot")
            {
                Global.caption_Num++;
                if (Global.Loop_Number == 1)
                    Global.caption_Sum = Global.caption_Num;
                Jes();
                label_Command.Text = "IO CMD_SHOT";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_mail")
            {
                if (ini12.INIRead(Global.MailSettingPath, "Send Mail", "value", "") == "1")
                {
                    Global.Pass_Or_Fail = "NG";
                    FormMail FormMail = new FormMail();
                    FormMail.send();
                    label_Command.Text = "IO CMD_MAIL";
                }
                else
                {
                    MessageBox.Show("Please open the mail function.");
                }
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString().Substring(0, 3) == "_rc")
            {
                String rc_key = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString();
                int startIndex = 4;
                int length = rc_key.Length - 4;
                String rc_key_substring = rc_key.Substring(startIndex, length);

                if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1")
                {
                    Autocommand_RedRat("Form1", rc_key_substring);
                    label_Command.Text = rc_key_substring;
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                {
                    Autocommand_BlueRat("Form1", rc_key_substring);
                    label_Command.Text = rc_key_substring;
                }
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString().Substring(0, 7) == "_logcmd")
            {
                String log_cmd = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString();
                int startIndex = 8;
                int length = log_cmd.Length - 8;
                String log_cmd_substring = log_cmd.Substring(startIndex, length);

                if (ini12.INIRead(Global.MainSettingPath, "Comport", "Checked", "") == "1")
                {
                    serialPort1.WriteLine(log_cmd_substring);
                }
                else if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1")
                {
                    serialPort2.WriteLine(log_cmd_substring);
                }
            }
        }
        #endregion

        #region -- KEYWORD 指令集 --
        private void KeywordCommand()
        {
            if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_pause")
            {
                button_Pause.PerformClick();
                label_Command.Text = "KEYWORD_PAUSE";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_stop")
            {
                button_Start.PerformClick();
                label_Command.Text = "KEYWORD_STOP";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_ac_restart")
            {
                GP0_GP1_AC_OFF_ON();
                Thread.Sleep(10);
                GP0_GP1_AC_OFF_ON();
                label_Command.Text = "KEYWORD_AC_RESTART";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_shot")
            {
                Global.caption_Num++;
                if (Global.Loop_Number == 1)
                    Global.caption_Sum = Global.caption_Num;
                Jes();
                label_Command.Text = "KEYWORD_SHOT";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_mail")
            {
                if (ini12.INIRead(Global.MailSettingPath, "Send Mail", "value", "") == "1")
                {
                    Global.Pass_Or_Fail = "NG";
                    FormMail FormMail = new FormMail();
                    FormMail.send();
                    label_Command.Text = "KEYWORD_MAIL";
                }
                else
                {
                    MessageBox.Show("Please open the mail function.");
                }
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_savelog1")
            {
                string fName = "";

                fName = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
                string t = fName + "\\_SaveLog1_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + label_LoopNumber_Value.Text + ".txt";

                StreamWriter MYFILE = new StreamWriter(t, false, Encoding.ASCII);
                MYFILE.Write(textBox1.Text);
                MYFILE.Close();
                Txtbox1("", textBox1);
                label_Command.Text = "KEYWORD_SAVELOG1";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString() == "_savelog2")
            {
                string fName = "";

                fName = ini12.INIRead(Global.MainSettingPath, "Record", "LogPath", "");
                string t = fName + "\\_SaveLog2_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + label_LoopNumber_Value.Text + ".txt";

                StreamWriter MYFILE = new StreamWriter(t, false, Encoding.ASCII);
                MYFILE.Write(textBox2.Text);
                MYFILE.Close();
                Txtbox2("", textBox2);
                label_Command.Text = "KEYWORD_SAVELOG2";
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString().Substring(0, 3) == "_rc")
            {
                String rc_key = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString();
                int startIndex = 4;
                int length = rc_key.Length - 4;
                String rc_key_substring = rc_key.Substring(startIndex, length);

                if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1")
                {
                    Autocommand_RedRat("Form1", rc_key_substring);
                    label_Command.Text = rc_key_substring;
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
                {
                    Autocommand_BlueRat("Form1", rc_key_substring);
                    label_Command.Text = rc_key_substring;
                }
            }
            else if (DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString().Substring(0, 7) == "_logcmd")
            {
                String log_cmd = DataGridView_Schedule.Rows[Global.Scheduler_Row].Cells[5].Value.ToString();
                int startIndex = 8;
                int length = log_cmd.Length - 8;
                String log_cmd_substring = log_cmd.Substring(startIndex, length);

                if (ini12.INIRead(Global.MainSettingPath, "Comport", "Checked", "") == "1")
                {
                    serialPort1.WriteLine(log_cmd_substring);
                }
                else if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1")
                {
                    serialPort2.WriteLine(log_cmd_substring);
                }
            }
        }
        #endregion

        #region -- 圖片比對 --
        private void MyCompareCamd()
        {
            //String fNameAll = "";
            //String fNameNG = "";
            /*            
            int i, j = 1;
            int TotalDelay = 0;

            switch (Global.Schedule_Num)
            {
                case 1:
                    TotalDelay = (Convert.ToInt32(Global.Schedule_Num1_Time) / Global.Schedule_Loop);
                    break;
                case 2:
                    TotalDelay = (Convert.ToInt32(Global.Schedule_Num2_Time) / Global.Schedule_Loop);
                    break;
                case 3:
                    TotalDelay = (Convert.ToInt32(Global.Schedule_Num3_Time) / Global.Schedule_Loop);
                    break;
                case 4:
                    TotalDelay = (Convert.ToInt32(Global.Schedule_Num4_Time) / Global.Schedule_Loop);
                    break;
                case 5:
                    TotalDelay = (Convert.ToInt32(Global.Schedule_Num5_Time) / Global.Schedule_Loop);
                    break;
            }       //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<


            //float[,] ReferenceResult = new float[Global.Schedule_Loop, Global.caption_Sum + 1];
            //float[] MeanValue = new float[Global.Schedule_Loop];
            //int[] TotalValue = new int[Global.Schedule_Loop];
            */
            //string ngPath = ini12.INIRead(sPath, "Record", "VideoPath", "") + "\\" + "Schedule" + Global.Schedule_Num + "_NG\\";
            string comparePath = ini12.INIRead(Global.MainSettingPath, "Record", "ComparePath", "") + "\\";
            string csvFile = comparePath + "SimilarityReport_" + Global.Schedule_Number + ".csv";

            //Console.WriteLine("Loop Number: " + Global.loop_Num);

            // 讀取ini中的路徑
            //fNameNG = ini12.INIRead(sPath, "Record", "VideoPath", "") + "\\" + "Schedule" + Global.Schedule_Num + "_NG\\";

            string pathCompare1 = comparePath + "cf-" + Global.Loop_Number + "_" + Global.caption_Num + ".png";
            string pathCompare2 = comparePath + "cf-" + (Global.Loop_Number - 1) + "_" + Global.caption_Num + ".png";
            if (Global.caption_Num == 0)
            {
                Console.WriteLine("Path Compare1: " + pathCompare1);
                Console.WriteLine("Path Compare2: " + pathCompare2);
            }
            if (System.IO.File.Exists(pathCompare1) && System.IO.File.Exists(pathCompare2))
            {
                string oHashCode = ImageHelper.produceFingerPrint(pathCompare1);
                string nHashCode = ImageHelper.produceFingerPrint(pathCompare2);
                int difference = ImageHelper.hammingDistance(oHashCode, nHashCode);
                int differenceNum = Convert.ToInt32(ini12.INIRead(Global.MainSettingPath, "Record", "CompareDifferent", ""));
                string differencePercent = "";

                if (difference == 0)
                {
                    differencePercent = "100%";
                }
                else if (difference <= 10)
                {
                    differencePercent = "90%";
                }
                else if (difference <= 20)
                {
                    differencePercent = "80%";
                }
                else if (difference <= 30)
                {
                    differencePercent = "70%";
                }
                else if (difference <= 40)
                {
                    differencePercent = "60%";
                }
                else if (difference <= 50)
                {
                    differencePercent = "50%";
                }
                else if (difference <= 60)
                {
                    differencePercent = "40%";
                }
                else if (difference <= 70)
                {
                    differencePercent = "30%";
                }
                else if (difference <= 80)
                {
                    differencePercent = "20%";
                }
                else if (difference <= 90)
                {
                    differencePercent = "10%";
                }
                else
                {
                    differencePercent = "0%";
                }
                // 匯出csv記錄檔
                StreamWriter sw = new StreamWriter(csvFile, true);

                // 比對值設定
                Global.excel_Num++;
                if (difference > differenceNum)
                {
                    Global.NGValue[Global.caption_Num]++;
                    Global.NGRateValue[Global.caption_Num] = (float)Global.NGValue[Global.caption_Num] / (Global.Loop_Number - 1);

                    /*
                                        string[] FileList = System.IO.Directory.GetFiles(fNameAll, "cf-" + Global.loop_Num + "_" + Global.caption_Num + ".png");
                                        foreach (string File in FileList)
                                        {
                                            System.IO.FileInfo fi = new System.IO.FileInfo(File);
                                            fi.CopyTo(fNameNG + fi.Name);
                                        }
                    */

                    Global.NGRateValue[Global.caption_Num] = (float)Global.NGValue[Global.caption_Num] / (Global.Loop_Number - 1);

                    /*
                    #region Excel function
                    try
                    {
                        // 引用第一個工作表
                        wSheet = (Excel._Worksheet)wBook.Worksheets[1];

                        // 命名工作表的名稱
                        wSheet.Name = "全部測試資料";

                        // 設定工作表焦點
                        wSheet.Activate();

                        // 設定第n列資料
                        excelApp.Cells[Global.excel_Num, 1] = " " + (Global.loop_Num - 1) + "-" + Global.caption_Num;
                        wSheet.Hyperlinks.Add(excelApp.Cells[Global.excel_Num, 1], "cf-" + (Global.loop_Num - 1) + "_" + Global.caption_Num + ".png", Type.Missing, Type.Missing, Type.Missing);
                        excelApp.Cells[Global.excel_Num, 2] = " " + (Global.loop_Num) + "-" + Global.caption_Num;
                        wSheet.Hyperlinks.Add(excelApp.Cells[Global.excel_Num, 2], "cf-" + (Global.loop_Num) + "_" + Global.caption_Num + ".png", Type.Missing, Type.Missing, Type.Missing);
                        excelApp.Cells[Global.excel_Num, 3] = differencePercent;
                        excelApp.Cells[Global.excel_Num, 4] = Global.NGValue[Global.caption_Num];
                        excelApp.Cells[Global.excel_Num, 5] = Global.NGRateValue[Global.caption_Num];
                        excelApp.Cells[Global.excel_Num, 6] = "NG";

                        // 設定第n列顏色
                        wRange = wSheet.Range[wSheet.Cells[Global.excel_Num, 1], wSheet.Cells[Global.excel_Num, 2]];
                        wRange.Select();
                        wRange.Font.Color = ColorTranslator.ToOle(Color.Blue);
                        wRange = wSheet.Range[wSheet.Cells[Global.excel_Num, 3], wSheet.Cells[Global.excel_Num, 6]];
                        wRange.Select();
                        wRange.Font.Color = ColorTranslator.ToOle(Color.Red);

                        // 自動調整欄寬
                        wRange = wSheet.Range[wSheet.Cells[Global.excel_Num, 1], wSheet.Cells[Global.excel_Num, 6]];
                        wRange.EntireRow.AutoFit();
                        wRange.EntireColumn.AutoFit();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("產生報表時出錯！" + Environment.NewLine + ex.Message);
                    }
                    #endregion
                    */

                    sw.Write("=hyperlink(\"cf-" + (Global.Loop_Number - 1) + "_" + Global.caption_Num + ".png\"，\"" + (Global.Loop_Number - 1) + "-" + Global.caption_Num + "\")" + ",");
                    sw.Write("=hyperlink(\"cf-" + (Global.Loop_Number) + "_" + Global.caption_Num + ".png\"，\"" + (Global.Loop_Number) + "-" + Global.caption_Num + "\")" + ",");
                    sw.Write(differencePercent + ",");
                    sw.Write(Global.NGValue[Global.caption_Num] + ",");
                    sw.Write(Global.NGRateValue[Global.caption_Num] + ",");
                    sw.WriteLine("NG");
                }
                else
                {
                    Global.NGRateValue[Global.caption_Num] = (float)Global.NGValue[Global.caption_Num] / (Global.Loop_Number - 1);

                    /*
                    #region Excel function
                    try
                    {
                        // 引用第一個工作表
                        wSheet = (Excel._Worksheet)wBook.Worksheets[1];

                        // 命名工作表的名稱
                        wSheet.Name = "全部測試資料";

                        // 設定工作表焦點
                        wSheet.Activate();

                        // 設定第n列資料
                        excelApp.Cells[Global.excel_Num, 1] = " " + (Global.loop_Num - 1) + "-" + Global.caption_Num;
                        wSheet.Hyperlinks.Add(excelApp.Cells[Global.excel_Num, 1], "cf-" + (Global.loop_Num - 1) + "_" + Global.caption_Num + ".png", Type.Missing, Type.Missing, Type.Missing);
                        excelApp.Cells[Global.excel_Num, 2] = " " + (Global.loop_Num) + "-" + Global.caption_Num;
                        wSheet.Hyperlinks.Add(excelApp.Cells[Global.excel_Num, 2], "cf-" + (Global.loop_Num) + "_" + Global.caption_Num + ".png", Type.Missing, Type.Missing, Type.Missing);
                        excelApp.Cells[Global.excel_Num, 3] = differencePercent;
                        excelApp.Cells[Global.excel_Num, 4] = Global.NGValue[Global.caption_Num];
                        excelApp.Cells[Global.excel_Num, 5] = Global.NGRateValue[Global.caption_Num];
                        excelApp.Cells[Global.excel_Num, 6] = "Pass";

                        // 設定第n列顏色
                        wRange = wSheet.Range[wSheet.Cells[Global.excel_Num, 1], wSheet.Cells[Global.excel_Num, 2]];
                        wRange.Select();
                        wRange.Font.Color = ColorTranslator.ToOle(Color.Blue);
                        wRange = wSheet.Range[wSheet.Cells[Global.excel_Num, 3], wSheet.Cells[Global.excel_Num, 6]];
                        wRange.Select();
                        wRange.Font.Color = ColorTranslator.ToOle(Color.Green);

                        // 自動調整欄寬
                        wRange = wSheet.Range[wSheet.Cells[Global.excel_Num, 1], wSheet.Cells[Global.excel_Num, 6]];
                        wRange.EntireRow.AutoFit();
                        wRange.EntireColumn.AutoFit();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("產生報表時出錯！" + Environment.NewLine + ex.Message);
                    }
                    #endregion
                    */

                    sw.Write("=hyperlink(\"cf-" + (Global.Loop_Number - 1) + "_" + Global.caption_Num + ".png\"，\"" + (Global.Loop_Number - 1) + "-" + Global.caption_Num + "\")" + ",");
                    sw.Write("=hyperlink(\"cf-" + (Global.Loop_Number) + "_" + Global.caption_Num + ".png\"，\"" + (Global.Loop_Number) + "-" + Global.caption_Num + "\")" + ",");
                    sw.Write(differencePercent + ",");
                    sw.Write(Global.NGValue[Global.caption_Num] + ",");
                    sw.Write(Global.NGRateValue[Global.caption_Num] + ",");
                    sw.WriteLine("Pass");
                }
                sw.Close();

                /*
                Bitmap picCompare1 = (Bitmap)Image.FromFile(pathCompare1);
                Bitmap picCompare2 = (Bitmap)Image.FromFile(pathCompare2);
                float CompareValue = Similarity(picCompare1, picCompare2);
                ReferenceResult[(Global.loop_Num - 1), Global.caption_Num] = CompareValue;
                Console.WriteLine("Reference(" + (Global.loop_Num - 1) + "," + Global.caption_Num + ") = " + ReferenceResult[(Global.loop_Num - 1), Global.caption_Num]);

                Global.SumValue[Global.caption_Num] = Global.SumValue[Global.caption_Num] + ReferenceResult[(Global.loop_Num - 1), Global.caption_Num];
                Console.WriteLine("SumValue" + Global.caption_Num + " = " + Global.SumValue[Global.caption_Num]);

                MeanValue[Global.caption_Num] = Global.SumValue[Global.caption_Num] / (Global.loop_Num - 1);
                Console.WriteLine("MeanValue" + Global.caption_Num + " = " + MeanValue[Global.caption_Num]);

                for (i = Global.loop_Num - 11; i < Global.loop_Num - 1; i++)
                {
                    for (j = 1; j < Global.caption_Sum + 1; j++)
                    {
                        string pathCompare1 = fNameAll + "cf-" + i + "_" + j + ".png";
                        string pathCompare2 = fNameAll + "cf-" + (i - 1) + "_" + j + ".png";
                        Bitmap picCompare1 = (Bitmap)Image.FromFile(pathCompare1);
                        Bitmap picCompare2 = (Bitmap)Image.FromFile(pathCompare2);
                        float CompareValue = Similarity(picCompare1, picCompare2);
                        ReferenceResult[i, j] = CompareValue;
                        Console.WriteLine("Reference(" + i + "," + j + ") = " + ReferenceResult[i, j]);

                        //int[] GetHisogram1 = GetHisogram(picCompare1);
                        //int[] GetHisogram2 = GetHisogram(picCompare2);
                        //float CompareResult = GetResult(GetHisogram1, GetHisogram2);

                        //long[] GetHistogram1 = GetHistogram(picCompare1);
                        //long[] GetHistogram2 = GetHistogram(picCompare2);
                        //float CompareResult = GetResult(GetHistogram1, GetHistogram2);

                    }
                    //Thread.Sleep(TotalDelay);
                }

                for (j = 1; j < Global.caption_Sum + 1; j++)
                {
                    for (i = 1; i < Global.loop_Num - 1; i++)
                    {
                        SumValue[j] = SumValue[j] + ReferenceResult[i, j];
                        TotalValue[j]++;
                        //Console.WriteLine("SumValue" + j + " = " + SumValue[j]);
                    }
                    //Thread.Sleep(TotalDelay);
                    MeanValue[j] = SumValue[j] / (Global.loop_Num - 2);
                    //Console.WriteLine("MeanValue" + j + " = " + MeanValue[j]);
                }

                StreamWriter sw = new StreamWriter(csvFile, true);
                if (Global.loop_Num == 2 && Global.caption_Num == 1)
                    sw.WriteLine("Point(X), Point(Y), MeanValue, Reference, NGValue, TotalValue, NGRate, Test Result");

                if (ReferenceResult[(Global.loop_Num - 1), Global.caption_Num] > (MeanValue[Global.caption_Num] + 0.5) || ReferenceResult[(Global.loop_Num - 1), Global.caption_Num] < (MeanValue[Global.caption_Num] - 0.5))
                {
                    Global.NGValue[Global.caption_Num]++;
                    Global.NGRateValue[Global.caption_Num] = (float)Global.NGValue[Global.caption_Num] / Global.loop_Num;
                    string[] FileList = System.IO.Directory.GetFiles(fNameAll, "cf-" + Global.loop_Num + "_" + Global.caption_Num + ".png");
                    foreach (string File in FileList)
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(File);
                        fi.CopyTo(fNameNG + fi.Name);
                    }
                    sw.Write((Global.loop_Num - 1) + ", " + Global.caption_Num + ", ");
                    sw.Write(MeanValue[Global.caption_Num] + ", ");
                    sw.Write(ReferenceResult[(Global.loop_Num - 1), Global.caption_Num] + ", ");
                    sw.Write(Global.NGValue[Global.caption_Num] + ", ");
                    sw.Write(Global.loop_Num + ", ");
                    sw.Write(Global.NGRateValue[Global.caption_Num] + ", ");
                    sw.WriteLine("NG");
                }
                else
                {
                    Global.NGRateValue[Global.caption_Num] = (float)Global.NGValue[Global.caption_Num] / Global.loop_Num;
                    sw.Write((Global.loop_Num - 1) + ", " + Global.caption_Num + ", ");
                    sw.Write(MeanValue[Global.caption_Num] + ", ");
                    sw.Write(ReferenceResult[(Global.loop_Num - 1), Global.caption_Num] + ", ");
                    sw.Write(Global.NGValue[Global.caption_Num] + ", ");
                    sw.Write(Global.loop_Num + ", ");
                    sw.Write(Global.NGRateValue[Global.caption_Num] + ", ");
                    sw.WriteLine("Pass");
                }
                sw.Close();

                RedratLable.Text = "End Compare Picture.";
                */
            }
        }
        #endregion

        #region -- 字幕 --
        private void MySrtCamd()
        {
            int count = 1;
            string starttime = "0:0:0";
            TimeSpan time_start = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"));

            while (VideoRecording)
            {
                System.Threading.Thread.Sleep(1000);
                TimeSpan time_end = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")); //計時結束 取得目前時間
                //後面的時間減前面的時間後 轉型成TimeSpan即可印出時間差
                string endtime = (time_end - time_start).Hours.ToString() + ":" + (time_end - time_start).Minutes.ToString() + ":" + (time_end - time_start).Seconds.ToString();
                StreamWriter srtWriter = new StreamWriter(srtstring, true);
                srtWriter.WriteLine(count);

                srtWriter.WriteLine(starttime + ",001" + " --> " + endtime + ",000");
                srtWriter.WriteLine(label_Command.Text + "     " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                srtWriter.WriteLine("");
                srtWriter.WriteLine("");
                srtWriter.Close();
                count++;
                starttime = endtime;
            }
        }
        #endregion

        private void Mysvideo() => Invoke(new EventHandler(delegate { Savevideo(); }));//開始錄影//
        
        private void Mysstop() => Invoke(new EventHandler(delegate//停止錄影//
        {
            capture.Stop();
            capture.Dispose();
            Camstart();
        }));

        private void Savevideo()//儲存影片//
        {
            string fName = ini12.INIRead(Global.MainSettingPath, "Record", "VideoPath", "");

            string t = fName + "\\" + "_pvr" + DateTime.Now.ToString("yyyyMMddHHmmss") + "__" + label_LoopNumber_Value.Text + ".wmv";
            srtstring = fName + "\\" + "_pvr" + DateTime.Now.ToString("yyyyMMddHHmmss") + "__" + label_LoopNumber_Value.Text + ".srt";

            if (!capture.Cued)
                capture.Filename = t;

            capture.RecFileMode = DirectX.Capture.Capture.RecFileModeType.Wmv; //宣告我要wmv檔格式
            capture.Cue(); // 創一個檔
            capture.Start(); // 開始錄影

            /*
            double chd; //檢查HD 空間 小於100M就停止錄影s
            chd = ImageOpacity.ChDisk(ImageOpacity.Dkroot(fName));
            if (chd < 0.1)
            {
                Vread = false;
                MessageBox.Show("Check the HD Capacity!", "HD Capacity Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void OnOffCamera()//啟動攝影機//
        {
            if (_captureInProgress == true)
            {
                Camstart();
            }

            if (_captureInProgress == false && capture != null)
            {
                capture.Stop();
                capture.Dispose();
            }
        }

        private void Camstart()
        {
            Filters filters = new Filters();
            Filter f;
            
            List<string> video = new List<string> { };
            for (int c = 0; c < filters.VideoInputDevices.Count; c++)
            {
                f = filters.VideoInputDevices[c];
                video.Add(f.Name);
            }

            List<string> audio = new List<string> { };
            for (int j = 0; j < filters.AudioInputDevices.Count; j++)
            {
                f = filters.AudioInputDevices[j];
                audio.Add(f.Name);
            }

            int scam = int.Parse(ini12.INIRead(Global.MainSettingPath, "Camera", "VideoIndex", ""));
            int saud = int.Parse(ini12.INIRead(Global.MainSettingPath, "Camera", "AudioIndex", ""));
            int VideoNum = int.Parse(ini12.INIRead(Global.MainSettingPath, "Camera", "VideoNumber", ""));
            int AudioNum = int.Parse(ini12.INIRead(Global.MainSettingPath, "Camera", "AudioNumber", ""));

            if (filters.VideoInputDevices.Count < VideoNum || 
                filters.AudioInputDevices.Count < AudioNum)
            {
                MessageBox.Show("Please reset video and/or audio device and select continue.", "Camera Status Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button_Setting.PerformClick();
            }
            else
            {
                capture = new Capture(filters.VideoInputDevices[scam], filters.AudioInputDevices[saud]);
                capture.CaptureComplete += new EventHandler(OnCaptureComplete);
            }

            if (capture.PreviewWindow == null)
            {
                capture.PreviewWindow = panelVideo;
            }
            else
            {
                capture.PreviewWindow = null;
            }
        }

        #region -- 讀取RC DB並填入combobox --
        private void LoadRCDB()
        {
            RedRatData.RedRatLoadSignalDB(ini12.INIRead(Global.MainSettingPath, "RedRat", "DBFile", ""));
            RedRatData.RedRatSelectDevice(ini12.INIRead(Global.MainSettingPath, "RedRat", "Brands", ""));

            DataGridViewComboBoxColumn RCDB = (DataGridViewComboBoxColumn)DataGridView_Schedule.Columns[0];

            string devicename = ini12.INIRead(Global.MainSettingPath, "RedRat", "Brands", "");
            if (RedRatData.RedRatSelectDevice(devicename))
            {
                RCDB.Items.AddRange(RedRatData.RedRatGetRCNameList().ToArray());
                Global.Rc_List = RedRatData.RedRatGetRCNameList();
                Global.Rc_Number = RedRatData.RedRatGetRCNameList().Count;
            }
            else
            {
                Console.WriteLine("Select Device Error: " + devicename);
            }

            RCDB.Items.Add("------------------------");
            RCDB.Items.Add("_cmd");
            RCDB.Items.Add("_log1");
            RCDB.Items.Add("_log2");
            RCDB.Items.Add("_astro");
            RCDB.Items.Add("_quantum");
            RCDB.Items.Add("_dektec");
            RCDB.Items.Add("_DOS");
            RCDB.Items.Add("_IO_Output");
            RCDB.Items.Add("_IO_Input");
            RCDB.Items.Add("_SXP");
            RCDB.Items.Add("_audio_debounce");
            RCDB.Items.Add("------------------------");
            RCDB.Items.Add("_PA10_0");
            RCDB.Items.Add("_PA10_1");
            RCDB.Items.Add("_PA11_0");
            RCDB.Items.Add("_PA11_1");
            RCDB.Items.Add("_PA14_0");
            RCDB.Items.Add("_PA14_1");
            RCDB.Items.Add("_PA15_0");
            RCDB.Items.Add("_PA15_1");
            RCDB.Items.Add("_PB01_0");
            RCDB.Items.Add("_PB01_1");
            RCDB.Items.Add("_PB07_0");
            RCDB.Items.Add("_PB07_1");
            RCDB.Items.Add("------------------------");
            RCDB.Items.Add("_keyword_1");
            RCDB.Items.Add("_keyword_2");
            RCDB.Items.Add("_keyword_3");
            RCDB.Items.Add("_keyword_4");
            RCDB.Items.Add("_keyword_5");
            RCDB.Items.Add("_keyword_6");
            RCDB.Items.Add("_keyword_7");
            RCDB.Items.Add("_keyword_8");
            RCDB.Items.Add("_keyword_9");
            RCDB.Items.Add("_keyword_10");

            //RCDB.Items.Add("_MonkeyTest");
        }
        #endregion

        Button[] Buttons;
        private void Sand_Key(int i)
        {
            if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1")
            {
                if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1")
                {
                    Autocommand_RedRat("Form1", Buttons[i - 1].Text);
                }
                else if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxVerson", "") == "2")
                {
                    Autocommand_BlueRat("Form1", Buttons[i - 1].Text);
                }
            }
            else if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1")
            {
                Autocommand_RedRat("Form1", Buttons[i - 1].Text);
            }
        }

        void DataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ComboBox cmb = e.Control as ComboBox;
            if (cmb != null)
            {
                cmb.DropDown -= new EventHandler(cmb_DropDown);
                cmb.DropDown += new EventHandler(cmb_DropDown);
            }
        }

        //自動調整ComboBox寬度//
        void cmb_DropDown(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            int width = cmb.DropDownWidth;
            Graphics g = cmb.CreateGraphics();
            Font font = cmb.Font;
            int vertScrollBarWidth = 0;
            if (cmb.Items.Count > cmb.MaxDropDownItems)
            {
                vertScrollBarWidth = SystemInformation.VerticalScrollBarWidth;
            }

            int maxWidth;
            foreach (string s in cmb.Items)
            {
                maxWidth = (int)g.MeasureString(s, font).Width + vertScrollBarWidth;
                if (width < maxWidth)
                {
                    width = maxWidth;
                }
            }

            DataGridViewComboBoxColumn c =
                this.DataGridView_Schedule.Columns[0] as DataGridViewComboBoxColumn;
            if (c != null)
            {
                c.DropDownWidth = width;
            }
        }
        
        private void StartBtn_Click(object sender, EventArgs e)
        {
            byte[] val = new byte[2];
            val[0] = 0;
            bool AutoBox_Status;

            Global.IO_PA10_0_COUNT = 0;
            Global.IO_PA10_1_COUNT = 0;
            Global.IO_PA11_0_COUNT = 0;
            Global.IO_PA11_1_COUNT = 0;
            Global.IO_PA14_0_COUNT = 0;
            Global.IO_PA14_1_COUNT = 0;
            Global.IO_PA15_0_COUNT = 0;
            Global.IO_PA15_1_COUNT = 0;
            Global.IO_PB1_0_COUNT = 0;
            Global.IO_PB1_1_COUNT = 0;
            Global.IO_PB7_0_COUNT = 0;
            Global.IO_PB7_1_COUNT = 0;

            AutoBox_Status = ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxExist", "") == "1" ? true : false;

            if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")
            {
                if (!_captureInProgress)
                {
                    _captureInProgress = true;
                    OnOffCamera();
                }
            }

            if (AutoBox_Status == true)//如果電腦有接上AutoBox//
            {
                button_Schedule1.PerformClick();
                Thread MainThread = new Thread(new ThreadStart(MyRunCamd));
                Thread LogThread1 = new Thread(new ThreadStart(MyLog1Camd));
                Thread LogThread2 = new Thread(new ThreadStart(MyLog2Camd));
                //Thread Log1Data = new Thread(new ThreadStart(Log1_Receiving_Task));
                //Thread Log2Data = new Thread(new ThreadStart(Log2_Receiving_Task));
                
                if (StartButtonPressed == true)//按下STOP//
                {
                    StartButtonPressed = false;
                    
                    Global.Break_Out_MyRunCamd = 1;//跳出倒數迴圈//
                    MainThread.Abort();//停止執行緒//
                    timer1.Stop();//停止倒數//
                    CloseDtplay();//關閉DtPlay//

                    if (ini12.INIRead(Global.MainSettingPath, "Comport", "Checked", "") == "1")
                    {
                        if (ini12.INIRead(Global.MainSettingPath, "LogSearch", "TextNum", "") != "0")
                        {
                            LogThread1.Abort();
                            //Log1Data.Abort();
                        }
                    }

                    if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1")
                    {
                        if (ini12.INIRead(Global.MainSettingPath, "LogSearch", "TextNum", "") != "0")
                        {
                            LogThread2.Abort();
                            //Log2Data.Abort();
                        }
                    }

                    button_Start.Enabled = false;
                    button_Setting.Enabled = false;
                    button_SaveSchedule.Enabled = false;
                    button_Pause.Enabled = true;

                    label_Command.Text = "Please wait...";
                }
                else//按下START//
                {
                    StartButtonPressed = true;

                    /*
                    for (int i = 1; i < 6; i++)
                    {
                        if (Directory.Exists(ini12.INIRead(sPath, "Record", "VideoPath", "") + "\\" + "Schedule" + i + "_Original") == true)
                        {
                            DirectoryInfo DIFO = new DirectoryInfo(ini12.INIRead(sPath, "Record", "VideoPath", "") + "\\" + "Schedule" + i + "_Original");
                            DIFO.Delete(true);
                        }

                        if (Directory.Exists(ini12.INIRead(sPath, "Record", "VideoPath", "") + "\\" + "Schedule" + i + "_NG") == true)
                        {
                            DirectoryInfo DIFO = new DirectoryInfo(ini12.INIRead(sPath, "Record", "VideoPath", "") + "\\" + "Schedule" + i + "_NG");
                            DIFO.Delete(true);
                        }                
                    }
                    */

                    Global.Break_Out_MyRunCamd = 0;

                    if (ini12.INIRead(Global.MainSettingPath, "Comport", "Checked", "") == "1")
                    {
                        OpenSerialPort1();
                        textBox1.Text = "";//清空serialport1//
                        if (ini12.INIRead(Global.MainSettingPath, "LogSearch", "TextNum", "") != "0")
                        {
                            LogThread1.IsBackground = true;
                            LogThread1.Start();
                        }
                    }

                    if (ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1")
                    {
                        OpenSerialPort2();
                        textBox2.Text = "";//清空serialport2//
                        if (ini12.INIRead(Global.MainSettingPath, "LogSearch", "TextNum", "") != "0")
                        {
                            LogThread2.IsBackground = true;
                            LogThread2.Start();
                        }
                    }
                    
                    ini12.INIWrite(Global.MainSettingPath, "LogSearch", "StartTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    MainThread.Start();       // 啟動執行緒
                    timer1.Start();     //開始倒數
                    button_Start.Text = "STOP";

                    button_Setting.Enabled = false;
                    button_Pause.Enabled = true;
                    button_SaveSchedule.Enabled = false;
                }
            }

            if (AutoBox_Status == false)//如果沒接AutoBox//
            {
                Thread MainThread = new Thread(new ThreadStart(MyRunCamd));
                
                if (StartButtonPressed == true)//按下STOP//
                {
                    StartButtonPressed = false;

                    Global.Break_Out_MyRunCamd = 1;    //跳出倒數迴圈
                    MainThread.Abort(); //停止執行緒
                    timer1.Stop();  //停止倒數
                    CloseDtplay();

                    button_Start.Enabled = false;
                    button_Setting.Enabled = false;
                    button_Pause.Enabled = true;
                    button_SaveSchedule.Enabled = false;
                    
                    label_Command.Text = "Please wait...";
                }
                else//按下START//
                {
                    StartButtonPressed = true;

                    Global.Break_Out_MyRunCamd = 0;
                    MainThread.Start();// 啟動執行緒
                    timer1.Start();     //開始倒數

                    button_Setting.Enabled = false;
                    button_Pause.Enabled = true;
                    pictureBox_AcPower.Image = Properties.Resources.OFF;
                    button_Start.Text = "STOP";
                }
            }
        }

        private void SettingBtn_Click(object sender, EventArgs e)
        {
            FormTabControl FormTabControl = new FormTabControl();
            Global.RCDB = ini12.INIRead(Global.MainSettingPath, "RedRat", "Brands", "");
            
            //如果serialport開著則先關閉//
            if (serialPort1.IsOpen == true)
            {
                CloseSerialPort1();
            }
            if (serialPort2.IsOpen == true)
            {
                CloseSerialPort2();
            }

            //關閉SETTING以後會讀這段>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            if (FormTabControl.ShowDialog() == DialogResult.OK)
            {
                if (ini12.INIRead(Global.MainSettingPath, "RedRat", "Brands", "") != Global.RCDB)
                {
                    DataGridViewComboBoxColumn RCDB = (DataGridViewComboBoxColumn)DataGridView_Schedule.Columns[0];
                    RCDB.Items.Clear();
                    LoadRCDB();
                }

                if (ini12.INIRead(Global.MainSettingPath, "Device", "RedRatExist", "") == "1")
                {
                    OpenRedRat3();
                    pictureBox_RedRat.Image = Properties.Resources.ON;
                }
                else
                {
                    pictureBox_RedRat.Image = Properties.Resources.OFF;
                }

                if (ini12.INIRead(Global.MainSettingPath, "Device", "CameraExist", "") == "1")
                {
                    pictureBox_Camera.Image = Properties.Resources.ON;
                    _captureInProgress = false;
                    OnOffCamera();
                    button_VirtualRC.Enabled = true;
                    comboBox_CameraDevice.Enabled = false;
                }
                else
                {
                    pictureBox_Camera.Image = Properties.Resources.OFF;
                }

                button_SerialPort1.Visible = ini12.INIRead(Global.MainSettingPath, "Comport", "Checked", "") == "1" ? true : false;
                button_SerialPort2.Visible = ini12.INIRead(Global.MainSettingPath, "ExtComport", "Checked", "") == "1" ? true : false;
                
                List<string> SchExist = new List<string> { };
                for (int i = 2; i < 6; i++)
                {
                    SchExist.Add(ini12.INIRead(Global.MainSettingPath, "Schedule" + i, "Exist", ""));
                }
                button_Schedule2.Visible = SchExist[0] == "0" ? false : true;
                button_Schedule3.Visible = SchExist[1] == "0" ? false : true;
                button_Schedule4.Visible = SchExist[2] == "0" ? false : true;
                button_Schedule5.Visible = SchExist[3] == "0" ? false : true;
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            FormTabControl.Dispose();
            button_Schedule1.Enabled = true;
            button_Schedule1.PerformClick();
        }

        //系統時間
        private void Timer1_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            TimeLabel.Text = string.Format("{0:R}", dt);
            TimeLabel2.Text = string.Format("{0:yyyy-MM-dd  HH:mm:ss}", dt);

            #region -- schedule timer --
            if (ini12.INIRead(Global.MainSettingPath, "Schedule1", "OnTimeStart", "") == "1")
                labelSch1Timer.Text = "Schedule 1 will start at" + "\r\n" + ini12.INIRead(Global.MainSettingPath, "Schedule1", "Timer", "");
            else if (ini12.INIRead(Global.MainSettingPath, "Schedule1", "OnTimeStart", "") == "0")
                labelSch1Timer.Text = "";

            if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "") == "1")
                labelSch2Timer.Text = "Schedule 2 will start at" + "\r\n" + ini12.INIRead(Global.MainSettingPath, "Schedule2", "Timer", "");
            else if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "") == "0")
                labelSch2Timer.Text = "";

            if (ini12.INIRead(Global.MainSettingPath, "Schedule3", "OnTimeStart", "") == "1")
                labelSch3Timer.Text = "Schedule 3 will start at" + "\r\n" + ini12.INIRead(Global.MainSettingPath, "Schedule3", "Timer", "");
            else if (ini12.INIRead(Global.MainSettingPath, "Schedule3", "OnTimeStart", "") == "0")
                labelSch3Timer.Text = "";

            if (ini12.INIRead(Global.MainSettingPath, "Schedule4", "OnTimeStart", "") == "1")
                labelSch4Timer.Text = "Schedule 4 will start at" + "\r\n" + ini12.INIRead(Global.MainSettingPath, "Schedule4", "Timer", "");
            else if (ini12.INIRead(Global.MainSettingPath, "Schedule4", "OnTimeStart", "") == "0")
                labelSch4Timer.Text = "";

            if (ini12.INIRead(Global.MainSettingPath, "Schedule5", "OnTimeStart", "") == "1")
                labelSch5Timer.Text = "Schedule 5 will start at" + "\r\n" + ini12.INIRead(Global.MainSettingPath, "Schedule5", "Timer", "");
            else if (ini12.INIRead(Global.MainSettingPath, "Schedule5", "OnTimeStart", "") == "0")
                labelSch5Timer.Text = "";

            if (ini12.INIRead(Global.MainSettingPath, "Schedule1", "OnTimeStart", "") == "1" &&
                ini12.INIRead(Global.MainSettingPath, "Schedule1", "Timer", "") == TimeLabel2.Text)
                button_Start.PerformClick();
            if (ini12.INIRead(Global.MainSettingPath, "Schedule2", "OnTimeStart", "") == "1" &&
                ini12.INIRead(Global.MainSettingPath, "Schedule2", "Timer", "") == TimeLabel2.Text &&
                timeCount != 0)
                Global.Break_Out_Schedule = 1;
            if (ini12.INIRead(Global.MainSettingPath, "Schedule3", "OnTimeStart", "") == "1" &&
                ini12.INIRead(Global.MainSettingPath, "Schedule3", "Timer", "") == TimeLabel2.Text &&
                timeCount != 0)
                Global.Break_Out_Schedule = 1;
            if (ini12.INIRead(Global.MainSettingPath, "Schedule4", "OnTimeStart", "") == "1" &&
                ini12.INIRead(Global.MainSettingPath, "Schedule4", "Timer", "") == TimeLabel2.Text &&
                timeCount != 0)
                Global.Break_Out_Schedule = 1;
            if (ini12.INIRead(Global.MainSettingPath, "Schedule5", "OnTimeStart", "") == "1" &&
                ini12.INIRead(Global.MainSettingPath, "Schedule5", "Timer", "") == TimeLabel2.Text &&
                timeCount != 0)
                Global.Break_Out_Schedule = 1;
            #endregion
        }

        //關閉Excel
        private void CloseExcel()
        {
            Process[] processes = Process.GetProcessesByName("EXCEL");

            foreach (Process p in processes)
            {
                p.Kill();
            }
        }

        //關閉DtPlay
        private void CloseDtplay()
        {
            Process[] processes = Process.GetProcessesByName("DtPlay");

            foreach (Process p in processes)
            {
                p.Kill();
            }
        }

        //關閉AutoBox
        private void CloseAutobox()
        {
            FormIsClosing = true;
            if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxVerson", "") == "1")
            {
                DisconnectAutoBox1();
            }

            if (ini12.INIRead(Global.MainSettingPath, "Device", "AutoboxVerson", "") == "2")
            {
                DisconnectAutoBox2();
            }

            Application.ExitThread();
            Application.Exit();
            Environment.Exit(Environment.ExitCode);
        }

        private void LabelVersion_MouseClick(object sender, MouseEventArgs e)
        {
            FormSurp SurpriseForm = new FormSurp();
            SurpriseForm.Show(this);
        }

        private void Com1Btn_Click(object sender, EventArgs e)
        {
            OpenSerialPort1();
            Controls.Add(textBox1);
            textBox1.BringToFront();
        }

        private void Com2Btn_Click(object sender, EventArgs e)
        {
            OpenSerialPort2();
            Controls.Add(textBox2);
            textBox2.BringToFront();
        }

        private void Button_TabScheduler_Click(object sender, EventArgs e) => DataGridView_Schedule.BringToFront();
        private void Button_TabCamera_Click(object sender, EventArgs e)
        {
            if (!_captureInProgress)
            {
                _captureInProgress = true;
                OnOffCamera();
            }
            panelVideo.BringToFront();
            comboBox_CameraDevice.Enabled = true;
            comboBox_CameraDevice.BringToFront();
        }

        private void MyExportCamd()
        {
            string ab_num = label_LoopNumber_Value.Text,                                                        //自動編號
                        ab_p_id = ini12.INIRead(Global.MailSettingPath, "Data Info", "ProjectNumber", ""),                    //Project number
                        ab_c_id = ini12.INIRead(Global.MailSettingPath, "Data Info", "TestCaseNumber", ""),                   //Test case number
                        ab_result = ini12.INIRead(Global.MailSettingPath, "Data Info", "Result", ""),                         //AutoTest 測試結果
                        ab_version = ini12.INIRead(Global.MailSettingPath, "Mail Info", "Version", ""),                       //軟體版號
                        ab_ng = ini12.INIRead(Global.MailSettingPath, "Data Info", "NGfrequency", ""),                        //NG frequency
                        ab_create = ini12.INIRead(Global.MailSettingPath, "Data Info", "CreateTime", ""),                     //測試開始時間
                        ab_close = ini12.INIRead(Global.MailSettingPath, "Data Info", "CloseTime", ""),                       //測試結束時間
                        ab_time = ini12.INIRead(Global.MailSettingPath, "Total Test Time", "value", ""),                      //測試執行花費時間
                        ab_loop = Global.Schedule_Loop.ToString(),                                              //執行loop次數
                        ab_loop_time = ini12.INIRead(Global.MailSettingPath, "Total Test Time", "value", ""),                 //1個loop需要次數
                        ab_loop_step = (DataGridView_Schedule.Rows.Count - 1).ToString(),                       //1個loop的step數
                        ab_root = ini12.INIRead(Global.MailSettingPath, "Data Info", "Reboot", ""),                           //測試重啟次數
                        ab_user = ini12.INIRead(Global.MailSettingPath, "Mail Info", "Tester", ""),                           //測試人員
                        ab_mail = ini12.INIRead(Global.MailSettingPath, "Mail Info", "To", "");                               //Mail address 列表

            List<string> DataList = new List<string> { };
            DataList.Add(ab_num);
            DataList.Add(ab_p_id);
            DataList.Add(ab_c_id);
            DataList.Add(ab_result);
            DataList.Add(ab_version);
            DataList.Add(ab_ng);
            DataList.Add(ab_create);
            DataList.Add(ab_close);
            DataList.Add(ab_time);
            DataList.Add(ab_loop);
            DataList.Add(ab_loop_time);
            DataList.Add(ab_loop_step);
            DataList.Add(ab_root);
            DataList.Add(ab_user);
            DataList.Add(ab_mail);

            //Form_DGV_Autobox.DataInsert(DataList);
            //Form_DGV_Autobox.ToCsV(Form_DGV_Autobox.DGV_Autobox, "C:\\AutoTest v2\\Report.xls");
        }

        #region -- 另存Schedule --
        private void WriteBtn_Click(object sender, EventArgs e)
        {
            string delimiter = ",";

            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "CSV files (*.csv)|*.csv";
            sfd.FileName = ini12.INIRead(Global.MainSettingPath, "Schedule" + Global.Schedule_Number, "Path", "");
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfd.FileName, false))
                {
                    //output header data
                    string strHeader = "";
                    for (int i = 0; i < DataGridView_Schedule.Columns.Count; i++)
                    {
                        strHeader += DataGridView_Schedule.Columns[i].HeaderText + delimiter;
                    }
                    sw.WriteLine(strHeader.Replace("\r\n", "~"));

                    //output rows data
                    for (int j = 0; j < DataGridView_Schedule.Rows.Count - 1; j++)
                    {
                        string strRowValue = "";

                        for (int k = 0; k < DataGridView_Schedule.Columns.Count; k++)
                        {
                            strRowValue += DataGridView_Schedule.Rows[j].Cells[k].Value + delimiter;
                        }
                        sw.WriteLine(strRowValue);
                    }
                    sw.Close();
                }
            }
            ReadSch();
            button_Start.Enabled = true;
        }
        #endregion

        private void button_insert_a_row_Click(object sender, EventArgs e)
        {
            DataGridView_Schedule.Rows.Insert(DataGridView_Schedule.CurrentCell.RowIndex +1, new DataGridViewRow());
        }

        #region -- Form1的Schedule 1~5按鈕功能 --
        private void SchBtn1_Click(object sender, EventArgs e)          ////////////Schedule1
        {
            portos_online = new SafeDataGridView();
            Global.Schedule_Number = 1;
            string loop = ini12.INIRead(Global.MainSettingPath, "Schedule1", "Loop", "");
            if (loop != "")
                Global.Schedule_Loop = int.Parse(loop);
            labellabel_LoopTimes_Value.Text = Global.Schedule_Loop.ToString();
            button_Schedule1.Enabled = false;
            button_Schedule2.Enabled = true;
            button_Schedule3.Enabled = true;
            button_Schedule4.Enabled = true;
            button_Schedule5.Enabled = true;
            ReadSch();
            ini12.INIWrite(Global.MailSettingPath, "Data Info", "TestCaseNumber", "0");
            ini12.INIWrite(Global.MailSettingPath, "Data Info", "Result", "N/A");
            ini12.INIWrite(Global.MailSettingPath, "Data Info", "NGfrequency", "0");
        }
        private void SchBtn2_Click(object sender, EventArgs e)          ////////////Schedule2
        {
            portos_online = new SafeDataGridView();
            Global.Schedule_Number = 2;
            string loop = "";
            loop = ini12.INIRead(Global.MainSettingPath, "Schedule2", "Loop", "");
            if (loop != "")
                Global.Schedule_Loop = int.Parse(loop);
            labellabel_LoopTimes_Value.Text = Global.Schedule_Loop.ToString();
            button_Schedule1.Enabled = true;
            button_Schedule2.Enabled = false;
            button_Schedule3.Enabled = true;
            button_Schedule4.Enabled = true;
            button_Schedule5.Enabled = true;
            LoadRCDB();
            ReadSch();
        }
        private void SchBtn3_Click(object sender, EventArgs e)          ////////////Schedule3
        {
            portos_online = new SafeDataGridView();
            Global.Schedule_Number = 3;
            string loop = ini12.INIRead(Global.MainSettingPath, "Schedule3", "Loop", "");
            if (loop != "")
                Global.Schedule_Loop = int.Parse(loop);
            labellabel_LoopTimes_Value.Text = Global.Schedule_Loop.ToString();
            button_Schedule1.Enabled = true;
            button_Schedule2.Enabled = true;
            button_Schedule3.Enabled = false;
            button_Schedule4.Enabled = true;
            button_Schedule5.Enabled = true;
            ReadSch();
        }
        private void SchBtn4_Click(object sender, EventArgs e)          ////////////Schedule4
        {
            portos_online = new SafeDataGridView();
            Global.Schedule_Number = 4;
            string loop = ini12.INIRead(Global.MainSettingPath, "Schedule4", "Loop", "");
            if (loop != "")
                Global.Schedule_Loop = int.Parse(loop);
            labellabel_LoopTimes_Value.Text = Global.Schedule_Loop.ToString();
            button_Schedule1.Enabled = true;
            button_Schedule2.Enabled = true;
            button_Schedule3.Enabled = true;
            button_Schedule4.Enabled = false;
            button_Schedule5.Enabled = true;
            ReadSch();
        }
        private void SchBtn5_Click(object sender, EventArgs e)          ////////////Schedule5
        {
            portos_online = new SafeDataGridView();
            Global.Schedule_Number = 5;
            string loop = ini12.INIRead(Global.MainSettingPath, "Schedule5", "Loop", "");
            if (loop != "")
                Global.Schedule_Loop = int.Parse(loop);
            labellabel_LoopTimes_Value.Text = Global.Schedule_Loop.ToString();
            button_Schedule1.Enabled = true;
            button_Schedule2.Enabled = true;
            button_Schedule3.Enabled = true;
            button_Schedule4.Enabled = true;
            button_Schedule5.Enabled = false;
            ReadSch();
        }
        private void ReadSch()
        {
            // Console.WriteLine(Global.Schedule_Num);
            // 戴入Schedule CSV 檔
            string SchedulePath = ini12.INIRead(Global.MainSettingPath, "Schedule" + Global.Schedule_Number, "Path", "");
            string ScheduleExist = ini12.INIRead(Global.MainSettingPath, "Schedule" + Global.Schedule_Number, "Exist", "");

            string TextLine = "";
            string[] SplitLine;
            int i = 0;
            if ((File.Exists(SchedulePath) == true) && ScheduleExist == "1" && IsFileLocked(SchedulePath) == false)
            {
                DataGridView_Schedule.Rows.Clear();
                StreamReader objReader = new StreamReader(SchedulePath);
                while ((objReader.Peek() != -1))
                {
                    TextLine = objReader.ReadLine();
                    if (i != 0)
                    {
                        SplitLine = TextLine.Split(',');
                        DataGridView_Schedule.Rows.Add(SplitLine);
                    }
                    i++;
                }
                objReader.Close();
            }
            else
            {
                MessageBox.Show("You can start to write a new schedule.", "New Script", MessageBoxButtons.OK, MessageBoxIcon.Information);

                button_Start.Enabled = false;
                button_Schedule1.PerformClick();
            }

            if (TextLine != "")
            {
                int j = Int32.Parse(TextLine.Split(',').Length.ToString());

                if (j == 11 || j == 10)
                {
                    long TotalDelay = 0;        //計算各個schedule測試時間>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    long RepeatTime = 0;
                    button_Start.Enabled = true;
                    for (int z = 0; z < DataGridView_Schedule.Rows.Count - 1; z++)
                    {
                        if (DataGridView_Schedule.Rows[z].Cells[9].Value.ToString() != "")
                        {
                            if (DataGridView_Schedule.Rows[z].Cells[2].Value.ToString() != "")
                            {
                                RepeatTime = (long.Parse(DataGridView_Schedule.Rows[z].Cells[1].Value.ToString())) * (long.Parse(DataGridView_Schedule.Rows[z].Cells[2].Value.ToString()));
                            }
                            TotalDelay += (long.Parse(DataGridView_Schedule.Rows[z].Cells[9].Value.ToString()) + RepeatTime);
                            RepeatTime = 0;
                        }
                    }       //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

                    if (ini12.INIRead(Global.MainSettingPath, "Record", "EachVideo", "") == "1")
                    {
                        ConvertToRealTime(((TotalDelay * Global.Schedule_Loop) + 63000) / 1000);
                    }
                    else
                    {
                        ConvertToRealTime((TotalDelay * Global.Schedule_Loop) / 1000);
                    }

                    switch (Global.Schedule_Number)
                    {
                        case 1:
                            Global.Schedule_1_TestTime = (TotalDelay * Global.Schedule_Loop) / 1000;
                            timeCount = Global.Schedule_1_TestTime;
                            break;
                        case 2:
                            Global.Schedule_2_TestTime = (TotalDelay * Global.Schedule_Loop) / 1000;
                            timeCount = Global.Schedule_2_TestTime;
                            break;
                        case 3:
                            Global.Schedule_3_TestTime = (TotalDelay * Global.Schedule_Loop) / 1000;
                            timeCount = Global.Schedule_3_TestTime;
                            break;
                        case 4:
                            Global.Schedule_4_TestTime = (TotalDelay * Global.Schedule_Loop) / 1000;
                            timeCount = Global.Schedule_4_TestTime;
                            break;
                        case 5:
                            Global.Schedule_5_TestTime = (TotalDelay * Global.Schedule_Loop) / 1000;
                            timeCount = Global.Schedule_5_TestTime;
                            break;
                    }       //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                }
                else
                {
                    button_Start.Enabled = false;
                    MessageBox.Show("This csv file format error", "File Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        public static bool IsFileLocked(string file)
        {
            try
            {
                using (File.Open(file, FileMode.Open, FileAccess.Write, FileShare.None))
                {
                    return false;
                }
            }
            catch (IOException exception)
            {
                var errorCode = Marshal.GetHRForException(exception) & 65535;
                return errorCode == 32 || errorCode == 33;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region -- 測試時間 --
        private string ConvertToRealTime(long iMs)
        {
            long ms, s, h, d = new int();
            ms = 0; s = 0; h = 0; d = 0;
            string sResult = "";
            try
            {
                ms = iMs % 60;
                if (iMs >= 60)
                {
                    s = iMs / 60;
                    if (s >= 60)
                    {
                        h = s / 60;
                        s = s % 60;
                        if (h >= 24)
                        {
                            d = (h) / 24;
                            h = h % 24;
                        }
                    }
                }
                label_ScheduleTime_Value.Text = d.ToString("0") + "d " + h.ToString("0") + "h " + s.ToString("0") + "m " + ms.ToString("0") + "s";
                ini12.INIWrite(Global.MailSettingPath, "Total Test Time", "value", d.ToString("0") + "d " + h.ToString("0") + "h " + s.ToString("0") + "m " + ms.ToString("0") + "s");

                // 寫入每個Schedule test time
                if (Global.Schedule_Number == 1)
                    ini12.INIWrite(Global.MailSettingPath, "Total Test Time", "value1", d.ToString("0") + "d " + h.ToString("0") + "h " + s.ToString("0") + "m " + ms.ToString("0") + "s");

                if (StartButtonPressed == true)
                {
                    switch (Global.Schedule_Number)
                    {
                        case 2:
                            ini12.INIWrite(Global.MailSettingPath, "Total Test Time", "value2", d.ToString("0") + "d " + h.ToString("0") + "h " + s.ToString("0") + "m " + ms.ToString("0") + "s");
                            break;
                        case 3:
                            ini12.INIWrite(Global.MailSettingPath, "Total Test Time", "value3", d.ToString("0") + "d " + h.ToString("0") + "h " + s.ToString("0") + "m " + ms.ToString("0") + "s");
                            break;
                        case 4:
                            ini12.INIWrite(Global.MailSettingPath, "Total Test Time", "value4", d.ToString("0") + "d " + h.ToString("0") + "h " + s.ToString("0") + "m " + ms.ToString("0") + "s");
                            break;
                        case 5:
                            ini12.INIWrite(Global.MailSettingPath, "Total Test Time", "value5", d.ToString("0") + "d " + h.ToString("0") + "h " + s.ToString("0") + "m " + ms.ToString("0") + "s");
                            break;
                    }
                }
            }
            catch
            {
                sResult = "Error!";
            }
            return sResult;
        }
        #endregion

        #region -- UI相關 --
        /*
        #region 陰影
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    cp.ClassStyle |= CS_DROPSHADOW;
                }
                return cp;
            }
        }
        #endregion
        */
        #region -- 關閉、縮小按鈕 --
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
            CloseDtplay();
            CloseAutobox();
        }

        private void MiniPicBox_Enter(object sender, EventArgs e)
        {
            MiniPicBox.Image = Properties.Resources.mini2;
        }

        private void MiniPicBox_Leave(object sender, EventArgs e)
        {
            MiniPicBox.Image = Properties.Resources.mini1;
        }

        private void MiniPicBox_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region -- 滑鼠拖曳視窗 --
        private void GPanelTitleBack_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);        //調用移動無窗體控件函數
        }
        #endregion

        #endregion

        private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            DataGridView_Schedule.CausesValidation = false;
        }

        private void DataBtn_Click(object sender, EventArgs e)            //背景執行填入測試步驟然後匯出reprot>>>>>>>>>>>>>
        {
            //Form_DGV_Autobox.ShowDialog();
        }

        private void PauseButton_Click(object sender, EventArgs e)      //暫停SCHEDULE
        {
            Pause = !Pause;

            if (Pause == true)
            {
                button_Pause.Text = "RESUME";
                button_Start.Enabled = false;
                SchedulePause.Reset();
            }
            else
            {
                button_Pause.Text = "PAUSE";
                button_Start.Enabled = true;
                SchedulePause.Set();
                timer1.Start();
            }
        }
        
        private void Timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Interval = 1000;

            if (timeCount > 0)
            {
                label_ScheduleTime_Value.Text = (--timeCount).ToString();
                ConvertToRealTime(timeCount);
            }
            
            TestTime++;
            long ms, s, h, d = new int();
            ms = 0; s = 0; h = 0; d = 0;

            ms = TestTime % 60;
            if (TestTime >= 60)
            {
                s = TestTime / 60;
                if (s >= 60)
                {
                    h = s / 60;
                    s = s % 60;
                    if (h >= 24)
                    {
                        d = (h) / 24;
                        h = h % 24;
                    }
                }
            }

            label_TestTime_Value.Text = d.ToString("0") + "d " + h.ToString("0") + "h " + s.ToString("0") + "m " + ms.ToString("0") + "s";
            ini12.INIWrite(Global.MailSettingPath, "Total Test Time", "How Long", d.ToString("0") + "d " + h.ToString("0") + "h " + s.ToString("0") + "m " + ms.ToString("0") + "s");
        }

        private void TimerPanelbutton_Click(object sender, EventArgs e)
        {
            TimerPanel = !TimerPanel;

            if (TimerPanel == true)
            {
                panel1.Show();
                panel1.BringToFront();
            }
            else
                panel1.Hide();
        }

        static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
                collection = searcher.Get();

            foreach (var device in collection)
            {
                devices.Add(new USBDeviceInfo(
                (string)device.GetPropertyValue("DeviceID"),
                (string)device.GetPropertyValue("PNPDeviceID"),
                (string)device.GetPropertyValue("Description")
                ));
            }

            collection.Dispose();
            return devices;
        }

        class USBDeviceInfo
        {
            public USBDeviceInfo(string deviceID, string pnpDeviceID, string description)
            {
                DeviceID = deviceID;
                PnpDeviceID = pnpDeviceID;
                Description = description;
            }
            public string DeviceID { get; private set; }
            public string PnpDeviceID { get; private set; }
            public string Description { get; private set; }
        }

        //釋放記憶體//
        [System.Runtime.InteropServices.DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);
        private void DisposeRam()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseAutobox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //UInt32 gpio_input_value;
            //MyBlueRat.Get_GPIO_Input(out gpio_input_value);
            //byte GPIO_Read_Data = Convert.ToByte(gpio_input_value & 0xff);
            //labelGPIO_Input.Text = "GPIO_IN:" + GPIO_Read_Data.ToString();
            //Console.WriteLine("GPIO_IN:" + GPIO_Read_Data.ToString());

            UInt32 GPIO_input_value, retry_cnt;
            bool bRet = false;
            retry_cnt = 3;
            do
            {
                String modified0 = "";
                bRet = MyBlueRat.Get_GPIO_Input(out GPIO_input_value);

                if (GPIO_input_value == 31)
                {
                    modified0 = "0" + Convert.ToString(31, 2);
                }
                else
                {
                    modified0 = Convert.ToString(GPIO_input_value, 2);
                }

                string modified1 = modified0.Insert(1, ",");
                string modified2 = modified1.Insert(3, ",");
                string modified3 = modified2.Insert(5, ",");
                string modified4 = modified3.Insert(7, ",");
                string modified5 = modified4.Insert(9, ",");

                Global.IO_INPUT = modified5;
                Console.WriteLine(Global.IO_INPUT);
                Console.WriteLine(Global.IO_INPUT.Substring(0, 1));
            }
            while ((bRet == false) && (--retry_cnt > 0));

            if (bRet)
            {
                labelGPIO_Input.Text = "GPIO_input: " + GPIO_input_value.ToString();
            }
            else
            {
                labelGPIO_Input.Text = "GPIO_input fail after retry";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string GPIO = "01010101";
            //byte GPIO_B = Convert.ToByte(GPIO, 2);
            //MyBlueRat.Set_GPIO_Output(GPIO_B);

            Graphics graphics = this.CreateGraphics();
            Console.WriteLine("dpiX = " + graphics.DpiX);
            Console.WriteLine("dpiY = " + graphics.DpiY);
            Console.WriteLine("-----------");
            Console.WriteLine("height = " + this.Size.Height);
            Console.WriteLine("width = " + this.Size.Width);
        }

        #region -- GPIO --
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool DeviceIoControl(SafeFileHandle hDevice,
                                                   uint dwIoControlCode,
                                                   ref uint InBuffer,
                                                   int nInBufferSize,
                                                   byte[] OutBuffer,
                                                   UInt32 nOutBufferSize,
                                                   ref UInt32 out_count,
                                                   IntPtr lpOverlapped);
        public SafeFileHandle hCOM;

        public const uint FILE_DEVICE_UNKNOWN = 0x00000022;
        public const uint USB2SER_IOCTL_INDEX = 0x0800;
        public const uint METHOD_BUFFERED = 0;
        public const uint FILE_ANY_ACCESS = 0;

        public bool PowerState;
        public bool USBState;

        public static uint GP0_SET_VALUE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 22, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public static uint GP1_SET_VALUE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 23, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public static uint GP2_SET_VALUE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 47, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public static uint GP3_SET_VALUE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 48, METHOD_BUFFERED, FILE_ANY_ACCESS);

        public static uint GP0_GET_VALUE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 24, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public static uint GP1_GET_VALUE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 25, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public static uint GP2_GET_VALUE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 49, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public static uint GP3_GET_VALUE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 50, METHOD_BUFFERED, FILE_ANY_ACCESS);

        public static uint GP0_OUTPUT_ENABLE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 20, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public static uint GP1_OUTPUT_ENABLE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 21, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public static uint GP2_OUTPUT_ENABLE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 45, METHOD_BUFFERED, FILE_ANY_ACCESS);
        public static uint GP3_OUTPUT_ENABLE = CTL_CODE(FILE_DEVICE_UNKNOWN, USB2SER_IOCTL_INDEX + 46, METHOD_BUFFERED, FILE_ANY_ACCESS);

        static uint CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
        {
            return ((DeviceType << 16) | (Access << 14) | (Function << 2) | Method);
        }

        #region -- GP0 --
        public bool PL2303_GP0_Enable(SafeFileHandle hDrv, uint enable)
        {
            UInt32 nBytes = 0;
            bool bSuccess = DeviceIoControl(hDrv, GP0_OUTPUT_ENABLE,
            ref enable, sizeof(byte), null, 0, ref nBytes, IntPtr.Zero);
            return bSuccess;
        }
        public bool PL2303_GP0_SetValue(SafeFileHandle hDrv, uint val)
        {
            UInt32 nBytes = 0;
            byte[] addr = new byte[6];
            bool bSuccess = DeviceIoControl(hDrv, GP0_SET_VALUE, ref val, sizeof(uint), null, 0, ref nBytes, IntPtr.Zero);
            return bSuccess;
        }
        #endregion

        #region -- GP1 --
        public bool PL2303_GP1_Enable(SafeFileHandle hDrv, uint enable)
        {
            UInt32 nBytes = 0;
            bool bSuccess = DeviceIoControl(hDrv, GP1_OUTPUT_ENABLE,
            ref enable, sizeof(byte), null, 0, ref nBytes, IntPtr.Zero);
            return bSuccess;
        }
        public bool PL2303_GP1_SetValue(SafeFileHandle hDrv, uint val)
        {
            UInt32 nBytes = 0;
            byte[] addr = new byte[6];
            bool bSuccess = DeviceIoControl(hDrv, GP1_SET_VALUE, ref val, sizeof(uint), null, 0, ref nBytes, IntPtr.Zero);
            return bSuccess;
        }
        #endregion

        #region -- GP2 --
        public bool PL2303_GP2_Enable(SafeFileHandle hDrv, uint enable)
        {
            UInt32 nBytes = 0;
            bool bSuccess = DeviceIoControl(hDrv, GP2_OUTPUT_ENABLE,
            ref enable, sizeof(byte), null, 0, ref nBytes, IntPtr.Zero);
            return bSuccess;
        }
        public bool PL2303_GP2_SetValue(SafeFileHandle hDrv, uint val)
        {
            UInt32 nBytes = 0;
            byte[] addr = new byte[6];
            bool bSuccess = DeviceIoControl(hDrv, GP2_SET_VALUE, ref val, sizeof(uint), null, 0, ref nBytes, IntPtr.Zero);
            return bSuccess;
        }
        #endregion

        #region -- GP3 --
        public bool PL2303_GP3_Enable(SafeFileHandle hDrv, uint enable)
        {
            UInt32 nBytes = 0;
            bool bSuccess = DeviceIoControl(hDrv, GP3_OUTPUT_ENABLE,
            ref enable, sizeof(byte), null, 0, ref nBytes, IntPtr.Zero);
            return bSuccess;
        }
        public bool PL2303_GP3_SetValue(SafeFileHandle hDrv, uint val)
        {
            UInt32 nBytes = 0;
            byte[] addr = new byte[6];
            bool bSuccess = DeviceIoControl(hDrv, GP3_SET_VALUE, ref val, sizeof(uint), null, 0, ref nBytes, IntPtr.Zero);
            return bSuccess;
        }
        #endregion

        private void GP0_GP1_AC_ON()
        {
            byte[] val1 = new byte[2];
            val1[0] = 0;
            uint val = (uint)int.Parse("1");

            bool Success_GP0_Enable = PL2303_GP0_Enable(hCOM, 1);
            bool Success_GP0_SetValue = PL2303_GP0_SetValue(hCOM, val);

            bool Success_GP1_Enable = PL2303_GP1_Enable(hCOM, 1);
            bool Success_GP1_SetValue = PL2303_GP1_SetValue(hCOM, val);

            PowerState = true;
            pictureBox_AcPower.Image = Properties.Resources.ON;
        }

        private void GP0_GP1_AC_OFF_ON()
        {
            if (StartButtonPressed == true)
            {
                // 電源開或關
                byte[] val1;
                val1 = new byte[2];
                val1[0] = 0;

                bool Success_GP0_Enable = PL2303_GP0_Enable(hCOM, 1);
                bool Success_GP1_Enable = PL2303_GP1_Enable(hCOM, 1);
                if (Success_GP0_Enable && Success_GP1_Enable && PowerState == false)
                {
                    uint val;
                    val = (uint)int.Parse("1");
                    bool Success_GP0_SetValue = PL2303_GP0_SetValue(hCOM, val);
                    bool Success_GP1_SetValue = PL2303_GP1_SetValue(hCOM, val);
                    if (Success_GP0_SetValue && Success_GP1_SetValue)
                    {
                        {
                            PowerState = true;
                            pictureBox_AcPower.Image = Properties.Resources.ON;
                        }
                    }
                }
                else if (Success_GP0_Enable && Success_GP1_Enable && PowerState == true)
                {
                    uint val;
                    val = (uint)int.Parse("0");
                    bool Success_GP0_SetValue = PL2303_GP0_SetValue(hCOM, val);
                    bool Success_GP1_SetValue = PL2303_GP1_SetValue(hCOM, val);
                    if (Success_GP0_SetValue && Success_GP1_SetValue)
                    {
                        {
                            PowerState = false;
                            pictureBox_AcPower.Image = Properties.Resources.OFF;
                        }
                    }
                }
            }
        }

        private void GP2_GP3_USB_PC()
        {
            byte[] val1 = new byte[2];
            val1[0] = 0;
            uint val = (uint)int.Parse("0");

            bool Success_GP2_Enable = PL2303_GP2_Enable(hCOM, 1);
            bool Success_GP2_SetValue = PL2303_GP2_SetValue(hCOM, val);

            bool Success_GP3_Enable = PL2303_GP3_Enable(hCOM, 1);
            bool Success_GP3_SetValue = PL2303_GP3_SetValue(hCOM, val);

            USBState = true;
        }

        private void IO_INPUT()
        {
            UInt32 GPIO_input_value, retry_cnt;
            bool bRet = false;
            retry_cnt = 3;
            do
            {
                String modified0 = "";
                bRet = MyBlueRat.Get_GPIO_Input(out GPIO_input_value);
                if (Convert.ToString(GPIO_input_value, 2).Length == 5)
                {
                    modified0 = "0" + Convert.ToString(GPIO_input_value, 2);
                }
                else if (Convert.ToString(GPIO_input_value, 2).Length == 4)
                {
                    modified0 = "0" + "0" + Convert.ToString(GPIO_input_value, 2);
                }
                else if (Convert.ToString(GPIO_input_value, 2).Length == 3)
                {
                    modified0 = "0" + "0" + "0" + Convert.ToString(GPIO_input_value, 2);
                }
                else if (Convert.ToString(GPIO_input_value, 2).Length == 2)
                {
                    modified0 = "0" + "0" + "0" + "0" + Convert.ToString(GPIO_input_value, 2);
                }
                else if (Convert.ToString(GPIO_input_value, 2).Length == 1)
                {
                    modified0 = "0" + "0" + "0" + "0" + "0" + Convert.ToString(GPIO_input_value, 2);
                }
                else
                {
                    modified0 = Convert.ToString(GPIO_input_value, 2);
                }

                string modified1 = modified0.Insert(1, ",");
                string modified2 = modified1.Insert(3, ",");
                string modified3 = modified2.Insert(5, ",");
                string modified4 = modified3.Insert(7, ",");
                string modified5 = modified4.Insert(9, ",");

                Global.IO_INPUT = modified5;
            }
            while ((bRet == false) && (--retry_cnt > 0));

            if (bRet)
            {
                labelGPIO_Input.Text = "GPIO_input: " + GPIO_input_value.ToString();
            }
            else
            {
                labelGPIO_Input.Text = "GPIO_input fail after retry";
            }
        }
        #endregion
        
        private void button_VirtualRC_Click(object sender, EventArgs e)
        {
            FormRC formRC = new FormRC();
            formRC.Owner = this;
            if (Global.FormRC == false)
            {
                formRC.Show();
            }
        }

        private void button_AcUsb_Click(object sender, EventArgs e)
        {
            AcUsbPanel = !AcUsbPanel;

            if (AcUsbPanel == true)
            {
                panel_AcUsb.Show();
                panel_AcUsb.BringToFront();
            }
            else
            {
                panel_AcUsb.Hide();
            }
        }

        private void pictureBox_Ac1_Click(object sender, EventArgs e)
        {
            byte[] val1 = new byte[2];
            val1[0] = 0;

            bool jSuccess = PL2303_GP0_Enable(hCOM, 1);
            if (PowerState == false) //Set GPIO Value as 1
            {
                uint val;
                val = (uint)int.Parse("1");
                bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                if (bSuccess == true)
                {
                    {
                        PowerState = true;
                        pictureBox_Ac1.Image = Properties.Resources.Switch_On_AC;
                    }
                }
            }
            else if (PowerState == true) //Set GPIO Value as 0
            {
                uint val;
                val = (uint)int.Parse("0");
                bool bSuccess = PL2303_GP0_SetValue(hCOM, val);
                if (bSuccess == true)
                {
                    {
                        PowerState = false;
                        pictureBox_Ac1.Image = Properties.Resources.Switch_Off_AC;
                    }
                }
            }
        }

        private void pictureBox_Ac2_Click(object sender, EventArgs e)
        {
            byte[] val1 = new byte[2];
            val1[0] = 0;

            bool jSuccess = PL2303_GP1_Enable(hCOM, 1);
            if (PowerState == false) //Set GPIO Value as 1
            {
                uint val;
                val = (uint)int.Parse("1");
                bool bSuccess = PL2303_GP1_SetValue(hCOM, val);
                if (bSuccess == true)
                {
                    {
                        PowerState = true;
                        pictureBox_Ac2.Image = Properties.Resources.Switch_On_AC;
                    }
                }
            }
            else if (PowerState == true) //Set GPIO Value as 0
            {
                uint val;
                val = (uint)int.Parse("0");
                bool bSuccess = PL2303_GP1_SetValue(hCOM, val);
                if (bSuccess == true)
                {
                    {
                        PowerState = false;
                        pictureBox_Ac2.Image = Properties.Resources.Switch_Off_AC;
                    }
                }
            }
        }

        private void pictureBox_Usb1_Click(object sender, EventArgs e)
        {
            byte[] val1 = new byte[2];
            val1[0] = 0;

            bool jSuccess = PL2303_GP2_Enable(hCOM, 1);
            if (USBState == true) //Set GPIO Value as 1
            {
                uint val;
                val = (uint)int.Parse("1");
                bool bSuccess = PL2303_GP2_SetValue(hCOM, val);
                if (bSuccess == true)
                {
                    {
                        USBState = false;
                        pictureBox_Usb1.Image = Properties.Resources.Switch_to_TV;
                    }
                }
            }
            else if (USBState == false) //Set GPIO Value as 0
            {
                uint val;
                val = (uint)int.Parse("0");
                bool bSuccess = PL2303_GP2_SetValue(hCOM, val);
                if (bSuccess == true)
                {
                    {
                        USBState = true;
                        pictureBox_Usb1.Image = Properties.Resources.Switch_to_PC;
                    }
                }
            }
        }

        private void pictureBox_Usb2_Click(object sender, EventArgs e)
        {
            byte[] val1 = new byte[2];
            val1[0] = 0;

            bool jSuccess = PL2303_GP3_Enable(hCOM, 1);
            if (USBState == true) //Set GPIO Value as 1
            {
                uint val;
                val = (uint)int.Parse("1");
                bool bSuccess = PL2303_GP3_SetValue(hCOM, val);
                if (bSuccess == true)
                {
                    {
                        USBState = false;
                        pictureBox_Usb2.Image = Properties.Resources.Switch_to_TV;
                    }
                }
            }
            else if (USBState == false) //Set GPIO Value as 0
            {
                uint val;
                val = (uint)int.Parse("0");
                bool bSuccess = PL2303_GP3_SetValue(hCOM, val);
                if (bSuccess == true)
                {
                    {
                        USBState = true;
                        pictureBox_Usb2.Image = Properties.Resources.Switch_to_PC;
                    }
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (ini12.INIRead(Global.MainSettingPath, "Device", "RunAfterStartUp", "") == "1")
            {
                button_Start.PerformClick();
            }
        }

        private void DataGridView_Schedule_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            FormScriptHelper formScriptHelper = new FormScriptHelper();
            formScriptHelper.Owner = this;

            try
            {
                if (DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString() == "_cmd" &&
                    DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText == "Picture" ||
                    DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString() == "_cmd" &&
                    DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText == ">Video Recording\r\n>Dektec" ||
                    DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString() == "_cmd" &&
                    DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText == ">AC/USB Switch\r\n>Stream Name" ||

                    DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString() == "_log1" &&
                    DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText == ">SerialPort\r\n>IO & Keyword" ||
                    DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString() == "_log2" &&
                    DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText == ">SerialPort\r\n>IO & Keyword")
                {
                    formScriptHelper.RCKeyForm1 = DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString();
                    formScriptHelper.SetValue(DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText);
                    formScriptHelper.ShowDialog();

                    DataGridView_Schedule[DataGridView_Schedule.CurrentCell.ColumnIndex,
                                          DataGridView_Schedule.CurrentCell.RowIndex].Value = strValue;
                    DataGridView_Schedule.RefreshEdit();
                }

                if (DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString().Length >= 3)
                {
                    if (DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 3) == "_PA" &&
                    DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText == ">SerialPort\r\n>IO & Keyword" ||
                    DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 3) == "_PB" &&
                    DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText == ">SerialPort\r\n>IO & Keyword")
                    {
                        formScriptHelper.RCKeyForm1 = DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 3);
                        formScriptHelper.SetValue(DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText);
                        formScriptHelper.ShowDialog();

                        DataGridView_Schedule[DataGridView_Schedule.CurrentCell.ColumnIndex,
                                              DataGridView_Schedule.CurrentCell.RowIndex].Value = strValue;
                        DataGridView_Schedule.RefreshEdit();
                    }
                }

                if (DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString().Length >= 8)
                {
                    if (DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 8) == "_keyword" &&
                    DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText == ">SerialPort\r\n>IO & Keyword")
                    {
                        formScriptHelper.RCKeyForm1 = DataGridView_Schedule.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(0, 8);
                        formScriptHelper.SetValue(DataGridView_Schedule.Columns[e.ColumnIndex].HeaderText);
                        formScriptHelper.ShowDialog();

                        DataGridView_Schedule[DataGridView_Schedule.CurrentCell.ColumnIndex,
                                              DataGridView_Schedule.CurrentCell.RowIndex].Value = strValue;
                        DataGridView_Schedule.RefreshEdit();
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
                MessageBox.Show("RC Key is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            
        }

        private string strValue;
        public string StrValue
        {
            set
            {
                strValue = value;
            }
        }

        private void comboBox_CameraDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MainSettingPath, "Camera", "VideoIndex", comboBox_CameraDevice.SelectedIndex.ToString());
            if (_captureInProgress == true)
            {
                capture.Stop();
                capture.Dispose();
                Camstart();
            }
        }

        private void DataGridView_Schedule_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Nowpoint = DataGridView_Schedule.Rows[e.RowIndex].Index;

            if (Breakfunction == true && Nowpoint != Breakpoint)
            {
                DataGridView_Schedule.Rows[Breakpoint].DefaultCellStyle.BackColor = Color.White;
                DataGridView_Schedule.Rows[Breakpoint].DefaultCellStyle.SelectionBackColor = Color.PeachPuff;
                DataGridView_Schedule.Rows[Breakpoint].DefaultCellStyle.SelectionForeColor = Color.Black;
                DataGridView_Schedule.Rows[Nowpoint].DefaultCellStyle.BackColor = Color.Yellow;
                DataGridView_Schedule.Rows[Nowpoint].DefaultCellStyle.SelectionBackColor = Color.Yellow;
                DataGridView_Schedule.Rows[Nowpoint].DefaultCellStyle.SelectionForeColor = Color.Red;
                Breakpoint = Nowpoint;
                Console.WriteLine("Change the Nowpoint");
            }
            else if (Breakfunction == true && Nowpoint == Breakpoint)
            {
                Breakfunction = false;
                DataGridView_Schedule.Rows[Breakpoint].DefaultCellStyle.BackColor = Color.White;
                DataGridView_Schedule.Rows[Breakpoint].DefaultCellStyle.SelectionBackColor = Color.PeachPuff;
                DataGridView_Schedule.Rows[Breakpoint].DefaultCellStyle.SelectionForeColor = Color.Black;
                DataGridView_Schedule.Rows[Nowpoint].DefaultCellStyle.SelectionBackColor = Color.PeachPuff;
                DataGridView_Schedule.Rows[Nowpoint].DefaultCellStyle.SelectionForeColor = Color.Black;
                Breakpoint = -1;
                Console.WriteLine("Disable the Breakfunction");
            }
            else
            {
                Breakfunction = true;
				Breakpoint = Nowpoint;
                DataGridView_Schedule.Rows[Breakpoint].DefaultCellStyle.BackColor = Color.Yellow;
                DataGridView_Schedule.Rows[Breakpoint].DefaultCellStyle.SelectionBackColor = Color.Yellow;
                DataGridView_Schedule.Rows[Breakpoint].DefaultCellStyle.SelectionForeColor = Color.Red;
                Console.WriteLine("Enable the Breakfunction");
            }
        }
    }

    public class SafeDataGridView : DataGridView
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                base.OnPaint(e);
            }
            catch
            {
                Invalidate();
            }
        }
    }

    public class Global//全域變數//
    {
        public static string MainSettingPath = Application.StartupPath + "\\Config.ini";
        public static string MailSettingPath = Application.StartupPath + "\\Mail.ini";
        public static string RcSettingPath = Application.StartupPath + "\\RC.ini";

        public static int Scheduler_Row = 0;
        public static List<string> VID = new List<string> { };
        public static List<string> PID = new List<string> { };
        public static List<string> AutoBoxComport = new List<string> { };
        public static int Schedule_Number = 0;
        public static int Schedule_1_Exist = 0;
        public static int Schedule_2_Exist = 0;
        public static int Schedule_3_Exist = 0;
        public static int Schedule_4_Exist = 0;
        public static int Schedule_5_Exist = 0;
        public static long Schedule_1_TestTime = 0;
        public static long Schedule_2_TestTime = 0;
        public static long Schedule_3_TestTime = 0;
        public static long Schedule_4_TestTime = 0;
        public static long Schedule_5_TestTime = 0;
        public static long Total_Test_Time = 0;
        public static int Loop_Number = 0;
        public static int Total_Loop = 0;
        public static int Schedule_Loop = 999999;
        public static int Schedule_Step;
        public static int caption_Num = 0;
        public static int caption_Sum = 0;
        public static int excel_Num = 0;
        public static int[] caption_NG_Num = new int[Schedule_Loop];
        public static int[] caption_Total_Num = new int[Schedule_Loop];
        public static float[] SumValue = new float[Schedule_Loop];
        public static int[] NGValue = new int[Global.Schedule_Loop];
        public static float[] NGRateValue = new float[Global.Schedule_Loop];
        //public static float[] ReferenceResult = new float[Schedule_Loop];
        public static bool FormSetting = true;
        public static bool FormSchedule = true;
        public static bool FormMail = true;
        public static bool FormLog = true;
        public static string RCDB = "";
        public static string IO_INPUT = "";
        public static int IO_PA10_0_COUNT = 0;
        public static int IO_PA10_1_COUNT = 0;
        public static int IO_PA11_0_COUNT = 0;
        public static int IO_PA11_1_COUNT = 0;
        public static int IO_PA14_0_COUNT = 0;
        public static int IO_PA14_1_COUNT = 0;
        public static int IO_PA15_0_COUNT = 0;
        public static int IO_PA15_1_COUNT = 0;
        public static int IO_PB1_0_COUNT = 0;
        public static int IO_PB1_1_COUNT = 0;
        public static int IO_PB7_0_COUNT = 0;
        public static int IO_PB7_1_COUNT = 0;
        public static string keyword_1 = "false";
        public static string keyword_2 = "false";
        public static string keyword_3 = "false";
        public static string keyword_4 = "false";
        public static string keyword_5 = "false";
        public static string keyword_6 = "false";
        public static string keyword_7 = "false";
        public static string keyword_8 = "false";
        public static string keyword_9 = "false";
        public static string keyword_10 = "false";
        public static List<string> Rc_List = new List<string> { };
        public static int Rc_Number = 0;
        public static string Pass_Or_Fail = "";//測試結果//
        public static int Break_Out_Schedule = 0;//定時器中斷變數//
        public static int Break_Out_MyRunCamd;//是否跳出倒數迴圈，1為跳出//
        public static bool FormRC = false;

        //MessageBox.Show("RC Key is empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);//MessageBox範例
    }
}
