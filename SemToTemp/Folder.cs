using System;
using System.Collections.Generic;
using System.Text;


class Folder : Element
{
    public int Id;

    private const string _ID_COL_NAME = "TR_NG";
    private const string _CHILD_ID_COL_NAME = "TR_NG1";
    private const string _NAME_COL_NAME = "TR_NM";
    private const string _TABLE_NAME = "TREE_GR_INS";

    private readonly string _name;
    private readonly int _parentId;
    private readonly int _isChild;
    private readonly List<int> _groupIds;
    private string _groups;

    private const int _N_NAME_CHAR = 200;
    private const int _N_GROUPS_CHAR = 2000;

    /// <summary>
    /// Конструктор для добавления папок на группы.
    /// </summary>
    /// <param name="name">Имя папки.</param>
    /// <param name="parentId">ID предыдущей папки.</param>
    /// <param name="level">Уровень вложенности.</param>
    /// <param name="type">Тип папки (0 - группа, 1 - позиция).</param>
    public Folder(string name, int parentId, int isChild, List<int> groupIds)
    {
        _name = name;
        _parentId = parentId;
        _isChild = isChild;
        _groupIds = groupIds;
        SetGroups();
    }

    /// <summary>
    /// Конструктор для добавления папок на группы.
    /// </summary>
    /// <param name="name">Имя папки.</param>
    /// <param name="parentId">ID предыдущей папки.</param>
    /// <param name="isChild"></param>
    public Folder(string name, int parentId, int isChild) :this(name, parentId, isChild, null)
    {

    }

    public static void UpdateFolderId(int id, int newChildId, bool isGroup)
    {
        string query = "select " + _CHILD_ID_COL_NAME + " from " + SqlOracle.PreLogin + _TABLE_NAME + " where " + _ID_COL_NAME + " = :GNAME";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("GNAME", id.ToString());
        string childs;
        SqlOracle.Sel(query, sqlParams, out childs);
        if (HasChild(childs, newChildId))
        {
            return;
        }
        char c;
        if (isGroup)
        {
            c = '-';
        }
        else
        {
            c = ' ';
        }
        childs += string.Format("{0}{1}", c, newChildId.ToString("D5"));

        sqlParams = new Dictionary<string, string>();
        string sGroups = Instr.PrepareSqlParamString(childs, _N_GROUPS_CHAR);
        sqlParams.Add("GROUPS", Instr.AddFirstSpace(sGroups));
        sqlParams.Add("IDF", id.ToString());

        query = "update " + SqlOracle.PreLogin + _TABLE_NAME;
        query += @" set tr_ng1 = :GROUPS where tr_ng = :IDF";
        SqlOracle.Update(query, sqlParams);
    }

    private static bool HasChild(string childs, int newiD)
    {
        string[] split = childs.Split(' ', '-');
        foreach (string s in split)
        {
            if (!string.IsNullOrEmpty(s))
            {
                int number = int.Parse(s);
                if (number == newiD)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool Exist(string name, out int id)
    {
        string query = "select " + _ID_COL_NAME + " from " + SqlOracle.PreLogin + _TABLE_NAME + " where " + _NAME_COL_NAME + " = :GNAME";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("GNAME", Instr.PrepareSqlParamString(name, _N_NAME_CHAR));
        return SqlOracle.Sel(query, sqlParams, out id);
    }

    /// <summary>
    /// Метод, записывающий данные в БД.
    /// </summary>
    public void WriteToDb()
    {
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("FNAME", Instr.PrepareSqlParamString(_name, _N_NAME_CHAR));
        Id = GetFreeId(_ID_COL_NAME, _TABLE_NAME);
        sqlParams.Add("IDF", Id.ToString());
        sqlParams.Add("PARENTID", _parentId.ToString());
        sqlParams.Add("ISCHILD", _isChild.ToString());
        string sGroups = Instr.PrepareSqlParamString(_groups, _N_GROUPS_CHAR);
        sqlParams.Add("GROUPS", Instr.AddFirstSpace(sGroups));
        sqlParams.Add("LOGINUSER", Instr.PrepareSqlParamString(SqlOracle.Login, NUserNameChar));
        sqlParams.Add("TODAYDATE", Instr.GetSqlToday());

        string query = "insert into " + SqlOracle.PreLogin + _TABLE_NAME;
        query += @" (tr_ng, tr_nm, tr_pr1, tr_pr2, tr_ng1, up_dt, up_us) values (:IDF, :FNAME, :ISCHILD, :PARENTID,
                             :GROUPS, :TODAYDATE, :LOGINUSER)";
        SqlOracle.Insert(query, sqlParams);
    }

    public void AddId(int id)
    {
        _groups += string.Format(" {0}", id.ToString("D5"));
    }

    public void AddFolderId(int id)
    {
        _groups += string.Format("-{0}", id.ToString("D5"));
    }

    public void AddEditions()
    {
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("FNAME", Instr.PrepareSqlParamString(_name, _N_NAME_CHAR));
        sqlParams.Add("IDF", Id.ToString());
        sqlParams.Add("PARENTID", _parentId.ToString());
        sqlParams.Add("ISCHILD", _isChild.ToString());
        string sGroups = Instr.PrepareSqlParamString(_groups, _N_GROUPS_CHAR);
        sqlParams.Add("GROUPS", Instr.AddFirstSpace(sGroups));
        sqlParams.Add("LOGINUSER", Instr.PrepareSqlParamString(SqlOracle.Login, NUserNameChar));
        sqlParams.Add("TODAYDATE", Instr.GetSqlToday());

        string query = "update " + SqlOracle.PreLogin + _TABLE_NAME;
        query += @" set tr_nm = :FNAME, tr_pr1 = :ISCHILD, tr_pr2 = :PARENTID, tr_ng1 = :GROUPS, up_dt = :TODAYDATE, up_us = :LOGINUSER where tr_ng = :IDF";
        SqlOracle.Update(query, sqlParams);
    }

    private void SetGroups()
    {
        _groups = "";
        if (_groupIds == null)
            return;
        foreach (int groupId in _groupIds)
        {
            _groups += string.Format(" {0}", groupId.ToString("D5"));
        }
    }
}

