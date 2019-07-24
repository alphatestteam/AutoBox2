using jini;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;

namespace AutoTest
{
    public partial class FormMail : Form
    {
        public FormMail()
        {
            InitializeComponent();
        }

        public void send()
        {
            string To = ini12.INIRead(Global.MailSettingPath, "Mail Info", "To", "") + ",";
            int z = 0;
            string[] to = To.Split(new char[] { ',' });
            List<string> MailList = new List<string> { };

            while (to[z] != "")
            {
                MailList.Add(to[z]);
                z++;
            }

            string PassOrFail = "";
            if (Global.Pass_Or_Fail == "PASS")
            {
                PassOrFail = "<img src=\"PASS.jpg\" />";
            }
            else
            {
                PassOrFail = "<img src=\"NG.jpg\" />";
            }

            string schedule2 = "", schedule3 = "", schedule4 = "", schedule5 = "";
            if (Global.Schedule_2_Exist == 1)
                schedule2 = ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase2", "");
            else if (Global.Schedule_2_Exist == 0)
                schedule2 = "";

            if (Global.Schedule_3_Exist == 1)
                schedule3 = ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase3", "");
            else if (Global.Schedule_3_Exist == 0)
                schedule3 = "";

            if (Global.Schedule_4_Exist == 1)
                schedule4 = ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase4", "");
            else if (Global.Schedule_4_Exist == 0)
                schedule4 = "";

            if (Global.Schedule_5_Exist == 1)
                schedule5 = ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase5", "");
            else if (Global.Schedule_5_Exist == 0)
                schedule5 = "";

            string Subject = "Stress test report by Alpha Team";
            Console.WriteLine(Global.Schedule_Loop);
            Console.WriteLine(Global.Loop_Number);
            string Body =
                                    PassOrFail + "<br>" + "<br>" +

                                    "Test Case : " + "<br>" +
                                    "1. " + ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase1", "") + "<br>" +
                                    "2. " + schedule2 + "<br>" +
                                    "3. " + schedule3 + "<br>" +
                                    "4. " + schedule4 + "<br>" +
                                    "5. " + schedule5 + "<br>" + "<br>" +

                                    "Urtracker Link : " + "http://fwtrack.tpvaoc.com/pts/issuelist.aspx?project=" + ini12.INIRead(Global.MailSettingPath, "Data Info", "ProjectNumber", "") + "<br>" +
                                    "Project Name : " + ini12.INIRead(Global.MailSettingPath, "Mail Info", "ProjectName", "") + "<br>" +
                                    "Model Name : " + ini12.INIRead(Global.MailSettingPath, "Mail Info", "ModelName", "") + "<br>" +
                                    "Version : " + ini12.INIRead(Global.MailSettingPath, "Mail Info", "Version", "") + "<br>" + "<br>" +

                                    "Tester : " + ini12.INIRead(Global.MailSettingPath, "Mail Info", "Tester", "") + "<br>" +
                                    "Test Loop : " + Global.Loop_Number + "<br>" +
                                    "Total Test Time : " + ini12.INIRead(Global.MailSettingPath, "Total Test Time", "How Long", "") + "<br>" + "<br>" +

                                    "Team Viewer ID : " + ini12.INIRead(Global.MailSettingPath, "Mail Info", "TeamViewerID", "") + "<br>" +
                                    "Team Viewer Password : " + ini12.INIRead(Global.MailSettingPath, "Mail Info", "TeamViewerPassWord", "") + "<br>" + "<br>" + "<br>" + "<br>" +



                                    "Please note this E-mail is sent by Alpha mail system automatically, do not reply. If you have any questions please contact system administrator.";

            SendMail(MailList, Subject, Body);
        }

        public void logsend()
        {
            string To = ini12.INIRead(Global.MailSettingPath, "Mail Info", "To", "") + ",";
            int z = 0;
            string i = ini12.INIRead(Global.MainSettingPath, "LogSearch", "Nowvalue", "");
            string[] to = To.Split(new char[] { ',' });
            List<string> MailList = new List<string> { };

            while (to[z] != "")
            {
                MailList.Add(to[z]);
                z++;
            }

            string Subject = "Event Notification: " + ini12.INIRead(Global.MainSettingPath, "LogSearch", "Text" + i, "") + " ------ " + ini12.INIRead(Global.MainSettingPath, "LogSearch", "Times" + i, "") + " times";
            string Body =
                                    "Event Notification" + "<br>" + "<br>" +

                                    "Search Keyword: " + ini12.INIRead(Global.MainSettingPath, "LogSearch", "Text" + i, "") + "<br>" +
                                    "Start Test time: " + ini12.INIRead(Global.MainSettingPath, "LogSearch", "StartTime", "") + "<br>" +
                                    "Search keyword time: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "<br>" + "<br>" +
                                    "Please note this E-mail is sent by Alpha mail system automatically, do not reply. If you have any questions please contact system administrator." + "<br>";

            SendMail(MailList, Subject, Body);
        }

        public void SendMail(List<string> MailList, string Subject, string Body)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(string.Join(",", MailList.ToArray()));       //收件者，以逗號分隔不同收件者
            msg.From = new MailAddress(ini12.INIRead(Global.MailSettingPath, "Mail Info", "From", ""), ini12.INIRead(Global.MailSettingPath, "Mail Info", "From", ""), System.Text.Encoding.UTF8);
            msg.Subject = Subject;      //郵件標題 
            msg.SubjectEncoding = System.Text.Encoding.UTF8;        //郵件標題編碼  
            msg.Body = Body;        //郵件內容

            msg.IsBodyHtml = true;
            msg.BodyEncoding = System.Text.Encoding.UTF8;       //郵件內容編碼 
            msg.Priority = MailPriority.Normal;     //郵件優先級 

            if (Global.Pass_Or_Fail == "PASS")
            {
                msg.Attachments.Add(GetAttachment("PASS.jpg", System.Text.Encoding.UTF8));
            }
            else
            {
                msg.Attachments.Add(GetAttachment("NG.jpg", System.Text.Encoding.UTF8));
            }

            string Schdule1Path = ini12.INIRead(Global.MainSettingPath, "Schedule1", "Path", "");
            msg.Attachments.Add(GetAttachmentSchdule(Path.GetFileName(Schdule1Path), System.Text.Encoding.UTF8));

            if (Global.Schedule_2_Exist == 1)
            {
                string Schdule2Path = ini12.INIRead(Global.MainSettingPath, "Schedule2", "Path", "");
                msg.Attachments.Add(GetAttachmentSchdule(Path.GetFileName(Schdule2Path), System.Text.Encoding.UTF8));
            }

            if (Global.Schedule_3_Exist == 1)
            {
                string Schdule3Path = ini12.INIRead(Global.MainSettingPath, "Schedule3", "Path", "");
                msg.Attachments.Add(GetAttachmentSchdule(Path.GetFileName(Schdule3Path), System.Text.Encoding.UTF8));
            }

            if (Global.Schedule_4_Exist == 1)
            {
                string Schdule4Path = ini12.INIRead(Global.MainSettingPath, "Schedule4", "Path", "");
                msg.Attachments.Add(GetAttachmentSchdule(Path.GetFileName(Schdule4Path), System.Text.Encoding.UTF8));
            }

            if (Global.Schedule_5_Exist == 1)
            {
                string Schdule5Path = ini12.INIRead(Global.MainSettingPath, "Schedule5", "Path", "");
                msg.Attachments.Add(GetAttachmentSchdule(Path.GetFileName(Schdule5Path), System.Text.Encoding.UTF8));
            }

            string RCDBPath = ini12.INIRead(Global.MainSettingPath, "RedRat", "DBFile", "");
            msg.Attachments.Add(GetAttachmentRCDB(Path.GetFileName(RCDBPath), System.Text.Encoding.UTF8));

            //建立 SmtpClient 物件 並設定 Gmail的smtp主機及Port 

            #region 其它 Host
            /*
            ~~~~~~~~~~~~~~~~~       outlook.com smtp.live.com port:25
            ~~~~~~~~~~~~~~~~~       yahoo smtp.mail.yahoo.com.tw port:465
            ~~~~~~~~~~~~~~~~~       smtp.gmail.com port:587
            ~~~~~~~~~~~~~~~~~       tpmx.tpvaoc.com port: 25        //公司內部的SMTP
            ~~~~~~~~~~~~~~~~~       msa.hinet.net port: 25
            */
            #endregion

            try
            {
                SmtpClient MySmtp = new SmtpClient("smtp.gmail.com", 587);
                MySmtp.Credentials = new System.Net.NetworkCredential("alphatestteam@gmail.com", "16433587");     //設定你的帳號密碼
                MySmtp.EnableSsl = true;      //Gmial 的 smtp 需打開 SSL
                MySmtp.Send(msg);
            }
            catch (Exception)
            {
                MessageBox.Show("Connect the google smtp server error and mail setting value is disabled. Please check the network connect status.", "Mail send error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ini12.INIWrite(Global.MailSettingPath, "Send Mail", "value", "0");
            }
        }

        private Attachment GetAttachment(string strFileName, Encoding encode)
        {
            //設定圖片路徑
            string strFilePath = Application.StartupPath + "\\Resources\\" + strFileName;

            Attachment attachment = new Attachment(strFilePath)
            {
                Name = strFileName,
                NameEncoding = encode,
                TransferEncoding = TransferEncoding.Base64
            };

            // 設定附件為內嵌
            attachment.ContentDisposition.Inline = true;
            attachment.ContentDisposition.DispositionType = "inline";

            return attachment;
        }

        private Attachment GetAttachmentSchdule(string strFileName, Encoding encode)
        {
            //設定圖片路徑
            string SchdulePath = ini12.INIRead(Global.MainSettingPath, "Schedule1", "Path", "");

            Attachment attachment = new Attachment(SchdulePath)
            {
                Name = strFileName,
                NameEncoding = encode,
                TransferEncoding = TransferEncoding.Base64
            };

            // 設定附件為內嵌
            attachment.ContentDisposition.Inline = true;
            attachment.ContentDisposition.DispositionType = "inline";

            return attachment;
        }

        private Attachment GetAttachmentRCDB(string strFileName, Encoding encode)
        {
            //設定圖片路徑
            string RCDBPath = ini12.INIRead(Global.MainSettingPath, "RedRat", "DBFile", "");

            Attachment attachment = new Attachment(RCDBPath)
            {
                Name = strFileName,
                NameEncoding = encode,
                TransferEncoding = TransferEncoding.Base64
            };

            // 設定附件為內嵌
            attachment.ContentDisposition.Inline = true;
            attachment.ContentDisposition.DispositionType = "inline";

            return attachment;
        }

        public void SendMailBtn_Click(object sender, EventArgs e)
        {
            send();
        }

        public void SaveSchBtn_Click(object sender, EventArgs e)
        {
            if (SendMailcheckBox.Checked == true)
            {
                if (string.IsNullOrEmpty(textBox_From.Text))
                {
                    label_ErrorMessage.Text = "Sender must exist !";
                }
                else if (string.IsNullOrEmpty(textBox_To.Text))
                {
                    label_ErrorMessage.Text = "Recipient must exist !";
                }
                else
                {
                    ini12.INIWrite(Global.MailSettingPath, "Mail Info", "From", textBox_From.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Mail Info", "To", textBox_To.Text.Trim());

                    ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase1", textBox_TestCase1.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase2", textBox_TestCase2.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase3", textBox_TestCase3.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase4", textBox_TestCase4.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase5", textBox_TestCase5.Text.Trim());

                    ini12.INIWrite(Global.MailSettingPath, "Data Info", "ProjectNumber", textBox_ProjectNumber.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Mail Info", "ProjectName", textBox_ProjectName.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Mail Info", "ModelName", textBox_ModelName.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Mail Info", "Version", textBox_Version.Text.Trim());

                    ini12.INIWrite(Global.MailSettingPath, "Mail Info", "Tester", textBox_Tester.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Total Test Time", "value", textBox_TotalTestTime.Text.Trim());

                    ini12.INIWrite(Global.MailSettingPath, "Mail Info", "TeamViewerID", textBox_TeamViewerID.Text.Trim());
                    ini12.INIWrite(Global.MailSettingPath, "Mail Info", "TeamViewerPassWord", textBox_TeamViewerPassWord.Text.Trim());

                    label_ErrorMessage.Text = "Save Successfully !";
                }
            }
        }

        public void FormMail_Load(object sender, EventArgs e)
        {
            label_ErrorMessage.Text = "";
            if (ini12.INIRead(Global.MailSettingPath, "Send Mail", "value", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MailSettingPath, "Send Mail", "value", "")) == 1)
                {
                    SendMailcheckBox.Checked = true;
                    textBox_From.Enabled = true;
                    textBox_To.Enabled = true;
                    textBox_TestCase1.Enabled = true;
                    textBox_TestCase2.Enabled = true;
                    textBox_TestCase3.Enabled = true;
                    textBox_TestCase4.Enabled = true;
                    textBox_TestCase5.Enabled = true;
                    textBox_ProjectNumber.Enabled = true;
                    textBox_ProjectName.Enabled = true;
                    textBox_ModelName.Enabled = true;
                    textBox_Version.Enabled = true;
                    textBox_Tester.Enabled = true;
                    textBox_TotalTestTime.Enabled = true;
                    textBox_TeamViewerID.Enabled = true;
                    textBox_TeamViewerPassWord.Enabled = true;
                }
                else
                {
                    SendMailcheckBox.Checked = false;
                    textBox_From.Enabled = false;
                    textBox_To.Enabled = false;
                    textBox_TestCase1.Enabled = false;
                    textBox_TestCase2.Enabled = false;
                    textBox_TestCase3.Enabled = false;
                    textBox_TestCase4.Enabled = false;
                    textBox_TestCase5.Enabled = false;
                    textBox_ProjectNumber.Enabled = false;
                    textBox_ProjectName.Enabled = false;
                    textBox_ModelName.Enabled = false;
                    textBox_Version.Enabled = false;
                    textBox_Tester.Enabled = false;
                    textBox_TotalTestTime.Enabled = false;
                    textBox_TeamViewerID.Enabled = false;
                    textBox_TeamViewerPassWord.Enabled = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MailSettingPath, "Send Mail", "value", "0");
                SendMailcheckBox.Checked = false;
                textBox_From.Enabled = false;
                textBox_To.Enabled = false;
                textBox_TestCase1.Enabled = false;
                textBox_TestCase2.Enabled = false;
                textBox_TestCase3.Enabled = false;
                textBox_TestCase4.Enabled = false;
                textBox_TestCase5.Enabled = false;
                textBox_ProjectNumber.Enabled = false;
                textBox_ProjectName.Enabled = false;
                textBox_ModelName.Enabled = false;
                textBox_Version.Enabled = false;
                textBox_Tester.Enabled = false;
                textBox_TotalTestTime.Enabled = false;
                textBox_TeamViewerID.Enabled = false;
                textBox_TeamViewerPassWord.Enabled = false;
            }

            textBox_From.Text = ini12.INIRead(Global.MailSettingPath, "Mail Info", "From", "");
            textBox_To.Text = ini12.INIRead(Global.MailSettingPath, "Mail Info", "To", "");

            textBox_TestCase1.Text = ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase1", "");
            textBox_TestCase2.Text = ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase2", "");
            textBox_TestCase3.Text = ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase3", "");
            textBox_TestCase4.Text = ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase4", "");
            textBox_TestCase5.Text = ini12.INIRead(Global.MailSettingPath, "Test Case", "TestCase5", "");

            textBox_ProjectNumber.Text = ini12.INIRead(Global.MailSettingPath, "Data Info", "ProjectNumber", "");
            textBox_ProjectName.Text = ini12.INIRead(Global.MailSettingPath, "Mail Info", "ProjectName", "");
            textBox_ModelName.Text = ini12.INIRead(Global.MailSettingPath, "Mail Info", "ModelName", "");
            textBox_Version.Text = ini12.INIRead(Global.MailSettingPath, "Mail Info", "Version", "");

            textBox_Tester.Text = ini12.INIRead(Global.MailSettingPath, "Mail Info", "Tester", "");
            textBox_TotalTestTime.Text = ini12.INIRead(Global.MailSettingPath, "Mail Info", "Total Test Time", "");

            textBox_TeamViewerID.Text = ini12.INIRead(Global.MailSettingPath, "Mail Info", "TeamViewerID", "");
            textBox_TeamViewerPassWord.Text = ini12.INIRead(Global.MailSettingPath, "Mail Info", "TeamViewerPassWord", "");
        }

        bool ConnectGoogleTW()
        {
            //Google網址
            string googleTW = "www.google.tw";
            //Ping網站
            Ping p = new Ping();
            //網站的回覆
            PingReply reply;

            try
            {
                //取得網站的回覆
                reply = p.Send(googleTW);
                //如果回覆的狀態為Success則return true
                if (reply.Status == IPStatus.Success)
                { return true; }
            }

            //catch這裡的Exception, 是有可能網站當下的某某狀況造成, 可以直接讓它傳回false.
            //或在重覆try{}裡的動作一次
            catch { return false; }

            //如果reply.Status !=IPStatus.Success, 直接回傳false
            return false;

        }

        bool ConnectGMailTW()
        {
            //建立 SmtpClient 物件 並設定 Gmail的smtp主機及Port 

            System.Net.Mail.SmtpClient MySmtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);

            //設定你的帳號密碼
            MySmtp.Credentials = new System.Net.NetworkCredential("alphatestteam@gmail.com", "16433587");

            //Gmial 的 smtp 使用 SSL
            MySmtp.EnableSsl = true;

            try
            {
                //發送Email
                MySmtp.Send("'AlphaTestTeam'<alphatestteam@gmail.com>", "<alphatestteam@gmail.com>", "Gmail sent mail function test", "Gmail sent mail function test.");
                MessageBox.Show("The GMail system is normal on the network environment.");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show("The GMail system is abnormal on the network environment.");
                SendMailcheckBox.Checked = false;
                GamilcheckBox.Checked = false;
                return false;
            }
        }

        private void SendMailcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SendMailcheckBox.Checked == true)
            {
                ConnectGoogleTW();
                if (ConnectGoogleTW() == true)
                {
                    ini12.INIWrite(Global.MailSettingPath, "Send Mail", "value", "1");
                    textBox_From.Enabled = true;
                    textBox_From_TextChanged(new TextBox(), new EventArgs());
                    textBox_To.Enabled = true;
                    textBox_TotalTestTime_TextChanged(new TextBox(), new EventArgs());
                    textBox_TestCase1.Enabled = true;
                    textBox_TestCase2.Enabled = true;
                    textBox_TestCase3.Enabled = true;
                    textBox_TestCase4.Enabled = true;
                    textBox_TestCase5.Enabled = true;
                    textBox_ProjectNumber.Enabled = true;
                    textBox_ProjectName.Enabled = true;
                    textBox_ModelName.Enabled = true;
                    textBox_Version.Enabled = true;
                    textBox_Tester.Enabled = true;
                    textBox_TotalTestTime.Enabled = true;
                    textBox_TeamViewerID.Enabled = true;
                    textBox_TeamViewerPassWord.Enabled = true;
                    GamilcheckBox.Visible = true;
                }
                else
                {
                    MessageBox.Show("Please check the network status !");
                    SendMailcheckBox.Checked = false;
                    GamilcheckBox.Checked = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MailSettingPath, "Send Mail", "value", "0");
                textBox_From.Enabled = false;
                pictureBox_From.Image = null;
                textBox_To.Enabled = false;
                pictureBox_To.Image = null;
                textBox_TestCase1.Enabled = false;
                textBox_TestCase2.Enabled = false;
                textBox_TestCase3.Enabled = false;
                textBox_TestCase4.Enabled = false;
                textBox_TestCase5.Enabled = false;
                textBox_ProjectNumber.Enabled = false;
                textBox_ProjectName.Enabled = false;
                textBox_ModelName.Enabled = false;
                textBox_Version.Enabled = false;
                textBox_Tester.Enabled = false;
                textBox_TotalTestTime.Enabled = false;
                textBox_TeamViewerID.Enabled = false;
                textBox_TeamViewerPassWord.Enabled = false;
                label_ErrorMessage.Text = "";
                GamilcheckBox.Visible = false;
            }
        }

        private void GamilcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SendMailcheckBox.Checked == true && GamilcheckBox.Checked == true)
            {
                ConnectGMailTW();
            }
            else if (SendMailcheckBox.Checked == false && GamilcheckBox.Checked == true)
            {
                MessageBox.Show("Please check the Mail function first.");
            }
        }

        private void textBox_From_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_From.Text))
            {
                label_ErrorMessage.Text = "Sender must exist !";
                pictureBox_From.Image = Properties.Resources.ERROR;
            }
            else
            {
                ini12.INIWrite(Global.MailSettingPath, "Mail Info", "From", textBox_From.Text.Trim());
                label_ErrorMessage.Text = "";
                pictureBox_From.Image = null;
            }
        }

        private void textBox_To_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox_To.Text))
            {
                label_ErrorMessage.Text = "Recipient must exist !";
                pictureBox_To.Image = Properties.Resources.ERROR;
            }
            else
            {
                ini12.INIWrite(Global.MailSettingPath, "Mail Info", "To", textBox_To.Text.Trim());
                label_ErrorMessage.Text = "";
                pictureBox_To.Image = null;
            }
        }

        private void textBox_TestCase1_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase1", textBox_TestCase1.Text.Trim());
        }

        private void textBox_TestCase2_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase2", textBox_TestCase2.Text.Trim());
        }

        private void textBox_TestCase3_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase3", textBox_TestCase3.Text.Trim());
        }

        private void textBox_TestCase4_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase4", textBox_TestCase4.Text.Trim());
        }

        private void textBox_TestCase5_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Test Case", "TestCase5", textBox_TestCase5.Text.Trim());
        }

        private void textBox_ProjectNumber_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Data Info", "ProjectNumber", textBox_ProjectNumber.Text.Trim());
        }

        private void textBox_ProjectName_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Mail Info", "ProjectName", textBox_ProjectName.Text.Trim());
        }

        private void textBox_ModelName_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Mail Info", "ModelName", textBox_ModelName.Text.Trim());
        }

        private void textBox_Version_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Mail Info", "Version", textBox_Version.Text.Trim());
        }

        private void textBox_Tester_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Mail Info", "Tester", textBox_Tester.Text.Trim());
        }

        private void textBox_TotalTestTime_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Total Test Time", "value", textBox_TotalTestTime.Text.Trim());
        }

        private void textBox_TeamViewerID_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Mail Info", "TeamViewerID", textBox_TeamViewerID.Text.Trim());
        }

        private void textBox_TeamViewerPassWord_TextChanged(object sender, EventArgs e)
        {
            ini12.INIWrite(Global.MailSettingPath, "Mail Info", "TeamViewerPassWord", textBox_TeamViewerPassWord.Text.Trim());
        }
    }
}
