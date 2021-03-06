﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
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

    private static Logger _logger;
    private static StreamWriter _userLog;

    private static readonly string _USER_LOG_FULL_PATH = AppDomain.CurrentDomain.BaseDirectory + "status.log";

    public static void ExportTo(string workshop)
    {
        
        


    }
    
    public static void SetOpers()
    {
        _logger = new Logger();
        _logger.WriteLine("----------------------------------------- NEW SESSION ----------------------------------------------");
        _logger.WriteLine("Выбор файла с операциями");

        OpenFileDialog xlsBooks = new OpenFileDialog();
        xlsBooks.Title = "Выберите файлы Excel с операциями";
        xlsBooks.Multiselect = false;
        xlsBooks.DefaultExt = "xlsx";
        xlsBooks.Filter = "Файлы Excel (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*";
        if (xlsBooks.ShowDialog() != DialogResult.OK)
        {
            _logger.WriteLine("Файл не выбран");
            return;
        }
        _logger.WriteLine("Файл - " + xlsBooks.FileName);

        List<Operation> operations = GetOperations(xlsBooks.FileName);
        WriteOpersToDb(operations);
    }

    /// <summary>
    /// Обработка Excel документов
    /// </summary>
    /// <param name="nameColName"></param>
    /// <param name="titleColName"></param>
    /// <param name="docColName"></param>
    /// <param name="yearColName"></param>
    public static void SelectXlsFiles(string nameColName, string titleColName, string docColName, string yearColName, ProgressBar bar, int type, Label status, Label nFiles)
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

        _userLog = new StreamWriter(_USER_LOG_FULL_PATH, false, Encoding.UTF8);
        _userLog.WriteLine(DateTime.Now.ToLongTimeString());
        _userLog.WriteLine("Начало работы");
        _logger = new Logger();
        _logger.WriteLine("----------------------------------------- NEW SESSION ----------------------------------------------");

        ExcelClass xls = new ExcelClass();
        try
        {
            bool cancel = false;
            for (int i = 0; i < xlsBooks.FileNames.Length; i++)
            {
                status.Text = "Чтение файлов";
                nFiles.Text = i.ToString();
                Application.DoEvents();
                List<Position> instruments = new List<Position>();
                int iRow = _FIRST_ROW;
                GroupElement group;

                bool exist;
                bool groupExist = false;
                try
                {
                    xls.OpenDocument(xlsBooks.FileNames[i], false);
                    _userLog.WriteLine("Открыт файл " + xlsBooks.FileNames[i]);
                    _logger.WriteLine("Открыт файл " + xlsBooks.FileNames[i]);

                    string shortFileName = GetShortFileName(xlsBooks.FileNames[i]);
                    _logger.WriteLine("Короткое имя группы - " + shortFileName);

                    string fullName = xls.GetCellStringValue("A", 1);
                    _logger.WriteLine("Полный путь группы - " + fullName);

                    group = new GroupElement(shortFileName, GetGroupParams(xls), fullName);
                    int oldId;
                    exist = GroupElement.Exist(group.Name, out oldId);
                    if (exist)
                    {
                        group = new GroupElement(oldId, group.Name, group.FullName);
                        groupExist = true;
                        _userLog.WriteLine("Группа уже существует. Найдена группа \"" + group.Name + "\"");
                        _logger.WriteLine("Группа уже существует. Найдена группа \"" + group.Name + "\"");
                    }
                    while (!xls.CellIsNullOrVoid(_nameColName, iRow) ||
                           !xls.CellIsNullOrVoid(_titleColName, iRow) ||
                           !xls.CellIsNullOrVoid(_docColName, iRow))
                    {
                        ProcessOneRow2(xls, iRow, ref mess, instruments, group,
                                      shortFileName, type);

                        iRow++;
                    }
                }
                finally
                {
                    xls.CloseDocument();
                }
                if (instruments.Count > 0)
                {
                    if (!exist)
                    {
                        group.WriteToDb();
                        group.AddGeneralFolders(@"Справочники цеха 254\");
                        _userLog.WriteLine("Группа записана в БД");
                        _logger.WriteLine("Группа \"" + group.Name + "\" записана в БД");
                    }
                }
                bar.Maximum = instruments.Count;
                bar.Value = 0;
                status.Text = "Запись в БД";
                foreach (Position pos in instruments)
                {
                    int oldPosId;
                    if (Position.Exist(pos.Title, out oldPosId))
                    {
                        int dbGroupId = Position.GetGroupId(oldPosId);
                        if (group.Id != dbGroupId)
                        {
                            DialogResult result = MessageBox.Show("Позиция \"" + pos.Title + "\" уже существует и находится в другой группе (\"" + GroupElement.GetName(dbGroupId) + "\"). Перенести в группу из электронной таблицы (\"" + group.Name + "\") ?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            switch (result)
                            {
                                case DialogResult.Cancel:
                                    cancel = true;
                                    goto Cancel;
                                case DialogResult.Yes:
                                    pos.SetNewGroup(oldPosId, group.Id);
                                break;
                            }
                        }
                        string message;
                        Dictionary<int, string> differentParams;
                        if (pos.IsSimilarPositionParams(oldPosId, out message, out differentParams))
                        {
                            _userLog.WriteLine(message);
                            _logger.WriteLine(message);
                            DialogResult result = MessageBox.Show(message + Environment.NewLine + "Обновить параметры?", "Внимание!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                            switch (result)
                            {
                                case DialogResult.Cancel:
                                    cancel = true;
                                    goto Cancel;
                                case DialogResult.Yes:
                                    Position.EditParametrs(oldPosId, differentParams);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        string message;
                        pos.IsSimilarGroupParams(group.GetParamCodes(), out message);
                        _userLog.WriteLine(message);
                        pos.WriteToDb2();
                    }
                    bar.Increment(1);
                    Application.DoEvents();
                }
                _userLog.WriteLine("Записано " + instruments.Count + " позиций");
                _userLog.Flush();
                _userLog.WriteLine();
                _logger.WriteLine("Записано " + instruments.Count + " позиций");
                
            }
        Cancel:
            if (cancel)
            {
                _userLog.WriteLine("***Операция отменена пользователем");
                _userLog.Flush();
                _userLog.WriteLine();
                _logger.WriteLine("***Операция отменена пользователем");
            }
        }
        finally
        {
            xls.Dispose();
            _userLog.Close();
        }
        Process.Start(_USER_LOG_FULL_PATH); 
        MessageBox.Show("Готово!");
    }

    private static List<Operation> GetOperations(string fileName)
    {
        List<Operation> opers = new List<Operation>();
        ExcelClass xls = new ExcelClass();
        try
        {
            int i = 1;
            try
            {
                xls.OpenDocument(fileName, false);
                string guid = xls.GetCellStringValue(1, 1);
                string kod = xls.GetCellStringValue(2, 1);
                string text = xls.GetCellStringValue(3, 1);
                
                while (!string.IsNullOrEmpty(guid))
                {
                    int kodI = int.Parse(kod);
                    opers.Add(new Operation(kodI, guid, text));

                    i++;
                    guid = xls.GetCellStringValue(1, i);
                    kod = xls.GetCellStringValue(2, i);
                    text = xls.GetCellStringValue(3, i);
                }
            }
            finally
            {
                xls.CloseDocument();
            }
            _logger.WriteLine("Записано " + i + " позиций");
        }
        finally
        {
            xls.Dispose();
        }
        if (opers.Count <= 0)
        {
            return null;
        }
        return opers;
    }

    private static void WriteOpersToDb(List<Operation> opers)
    {
        foreach (Operation operation in opers)
        {
            if (SqlOracle.Exist(operation.Kod, "KOD", "TABLE_OPERS"))
            {
                SqlOracle.ExecuteVoidQuery("delete from TABLE_OPERS where KOD = " + operation.Kod);
            }
            Dictionary<string, string> pDictionary = new Dictionary<string, string>();
            pDictionary.Add("KOD", operation.Kod.ToString());
            pDictionary.Add("GUID", operation.Guid);
            pDictionary.Add("TEXT", operation.Text);
            SqlOracle.Insert("insert into " + SqlOracle.PreLogin +
                             "TABLE_OPERS (KOD, GUID, TEXT) VALUES (:KOD, :GUID, :TEXT)", pDictionary);
        }
    }

    private static void ProcessOneRow2(ExcelClass xls, int iRow, ref string message, List<Position> positions, GroupElement group, string fileName, int posType)
    {
        string name = xls.GetCellStringValue(_nameColName, iRow);
        string title = xls.GetCellStringValue(_titleColName, iRow);
        if (string.IsNullOrEmpty(title))
        {
            message +=
                fileName + " - в " + iRow +
                " строке позиция без обозначения!" + Environment.NewLine;
            _userLog.WriteLine("*** Ошибка! *** " + message);
            _logger.WriteError(message);
            return;
        }


        string doc, year = "";
        if (_docColName == _yearColName)
        {
            doc = xls.GetCellStringValue(_docColName, iRow);
            string[] split = doc.Split('-');
            if (split.Length > 0)
            {
                doc = "";
                for (int i = 0; i < split.Length - 1; i++)
                {
                    doc += split[i];
                }
                year = split[split.Length - 1];
            }

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

        Position pos = null;
        switch (posType)
        {
                case 2:
                    pos = new Materials(name, title, group,
                                                             GetPositionParams(xls, iRow),
                                                             doc,
                                                             year);
                break;
                case 1:
                    pos = new BuyInstrument(name, title, group,
                                                         GetPositionParams(xls, iRow),
                                                         doc,
                                                         year);
                    break;
                case 0:
                    pos = new Machine(name, title, group,
                                                         GetPositionParams(xls, iRow),
                                                         doc,
                                                         year);
                    break;
                case 3:
                    pos = new AssembleInstrument(name, title, group,
                                                         GetPositionParams(xls, iRow),
                                                         doc,
                                                         year);
                    break;
                case 4:
                    pos = new MeasureInstrument(name, title, group,
                                                         GetPositionParams(xls, iRow),
                                                         doc,
                                                         year);
                break;
        }

        positions.Add(pos);
    }

    private static Dictionary<string, string> GetPositionParams(ExcelClass xls, int iRow)
    {
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        int iCol = 1;
        string sKey = xls.GetCellStringValue(iCol, _ATTR_TITLE_ROW);
        string sVal = xls.GetCellStringValue(iCol, iRow);
        string psevdoYear = _yearColName;
        if (string.IsNullOrEmpty(psevdoYear))
        {
            psevdoYear = _nameColName;
        }
        while (!string.IsNullOrEmpty(sKey))
        {
            if (_nameColName != GetChar(iCol) &&
                _titleColName != GetChar(iCol) && 
                _docColName != GetChar(iCol) &&
                psevdoYear != GetChar(iCol))
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
            }
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

    private static string GetChar(int jCol)
    {
        return ((char) (jCol + 64)).ToString();
    }

    private static Dictionary<string, string> GetGroupParams(ExcelClass xls)
    {
        _logger.WriteLine("Получение параметров группы");
        Dictionary<string, string> parametrs = new Dictionary<string, string>();
        int iCol = 1;
        string sKey = xls.GetCellStringValue(iCol, _ATTR_TITLE_ROW);
        string sVal = xls.GetCellStringValue(iCol, _ATTR_NAME_ROW);
        string psevdoYear = _yearColName;
        if (string.IsNullOrEmpty(psevdoYear))
        {
            psevdoYear = _nameColName;
        }
        while (!string.IsNullOrEmpty(sKey))
        {
            if (_nameColName != GetChar(iCol) &&
                _titleColName != GetChar(iCol) &&
                _docColName != GetChar(iCol) &&
                psevdoYear != GetChar(iCol))
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
                _logger.WriteLine(string.Format("\"{0}\" - \"{1}\"", sKey, sVal));
            }
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

