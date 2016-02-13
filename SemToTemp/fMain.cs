using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SemToTemp
{
    public partial class fMain : Form
    {
        private Logger _logger;

        public fMain(Logger logger)
        {
            InitializeComponent();
            _logger = logger;
        }

        private void bttnRecord_Click(object sender, EventArgs e)
        {
            int elementType = 0;
            if (rbInstr.Checked)
            {
                elementType = 1;
            }
            if (rbAddition.Checked)
            {
                elementType = 3;
            }
            if (rbMeasure.Checked)
            {
                elementType = 4;
            }
            if (rbMat.Checked)
            {
                elementType = 2;
            }
            if (rbMachine.Checked)
            {
                elementType = 0;
            }
            Processor.SelectXlsFiles(tbName.Text, tbTitle.Text, tbDoc.Text, tbYear.Text, pbLoad, elementType, lblStatus, lblNfiles);
        }
    }
}