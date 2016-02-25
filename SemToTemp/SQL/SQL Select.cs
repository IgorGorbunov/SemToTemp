using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Devart.Data.Oracle;

/// <summary>
/// Класс c методами типа Select
/// </summary>
partial class SqlOracle
{
    /// <summary>
    /// Метод, реализующий параметризированный select-запрос
    /// </summary>
    /// <param name="cmdQuery">SQL-текст запроса.</param>
    /// <param name="paramsDict">Dictionary c параметризаторами запроса.</param>
    /// <param name="value">Результирующее значение.</param>
    /// <returns></returns>
    public static bool Sel<T>(string cmdQuery, Dictionary<string, string> paramsDict, out T value)
    {
        value = default(T);
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                cmd.Parameters.AddWithValue(":" + pair.Key, pair.Value);
            }
            bool flag = false;
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (!reader.IsDBNull(0))
                {
                    value = (T)reader.GetValue(0);
                }
                flag = true;
            }

            reader.Close();
            cmd.Dispose();

            ProcessSuccess(cmdQuery, paramsDict, value);
            return flag;
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (OracleException ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            return false;
        }
        finally
        {
            _close();
        }
    }

    public static bool Sel<T>(string cmdQuery, Dictionary<string, string> paramsDict, out List<T> values)
    {
        values = new List<T>();
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                cmd.Parameters.AddWithValue(":" + pair.Key, pair.Value);
            }
            
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (reader.IsDBNull(i))
                    {
                        values.Add(default(T));
                    }
                    else
                    {
                        values.Add((T)reader.GetValue(i));
                    }
                }
            }

            reader.Close();
            cmd.Dispose();

            ProcessSuccess(cmdQuery, paramsDict, values);
            return true;
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (OracleException ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            return false;
        }
        finally
        {
            _close();
        }
    }

    public static List<T> Sel<T>(string cmdQuery)
    {
        List<T> values = new List<T>();
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);

            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader.IsDBNull(0))
                {
                    values.Add(default(T));
                }
                else
                {
                    values.Add((T)reader.GetValue(0));
                }
            }

            reader.Close();
            cmd.Dispose();

            ProcessSuccess(cmdQuery, values);
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (OracleException ex)
        {
            string mess = "Ошибка в запросе к БД!" + Environment.NewLine + cmdQuery + Environment.NewLine + ex;
            _logger.WriteError(mess);
            Message.Show(mess);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            string mess = "Ошибка в запросе к БД!" + Environment.NewLine + cmdQuery + Environment.NewLine + ex;
            _logger.WriteError(mess);
            Message.Show(mess);
        }
        finally
        {
            _close();
        }
        return values;
    }

    public static bool Sel<T1, T2>(string cmdQuery, Dictionary<string, string> paramsDict, out Dictionary<T1, T2> values)
    {
        values = new Dictionary<T1, T2>();
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                cmd.Parameters.AddWithValue(":" + pair.Key, pair.Value);
            }
            
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                T1 t1 = default(T1);
                T2 t2 = default(T2);
                object val = reader.GetValue(0);
                if (!reader.IsDBNull(0))
                {
                    t1 = (T1)val;
                }
                val = reader.GetValue(1);
                if (!reader.IsDBNull(1))
                {
                    t2 = (T2)val;
                }
                values.Add(t1, t2);
            }

            reader.Close();
            cmd.Dispose();

            ProcessSuccess(cmdQuery, paramsDict, values);
            return true;
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (OracleException ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            return false;
        }
        finally
        {
            _close();
        }
    }

    /// <summary>
    /// Метод, реализующий параметризированный select-запрос
    /// </summary>
    /// <param name="cmdQuery">SQL-текст запроса</param>
    /// <param name="paramsDict">Dictionary c параметризаторами запроса</param>
    /// <param name="value">Результирующая таблица данных.</param>
    /// <returns></returns>
    public static bool SelData(string cmdQuery, Dictionary<string, string> paramsDict, out DataTable value)
    {
        value = null;
        DataSet ds = new DataSet();
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                da.SelectCommand.Parameters.AddWithValue(":" + pair.Key, pair.Value);
            }

            da.Fill(ds);

            da.Dispose();
            cmd.Dispose();

            value = ds.Tables[0].Rows.Count == 0 ? null : ds.Tables[0];

            ProcessSuccessData(cmdQuery, paramsDict, value);
            return true;
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (OracleException ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            return false;
        }
        finally
        {
            _close();
        }
    }

    /// <summary>
    /// Метод, реализующий параметризированный select-запрос картинки.
    /// </summary>
    /// <param name="cmdQuery">SQL-текст запроса.</param>
    /// <param name="paramsDict">Dictionary c параметризаторами запроса.</param>
    /// <param name="value">Результирующие изображение.</param>
    /// <returns></returns>
    public static bool SelImage(string cmdQuery, Dictionary<string, string> paramsDict,
                                out Image value)
    {
        value = null;
        byte[] bytes = null;
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                cmd.Parameters.AddWithValue(":" + pair.Key, pair.Value);
            }

            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                bytes = (byte[]) reader.GetValue(0);
            }

            reader.Close();
            cmd.Dispose();

            if (bytes != null)
            {
                value = Instr.GetImage(bytes);
            }

            ProcessSuccess(cmdQuery, paramsDict, value);
            return true;
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (OracleException ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            ProcessUnSuccess(cmdQuery, paramsDict, ex);
            return false;
        }
        finally
        {
            _close();
        }
    }

    public static object TestSelect(string cmdQuery)
    {
        _open();
        _logger.WriteLine(cmdQuery);
        object val = 0;

        OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
        OracleDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            val = reader.GetValue(0);
            break;
        }

        reader.Close();
        cmd.Dispose();
        _close();

        return val;
    }

    static void ProcessSuccess(string cmdQuery, Dictionary<string, string> paramsDict)
    {
        string mess = "Запрос прошёл!";
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        _logger.WriteLine(mess);
    }
    static void ProcessSuccess<T>(string cmdQuery, Dictionary<string, string> paramsDict, T value)
    {
        string mess = "Запрос прошёл!";
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        mess += Environment.NewLine + "-";
        mess += Environment.NewLine + "Data:";
        mess += Environment.NewLine + value;
        _logger.WriteLine(mess);
    }
    static void ProcessSuccess<T>(string cmdQuery, Dictionary<string, string> paramsDict, List<T> values)
    {
        string mess = "Запрос прошёл!";
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        mess += Environment.NewLine + "-";
        mess += Environment.NewLine + "Data:";
        foreach (T value in values)
        {
            mess += Environment.NewLine + value;
        }
        _logger.WriteLine(mess);
    }
    static void ProcessSuccess<T>(string cmdQuery, IEnumerable<T> values)
    {
        string mess = "Запрос прошёл!";
        mess += Environment.NewLine + cmdQuery;
        mess += Environment.NewLine + "Data (первые 15 записей):";
        int i = 0;
        foreach (T value in values)
        {
            mess += Environment.NewLine + value;
            i++;
            if (i == 15)
            {
                break;
            }
        }
        _logger.WriteLine(mess);
    }
    static void ProcessSuccess<T1, T2>(string cmdQuery, Dictionary<string, string> paramsDict, Dictionary<T1, T2> values)
    {
        string mess = "Запрос прошёл!";
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        mess += Environment.NewLine + "-";
        mess += Environment.NewLine + "Data:";
        foreach (KeyValuePair<T1, T2> keyValuePair in values)
        {
            mess += Environment.NewLine + keyValuePair.ToString();
        }
        _logger.WriteLine(mess);
    }

    static void ProcessSuccessData(string cmdQuery, Dictionary<string, string> paramsDict, DataTable value)
    {
        string mess = "Запрос прошёл!";
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        mess += Environment.NewLine + "-";
        mess += Environment.NewLine + "Data:" + Environment.NewLine;
        if (value != null && value.Rows.Count > 0)
        {
            foreach (DataRow row in value.Rows)
            {
                for (int i = 0; i < value.Columns.Count; i++)
                {
                    mess += row[i] + " | ";
                }
                mess += Environment.NewLine;
            }
        }
        _logger.WriteLine(mess);
    }

    static void ProcessUnSuccess(string cmdQuery, Dictionary<string, string> paramsDict, Exception ex)
    {
        string mess = "Запрос НЕ прошёл!";
        Message.Show(mess);
        mess = RecordQuery(mess, cmdQuery, paramsDict);
        mess += Environment.NewLine + ex;
        _logger.WriteError(mess);
    }

    static string RecordQuery(string mess, string cmdQuery, Dictionary<string, string> paramsDict)
    {
        mess += Environment.NewLine + cmdQuery;
        mess += Environment.NewLine + "-";
        mess += Environment.NewLine + "Parametrs:";
        foreach (KeyValuePair<string, string> pair in paramsDict)
        {
            mess += Environment.NewLine + pair.Key + " - " + pair.Value;
        }
        return mess;
    }
}