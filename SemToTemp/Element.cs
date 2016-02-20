using System;
using System.Collections.Generic;

/// <summary>
/// Класс для записи данных об элемента в БД Темп2.
/// </summary>
public class Element
{
    protected enum TempElement
    {
        GroupElement,
        SingleElement
    }

    protected enum EquipType
    {
        CuttingTool,
        MaterialGeneral
    }

    public enum ElementType
    {
        Machine = 0,
        Tool = 1,
        Material = 2,
        SpecTool = 6,
        CommentMaterial = 7,
        CommentMachine = 8,
        CommentTool = 9
    }

    protected enum ToolType
    {
        Gost,
        Special
    }


    public const int NUserNameChar = 32;
    protected const int NParams = 10;
    protected const int NColParams = 2;

    private const int _N_PARAM_VALUE_CHAR = 8;
    private const int _N_PARAM_NAME_CHAR = 2;
    private const int _N_CHAR = 80;
    

    protected string SqlToday, SqlLogin;

    protected string[,] GetTenParams(TempElement element, Dictionary<string, string> parametrs)
    {
        int nParamValueChar = element == TempElement.SingleElement ? _N_PARAM_VALUE_CHAR : _N_CHAR;
        string[,] paramSql = new string[NParams, NColParams];
        int i = 0;
        foreach (KeyValuePair<string, string> keyValuePair in parametrs)
        {
            paramSql[i, 0] = Instr.PrepareSqlParamString(keyValuePair.Key, _N_PARAM_NAME_CHAR);
            paramSql[i, 1] = Instr.PrepareSqlParamString(keyValuePair.Value, nParamValueChar);
            i++;
            if (i >= NParams)
            {
                break;
            }
        }
        while (i < NParams)
        {
            paramSql[i, 0] = "NULL";
            paramSql[i, 1] = "NULL";
            i++;
        }
        return paramSql;
    }

    protected string[,] GetSqlParams(TempElement element, Dictionary<string, string> parametrs)
    {
        int nParamValueChar = element == TempElement.SingleElement ? _N_PARAM_VALUE_CHAR : _N_CHAR;
        string[,] paramSql = new string[parametrs.Count, NColParams];
        int i = 0;
        foreach (KeyValuePair<string, string> keyValuePair in parametrs)
        {
            paramSql[i, 0] = Instr.PrepareSqlParamString(keyValuePair.Key, _N_PARAM_NAME_CHAR);
            paramSql[i, 1] = Instr.PrepareSqlParamString(keyValuePair.Value, nParamValueChar);
            i++;
        }
        return paramSql;
    }

    protected string GetFullDoc(string doc, string year)
    {
        if (String.IsNullOrEmpty(doc))
        {
            return "";
        }
        if (String.IsNullOrEmpty(year))
        {
            return doc;
        }
        return String.Format("{0}-{1}", doc, year);
    }

    protected string GetEnumSql(EquipType equipType)
    {
        if (equipType == EquipType.CuttingTool)
        {
            return "РИ  ";
        }
        if (equipType == EquipType.MaterialGeneral)
        {
            return "МО  ";
        }
        return null;
    }

    protected string GetStringEnum(EquipType equipType)
    {
        if (equipType == EquipType.CuttingTool)
        {
            return "Режущий инструмент";
        }
        if (equipType == EquipType.MaterialGeneral)
        {
            return "Материалы";
        }
        return null;
    }

    protected void AddSqlElParam()
    {
        SqlToday = Instr.GetSqlToday();
        SqlLogin = Instr.PrepareSqlParamString(SqlOracle.Login, NUserNameChar);
    }

    protected int GetFreeId(string colName, string table)
    {
        List<int> ids = SqlOracle.Sel<int>("select " + colName + " from " + SqlOracle.PreLogin + table + " order by " + colName);
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
}

