using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.IO;

/// <summary>
/// Класс логирования.
/// </summary>
public class Logger
{
    StreamWriter _sW;

    private readonly string _name = @"Logger";
    private readonly string _extension = @".log";
    private const int Count = 5;

    private const long MaxSize = 1000000; //~ мегабайт

    public Logger(string name, string ext)
    {
        _name = name;
        if (ext[0] == '.')
        {
            _extension = ext;
        }
        else
        {
            _extension = '.' + ext;
        }
    }

    /// <summary>
    /// Записывает новую строку в лог.
    /// </summary>
    /// <param name="line">Строка.</param>
    public void WriteLine(object line)
    {
        SetFile();
        _sW.WriteLine(DateTime.Now + Environment.NewLine + line + Environment.NewLine);
        _sW.Flush();

        _sW.Close();
    }

    /// <summary>
    /// Записывает новую строку в лог.
    /// </summary>
    /// <param name="lines">Переменные для строки.</param>
    public void WriteLine(params object[] lines)
    {
        string line = "";
        foreach (object o in lines)
        {
            line += o + Environment.NewLine;
        }
        WriteLine(line);
    }
    /// <summary>
    /// Записывает новую строку в лог.
    /// </summary>
    /// <param name="lines">Переменные для строки.</param>
    public void WriteLine<T>(List<T> lines)
    {
        string line = "";
        foreach (T line1 in lines)
        {
            line += line1 + Environment.NewLine;
        }
        WriteLine(line);
    }
    /// <summary>
    /// Записывает новую строку с сообщением о предупреждении или ошибки пользователю.
    /// </summary>
    /// <param name="warning"></param>
    public void WriteWarning(object warning)
    {
        string message = "||||||||||||||||||||||||||||||||||||||||||||" +
            Environment.NewLine + warning;
        WriteLine(message);
    }
    /// <summary>
    /// Записывает новую строку с сообщением об ошибки.
    /// </summary>
    /// <param name="warning">Текст ошибки.</param>
    public void WriteError(object warning)
    {
        string message = "************************************" +
            Environment.NewLine + warning;
        WriteLine(message);
    }

    /// <summary>
    /// Записывает новую строку с сообщением об ошибки.
    /// </summary>
    /// <param name="errors">Текст ошибки.</param>
    public void WriteError(params object[] errors)
    {
        string message = "************************************";
        foreach (object error in errors)
        {
            message += Environment.NewLine + error;
        }
        WriteLine(message);
    }

    void SetFile()
    {
        string currFile = AppDomain.CurrentDomain.BaseDirectory + _name + _extension;
        try
        {
            FileInfo info = new FileInfo(currFile);

            bool append = true;
            if (info.Length > MaxSize)
            {
                CopyFiles();
                append = false;
            }

            _sW = new StreamWriter(currFile, append, Encoding.UTF8);
        }
        catch (FileNotFoundException)
        {
            _sW = new StreamWriter(currFile, false, Encoding.UTF8);
        }        
    }

    void CopyFiles()
    {
        for (int i = Count; i > 2; i--)
        {
            try
            {
                FileInfo f = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + _name +
                                (i - 1).ToString(CultureInfo.InvariantCulture) + _extension);
                f.CopyTo(AppDomain.CurrentDomain.BaseDirectory + _name + i + _extension, true);
            }
            catch (FileNotFoundException ) { }
            
        }

        FileInfo firstFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + _name + _extension);
        firstFile.CopyTo(AppDomain.CurrentDomain.BaseDirectory + _name + "2" + _extension, true);
    }
}

