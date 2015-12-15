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
            try
            {
                if (SqlOracle.TestQuery(SqlOracle.PreLogin + "TABLE_1"))
                {
                    Visible = false;
                    fMain mainForm = new fMain();
                    mainForm.ShowDialog();
                }
                Close();
            }
            catch (TimeoutException exception)
            {
                const string mess = "���� ������ ����������!";
                Logger.WriteError(mess, exception);
                MessageBox.Show(mess);
            }
        }

        private void fConnect_Load(object sender, EventArgs e)
        {

        }

        private void fConnect_FormClosed(object sender, FormClosedEventArgs e)
        {
            SqlOracle._close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.Text)
            {
                case "���� - Debug":
                    tBlogin.Text = "avia_design";
                    tBpassword.Text = "avia_design";
                    tBsid.Text = "temp";
                    tBhostname.Text = "temp-server";
                    tBport.Text = "1521";
                    SqlOracle.PreLogin = "";
                    break;
                case "�������� - Debug":
                    tBlogin.Text = "ulgu";
                    tBpassword.Text = "1";
                    tBsid.Text = "";
                    tBhostname.Text = "OTL.KTPP.AVIASTAR.LINK-UL.RU";
                    tBport.Text = "";
                    SqlOracle.PreLogin = "AVIA_DESIGN.";
                    break;
                case "�������� - Release":
                    tBlogin.Text = "avia_design";
                    tBpassword.Text = "avia_design";
                    tBsid.Text = "temp";
                    tBhostname.Text = "temp-server";
                    tBport.Text = "1521";
                    SqlOracle.PreLogin = "";
                    break;
            }
        }

    }
}