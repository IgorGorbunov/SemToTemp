using System;
using System.Collections.Generic;
using System.Text;


class Folder : Element
{
    public int Id;

    private const string _ID_COL_NAME = "TR_NG";
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
    /// ����������� ��� ���������� ����� �� ������.
    /// </summary>
    /// <param name="name">��� �����.</param>
    /// <param name="parentId">ID ���������� �����.</param>
    /// <param name="level">������� �����������.</param>
    /// <param name="type">��� ����� (0 - ������, 1 - �������).</param>
    public Folder(string name, int parentId, int isChild, List<int> groupIds)
    {
        _name = name;
        _parentId = parentId;
        _isChild = isChild;
        _groupIds = groupIds;
        SetGroups();
    }

    /// <summary>
    /// ����������� ��� ���������� ����� �� ������.
    /// </summary>
    /// <param name="name">��� �����.</param>
    /// <param name="parentId">ID ���������� �����.</param>
    /// <param name="isChild"></param>
    public Folder(string name, int parentId, int isChild) :this(name, parentId, isChild, null)
    {

    }

    public static bool Exist(string name, out int id)
    {
        string query = "select " + _ID_COL_NAME + " from " + SqlOracle.PreLogin + _TABLE_NAME + " where " + _NAME_COL_NAME + " = :GNAME";
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("GNAME", Instr.PrepareSqlParamString(name, _N_NAME_CHAR));
        return SqlOracle.Sel(query, sqlParams, out id);
    }

    /// <summary>
    /// �����, ������������ ������ � ��.
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

