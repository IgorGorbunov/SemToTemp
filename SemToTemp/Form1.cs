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
                const string mess = "База данных недоступна!";
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
                case "УлГУ - Debug":
                    tBlogin.Text = "avia_design";
                    tBpassword.Text = "avia_design";
                    tBsid.Text = "temp";
                    tBhostname.Text = "temp-server";
                    tBport.Text = "1521";
                    SqlOracle.PreLogin = "";
                    break;
                case "Авиастар - Debug":
                    tBlogin.Text = "ulgu";
                    tBpassword.Text = "1";
                    tBsid.Text = "OTL";
                    tBhostname.Text = "10.68.0.24";
                    tBport.Text = "1521";
                    SqlOracle.PreLogin = "AVIA_DESIGN.";
                    break;
                case "Авиастар - Release":
                    tBlogin.Text = "avia_design";
                    tBpassword.Text = "avia_design0";
                    tBsid.Text = "asp";
                    tBhostname.Text = "10.68.2.61";
                    tBport.Text = "1523";
                    SqlOracle.PreLogin = "avia_design.";
                    break;
            }
        }

    }
}