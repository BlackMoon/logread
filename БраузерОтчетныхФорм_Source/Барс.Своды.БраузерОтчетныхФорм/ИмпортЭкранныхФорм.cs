namespace Барс.Своды.БраузерОтчетныхФорм
{
    using Syncfusion.GridExcelConverter;
    using Syncfusion.Windows.Forms.Grid;
    using Syncfusion.XlsIO;
    using System;
    using System.Collections.Generic;

    public class ИмпортЭкранныхФорм : GridExcelConverterControl
    {
        private int sheetFamilyID;

        private void ИдентифицироватьЗакладу(string Строка, ТаблицаОтчетнойФормы Таблица)
        {
            string[] strArray = Строка.Split(new char[] { ' ' });
            if (!(strArray[0] != "#Закладка") || !(strArray[0] != "#ДинамическаяТаблица"))
            {
                foreach (string str in strArray)
                {
                    if (str.ToLower().StartsWith("код"))
                    {
                        string str2 = str.Split(new char[] { '=' })[1];
                        Таблица.КодТаблицы = str2;
                    }
                    else if (str.ToLower().StartsWith("наименование"))
                    {
                        string str3 = str.Split(new char[] { '=' })[1];
                        Таблица.Наименование = str3;
                    }
                    else if (str.ToLower().StartsWith("фиксстолбцов"))
                    {
                        string s = str.Split(new char[] { '=' })[1];
                        Таблица.Cols.FrozenCount = int.Parse(s);
                    }
                    else if (str.ToLower().StartsWith("фиксстрок"))
                    {
                        string str5 = str.Split(new char[] { '=' })[1];
                        Таблица.Rows.FrozenCount = int.Parse(str5);
                    }
                }
            }
        }

        public ЭкраннаяФорма ИмпортироватьЭкраннуюФорму(string Файл)
        {
            ЭкраннаяФорма форма = new ЭкраннаяФорма();
            this.sheetFamilyID = GridFormulaEngine.CreateSheetFamilyID();
            ExcelEngine engine = new ExcelEngine();
            IApplication excel = engine.Excel;
            IWorkbook workbook = engine.Excel.Workbooks.Open(Файл);
            foreach (IWorksheet worksheet in workbook.Worksheets)
            {
                if (worksheet.Name.Trim().ToLower() == "code")
                {
                    continue;
                }
                if (worksheet.Name.Trim().ToLower() == "шапка")
                {
                    ШапкаЭкраннойФормы формы = new ШапкаЭкраннойФормы();
                    GridFormulaEngine.RegisterGridAsSheet("sys_шапка", формы.Представление, this.sheetFamilyID);
                    base.ExcelToGrid(worksheet, формы.Представление);
                    формы.УстановитьСтилевыеНастройкиТаблицы();
                    this.ОбработатьЯчейкиТаблицы(формы);
                    форма.Шапка = формы;
                }
                else
                {
                    this.ПолучитьТаблицыФормы(worksheet, форма);
                }
            }
            return форма;
        }

        private void ИсправитьГраницыЯчейки(int Row, int Col, ТаблицаОтчетнойФормы Таблица)
        {
            int rowCount = Таблица.RowCount;
            int colCount = Таблица.ColCount;
            GridStyleInfo info = Таблица[Row, Col];
            GridRangeInfo range = null;
            Таблица.CoveredRanges.Find(Row, Col, out range);
            bool flag = false;
            if (((range != null) && ((range.Width != 1) || (range.Height != 1))) && (range.Right == colCount))
            {
                flag = true;
            }
            if ((info.Borders.Left.Style == GridBorderStyle.Solid) && ((Col != 1) && (Таблица[Row, Col - 1].Borders.Right.Style == GridBorderStyle.Solid)))
            {
                info.Borders.Left = new GridBorder(GridBorderStyle.Standard);
            }
            if ((!flag && (info.Borders.Right.Style == GridBorderStyle.Solid)) && ((Col != colCount) && (Таблица[Row, Col + 1].Borders.Left.Style == GridBorderStyle.Solid)))
            {
                info.Borders.Right = new GridBorder(GridBorderStyle.Standard);
            }
            if ((info.Borders.Top.Style == GridBorderStyle.Solid) && ((Row != 1) && (Таблица[Row - 1, Col].Borders.Bottom.Style == GridBorderStyle.Solid)))
            {
                info.Borders.Top = new GridBorder(GridBorderStyle.Standard);
            }
            if ((info.Borders.Bottom.Style == GridBorderStyle.Solid) && ((Row != rowCount) && (Таблица[Row + 1, Col].Borders.Top.Style == GridBorderStyle.Solid)))
            {
                info.Borders.Bottom = new GridBorder(GridBorderStyle.Standard);
            }
        }

        private void ОбработатьЯчейкиТаблицы(ТаблицаОтчетнойФормы Таблица)
        {
            for (int i = 1; i <= Таблица.RowCount; i++)
            {
                for (int j = 1; j <= Таблица.ColCount; j++)
                {
                    this.ИсправитьГраницыЯчейки(i, j, Таблица);
                    GridStyleInfo info = Таблица[i, j];
                    if ((info.CellValue != null) && info.CellValue.ToString().StartsWith("#="))
                    {
                        info.CellType = "FormulaCell";
                        info.CellValue = info.CellValue.ToString().Substring(1);
                    }
                }
            }
        }

        private List<ТаблицаОтчетнойФормы> ПолучитьТаблицыФормы(IWorksheet worksheet, ЭкраннаяФорма Форма)
        {
            ТаблицаОтчетнойФормы формы = new ТаблицаОтчетнойФормы();
            GridFormulaEngine.RegisterGridAsSheet("sys_общаяТаблица", формы.Представление, this.sheetFamilyID);
            base.ExcelToGrid(worksheet, формы.Представление);
            bool flag = false;
            int top = -1;
            string str = string.Empty;
            int length = worksheet.Rows.Length;
            int right = worksheet.Columns.Length;
            bool flag2 = true;
            List<ТаблицаОтчетнойФормы> list = new List<ТаблицаОтчетнойФормы>();
            for (int i = 1; i <= length; i++)
            {
                string text = worksheet[i, 1].Text;
                if (string.IsNullOrEmpty(text))
                {
                    if ((i == length) && (top != -1))
                    {
                        text = "#Конец_Закладки";
                    }
                    else
                    {
                        continue;
                    }
                }
                if (text.ToLower().StartsWith("#закладка"))
                {
                    flag = false;
                    top = i;
                    str = text;
                }
                else if (text.ToLower().StartsWith("#динамическаятаблица"))
                {
                    flag = true;
                    top = i;
                    str = text;
                }
                else if (text.ToLower().StartsWith("#конец_закладки") && (top != -1))
                {
                    GridRangeInfo range = GridRangeInfo.Cells(top, 1, i, right);
                    GridStyleInfoStoreTable cells = формы.GetCells(range);
                    int rowCount = cells.RowCount;
                    int colCount = cells.ColCount;
                    ТаблицаОтчетнойФормы формы2 = new ТаблицаОтчетнойФормы();
                    формы2.RowCount = rowCount;
                    формы2.ColCount = colCount;
                    this.ИдентифицироватьЗакладу(str, формы2);
                    string str3 = str.Trim().ToLower();
                    if (flag2)
                    {
                        формы2.ИмяЛиста = worksheet.Name;
                    }
                    else
                    {
                        формы2.ИмяЛиста = формы2.КодТаблицы;
                    }
                    flag2 = false;
                    GridFormulaEngine.RegisterGridAsSheet(формы2.ИмяЛиста, формы2.Представление, this.sheetFamilyID);
                    GridRangeInfo info2 = GridRangeInfo.Cells(1, 1, rowCount, colCount);
                    формы2.SetCells(info2, cells);
                    for (int j = 0; j <= colCount; j++)
                    {
                        формы2.ColWidths[j] = формы.ColWidths[j];
                    }
                    foreach (GridRangeInfo info3 in формы.CoveredRanges.Ranges)
                    {
                        формы2.CoveredRanges.Add(GridRangeInfo.Cells((info3.Top - top) + 1, info3.Left, (info3.Bottom - top) + 1, info3.Right));
                    }
                    формы2.RowHeights.ResizeToFit(GridRangeInfo.Rows(0, rowCount));
                    формы2.RowHeights[0] = 0;
                    формы2.RowHeights[1] = 0;
                    формы2.RowHeights[rowCount] = 0;
                    if (!flag)
                    {
                        this.ПроанализироватьТаблицуНаПеременные(формы2);
                        this.ОбработатьЯчейкиТаблицы(формы2);
                        Форма.ДобавитьТаблицу(формы2);
                    }
                    else
                    {
                        string[] strArray = формы2.КодТаблицы.Split(new char[] { ',' });
                        bool flag3 = str3.Contains("размещатьназакладке");
                        foreach (string str4 in strArray)
                        {
                            ДинамическаяТаблица таблица = new ДинамическаяТаблица();
                            таблица.ПостроитьТаблицуПоМодели(формы2);
                            таблица.КодТаблицы = str4;
                            таблица.РазмещатьНаЗакладке = flag3;
                            Форма.ДобавитьДинамическуюТаблицу(таблица);
                        }
                    }
                    top = -1;
                }
            }
            GridFormulaEngine.UnregisterGridAsSheet("sys_общаяТаблица", формы.Представление);
            return list;
        }

        private void ПроанализироватьТаблицуНаПеременные(ТаблицаОтчетнойФормы Таблица)
        {
            int num3;
            int num = -1;
            int num2 = -1;
            for (num3 = 1; num3 <= Таблица.ColCount; num3++)
            {
                if ((Таблица[2, num3].CellValue != null) && (Таблица[2, num3].CellValue.ToString().Trim() == "#КодыСтрок"))
                {
                    Таблица.ColWidths[num3] = 0;
                    num = num3;
                    break;
                }
            }
            if (num != -1)
            {
                for (num3 = 1; num3 <= Таблица.RowCount; num3++)
                {
                    if ((Таблица[num3, 1].CellValue != null) && (Таблица[num3, 1].CellValue.ToString().Trim() == "#КодыСтолбцов"))
                    {
                        Таблица.RowHeights[num3] = 0;
                        num2 = num3;
                        break;
                    }
                }
                if (num2 != -1)
                {
                    for (num3 = num2 + 1; num3 <= Таблица.RowCount; num3++)
                    {
                        if ((Таблица[num3, num].CellValue != null) && !string.IsNullOrEmpty(Таблица[num3, num].CellValue.ToString()))
                        {
                            for (int i = num + 1; i <= Таблица.ColCount; i++)
                            {
                                if ((Таблица[num2, i].CellValue != null) && !string.IsNullOrEmpty(Таблица[num2, i].CellValue.ToString()))
                                {
                                    Таблица[num3, i].Tag = string.Format("${0}:{1}$", Таблица[num2, i].CellValue.ToString(), Таблица[num3, num].CellValue.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

