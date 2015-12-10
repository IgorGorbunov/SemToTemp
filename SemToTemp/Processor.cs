using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// Класс обработки данных из документов Excel
/// </summary>
public static class Processor
{
    private const string _NAME_COL_NAME = "A";
    private const string _TITLE_COL_NAME = "B";
    private const string _DOC_COL_NAME = "C";
    private const string _YEAR_COL_NAME = "D";
    private const int _PARS_COL_NUM = 5;
    private const int _FIRST_ROW = 2;

    /// <summary>
    /// Обработка Excel документов
    /// </summary>
    public static void SelectXlsFiles()
    {
        string mess = "";
        OpenFileDialog xlsBooks = new OpenFileDialog();
        xlsBooks.Title = "Выберите файлы Excel с позициями";
        xlsBooks.Multiselect = true;
        xlsBooks.DefaultExt = "xlsx";
        xlsBooks.Filter = "Файлы Excel (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
        if (xlsBooks.ShowDialog() != DialogResult.OK)
            return;

        ExcelClass xls = new ExcelClass();
        try
        {
            for (int i = 0; i < xlsBooks.FileNames.Length; i++)
            {
                List<BuyInstrument> instruments = new List<BuyInstrument>();
                GroupElement group = new GroupElement(GetShortFileName(xlsBooks.FileNames[i]), null);
                try
                {
                    xls.OpenDocument(xlsBooks.FileNames[i], false);
                    int iRow = _FIRST_ROW;
                    while (!xls.CellIsNullOrVoid(_NAME_COL_NAME, iRow) ||
                           !xls.CellIsNullOrVoid(_TITLE_COL_NAME, iRow) ||
                           !xls.CellIsNullOrVoid(_DOC_COL_NAME, iRow) ||
                           !xls.CellIsNullOrVoid(_YEAR_COL_NAME, iRow))
                    {
                        string name = xls.GetCellStringValue(_NAME_COL_NAME, iRow);
                        string title = xls.GetCellStringValue(_TITLE_COL_NAME, iRow);
                        if (string.IsNullOrEmpty(title))
                        {
                            mess +=
                                GetShortFileName(xlsBooks.FileNames[i]) + " - в " + iRow +
                                " строке позиция без обозначения!" + Environment.NewLine;
                            iRow++;
                            continue;
                        }
                        string doc = xls.GetCellStringValue(_DOC_COL_NAME, iRow);
                        string year = xls.GetCellStringValue(_YEAR_COL_NAME, iRow);
                        BuyInstrument instrument = new BuyInstrument(name, title, group,
                                                                     GetElementParams(xls, iRow),
                                                                     doc,
                                                                     year);
                        instruments.Add(instrument);
                        iRow++;
                    }
                }
                finally
                {
                    xls.CloseDocument();
                }
                if (instruments.Count > 0)
                {
                    group.WriteToDb();
                }
                foreach (BuyInstrument buyInstrument in instruments)
                {
                    buyInstrument.WriteToDb();
                }
            }
        }
        finally
        {
            xls.Dispose();
        }
        MessageBox.Show(mess);
    }

    private static Dictionary<string, string> GetElementParams(ExcelClass xls, int iRow)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        int iCol = _PARS_COL_NUM;
        string sKey = xls.GetCellStringValue(_FIRST_ROW - 1, iCol);
        string sVal = xls.GetCellStringValue(iRow, iCol);
        while (!string.IsNullOrEmpty(sKey) &&
               !string.IsNullOrEmpty(sVal))
        {
            parametrs.Add(sKey, sVal);
            iCol++;
            sKey = xls.GetCellStringValue(_FIRST_ROW - 1, iCol);
            sVal = xls.GetCellStringValue(iRow, iCol);
        }
        if (parametrs.Count > 0)
        {
            return parametrs;
        }
        return null;
    }

    private static string GetShortFileName(string fullName)
    {
        return Path.GetFileNameWithoutExtension(fullName);
    }
}

