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
        AddSqlPosParam();
        Stype = ((int)ElementType.Tool).ToString();
        Stool = ((int)ToolType.Gost).ToString();
        SvidOsn = GetEnumSql(EquipType.CuttingTool);
    }


    

    
}

