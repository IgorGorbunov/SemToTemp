using System;
using System.Collections.Generic;
using Devart.Data.Oracle;


/// <summary>
/// ����� c �������� ���� Insert
/// </summary>
partial class SqlOracle
{
    /// <summary>
    /// �����, ����������� ������������������� insert-������ � ���� ������ Oracle
    /// </summary>
    /// <param name="cmdQuery">����� sql-������</param>
    /// <param name="paramsDict">������ ����������</param>
    /// <returns></returns>
    public static bool Insert(string cmdQuery, Dictionary<string, string> paramsDict)
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

    /// <summary>
    /// �����, ����������� insert-������ � ���� ������ Oracle
    /// </summary>
    /// <param name="cmdQuery">����� sql-�������</param>
    public static void Insert(string cmdQuery)
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
            string mess = "������ � ������� � ��!" + Environment.NewLine + ex;
            Message.Show(mess);
            _logger.WriteError(mess);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            string mess = "������ � ������� � ��!" + Environment.NewLine + ex;
            _logger.WriteError(mess);
            Message.Show(mess);
        }
        finally
        {
            _close();
        }
    }

}