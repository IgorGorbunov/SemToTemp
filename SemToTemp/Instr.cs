using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;

/// <summary>
/// Статический класс со стандартным инструментарием.
/// </summary>
public static class Instr
{
    public static void QSort(int[] a, int low, int high)
    {
        int i = low;
        int j = high;
        int x = a[(low + high) / 2];  // x - опорный элемент посредине между low и high
        do
        {
            while (a[i] < x) ++i;  // поиск элемента для переноса в старшую часть
            while (a[j] > x) --j;  // поиск элемента для переноса в младшую часть
            if (i <= j)
            {
                // обмен элементов местами:
                int temp = a[i];
                a[i] = a[j];
                a[j] = temp;
                // переход к следующим элементам:
                i++; j--;
            }
        } while (i < j);
        if (low < j) QSort(a, low, j);
        if (i < high) QSort(a, i, high);
    }

    /// <summary>
    /// Производит "быструю сортировку" массива из пар Грань - Вещественное число (расстояние до грани).
    /// </summary>
    /// <param name="a">Одномерный массив пар Грань - Вещественное число</param>
    /// <param name="low">Нижняя грань сортировки (по умолчанию - 0).</param>
    /// <param name="high">Верхняя грань сортировки (по умолчанию - длина_массива-1).</param>
    public static void QSortPairs<TKey>(KeyValuePair<TKey, double>[] a, int low, int high)
    {
        try
        {
            int i = low;
            int j = high;
            double x = a[(low + high) / 2].Value;  // x - опорный элемент посредине между low и high
            do
            {
                while (a[i].Value < x) ++i;  // поиск элемента для переноса в старшую часть
                while (a[j].Value > x) --j;  // поиск элемента для переноса в младшую часть
                if (i <= j)
                {
                    // обмен элементов местами:
                    KeyValuePair<TKey, double> temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                    // переход к следующим элементам:
                    i++; j--;
                }
            } while (i < j);
            if (low < j) QSortPairs(a, low, j);
            if (i < high) QSortPairs(a, i, high);
        }
        catch (Exception exception)
        {
            const string mess = "Проблемы с сортировкой!";
            Logger.WriteError(mess, exception.ToString(), "a.Length - " + a.Length, "low - " + low, "high - " + high);
            Message.ShowError(mess);
        }
    }


    /// <summary>
    /// Добавляет объект в уникальный список.
    /// </summary>
    /// <param name="list">Список.</param>
    /// <param name="obj">Объект.</param>
    public static void AddUnicToList<T>(List<T> list, T obj)
    {
        bool alreadyHave = false;
        foreach (T t in list)
        {
            if (!t.Equals(obj)) continue;

            alreadyHave = true;
            break;
        }
        if (alreadyHave) return;

        list.Add(obj);
    }


    public static bool Exist<T>(List<T> list, T obj)
    {
        foreach (T var in list)
        {
            if (var.Equals(obj))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Возвращает экземпляр класса Image из массива байт.
    /// </summary>
    /// <param name="bytes">Массив байт.</param>
    /// <returns></returns>
    public static Image GetImage(byte[] bytes)
    {
        return Image.FromStream(new MemoryStream(bytes));
    }
    /// <summary>
    /// Возвращает максимальное значение для заданного массива.
    /// </summary>
    /// <param name="array"></param>
    public static double Max(IEnumerable<double> array)
    {
        double maxEl = double.MinValue;
        if (array == null)
            return maxEl;

        foreach (double var in array)
        {
            if (var > maxEl)
            {
                maxEl = var;
            }
        }
        return maxEl;
    }

    /// <summary>
    /// Возвращает md5 хэш-сумму для файла.
    /// </summary>
    /// <param name="path">Путь к файлу.</param>
    /// <returns></returns>
    public static string ComputeMd5Checksum(string path)
    {
        using (FileStream fs = File.OpenRead(path))
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fileData = new byte[fs.Length];
            fs.Read(fileData, 0, (int) fs.Length);
            byte[] checkSum = md5.ComputeHash(fileData);
            string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
            return result;
        }
    }

    /// <summary>
    /// Возвращает строку без лишних пробелов.
    /// </summary>
    /// <param name="line">Начальная строка.</param>
    /// <returns></returns>
    public static string DeleteDoubleSpaces(string line)
    {
        string[] split = line.Split(' ');
        string newLine = "";
        bool first = true;
        foreach (string s in split)
        {
            if (string.IsNullOrEmpty(s))
                continue;

            if (!first)
            {
                newLine += ' ';
            }
            newLine += s;
            first = false;
        }
        return newLine;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="nDataTypeBytes"></param>
    public static void AddSpaces(ref string data, int nDataTypeBytes)
    {
        data = AddSpaces(data, nDataTypeBytes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="nDataTypeBytes"></param>
    /// <returns></returns>
    public static string AddSpaces(string data, int nDataTypeBytes)
    {
        if (data.Length >= nDataTypeBytes)
        {
            return data.Substring(0, nDataTypeBytes);
        }
        string spaces = "";
        for (int i = 0; i < nDataTypeBytes - data.Length; i++)
        {
            spaces += " ";
        }
        return data + spaces;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static void AddQuotes(ref string data)
    {
        data = String.Format("\'{0}\'", data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="nDataTypeBytes"></param>
    /// <returns></returns>
    public static string PrepareSqlString(string data, int nDataTypeBytes)
    {
        string newData = PrepareSqlParamString(data, nDataTypeBytes);
        if (newData == "NULL")
        {
            return newData;
        }
        AddQuotes(ref newData);
        return newData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="nDataTypeBytes"></param>
    /// <returns></returns>
    public static string PrepareSqlParamString(string data, int nDataTypeBytes)
    {
        if (string.IsNullOrEmpty(data))
        {
            return "NULL";
        }
        string newData = DeleteDoubleSpaces(data);
        if (string.IsNullOrEmpty(newData))
        {
            return "NULL";
        }
        AddSpaces(ref newData, nDataTypeBytes);
        return newData;
    }

    public static string AddFirstSpace(string data)
    {
        if (data == "NULL")
        {
            return data;
        }
        if (data[0] == '-')
        {
            return data;
        }
        data = data.Substring(0, data.Length - 1);
        return string.Format(" {0}", data);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static string GetSqlToday()
    {
        return DateTime.Today.ToString("dd.MM.yy");
    }
}

