namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Mask;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Views.BandedGrid;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;
    using Барс;
    using Барс.Интерфейс.ЭлементыТаблицы;
    using Барс.Своды;
    using Барс.Своды.ОтчетнаяФорма;
    using Барс.Своды.ТипыЯчеек;

    public class СтолбецОбъединенияДинамическойТаблицы : BandedGridColumn
    {
        private string автоблок = string.Empty;
        private HorzAlignment выравниваниеТекста = HorzAlignment.Default;
        private object источникДанных;
        private ОбщееОписаниеТипаЯчейки описаниеЯчейки = null;
        private Type типЭлементаДляВыбора;

        public СтолбецОбъединенияДинамическойТаблицы()
        {
            this.Visible = true;
            this.VisibleIndex = 0;
            base.OptionsColumn.AllowEdit = false;
            base.OptionsColumn.AllowGroup = DefaultBoolean.True;
            base.OptionsColumn.AllowMove = true;
            base.DisplayFormat.FormatType = FormatType.None;
            this.РазрешитьРедактирование = true;
            base.UnboundType = UnboundColumnType.Object;
        }

        public void ВыставитьСтильВычисляемогоСтолбца()
        {
            base.OptionsColumn.AllowEdit = false;
            base.AppearanceCell.BackColor = Color.FromArgb(210, 220, 0xff);
            base.AppearanceCell.Options.UseBackColor = true;
            base.UnboundType = UnboundColumnType.String;
            base.FilterMode = ColumnFilterMode.DisplayText;
        }

        public void ВыставитьТипСтолбца(СтолбецМетаструктуры СтолбецМетастуктуры, ОтчетнаяФормаДанных ОтчетнаяФорма)
        {
            RepositoryItem item = null;
            ОписаниеМножественнойСсылкиНаСправочник справочник;
            ОписаниеСсылкиНаСправочник справочник2;
            this.описаниеЯчейки = СтолбецМетастуктуры.ОписаниеТипаЯчейки;
            if (СтолбецМетастуктуры.Тип.IsSubclassOf(typeof(ОбщийЧисловойТип)))
            {
                Type type = СтолбецМетастуктуры.Тип;
                int num = 0;
                string str = "";
                string displayFormat = "";
                if (type == typeof(ФинансовыйТип))
                {
                    num = 2;
                }
                else if (type == typeof(ЦелочисленныйТип))
                {
                    num = 0;
                }
                else if ((type == typeof(ЧисловойТип)) && ((this.описаниеЯчейки != null) && (this.описаниеЯчейки is ОписаниеЧисловогоТипа)))
                {
                    num = (this.описаниеЯчейки as ОписаниеЧисловогоТипа).Точность;
                }
                if (num == 0)
                {
                    str = "N00";
                }
                else
                {
                    str = "N" + num.ToString();
                }
                displayFormat = "{" + string.Format("0:{0}", str) + "}";
                this.СтрокаФорматирования = str;
                base.SummaryItem.SummaryType = SummaryItemType.Sum;
                base.SummaryItem.DisplayFormat = displayFormat;
                base.UnboundType = UnboundColumnType.Decimal;
                РедакторЧислаДинамическойТаблицы таблицы = new РедакторЧислаДинамическойТаблицы();
                таблицы.DisplayFormat.FormatString = displayFormat;
                if (base.View != null)
                {
                    base.View.GroupSummary.Add(new GridGroupSummaryItem(SummaryItemType.Sum, base.FieldName, this, displayFormat));
                }
                item = таблицы;
                goto Label_0679;
            }
            if (СтолбецМетастуктуры.Тип == typeof(ТипДатаВремя))
            {
                base.UnboundType = UnboundColumnType.DateTime;
                if (СтолбецМетастуктуры.ОписаниеТипаЯчейки is ОписаниеТипаДатаВремя)
                {
                    RepositoryItem item2 = null;
                    ОписаниеТипаДатаВремя время = СтолбецМетастуктуры.ОписаниеТипаЯчейки as ОписаниеТипаДатаВремя;
                    if (время.РедакторВремени)
                    {
                        item2 = new RepositoryItemTimeEdit();
                    }
                    else
                    {
                        item2 = new РедакторДатыДинамическойТаблицы();
                        if (!string.IsNullOrEmpty(время.ФорматОтображения))
                        {
                            (item2 as РедакторДатыДинамическойТаблицы).Mask.MaskType = MaskType.DateTime;
                            (item2 as РедакторДатыДинамическойТаблицы).Mask.EditMask = время.ФорматОтображения;
                            (item2 as РедакторДатыДинамическойТаблицы).Mask.UseMaskAsDisplayFormat = true;
                        }
                    }
                    item = item2;
                }
                else
                {
                    РедакторДатыДинамическойТаблицы таблицы2 = new РедакторДатыДинамическойТаблицы();
                    item = таблицы2;
                }
                goto Label_0679;
            }
            if (СтолбецМетастуктуры.Тип == typeof(ТипИзображение))
            {
                base.UnboundType = UnboundColumnType.Object;
                RepositoryItemImageEdit edit = new RepositoryItemImageEdit();
                edit.NullText = string.Empty;
                edit.ShowIcon = true;
                edit.SizeMode = PictureSizeMode.Stretch;
                item = edit;
                goto Label_0679;
            }
            if (СтолбецМетастуктуры.Тип == typeof(СтроковыйТип))
            {
                if ((this.описаниеЯчейки != null) && (this.описаниеЯчейки is ОписаниеСтроковогоТипаЯчейки))
                {
                    ОписаниеСтроковогоТипаЯчейки ячейки = this.описаниеЯчейки as ОписаниеСтроковогоТипаЯчейки;
                    if (!string.IsNullOrEmpty(ячейки.МаскаВвода))
                    {
                        ПолеВводаТекста текста = new ПолеВводаТекста();
                        текста.Mask.MaskType = MaskType.RegEx;
                        текста.Mask.EditMask = ячейки.МаскаВвода;
                        текста.Mask.ShowPlaceHolders = true;
                        текста.Mask.UseMaskAsDisplayFormat = true;
                        текста.Mask.IgnoreMaskBlank = false;
                        item = текста;
                    }
                    else if (ячейки.МногострочныйРедактор)
                    {
                        ВыпадающийТекст текст = new ВыпадающийТекст();
                        item = текст;
                    }
                    else
                    {
                        base.UnboundType = UnboundColumnType.String;
                    }
                }
                else
                {
                    base.UnboundType = UnboundColumnType.String;
                }
                goto Label_0679;
            }
            if (СтолбецМетастуктуры.Тип == typeof(ЛогическийТип))
            {
                this.ТипЭлементаДляВыбора = typeof(Учреждение);
                base.UnboundType = UnboundColumnType.Boolean;
                РедакторФлажокДинамическойТаблицы таблицы3 = new РедакторФлажокДинамическойТаблицы();
                item = таблицы3;
                goto Label_0679;
            }
            if ((СтолбецМетастуктуры.Тип != typeof(СсылкаНаСправочник)) && (СтолбецМетастуктуры.Тип != typeof(МножественнаяСсылкаНаСправочник)))
            {
                goto Label_0679;
            }
            ВыборИзСправочника справочника = new ВыборИзСправочника();
            if ((this.описаниеЯчейки != null) && ((this.описаниеЯчейки is ОписаниеСсылкиНаСправочник) || (this.описаниеЯчейки is ОписаниеМножественнойСсылкиНаСправочник)))
            {
                справочник = null;
                справочник2 = null;
                string str3 = "";
                if (this.описаниеЯчейки is ОписаниеМножественнойСсылкиНаСправочник)
                {
                    справочник = this.описаниеЯчейки as ОписаниеМножественнойСсылкиНаСправочник;
                    str3 = справочник.КодСправочника;
                }
                else if (this.описаниеЯчейки is ОписаниеСсылкиНаСправочник)
                {
                    справочник2 = this.описаниеЯчейки as ОписаниеСсылкиНаСправочник;
                    str3 = справочник2.КодСправочника;
                }
                string str4 = str3.Trim().ToLower();
                if (str4 == null)
                {
                    goto Label_057D;
                }
                if (!(str4 == "учреждение"))
                {
                    if (str4 == "типучреждения")
                    {
                        this.ТипЭлементаДляВыбора = typeof(ТипУчреждения);
                        this.ИсточникДанных = null;
                        goto Label_0643;
                    }
                    if (str4 == "видучреждения")
                    {
                        this.ТипЭлементаДляВыбора = typeof(ВидУчреждения);
                        this.ИсточникДанных = null;
                        goto Label_0643;
                    }
                    goto Label_057D;
                }
                this.ТипЭлементаДляВыбора = typeof(Учреждение);
                this.ИсточникДанных = null;
            }
            goto Label_0643;
        Label_057D:
            this.ТипЭлементаДляВыбора = null;
            if (справочник != null)
            {
                ВыборЗаписейСправочника справочника2 = new ВыборЗаписейСправочника(РежимВыбораЭлементовСпискаВыбора.Множественный);
                справочника2.ОписаниеСсылки = справочник;
                if ((ОтчетнаяФорма != null) && (ОтчетнаяФорма.КомпонентОтчетногоПериода != null))
                {
                    справочника2.ДатаАктуальности = ОтчетнаяФорма.КомпонентОтчетногоПериода.ОтчетныйПериод.ДатаНачала;
                }
                this.ИсточникДанных = справочника2;
            }
            else if (справочник2 != null)
            {
                ВыборИзУниверсальногоСправочника справочника3 = new ВыборИзУниверсальногоСправочника();
                справочника3.ОписаниеСсылки = справочник2;
                if ((ОтчетнаяФорма != null) && (ОтчетнаяФорма.КомпонентОтчетногоПериода != null))
                {
                    справочника3.ДатаАктуальности = ОтчетнаяФорма.КомпонентОтчетногоПериода.ОтчетныйПериод.ДатаНачала;
                }
                this.ИсточникДанных = справочника3;
            }
        Label_0643:
            base.UnboundType = UnboundColumnType.Object;
            base.SortMode = ColumnSortMode.DisplayText;
            base.FilterMode = ColumnFilterMode.DisplayText;
            base.OptionsColumn.AllowGroup = DefaultBoolean.True;
            base.OptionsColumn.AllowSort = DefaultBoolean.True;
            item = справочника;
        Label_0679:
            base.ColumnEdit = item;
        }

        public void ВыставитьШаблон(string Шаблон)
        {
            if (!string.IsNullOrEmpty(Шаблон) && Шаблон.StartsWith("#"))
            {
                string input = Шаблон;
                input = input.Substring(1);
                if (input.IndexOf("[") == -1)
                {
                    if (input.StartsWith("={") && input.EndsWith("}"))
                    {
                        this.автоблок = "=" + input.Substring(2, input.Length - 3);
                    }
                    else
                    {
                        input = Regex.Replace(input, @"[_A-Za-zа-яА-Я\d]+", delegate (Match match) {
                            return string.Format("[{0}]", match);
                        });
                        this.автоблок = input;
                    }
                }
                else
                {
                    this.автоблок = input;
                }
            }
        }

        public bool ЕстьНезаполненныеЯчейки(ДанныеДинамическойТаблицы Данные, out List<string> НомераСтрок)
        {
            НомераСтрок = new List<string>();
            if ((Данные == null) || (Данные.ТаблицаДанных.МатрицаЗначений.Количество == 0))
            {
                return false;
            }
            bool flag = false;
            for (int i = 0; i < Данные.ТаблицаДанных.МатрицаЗначений.Количество; i++)
            {
                СтрокаДанных данных = Данные.ТаблицаДанных[i];
                ТипЯчейки ячейки = данных[base.FieldName];
                if (!((ячейки == null) || ячейки.ЗначениеЗаполнено))
                {
                    НомераСтрок.Add(данных.КодСтроки);
                    flag = true;
                }
            }
            return flag;
        }

        public string Автоблок
        {
            get
            {
                return this.автоблок;
            }
            set
            {
                this.автоблок = value;
            }
        }

        public HorzAlignment ВыравниваниеТекста
        {
            get
            {
                return this.выравниваниеТекста;
            }
            set
            {
                this.выравниваниеТекста = value;
            }
        }

        public bool ВычисляемыйСтолбец
        {
            get
            {
                return !string.IsNullOrEmpty(this.Автоблок);
            }
        }

        public string Заголовок
        {
            get
            {
                return this.Caption;
            }
            set
            {
                this.Caption = value;
            }
        }

        public string ИмяПоляИсточникаДанных
        {
            get
            {
                return base.FieldName;
            }
            set
            {
                base.FieldName = value;
                this.Name = "col" + value;
            }
        }

        public int ИндексВидимогоСтолбца
        {
            get
            {
                return base.AbsoluteIndex;
            }
            set
            {
                base.AbsoluteIndex = value;
            }
        }

        public object ИсточникДанных
        {
            get
            {
                return this.источникДанных;
            }
            set
            {
                this.источникДанных = value;
            }
        }

        public bool ОбязателенДляЗаполнения
        {
            get
            {
                return ((this.описаниеЯчейки != null) && this.описаниеЯчейки.ОбязательноДляЗаполнения);
            }
        }

        public ОбщееОписаниеТипаЯчейки ОписаниеЯчейки
        {
            get
            {
                return this.описаниеЯчейки;
            }
            set
            {
                this.описаниеЯчейки = value;
            }
        }

        public bool РазрешитьПереход
        {
            get
            {
                return base.OptionsColumn.AllowFocus;
            }
            set
            {
                base.OptionsColumn.AllowFocus = value;
            }
        }

        public bool РазрешитьРедактирование
        {
            get
            {
                return !base.OptionsColumn.ReadOnly;
            }
            set
            {
                base.OptionsColumn.AllowEdit = value;
                base.OptionsColumn.ReadOnly = !value;
            }
        }

        public bool СтолбецВидим
        {
            get
            {
                return this.Visible;
            }
            set
            {
                this.Visible = value;
            }
        }

        public string СтрокаФорматирования
        {
            get
            {
                return base.DisplayFormat.FormatString;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    base.DisplayFormat.FormatString = value;
                    base.DisplayFormat.FormatType = FormatType.Custom;
                    base.SummaryItem.DisplayFormat = "{0:" + value + "}";
                }
                else
                {
                    base.DisplayFormat.FormatType = FormatType.None;
                }
            }
        }

        public Type ТипЭлементаДляВыбора
        {
            get
            {
                return this.типЭлементаДляВыбора;
            }
            set
            {
                this.типЭлементаДляВыбора = value;
            }
        }

        public int ШиринаСтолбца
        {
            get
            {
                return this.Width;
            }
            set
            {
                this.Width = value;
            }
        }
    }
}

