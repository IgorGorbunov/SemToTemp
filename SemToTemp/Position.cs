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
    public readonly string Title;
    public int SIdGroup;

    private const int _N_NAME_CHAR = 200;
    private const int _N_CHAR = 100;
    private const int _N_YEAR_CHAR = 4;

    private readonly string _name;
    private readonly Dictionary<string, string> _parametrs;
    private readonly string _doc = "";
    private readonly string _docYear = "";
    private readonly GroupElement _groupElement;

    protected int _id = -1;

    
    protected string[,] SParams, SGroupParams, SParams10;
    protected string SModelType, SGeom, SdocFile, SNotes,
        STitle, SName, SDocType, SDocYear, SBigName, Stype, SvidOsn, Stool;

    private string _fullDoc;

    public static bool Exist(string title, out int id)
    {
        string query = "select T2_NN from " + SqlOracle.PreLogin + "TABLE_2 where T2_OBOZ = :GNAME";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("GNAME", Instr.PrepareSqlParamString(title, _N_CHAR));
        return SqlOracle.Sel(query, sqlParams, out id);
    }

    public static int GetGroupId(int posId)
    {
        string query = "select T2_NG from " + SqlOracle.PreLogin + "TABLE_2 where T2_NN = :IDN";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("IDN", posId.ToString());
        int gId;
        SqlOracle.Sel(query, sqlParams, out gId);
        return gId;
    }

    public int GetGroupId()
    {
        string query = "select T2_NG from " + SqlOracle.PreLogin + "TABLE_2 where T2_NN = :IDN";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("IDN", Id.ToString());
        int gId;
        SqlOracle.Sel(query, sqlParams, out gId);
        return gId;
    }


    public void AddUserFolder()
    {
        FolderUserPosition folderUser = new FolderUserPosition(_groupElement.UserFolderId, Id);
        folderUser.WriteToDb();
    }

    /// <summary>
    /// Метод, записывающий данные в БД.
    /// </summary>
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
            sqlParams.Add("PA" + i, SParams10[i, 0]);
            sqlParams.Add("PS" + i, SParams10[i, 1]);
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

    public bool IsSimilarGroupParams(List<string> parametrs, out string mess)
    {
        int p = 0;
        mess = "";
        SGroupParams = new string[NParams, NColParams];
        foreach (string parametr in parametrs)
        {
            bool found = false;
            for (int i = 0; i < SParams.Length / NColParams; i++)
            {
                if (parametr == SParams[i, 0])
                {
                    SGroupParams[p, 0] = SParams[i, 0];
                    SGroupParams[p, 1] = SParams[i, 1];
                    p++;
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                mess += "*** Не найден параметр " + parametr + Environment.NewLine;
            }
        }
        if (p == parametrs.Count)
        {
            return true;
        }
        return false;
    }

    protected Position(string name, string title, GroupElement groupElement, Dictionary<string, string> parametrs, string doc, string docYear)
    {
        _name = name;
        Title = title;
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
        SParams = GetSqlParams(TempElement.SingleElement, _parametrs);
        SParams10 = GetTenParams();
        SGeom = "NULL";
        SdocFile = "NULL";
        STitle = Instr.PrepareSqlParamString(Title, _N_CHAR);
        SName = Instr.PrepareSqlParamString(_name, _N_CHAR);
        SDocType = Instr.PrepareSqlParamString(_doc, _N_CHAR);
        SDocYear = Instr.PrepareSqlParamString(_docYear, _N_YEAR_CHAR);
        _fullDoc = GetFullDoc(_doc, _docYear);
        SNotes = GetNotes();
        SBigName = String.Format("{0} {1} {2}", _name, Title, _fullDoc);
        if (!string.IsNullOrEmpty(SNotes))
        {
            SBigName += string.Format(" ({0})", SNotes);
        }
        SBigName = Instr.PrepareSqlParamString(SBigName, _N_NAME_CHAR);
        SBigName = SBigName.Substring(0, _N_NAME_CHAR - 10);
        SNotes = Instr.PrepareSqlParamString(SNotes, _N_CHAR);
    }

    protected string[,] GetTenParams()
    {
        if (SGroupParams == null)
        {
            string[,] array = new string[NParams, NColParams];
            int i;
            for (i = 0; i < NParams; i++)
            {
                if (SParams.Length / NColParams > i)
                {
                    array[i, 0] = SParams[i, 0];
                    array[i, 1] = SParams[i, 1];
                }
                else
                {
                    array[i, 0] = "NULL";
                    array[i, 1] = "NULL";
                }
                
            }
            SParams10 = array;
        }
        else
        {
            SParams10 = SGroupParams;
        }
        return SParams10;
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

    //--------------------------------------- SQL -------------------------------

    public void SetNewGroup(int posId, int newGroupId)
    {
        string query = "UPDATE " + SqlOracle.PreLogin + "table_2 set t2_ng = :IDG where t2_nn = :IDN";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("IDG", newGroupId.ToString());
        sqlParams.Add("IDN", posId.ToString());
        SqlOracle.Update(query, sqlParams);
    }
}

