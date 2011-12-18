namespace Барс.Своды.БраузерОтчетныхФорм
{
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Xml.XPath;
    using Барс;
    using Барс.Своды.ОтчетнаяФорма;
    using Барс.Своды.ТипыЯчеек;

    public class ШапкаЭкраннойФормы : ТаблицаОтчетнойФормы
    {
        private НаборЯчеекДанных ячейкиДанных;
        private Dictionary<string, ЯчейкаМетаструктуры> ячейкиМетаструктуры;

        public ШапкаЭкраннойФормы()
        {
            this.ячейкиМетаструктуры = new Dictionary<string, ЯчейкаМетаструктуры>();
            this.ячейкиДанных = new НаборЯчеекДанных();
        }

        public ШапкаЭкраннойФормы(Dictionary<string, ЯчейкаМетаструктуры> ЯчейкиМетаструктуры, НаборЯчеекДанных ЯчейкиДанных) : this()
        {
            this.ячейкиМетаструктуры = ЯчейкиМетаструктуры;
            this.ячейкиДанных = ЯчейкиДанных;
            base.CurrentCellActivated += new EventHandler(this.ШапкаЭкраннойФормы_CurrentCellActivated);
        }

        public ШапкаЭкраннойФормы(МетаструктураФормы Метаструктура, ТаблицаДанных ТаблицаДанных) : this()
        {
        }

        protected override void Dispose(bool disposing)
        {
            this.ячейкиМетаструктуры = null;
            this.ячейкиДанных = null;
            base.Dispose(disposing);
        }

        protected override void OnSaveCellInfo(GridSaveCellInfoEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColIndex;
            if ((rowIndex > 0) && (colIndex > 0))
            {
                GridStyleInfo style = e.Style;
                if ((style.Tag != null) && (this.ЯчейкиДанных != null))
                {
                    string str = ЯчейкаМетаструктуры.ПолучитьКодЯчейки(style.Tag.ToString());
                    if (this.ЯчейкиДанных.СодержитИндекс(str))
                    {
                        ТипЯчейки ячейки = this.ЯчейкиДанных[str];
                        if (style.CellType == "FormulaCell")
                        {
                            ячейки.Значение = e.Style.FormattedText;
                        }
                        else
                        {
                            if (ячейки is СсылкаНаСправочник)
                            {
                                if (e.Style.CellValue is ЗаписьСправочника)
                                {
                                    ячейки.Значение = e.Style.CellValue;
                                    e.Style.Text = (ячейки as СсылкаНаСправочник).ЗначениеСтрокой;
                                }
                                if (e.Style.CellValue == null)
                                {
                                    ячейки.Значение = null;
                                    e.Style.Text = string.Empty;
                                }
                            }
                            else
                            {
                                ячейки.Значение = e.Style.Text;
                            }
                            base.ЭкраннаяФорма.ДанныеИзменились = true;
                            if (ячейки.ТолькоЧтение)
                            {
                                this.УстановитьСтильЯчейкиТолькоЧтение(style);
                            }
                            else if (ячейки.Описание.ОбязательноДляЗаполнения)
                            {
                                this.УстановитьСтильОбязательнойДляЗаполненияЯчейки(style, ячейки.ЗначениеЗаполнено);
                            }
                            else if ((base.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (base.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФормаБезРедактирования))
                            {
                                base.УстановитьСтильНередактируемойЯчейки(style);
                            }
                            else
                            {
                                base.УстановитьСтильРедактируемойЯчейки(style);
                            }
                        }
                    }
                }
            }
        }

        public override void ЗагрузитьИзXML(XPathNavigator документ)
        {
            base.ЗагрузитьИзXML(документ);
            base.Rows.Hidden[base.RowCount] = false;
        }

        public override void ОбновитьЗначения()
        {
            base.BeginUpdate();
            for (int i = 1; i <= base.RowCount; i++)
            {
                for (int j = 1; j <= base.ColCount; j++)
                {
                    GridStyleInfo info = base[i, j];
                    if ((info.CellType != "FormulaCell") && ((info.Tag != null) && (this.ЯчейкиМетаструктуры != null)))
                    {
                        string str2 = ЯчейкаМетаструктуры.ПолучитьКодЯчейки(info.Tag.ToString());
                        if (this.ЯчейкиДанных.СодержитИндекс(str2))
                        {
                            ТипЯчейки ячейки = this.ЯчейкиДанных[str2];
                            if (ячейки != null)
                            {
                                if (ячейки is СсылочныйТип)
                                {
                                    info.CellValue = (ячейки as СсылочныйТип).ЗначениеСтрокой;
                                }
                                else
                                {
                                    info.CellValue = ячейки.Значение;
                                }
                            }
                            else
                            {
                                info.CellValue = null;
                            }
                        }
                    }
                }
            }
            base.EndUpdate();
        }

        protected override void ПроанализироватьЯчейку(ЯчейкаТаблицыОтчетнойФормы Ячейка, int Row, int Col)
        {
            GridStyleInfo info = base[Row, Col];
            if (Ячейка.Формула)
            {
                base.УстановитьСтильАвтоблока(info);
            }
            else if (!string.IsNullOrEmpty(Ячейка.Значение))
            {
                if (Ячейка.Значение.StartsWith("@"))
                {
                    base.УстановитьСтильКнопки(info, Ячейка.Значение);
                }
                info.EndUpdate();
                return;
            }
            if (Ячейка.Переменная != null)
            {
                info.Tag = Ячейка.Переменная;
                string key = ЯчейкаМетаструктуры.ПолучитьКодЯчейки(Ячейка.Переменная);
                if (this.ЯчейкиМетаструктуры.ContainsKey(key) && !Ячейка.Формула)
                {
                    Type type = this.ЯчейкиМетаструктуры[key].ТипЗначения;
                    info.BeginUpdate();
                    try
                    {
                        base.УстановитьТипЯчейки(info, type, this.ЯчейкиМетаструктуры[key].Описание);
                        if ((base.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (base.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФормаБезРедактирования))
                        {
                            base.УстановитьСтильНередактируемойЯчейки(info);
                        }
                        else if (this.ЯчейкиДанных[key].Описание.ОбязательноДляЗаполнения)
                        {
                            base.словарьОбязательныхДляЗаполненияЯчеек.Add(key, this.ЯчейкиДанных[key]);
                            this.УстановитьСтильОбязательнойДляЗаполненияЯчейки(info, false);
                        }
                        else if (this.ЯчейкиДанных[key].ТолькоЧтение)
                        {
                            this.УстановитьСтильЯчейкиТолькоЧтение(info);
                        }
                        else
                        {
                            base.УстановитьСтильРедактируемойЯчейки(info);
                        }
                        if (this.ЯчейкиДанных.СодержитИндекс(key))
                        {
                            ТипЯчейки ячейки = this.ЯчейкиДанных[key];
                            if (ячейки != null)
                            {
                                if (ячейки is СсылочныйТип)
                                {
                                    info.CellValue = (ячейки as СсылочныйТип).ЗначениеСтрокой;
                                }
                                else
                                {
                                    info.CellValue = ячейки.Значение;
                                }
                            }
                            else
                            {
                                info.CellValue = null;
                            }
                        }
                    }
                    finally
                    {
                        info.EndUpdate();
                    }
                }
            }
        }

        public override bool ПроверитьЗаполненностьДанных(out Dictionary<string, List<string>> КоординатыНезаполненныхЯчеек)
        {
            КоординатыНезаполненныхЯчеек = new Dictionary<string, List<string>>();
            if (base.словарьОбязательныхДляЗаполненияЯчеек.Count == 0)
            {
                return true;
            }
            List<string> list = null;
            foreach (KeyValuePair<string, ТипЯчейки> pair in base.словарьОбязательныхДляЗаполненияЯчеек)
            {
                if ((pair.Value == null) || !pair.Value.ЗначениеЗаполнено)
                {
                    if (!КоординатыНезаполненныхЯчеек.TryGetValue("!Свободные ячейки", out list))
                    {
                        list = new List<string>();
                        КоординатыНезаполненныхЯчеек.Add("!Свободные ячейки", list);
                    }
                    list.Add(pair.Key.Replace("$", ""));
                }
            }
            return (КоординатыНезаполненныхЯчеек.Count == 0);
        }

        protected override void УстановитьСтандартныйСтильЯчейки()
        {
            base.УстановитьСтандартныйСтильЯчейки();
            base.СтандартныйСтиль.Enabled = false;
        }

        protected override void УстановитьСтильЯчейкиТолькоЧтение(GridStyleInfo Стиль)
        {
            Стиль.Enabled = true;
            Стиль.ReadOnly = true;
            Стиль.BackColor = Color.FromArgb(210, 220, 0xff);
            if (Стиль.Tag != null)
            {
                ТипЯчейки ячейки = null;
                ячейки = this.ЯчейкиДанных[Стиль.Tag.ToString().Replace("$", "")];
                ячейки.ТолькоЧтение = true;
            }
        }

        private void ШапкаЭкраннойФормы_CurrentCellActivated(object sender, EventArgs e)
        {
            if (base.ЭкраннаяФорма != null)
            {
                base.ЭкраннаяФорма.ОтобразитьПодсказку("");
            }
            int rowIndex = this.CurrentCell.RowIndex;
            int colIndex = this.CurrentCell.ColIndex;
            GridStyleInfo info = base[rowIndex, colIndex];
            string str = "";
            if (((info != null) && (info.Tag != null)) && (info.Tag is string))
            {
                string str2 = info.Tag.ToString().Trim(new char[] { '$' });
                if (this.ЯчейкиДанных != null)
                {
                    ТипЯчейки ячейки = this.ЯчейкиДанных[str2];
                    if (!((ячейки == null) || string.IsNullOrEmpty(ячейки.Описание.Комментарий)))
                    {
                        str = str + string.Format(" Комментарий : {0}", ячейки.Описание.Комментарий);
                    }
                }
            }
            base.ЭкраннаяФорма.ОтобразитьПодсказку(str);
        }

        public override ТипЯчейки ВыбраннаяЯчейка
        {
            get
            {
                GridStyleInfo info = base.СтильТекущейЯчейки;
                if (((info != null) && (info.Tag != null)) && (info.Tag is string))
                {
                    string str = info.Tag.ToString().Replace("$", "");
                    if (this.ЯчейкиДанных.СодержитИндекс(str))
                    {
                        return this.ЯчейкиДанных[str];
                    }
                }
                return null;
            }
        }

        public НаборЯчеекДанных ЯчейкиДанных
        {
            get
            {
                return this.ячейкиДанных;
            }
        }

        public Dictionary<string, ЯчейкаМетаструктуры> ЯчейкиМетаструктуры
        {
            get
            {
                return this.ячейкиМетаструктуры;
            }
        }
    }
}

