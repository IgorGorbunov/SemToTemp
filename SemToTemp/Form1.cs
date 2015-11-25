using System;
using System.Windows.Forms;

namespace SemToTemp
{
    public partial class fConnect : Form
    {
        public fConnect()
        {
            InitializeComponent();
        }

        private void bttnConnect_Click(object sender, EventArgs e)
        {
            SqlOracle.BuildConnectionStringSid(tBlogin.Text.Trim(), tBpassword.Text.Trim(),
                                               tBsid.Text.Trim(), tBhostname.Text.Trim(),
                                               tBport.Text.Trim());

            if (SqlOracle.TestQuery("TABLE_1"))
            {
                Visible = false;
                fMain mainForm = new fMain();
                mainForm.ShowDialog();
            }
            Close();
        }

        private void fConnect_Load(object sender, EventArgs e)
        {

        }

        private void fConnect_FormClosed(object sender, FormClosedEventArgs e)
        {
            SqlOracle._close();
        }
    }
}