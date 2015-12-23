using System;
using System.Collections.Generic;

/// <summary>
/// Класс для записи данных о позиции в БД Темп2.
/// </summary>
public class Position : Element
{
    public int Id
    {
        get
        {
            if (_id == -1)
            {
                SetId();
            }
            return _id;
        }
    }

    private const int _N_NAME_CHAR = 200;
    private const int _N_CHAR = 100;
    private const int _N_YEAR_CHAR = 4;

    private readonly string _name;
    private readonly string _title;
    private readonly Dictionary<string, string> _parametrs;
    private readonly string _doc = "";
    private readonly string _docYear = "";
    private readonly GroupElement _groupElement;

    protected int _id = -1;

    protected int SIdGroup;
    protected string[,] SParams;
    protected string SModelType, SGeom, SdocFile, SNotes,
        STitle, SName, SDocType, SDocYear, SBigName, Stype, SvidOsn, Stool;

    private string _fullDoc;


    public void AddUserFolder()
    {
        FolderUserPosition folderUser = new FolderUserPosition(_groupElement.UserFolderId, Id);
        folderUser.WriteToDb();
    }

    /// <summary>
    /// Метод, записывающий данные в БД.
    /// </summary>
    public void WriteToDb2(string sType, string sTool, string sVidOsn)
    {
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("BIGTITLE", SBigName);
        sqlParams.Add("IDG", SIdGroup.ToString());
        sqlParams.Add("IDN", Id.ToString());
        sqlParams.Add("TYPE", sType);
        sqlParams.Add("TOOLTYPE", sTool);
        sqlParams.Add("MODELTYPE", SModelType);
        for (int i = 0; i < NParams; i++)
        {
            sqlParams.Add("PA" + i, SParams[i, 0]);
            sqlParams.Add("PS" + i, SParams[i, 1]);
        }
        sqlParams.Add("GEOM", SGeom);
        sqlParams.Add("TODAYDATE", SqlToday);
        sqlParams.Add("LOGINUSER", SqlLogin);
        sqlParams.Add("DOCFILE", SdocFile);
        sqlParams.Add("VIDOSN", sVidOsn);
        sqlParams.Add("TITLE", STitle);
        sqlParams.Add("NAME", SName);
        sqlParams.Add("DOCTYPE", SDocType);
        sqlParams.Add("NOTES", SNotes);
        sqlParams.Add("DOCYEAR", SDocYear);

        string query = "insert into " + SqlOracle.PreLogin + "TABLE_2 ";
        query += @"values (:BIGTITLE, :IDG, :IDN,
                            :TYPE, :TOOLTYPE, :MODELTYPE,
                            :PA0, :PS0, :PA1, :PS1, :PA2, :PS2, :PA3, :PS3, 
                            :PA4, :PS4, :PA5, :PS5, :PA6, :PS6, :PA7, :PS7, 
                            :PA8, :PS8, :PA9, :PS9, :GEOM, 
                            :TODAYDATE, :LOGINUSER, :DOCFILE, :VIDOSN,
                            :TITLE, :NAME, :DOCTYPE, :NOTES, :DOCYEAR)";
        SqlOracle.Insert(query, sqlParams);
    }

    public void WriteToDb2()
    {
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("BIGTITLE", SBigName);
        sqlParams.Add("IDG", SIdGroup.ToString());
        sqlParams.Add("IDN", Id.ToString());
        sqlParams.Add("TYPE", Instr.PrepareSqlParamString(Stype, 1));
        sqlParams.Add("TOOLTYPE", Instr.PrepareSqlParamString(Stool, 1));
        sqlParams.Add("MODELTYPE", SModelType);
        for (int i = 0; i < NParams; i++)
        {
            sqlParams.Add("PA" + i, SParams[i, 0]);
            sqlParams.Add("PS" + i, SParams[i, 1]);
        }
        sqlParams.Add("GEOM", SGeom);
        sqlParams.Add("TODAYDATE", SqlToday);
        sqlParams.Add("LOGINUSER", SqlLogin);
        sqlParams.Add("DOCFILE", SdocFile);
        sqlParams.Add("VIDOSN", Instr.PrepareSqlParamString(SvidOsn, 4));
        sqlParams.Add("TITLE", STitle);
        sqlParams.Add("NAME", SName);
        sqlParams.Add("DOCTYPE", SDocType);
        sqlParams.Add("NOTES", SNotes);
        sqlParams.Add("DOCYEAR", SDocYear);

        string query = "insert into " + SqlOracle.PreLogin + "TABLE_2 ";
        query += @"values (:BIGTITLE, :IDG, :IDN,
                            :TYPE, :TOOLTYPE, :MODELTYPE,
                            :PA0, :PS0, :PA1, :PS1, :PA2, :PS2, :PA3, :PS3, 
                            :PA4, :PS4, :PA5, :PS5, :PA6, :PS6, :PA7, :PS7, 
                            :PA8, :PS8, :PA9, :PS9, :GEOM, 
                            :TODAYDATE, :LOGINUSER, :DOCFILE, :VIDOSN,
                            :TITLE, :NAME, :DOCTYPE, :NOTES, :DOCYEAR)";
        SqlOracle.Insert(query, sqlParams);
    }

    protected Position(string name, string title, GroupElement groupElement, Dictionary<string, string> parametrs, string doc, string docYear)
    {
        _name = name;
        _title = title;
        if (parametrs == null)
        {
            _parametrs = new Dictionary<string, string>();
        }
        else
        {
            _parametrs = parametrs;
        }
        _groupElement = groupElement;
        _doc = doc;
        _docYear = docYear;
    }

    protected void AddSqlPosParam()
    {
        AddSqlElParam();
        SIdGroup = _groupElement.Id;
        SModelType = " ";
        SParams = GetTenParams(TempElement.SingleElement, _parametrs);
        SGeom = "NULL";
        SdocFile = "NULL";
        STitle = Instr.PrepareSqlParamString(_title, _N_CHAR);
        SName = Instr.PrepareSqlParamString(_name, _N_CHAR);
        SDocType = Instr.PrepareSqlParamString(_doc, _N_CHAR);
        SDocYear = Instr.PrepareSqlParamString(_docYear, _N_YEAR_CHAR);
        _fullDoc = GetFullDoc(_doc, _docYear);
        SNotes = GetNotes();
        SBigName = String.Format("{0} {1} {2}", _name, _title, _fullDoc);
        if (!string.IsNullOrEmpty(SNotes))
        {
            SBigName += string.Format(" ({0})", SNotes);
        }
        SBigName = Instr.PrepareSqlParamString(SBigName, _N_NAME_CHAR);
        SNotes = Instr.PrepareSqlParamString(SNotes, _N_CHAR);
    }

    private void SetId()
    {
        _id = GetFreeId();
    }

    private int GetFreeId()
    {
        List<int> ids = SqlOracle.Sel<int>("select T2_NN from " + SqlOracle.PreLogin + "TABLE_2 order by T2_NN");
        int i = 1;
        foreach (int id in ids)
        {
            if (id > i)
            {
                return i;
            }
            i++;
        }
        return i;
    }

    private string GetNotes()
    {
        string text = "";
        int i = 1;
        foreach (KeyValuePair<string, string> keyValuePair in _parametrs)
        {
            if (!string.IsNullOrEmpty(keyValuePair.Key.Trim()))
            {
                text += string.Format("{0}={1} ", keyValuePair.Key.Trim(), keyValuePair.Value.Trim());
                i++;
            }
            if (i > NParams)
            {
                break;
            }
        }
        return text.Trim();
    }
}

