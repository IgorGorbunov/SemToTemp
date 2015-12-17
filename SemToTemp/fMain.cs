using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SemToTemp
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void bttnRecord_Click(object sender, EventArgs e)
        {
            Processor.SelectXlsFiles(tbName.Text, tbTitle.Text, tbDoc.Text, tbYear.Text, pbLoad);
        }
    }
}