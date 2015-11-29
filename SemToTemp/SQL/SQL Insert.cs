using System;
using System.Collections.Generic;
using Devart.Data.Oracle;


/// <summary>
/// Класс c методами типа Insert
/// </summary>
partial class SqlOracle
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cmdQuery"></param>
    /// <param name="paramsDict"></param>
    /// <returns></returns>
    static public bool Insert(string cmdQuery, Dictionary<string, string> paramsDict)
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

    static public void Insert(string cmdQuery)
    {
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (OracleException ex)
        {
            string mess = "Ошибка в запросе к БД!" + Environment.NewLine + ex;
            Message.Show(mess);
            Logger.WriteError(mess);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            string mess = "Ошибка в запросе к БД!" + Environment.NewLine + ex;
            Logger.WriteError(mess);
            Message.Show(mess);
        }
        finally
        {
            _close();
        }
    }

}