using System.Collections.Generic;

/// <summary>
/// Класс для добавления папок на позиции.
/// </summary>
public class FolderUserPosition
{
    private const string _TABLE_NAME = "INS_DIR";

    private readonly int _folderGroupId;
    private readonly int _positionId;

    private const int _N_NAME_CHAR = 60;

    /// <summary>
    /// Конструктор для добавления папок на позиции.
    /// </summary>
    /// <param name="folderGroupId">ID папки группы.</param>
    /// <param name="positionId">ID позиции.</param>
    public FolderUserPosition(int folderGroupId, int positionId)
    {
        _folderGroupId = folderGroupId;
        _positionId = positionId;
    }

    /// <summary>
    /// Метод, записывающий данные в БД.
    /// </summary>
    public void WriteToDb()
    {
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("FNAME", Instr.PrepareSqlParamString("Инструменты НСИ", _N_NAME_CHAR));
        sqlParams.Add("GROUPID", _folderGroupId.ToString());
        sqlParams.Add("POSITIONID", _positionId.ToString());
        sqlParams.Add("FTYPE", 1.ToString());

        string query = "insert into " + SqlOracle.PreLogin + _TABLE_NAME;
        query += @" values (:GROUPID, :FNAME, :POSITIONID, :FTYPE)";
        SqlOracle.Insert(query, sqlParams);
    }
}

