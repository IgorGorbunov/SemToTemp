using System.Collections.Generic;

/// <summary>
/// Класс для записи данных о покупном инструменте в БД Темп2.
/// </summary>
public class BuyInstrument : Position
{
    private string _type, _toolType, _vidOsn;
    
    /// <summary>
    /// Конструктор для записи данных о покупном инструменте в БД Темп2.
    /// </summary>
    /// <param name="name">Наименование</param>
    /// <param name="title">Обозначение</param>
    /// <param name="groupElement">Группа, в которую входит инструмент</param>
    /// <param name="parametrs">Параметры группы</param>
    /// <param name="doc">Обозначение документа без года</param>
    /// <param name="docYear">Год документа</param>
    public BuyInstrument(string name, string title, GroupElement groupElement, Dictionary<string, string> parametrs, string doc, string docYear)
        :base(name, title, groupElement, parametrs, doc, docYear)
    {

    }

    /// <summary>
    /// Метод, записывающий данные в БД.
    /// </summary>
    public void WriteToDb()
    {
        AddParams();
        Dictionary<string, string> sqlParams = new Dictionary<string, string>();
        sqlParams.Add("BIGTITLE", SBigName);
        sqlParams.Add("IDG", SIdGroup.ToString());
        sqlParams.Add("IDN", Id.ToString());
        sqlParams.Add("TYPE", _type);
        sqlParams.Add("TOOLTYPE", _toolType);
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
        sqlParams.Add("VIDOSN", _vidOsn);
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

    private void AddParams()
    {
        AddSqlPosParam();
        _type = ((int)ElementType.Tool).ToString();
        _toolType = ((int)ToolType.Gost).ToString();
        _vidOsn = GetEnumSql(EquipType.CuttingTool);
    }

    

    
}

