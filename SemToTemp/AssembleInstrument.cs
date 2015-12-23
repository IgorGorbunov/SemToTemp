using System;
using System.Collections.Generic;
using System.Text;


class AssembleInstrument : Position
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
    public AssembleInstrument(string name, string title, GroupElement groupElement, Dictionary<string, string> parametrs, string doc, string docYear)
    :base(name, title, groupElement, parametrs, doc, docYear)
    {
        AddSqlPosParam();
        Stype = ((int)ElementType.Tool).ToString();
        Stool = ((int)ToolType.Gost).ToString();
        SvidOsn = "СЛ";
    }

    //public void WriteToDb()
    //{
    //    AddParams();
    //    WriteToDb2(_type, _toolType, _vidOsn);
    //}

    //private void AddParams()
    //{
    //    AddSqlPosParam();
    //    _type = ((int) ElementType.Material).ToString();
    //    _toolType = ((int) ToolType.Gost).ToString();
    //    _vidOsn = "  ";
    //}

}

