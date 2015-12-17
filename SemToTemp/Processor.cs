using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

/// <summary>
/// Класс обработки данных из документов Excel
/// </summary>
public static class Processor
{
    private static string _nameColName = "A";
    private static string _titleColName = "B";
    private static string _docColName = "C";
    private static string _yearColName = "D";
    private const int _PARS_COL_NUM = 5;
    private const int _ATTR_NAME_ROW = 2;
    private const int _ATTR_TITLE_ROW = 3;
    private const int _FIRST_ROW = 4;

    
    // 
    /// <summary>
    /// Обработка Excel документов
    /// </summary>
    /// <param name="nameColName"></param>
    /// <param name="titleColName"></param>
    /// <param name="docColName"></param>
    /// <param name="yearColName"></param>
    public static void SelectXlsFiles(string nameColName, string titleColName, string docColName, string yearColName, ProgressBar bar)
    {
        _nameColName = nameColName;
        _titleColName = titleColName;
        _docColName = docColName;
        _yearColName = yearColName;

        

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
                int iRow = _FIRST_ROW;
                GroupElement group;
                try
                {
                    xls.OpenDocument(xlsBooks.FileNames[i], false);
                    string shortFileName = GetShortFileName(xlsBooks.FileNames[i]);
                    string fullName = xls.GetCellStringValue("A", 1);
                    group = new GroupElement(shortFileName, GetGroupParams(xls), fullName);
                    bool exit;
                    int oldId;
                    bool reWrite = ReWrite(shortFileName, out oldId, out exit);
                    if (exit)
                    {
                        mess += "Операция прекращена пользователем на файле " + shortFileName;
                        break;
                    }
                    while (!xls.CellIsNullOrVoid(_nameColName, iRow) ||
                           !xls.CellIsNullOrVoid(_titleColName, iRow) ||
                           !xls.CellIsNullOrVoid(_docColName, iRow))
                    {
                        ProcessOneRow(xls, iRow, ref mess, instruments, group,
                                      shortFileName);

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
                    group.AddFolders();
                }
                bar.Maximum = instruments.Count;
                bar.Value = 0;
                foreach (BuyInstrument buyInstrument in instruments)
                {
                    buyInstrument.WriteToDb();
                    buyInstrument.AddFolder();
                    bar.Increment(1);
                }
            }
        }
        finally
        {
            xls.Dispose();
        }
        MessageBox.Show(mess);
    }

    private static bool ReWrite(string shortFileName, out int oldId, out bool exit)
    {
        if (GroupElement.Exist(shortFileName, out oldId) && oldId != 0)
        {
            DialogResult result =
                MessageBox.Show(
                    "В базе данных уже существует группа \"" + shortFileName +
                    "\".\nОбновить атрибуты группы?", "Дубликат записи",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                exit = false;
                return true;
            }
            if (result == DialogResult.Cancel)
            {
                exit = true;
                return false;
            }
        }
        exit = false;
        return false;
    }

    private static void ProcessOneRow(ExcelClass xls, int iRow, ref string message, List<BuyInstrument> instruments, GroupElement group, string fileName)
    {
        string name = xls.GetCellStringValue(_nameColName, iRow);
        string title = xls.GetCellStringValue(_titleColName, iRow);
        if (string.IsNullOrEmpty(title))
        {
            message +=
                fileName + " - в " + iRow +
                " строке позиция без обозначения!" + Environment.NewLine;
            return;
        }
        string doc = "", year;
        if (_docColName == _yearColName)
        {
            string[] split = xls.GetCellStringValue(_docColName, iRow).Split('-');
            for (int i = 0; i < split.Length - 1; i++)
            {
                doc += split[i];
            }
            year = split[split.Length - 1];
        }
        else if (string.IsNullOrEmpty(_yearColName.Trim()))
        {
            doc = xls.GetCellStringValue(_docColName, iRow);
            year = "";
        }
        else
        {
            doc = xls.GetCellStringValue(_docColName, iRow);
            year = xls.GetCellStringValue(_yearColName, iRow);
        }
        
        BuyInstrument instrument = new BuyInstrument(name, title, group,
                                                     GetPositionParams(xls, iRow),
                                                     doc,
                                                     year);
        instruments.Add(instrument);
    }

    private static Dictionary<string, string> GetPositionParams(ExcelClass xls, int iRow)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        int iCol = _PARS_COL_NUM;
        string sKey = xls.GetCellStringValue(iCol, _ATTR_TITLE_ROW);
        string sVal = xls.GetCellStringValue(iCol, iRow);
        while (!string.IsNullOrEmpty(sKey))
        {
            if (sKey.Length > 2)
            {
                sKey = sKey.Substring(0, GroupElement.NParamNameChar);
            }
            int i = 1;
            while (parametrs.ContainsKey(sKey))
            {
                sKey = sKey.Substring(0, GroupElement.NParamNameChar - 1);
                sKey += i;
                i++;
            }
            parametrs.Add(sKey, sVal);
            iCol++;
            sKey = xls.GetCellStringValue(iCol, _ATTR_TITLE_ROW);
            sVal = xls.GetCellStringValue(iCol, iRow);
        }
        if (parametrs.Count > 0)
        {
            return parametrs;
        }
        return null;
    }

    private static Dictionary<string, string> GetGroupParams(ExcelClass xls)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        int iCol = _PARS_COL_NUM;
        string sKey = xls.GetCellStringValue(iCol, _ATTR_TITLE_ROW);
        string sVal = xls.GetCellStringValue(iCol, _ATTR_NAME_ROW);
        while (!string.IsNullOrEmpty(sKey))
        {
            if (sKey.Length > 2)
            {
                sKey = sKey.Substring(0, GroupElement.NParamNameChar);
            }
            int i = 1;
            while (parametrs.ContainsKey(sKey))
            {
                sKey = sKey.Substring(0, GroupElement.NParamNameChar - 1);
                sKey += i;
                i++;
            }
            parametrs.Add(sKey, sVal);
            iCol++;
            sKey = xls.GetCellStringValue(iCol, _ATTR_TITLE_ROW);
            sVal = xls.GetCellStringValue(iCol, _ATTR_NAME_ROW);
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

