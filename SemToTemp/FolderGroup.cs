using System;
using System.Collections.Generic;
using System.Text;


public class FolderGroup : Element
{
    public int Id;

    private string idColName = "TR_NN";
    private string tableName = "Tree_Dir";

    private string _name;
    private int _parentId;
    private int _level;
    private int _type;

    private const int _N_NAME_CHAR = 200;

    public FolderGroup(string name, int parentId, int level, int type)
    {
        _name = name;
        _parentId = parentId;
        _level = level;
        _type = type;
    }

    /// <summary>
    /// Метод, записывающий данные в БД.
    /// </summary>
    public void WriteToDb()
    {
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("FNAME", Instr.PrepareSqlParamString(_name, _N_NAME_CHAR));
        Id = GetFreeId(idColName, tableName);
        sqlParams.Add("IDF", Id.ToString());
        sqlParams.Add("PARENTID", _parentId.ToString());
        sqlParams.Add("FLEVEL", _level.ToString());
        sqlParams.Add("FTYPE", _type.ToString());
        sqlParams.Add("LOGINUSER", Instr.PrepareSqlParamString(SqlOracle.Login, NUserNameChar));
        sqlParams.Add("TODAYDATE", Instr.GetSqlToday());

        string query = "insert into " + SqlOracle.PreLogin + tableName;
        query += @" values (:IDF, :FNAME, :PARENTID,
                            :FLEVEL, :FTYPE, :LOGINUSER, :TODAYDATE)";
        SqlOracle.Insert(query, sqlParams);
    }
}

