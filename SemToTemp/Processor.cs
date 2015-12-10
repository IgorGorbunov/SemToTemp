using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


public static class Processor
{
    private const string _NAME_COL_NAME = "A";
    private const string _TITLE_COL_NAME = "B";
    private const string _DOC_COL_NAME = "C";
    private const string _YEAR_COL_NAME = "D";
    private const int _PARS_COL_NUM = 5;
    private const int _FIRST_ROW = 1;

    public static void SelectXlsFiles()
    {
        OpenFileDialog xlsBooks = new OpenFileDialog();
        xlsBooks.Title = "Выберите файлы Excel с позициями";
        xlsBooks.Multiselect = true;
        xlsBooks.DefaultExt = "xlsx";
        xlsBooks.Filter = "Файлы Excel (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
        if (xlsBooks.ShowDialog() != DialogResult.OK)
            return;

        ExcelClass xls = new ExcelClass();
        for (int i = 0; i < xlsBooks.FileNames.Length; i++)
        {
            List<BuyInstrument> instruments = new List<BuyInstrument>();
            GroupElement group = new GroupElement(xlsBooks.FileNames[i], null);
            try
            {
                xls.OpenDocument(xlsBooks.FileNames[i], false);
                int iRow = _FIRST_ROW;
                while (!xls.CellIsNullOrVoid(_NAME_COL_NAME, iRow) &&
                       !xls.CellIsNullOrVoid(_TITLE_COL_NAME, iRow) &&
                       !xls.CellIsNullOrVoid(_DOC_COL_NAME, iRow) &&
                       !xls.CellIsNullOrVoid(_YEAR_COL_NAME, iRow))
                {
                    string name = xls.GetCellStringValue(_NAME_COL_NAME, iRow);
                    string title = xls.GetCellStringValue(_TITLE_COL_NAME, iRow);
                    string doc = xls.GetCellStringValue(_DOC_COL_NAME, iRow);
                    string year = xls.GetCellStringValue(_YEAR_COL_NAME, iRow);
                    BuyInstrument instrument = new BuyInstrument(name, title, group,
                                                                 GetElementParams(xls, iRow), doc,
                                                                 year);
                    instruments.Add(instrument);
                    iRow++;
                }
            }
            finally
            {
                xls.CloseDocument();
                xls.Dispose();
            }
            group.WriteToDb();
            foreach (BuyInstrument buyInstrument in instruments)
            {
                buyInstrument.WriteToDb();
            }
        }
    }

    private static Dictionary<string, string> GetElementParams(ExcelClass xls, int iRow)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        int iCol = _PARS_COL_NUM;
        string sKey = xls.GetCellStringValue(iCol, _FIRST_ROW);
        string sVal = xls.GetCellStringValue(iCol, iRow);
        while (!string.IsNullOrEmpty(sKey) &&
               !string.IsNullOrEmpty(sVal))
        {
            parametrs.Add(sKey, sVal);
            iCol++;
            sKey = xls.GetCellStringValue(iCol, _FIRST_ROW);
            sVal = xls.GetCellStringValue(iCol, iRow);
        }
        if (parametrs.Count > 0)
        {
            return parametrs;
        }
        return null;
    }
}

