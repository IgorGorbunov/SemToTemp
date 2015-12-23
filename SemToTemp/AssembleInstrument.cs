using System;
using System.Collections.Generic;
using System.Text;


class AssembleInstrument : Position
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
    public AssembleInstrument(string name, string title, GroupElement groupElement, Dictionary<string, string> parametrs, string doc, string docYear)
    :base(name, title, groupElement, parametrs, doc, docYear)
    {
        AddSqlPosParam();
        Stype = ((int)ElementType.Tool).ToString();
        Stool = ((int)ToolType.Gost).ToString();
        SvidOsn = "��";
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

