using System.Collections.Generic;

/// <summary>
/// Класс для добавления папок на группы.
/// </summary>
public class FolderGroup : Element
{
    public int Id;

    private const string _ID_COL_NAME = "TR_NN";
    private const string _NAME_COL_NAME = "TR_NM";
    private const string _TABLE_NAME = "Tree_Dir";

    private readonly string _name;
    private readonly int _parentId;
    private readonly int _level;
    private readonly int _type;

    private const int _N_NAME_CHAR = 200;

    /// <summary>
    /// Конструктор для добавления папок на группы.
    /// </summary>
    /// <param name="name">Имя папки.</param>
    /// <param name="parentId">ID предыдущей папки.</param>
    /// <param name="level">Уровень вложенности.</param>
    /// <param name="type">Тип папки (0 - группа, 1 - позиция).</param>
    public FolderGroup(string name, int parentId, int level, int type)
    {
        _name = name;
        _parentId = parentId;
        _level = level;
        _type = type;
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
        sqlParams.Add("FLEVEL", _level.ToString());
        sqlParams.Add("FTYPE", _type.ToString());
        sqlParams.Add("LOGINUSER", Instr.PrepareSqlParamString(SqlOracle.Login, NUserNameChar));
        sqlParams.Add("TODAYDATE", Instr.GetSqlToday());

        string query = "insert into " + SqlOracle.PreLogin + _TABLE_NAME;
        query += @" values (:IDF, :FNAME, :PARENTID,
                            :FLEVEL, :FTYPE, :LOGINUSER, :TODAYDATE)";
        SqlOracle.Insert(query, sqlParams);
    }
}

