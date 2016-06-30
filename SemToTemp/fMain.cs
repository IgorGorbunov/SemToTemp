using System;
using System.Collections.Generic;
using System.Globalization;
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
            if (rbOpers.Checked)
            {
                Processor.SetOpers();
                return;
            }
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

        private void bExcelExportTo_Click(object sender, EventArgs e)
        {
            SaveFileDialog xlsF = new SaveFileDialog();
            xlsF.Title = "Выберите файлы Excel с позициями";
            xlsF.DefaultExt = "xlsx";
            xlsF.Filter = "Файлы Excel (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
            if (xlsF.ShowDialog() != DialogResult.OK)
                return;

            ExcelClass xls = new ExcelClass();
            try
            {
                xls.NewDocument();
                xls.SaveDocument(xlsF.FileName);
                xls.OpenDocument(xlsF.FileName, false);
                try
                {
                    string workshop = tbWorkshop.Text;

                    List<string> tps = new List<string>();
                    Dictionary<string, string> param = new Dictionary<string, string>();
                    param.Add("T5_CE", workshop);
                    SqlOracle.Sel(
                        "select distinct t5_tp from table_5 where t5_ce = :T5_CE order by t5_tp",
                        param, out tps);
                    label16.Text = tps.Count.ToString();

                    int itp = 1;
                    int rowN = 2;
                    foreach (string tp in tps)
                    {
                        label10.Text = itp.ToString();

                        xls.SetCellValue("A", rowN, tp);
                        List<int> opers = new List<int>();
                        param = new Dictionary<string, string>();
                        param.Add("T5_TP", tp + "%");
                        SqlOracle.Sel(
                            "select distinct t5_no from table_5 where t5_tp like :T5_TP order by t5_no",
                            param, out opers);

                        label17.Text = opers.Count.ToString();
                        int ioper = 1;
                        foreach (int oper in opers)
                        {
                            label11.Text = ioper.ToString();

                            xls.SetCellValue("B", rowN, oper.ToString());
                            List<int> pers = new List<int>();
                            param = new Dictionary<string, string>();
                            param.Add("T5_TP", tp + "%");
                            param.Add("T5_NO", oper.ToString());
                            SqlOracle.Sel(
                                "select distinct t5_np from table_5 where t5_tp like :T5_TP and t5_no = :T5_NO order by t5_np",
                                param, out pers);

                            label18.Text = pers.Count.ToString();
                            int iper = 1;
                            foreach (int per in pers)
                            {
                                label12.Text = iper.ToString();
                                xls.SetCellValue("C", rowN, per.ToString());

                                if (oper == 35 && per == 30)
                                {
                                    
                                }

                                param = new Dictionary<string, string>();
                                param.Add("T5_TP", tp + "%");
                                param.Add("T5_NO", oper.ToString());
                                param.Add("T5_NP", per.ToString());
                                int btm;

                                SqlOracle.Sel(
                                    "select distinct t5_bt from table_5 where t5_tp like :T5_TP and t5_no = :T5_NO and t5_np = :T5_NP",
                                    param, out btm);
                                xls.SetCellValue("D", rowN, btm.ToString());

                                byte[] vo =
                                    SqlOracle.GetBytes(
                                        "select distinct t5_vo from table_5 where t5_tp like :T5_TP and t5_no = :T5_NO and t5_np = :T5_NP",
                                        param);

                                if (vo.Length > 1)
                                {
                                    for (int i = 0; i < vo.Length/2; i++)
                                    {
                                        int i1 = vo[i*2];
                                        int i2 = vo[i*2 + 1];
                                        if (i1 == 0 && i2 == 0)
                                        {
                                            continue;
                                        }
                                        string h1 = Convert.ToString(i1, 16);
                                        string h2 = Convert.ToString(i2, 16);
                                        if (h1.Length < 2)
                                        {
                                            h1 = "0" + h1;
                                        }
                                        string h = h2 + h1;
                                        int nn = int.Parse(h, NumberStyles.AllowHexSpecifier);

                                        param = new Dictionary<string, string>();
                                        param.Add("T2", nn.ToString());
                                        string pr;
                                        SqlOracle.Sel("select t2_r1 from table_2 where t2_nn = :T2", param, out pr);
                                        string name;
                                        SqlOracle.Sel("select t2_nm from table_2 where t2_nn = :T2", param, out name);
                                        string title;
                                        SqlOracle.Sel("select t2_oboz from table_2 where t2_nn = :T2", param, out title);
                                        int gr;
                                        SqlOracle.Sel("select t2_ng from table_2 where t2_nn = :T2", param, out gr);

                                        if (pr != null)
                                        {
                                            xls.SetCellValue("E", rowN, pr);
                                            xls.SetCellValue("F", rowN, name);
                                            xls.SetCellValue("G", rowN, title);
                                            xls.SetCellValue("H", rowN, gr.ToString());
                                            xls.SetCellValue("I", rowN, nn.ToString());
                                        }
                                        else
                                        {
                                            xls.SetCellValue("E", rowN, "Инструмент не найден в базе");
                                            xls.SetCellValue("I", rowN, nn.ToString());
                                        }
                                        
                                        rowN++;
                                        
                                    }
                                }


                                iper++;
                                Application.DoEvents();
                                rowN++;
                            }
                            ioper++;
                            rowN++;
                        }

                        itp++;
                        rowN++;
                    }
                }
                finally 
                {
                    xls.CloseDocumentSave();
                }
            }
            finally
            {
                xls.Dispose();
            }



        }


    }
}