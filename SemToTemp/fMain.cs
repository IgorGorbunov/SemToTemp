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
            Element.ElementType elementType = Element.ElementType.Machine;
            if (rbInstr.Checked)
            {
                elementType = Element.ElementType.Tool;
            }
            if (rbMat.Checked)
            {
                elementType = Element.ElementType.Material;
            }
            Processor.SelectXlsFiles(tbName.Text, tbTitle.Text, tbDoc.Text, tbYear.Text, pbLoad, elementType, lblStatus, lblNfiles);
        }
    }
}