using System;
using System.Collections.Generic;
using System.Text;


public class FolderPosition
{

    private string tableName = "INS_DIR";

    private int _folderGroupId;
    private int _positionId;

    private const int _N_NAME_CHAR = 60;

    public FolderPosition(int folderGroupId, int positionId)
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
        sqlParams.Add("FNAME", Instr.PrepareSqlParamString("   ", _N_NAME_CHAR));
        sqlParams.Add("GROUPID", _folderGroupId.ToString());
        sqlParams.Add("POSITIONID", _positionId.ToString());
        sqlParams.Add("FTYPE", 1.ToString());


        string query = "insert into " + SqlOracle.PreLogin + tableName;
        query += @" values (:GROUPID, :FNAME, :POSITIONID, :FTYPE)";
        SqlOracle.Insert(query, sqlParams);
    }
}

