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
            Dictionary<string, string> pars = new Dictionary<string, string>();
            pars.Add(tbAb1.Text, tbSum1.Text);
            pars.Add(tbAb2.Text, tbSum2.Text);
            pars.Add(tbAb3.Text, tbSum3.Text);
            pars.Add(tbAb4.Text, tbSum4.Text);
            pars.Add(tbAb5.Text, tbSum5.Text);
            GroupElement groupElement = new GroupElement("Какая-то группа  1", pars);
            groupElement.WriteToDb();
            BuyInstrument buyInstrument = new BuyInstrument("Super  cool  instr ", "654 654 654   5", groupElement, pars, "  document", "1706");
            buyInstrument.WriteToDb();
            MessageBox.Show("OK!");

        }
    }
}