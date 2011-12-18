namespace Барс.Своды.БраузерОтчетныхФорм
{
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.XPath;
    using Барс.Своды.ОтчетнаяФорма;

    public class ТаблицаExcel : GridControl
    {
        private string имяЛиста;
        private string кодТаблицы;
        private static Regex регулярник = new Regex(@"^=IF\((?<условие>\s*[^=]+\s*=\s*0\s*|ISERROR.*),(?<true>[^,]*),(?<false>.*)\)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        protected ФункцииАвтоблоков функцииАвтоблоков;

        public ТаблицаExcel()
        {
            this.ПроинициализироватьФункцииАвтоблоков(null);
        }

        protected override void Dispose(bool disposing)
        {
            if (this.функцииАвтоблоков != null)
            {
                this.функцииАвтоблоков.Dispose();
                this.функцииАвтоблоков = null;
            }
            base.Dispose(disposing);
        }

        public virtual void ЗагрузитьИзXML(XPathNavigator Документ)
        {
            МодельТаблицыОтчетнойФормы формы = new МодельТаблицыОтчетнойФормы();
            формы.ЗагрузитьФорму(Документ);
            this.ПостроитьТаблицуПоМодели(формы);
            this.Refresh();
        }

        public void Обновить()
        {
            this.Refresh();
        }

        public int ПолучитьВысотуТаблицы()
        {
            int num = 0;
            for (int i = 1; i <= base.RowCount; i++)
            {
                num += base.RowHeights[i];
            }
            return (num - 0x11);
        }

        private void ПостроитьТаблицуПоМодели(МодельТаблицыОтчетнойФормы МодельТаблицы)
        {
            int num2;
            base.Rows.FrozenCount = МодельТаблицы.КоличествоФиксированныхСтрок;
            base.Cols.FrozenCount = МодельТаблицы.КоличествоФиксированныхСтолбцов;
            base.ColCount = МодельТаблицы.КоличествоСтолбцов;
            base.RowCount = МодельТаблицы.КоличествоСтрок;
            int index = 1;
            while (index <= base.ColCount)
            {
                if (МодельТаблицы.ШириныСтолбцов[index] == 0)
                {
                    base.Cols.Hidden[index] = true;
                }
                else
                {
                    base.ColWidths[index] = МодельТаблицы.ШириныСтолбцов[index];
                }
                index++;
            }
            for (num2 = 1; num2 <= base.RowCount; num2++)
            {
                if (МодельТаблицы.ВысотыСтрок[num2] == 0)
                {
                    base.Rows.Hidden[num2] = true;
                }
                else
                {
                    base.RowHeights[num2] = МодельТаблицы.ВысотыСтрок[num2];
                }
            }
            foreach (ОбъединениеЯчеек ячеек in МодельТаблицы.ОбъедиенияЯчеек)
            {
                base.CoveredRanges.Add(GridRangeInfo.Cells(ячеек.Top, ячеек.Left, ячеек.Bottom, ячеек.Right));
            }
            this.Представление.BeginUpdate();
            Color color = Color.FromArgb(0x24, 0x33, 100);
            GridBorder border = new GridBorder(GridBorderStyle.Solid, color);
            for (num2 = 1; num2 <= base.RowCount; num2++)
            {
                for (index = 1; index <= base.ColCount; index++)
                {
                    ЯчейкаТаблицыОтчетнойФормы формы = МодельТаблицы.Ячейки[num2, index];
                    if (формы != null)
                    {
                        формы.Значение = this.ПроанализироватьСсылкиНаКонстанты(формы.Значение);
                        GridStyleInfo info = base[num2, index];
                        info.BeginUpdate();
                        if (формы.Формула)
                        {
                            info.CellType = "FormulaCell";
                        }
                        if (!string.IsNullOrEmpty(формы.Значение))
                        {
                            string input = формы.Значение;
                            if (формы.Формула && input.ToUpper().StartsWith("=IF"))
                            {
                                Match match = регулярник.Match(input);
                                if (match.Success)
                                {
                                    input = string.Format("={0}", match.Groups["false"]);
                                }
                            }
                            info.CellValue = input;
                        }
                        if (!string.IsNullOrEmpty(формы.ПараметрыШрифта.Наименование))
                        {
                            info.Font.Facename = формы.ПараметрыШрифта.Наименование;
                        }
                        if (формы.ПараметрыШрифта.Размер != 0f)
                        {
                            info.Font.Size = формы.ПараметрыШрифта.Размер;
                        }
                        if (формы.ПараметрыШрифта.Ориентация != 0)
                        {
                            info.Font.Orientation = формы.ПараметрыШрифта.Ориентация;
                        }
                        info.Font.Bold = формы.ПараметрыШрифта.Жирный;
                        info.Font.Italic = формы.ПараметрыШрифта.Курсив;
                        info.Font.Strikeout = формы.ПараметрыШрифта.Зачеркнутый;
                        info.Font.Underline = формы.ПараметрыШрифта.Подчеркнутый;
                        if (формы.ПараметрыЗаливки != null)
                        {
                            info.Interior = формы.ПараметрыЗаливки;
                        }
                        if (формы.ЦветТекста != Color.Empty)
                        {
                            info.TextColor = формы.ЦветТекста;
                        }
                        if (формы.ВерхняяГраница == ГраницаЯчейки.Выделенная)
                        {
                            info.Borders.Top = border;
                        }
                        if (формы.ЛеваяГраница == ГраницаЯчейки.Выделенная)
                        {
                            info.Borders.Left = border;
                        }
                        if (формы.НижняяГраница == ГраницаЯчейки.Выделенная)
                        {
                            info.Borders.Bottom = border;
                        }
                        if (формы.ПраваяГраница == ГраницаЯчейки.Выделенная)
                        {
                            info.Borders.Right = border;
                        }
                        info.HorizontalAlignment = (GridHorizontalAlignment) формы.ВыравниваниеПоГоризонтали;
                        info.VerticalAlignment = (GridVerticalAlignment) формы.ВыравниваниеПоВертикали;
                        info.EndUpdate();
                        this.ПроанализироватьЯчейку(формы, num2, index);
                    }
                }
            }
            this.Представление.EndUpdate();
        }

        protected virtual string ПроанализироватьСсылкиНаКонстанты(string ИсходноеЗначение)
        {
            return ИсходноеЗначение;
        }

        protected virtual void ПроанализироватьЯчейку(ЯчейкаТаблицыОтчетнойФормы Ячейка, int Row, int Col)
        {
        }

        protected void ПроинициализироватьФункцииАвтоблоков(ОтчетнаяФормаДанных ОтчетнаяФорма)
        {
            GridFormulaCellModel model = (GridFormulaCellModel) this.Представление.CellModels["FormulaCell"];
            GridFormulaEngine engine = model.Engine;
            base.DrawCellDisplayText += new GridDrawCellDisplayTextEventHandler(this.ТаблицаExcel_DrawCellDisplayText);
            this.функцииАвтоблоков = new ФункцииАвтоблоков(ОтчетнаяФорма);
            if (engine.LibraryFunctions.ContainsKey("СуммаПоСтолбцу".ToUpper()))
            {
                engine.RemoveFunction("СуммаПоСтолбцу".ToUpper());
            }
            if (engine.LibraryFunctions.ContainsKey("КоличествоСтрок".ToUpper()))
            {
                engine.RemoveFunction("КоличествоСтрок".ToUpper());
            }
            engine.AddFunction("СуммаПоСтолбцу", new GridFormulaEngine.LibraryFunction(this.функцииАвтоблоков.СуммаПоСтолбцу));
            engine.AddFunction("КоличествоСтрок", new GridFormulaEngine.LibraryFunction(this.функцииАвтоблоков.КоличествоСтрок));
        }

        public XmlDocument СериализоватьВXML()
        {
            XmlElement element10;
            int num2;
            XmlElement element15;
            XmlElement element16;
            XmlElement element17;
            XmlElement element18;
            XmlDocument document = new XmlDocument();
            XmlElement newChild = document.CreateElement("GridSyncProperties");
            document.AppendChild(newChild);
            XmlElement element2 = document.CreateElement("FrozenRowCount");
            element2.InnerText = base.Rows.FrozenCount.ToString();
            newChild.AppendChild(element2);
            XmlElement element3 = document.CreateElement("FrozenColCount");
            element3.InnerText = base.Cols.FrozenCount.ToString();
            newChild.AppendChild(element3);
            XmlElement element4 = document.CreateElement("ColCount");
            element4.InnerText = base.ColCount.ToString();
            newChild.AppendChild(element4);
            XmlElement element5 = document.CreateElement("RowCount");
            element5.InnerText = base.RowCount.ToString();
            newChild.AppendChild(element5);
            XmlElement element6 = document.CreateElement("Cells");
            newChild.AppendChild(element6);
            XmlElement element7 = document.CreateElement("GridCellsMemento");
            element6.AppendChild(element7);
            XmlElement element8 = document.CreateElement("ColumnStyleCollection");
            element7.AppendChild(element8);
            XmlElement element9 = document.CreateElement("ColumnWidths");
            element8.AppendChild(element9);
            int num = 0;
            while (num <= base.ColCount)
            {
                element10 = document.CreateElement("GridRowColEntry");
                element9.AppendChild(element10);
                element10.SetAttribute("Index", num.ToString());
                int num3 = base.ColWidths[num];
                element10.SetAttribute("Length", num3.ToString());
                num++;
            }
            XmlElement element11 = document.CreateElement("RowStyleCollection");
            element7.AppendChild(element11);
            XmlElement element12 = document.CreateElement("RowHeights");
            element11.AppendChild(element12);
            for (num2 = 0; num2 <= base.RowCount; num2++)
            {
                element10 = document.CreateElement("GridRowColEntry");
                element12.AppendChild(element10);
                element10.SetAttribute("Index", num2.ToString());
                element10.SetAttribute("Length", base.RowHeights[num2].ToString());
            }
            XmlElement element13 = document.CreateElement("CoveredRanges");
            element7.AppendChild(element13);
            foreach (GridRangeInfo info in base.CoveredRanges)
            {
                XmlElement element14 = document.CreateElement("GridNonImmutableRangeInfo");
                element13.AppendChild(element14);
                element15 = document.CreateElement("Top");
                element15.InnerText = info.Top.ToString();
                element14.AppendChild(element15);
                element16 = document.CreateElement("Left");
                element16.InnerText = info.Left.ToString();
                element14.AppendChild(element16);
                element17 = document.CreateElement("Bottom");
                element17.InnerText = info.Bottom.ToString();
                element14.AppendChild(element17);
                element18 = document.CreateElement("Right");
                element18.InnerText = info.Right.ToString();
                element14.AppendChild(element18);
            }
            for (num2 = 1; num2 <= base.RowCount; num2++)
            {
                for (num = 1; num <= base.ColCount; num++)
                {
                    GridStyleInfo info2 = base[num2, num];
                    XmlElement element19 = document.CreateElement("CellData");
                    element7.AppendChild(element19);
                    element19.SetAttribute("Row", num2.ToString());
                    element19.SetAttribute("Col", num.ToString());
                    XmlElement element20 = document.CreateElement("CellContents");
                    element19.AppendChild(element20);
                    if (info2.CellType != "TextBox")
                    {
                        XmlElement element21 = document.CreateElement("CellType");
                        element21.InnerText = string.Format("\"{0}\"", info2.CellType);
                        element20.AppendChild(element21);
                    }
                    if (info2.HasCellValue)
                    {
                        XmlElement element22 = document.CreateElement("CellValue");
                        element20.AppendChild(element22);
                        XmlElement element23 = document.CreateElement("anyType");
                        element23.InnerText = (info2.CellValue == null) ? "" : info2.CellValue.ToString();
                        element22.AppendChild(element23);
                    }
                    if (info2.HasInterior && (info2.Interior.Description != "Solid; 210, 220, 255"))
                    {
                        XmlElement element24 = document.CreateElement("Interior");
                        element20.AppendChild(element24);
                        element24.InnerText = info2.Interior.Description;
                    }
                    if (info2.HasTextColor && (info2.TextColor != Color.FromArgb(0, 0, 0)))
                    {
                        XmlElement element25 = document.CreateElement("TextColor");
                        element20.AppendChild(element25);
                        element25.InnerText = string.Format("{0}; {1}; {2}", info2.TextColor.R, info2.TextColor.G, info2.TextColor.B);
                    }
                    if (info2.HasFont)
                    {
                        XmlElement element26 = document.CreateElement("Font");
                        element20.AppendChild(element26);
                        if (info2.Font.HasFacename)
                        {
                            XmlElement element27 = document.CreateElement("Facename");
                            element27.InnerText = info2.Font.Facename;
                            element26.AppendChild(element27);
                        }
                        if (info2.Font.HasSize)
                        {
                            XmlElement element28 = document.CreateElement("Size");
                            element28.InnerText = info2.Font.Size.ToString();
                            element26.AppendChild(element28);
                        }
                        if (info2.Font.HasOrientation)
                        {
                            XmlElement element29 = document.CreateElement("Orientation");
                            element29.InnerText = info2.Font.Orientation.ToString();
                            element26.AppendChild(element29);
                        }
                        if (info2.Font.HasBold && info2.Font.Bold)
                        {
                            XmlElement element30 = document.CreateElement("Bold");
                            element30.InnerText = "true";
                            element26.AppendChild(element30);
                        }
                        if (info2.Font.HasItalic && info2.Font.Italic)
                        {
                            XmlElement element31 = document.CreateElement("Italic");
                            element31.InnerText = "true";
                            element26.AppendChild(element31);
                        }
                        if (info2.Font.HasStrikeout && info2.Font.Strikeout)
                        {
                            XmlElement element32 = document.CreateElement("Strikeout");
                            element32.InnerText = "true";
                            element26.AppendChild(element32);
                        }
                        if (info2.Font.HasUnderline && info2.Font.Underline)
                        {
                            XmlElement element33 = document.CreateElement("Underline");
                            element33.InnerText = "true";
                            element26.AppendChild(element33);
                        }
                    }
                    if (info2.Tag != null)
                    {
                        XmlElement element34 = document.CreateElement("Tag");
                        element20.AppendChild(element34);
                        XmlElement element35 = document.CreateElement("anyType");
                        element35.InnerText = info2.Tag.ToString();
                        element34.AppendChild(element35);
                    }
                    if (info2.HasBorders)
                    {
                        XmlElement element36 = document.CreateElement("Borders");
                        element20.AppendChild(element36);
                        if (info2.Borders.Top.Style != GridBorderStyle.Standard)
                        {
                            element15 = document.CreateElement("Top");
                            element15.InnerText = "Solid";
                            element36.AppendChild(element15);
                        }
                        if (info2.Borders.Bottom.Style != GridBorderStyle.Standard)
                        {
                            element17 = document.CreateElement("Bottom");
                            element17.InnerText = "Solid";
                            element36.AppendChild(element17);
                        }
                        if (info2.Borders.Left.Style != GridBorderStyle.Standard)
                        {
                            element16 = document.CreateElement("Left");
                            element16.InnerText = "Solid";
                            element36.AppendChild(element16);
                        }
                        if (info2.Borders.Right.Style != GridBorderStyle.Standard)
                        {
                            element18 = document.CreateElement("Right");
                            element18.InnerText = "Solid";
                            element36.AppendChild(element18);
                        }
                    }
                    XmlElement element37 = document.CreateElement("HorizontalAlignment");
                    element37.InnerText = info2.HorizontalAlignment.ToString();
                    element20.AppendChild(element37);
                    XmlElement element38 = document.CreateElement("VerticalAlignment");
                    element38.InnerText = info2.VerticalAlignment.ToString();
                    element20.AppendChild(element38);
                }
            }
            return document;
        }

        private void ТаблицаExcel_DrawCellDisplayText(object sender, GridDrawCellDisplayTextEventArgs e)
        {
            string str = e.DisplayText.ToUpper();
            if ((str != null) && ((str == "NAN") || (str == "БЕСКОНЕЧНОСТЬ")))
            {
                if (!string.IsNullOrEmpty(e.Style.Format))
                {
                    e.DisplayText = 0.ToString(e.Style.Format);
                }
                else
                {
                    e.DisplayText = "0";
                }
            }
        }

        public string ИмяЛиста
        {
            get
            {
                return this.имяЛиста;
            }
            set
            {
                this.имяЛиста = value;
            }
        }

        public string КодТаблицы
        {
            get
            {
                return this.кодТаблицы;
            }
            set
            {
                this.кодТаблицы = value;
            }
        }

        public GridModel Представление
        {
            get
            {
                return base.Model;
            }
            set
            {
                base.Model = value;
            }
        }

        public GridStyleInfo СтандартныйСтиль
        {
            get
            {
                return base.BaseStylesMap["Standard"].StyleInfo;
            }
        }
    }
}

