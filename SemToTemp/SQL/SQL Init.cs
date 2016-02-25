using System;
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
            _logger.WriteLine("Соединение закрыто!");
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
                                               ";SID  = " + sid +
                                                    ";Host = " + host +
                                                        ";Direct = true" +
                                                            ";Port = " + port;
        BuildConnection();
    }

    private static void BuildConnection()
    {
        _logger = new Logger("sql", ".ttt");
        _logger.WriteLine("----------------------------------------- NEW SESSION ----------------------------------------------");
    }

    private static void _open()
    {
        try
        {
            _conn = new OracleConnection(_connectionString);
            _logger.WriteLine("Статус соединения: " + _conn.State + " - открытие соединения...");
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