using System;
using System.Collections.Generic;
using System.Data;
using Devart.Data.Oracle;

/// <summary>
/// Класс c инициаизацией переменных и проверкой соединения с БД
/// </summary>
static partial class SqlOracle
{
    /// <summary>
    /// Имя схемы (с точкой)
    /// </summary>
    public static string PreLogin;
    /// <summary>
    /// Логин пользователя в БД
    /// </summary>
    public static string Login;

    private static Logger _logger;
    private static OracleConnection _conn;
    private static string _connectionString;

    /// <summary>
    /// Тестовый запрос селект.
    /// </summary>
    /// <param name="tableName">Имя таблицы.</param>
    /// <returns></returns>
    public static bool TestQuery(string tableName)
    {
        object data = TestSelect("SELECT * FROM " + tableName);
        if (data != null)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Метод вызывает очистку таблице в выбранной схеме.
    /// </summary>
    /// <param name="table">Таблица</param>
    /// <param name="preLogin">Схема</param>
    public static void TruncateTable(string table, string preLogin)
    {
        try
        {
            _open();
            string tableWithSchema;
            if (!string.IsNullOrEmpty(preLogin))
            {
                tableWithSchema = string.Format("{0}{1}", preLogin, table);
            }
            else
            {
                tableWithSchema = table;
            }
            string cmdQuery = "truncate table " + tableWithSchema;
            _logger.WriteLine(cmdQuery);
            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            _logger.WriteLine("Удачно!");
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (OracleException ex)
        {
            string mess = "Ошибка в запросе к БД!" + Environment.NewLine + ex;
            Message.Show(mess);
            _logger.WriteError(mess);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            string mess = "Ошибка в запросе к БД!" + Environment.NewLine + ex;
            _logger.WriteError(mess);
            Message.Show(mess);
        }
        finally
        {
            _close();
        }
    }

    /// <summary>
    /// Метод вызывает очистку таблице в активной схеме.
    /// </summary>
    /// <param name="table">Таблица</param>
    public static void TruncateTable(string table)
    {
        TruncateTable(table, PreLogin);
    }

    /// <summary>
    /// Метод, реализующий параметризированный запрос в базу данных Oracle
    /// </summary>
    /// <param name="cmdQuery">Текст sql-зароса</param>
    /// <param name="paramsDict">Список параметров</param>
    /// <returns></returns>
    public static bool ExecuteVoidQuery(string cmdQuery, Dictionary<string, string> paramsDict)
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
    /// Метод реализующий параметризированный запрос в базу данных Oracle
    /// </summary>
    /// <param name="cmdQuery">Текст sql-запроса</param>
    public static void ExecuteVoidQuery(string cmdQuery)
    {
        _logger.WriteLine(cmdQuery);
        try
        {
            _open();

            OracleCommand cmd = new OracleCommand(cmdQuery, _conn);

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            _logger.WriteLine("Удачно!");
        }
        catch (TimeoutException)
        {
            throw;
        }
        catch (OracleException ex)
        {
            string mess = "Ошибка в запросе к БД!" + Environment.NewLine + ex;
            Message.Show(mess);
            _logger.WriteError(mess);
            throw new BadQueryExeption();
        }
        catch (Exception ex)
        {
            string mess = "Ошибка в запросе к БД!" + Environment.NewLine + ex;
            _logger.WriteError(mess);
            Message.Show(mess);
        }
        finally
        {
            _close();
        }
    }

    /// <summary>
    /// Закрывает соединение с БД.
    /// </summary>
    public static void _close()
    {
        if (_conn != null)
        {
            _logger.WriteLine("Статус соединения: " + _conn.State + " - закрытие соединения...");
            bool closed = false;
            do
            {
                switch (_conn.State)
                {
                    case ConnectionState.Closed:
                        {
                            closed = true;
                            break;
                        }
                    case ConnectionState.Broken:
                        {
                            closed = true;
                            _conn.Close();
                            break;
                        }
                    case ConnectionState.Connecting:
                        {
                            break;
                        }
                    case ConnectionState.Executing:
                        {
                            break;
                        }
                    case ConnectionState.Fetching:
                        {
                            break;
                        }
                    case ConnectionState.Open:
                        {
                            closed = true;
                            _conn.Close();
                            break;
                        }
                }
            } while (!closed);
            _logger.WriteLine("--- Соединение закрыто!");
        }
        
    }

    /// <summary>
    /// Метод составляющий строку соединения с БД.
    /// </summary>
    /// <param name="user">Логин пользователя.</param>
    /// <param name="password">Пароль пользователя.</param>
    /// <param name="dataSource">Источник данных.</param>
    public static void BuildConnectionString(string user, string password, string dataSource)
    {
        Login = user;
        _connectionString = "User id=" + user +
                            ";password=" + password +
                            ";Data Source = " + dataSource;
        BuildConnection();
    }
    
    /// <summary>
    /// Метод составляет строку соединения.
    /// </summary>
    /// <param name="user">Логин</param>
    /// <param name="password">Пароль</param>
    /// <param name="dataSource">Имя службы</param>
    /// <param name="host">Имя хоста</param>
    /// <param name="port">Порт</param>
    public static void BuildConnectionString(string user, string password, string dataSource, string host, string port)
    {
        Login = user;
        _connectionString = "User id=" + user +
                                             ";password=" + password +
                                               ";Service Name  = " + dataSource +
                                                    ";Host = " + host +
                                                        ";Direct = true" +
                                                            ";Port = " + port;
        BuildConnection();
    }

    /// <summary>
    /// Метод составляет строку соединения.
    /// </summary>
    /// <param name="user">Логин</param>
    /// <param name="password">Пароль</param>
    /// <param name="sid">Идентификатор службы</param>
    /// <param name="host">Имя хоста</param>
    /// <param name="port">Порт</param>
    public static void BuildConnectionStringSid(string user, string password, string sid, string host, string port)
    {
        Login = user;
        _connectionString = "User id=" + user +
                                             ";password=" + password +
                                               ";SID = " + sid +
                                                    ";Host = " + host +
                                                        ";Direct = true" +
                                                            ";Port = " + port;
        BuildConnection();
    }

    private static void BuildConnection()
    {
        _logger = new Logger("sql", ".ttt");
        _logger.WriteLine("----------------------------------------- NEW SESSION ----------------------------------------------");
        _logger.WriteLine("PreLogin - " + PreLogin);
        _logger.WriteLine("_connectionString - " + _connectionString);
    }

    private static void _open()
    {
        try
        {
            _conn = new OracleConnection(_connectionString);
            _logger.WriteLine("+++ Статус соединения: " + _conn.State + " - открытие соединения...");
            _conn.Open();
            _logger.WriteLine("Соединение открыто!");
        }
        catch (Exception ex)
        {
            const string sss = "Попытка соединения с БД прошла неудачно!";
            _logger.WriteLine(sss, ex);
            throw new TimeoutException();
        }
    }
}