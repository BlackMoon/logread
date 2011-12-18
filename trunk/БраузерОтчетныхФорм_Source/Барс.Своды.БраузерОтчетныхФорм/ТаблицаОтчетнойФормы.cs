namespace Барс.Своды.БраузерОтчетныхФорм
{
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml.XPath;
    using Барс;
    using Барс.Интерфейс;
    using Барс.Своды.АргументыСобытийОтчетнойФормы;
    using Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек;
    using Барс.Своды.ОтчетнаяФорма;
    using Барс.Своды.ТипыЯчеек;
    using Барс.Ядро;

    public class ТаблицаОтчетнойФормы : ТаблицаExcel, ИнтерфейсОтображаемойТаблицыЭкраннойФормы, IDisposable
    {
        private КонтекстноеМеню контекстноеМеню;
        private string наименование;
        internal ПараметрыСозданияРедактора_ВыборИзСправочника параметрыСозданияРедактора_ВыборИзСправочника;
        private КонтПодменю подменюИсторияСборки;
        protected Dictionary<string, ТипЯчейки> словарьОбязательныхДляЗаполненияЯчеек;
        protected List<string> списокОбязательныхДляЗаполненияСтолбцов;
        private Барс.Своды.ОтчетнаяФорма.ТаблицаДанных таблицаДанных;
        private Барс.Своды.ОтчетнаяФорма.ТаблицаМетаструктуры таблицаМетаструктуры;
        private Барс.Своды.БраузерОтчетныхФорм.ЭкраннаяФорма экраннаяФорма;

        public event СобытиеПослеУстановкиЗначенияЯчейки ПослеУстановкиЗначенияЯчейкиСправочника;

        public event СобытиеИзмененияТаблицыДанных ПриИзмененииДанных;

        public ТаблицаОтчетнойФормы()
        {
            this.списокОбязательныхДляЗаполненияСтолбцов = new List<string>();
            this.словарьОбязательныхДляЗаполненияЯчеек = new Dictionary<string, ТипЯчейки>();
            this.параметрыСозданияРедактора_ВыборИзСправочника = null;
            this.контекстноеМеню = null;
            this.подменюИсторияСборки = null;
            this.экраннаяФорма = null;
            this.таблицаМетаструктуры = null;
            this.таблицаДанных = null;
            КонтПунктМеню меню = null;
            this.контекстноеМеню = new КонтекстноеМеню();
            this.контекстноеМеню.ПриОткрытии = (ОбработчикОтменяемогоСобытия) Delegate.Combine(this.контекстноеМеню.ПриОткрытии, new ОбработчикОтменяемогоСобытия(this.контекстноеМеню_ПриОткрытии));
            this.подменюИсторияСборки = this.контекстноеМеню.ДобавитьПодМеню("Показать историю сборки");
            меню = this.подменюИсторияСборки.ДобавитьПунктМеню("Для текущей ячейки", new ОбработчикСобытия(this.элементМенюИстория_ПриНажатии));
            if (меню != null)
            {
                меню.Tag = ТипПостроенияИсторииСборки.ПоСтолбцу;
            }
            меню = this.подменюИсторияСборки.ДобавитьПунктМеню("Для всей строки", new ОбработчикСобытия(this.элементМенюИстория_ПриНажатии));
            if (меню != null)
            {
                меню.Tag = ТипПостроенияИсторииСборки.ПоСтроке;
            }
            меню = this.подменюИсторияСборки.ДобавитьПунктМеню("Сравнение с текущими данными источника", new ОбработчикСобытия(this.элементМенюИстория_ПриНажатии));
            if (меню != null)
            {
                меню.Tag = ТипПостроенияИсторииСборки.ПоСтрокеСИсточником;
            }
            this.ContextMenuStrip = this.контекстноеМеню;
            this.ЭкраннаяФорма = this.ЭкраннаяФорма;
            this.параметрыСозданияРедактора_ВыборИзСправочника = new ПараметрыСозданияРедактора_ВыборИзСправочника();
            this.параметрыСозданияРедактора_ВыборИзСправочника.Таблица = this;
            this.параметрыСозданияРедактора_ВыборИзСправочника.ОбработкаВыбораИзСправочника = new ВыборИзСправочника.СобытиеПолученияИсточникаЗаписейДляВыбора(this.ВыборИзСправочника_ПолучениеИсточникаЗаписейДляВыбора);
            this.УстановитьСтандартныйСтильЯчейки();
            this.УстановитьСтилевыеНастройкиТаблицы();
            this.ПроинициализироватьРедакторыЯчеек();
            base.CellButtonClicked += new GridCellButtonClickedEventHandler(this.ТаблицаОтчетнойФормы_CellButtonClicked);
            base.CurrentCellKeyDown += new KeyEventHandler(this.ТаблицаОтчетнойФормы_CurrentCellKeyDown);
            base.CurrentCellStartEditing += new CancelEventHandler(this.ТаблицаОтчетнойФормы_CurrentCellStartEditing);
            base.ColsHiding += new GridRowColHidingEventHandler(this.ТаблицаОтчетнойФормы_ColsHiding);
            base.RowsHiding += new GridRowColHidingEventHandler(this.ТаблицаОтчетнойФормы_RowsHiding);
            base.CurrentCellActivated += new EventHandler(this.ТаблицаОтчетнойФормы_CurrentCellActivated);
        }

        public ТаблицаОтчетнойФормы(Барс.Своды.ОтчетнаяФорма.ТаблицаМетаструктуры ТаблицаМетаструктуры, Барс.Своды.ОтчетнаяФорма.ТаблицаДанных ТаблицаДанных) : this()
        {
            this.таблицаМетаструктуры = ТаблицаМетаструктуры;
            this.таблицаДанных = ТаблицаДанных;
        }

        protected override void Dispose(bool disposing)
        {
            this.таблицаМетаструктуры = null;
            this.таблицаДанных = null;
            this.экраннаяФорма = null;
            base.Dispose(disposing);
        }

        protected override void OnDrawCellDisplayText(GridDrawCellDisplayTextEventArgs e)
        {
            base.OnDrawCellDisplayText(e);
            e.Cancel = GridGdiPaint.Instance.DrawText(e.Graphics, e.DisplayText, e.TextRectangle, e.Style);
        }

        protected override void OnFillRectangleHook(GridFillRectangleHookEventArgs e)
        {
            base.OnFillRectangleHook(e);
            e.Cancel = GridGdiPaint.Instance.FillRectangle(e.Graphics, e.Bounds, e.Brush);
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
        }

        protected override void OnSaveCellInfo(GridSaveCellInfoEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int colIndex = e.ColIndex;
            if ((rowIndex > 0) && (colIndex > 0))
            {
                GridStyleInfo style = e.Style;
                if ((style.Tag != null) && (this.ТаблицаДанных != null))
                {
                    string str = style.Tag.ToString();
                    ТипЯчейки ячейки = this.ТаблицаДанных[str];
                    if (ячейки != null)
                    {
                        if (style.CellType == "FormulaCell")
                        {
                            ячейки.Значение = e.Style.FormattedText;
                        }
                        else
                        {
                            style.BeginUpdate();
                            if (ячейки is СсылкаНаСправочник)
                            {
                                if (e.Style.CellValue is ЗаписьСправочника)
                                {
                                    ячейки.Значение = e.Style.CellValue;
                                    e.Style.Text = (ячейки as СсылкаНаСправочник).ЗначениеСтрокой;
                                }
                                else if (e.Style.CellValue is СписокВыбранныхЗаписейСправочника)
                                {
                                    ячейки.Значение = e.Style.CellValue;
                                    if (ячейки is МножественнаяСсылкаНаСправочник)
                                    {
                                        e.Style.Text = (ячейки as МножественнаяСсылкаНаСправочник).ЗначениеСтрокой;
                                    }
                                    else
                                    {
                                        e.Style.Text = ячейки.Значение.ToString();
                                    }
                                }
                                else if (e.Style.CellValue == null)
                                {
                                    ячейки.Значение = e.Style.CellValue;
                                }
                                if (this.ПослеУстановкиЗначенияЯчейкиСправочника != null)
                                {
                                    string str2 = string.Empty;
                                    string str3 = string.Empty;
                                    ЯчейкаМетаструктуры.ПолучитьКоординатыЯчейки(str, out str2, out str3);
                                    this.ПослеУстановкиЗначенияЯчейкиСправочника(this, new АргументыПослеУстановкиЗначенияЯчейки(this.ТаблицаДанных, str2, str3));
                                }
                            }
                            else
                            {
                                ячейки.Значение = e.Style.Text;
                            }
                            this.ЭкраннаяФорма.ДанныеИзменились = true;
                            if (this.ПриИзмененииДанных != null)
                            {
                                this.ПриИзмененииДанных(this);
                            }
                            if (ячейки.ТолькоЧтение)
                            {
                                this.УстановитьСтильЯчейкиТолькоЧтение(style);
                            }
                            else if (ячейки.Описание.ОбязательноДляЗаполнения)
                            {
                                this.УстановитьСтильОбязательнойДляЗаполненияЯчейки(style, ячейки.ЗначениеЗаполнено);
                            }
                            else if (ячейки.НеВидимая)
                            {
                                this.УстановитьСтильЯчейкиНеВидимая(style);
                            }
                            else if ((this.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (this.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФормаБезРедактирования))
                            {
                                this.УстановитьСтильНередактируемойЯчейки(style);
                            }
                            else
                            {
                                this.УстановитьСтильРедактируемойЯчейки(style);
                            }
                            style.EndUpdate();
                        }
                    }
                }
            }
        }

        private object ВыборИзСправочника_ПолучениеИсточникаЗаписейДляВыбора(АргументыОтменяемогоСобытия Аргументы)
        {
            if (((this.ЭкраннаяФорма == null) || (this.ЭкраннаяФорма.ОтчетнаяФорма == null)) || (this.CurrentCell == null))
            {
                return null;
            }
            string str = "";
            string str2 = "";
            ОписаниеСсылкиНаСправочник справочник = null;
            try
            {
                GridStyleInfo info = base[this.CurrentCell.RowIndex, this.CurrentCell.ColIndex];
                if (info == null)
                {
                    return null;
                }
                string key = info.Tag.ToString().Replace("$", "");
                string[] strArray = key.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length == 0)
                {
                    return null;
                }
                ТипЯчейки ячейки = null;
                ЯчейкаМетаструктуры метаструктуры = null;
                if (this.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура.СвободныеЯчейки.ContainsKey(key))
                {
                    метаструктуры = this.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура.СвободныеЯчейки[key];
                    if (!метаструктуры.ТипЗначения.IsSubclassOf(typeof(СсылочныйТип)))
                    {
                        return null;
                    }
                    справочник = new ОписаниеСсылкиНаСправочник(метаструктуры.Описание);
                }
                else if (this.ТаблицаДанных != null)
                {
                    ячейки = this.ТаблицаДанных[key];
                    if ((ячейки == null) && !ячейки.GetType().IsSubclassOf(typeof(СсылочныйТип)))
                    {
                        return null;
                    }
                    справочник = new ОписаниеСсылкиНаСправочник(ячейки.ОписаниеСтрокой);
                }
                if (!((справочник != null) && справочник.ДополнительнаяОбработкаПолученияИсточникаЗаписей))
                {
                    return null;
                }
                str2 = strArray[0];
                if (strArray.Length >= 2)
                {
                    str = strArray[1];
                }
                АргументыСобытияПолученияИсточникаЗаписейДляВыбораИзСправочника справочника = new АргументыСобытияПолученияИсточникаЗаписейДляВыбораИзСправочника();
                справочника.ТаблицаДанных = this.ТаблицаДанных;
                справочника.ОписаниеСсылки = справочник;
                справочника.КодСтолбца = str2;
                справочника.КодСтроки = str;
                справочника.ОтменитьВыбор = Аргументы.ОтменитьДействие;
                object obj2 = null;
                try
                {
                    obj2 = this.ЭкраннаяФорма.ОтчетнаяФорма.ВыполнитьМетодМакроса("ПолучитьИсточникЗаписейДляВыбораИзСправочника", new object[] { справочника });
                }
                catch (Exception exception)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию("Ошибка получения источника записей для выбора из справочника.", exception);
                    obj2 = null;
                    справочника.ОтменитьВыбор = true;
                }
                Аргументы.ОтменитьДействие = справочника.ОтменитьВыбор;
                return obj2;
            }
            catch
            {
                return null;
            }
        }

        public override void ЗагрузитьИзXML(XPathNavigator документ)
        {
            base.ЗагрузитьИзXML(документ);
            base.Rows.Hidden[0] = true;
            base.Cols.Hidden[0] = true;
            if (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.РедакторУвязок)
            {
                base.ResizeColsBehavior = GridResizeCellsBehavior.None;
                base.ResizeRowsBehavior = GridResizeCellsBehavior.None;
            }
        }

        private void ЗаполнитьСлучайнымиЗначениями()
        {
            int num = 1;
            for (int i = 1; i <= base.RowCount; i++)
            {
                for (int j = 1; j <= base.ColCount; j++)
                {
                    GridStyleInfo info = base[i, j];
                    if (((info.CellType != "FormulaCell") && !(info.CellType == "ВыборИзСправочника")) && ((info.Tag != null) && (info.CellType != "TextBox")))
                    {
                        string str = info.Tag.ToString();
                        ТипЯчейки ячейки = this.ТаблицаДанных[str];
                        if (!((ячейки == null) || ячейки.ТолькоЧтение))
                        {
                            info.CellValue = num++;
                        }
                    }
                }
            }
            this.Update();
        }

        private void контекстноеМеню_ПриОткрытии(object Отправитель, АргументыОтменяемогоСобытия Аргументы)
        {
            if (((this.ЭкраннаяФорма == null) || (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Разработка)) || (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.РедакторУвязок))
            {
                Аргументы.ОтменитьДействие = true;
            }
            else if (((base.CurrentCellInfo == null) || (base.CurrentCellInfo.CellView == null)) || (base.CurrentCellInfo.CellView.CurrentStyle == null))
            {
                this.подменюИсторияСборки.Enabled = false;
            }
            else if (!(base.CurrentCellInfo.CellView.CurrentStyle.HasCellType || base.CurrentCellInfo.CellView.CurrentStyle.HasCellValueType))
            {
                Аргументы.ОтменитьДействие = true;
            }
            else
            {
                this.подменюИсторияСборки.Enabled = (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) && (base.CurrentCellInfo.CellView.CurrentStyle.CellType != "PushButton");
            }
        }

        public virtual void ОбновитьЗначения()
        {
            if (this.CurrentCell.IsEditing)
            {
                this.CurrentCell.EndEdit();
                this.CurrentCell.Deactivate(false);
            }
            base.BeginUpdate();
            base.IgnoreReadOnly = true;
            for (int i = 1; i <= base.RowCount; i++)
            {
                for (int j = 1; j <= base.ColCount; j++)
                {
                    GridStyleInfo info = base[i, j];
                    if ((info.CellType != "FormulaCell") && (((info.Tag != null) && (this.ТаблицаМетаструктуры != null)) && (info.CellType != "TextBox")))
                    {
                        string str = info.Tag.ToString();
                        ТипЯчейки ячейки = this.ТаблицаДанных[str];
                        if (ячейки != null)
                        {
                            info.BeginUpdate();
                            if (ячейки is СсылочныйТип)
                            {
                                info.CellValue = (ячейки as СсылочныйТип).ЗначениеСтрокой;
                            }
                            else
                            {
                                info.CellValue = ячейки.Значение;
                            }
                            info.EndUpdate();
                        }
                    }
                }
            }
            base.IgnoreReadOnly = false;
            base.EndUpdate();
        }

        public void ОбработатьНажатиеКнопкиВТаблице(GridStyleInfo Style)
        {
            if (this.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.Разработка)
            {
                if (!((Style.Tag != null) && (Style.Tag is ПараметрыКнопкиТаблицы)))
                {
                    Сообщение.ПоказатьОшибку("Не указаны параметры запуска.");
                }
                else
                {
                    ПараметрыКнопкиТаблицы tag = (ПараметрыКнопкиТаблицы) Style.Tag;
                    if (tag.ТипОперации == ПараметрыКнопкиТаблицы.ТипОперацииКнопкиТаблицы.НеОпределено)
                    {
                        Сообщение.ПоказатьПредупреждение("Не определена операция кнопки.");
                    }
                    else
                    {
                        if (tag.ТипОперации == ПараметрыКнопкиТаблицы.ТипОперацииКнопкиТаблицы.ОткрытиеТаблицы)
                        {
                            this.ОтобразитьСубТаблицу(tag.Операция);
                        }
                        else if (tag.ТипОперации == ПараметрыКнопкиТаблицы.ТипОперацииКнопкиТаблицы.ВызовМакроса)
                        {
                            this.ЭкраннаяФорма.ОтчетнаяФорма.ВыполнитьМетодМакроса(tag.Операция, tag.МассивПараметров());
                        }
                        if (!tag.СодержитУтверждение("НеОбновлятьДанные") && (((this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) || (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.ПредварительныйПросмотр)) && (this.ЭкраннаяФорма.ВариантОткрытия != ВариантОткрытияФормы.Чтение)))
                        {
                            this.ЭкраннаяФорма.ОбновитьДанные();
                        }
                        if (!tag.СодержитУтверждение("НеПересчитыватьАвтоблоки") && (this.ЭкраннаяФорма.ВариантОткрытия != ВариантОткрытияФормы.Чтение))
                        {
                            this.ЭкраннаяФорма.ПересчитатьАвтоблоки();
                        }
                    }
                }
            }
        }

        public void ОтобразитьСубТаблицу(string КодТаблицы)
        {
            if (this.ЭкраннаяФорма.ДинамическиеТаблицы.ContainsKey(КодТаблицы))
            {
                ФормаДинамическойТаблицы таблицы = new ФормаДинамическойТаблицы();
                ДинамическаяТаблица таблица = this.ЭкраннаяФорма.ДинамическиеТаблицы[КодТаблицы];
                if ((таблицы.ПоказатьДиалог(таблица) == DialogResult.OK) && this.ЭкраннаяФорма.ДинамическиеТаблицы[КодТаблицы].ЗначенияИзменились)
                {
                    this.ЭкраннаяФорма.ДанныеИзменились = true;
                }
            }
        }

        public void ПересчитатьАвтоблоки()
        {
            GridFormulaCellModel model = (GridFormulaCellModel) base.Представление.CellModels["FormulaCell"];
            model.Engine.RecalculateRange(GridRangeInfo.Table(), true);
        }

        public void ПроанализироватьЗапретыДоступаКЭлементамФормы(Dictionary<string, string> СписокОграничений)
        {
            base.BeginUpdate();
            if (СписокОграничений.ContainsKey("Таблица"))
            {
                this.УстановитьОграничениеНаТаблицу(СписокОграничений);
            }
            else
            {
                foreach (string str in СписокОграничений.Keys)
                {
                    string[] strArray = str.Split(new char[] { ':' });
                    if (strArray.Length == 1)
                    {
                        this.УстановитьОграничениеНаСвободнуюЯчейку(strArray[0], СписокОграничений[str]);
                    }
                    else if (strArray.Length == 2)
                    {
                        if (!(!string.IsNullOrEmpty(strArray[1]) || string.IsNullOrEmpty(strArray[0])))
                        {
                            this.УстановитьОграничениеНаСтолбец(strArray[0], СписокОграничений[str]);
                        }
                        if (!(!string.IsNullOrEmpty(strArray[0]) || string.IsNullOrEmpty(strArray[1])))
                        {
                            this.УстановитьОграничениеНаСтроку(strArray[1], СписокОграничений[str]);
                        }
                        if (!(string.IsNullOrEmpty(strArray[0]) || string.IsNullOrEmpty(strArray[1])))
                        {
                            this.УстановитьОграничениеНаЯчейку(strArray[0], strArray[1], СписокОграничений[str]);
                        }
                    }
                }
            }
            base.EndUpdate();
        }

        protected override string ПроанализироватьСсылкиНаКонстанты(string ИсходноеЗначение)
        {
            if (this.ЭкраннаяФорма == null)
            {
                return ИсходноеЗначение;
            }
            return this.ЭкраннаяФорма.ПроанализироватьСсылкиНаКонстанты(ИсходноеЗначение);
        }

        protected override void ПроанализироватьЯчейку(ЯчейкаТаблицыОтчетнойФормы Ячейка, int Row, int Col)
        {
            base.ПроанализироватьЯчейку(Ячейка, Row, Col);
            GridStyleInfo info = base[Row, Col];
            info.BeginUpdate();
            info.Tag = Ячейка.Переменная;
            if (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.РедакторУвязок)
            {
                if (!(string.IsNullOrEmpty(Ячейка.Переменная) || !Ячейка.Переменная.StartsWith("$")))
                {
                    info.CellValue = Ячейка.Переменная;
                    this.УстановитьСтильАвтоблока(info);
                }
            }
            else
            {
                ТипЯчейки ячейки = null;
                if (!(string.IsNullOrEmpty(Ячейка.Переменная) || (this.ТаблицаДанных == null)))
                {
                    ячейки = this.ТаблицаДанных[Ячейка.Переменная];
                }
                if (((ячейки != null) && (ячейки.Описание != null)) && ячейки.Описание.ОбязательноДляЗаполнения)
                {
                    string[] strArray = Ячейка.Переменная.Replace("$", "").Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArray.Length > 0)
                    {
                        if (this.ТаблицаДанных.СвободныеЯчейки.СодержитИндекс(strArray[0]))
                        {
                            this.словарьОбязательныхДляЗаполненияЯчеек.Add(strArray[0], ячейки);
                        }
                        else if (!this.списокОбязательныхДляЗаполненияСтолбцов.Contains(strArray[0]))
                        {
                            this.списокОбязательныхДляЗаполненияСтолбцов.Add(strArray[0]);
                        }
                    }
                }
                else if (((ячейки != null) && (ячейки.Описание != null)) && ячейки.ТолькоЧтение)
                {
                    this.УстановитьСтильЯчейкиТолькоЧтение(info);
                }
                if (Ячейка.Формула)
                {
                    this.УстановитьСтильАвтоблока(info);
                }
                else if (!string.IsNullOrEmpty(Ячейка.Значение))
                {
                    if (Ячейка.Значение.StartsWith("@"))
                    {
                        this.УстановитьСтильКнопки(info, Ячейка.Значение);
                    }
                    else if (ячейки != null)
                    {
                        ячейки.УстановитьЗначениеПоУмолчанию();
                        if (ячейки.Описание.ОбязательноДляЗаполнения)
                        {
                            this.УстановитьСтильОбязательнойДляЗаполненияЯчейки(info, false);
                        }
                    }
                    info.EndUpdate();
                    return;
                }
                if (Ячейка.Переменная != null)
                {
                    info.Tag = Ячейка.Переменная;
                    if (this.ТаблицаМетаструктуры != null)
                    {
                        ЯчейкаМетаструктуры метаструктуры = this.ТаблицаМетаструктуры[Ячейка.Переменная];
                        if (метаструктуры != null)
                        {
                            this.УстановитьТипЯчейки(info, метаструктуры.ТипЗначения, метаструктуры.Описание);
                            if (!Ячейка.Формула)
                            {
                                ячейки = this.ТаблицаДанных[Ячейка.Переменная];
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
                                if (((this.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (this.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФормаБезРедактирования)) || (метаструктуры.ТипЯчейки != ТипЯчейкиМетаописания.Обычная))
                                {
                                    this.УстановитьСтильНередактируемойЯчейки(info);
                                }
                                else
                                {
                                    this.УстановитьСтильРедактируемойЯчейки(info);
                                }
                                if (ячейки.ТолькоЧтение)
                                {
                                    this.УстановитьСтильЯчейкиТолькоЧтение(info);
                                }
                                else if (ячейки.Описание.ОбязательноДляЗаполнения)
                                {
                                    this.УстановитьСтильОбязательнойДляЗаполненияЯчейки(info, ячейки.ЗначениеЗаполнено);
                                }
                            }
                        }
                    }
                }
            }
            info.EndUpdate();
        }

        public bool ПроверитьЗаполненностьДанных()
        {
            Dictionary<string, List<string>> dictionary = null;
            bool flag = this.ПроверитьЗаполненностьДанных(out dictionary);
            dictionary = null;
            return flag;
        }

        public virtual bool ПроверитьЗаполненностьДанных(out Dictionary<string, List<string>> КоординатыНезаполненныхЯчеек)
        {
            КоординатыНезаполненныхЯчеек = new Dictionary<string, List<string>>();
            if ((this.списокОбязательныхДляЗаполненияСтолбцов.Count == 0) && (this.словарьОбязательныхДляЗаполненияЯчеек.Count == 0))
            {
                return true;
            }
            for (int i = 0; i < base.RowCount; i++)
            {
                for (int j = 0; j < base.ColCount; j++)
                {
                    GridStyleInfo info = base[i, j];
                    if ((((info != null) && (info.CellType != "FormulaCell")) && (info.Tag is string)) && !string.IsNullOrEmpty(info.Tag.ToString()))
                    {
                        object cellValue = info.CellValue;
                        object obj3 = null;
                        bool flag = false;
                        string str = info.Tag.ToString().Replace("$", "");
                        string[] strArray = str.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                        ТипЯчейки ячейки = null;
                        if (strArray.Length > 0)
                        {
                            if (this.списокОбязательныхДляЗаполненияСтолбцов.Contains(strArray[0]))
                            {
                                ячейки = this.ТаблицаДанных[str];
                            }
                            else if (this.словарьОбязательныхДляЗаполненияЯчеек.ContainsKey(strArray[0]))
                            {
                                ячейки = this.словарьОбязательныхДляЗаполненияЯчеек[strArray[0]];
                                flag = true;
                            }
                        }
                        if ((ячейки != null) && ячейки.Описание.ОбязательноДляЗаполнения)
                        {
                            List<string> list = null;
                            obj3 = ячейки.ПолучитьЗначениеПоУмолчанию();
                            if (!ячейки.ЗначениеЗаполнено)
                            {
                                if (flag)
                                {
                                    if (!КоординатыНезаполненныхЯчеек.TryGetValue("!Свободные ячейки", out list))
                                    {
                                        list = new List<string>();
                                        КоординатыНезаполненныхЯчеек.Add("!Свободные ячейки", list);
                                    }
                                    list.Add(strArray[0]);
                                }
                                else
                                {
                                    if (!КоординатыНезаполненныхЯчеек.TryGetValue(strArray[0], out list))
                                    {
                                        list = new List<string>();
                                        КоординатыНезаполненныхЯчеек.Add(strArray[0], list);
                                    }
                                    list.Add(strArray[1]);
                                }
                            }
                            list = null;
                        }
                    }
                }
            }
            return (КоординатыНезаполненныхЯчеек.Count == 0);
        }

        public virtual bool ПроверитьЗаполненностьДанных(out string Сообщение)
        {
            Сообщение = "";
            Dictionary<string, List<string>> dictionary = null;
            bool flag = this.ПроверитьЗаполненностьДанных(out dictionary);
            if (!flag)
            {
                foreach (KeyValuePair<string, List<string>> pair in dictionary)
                {
                    bool flag2 = pair.Key.StartsWith("!Свободные");
                    Сообщение = Сообщение + (string.IsNullOrEmpty(Сообщение) ? "" : "\r\n") + (flag2 ? pair.Key.Substring(1) : ("Столбец " + pair.Key));
                    foreach (string str in pair.Value)
                    {
                        Сообщение = Сообщение + "\r\n\t" + (flag2 ? "Код ячейки : " : "Строка ") + str.ToString();
                    }
                    Сообщение = Сообщение + "\r\n";
                }
            }
            return flag;
        }

        private void ПроинициализироватьРедакторыЯчеек()
        {
            this.параметрыСозданияРедактора_ВыборИзСправочника.РедактированиеРазрешено = this.параметрыСозданияРедактора_ВыборИзСправочника.РедактированиеРазрешено = (this.ЭкраннаяФорма != null) && (((this.ВариантОткрытия != ВариантОткрытияФормы.Чтение) && (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)) || ((this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.ПредварительныйПросмотр) && МенеджерБД.МенеджерИнициализирован));
            if (!base.CellModels.ContainsKey("ВыборИзСправочника"))
            {
                base.CellModels.Add("ВыборИзСправочника", new РедакторЯчеек_ВыборИзСправочника(this.параметрыСозданияРедактора_ВыборИзСправочника));
            }
            else
            {
                base.CellModels["ВыборИзСправочника"] = new РедакторЯчеек_ВыборИзСправочника(this.параметрыСозданияРедактора_ВыборИзСправочника);
            }
            if (!base.CellModels.ContainsKey("МножественныйВыборИзСправочника"))
            {
                base.CellModels.Add("МножественныйВыборИзСправочника", new РедакторЯчеек_ВыборИзСправочника(this.параметрыСозданияРедактора_ВыборИзСправочника));
            }
            else
            {
                base.CellModels["МножественныйВыборИзСправочника"] = new РедакторЯчеек_ВыборИзСправочника(this.параметрыСозданияРедактора_ВыборИзСправочника);
            }
            if (!base.CellModels.ContainsKey("ВыборДаты"))
            {
                base.CellModels.Add("ВыборДаты", new РедакторЯчеек_ВыборДаты(base.Представление));
            }
            else
            {
                base.CellModels["ВыборДаты"] = new РедакторЯчеек_ВыборДаты(base.Представление);
            }
            if (!base.CellModels.ContainsKey("РедакторВремени"))
            {
                base.CellModels.Add("РедакторВремени", new РедакторЯчеек_РедакторВремени(base.Представление));
            }
            else
            {
                base.CellModels["РедакторВремени"] = new РедакторЯчеек_РедакторВремени(base.Представление);
            }
            if (!base.CellModels.ContainsKey("МногострочныйРедактор"))
            {
                base.CellModels.Add("МногострочныйРедактор", new РедакторЯчеек_МногострочныйРедактор(base.Представление));
            }
            else
            {
                base.CellModels["МногострочныйРедактор"] = new РедакторЯчеек_МногострочныйРедактор(base.Представление);
            }
            if (!base.CellModels.ContainsKey("ТекстСМаской"))
            {
                base.CellModels.Add("ТекстСМаской", new РедакторЯчеек_ТекстСМаской(base.Представление));
            }
            else
            {
                base.CellModels["ТекстСМаской"] = new РедакторЯчеек_ТекстСМаской(base.Представление);
            }
            if (!base.CellModels.ContainsKey("РедакторИзображения"))
            {
                base.CellModels.Add("РедакторИзображения", new РедакторЯчеек_РедакторИзображения(base.Представление));
            }
            else
            {
                base.CellModels["РедакторИзображения"] = new РедакторЯчеек_РедакторИзображения(base.Представление);
            }
        }

        private void ТаблицаОтчетнойФормы_CellButtonClicked(object sender, GridCellButtonClickedEventArgs e)
        {
            GridStyleInfo style = base[e.RowIndex, e.ColIndex];
            this.ОбработатьНажатиеКнопкиВТаблице(style);
        }

        private void ТаблицаОтчетнойФормы_ColsHiding(object sender, GridRowColHidingEventArgs e)
        {
            e.Cancel = false;
        }

        private void ТаблицаОтчетнойФормы_CurrentCellActivated(object sender, EventArgs e)
        {
            if (this.ЭкраннаяФорма != null)
            {
                this.ЭкраннаяФорма.ОтобразитьПодсказку("");
            }
            int rowIndex = this.CurrentCell.RowIndex;
            int colIndex = this.CurrentCell.ColIndex;
            GridStyleInfo info = base[rowIndex, colIndex];
            string str = "";
            if ((info != null) && (info.Tag != null))
            {
                if (info.Tag is string)
                {
                    string str2 = info.Tag.ToString();
                    string text = string.Empty;
                    if (this.ТаблицаДанных != null)
                    {
                        ТипЯчейки ячейки = this.ТаблицаДанных[str2];
                        if (ячейки != null)
                        {
                            if (ячейки.Значение == null)
                            {
                                text = "";
                            }
                            else
                            {
                                text = ячейки.ToXmlString();
                                if (ячейки is ТипДатаВремя)
                                {
                                    ОписаниеТипаДатаВремя время = ячейки.Описание as ОписаниеТипаДатаВремя;
                                    if ((время != null) && время.РедакторВремени)
                                    {
                                        text = ячейки.ЗначениеДатаВремя.ToShortTimeString();
                                    }
                                }
                            }
                        }
                        if (info.CellType == "FormulaCell")
                        {
                            if ((info.FormulaTag == null) || string.IsNullOrEmpty(info.FormulaTag.Text))
                            {
                                text = "";
                            }
                            else
                            {
                                text = info.FormulaTag.Text;
                            }
                            str = string.Format("Таблица: {0} Ячейка : {1} Значение : {2} Автоблок : {3}", new object[] { this.ТаблицаДанных.КодТаблицы, str2, text, info.CellValue.ToString() });
                        }
                        else
                        {
                            str = string.Format("Таблица: {0} Ячейка : {1} Значение : {2}", this.ТаблицаДанных.КодТаблицы, str2, text);
                        }
                        if (((ячейки != null) && (ячейки.Описание != null)) && ячейки.Описание.ОбязательноДляЗаполнения)
                        {
                            str = str + " Обязательна для заполнения";
                        }
                        if (!((ячейки == null) || string.IsNullOrEmpty(ячейки.Описание.Комментарий)))
                        {
                            str = str + string.Format(" Комментарий : {0}", ячейки.Описание.Комментарий);
                        }
                        if (((ячейки != null) && (ячейки.Описание is ОписаниеСтроковогоТипаЯчейки)) && !string.IsNullOrEmpty((ячейки.Описание as ОписаниеСтроковогоТипаЯчейки).МаскаВвода))
                        {
                            str = str + " Маска заполнения : " + (ячейки.Описание as ОписаниеСтроковогоТипаЯчейки).МаскаВвода;
                        }
                    }
                }
                else if (info.Tag is ПараметрыКнопкиТаблицы)
                {
                    ПараметрыКнопкиТаблицы tag = (ПараметрыКнопкиТаблицы) info.Tag;
                    str = "";
                    if (tag.ТипОперации == ПараметрыКнопкиТаблицы.ТипОперацииКнопкиТаблицы.ВызовМакроса)
                    {
                        str = "Вызов макроса <" + tag.Операция + ">";
                    }
                    else if (tag.ТипОперации == ПараметрыКнопкиТаблицы.ТипОперацииКнопкиТаблицы.ОткрытиеТаблицы)
                    {
                        str = "Открытие таблицы <" + tag.Операция + ">";
                    }
                }
            }
            this.ЭкраннаяФорма.ОтобразитьПодсказку(str);
        }

        private void ТаблицаОтчетнойФормы_CurrentCellKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode != Keys.Return) || !e.Control)
            {
                if ((e.KeyCode == Keys.Delete) && e.Control)
                {
                    e.Handled = true;
                }
                else if ((e.KeyCode == Keys.Space) || (e.KeyCode == Keys.Return))
                {
                    if (base.CurrentCellInfo.CellView.CurrentStyle.CellType == "PushButton")
                    {
                        this.ОбработатьНажатиеКнопкиВТаблице(base.CurrentCellInfo.CellView.CurrentStyle);
                    }
                }
                else if (((((e.KeyCode == Keys.R) && e.Alt) && e.Control) && e.Shift) && (this.ЭкраннаяФорма.ВариантОткрытия != ВариантОткрытияФормы.Чтение))
                {
                    this.ЗаполнитьСлучайнымиЗначениями();
                }
            }
        }

        private void ТаблицаОтчетнойФормы_CurrentCellStartEditing(object sender, CancelEventArgs e)
        {
            GridStyleInfo info = base[this.CurrentCell.RowIndex, this.CurrentCell.ColIndex];
            if (info.ReadOnly)
            {
                e.Cancel = true;
            }
        }

        private void ТаблицаОтчетнойФормы_RowsHiding(object sender, GridRowColHidingEventArgs e)
        {
            e.Cancel = false;
        }

        private void УстановитьОграничениеНаСвободнуюЯчейку(string КодСвободнойЯчейки, string Ограничение)
        {
            if (!this.словарьОбязательныхДляЗаполненияЯчеек.ContainsKey(КодСвободнойЯчейки))
            {
                for (int i = 0; i <= base.RowCount; i++)
                {
                    for (int j = 0; j <= base.ColCount; j++)
                    {
                        if ((base[i, j].Tag != null) && base[i, j].Tag.ToString().Contains(КодСвободнойЯчейки))
                        {
                            GridStyleInfo info = base[i, j];
                            info.BeginUpdate();
                            if (Ограничение == "ЗапретПросмотра")
                            {
                                this.УстановитьСтильЯчейкиНеВидимая(info);
                            }
                            else
                            {
                                this.УстановитьСтильЯчейкиТолькоЧтение(info);
                            }
                            info.EndUpdate();
                            return;
                        }
                    }
                }
            }
        }

        private void УстановитьОграничениеНаСтолбец(string КодСтолбца, string Ограничение)
        {
            if (!this.списокОбязательныхДляЗаполненияСтолбцов.Contains(КодСтолбца))
            {
                bool flag = false;
                for (int i = 0; i <= base.RowCount; i++)
                {
                    if (!flag)
                    {
                        for (int j = 0; j <= base.ColCount; j++)
                        {
                            GridStyleInfo info = base[i, j];
                            if ((info.Tag != null) && info.Tag.ToString().Contains(КодСтолбца + ":"))
                            {
                                info.BeginUpdate();
                                if (Ограничение == "ЗапретПросмотра")
                                {
                                    base.ColHiddenEntries.Add(new GridColHidden(j));
                                }
                                this.УстановитьСтильЯчейкиТолькоЧтение(info);
                                info.EndUpdate();
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void УстановитьОграничениеНаСтроку(string КодСтроки, string Ограничение)
        {
            bool flag = false;
            for (int i = 0; i <= base.RowCount; i++)
            {
                for (int j = 0; j <= base.ColCount; j++)
                {
                    if ((base[i, j].Tag != null) && base[i, j].Tag.ToString().Contains(":" + КодСтроки))
                    {
                        flag = true;
                        GridStyleInfo info = base[i, j];
                        info.BeginUpdate();
                        if (Ограничение == "ЗапретПросмотра")
                        {
                            base.RowHeights[i] = 0;
                        }
                        this.УстановитьСтильЯчейкиТолькоЧтение(info);
                        info.EndUpdate();
                    }
                }
                if (flag)
                {
                    break;
                }
            }
        }

        private void УстановитьОграничениеНаТаблицу(Dictionary<string, string> СписокОграничений)
        {
            if (СписокОграничений["Таблица"] == "ЗапретРедактирования")
            {
                for (int i = 0; i < base.RowCount; i++)
                {
                    for (int j = 0; j <= base.ColCount; j++)
                    {
                        if (base[i, j].Tag != null)
                        {
                            GridStyleInfo info = base[i, j];
                            info.BeginUpdate();
                            this.УстановитьСтильЯчейкиТолькоЧтение(info);
                            info.EndUpdate();
                        }
                    }
                }
            }
            else if (СписокОграничений["Таблица"] == "ЗапретПросмотра")
            {
                base.Hide();
                base.ReadOnly = true;
            }
        }

        private void УстановитьОграничениеНаЯчейку(string Столбец, string Строка, string Ограничение)
        {
            if (!this.списокОбязательныхДляЗаполненияСтолбцов.Contains(Столбец))
            {
                string str = Столбец + ":" + Строка;
                for (int i = 0; i <= base.RowCount; i++)
                {
                    for (int j = 0; j <= base.ColCount; j++)
                    {
                        if ((base[i, j].Tag != null) && base[i, j].Tag.ToString().Contains(str))
                        {
                            GridStyleInfo info = base[i, j];
                            info.BeginUpdate();
                            if (Ограничение == "ЗапретПросмотра")
                            {
                                this.УстановитьСтильЯчейкиНеВидимая(info);
                            }
                            else
                            {
                                this.УстановитьСтильЯчейкиТолькоЧтение(info);
                            }
                            info.EndUpdate();
                            return;
                        }
                    }
                }
            }
        }

        protected virtual void УстановитьСтандартныйСтильЯчейки()
        {
            GridStyleInfo info = base.СтандартныйСтиль;
            info.FloatCell = true;
            info.AllowEnter = false;
            info.Clickable = false;
            info.Enabled = true;
            info.ReadOnly = true;
            info.Font.Facename = "Tahoma";
            info.Font.Bold = false;
        }

        public void УстановитьСтилевыеНастройкиТаблицы()
        {
            base.DefaultGridBorderStyle = GridBorderStyle.None;
            base.AllowSelection = GridSelectionFlags.Keyboard;
            base.Properties.ColHeaders = true;
            base.Properties.RowHeaders = true;
            base.Rows.Hidden[0] = true;
            base.Cols.Hidden[0] = true;
            this.ForeColor = Color.MidnightBlue;
            base.Properties.BackgroundColor = Color.White;
            this.VerticalThumbTrack = true;
            this.HorizontalThumbTrack = true;
            base.BorderStyle = BorderStyle.Fixed3D;
            base.ResizeColsBehavior = GridResizeCellsBehavior.InsideGrid | GridResizeCellsBehavior.ResizeSingle;
            base.ResizeRowsBehavior = GridResizeCellsBehavior.InsideGrid | GridResizeCellsBehavior.ResizeSingle;
            base.SupportsPrepareViewStyleInfo = false;
            base.OptimizeDrawBackground = true;
            this.OptimizeInsertRemoveCells = true;
            base.EnterKeyBehavior = GridDirectionType.Right;
            base.UseRightToLeftCompatibleTextBox = true;
            base.SerializeCellsBehavior = GridSerializeCellsBehavior.SerializeAsRangeStylesIntoCode;
            base.ActivateCurrentCellBehavior = GridCellActivateAction.SelectAll;
            base.ClickedOnDisabledCellBehavior = GridClickedOnDisabledCellBehavior.DeactivateCurrentCell;
            base.ControllerOptions = GridControllerOptions.All;
            base.DragSelectedCellsMouseButtonsMask = MouseButtons.None;
            base.FloatCellsMode = GridFloatCellsMode.None;
            base.RefreshCurrentCellBehavior = GridRefreshCurrentCellBehavior.RefreshCell;
            base.ForceCurrentCellMoveTo = true;
            base.WantEscapeKey = false;
            GridFormulaCellModel model = (GridFormulaCellModel) base.Представление.CellModels["FormulaCell"];
            model.Engine.DoCircularCheckInValidating = true;
        }

        protected void УстановитьСтильАвтоблока(GridStyleInfo Стиль)
        {
            Стиль.Enabled = true;
            Стиль.BackColor = Color.FromArgb(210, 220, 0xff);
            Стиль.Format = "N2";
            Стиль.HorizontalAlignment = GridHorizontalAlignment.Right;
        }

        protected void УстановитьСтильКнопки(GridStyleInfo Стиль, string ТекстЯчейки)
        {
            Стиль.Enabled = true;
            Стиль.Clickable = true;
            Стиль.CellType = "PushButton";
            Стиль.Description = "Строки >>";
            string str = ТекстЯчейки.Trim();
            string key = "";
            if (str.IndexOf('{') != -1)
            {
                key = str.Substring(1, str.IndexOf('{') - 1).Trim();
            }
            else
            {
                key = str.Substring(1).Trim();
            }
            ПараметрыКнопкиТаблицы.ТипОперацииКнопкиТаблицы таблицы = ПараметрыКнопкиТаблицы.ТипОперацииКнопкиТаблицы.НеОпределено;
            if ((((this.ЭкраннаяФорма != null) && (this.ЭкраннаяФорма.ОтчетнаяФорма != null)) && (this.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура != null)) && this.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура.Таблицы.ContainsKey(key))
            {
                таблицы = ПараметрыКнопкиТаблицы.ТипОперацииКнопкиТаблицы.ОткрытиеТаблицы;
            }
            else
            {
                таблицы = ПараметрыКнопкиТаблицы.ТипОперацииКнопкиТаблицы.ВызовМакроса;
            }
            ПараметрыКнопкиТаблицы таблицы2 = new ПараметрыКнопкиТаблицы(key, таблицы);
            if (str.IndexOf('{') != -1)
            {
                string[] strArray = str.Substring(str.IndexOf('{') + 1, (str.Length - str.IndexOf('{')) - 2).Trim().Split(new char[] { ';' });
                foreach (string str3 in strArray)
                {
                    if (str3.IndexOf('=') == -1)
                    {
                        goto Label_01FE;
                    }
                    string[] strArray2 = str3.Split(new char[] { '=' });
                    string str4 = strArray2[0].Trim().ToLower();
                    string str5 = "";
                    if (strArray2.Length > 1)
                    {
                        str5 = strArray2[1];
                    }
                    string str6 = str4;
                    if (str6 == null)
                    {
                        goto Label_01ED;
                    }
                    if (!(str6 == "заголовок"))
                    {
                        if (str6 == "фильтр")
                        {
                            goto Label_01D5;
                        }
                        if (str6 == "параметрывызова")
                        {
                            goto Label_01E1;
                        }
                        goto Label_01ED;
                    }
                    Стиль.Description = str5;
                    goto Label_020A;
                Label_01D5:
                    таблицы2.Фильтр = str5;
                    goto Label_020A;
                Label_01E1:
                    таблицы2.ДобавитьПараметрыИзСтроки(str5);
                    goto Label_020A;
                Label_01ED:
                    таблицы2.ДобавитьИменованыйПараметр(str4, str5);
                    goto Label_020A;
                Label_01FE:
                    таблицы2.ДобавитьУтверждение(str3);
                Label_020A:;
                }
                Стиль.Tag = таблицы2;
            }
            else
            {
                Стиль.Tag = таблицы2;
            }
        }

        protected void УстановитьСтильНередактируемойЯчейки(GridStyleInfo Стиль)
        {
        }

        protected virtual void УстановитьСтильОбязательнойДляЗаполненияЯчейки(GridStyleInfo Стиль, bool ЯчейкаЗаполнена)
        {
            Стиль.Enabled = true;
            Стиль.ReadOnly = false;
            if (ЯчейкаЗаполнена)
            {
                Стиль.BackColor = Color.FromArgb(0xf9, 0xf5, 0xd3);
            }
            else
            {
                Стиль.BackColor = Color.Yellow;
            }
        }

        protected void УстановитьСтильРедактируемойЯчейки(GridStyleInfo Стиль)
        {
            Стиль.Enabled = true;
            Стиль.ReadOnly = false;
            Стиль.BackColor = Color.FromArgb(0xf9, 0xf5, 0xd3);
        }

        protected virtual void УстановитьСтильЯчейкиНеВидимая(GridStyleInfo Стиль)
        {
            Стиль.Enabled = false;
            Стиль.ReadOnly = true;
            Стиль.BackColor = Color.White;
            Стиль.TextColor = Color.White;
            ТипЯчейки ячейки = this.ТаблицаДанных[Стиль.Tag.ToString()];
            ячейки.НеВидимая = true;
        }

        protected virtual void УстановитьСтильЯчейкиТолькоЧтение(GridStyleInfo Стиль)
        {
            Стиль.Enabled = true;
            Стиль.ReadOnly = true;
            Стиль.BackColor = Color.FromArgb(210, 220, 0xff);
            if (Стиль.Tag != null)
            {
                ТипЯчейки ячейки = this.ТаблицаДанных[Стиль.Tag.ToString()];
                ячейки.ТолькоЧтение = true;
            }
        }

        protected void УстановитьТипЯчейки(GridStyleInfo Style, System.Type ТипЗначения, string Описание)
        {
            ОбщееОписаниеТипаЯчейки ячейки = ОбщееОписаниеТипаЯчейки.ПолучитьОписаниеТипаЯчейки(ТипЗначения, Описание);
            if (ТипЗначения == typeof(ФинансовыйТип))
            {
                Style.Format = "N2";
                if (Style.CellType != "FormulaCell")
                {
                    Style.CellValueType = typeof(decimal);
                    Style.CellType = "Currency";
                    Style.CurrencyEdit.CurrencyDecimalDigits = 2;
                    Style.CurrencyEdit.CurrencySymbol = "";
                }
                Style.HorizontalAlignment = GridHorizontalAlignment.Right;
            }
            else if (ТипЗначения == typeof(СсылкаНаУчреждение))
            {
                Style.CellType = "ВыборИзСправочника";
                Style.BackColor = Color.White;
                Style.Description = "Учреждение";
                Style.Enabled = false;
            }
            else if ((ТипЗначения == typeof(СсылкаНаСправочник)) || (ТипЗначения == typeof(МножественнаяСсылкаНаСправочник)))
            {
                Style.CellType = "ВыборИзСправочника";
                Style.BackColor = Color.White;
                Style.Description = Описание;
                if (ТипЗначения == typeof(МножественнаяСсылкаНаСправочник))
                {
                    Style.CellType = "МножественныйВыборИзСправочника";
                }
            }
            else if (ТипЗначения == typeof(СтроковыйТип))
            {
                ОписаниеСтроковогоТипаЯчейки ячейки2 = null;
                if ((ячейки != null) && (ячейки is ОписаниеСтроковогоТипаЯчейки))
                {
                    ячейки2 = ячейки as ОписаниеСтроковогоТипаЯчейки;
                }
                else
                {
                    ячейки2 = new ОписаниеСтроковогоТипаЯчейки();
                }
                if (!string.IsNullOrEmpty(ячейки2.МаскаВвода))
                {
                    Style.CellType = "ТекстСМаской";
                    Style.BackColor = Color.FromArgb(0xf9, 0xf5, 0xd3);
                    Style.Description = ячейки2.МаскаВвода;
                }
                else if (ячейки2.МногострочныйРедактор)
                {
                    Style.CellType = "МногострочныйРедактор";
                    Style.BackColor = Color.FromArgb(0xf9, 0xf5, 0xd3);
                    Style.Description = Описание;
                }
                Style.HorizontalAlignment = GridHorizontalAlignment.Right;
            }
            else if (ТипЗначения == typeof(ЛогическийТип))
            {
                base.TableStyle.CheckBoxOptions = new GridCheckBoxCellInfo("True", "False", "", false);
                Style.CellType = "CheckBox";
                Style.TriState = false;
                Style.HorizontalAlignment = GridHorizontalAlignment.Center;
                Style.VerticalAlignment = GridVerticalAlignment.Middle;
            }
            else if (ТипЗначения == typeof(ТипДатаВремя))
            {
                Style.CellType = "ВыборДаты";
                Style.BackColor = Color.White;
                if (ячейки is ОписаниеТипаДатаВремя)
                {
                    ОписаниеТипаДатаВремя время = ячейки as ОписаниеТипаДатаВремя;
                    if (время.РедакторВремени)
                    {
                        Style.CellType = "РедакторВремени";
                    }
                    else if (!string.IsNullOrEmpty(время.ФорматОтображения))
                    {
                        Style.Description = время.ФорматОтображения;
                    }
                }
            }
            else if (ТипЗначения == typeof(ТипИзображение))
            {
                Style.CellType = "РедакторИзображения";
                Style.BackColor = Color.White;
            }
            else if (ТипЗначения == typeof(ЦелочисленныйТип))
            {
                Style.Format = "N0";
                if (Style.CellType != "FormulaCell")
                {
                    Style.CellValueType = typeof(int);
                    Style.CellType = "Currency";
                    Style.CurrencyEdit.CurrencyDecimalDigits = 0;
                    Style.CurrencyEdit.CurrencySymbol = "";
                }
                Style.HorizontalAlignment = GridHorizontalAlignment.Right;
            }
            else if (ТипЗначения == typeof(ЧисловойТип))
            {
                int num = 0;
                if ((ячейки != null) && (ячейки is ОписаниеЧисловогоТипа))
                {
                    num = (ячейки as ОписаниеЧисловогоТипа).Точность;
                }
                Style.Format = "N" + num.ToString();
                if (Style.CellType != "FormulaCell")
                {
                    Style.CellValueType = typeof(decimal);
                    Style.CellType = "Currency";
                    Style.CurrencyEdit.CurrencyDecimalDigits = num;
                    Style.CurrencyEdit.CurrencySymbol = "";
                }
                Style.HorizontalAlignment = GridHorizontalAlignment.Right;
            }
        }

        private void элементМенюИстория_ПриНажатии(object Отправитель, АргументыСобытия Аргументы)
        {
            string str = "";
            try
            {
                str = base.CurrentCellInfo.CellView.CurrentStyle.Tag.ToString();
            }
            catch
            {
                str = "";
            }
            if (string.IsNullOrEmpty(str))
            {
                Сообщение.ПоказатьПредупреждение("Невозможно определить текущую ячейку.");
            }
            else
            {
                ТипПостроенияИсторииСборки tag;
                if ((Отправитель is ToolStripMenuItem) && ((Отправитель as ToolStripMenuItem).Tag is ТипПостроенияИсторииСборки))
                {
                    tag = (ТипПостроенияИсторииСборки) (Отправитель as ToolStripMenuItem).Tag;
                }
                else
                {
                    Сообщение.ПоказатьОшибку("Некорректно задан элемент управления.");
                    return;
                }
                if (this.ТаблицаМетаструктуры != null)
                {
                    ЯчейкаМетаструктуры метаструктуры = this.ТаблицаМетаструктуры[str];
                    string str2 = "";
                    string str3 = "";
                    if (метаструктуры != null)
                    {
                        if (метаструктуры.Свободная)
                        {
                            str2 = "СвободныеЯчейки";
                            str3 = метаструктуры.Код;
                        }
                        else
                        {
                            метаструктуры.ПолучитьКоординатыЯчейки(out str2, out str3);
                        }
                        string str4 = "";
                        if (((this.ЭкраннаяФорма != null) && (this.ЭкраннаяФорма.ОтчетнаяФорма != null)) && (this.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура != null))
                        {
                            str4 = this.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура.Код;
                        }
                        ФормаИсторииСборки сборки2 = new ФормаИсторииСборки(str4);
                        сборки2.ТипПостроенияИстории = tag;
                        сборки2.СтолбецПостроения = str3;
                        if (сборки2.Загрузить(this.ЭкраннаяФорма.ОтчетнаяФорма, this.ТаблицаМетаструктуры.Идентификатор, str2) && сборки2.Построить())
                        {
                            сборки2.ShowDialog();
                        }
                        else
                        {
                            Сообщение.Показать("История сборки данного поля отсутствует.");
                        }
                    }
                }
            }
        }

        public ВариантОткрытияФормы ВариантОткрытия
        {
            get
            {
                return this.ЭкраннаяФорма.ВариантОткрытия;
            }
        }

        public virtual ТипЯчейки ВыбраннаяЯчейка
        {
            get
            {
                GridStyleInfo info = this.СтильТекущейЯчейки;
                if (((info != null) && (info.Tag != null)) && (info.Tag is string))
                {
                    string str = info.Tag.ToString().Replace("$", "");
                    if (this.ТаблицаДанных != null)
                    {
                        return this.ТаблицаДанных[str];
                    }
                }
                return null;
            }
        }

        public string Заголовок
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Наименование))
                {
                    return this.Наименование.Replace('_', ' ');
                }
                return base.КодТаблицы;
            }
        }

        public string Наименование
        {
            get
            {
                return this.наименование;
            }
            set
            {
                this.наименование = value;
            }
        }

        public bool РазмещатьНаЗакладке
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        protected GridStyleInfo СтильТекущейЯчейки
        {
            get
            {
                try
                {
                    return base[this.CurrentCell.RowIndex, this.CurrentCell.ColIndex];
                }
                catch
                {
                    return null;
                }
            }
        }

        public Барс.Своды.ОтчетнаяФорма.ТаблицаДанных ТаблицаДанных
        {
            get
            {
                return this.таблицаДанных;
            }
        }

        public Барс.Своды.ОтчетнаяФорма.ТаблицаМетаструктуры ТаблицаМетаструктуры
        {
            get
            {
                return this.таблицаМетаструктуры;
            }
        }

        public Барс.Своды.БраузерОтчетныхФорм.ЭкраннаяФорма ЭкраннаяФорма
        {
            get
            {
                return this.экраннаяФорма;
            }
            set
            {
                this.экраннаяФорма = value;
                if (this.экраннаяФорма != null)
                {
                    this.параметрыСозданияРедактора_ВыборИзСправочника.РедактированиеРазрешено = this.параметрыСозданияРедактора_ВыборИзСправочника.РедактированиеРазрешено = (this.ЭкраннаяФорма != null) && (((this.ВариантОткрытия != ВариантОткрытияФормы.Чтение) && (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)) || ((this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.ПредварительныйПросмотр) && МенеджерБД.МенеджерИнициализирован));
                    if (base.CellModels.ContainsKey("ВыборИзСправочника"))
                    {
                        base.CellModels["ВыборИзСправочника"] = new РедакторЯчеек_ВыборИзСправочника(this.параметрыСозданияРедактора_ВыборИзСправочника);
                    }
                    if (base.CellModels.ContainsKey("МножественныйВыборИзСправочника"))
                    {
                        base.CellModels["МножественныйВыборИзСправочника"] = new РедакторЯчеек_ВыборИзСправочника(this.параметрыСозданияРедактора_ВыборИзСправочника);
                    }
                    base.ПроинициализироватьФункцииАвтоблоков(this.экраннаяФорма.ОтчетнаяФорма);
                }
            }
        }

        public Control ЭлементУправления
        {
            get
            {
                return this;
            }
        }
    }
}

