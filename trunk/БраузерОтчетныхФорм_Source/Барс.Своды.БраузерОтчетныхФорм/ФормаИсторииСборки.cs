namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraExport;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Export;
    using DevExpress.XtraGrid.Views.BandedGrid;
    using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
    using DevExpress.XtraGrid.Views.Base;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using Барс;
    using Барс.Интерфейс;
    using Барс.Своды;
    using Барс.Своды.ОтчетнаяФорма;
    using Барс.Своды.ТипыЯчеек;
    using Барс.Типы;

    public class ФормаИсторииСборки : XtraForm
    {
        private IContainer components = null;
        private AdvBandedGridView данныеТаблицыИстории;
        private Dictionary<string, object> значенияЯчеек = new Dictionary<string, object>();
        private string идентификаторФормы = "";
        private ФормаИндикаторПроцесса индикаторПроцесса = null;
        private SimpleButton кнопкаНазад;
        private Dictionary<string, ОписаниеУровняПросмотраИсторииСборки> кэшПодчиненныхОписаний = new Dictionary<string, ОписаниеУровняПросмотраИсторииСборки>();
        private Dictionary<string, ТаблицаДанных> кэшТаблицИсточников = new Dictionary<string, ТаблицаДанных>();
        private MemoEdit описаниеИстории;
        private BandedGridViewInfo параметрыОтображенияТаблицы = null;
        private RepositoryItemCalcEdit редакторЗначения;
        private Dictionary<string, СтолбецМетаструктуры> списокСтолбцов = null;
        private string столбецПостроения = "";
        private GridControl таблицаИстории;
        private ОписаниеУровняПросмотраИсторииСборки текущееОписание = null;
        private ТипПостроенияИсторииСборки типПостроения = ТипПостроенияИсторииСборки.ПоСтолбцу;

        public ФормаИсторииСборки(string идентификаторФормы)
        {
            this.InitializeComponent();
            this.идентификаторФормы = идентификаторФормы;
            КонтекстноеМеню меню = new КонтекстноеМеню();
            КонтПодменю подменю = меню.ДобавитьПодМеню("Печать таблицы");
            подменю.ДобавитьПунктМеню("Печать в Excel", new ОбработчикСобытия(this.пунктМенюПечать_Нажатие)).Tag = "excel";
            подменю.ДобавитьПунктМеню("Печать в HTML", new ОбработчикСобытия(this.пунктМенюПечать_Нажатие)).Tag = "html";
            подменю.ДобавитьПунктМеню("Печать в RTF", new ОбработчикСобытия(this.пунктМенюПечать_Нажатие)).Tag = "rtf";
            подменю.ДобавитьПунктМеню("Печать в PDF", new ОбработчикСобытия(this.пунктМенюПечать_Нажатие)).Tag = "pdf";
            меню.ДобавитьПодМеню("Настройки таблицы").ДобавитьПунктМеню("Сбросить настройки", new ОбработчикСобытия(this.пунктМенюНастройки_Нажатие)).Tag = "сбросить";
            this.таблицаИстории.ContextMenuStrip = меню;
            this.данныеТаблицыИстории.CustomUnboundColumnData += new CustomColumnDataEventHandler(this.данныеТаблицыИстории_CustomUnboundColumnData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.описаниеИстории = new MemoEdit();
            this.кнопкаНазад = new SimpleButton();
            this.таблицаИстории = new GridControl();
            this.данныеТаблицыИстории = new AdvBandedGridView();
            this.редакторЗначения = new RepositoryItemCalcEdit();
            this.описаниеИстории.Properties.BeginInit();
            this.таблицаИстории.BeginInit();
            this.данныеТаблицыИстории.BeginInit();
            this.редакторЗначения.BeginInit();
            base.SuspendLayout();
            this.описаниеИстории.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.описаниеИстории.Enabled = false;
            this.описаниеИстории.Location = new Point(0x57, 3);
            this.описаниеИстории.Name = "описаниеИстории";
            this.описаниеИстории.Properties.ReadOnly = true;
            this.описаниеИстории.Size = new Size(0x207, 60);
            this.описаниеИстории.TabIndex = 0;
            this.кнопкаНазад.Enabled = false;
            this.кнопкаНазад.Location = new Point(8, 3);
            this.кнопкаНазад.Name = "кнопкаНазад";
            this.кнопкаНазад.Size = new Size(0x49, 60);
            this.кнопкаНазад.TabIndex = 3;
            this.кнопкаНазад.Text = "Назад";
            this.кнопкаНазад.Click += new EventHandler(this.кнопкаНазад_Click);
            this.таблицаИстории.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.таблицаИстории.EmbeddedNavigator.Name = "";
            this.таблицаИстории.Location = new Point(8, 0x45);
            this.таблицаИстории.MainView = this.данныеТаблицыИстории;
            this.таблицаИстории.Name = "таблицаИстории";
            this.таблицаИстории.RepositoryItems.AddRange(new RepositoryItem[] { this.редакторЗначения });
            this.таблицаИстории.Size = new Size(0x256, 0xf7);
            this.таблицаИстории.TabIndex = 4;
            this.таблицаИстории.ViewCollection.AddRange(new BaseView[] { this.данныеТаблицыИстории });
            this.таблицаИстории.DoubleClick += new EventHandler(this.таблицаИстории_DoubleClick);
            this.данныеТаблицыИстории.GridControl = this.таблицаИстории;
            this.данныеТаблицыИстории.Name = "данныеТаблицыИстории";
            this.данныеТаблицыИстории.OptionsBehavior.AllowIncrementalSearch = true;
            this.данныеТаблицыИстории.OptionsBehavior.AutoExpandAllGroups = true;
            this.данныеТаблицыИстории.OptionsBehavior.Editable = false;
            this.данныеТаблицыИстории.OptionsView.ColumnAutoWidth = true;
            this.данныеТаблицыИстории.OptionsView.ShowFooter = true;
            this.редакторЗначения.AutoHeight = false;
            this.редакторЗначения.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.редакторЗначения.Name = "редакторЗначения";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x264, 0x143);
            base.Controls.Add(this.таблицаИстории);
            base.Controls.Add(this.кнопкаНазад);
            base.Controls.Add(this.описаниеИстории);
            base.Name = "ФормаИсторииСборки";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "История сборки";
            base.Load += new EventHandler(this.ФормаИсторииСборки_Load);
            base.FormClosing += new FormClosingEventHandler(this.ФормаИсторииСборки_FormClosing);
            this.описаниеИстории.Properties.EndInit();
            this.таблицаИстории.EndInit();
            this.данныеТаблицыИстории.EndInit();
            this.редакторЗначения.EndInit();
            base.ResumeLayout(false);
        }

        private void данныеТаблицыИстории_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            ЭлементПросмотраЗаписиИсторииСборки сборки;
            string str3;
            string str = "";
            if (e.Column.Tag != null)
            {
                string str2 = e.Column.Tag.ToString().Trim().ToLower();
                int dataSourceRowIndex = this.данныеТаблицыИстории.GetDataSourceRowIndex(e.RowHandle);
                сборки = this.ТекущийСписокЭлементов[dataSourceRowIndex];
                if (str2.StartsWith("значение_столбца_источника_"))
                {
                    str = str2.Substring(0x1b).Trim();
                    goto Label_00CA;
                }
                if (!str2.StartsWith("значение_столбца_приемника_") && str2.StartsWith("значение_столбца_"))
                {
                    str = str2.Substring(0x11).Trim();
                    goto Label_00CA;
                }
            }
            return;
        Label_00CA:
            str3 = сборки.ПолучитьЗначениеПараметра("значение_столбца_" + str);
            object obj2 = this.ОписаниеТекущегоУровня.ПолучитьЗначениеПоляИсточника(сборки, str);
            bool flag = true;
            if (obj2 is ОбщийЧисловойТип)
            {
                obj2 = ((ОбщийЧисловойТип) obj2).Значение;
                if (obj2 is decimal)
                {
                    decimal result = 0M;
                    if (decimal.TryParse(str3, out result) && (result == ((decimal) obj2)))
                    {
                        flag = false;
                    }
                }
                else if (obj2 is int)
                {
                    int num3 = 0;
                    if (int.TryParse(str3, out num3) && (num3 == ((int) obj2)))
                    {
                        flag = false;
                    }
                }
            }
            else if (((obj2 == null) && string.IsNullOrEmpty(str3)) || ((obj2 != null) && (str3 == obj2.ToString())))
            {
                flag = false;
            }
            if (flag)
            {
                e.Appearance.BackColor = Color.WhiteSmoke;
                e.Appearance.BackColor2 = Color.Red;
                e.Appearance.DrawBackground(e.Cache, e.Bounds);
                e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                e.Handled = true;
            }
        }

        private void данныеТаблицыИстории_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            List<ЭлементПросмотраЗаписиИсторииСборки> list = this.ТекущийСписокЭлементов;
            if (((list != null) && (((e.Column != null) && (e.Column.Tag != null)) && !string.IsNullOrEmpty(e.Column.Tag.ToString()))) && ((e.ListSourceRowIndex >= 0) && (e.ListSourceRowIndex < list.Count)))
            {
                string key = string.Format("{0}:{1}", e.Column.Name, e.ListSourceRowIndex);
                object obj2 = e.Value;
                if (this.значенияЯчеек.ContainsKey(key))
                {
                    obj2 = this.значенияЯчеек[key];
                }
                else
                {
                    string str3;
                    string str2 = e.Column.Tag.ToString().Trim().ToLower();
                    ЭлементПросмотраЗаписиИсторииСборки сборки = list[e.ListSourceRowIndex];
                    if (str2.StartsWith("значение_столбца_источника_"))
                    {
                        str3 = str2.Substring(0x1b).Trim();
                        obj2 = this.ОписаниеТекущегоУровня.ПолучитьЗначениеПоляИсточника(сборки, str3);
                        if (obj2 == null)
                        {
                            obj2 = 0M;
                        }
                        else if (obj2 is ОбщийЧисловойТип)
                        {
                            obj2 = (obj2 as ОбщийЧисловойТип).Значение;
                            if (obj2 == null)
                            {
                                obj2 = 0.00M;
                            }
                        }
                    }
                    else if (str2.StartsWith("значение_столбца_приемника_"))
                    {
                        str3 = str2.Substring(0x1b).Trim();
                        obj2 = this.ОписаниеТекущегоУровня.ПолучитьЗначениеПоляТекущейТаблицы(сборки, str3);
                        if (obj2 == null)
                        {
                            obj2 = 0M;
                        }
                        else if (obj2 is ОбщийЧисловойТип)
                        {
                            obj2 = (obj2 as ОбщийЧисловойТип).Значение;
                            if (obj2 == null)
                            {
                                obj2 = 0.00M;
                            }
                        }
                    }
                    else
                    {
                        obj2 = сборки.ПолучитьЗначениеПараметра(str2);
                        if (obj2 is ОбщийЧисловойТип)
                        {
                            obj2 = (obj2 as ОбщийЧисловойТип).Значение;
                            if (obj2 == null)
                            {
                                obj2 = 0.00M;
                            }
                        }
                        decimal num = 0.00M;
                        if (!(obj2 is decimal))
                        {
                            try
                            {
                                if (Конвертер.ПопробоватьВЧисло(obj2.ToString(), out num))
                                {
                                    obj2 = num;
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    this.значенияЯчеек.Add(key, obj2);
                }
                e.Value = obj2;
            }
        }

        private GridBand ДобавитьГруппуСтолбцов(string заголовок, GridBand родитель)
        {
            GridBand band = new GridBand();
            band.Caption = заголовок;
            band.Name = "группаСтолбцов" + this.данныеТаблицыИстории.Bands.Count.ToString();
            band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            band.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            band.AppearanceHeader.Options.UseTextOptions = true;
            band.Visible = true;
            this.данныеТаблицыИстории.Bands.Add(band);
            if (родитель != null)
            {
                родитель.Children.Add(band);
            }
            return band;
        }

        private BandedGridColumn ДобавитьСтолбец(GridBand группа, string заголовок, string элементЗаписи, bool сортировка, bool группировка)
        {
            BandedGridColumn column = new BandedGridColumn();
            column.Name = string.Format("столбец{0}", элементЗаписи.Replace(" ", ""));
            column.FieldName = column.Name;
            column.Caption = заголовок;
            column.Visible = true;
            column.VisibleIndex = this.данныеТаблицыИстории.Columns.Count;
            column.OptionsColumn.AllowSort = сортировка ? DefaultBoolean.True : DefaultBoolean.False;
            column.OptionsColumn.AllowGroup = группировка ? DefaultBoolean.True : DefaultBoolean.False;
            column.OptionsColumn.AllowMerge = DefaultBoolean.False;
            column.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            column.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            column.AppearanceHeader.Options.UseTextOptions = true;
            column.UnboundType = UnboundColumnType.String;
            column.Tag = элементЗаписи;
            группа.Columns.Add(column);
            группа.View.Columns.Add(column);
            return column;
        }

        private BandedGridColumn ДобавитьСтолбецЗначения(GridBand группа, string заголовок, string элементЗаписи)
        {
            BandedGridColumn column = this.ДобавитьСтолбец(группа, заголовок, элементЗаписи, true, false);
            column.DisplayFormat.FormatType = FormatType.Numeric;
            column.DisplayFormat.FormatString = "N2";
            column.GroupFormat.FormatType = FormatType.Numeric;
            column.GroupFormat.FormatString = "N2";
            column.OptionsColumn.AllowMerge = DefaultBoolean.False;
            column.UnboundType = UnboundColumnType.Decimal;
            column.SummaryItem.FieldName = column.FieldName;
            column.SummaryItem.SummaryType = SummaryItemType.Sum;
            column.SummaryItem.DisplayFormat = "{0:N2}";
            return column;
        }

        public bool Загрузить(ОтчетнаяФормаДанных отчетнаяФорма, string таблица, string строка)
        {
            return this.Загрузить(отчетнаяФорма, таблица, строка, "");
        }

        public bool Загрузить(ОтчетнаяФормаДанных отчетнаяФорма, string таблица, string строка, string строкаСтарая)
        {
            if (!(((отчетнаяФорма != null) && !string.IsNullOrEmpty(таблица)) && отчетнаяФорма.Данные.СодержитТаблицу(таблица)))
            {
                return false;
            }
            if ((this.типПостроения == ТипПостроенияИсторииСборки.ПоСтолбцу) && string.IsNullOrEmpty(this.столбецПостроения))
            {
                Сообщение.ПоказатьПредупреждение("Не указан столбец отображения.");
                return false;
            }
            bool flag = false;
            this.КурсорОжидания();
            try
            {
                try
                {
                    Guid guid = ХранилищеДанныхФорм.ПолучитьИдентификаторХранимыхДанных(отчетнаяФорма.Данные.Идентификатор);
                    string str = отчетнаяФорма.Метаструктура.Наименование;
                    string str2 = отчетнаяФорма.НаименованиеУчреждения;
                    this.списокСтолбцов = отчетнаяФорма.Метаструктура.Таблицы[таблица].Столбцы;
                    ОписаниеУровняПросмотраИсторииСборки сборки = new ОписаниеУровняПросмотраИсторииСборки(guid, str, str2, таблица, строка, строкаСтарая);
                    сборки.СтолбецПостроения = this.СтолбецПостроения;
                    сборки.ТипПостроения = this.ТипПостроенияИстории;
                    сборки.КэшТаблицИсточников = this.кэшТаблицИсточников;
                    сборки.КэшПодчиненныхОписаний = this.кэшПодчиненныхОписаний;
                    сборки.ПередФормированиемЭлементов += new СобытиеОписанияУровняИстории(this.ПередЗагрузкойЭлементовОписания);
                    сборки.ФормированиеЭлементов += new СобытиеОписанияУровняИстории(this.ЗагрузкаЭлементовОписания);
                    сборки.ПослеФормированияЭлементов += new СобытиеОписанияУровняИстории(this.ПослеЗагрузкиЭлементовОписания);
                    if (!сборки.ПостроитьОписание())
                    {
                        сборки.Очистить();
                    }
                    else
                    {
                        this.ОписаниеТекущегоУровня = сборки;
                        flag = true;
                    }
                }
                catch (Exception exception)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию("Ошибка загрузки данных.", exception);
                    flag = false;
                }
            }
            finally
            {
                this.ОбычныйКурсор();
            }
            return flag;
        }

        private void ЗагрузкаНастроек()
        {
            System.Type type = base.GetType();
            string str = "ТаблицаИстории_" + this.идентификаторФормы + "_" + this.типПостроения.ToString();
            try
            {
                string str2 = str + ".Расположение";
                string str3 = new ПутьФайлаНастроек(type, str2).ПолучитьПутьКФайлуНастроек(Метод.Чтение);
                if (!(string.IsNullOrEmpty(str3) || !File.Exists(str3)))
                {
                    this.таблицаИстории.MainView.RestoreLayoutFromXml(str3);
                }
                this.данныеТаблицыИстории.OptionsView.EnableAppearanceEvenRow = true;
                this.данныеТаблицыИстории.OptionsView.EnableAppearanceOddRow = true;
                this.данныеТаблицыИстории.OptionsView.ShowFooter = true;
            }
            catch (Exception exception)
            {
                Сообщение.ПоказатьИсключительнуюСитуацию("Ошибка загрузки параметров таблицы", exception);
            }
        }

        private void ЗагрузкаЭлементовОписания(ОписаниеУровняПросмотраИсторииСборки описание, object дополнительно)
        {
            if ((дополнительно != null) && (дополнительно is ЭлементПросмотраЗаписиИсторииСборки))
            {
                this.индикаторПроцесса.СледующийШаг((дополнительно as ЭлементПросмотраЗаписиИсторииСборки).Учреждение);
                Application.DoEvents();
            }
        }

        private void ИзменитьУровеньИстории(bool следующийУровень)
        {
            if (this.ОписаниеТекущегоУровня != null)
            {
                this.СохранениеНастроек();
                this.КурсорОжидания();
                try
                {
                    if (следующийУровень)
                    {
                        int dataSourceRowIndex = this.данныеТаблицыИстории.GetDataSourceRowIndex(this.данныеТаблицыИстории.FocusedRowHandle);
                        ОписаниеУровняПросмотраИсторииСборки сборки = this.ОписаниеТекущегоУровня.ПолучитьОписаниеПоЭлементу(dataSourceRowIndex);
                        if ((сборки != null) && сборки.ПостроитьОписание())
                        {
                            this.ОписаниеТекущегоУровня = сборки;
                        }
                        else
                        {
                            Сообщение.Показать("История сборки для данной строки отсутствует.");
                        }
                    }
                    else if (this.ОписаниеТекущегоУровня.Родитель != null)
                    {
                        this.ОписаниеТекущегоУровня = this.ОписаниеТекущегоУровня.Родитель;
                    }
                }
                finally
                {
                    this.таблицаИстории.RefreshDataSource();
                    this.ОбычныйКурсор();
                }
                this.кнопкаНазад.Enabled = this.ОписаниеТекущегоУровня.Родитель != null;
            }
        }

        private void кнопкаНазад_Click(object sender, EventArgs e)
        {
            this.ИзменитьУровеньИстории(false);
        }

        private void КурсорОжидания()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        private void ОбновитьИнформационноеПоле()
        {
            if (this.ОписаниеТекущегоУровня != null)
            {
                this.описаниеИстории.Lines = new string[] { "Элемент цепочки : " + this.ОписаниеТекущегоУровня.Учреждение, "Код учреждения : " + this.ОписаниеТекущегоУровня.КодУчреждения, "Форма : " + this.ОписаниеТекущегоУровня.Форма, "Таблица : " + this.ОписаниеТекущегоУровня.Таблица + ", Строка / Ключевое значение : " + this.ОписаниеТекущегоУровня.Строка + ((this.типПостроения == ТипПостроенияИсторииСборки.ПоСтолбцу) ? (", Столбец : " + this.столбецПостроения) : "") };
            }
        }

        private void ОбновитьСтолбцыТаблицы()
        {
            this.данныеТаблицыИстории.BeginUpdate();
            try
            {
                this.данныеТаблицыИстории.Bands.Clear();
                this.данныеТаблицыИстории.Columns.Clear();
                GridBand band = this.ДобавитьГруппуСтолбцов("Показатели", null);
                this.ДобавитьСтолбец(band, "Код учреждения", "КодУчреждения", true, true);
                this.ДобавитьСтолбец(band, "Элемент цепочки", "Учреждение", true, true);
                this.ДобавитьСтолбец(band, "Форма", "Форма", true, true);
                base.WindowState = FormWindowState.Maximized;
                if (this.типПостроения == ТипПостроенияИсторииСборки.ПоСтолбцу)
                {
                    this.данныеТаблицыИстории.OptionsView.ColumnAutoWidth = true;
                    this.данныеТаблицыИстории.OptionsBehavior.AutoUpdateTotalSummary = true;
                    this.ДобавитьСтолбец(band, "Строка / Ключевое значение", "Строка", true, true);
                    this.ДобавитьСтолбец(band, "Столбец", "Столбец", true, true);
                    this.ДобавитьСтолбецЗначения(band, "Значение", "Значение");
                }
                else
                {
                    this.данныеТаблицыИстории.OptionsView.ColumnAutoWidth = false;
                    if (this.типПостроения == ТипПостроенияИсторииСборки.ПоСтрокеСИсточником)
                    {
                        this.данныеТаблицыИстории.OptionsView.EnableAppearanceOddRow = false;
                        this.данныеТаблицыИстории.OptionsView.EnableAppearanceEvenRow = false;
                        this.данныеТаблицыИстории.CustomDrawCell += new RowCellCustomDrawEventHandler(this.данныеТаблицыИстории_CustomDrawCell);
                    }
                    GridBand band2 = this.ДобавитьГруппуСтолбцов("Значения", null);
                    foreach (СтолбецМетаструктуры метаструктуры in this.списокСтолбцов.Values)
                    {
                        if (this.типПостроения == ТипПостроенияИсторииСборки.ПоСтрокеСИсточником)
                        {
                            GridBand band3 = this.ДобавитьГруппуСтолбцов("Столбец " + метаструктуры.Код, band2);
                            this.ДобавитьСтолбецЗначения(band3, "История", "значение_столбца_" + метаструктуры.Код);
                            this.ДобавитьСтолбецЗначения(band3, "Источник", "значение_столбца_источника_" + метаструктуры.Код);
                            BandedGridColumn column = this.ДобавитьСтолбецЗначения(band3, "Приемник", "значение_столбца_приемника_" + метаструктуры.Код);
                            column.OptionsColumn.AllowMerge = DefaultBoolean.True;
                            column.SummaryItem.SummaryType = SummaryItemType.None;
                        }
                        else
                        {
                            this.ДобавитьСтолбецЗначения(band2, "Столбец " + метаструктуры.Код, "значение_столбца_" + метаструктуры.Код);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Сообщение.ПоказатьИсключительнуюСитуацию("Ошибка построения таблицы.", exception);
            }
            this.данныеТаблицыИстории.EndUpdate();
        }

        private void ОбычныйКурсор()
        {
            Cursor.Current = Cursors.Default;
        }

        private void ПередЗагрузкойЭлементовОписания(ОписаниеУровняПросмотраИсторииСборки описание, object дополнительно)
        {
            if (this.индикаторПроцесса == null)
            {
                this.индикаторПроцесса = new ФормаИндикаторПроцесса();
            }
            this.индикаторПроцесса.Минимум = 0;
            this.индикаторПроцесса.Максимум = описание.История.Count;
            this.индикаторПроцесса.Позиция = 0;
            this.индикаторПроцесса.Шаг = 1;
            this.индикаторПроцесса.Показать();
        }

        private void ПослеЗагрузкиЭлементовОписания(ОписаниеУровняПросмотраИсторииСборки описание, object дополнительно)
        {
            this.индикаторПроцесса.Закрыть();
        }

        public bool Построить()
        {
            if (this.ОписаниеТекущегоУровня == null)
            {
                return false;
            }
            this.ОбновитьИнформационноеПоле();
            this.ОбновитьСтолбцыТаблицы();
            this.ТекущийСписокЭлементов = this.ОписаниеТекущегоУровня.СписокЭлементов;
            this.ЗагрузкаНастроек();
            this.СохранениеНастроек();
            return true;
        }

        private void пунктМенюНастройки_Нажатие(object отправитель, АргументыСобытия аргументы)
        {
            if (((отправитель != null) && (отправитель is ToolStripMenuItem)) && ((отправитель as ToolStripMenuItem).Tag != null))
            {
                string str2 = (отправитель as ToolStripMenuItem).Tag.ToString().Trim().ToLower();
                if ((((str2 != null) && (str2 != "сохранить")) && (str2 != "загрузить")) && (str2 == "сбросить"))
                {
                    this.СброситьНастройки(true);
                }
            }
        }

        private void пунктМенюПечать_Нажатие(object отправитель, АргументыСобытия аргументы)
        {
            if (((отправитель == null) || !(отправитель is ToolStripMenuItem)) || ((отправитель as ToolStripMenuItem).Tag == null))
            {
                return;
            }
            string str = (отправитель as ToolStripMenuItem).Tag.ToString().Trim().ToLower();
            string tempFileName = Path.GetTempFileName();
            this.КурсорОжидания();
            try
            {
                try
                {
                    string str3 = str;
                    if (str3 != null)
                    {
                        if (!(str3 == "excel"))
                        {
                            if (str3 == "html")
                            {
                                goto Label_00D0;
                            }
                            if (str3 == "rtf")
                            {
                                goto Label_00DF;
                            }
                            if (str3 == "pdf")
                            {
                                goto Label_00FA;
                            }
                        }
                        else
                        {
                            tempFileName = tempFileName + ".xls";
                            ExportXlsProvider provider = new ExportXlsProvider(tempFileName);
                            GridViewExportLink link = this.данныеТаблицыИстории.CreateExportLink(provider) as GridViewExportLink;
                            link.ExportCellsAsDisplayText = false;
                            link.ExportTo(true);
                        }
                    }
                    goto Label_013E;
                Label_00D0:
                    this.таблицаИстории.ExportToHtml(tempFileName);
                    goto Label_013E;
                Label_00DF:
                    tempFileName = tempFileName + ".rtf";
                    this.таблицаИстории.ExportToRtf(tempFileName);
                    goto Label_013E;
                Label_00FA:
                    tempFileName = tempFileName + ".pdf";
                    this.таблицаИстории.ExportToPdf(tempFileName);
                }
                catch (Exception exception)
                {
                    Сообщение.ПоказатьИсключительнуюСитуацию("Ошибка печати таблицы истории сборки.", exception);
                    tempFileName = "";
                }
            }
            finally
            {
                this.ОбычныйКурсор();
            }
        Label_013E:
            if (!(string.IsNullOrEmpty(tempFileName) || !File.Exists(tempFileName)))
            {
                ОткрытиеФайлов.ОткрытьФайл(tempFileName);
            }
        }

        private void СброситьНастройки(bool обновитьСразу)
        {
            System.Type type = base.GetType();
            try
            {
                if (type != null)
                {
                    string str = "ТаблицаИстории_" + this.идентификаторФормы + "_" + this.типПостроения.ToString() + ".Расположение";
                    string path = new ПутьФайлаНастроек(type, str).ПолучитьПутьКФайлуНастроек(Метод.Чтение);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        if (обновитьСразу)
                        {
                            this.Построить();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Сообщение.ПоказатьПредупреждение("Не удалось сбросить настройки. Причина: ", exception);
            }
        }

        private void СохранениеНастроек()
        {
            System.Type type = base.GetType();
            string str = "ТаблицаИстории_" + this.идентификаторФормы + "_" + this.типПостроения.ToString();
            if ((type != null) && !string.IsNullOrEmpty(str))
            {
                try
                {
                    string str2 = str + ".Расположение";
                    ПутьФайлаНастроек настроек = new ПутьФайлаНастроек(type, str2);
                    string xmlFile = настроек.ПолучитьПутьКФайлуНастроек(Метод.Запись) + @"\" + str2 + ".xml";
                    this.таблицаИстории.MainView.SaveLayoutToXml(xmlFile);
                }
                catch (Exception)
                {
                }
            }
        }

        private void таблицаИстории_DoubleClick(object sender, EventArgs e)
        {
            if (this.данныеТаблицыИстории.FocusedRowHandle >= 0)
            {
                this.ИзменитьУровеньИстории(true);
            }
        }

        private void ФормаИсторииСборки_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.СохранениеНастроек();
            foreach (ТаблицаДанных данных in this.кэшТаблицИсточников.Values)
            {
                данных.ОчиститьХешСловарь();
            }
        }

        private void ФормаИсторииСборки_Load(object sender, EventArgs e)
        {
            this.ЗагрузкаНастроек();
            this.параметрыОтображенияТаблицы = new BandedGridViewInfo(this.данныеТаблицыИстории);
        }

        private ОписаниеУровняПросмотраИсторииСборки ОписаниеТекущегоУровня
        {
            get
            {
                return this.текущееОписание;
            }
            set
            {
                this.текущееОписание = value;
                if (this.текущееОписание != null)
                {
                    this.ОбновитьИнформационноеПоле();
                    this.ТекущийСписокЭлементов = this.текущееОписание.СписокЭлементов;
                }
            }
        }

        public string СтолбецПостроения
        {
            get
            {
                return this.столбецПостроения;
            }
            set
            {
                this.столбецПостроения = value;
            }
        }

        private List<ЭлементПросмотраЗаписиИсторииСборки> ТекущийСписокЭлементов
        {
            get
            {
                if (this.таблицаИстории.DataSource == null)
                {
                    return null;
                }
                return (List<ЭлементПросмотраЗаписиИсторииСборки>) this.таблицаИстории.DataSource;
            }
            set
            {
                this.таблицаИстории.DataSource = value;
                this.таблицаИстории.RefreshDataSource();
                this.значенияЯчеек = new Dictionary<string, object>();
            }
        }

        public ТипПостроенияИсторииСборки ТипПостроенияИстории
        {
            get
            {
                return this.типПостроения;
            }
            set
            {
                this.типПостроения = value;
            }
        }
    }
}

