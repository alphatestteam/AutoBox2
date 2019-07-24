using System;
using System.Windows.Forms;

namespace AutoTest
{
    public partial class frm_Splash : Form
    {
        public frm_Splash()
        {
            InitializeComponent();
        }

        private void frm_Splash_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.StartAutoBox;
        }
    }
}
