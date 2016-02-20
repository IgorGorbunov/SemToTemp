using System.Collections.Generic;

/// <summary>
/// ����� ��� ������ ������ � �������� ����������� � �� ����2.
/// </summary>
public class BuyInstrument : Position
{
    private string _type, _toolType, _vidOsn;
    
    /// <summary>
    /// ����������� ��� ������ ������ � �������� ����������� � �� ����2.
    /// </summary>
    /// <param name="name">������������</param>
    /// <param name="title">�����������</param>
    /// <param name="groupElement">������, � ������� ������ ����������</param>
    /// <param name="parametrs">��������� ������</param>
    /// <param name="doc">����������� ��������� ��� ����</param>
    /// <param name="docYear">��� ���������</param>
    public BuyInstrument(string name, string title, GroupElement groupElement, Dictionary<string, string> parametrs, string doc, string docYear)
        :base(name, title, groupElement, parametrs, doc, docYear)
    {
        AddSqlPosParam();
        Stype = ((int)ElementType.Tool).ToString();
        Stool = ((int)ToolType.Gost).ToString();
        SvidOsn = GetEnumSql(EquipType.CuttingTool);
    }


    

    
}

