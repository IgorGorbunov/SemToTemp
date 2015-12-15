using System;
using System.Collections.Generic;

/// <summary>
/// Класс для записи данных о позиции в БД Темп2.
/// </summary>
public class Position : Element
{
    public int Id
    {
        get
        {
            if (_id == -1)
            {
                SetId();
            }
            return _id;
        }
    }

    private const int _N_NAME_CHAR = 200;
    private const int _N_CHAR = 100;
    private const int _N_YEAR_CHAR = 4;

    private readonly string _name;
    private readonly string _title;
    private readonly Dictionary<string, string> _parametrs;
    private readonly string _doc = "";
    private readonly string _docYear = "";
    private readonly GroupElement _groupElement;

    protected int _id = -1;

    protected int SIdGroup;
    protected string[,] SParams;
    protected string SModelType, SGeom, SdocFile, SNotes,
        STitle, SName, SDocType, SDocYear, SBigName;

    private string _fullDoc;


    protected Position(string name, string title, GroupElement groupElement, Dictionary<string, string> parametrs, string doc, string docYear)
    {
        _name = name;
        _title = title;
        if (parametrs == null)
        {
            _parametrs = new Dictionary<string, string>();
        }
        else
        {
            _parametrs = parametrs;
        }
        _groupElement = groupElement;
        _doc = doc;
        _docYear = docYear;
    }

    protected void AddSqlPosParam()
    {
        AddSqlElParam();
        SIdGroup = _groupElement.Id;
        SModelType = "0";
        SParams = GetTenParams(TempElement.SingleElement, _parametrs);
        SGeom = "NULL";
        SdocFile = "NULL";
        STitle = Instr.PrepareSqlParamString(_title, _N_CHAR);
        SName = Instr.PrepareSqlParamString(_name, _N_CHAR);
        SDocType = Instr.PrepareSqlParamString(_doc, _N_CHAR);
        SDocYear = Instr.PrepareSqlParamString(_docYear, _N_YEAR_CHAR);
        _fullDoc = GetFullDoc(_doc, _docYear);
        SNotes = GetNotes();
        SBigName = String.Format("{0} {1} {2}", _name, _title, _fullDoc);
        if (!string.IsNullOrEmpty(SNotes))
        {
            SBigName += string.Format(" ({0})", SNotes);
        }
        SBigName = Instr.PrepareSqlParamString(SBigName, _N_NAME_CHAR);
        SNotes = Instr.PrepareSqlParamString(SNotes, _N_CHAR);
    }

    private void SetId()
    {
        _id = GetFreeId();
    }

    private int GetFreeId()
    {
        List<int> ids = SqlOracle.Sel<int>("select T2_NN from " + SqlOracle.PreLogin + "TABLE_2 order by T2_NN");
        int i = 1;
        foreach (int id in ids)
        {
            if (id > i)
            {
                return i;
            }
            i++;
        }
        return i;
    }

    private string GetNotes()
    {
        string text = "";
        foreach (KeyValuePair<string, string> keyValuePair in _parametrs)
        {
            if (!string.IsNullOrEmpty(keyValuePair.Key.Trim()))
            {
                text += string.Format("{0}={1} ", keyValuePair.Key.Trim(), keyValuePair.Value.Trim());
            }
        }
        return text.Trim();
    }
}

