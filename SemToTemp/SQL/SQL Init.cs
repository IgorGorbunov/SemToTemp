using System;
using System.Data;
using Devart.Data.Oracle;

/// <summary>
/// Класс c инициаизацией переменных и проверкой соединения с БД
/// </summary>
static partial class SqlOracle
{
    /// <summary>
    /// Объявление компонентов
    /// </summary>
    static OracleConnection _conn;

    private static string _connectionString;


    /// <summary>
    /// Cоединене с БД (второе исполнение)
    /// </summary>
    public static void _open()
    {
        try
        {
            _conn = new OracleConnection(_connectionString);
            Logger.WriteLine("Статус соединения: " + _conn.State + " - открытие соединения...");
            _conn.Open();
            Logger.WriteLine("Соединение открыто!");
        }
        catch (Exception ex)
        {
            const string sss = "Попытка соединения с БД прошла неудачно!";
            Logger.WriteLine(sss, ex);
            throw new TimeoutException();
        }
    }
    public static void _close()
    {
        if (_conn != null)
        {
            Logger.WriteLine("Статус соединения: " + _conn.State + " - закрытие соединения...");
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
        }
        Logger.WriteLine("Соединение закрыто!");
    }



    /// <summary>
    /// Метод построения строки соеднинения
    /// </summary>   
    /// <returns></returns>
    public static void BuildConnectionString(string user, string password, string dataSource)
    {
        _connectionString = "User id=" + user +
                            ";password=" + password +
                            ";Data Source = " + dataSource;
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
        _connectionString = "User id=" + user +
                                             ";password=" + password +
                                               ";Service Name  = " + dataSource +
                                                    ";Host = " + host +
                                                        ";Direct = true" +
                                                            ";Port = " + port;

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
        _connectionString = "User id=" + user +
                                             ";password=" + password +
                                               ";SID  = " + sid +
                                                    ";Host = " + host +
                                                        ";Direct = true" +
                                                            ";Port = " + port;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tableName"></param>
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
}