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
    /// <summary>
    /// Dроизводит "быструю сортировку" массива чисел
    /// </summary>
    /// <param name="a">Входной массив</param>
    /// <param name="low">Нижняя грань сортировки (по умолчанию - 0)</param>
    /// <param name="high">Верхняя грань сортировки (по умолчанию - длина_массива-1)</param>
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
    /// Производит "быструю сортировку" массива из пар Первый параметр - Вещественное число
    /// </summary>
    /// <typeparam name="TKey">Первый параметр входного массива</typeparam>
    /// <param name="a">Входной массив</param>
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
            //Logger.WriteError(mess, exception.ToString(), "a.Length - " + a.Length, "low - " + low, "high - " + high);
            Message.ShowError(mess);
        }
    }

    /// <summary>
    /// Усечение массива (убираем пустые поля)
    /// </summary>
    /// <typeparam name="T">Тип данных массива</typeparam>
    /// <param name="mass">Входной массив</param>
    /// <returns></returns>
    public static T[] ReSize<T>(T[] mass)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < mass.Length; i++)
        {
            if (mass[i] != null && !string.IsNullOrEmpty(mass[i].ToString()))
            {
                list.Add(mass[i]);
            }
        }
        T[] newMass = new T[list.Count];
        int j = 0;
        foreach (T s in list)
        {
            newMass[j] = s;
            j++;
        }
        return newMass;
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

    /// <summary>
    /// Метод проверяющий существование элемента в списке
    /// </summary>
    /// <typeparam name="T">Тип данных в списке</typeparam>
    /// <param name="list">Входной список</param>
    /// <param name="obj">Проверяемый элемент</param>
    /// <returns></returns>
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
    /// <param name="array">Входной массив</param>
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
    /// Метод подготавливает строку для SQL-запроса с кавычками
    /// </summary>
    /// <param name="data">Входная строка</param>
    /// <param name="nDataTypeBytes">Количество символов в поле</param>
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
    /// Метод подготавливает строку для SQL-запроса без кавычек
    /// </summary>
    /// <param name="data">Входная строка</param>
    /// <param name="nDataTypeBytes">Количество символов в поле</param>
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

    /// <summary>
    /// Добавление пробела в строку
    /// </summary>
    /// <param name="data">Входная строка</param>
    /// <returns></returns>
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
    /// Получение даты для SQL строки
    /// </summary>
    /// <returns></returns>
    public static string GetSqlToday()
    {
        return DateTime.Today.ToString("dd.MM.yy");
    }

    /// <summary>
    /// Возвращает строку без лишних пробелов.
    /// </summary>
    /// <param name="line">Начальная строка.</param>
    /// <returns></returns>
    private static string DeleteDoubleSpaces(string line)
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

    private static void AddSpaces(ref string data, int nDataTypeBytes)
    {
        data = AddSpaces(data, nDataTypeBytes);
    }

    private static string AddSpaces(string data, int nDataTypeBytes)
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

    private static void AddQuotes(ref string data)
    {
        data = String.Format("\'{0}\'", data);
    }
}

