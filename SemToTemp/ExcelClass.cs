using System;
using System.Drawing;
using System.IO;

using Excel =  Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

/// <summary>
/// ����� ��� ������ � Excel.
/// </summary>
public sealed class ExcelClass
{
    Excel.ApplicationClass _xlApp;
    Excel.Workbook _xlWorkBook;      
    Excel.Worksheet _xlWorkSheet;     
    Excel.Range _range;
    Excel.Pictures _p;
    Excel.Picture _pic;
    private readonly object _misValue = Type.Missing;


    private const string _TEMPLATE_PATH = @"D:\USP\UchetUSP\Templates\";
    //private string templatePath = UchetUSP.Program.PathString + "\\";


    /// <summary>
    /// ����������� ������.
    /// </summary>
    public ExcelClass()
    {
        _xlApp = new Excel.ApplicationClass();
    }


    //��������� EXCEL
    public bool Visible
    {
        set
        {
            if (false == value)
                _xlApp.Visible = false;

            else
                _xlApp.Visible = true;
        }
    }



    //������� ����� ��������
    public void NewDocument()
    {
        _xlWorkBook = _xlApp.Workbooks.Add(_misValue);
        _xlWorkSheet = (Excel.Worksheet)_xlWorkBook.Worksheets.get_Item(1);

        //_xlApp.Visible = true;
        //_xlApp.UserControl = true;
    }

    //������� ����� �������� C ��������
    public void NewDocument(string templateName)
    {
        _xlWorkBook = _xlApp.Workbooks.Add(_TEMPLATE_PATH + templateName);
        _xlWorkSheet = (Excel.Worksheet)_xlWorkBook.Worksheets.get_Item(1);
    }

    /// <summary>
    /// ��������� Excel-��������.
    /// </summary>
    /// <param name="fileName">���� � Excel �����.</param>
    /// <param name="isVisible">���������� �� �������� ��� ��������.</param>
    public void OpenDocument(string fileName, bool isVisible)
    {
        _xlWorkBook = _xlApp.Workbooks.Open(fileName, _misValue, _misValue, _misValue, _misValue,
            _misValue, _misValue, _misValue, _misValue, _misValue, _misValue,
            _misValue, _misValue, _misValue, _misValue);
        _xlApp.Visible = isVisible;
        _xlWorkSheet = (Excel.Worksheet)_xlWorkBook.Worksheets.get_Item(1);
    }

    /// <summary>
    /// ���������� ���������� ������ � ����� Excel.
    /// </summary>
    /// <returns></returns>
    public int GetNShieets()
    {
        return _xlWorkBook.Worksheets.Count;
    }


    //��������� ��������
    public void SaveDocument(string name)
    {
        _xlApp.DisplayAlerts = true;
        if (File.Exists(name))
        {
            DialogResult result = MessageBox.Show("����� ���� ��� ����������! �������� ���?", "Error",
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);

             if (result == DialogResult.Yes)
            {
                File.Delete(name);
                _xlWorkBook.SaveAs(name, Excel.XlFileFormat.xlWorkbookDefault, _misValue, _misValue, _misValue, _misValue, Excel.XlSaveAsAccessMode.xlExclusive, _misValue, _misValue, _misValue, _misValue, _misValue);
            }
        }else
        {
            _xlWorkBook.SaveAs(name, Excel.XlFileFormat.xlWorkbookDefault, _misValue, _misValue, _misValue, _misValue, Excel.XlSaveAsAccessMode.xlExclusive, _misValue, _misValue, _misValue, _misValue, _misValue);
        }      
        
    }

    //�������� ������
    public void SelectCells(Object start, Object end)
    {
        if (start == null)
        {
            start = _misValue;
        }

        if (end == null)
        {
            start = _misValue;
        }            
        _range = _xlWorkSheet.get_Range(start, end);
    }

    //����������� ���������� ������
    public void CopyTo(Object cell)
    {
        Excel.Range rangeDest = _xlWorkSheet.get_Range(cell, _misValue);
        _range.Copy(rangeDest);
    }

    //�������� ����
    public void SelectWorksheet(int count)
    {
        if (count <= _xlWorkBook.Worksheets.Count)
        {
            _xlWorkSheet = (Excel.Worksheet)_xlWorkBook.Worksheets.get_Item(count);
        }
        else {

            MessageBox.Show("��������� ������ � ����! ������� �������������� ����!");

        }
        
    }



    //��������� ����� ���� ������

    public void SetColor(int color)
    {            
        _range.Interior.Color = color;
        _range.Interior.PatternColorIndex = Excel.Constants.xlAutomatic;
    }

    public void SetColor(string colLetter, int rowNumber, object color)
    {
        _range = _xlWorkSheet.get_Range(colLetter + rowNumber, Type.Missing);
        _range.Interior.Color = color;
        _range.Interior.PatternColorIndex = Excel.Constants.xlAutomatic;
    }

    //���������� ��������
   
    public void SetOrientation(Excel.XlPageOrientation orientation)
    {
        _xlWorkSheet.PageSetup.Orientation = orientation;
    }

    //��������� �������� ����� �����
    public void SetMargin(double left, double right, double top, double bottom)
    {
        //Range.PageSetup.LeftMargin - �����
        //Range.PageSetup.RightMargin - ������ 
        //Range.PageSetup.TopMargin - �������
        //Range.PageSetup.BottomMargin - ������

        _xlWorkSheet.PageSetup.RightMargin = right;
        _xlWorkSheet.PageSetup.LeftMargin = left;
        _xlWorkSheet.PageSetup.TopMargin = top;
        _xlWorkSheet.PageSetup.BottomMargin = bottom;

    }


    //��������� ������� �����
    public void SetPaperSize(Excel.XlPaperSize size)
    {
        _xlWorkSheet.PageSetup.PaperSize = size;
    }

    //��������� �������� ������
    public void SetZoom(int percent)
    {
        _xlWorkSheet.PageSetup.Zoom = percent;
                
    }

    
    //������������� ����
    private void ReNamePage(int n, string name)
    {

        _xlWorkSheet = (Excel.Worksheet)_xlWorkBook.Worksheets.get_Item(n);

        _xlWorkSheet.Name = name;
    }

    //���������� ����� � ������ ������
    public void AddNewPageAtTheStart(string name)
    {          
       _xlWorkSheet = (Excel.Worksheet)_xlWorkBook.Worksheets.Add(_misValue, _misValue, _misValue, _misValue);

       ReNamePage(_xlWorkSheet.Index, name); 
       
    }

    //���������� ����� � ����� ������
    public void AddNewPageAtTheEnd(string name)
    {
        _xlWorkSheet = (Excel.Worksheet)_xlWorkBook.Worksheets.Add(_misValue, _xlWorkBook.Worksheets.get_Item(_xlWorkBook.Worksheets.Count), 1, _misValue);

        ReNamePage(_xlWorkSheet.Index, name);

    }

    //���������� ������ � ������
    public void SetFont(Font font,int colorIndex)
    {            
        _range.Font.Size = font.Size;
        _range.Font.Bold = font.Bold;
        _range.Font.Italic = font.Italic;
        _range.Font.Name = font.Name;
        _range.Font.ColorIndex = colorIndex;
    }


    //������ �������� � ������
    public void SetCellValue(string value)
    {
        _range.Value2 = value;
    }
    
    /// <summary>
    /// ���������� �������� � ������.
    /// </summary>
    /// <param name="cell">������ ������.</param>
    /// <param name="value">�������� ������.</param>
    public void SetCellValue(string cell, string value)
    {
        _range = _xlWorkSheet.get_Range(cell, _misValue);
        _range.Value2 = value;
    }

    /// <summary>
    /// ���������� �������� � ������.
    /// </summary>
    /// <param name="rowI">����� ������.</param>
    /// <param name="colI">����� �������.</param>
    /// <param name="value">�������� ������.</param>
    public void SetCellValue(int rowI, int colI, string value)
    {
        _range = _xlWorkSheet.get_Range(_xlWorkSheet.Cells[rowI, colI], _xlWorkSheet.Cells[rowI, colI]);
        _range.Value2 = value;
    }

    /// <summary>
    /// ���������� �������� � ������.
    /// </summary>
    /// <param name="column">����� ������� ������.</param>
    /// <param name="row">����� ������ ������.</param>
    /// <param name="value">�������� ������.</param>
    public void SetCellValue(string column, int row, string value)
    {
        SetCellValue(column + row, value);
    }

    /// <summary>
    /// ��������� � ��������� ������ � ������ �����
    /// </summary>
    /// <param name="cell">����� ������ � ������� "A1"</param>
    /// <param name="addValue">������</param>
    public void AddValueToCell(string cell, string addValue)
    {
        _range = _xlWorkSheet.get_Range(cell, Type.Missing);
        _range.Value2 += addValue;
    }

    //����������� �����
    public void SetMerge()
    {
        _range.Merge(_misValue);
    }

    //��������� ������ ��������
    public void SetColumnWidth(double width)
    {
        _range.ColumnWidth = width;           
    }

    //��������� ����������� ������
    public void SetTextOrientation(int orientation)
    {
        _range.Orientation = orientation;           
    }

    //������������ ������ � ������ �� ���������
    public void SetVerticalAlignment(int alignment)
    {
        _range.VerticalAlignment = alignment;
    }

    //������������ ������ � ������ �� �����������
    public void SetHorisontalAlignment(int alignment)
    {
        _range.HorizontalAlignment = alignment;   
    }



    //������� ���� � ������
    public void SetWrapText(bool value)
    {
        _range.WrapText = value;            
    }

    //��������� ������ ������
    public void SetRowHeight(double height)
    {
        _range.RowHeight = height;         
    }

    //��������� ���� ������
    public void SetBorderStyle(int color, Excel.XlLineStyle lineStyle, Excel.XlBorderWeight weight)
    {
        _range.Borders.ColorIndex = color;
        _range.Borders.LineStyle = lineStyle;
        _range.Borders.Weight = weight;
    }

    //������ �������� �� ������
    public string GetValue()
    {
        return _range.Value2.ToString();
    }

    /// <summary>
    /// ���������� ������ �� ������.
    /// </summary>
    /// <param name="cellAdress">����� ������ � ������� "A1".</param>
    /// <returns></returns>
    public object GetCellValue(string cellAdress)
    {
        return GetCellValue(_xlWorkSheet.get_Range(cellAdress, Type.Missing));
    }

    private object GetCellValue(Excel.Range range)
    {
        return range.Value2;
    }

    /// <summary>
    /// ���������� ������ �� ������ � ���� ������. ���� ������ ��� ������, ������������ ������ ������.
    /// </summary>
    /// <param name="iRow">����� ������.</param>
    /// <param name="iCol">����� �������.</param>
    /// <returns></returns>
    public string GetCellStringValue(int iCol, int iRow)
    {
        object o =
            GetCellValue((Excel.Range)_xlWorkSheet.Cells[iRow, iCol]);
        return o == null ? "" : o.ToString();
    }

    /// <summary>
    /// ���������� ������ �� ������ � ���� ������. ���� ������ ��� ������, ������������ ������ ������.
    /// </summary>
    /// <param name="cellAdress">����� ������ � ������� "A1".</param>
    /// <returns></returns>
    public string GetCellStringValue(string cellAdress)
    {
        object o = GetCellValue(cellAdress);
        return o == null ? "" : o.ToString();
    }

    /// <summary>
    /// ���������� ������ �� ������ � ���� ������. ���� ������ ��� ������, ������������ ������ ������.
    /// </summary>
    /// <param name="colLetter">����� ������� ������.</param>
    /// <param name="nRow">����� ������ ������.</param>
    /// <returns></returns>
    public string GetCellStringValue(string colLetter, int nRow)
    {
        return GetCellStringValue(colLetter + nRow);
    }

    public bool CellIsWhiteSpace(string cellAdress)
    {
        string cell = GetCellStringValue(cellAdress);
        if (cell == null)
        {
            return true;
        }
        if (cell.Trim() == "")
        {
            return true;
        }
        return false;
    }

    public bool CellIsWhiteSpace(string colLetter, int nRow)
    {
        return CellIsWhiteSpace(colLetter + nRow);
    }

    /// <summary>
    /// ���������� true, ���� ������ ������("") ��� ��� ������(NULL).
    /// </summary>
    /// <param name="cellAdress">����� ������ � ������� "A1".</param>
    /// <returns></returns>
    public bool CellIsNullOrVoid(string cellAdress)
    {
        return GetCellStringValue(cellAdress) == "";
    }

    /// <summary>
    /// ���������� true, ���� ������ ������("") ��� ��� ������(NULL).
    /// </summary>
    /// <param name="iRow">����� ������.</param>
    /// <param name="iCol">����� �������.</param>
    /// <returns></returns>
    public bool CellIsNullOrVoid(int iCol, int iRow)
    {
        return GetCellStringValue(iRow, iCol) == "";
    }

    /// <summary>
    /// ���������� true, ���� ������ ������("") ��� ��� ������(NULL).
    /// </summary>
    /// <param name="colLetter">����� ������� ������.</param>
    /// <param name="nRow">����� ������ ������.</param>
    /// <returns></returns>
    public bool CellIsNullOrVoid(string colLetter, int nRow)
    {
        return CellIsNullOrVoid(colLetter + nRow);
    }

    /// <summary>
    /// ���������� ����� ����� ���� ������.
    /// </summary>
    /// <param name="colLetter">����� ������� ������.</param>
    /// <param name="nRow">����� ������ ������.</param>
    /// <returns></returns>
    public int GetCellColorIndex(string colLetter, int nRow)
    {
        _range = _xlWorkSheet.get_Range(colLetter + nRow, Type.Missing);
        return (int)_range.Interior.ColorIndex;
    }

    //������� ��������
    public void CloseDocument()
    {            
        _xlWorkBook.Close(false, _misValue, _misValue);
        _xlApp.Quit();
        GC.GetTotalMemory(true);
    }

    /// <summary>
    /// ������� �������� � �����������.
    /// </summary>
    public void CloseDocumentSave()
    {
        _xlWorkBook.Close(true, _misValue, _misValue);
        _xlApp.Quit();
        GC.GetTotalMemory(true);
    }

    //��������� �������
    public void Exit()
    {
        _xlApp.Quit();
    }

    //����������� �������
    //��� ����� ������� �����
    public void Dispose()
    {
        releaseObject(_range);
        releaseObject(_xlWorkSheet);
        releaseObject(_xlWorkBook);
        releaseObject(_xlApp);
        GC.GetTotalMemory(true);
    }

    private void releaseObject(object obj)
    {
        if (obj == null)
            return;

        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Exception Occured while releasing object " + ex);
        }
        finally
        {
            GC.Collect();
        }
    }

    //������ �������� � ������
    public void WritePictureToCell(string path)
    {            
        _p = _xlWorkSheet.Pictures(_misValue) as Excel.Pictures;           
        _pic = _p.Insert(path, _misValue);
        _pic.Left = Convert.ToDouble(_range.Left);
        _pic.Top = Convert.ToDouble(_range.Top);            

    }

    /// <summary>
    /// ����� �������� ��������� �������� ����� �� ����� �����
    /// </summary>
    /// <param name="start">����� ������� ������ ���������</param>
    /// <param name="end">������ ������ ������ ���������</param>
    /// <param name="destination">������ ������ ��������������</param>
    public void CopyCells(object start, object end, object destination)
    {
        Excel.Range rangeDest = _xlWorkSheet.get_Range(destination, _misValue);
        _range = _xlWorkSheet.get_Range(start, end);
        _range.Copy(rangeDest);
    }

    /// <summary>
    /// ����� ������ ������ ���������� ������
    /// </summary>
    /// <param name="start">������ ���������</param>
    /// <param name="end">����� ���������</param>
    public void SetBold(object start, object end)
    {
        SelectCells(start, end);
        _range.Font.Bold = true;
    }

    /// <summary>
    /// ����� ����������� ������ �������� �� max ������
    /// </summary>
    /// <param name="columnName">������������ �������/�������� � ������� "�:�"</param>
    public void SetAutoFit(string columnName)
    {
        _range = _xlWorkSheet.get_Range(columnName, _misValue);
        _range.Columns.AutoFit();
    }

    public void AddRow(int rowNum)
    {
        _range = (Excel.Range)_xlWorkSheet.Rows[rowNum, _misValue];
        _range.Select();
        _range.Insert(Excel.XlInsertShiftDirection.xlShiftDown, _misValue);

    }

}
