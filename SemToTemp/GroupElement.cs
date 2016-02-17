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
    public readonly string Name;
    public readonly string FullName;

    public int UserFolderId;
    public const int NParamNameChar = 2;

    
    private readonly Dictionary<string, string> _parametrs;
    int _isSketch = 0;
    string _fileExt = "";
    string _file = "";
    
    private int _id = -1;
    private List<int> _listId; 

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

    public static GroupElement GetGroupElement(int id)
    {
        return new GroupElement(id, GetName(id), null);
    }

    public GroupElement(int id, string name, string fullName)
    {
        _id = id;
        FullName = fullName;
        Name = name;
    }

    public GroupElement(string name, Dictionary<string, string> parametrs, string fullName)
    {
        Name = name;
        if (parametrs == null)
        {
            _parametrs = new Dictionary<string, string>();
        }
        else
        {
            _parametrs = parametrs;
        }
        FullName = fullName;
        _listId = new List<int>();
    }


    public void WriteToDb()
    {
        string nameSql = Instr.PrepareSqlParamString(Name, _N_CHAR);
        string[,] paramSql = GetTenParams();
        string sqlToday = Instr.GetSqlToday();
        string sqlLogin = Instr.PrepareSqlParamString(SqlOracle.Login, Element.NUserNameChar);
        string sqlFileExt = _fileExt;
        string sqlFile = _file;

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
        sqlParams.Add("SKETCH", _isSketch.ToString());
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

    public void AddUserFolders()
    {
        string[] split = FullName.Split('\\');
        int parentId = 0;
        int level = 0;
        foreach (string name in split)
        {
            if (string.IsNullOrEmpty(name))
            {
                continue;
            }
            int existId;
            if (FolderUserGroup.Exist(name.Trim(), out existId))
            {
                parentId = existId;
            }
            else
            {
                FolderUserGroup folderUser = new FolderUserGroup(name.Trim(), parentId, level, 1);
                folderUser.WriteToDb();
                parentId = folderUser.Id;
            }
            
            level++;
        }
        UserFolderId = parentId;
    }

    public void AddGeneralFolders(string begin)
    {
        begin += FullName;
        string[] split1 = begin.Split('\\');
        string[] split = ReSize(split1);
        int parentId = 0;
        int isChild = 0;
        Folder parentFolder = null;
        bool exist = false;
        for (int i = 0; i < split.Length - 1; i++)
        {
            if (string.IsNullOrEmpty(split[i]))
            {
                continue;
            }
            int existId;
            if (Folder.Exist(split[i].Trim(), out existId))
            {
                parentId = existId;
                exist = true;
                //poslednya papka
                if (i == split.Length - 2)
                {
                    Folder.UpdateFolderId(parentId, Id, false);
                }
            }
            else
            {
                Folder folder;
                if (i == split.Length - 2)
                {
                    _listId = new List<int>();
                    _listId.Add(Id);
                    folder = new Folder(split[i].Trim(), parentId, isChild, _listId);
                }
                else
                {
                    folder = new Folder(split[i].Trim(), parentId, isChild);
                }
                folder.WriteToDb();
                if (exist)
                {
                    Folder.UpdateFolderId(parentId, folder.Id, true);
                    exist = false;
                }
                parentId = folder.Id;
                if (parentFolder != null)
                {
                    parentFolder.AddFolderId(folder.Id);
                    parentFolder.AddEditions();
                }
                parentFolder = folder;
            }
            isChild = 1;
        }
    }


    private string[] ReSize(string[] mass)
    {
        List<string> list = new List<string>();
        for (int i = 0; i < mass.Length; i++)
        {
            if (!string.IsNullOrEmpty(mass[i]))
            {
                list.Add(mass[i]);
            }
        }
        string[] newMass = new string[list.Count];
        int j = 0;
        foreach (string s in list)
        {
            newMass[j] = s;
            j++;
        }
        return newMass;
    }
    

    private void SetId()
    {
        _id = GetFreeId();
    }

    private int GetFreeId()
    {
        List<int> ids = SqlOracle.Sel<int>("select T1_NG from " + SqlOracle.PreLogin + "TABLE_1 order by T1_NG");
        int i = 0;
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

    //--------------------------  SQL  ---------------------------------

    private static string GetName(int id)
    {
        string query = "select t1_nm from " + SqlOracle.PreLogin + "TABLE_1 where T1_NG = :IDG";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("IDG", id.ToString());
        string name;
        SqlOracle.Sel(query, sqlParams, out name);
        return name;
    }

    private int GetSketch(int id)
    {
        string query = "select t1_r1 from " + SqlOracle.PreLogin + "TABLE_1 where T1_NG = :IDG";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("IDG", id.ToString());
        int sketch;
        SqlOracle.Sel(query, sqlParams, out sketch);
        return sketch;
    }

 
}

