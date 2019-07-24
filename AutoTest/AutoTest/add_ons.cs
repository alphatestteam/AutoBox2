using jini;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace AutoTest
{
    class Add_ons
    {
        string MonkeyTestPath = Application.StartupPath + "\\Monkey_Test.ini";

        #region MonketTest指令
        public void MonkeyTest()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            string strOutput = null;

            p.Start();

            p.StandardInput.WriteLine("D:");
            p.StandardInput.WriteLine("cd \\adb");
            p.StandardInput.WriteLine("adb kill-server");

            //選擇連線模式
            if(ini12.INIRead(MonkeyTestPath, "Monket Test", "Connection Mode", "") == "Ethernet")
                p.StandardInput.WriteLine("adb connect " + ini12.INIRead(MonkeyTestPath, "Monket Test", "TV IP", ""));
            if(ini12.INIRead(MonkeyTestPath, "Monket Test", "Connection Mode", "") == "USB")
                p.StandardInput.WriteLine("adb devices");

            if (ini12.INIRead(MonkeyTestPath, "Monket Test", "Basic Monkey", "") == "1")
                p.StandardInput.WriteLine("adb shell monkey --ignore-crashes --ignore-timeouts -v -v -v 50000 > " + ini12.INIRead(MonkeyTestPath, "Monket Test", "Path", "") + "\\logcat.txt");

            else if (ini12.INIRead(MonkeyTestPath, "Monket Test", "Specified Monkey", "") == "1"  && ini12.INIRead(MonkeyTestPath, "Monket Test", "Package", "") != "")
                p.StandardInput.WriteLine("adb shell monkey -p " + ini12.INIRead(MonkeyTestPath, "Monket Test", "Package", "") + " --ignore-crashes --ignore-timeouts -v -v -v 500 > " + ini12.INIRead(MonkeyTestPath, "Monket Test", "Path", "") + "\\logcat.txt");

            else if (ini12.INIRead(MonkeyTestPath, "Monket Test", "MonkeyRunner", "") == "1")
            {
                p.StandardInput.WriteLine("cd " + ini12.INIRead(MonkeyTestPath, "Monket Test", "SDK Tools", ""));
                p.StandardInput.WriteLine("monkeyrunner " + ini12.INIRead(MonkeyTestPath, "Monket Test", "MonkeyRunner Scheduler", ""));
            }
            else
                p.StandardInput.WriteLine("adb shell pm list packages > " + Application.StartupPath + "\\app.txt");
                //p.StandardInput.WriteLine("adb shell pm list packages > D:\\app.txt");

            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("exit");  //不加exit便不會輸出到console
            strOutput = p.StandardOutput.ReadToEnd();
            Console.WriteLine(strOutput);

            p.WaitForExit();
            p.Close();
        }
        #endregion

        private void SearchMonkeyLog()
        {
            string FileName = @"d:\5.5.5.101_5555_Log_18.txt";
            string[] filelist = File.ReadAllLines(FileName, Encoding.Default);
            List<string> StringLists = new List<string>();
            
            for (int linenum = filelist.Length - 1; linenum >= 0; linenum--)
            {
                if (filelist[linenum].IndexOf("ANR") > -1)
                {
                    int first = filelist[linenum].IndexOf("ANR in ") + "ANR in ".Length;
                    int last = filelist[linenum].LastIndexOf(" (");
                    string str2 = filelist[linenum].Substring(first, last - first);
                    StringLists.Add(str2);
                }
            }
            
            System.IO.StreamWriter sw = new System.IO.StreamWriter(@"d:\ANR_LIST.txt"); // open the file for streamwriter
            foreach (string myStringList in StringLists)
            {
                sw.WriteLine(myStringList); // output the reslut to the file (WriteLine or Write)
            }
            sw.Close(); // close the file

            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string myStringList in StringLists)
            {
                if (dict.ContainsKey(myStringList))
                {
                    //如果Dictionary中存在这个关键词元素，则把这个Dictionary的key+1
                    dict[myStringList]++;
                }
                else
                {
                    //如果Dictionary中不存在这个关键词元素，则把它添加进Dictionary
                    dict.Add(myStringList, 1);
                }
            }

            foreach (KeyValuePair<string, int> item in dict)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine(item.Value);
            }
        }

        #region -- 產生Monkey Test Excel報告 --
        public void CreateExcelFile()
        {
            //string FileName = @"d:\5.5.5.101_5555_Log_18.txt";
            string FileName = ini12.INIRead(MonkeyTestPath, "Monket Test", "Path", "") + @"\logcat.txt";
            string[] filelist = File.ReadAllLines(FileName, Encoding.Default);
            List<string> StringLists = new List<string>();

            for (int linenum = filelist.Length - 1; linenum >= 0; linenum--)
            {
                if (filelist[linenum].IndexOf("ANR") > -1)
                {
                    int first = filelist[linenum].IndexOf("ANR in ") + "ANR in ".Length;
                    int last = filelist[linenum].LastIndexOf(" (");
                    string str2 = filelist[linenum].Substring(first, last - first);
                    StringLists.Add(str2);
                    Console.WriteLine(str2);
                }
            }
            
            ////建立Excel 2007檔案
            IWorkbook workbook = new NPOI.XSSF.UserModel.XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            //合併區
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 1, 2));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 1, 2));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(2, 2, 1, 2));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(3, 3, 1, 2));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(4, 4, 1, 2));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(5, 5, 1, 2));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(6, 6, 1, 2));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(7, 7, 1, 2));
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(9, 9, 0, 6));   //合併Summary行
            sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(10, 10, 0, 5)); //合併Error List行

            //背景色（藍色）
            ICellStyle cellStyle0 = workbook.CreateCellStyle();
            cellStyle0.FillPattern = FillPattern.SolidForeground;
            cellStyle0.FillForegroundColor = IndexedColors.PaleBlue.Index;
            cellStyle0.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle0.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle0.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle0.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            //背景色（綠色）
            ICellStyle cellStyle1 = workbook.CreateCellStyle();
            cellStyle1.FillPattern = FillPattern.SolidForeground;
            cellStyle1.FillForegroundColor = IndexedColors.Lime.Index;
            cellStyle1.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle1.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle1.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle1.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            //背景色（粉色）
            ICellStyle cellStyle2 = workbook.CreateCellStyle();
            cellStyle2.FillPattern = FillPattern.SolidForeground;
            cellStyle2.FillForegroundColor = IndexedColors.Tan.Index;
            cellStyle2.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle2.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            //背景色（灰色）
            ICellStyle cellStyle3 = workbook.CreateCellStyle();
            cellStyle3.FillPattern = FillPattern.SolidForeground;
            cellStyle3.FillForegroundColor = IndexedColors.Grey25Percent.Index;

            //背景色（白色）
            ICellStyle cellStyle4 = workbook.CreateCellStyle();
            cellStyle4.FillPattern = FillPattern.SolidForeground;
            cellStyle4.FillForegroundColor = IndexedColors.White.Index;

            //Summary儲存格格式
            ICellStyle summaryStyle = workbook.CreateCellStyle();
            IFont summaryFont = workbook.CreateFont();
            summaryFont.FontHeightInPoints = 18;
            summaryStyle.SetFont(summaryFont);
            summaryStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            summaryStyle.FillPattern = FillPattern.SolidForeground;
            summaryStyle.FillForegroundColor = IndexedColors.PaleBlue.Index;

            //A列
            sheet.CreateRow(0).CreateCell(0).SetCellValue("Project Name");
            sheet.CreateRow(1).CreateCell(0).SetCellValue("Model Name");
            sheet.CreateRow(2).CreateCell(0).SetCellValue("Start Time");
            sheet.CreateRow(3).CreateCell(0).SetCellValue("Renew Time");
            sheet.CreateRow(4).CreateCell(0).SetCellValue("SW Build Time");
            sheet.CreateRow(5).CreateCell(0).SetCellValue("Project No.");
            sheet.CreateRow(6).CreateCell(0).SetCellValue("Test Device");
            sheet.CreateRow(7).CreateCell(0).SetCellValue("Tester");
            for (int A = 0; A < 8; A++)
                sheet.GetRow(A).GetCell(0).CellStyle = cellStyle0;

            //E列
            sheet.GetRow(0).CreateCell(4).SetCellValue("Date");
            sheet.GetRow(1).CreateCell(4).SetCellValue("Period (H)");
            sheet.GetRow(2).CreateCell(4).SetCellValue("SW ISSUES");
            sheet.GetRow(3).CreateCell(4).SetCellValue("System Crash");
            sheet.GetRow(4).CreateCell(4).SetCellValue("Result");
            sheet.GetRow(5).CreateCell(4).SetCellValue("MTBF_SW");
            sheet.GetRow(6).CreateCell(4).SetCellValue("MTBF_Crash");
            for (int E = 0; E < 7; E++)
                sheet.GetRow(E).GetCell(4).CellStyle = cellStyle0;
            sheet.GetRow(4).GetCell(4).CellStyle = cellStyle4;

            //F列
            sheet.GetRow(0).CreateCell(5).SetCellValue("-----");
            sheet.GetRow(1).CreateCell(5).SetCellValue("-----");
            sheet.GetRow(2).CreateCell(5).SetCellValue("-----");
            sheet.GetRow(3).CreateCell(5).SetCellValue("-----");
            sheet.GetRow(4).CreateCell(5).SetCellValue("");
            sheet.GetRow(5).CreateCell(5).SetCellValue("-----");
            sheet.GetRow(6).CreateCell(5).SetCellValue("-----");
            for (int F = 0; F < 7; F++)
                sheet.GetRow(F).GetCell(5).CellStyle = cellStyle2;
            sheet.GetRow(4).GetCell(5).CellStyle = cellStyle4;

            //Summary
            sheet.CreateRow(9).CreateCell(0).SetCellValue("Summary");
            sheet.GetRow(9).GetCell(0).CellStyle = summaryStyle;

            //Error List
            sheet.CreateRow(10).CreateCell(0).SetCellValue("Error List");
            sheet.GetRow(10).GetCell(0).CellStyle = cellStyle3;

            //Total
            sheet.GetRow(10).CreateCell(6).SetCellValue("Total");
            sheet.GetRow(10).GetCell(6).CellStyle = cellStyle3;

            //搜尋相同字串並記次
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (string myStringList in StringLists)
            {
                if (dict.ContainsKey(myStringList))
                {
                    //如果Dictionary中存在这个关键词元素，则把这个Dictionary的key+1
                    dict[myStringList]++;
                }
                else
                {
                    //如果Dictionary中不存在这个关键词元素，则把它添加进Dictionary
                    dict.Add(myStringList, 1);
                }
            }

            int rowcnt = dict.Count;
            while (rowcnt != 0)
            {
                foreach (KeyValuePair<string, int> item in dict)
                {
                    Console.WriteLine(item.Key);
                    Console.WriteLine(item.Value);

                    sheet.AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(10 + rowcnt, 10 + rowcnt, 0, 5)); //合併Error List行
                    sheet.CreateRow(10 + rowcnt).CreateCell(0).SetCellValue(item.Key);
                    sheet.GetRow(10 + rowcnt).CreateCell(6).SetCellValue(item.Value);
                    rowcnt--;
                }
            }

            for (int c = 0; c <= 25; c++)
                sheet.AutoSizeColumn(c);

            FileStream file = new FileStream(ini12.INIRead(MonkeyTestPath, "Monket Test", "Path", "") + @"\MonkeyTest Report.xlsx", FileMode.Create);//產生檔案
            workbook.Write(file);
            file.Close();
        }
        #endregion

        #region -- 讀取USB裝置 --
        public void USB_Read()
        {
            //預設AutoBox沒接上
            ini12.INIWrite(Global.MainSettingPath, "Device", "AutoboxExist", "0");
            ini12.INIWrite(Global.MainSettingPath, "Device", "AutoboxPort", "");

            ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity");
            ManagementObjectCollection collection = search.Get();
            var usbList = from u in collection.Cast<ManagementBaseObject>()
                          select new
                          {
                              id = u.GetPropertyValue("DeviceID"),
                              name = u.GetPropertyValue("Name"),
                              description = u.GetPropertyValue("Description"),
                              status = u.GetPropertyValue("Status"),
                              system = u.GetPropertyValue("SystemName"),
                              caption = u.GetPropertyValue("Caption"),
                              pnp = u.GetPropertyValue("PNPDeviceID"),
                          };

            foreach (var usbDevice in usbList)
            {
                string deviceId = (string)usbDevice.id;
                string deviceTp = (string)usbDevice.name;
                string deviecDescription = (string)usbDevice.description;

                string deviceStatus = (string)usbDevice.status;
                string deviceSystem = (string)usbDevice.system;
                string deviceCaption = (string)usbDevice.caption;
                string devicePnp = (string)usbDevice.pnp;

                if (deviecDescription != null)
                {
                    #region 偵測相機
                    if (deviecDescription.IndexOf("USB 視訊裝置", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        deviecDescription.IndexOf("USB 视频设备", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        deviceTp.IndexOf("Webcam", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        deviceTp.IndexOf("Camera", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        if (deviceId.IndexOf("VID_", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            int vidIndex = deviceId.IndexOf("VID_");
                            string startingAtVid = deviceId.Substring(vidIndex + 4); // + 4 to remove "VID_"
                            string vid = startingAtVid.Substring(0, 4); // vid is four characters long
                            Global.VID.Add(vid);
                        }

                        if (deviceId.IndexOf("PID_", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            int pidIndex = deviceId.IndexOf("PID_");
                            string startingAtPid = deviceId.Substring(pidIndex + 4); // + 4 to remove "PID_"
                            string pid = startingAtPid.Substring(0, 4); // pid is four characters long
                            Global.PID.Add(pid);
                        }

                        Console.WriteLine("-----------------Camera------------------");
                        Console.WriteLine("DeviceID: {0}\n" +
                                              "Name: {1}\n" +
                                              "Description: {2}\n" +
                                              "Status: {3}\n" +
                                              "System: {4}\n" +
                                              "Caption: {5}\n" +
                                              "Pnp: {6}\n"
                                              , deviceId, deviceTp, deviecDescription, deviceStatus, deviceSystem, deviceCaption, devicePnp);

                        //Camera存在
                        ini12.INIWrite(Global.MainSettingPath, "Device", "CameraExist", "1");
                    }
                    #endregion

                    #region 偵測AutoBox1
                    if (deviceId.Substring(deviceId.Length - 2, 2) == "&3" &&
                        deviceId.IndexOf("USB\\VID_067B&PID_2303\\", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Console.WriteLine("-----------------AutoBox1------------------");
                        Console.WriteLine("DeviceID: {0}\n" +
                                              "Name: {1}\n" +
                                              "Description: {2}\n" +
                                              "Status: {3}\n" +
                                              "System: {4}\n" +
                                              "Caption: {5}\n" +
                                              "Pnp: {6}\n"
                                              , deviceId, deviceTp, deviecDescription, deviceStatus, deviceSystem, deviceCaption, devicePnp);

                        int FirstIndex = deviceTp.IndexOf("(");
                        string AutoBoxPortSubstring = deviceTp.Substring(FirstIndex + 1);
                        string AutoBoxPort = AutoBoxPortSubstring.Substring(0);

                        int AutoBoxPortLengh = AutoBoxPort.Length;
                        string AutoBoxPortFinal = AutoBoxPort.Remove(AutoBoxPortLengh - 1);
                        
                        if (AutoBoxPortSubstring.Substring(0, 3) == "COM")
                        {
                            ini12.INIWrite(Global.MainSettingPath, "Device", "AutoboxExist", "1");
                            ini12.INIWrite(Global.MainSettingPath, "Device", "AutoboxVerson", "1");
                            ini12.INIWrite(Global.MainSettingPath, "Device", "AutoboxPort", AutoBoxPortFinal);
                        }
                    }
                    #endregion

                    #region 偵測AutoBox2
                    if (deviceId.IndexOf("&0&5", StringComparison.OrdinalIgnoreCase) >= 0 &&
                        deviceId.IndexOf("USB\\VID_067B&PID_2303\\", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Console.WriteLine("-----------------AutoBox2------------------");
                        Console.WriteLine("DeviceID: {0}\n" +
                                              "Name: {1}\n" +
                                              "Description: {2}\n" +
                                              "Status: {3}\n" +
                                              "System: {4}\n" +
                                              "Caption: {5}\n" +
                                              "Pnp: {6}\n"
                                              , deviceId, deviceTp, deviecDescription, deviceStatus, deviceSystem, deviceCaption, devicePnp);

                        int FirstIndex = deviceTp.IndexOf("(");
                        string AutoBoxPortSubstring = deviceTp.Substring(FirstIndex + 1);
                        string AutoBoxPort = AutoBoxPortSubstring.Substring(0);

                        int AutoBoxPortLengh = AutoBoxPort.Length;
                        string AutoBoxPortFinal = AutoBoxPort.Remove(AutoBoxPortLengh - 1);
                        
                        if (AutoBoxPortSubstring.Substring(0, 3) == "COM")
                        {
                            ini12.INIWrite(Global.MainSettingPath, "Device", "AutoboxExist", "1");
                            ini12.INIWrite(Global.MainSettingPath, "Device", "AutoboxVerson", "2");
                            ini12.INIWrite(Global.MainSettingPath, "Device", "AutoboxPort", AutoBoxPortFinal);
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion

        public void SaveSXPReport()
        {
            StreamReader Log = new StreamReader(@"D:\AUTO BOX\LOG\_Log2_20160906132452_1.txt");
            StreamWriter SXP = new StreamWriter(@"D:\AUTO BOX\LOG\SXP.txt");

            string line = string.Empty;
            while ((line = Log.ReadLine()) != null)
            {
                Console.WriteLine(line);
                Console.WriteLine(line.Length);
            }
            Log.Close();
        }

        #region -- 創建Config.ini --
        public void CreateConfig()
        {
            string[] Device = { "AutoboxExist", "AutoboxVerson", "AutoboxPort", "CameraExist", "RedRatExist", "DOS", "RunAfterStartUp" };
            string[] RedRat = { "RedRatIndex", "DBFile", "Brands", "SerialNumber" };
            string[] Camera = { "VideoIndex", "VideoNumber", "VideoName", "AudioIndex", "AudioNumber", "AudioName" };
            string[] Comport = { "Checked", "PortName", "BaudRate", "DataBit", "StopBits" };
            string[] ExtComport = { "Checked", "PortName", "BaudRate", "DataBit", "StopBits" };
            string[] Record = { "VideoPath", "LogPath", "Generator", "CompareChoose", "CompareDifferent", "EachVideo", "ImportDB", "Footprint Mode" };
            string[] Schedule1 = { "Exist", "Loop", "OnTimeStart", "Timer", "Path" };
            string[] Schedule2 = { "Exist", "Loop", "OnTimeStart", "Timer", "Path" };
            string[] Schedule3 = { "Exist", "Loop", "OnTimeStart", "Timer", "Path" };
            string[] Schedule4 = { "Exist", "Loop", "OnTimeStart", "Timer", "Path" };
            string[] Schedule5 = { "Exist", "Loop", "OnTimeStart", "Timer", "Path" };
            string[] LogSearch = { "StartTime", "Comport1", "Comport2", "TextNum", "Camerarecord", "Camerashot", "Sendmail", "Savelog", "Showmessage", "ACcontrol", "Stop", "AC OFF", "Nowvalue",
                                   "Text0", "Text1", "Text2", "Text3", "Text4", "Text5", "Text6", "Text7", "Text8", "Text9",
                                   "Times0", "Times1", "Times2", "Times3", "Times4", "Times5", "Times6", "Times7", "Times8", "Times9",
                                   "Display0", "Display1", "Display2", "Display3", "Display4", "Display5", "Display6", "Display7", "Display8", "Display9" };

            if (File.Exists(Global.MainSettingPath) == false)
            {
                for (int i = 0; i < Device.Length; i++)
                {
                    if (i == (Device.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Device", Device[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Device", Device[i], "");
                    }
                }

                for (int i = 0; i < RedRat.Length; i++)
                {
                    if (i == (RedRat.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "RedRat", RedRat[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "RedRat", RedRat[i], "");
                    }
                }

                for (int i = 0; i < Camera.Length; i++)
                {
                    if (i == (Camera.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Camera", Camera[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Camera", Camera[i], "");
                    }
                }

                for (int i = 0; i < Comport.Length; i++)
                {
                    if (i == (Comport.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Comport", Comport[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Comport", Comport[i], "");
                    }
                }

                for (int i = 0; i < ExtComport.Length; i++)
                {
                    if (i == (ExtComport.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "ExtComport", ExtComport[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "ExtComport", ExtComport[i], "");
                    }
                }

                for (int i = 0; i < Record.Length; i++)
                {
                    if (i == (Record.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Record", Record[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Record", Record[i], "");
                    }
                }

                for (int i = 0; i < Schedule1.Length; i++)
                {
                    if (i == (Schedule1.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule1", Schedule1[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule1", Schedule1[i], "");
                    }
                }

                for (int i = 0; i < Schedule2.Length; i++)
                {
                    if (i == (Schedule2.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule2", Schedule2[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule2", Schedule2[i], "");
                    }
                }

                for (int i = 0; i < Schedule3.Length; i++)
                {
                    if (i == (Schedule3.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule3", Schedule3[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule3", Schedule3[i], "");
                    }
                }

                for (int i = 0; i < Schedule4.Length; i++)
                {
                    if (i == (Schedule4.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule4", Schedule4[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule4", Schedule4[i], "");
                    }
                }

                for (int i = 0; i < Schedule5.Length; i++)
                {
                    if (i == (Schedule5.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule5", Schedule5[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "Schedule5", Schedule5[i], "");
                    }
                }

                for (int i = 0; i < LogSearch.Length; i++)
                {
                    if (i == (LogSearch.Length - 1))
                    {
                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", LogSearch[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MainSettingPath, "LogSearch", LogSearch[i], "");
                    }
                }
            }
        }
        #endregion

        #region -- 創建Mail.ini --
        public void CreateMailConfig()
        {
            string[] SendMail = { "value" };
            string[] DataInfo = { "TestCaseNumber", "Result", "NGfrequency", "CreateTime", "CloseTime", "ProjectNumber" };
            string[] TotalTestTime = { "value", "value1", "value2", "value3", "value4", "value5", "How Long" };
            string[] TestCase = { "TestCase1", "TestCase2", "TestCase3", "TestCase4", "TestCase5" };
            string[] MailInfo = { "From", "To", "ProjectName", "ModelName", "Version", "Tester", "TeamViewerID", "TeamViewerPassWord" };

            if (File.Exists(Global.MailSettingPath) == false)
            {
                for (int i = 0; i < SendMail.Length; i++)
                {
                    if (i == (SendMail.Length - 1))
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Send Mail", SendMail[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Send Mail", SendMail[i], "");
                    }
                }

                for (int i = 0; i < DataInfo.Length; i++)
                {
                    if (i == (DataInfo.Length - 1))
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Data Info", DataInfo[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Data Info", DataInfo[i], "");
                    }
                }

                for (int i = 0; i < TotalTestTime.Length; i++)
                {
                    if (i == (TotalTestTime.Length - 1))
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Total Test Time", TotalTestTime[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Total Test Time", TotalTestTime[i], "");
                    }
                }

                for (int i = 0; i < TestCase.Length; i++)
                {
                    if (i == (TestCase.Length - 1))
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Test Case", TestCase[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Test Case", TestCase[i], "");
                    }
                }

                for (int i = 0; i < MailInfo.Length; i++)
                {
                    if (i == (MailInfo.Length - 1))
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Mail Info", MailInfo[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.MailSettingPath, "Mail Info", MailInfo[i], "");
                    }
                }
            }
        }
        #endregion

        #region -- 創建RC.ini --
        public void CreateRcConfig()
        {
            string[] Setting = { "SelectRcLastTime", "SelectRcLastTimePath" };

            if (File.Exists(Global.RcSettingPath) == false)
            {
                for (int i = 0; i < Setting.Length; i++)
                {
                    if (i == (Setting.Length - 1))
                    {
                        ini12.INIWrite(Global.RcSettingPath, "Setting", Setting[i], "" + Environment.NewLine + Environment.NewLine);
                    }
                    else
                    {
                        ini12.INIWrite(Global.RcSettingPath, "Setting", Setting[i], "");
                    }
                }
            }
        }
        #endregion
    }
}
