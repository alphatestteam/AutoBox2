using jini;
using System;
using System.Windows.Forms;

namespace AutoTest
{
    public partial class FormSurp : Form
    {
        public FormSurp()
        {
            InitializeComponent();

        }

        private void FormSurp_MouseEnter(object sender, EventArgs e)
        {
            Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            if (ini12.INIRead(Global.MainSettingPath, "Record", "ImportDB", "") != "")
            {
                if (int.Parse(ini12.INIRead(Global.MainSettingPath, "Record", "ImportDB", "")) == 1)
                {
                    ImportDB.Checked = true;
                }
                else
                {
                    ImportDB.Checked = false;
                }
            }
            else
            {
                ini12.INIWrite(Global.MainSettingPath, "Record", "ImportDB", "0");
                ImportDB.Checked = false;
            }
        }

        private void ImportDB_CheckedChanged(object sender, EventArgs e)
        {
            if (ImportDB.Checked == true)
            {
                // 寫入Import DadaBase啟動
                ini12.INIWrite(Global.MainSettingPath, "Record", "ImportDB", "1");
            }
            else
            {
                // 寫入Import DadaBase關閉
                ini12.INIWrite(Global.MainSettingPath, "Record", "ImportDB", "0");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
