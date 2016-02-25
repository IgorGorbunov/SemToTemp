using System;
using System.Collections.Generic;
using Devart.Data.Oracle;

/// <summary>
/// Класс c методами типа Select
/// </summary>
partial class SqlOracle
{
    /// <summary>
    /// Метод, реализующий UPDATE-запрос
    /// </summary>
    /// <param name="cmdQuery">Текст UPDATE-запроса</param>
    public static void Update(string cmdQuery)
    {
        _open();
        _logger.WriteLine(cmdQuery);

        OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
        cmd.ExecuteNonQuery();
        cmd.Dispose();
        _close();
    }

    /// <summary>
    /// Метод, реализующий параметризированный UPDATE-запрос
    /// </summary>
    /// <param name="cmdQuery">Текст UPDATE-запроса</param>
    /// <param name="paramsDict">Список параметров</param>
    /// <returns></returns>
    public static bool Update(string cmdQuery, Dictionary<string, string> paramsDict)
    {
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);
            foreach (KeyValuePair<string, string> pair in paramsDict)
            {
                object o = pair.Value == "NULL" ? null : pair.Value;
                cmd.Parameters.AddWithValue(":" + pair.Key, o);
            }

            cmd.ExecuteNonQuery();
            cmd.Dispose();

            ProcessSuccess(cmdQuery, paramsDict);
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
}