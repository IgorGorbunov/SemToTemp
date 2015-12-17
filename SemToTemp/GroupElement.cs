using System.Collections.Generic;


public class GroupElement
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

    public int FolderId;
    public const int NParamNameChar = 2;

    private readonly string _name;
    private readonly Dictionary<string, string> _parametrs;
    private readonly string _fullName;
    private int _id = -1;

    private const int _N_PARAMS = 10;
    private const int _N_COL_PARAMS = 2;

    private const int _N_CHAR = 80;
    

    public static bool Exist(string name, out int id)
    {
        string query = "select T1_NG from " + SqlOracle.PreLogin + "TABLE_1 where T1_NM = :GNAME";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("GNAME", Instr.PrepareSqlParamString(name, _N_CHAR));
        return SqlOracle.Sel(query, sqlParams, out id);
    }

    public GroupElement(string name, Dictionary<string, string> parametrs, string fullName)
    {
        _name = name;
        if (parametrs == null)
        {
            _parametrs = new Dictionary<string, string>();
        }
        else
        {
            _parametrs = parametrs;
        }
        _fullName = fullName;
    }


    public void WriteToDb()
    {
        string nameSql = Instr.PrepareSqlParamString(_name, _N_CHAR);
        string[,] paramSql = GetTenParams();
        string sqlToday = Instr.GetSqlToday();
        string sqlLogin = Instr.PrepareSqlParamString(SqlOracle.Login, Element.NUserNameChar);
        int isSketch = 0;
        string sqlFileExt = "NULL";
        string sqlFile = "NULL";

        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("IDGROUP", Id.ToString());
        sqlParams.Add("NAME", nameSql);
        for (int i = 0; i < _N_PARAMS; i++)
        {
            sqlParams.Add("PA" + i, paramSql[i, 0]);
            sqlParams.Add("PS" + i, paramSql[i, 1]);
        }
        sqlParams.Add("TODAYDATE", sqlToday);
        sqlParams.Add("LOGINUSER", sqlLogin);
        sqlParams.Add("SKETCH", isSketch.ToString());
        sqlParams.Add("FILEEXT", sqlFileExt);
        sqlParams.Add("FILEBLOB", sqlFile);

        string query = @"insert into " + SqlOracle.PreLogin + "TABLE_1 ";
        query += @"values (:IDGROUP, :NAME, 
                            :PA0, :PS0, :PA1, :PS1, :PA2, :PS2, :PA3, :PS3, 
                            :PA4, :PS4, :PA5, :PS5, :PA6, :PS6, :PA7, :PS7, 
                            :PA8, :PS8, :PA9, :PS9, :TODAYDATE, :LOGINUSER,
                            :SKETCH, :FILEEXT, :FILEBLOB)";
        SqlOracle.Insert(query, sqlParams);
    }

    public void AddFolders()
    {
        string[] split = _fullName.Split('\\');
        int parentId = 0;
        int level = 0;
        foreach (string name in split)
        {
            FolderGroup folder = new FolderGroup(name.Trim(), parentId, level, 1);
            folder.WriteToDb();
            parentId = folder.Id;
            level++;
        }
        FolderId = parentId;
    }

    

    private void SetId()
    {
        _id = GetFreeId();
    }

    private int GetFreeId()
    {
        List<int> ids = SqlOracle.Sel<int>("select T1_NG from " + SqlOracle.PreLogin + "TABLE_1 order by T1_NG");
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

    private string[,] GetTenParams()
    {
        string[,] paramSql = new string[_N_PARAMS, _N_COL_PARAMS];
        int i = 0;
        foreach (KeyValuePair<string, string> keyValuePair in _parametrs)
        {
            paramSql[i, 0] = Instr.PrepareSqlParamString(keyValuePair.Key, NParamNameChar);
            paramSql[i, 1] = Instr.PrepareSqlParamString(keyValuePair.Value, _N_CHAR);
            i++;
            if (i >= _N_PARAMS)
            {
                break;
            }
        }
        while (i < _N_PARAMS)
        {
            paramSql[i, 0] = "NULL";
            paramSql[i, 1] = "NULL";
            i++;
        }
        return paramSql;
    }
}

