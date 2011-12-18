namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Utils;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Columns;
    using DevExpress.XtraGrid.Views.BandedGrid;
    using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
    using DevExpress.XtraGrid.Views.Base;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraPrinting;
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.XPath;
    using Барс;
    using Барс.БраузерОтчетныхФорм.Properties;
    using Барс.Интерфейс;
    using Барс.Интерфейс.ЭлементыТаблицы;
    using Барс.Своды;
    using Барс.Своды.АргументыСобытийОтчетнойФормы;
    using Барс.Своды.ОтчетнаяФорма;
    using Барс.Своды.ТипыЯчеек;
    using Барс.Ядро;

    public class ДинамическаяТаблица : DevExpress.XtraGrid.GridControl, ИнтерфейсОтображаемойТаблицыЭкраннойФормы, IDisposable
    {
        private IContainer components;
        private GridView gridView1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem вставитьИзБуфераToolStripMenuItem;
        private ДанныеДинамическойТаблицы данные;
        private ToolStripMenuItem добавитьToolStripMenuItem;
        private Барс.Своды.ОтчетнаяФорма.ТаблицаДанных зеркалоДанных;
        private bool значенияИзменились;
        private string имяЛиста;
        private string кодТаблицы;
        private int количествоФиксированныхСтолбцов;
        private ContextMenuStrip контекстноеМеню;
        private КонтПодменю контПодменюИстория;
        private ToolStripMenuItem копироватьВБуферToolStripMenuItem;
        private ContextMenuStrip МенюДинамическойТаблицы;
        private AdvBandedGridView многоколоночноеПредставление;
        private string наименование;
        private ToolStripMenuItem настройкиToolStripMenuItem;
        private bool настройкиСброшены;
        private ToolStripMenuItem печатьToolStripMenuItem;
        private bool размещатьНаЗакладке;
        private ToolStripMenuItem свернутьСтрокиtoolStripMenuItem;
        private Барс.Своды.ОтчетнаяФорма.ТаблицаДанных таблицаДанных;
        private Барс.Своды.ОтчетнаяФорма.ТаблицаМетаструктуры таблицаМетаструктуры;
        private System.Type типФормы;
        private ToolStripMenuItem удалитьToolStripMenuItem;
        private ToolStripMenuItem удалитьНастройкиToolStripMenuItem;
        private Барс.Своды.БраузерОтчетныхФорм.ЭкраннаяФорма экраннаяФорма;

        public event СобытиеПослеУстановкиЗначенияЯчейки ПослеУстановкиЗначенияЯчейкиСправочника;

        public event СобытиеИзмененияТаблицыДанных ПриИзмененииДанных;

        public ДинамическаяТаблица()
        {
            this.многоколоночноеПредставление = null;
            this.данные = null;
            this.значенияИзменились = false;
            this.количествоФиксированныхСтолбцов = 0;
            this.контекстноеМеню = null;
            this.имяЛиста = "";
            this.размещатьНаЗакладке = false;
            this.экраннаяФорма = null;
            this.таблицаМетаструктуры = null;
            this.таблицаДанных = null;
            this.зеркалоДанных = null;
            this.настройкиСброшены = false;
            this.InitializeComponent();
            this.многоколоночноеПредставление = new AdvBandedGridView(this);
            this.многоколоночноеПредставление.Name = "Представление с объединением колонок";
            this.многоколоночноеПредставление.GridControl = this;
            base.ViewCollection.AddRange(new BaseView[] { this.многоколоночноеПредставление });
            base.MainView = this.многоколоночноеПредставление;
            this.многоколоночноеПредставление.Appearance.BandPanel.Options.UseTextOptions = true;
            this.многоколоночноеПредставление.Appearance.BandPanel.TextOptions.HAlignment = HorzAlignment.Center;
            this.многоколоночноеПредставление.Appearance.BandPanel.TextOptions.VAlignment = VertAlignment.Top;
            this.многоколоночноеПредставление.Appearance.BandPanel.TextOptions.WordWrap = WordWrap.Wrap;
            this.многоколоночноеПредставление.Appearance.BandPanel.TextOptions.Trimming = Trimming.Word;
            this.многоколоночноеПредставление.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.многоколоночноеПредставление.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;
            this.многоколоночноеПредставление.OptionsBehavior.AutoPopulateColumns = false;
            this.многоколоночноеПредставление.OptionsBehavior.Editable = true;
            this.многоколоночноеПредставление.OptionsBehavior.EditorShowMode = EditorShowMode.Default;
            this.многоколоночноеПредставление.OptionsNavigation.EnterMoveNextColumn = true;
            this.многоколоночноеПредставление.OptionsCustomization.AllowColumnMoving = true;
            this.многоколоночноеПредставление.OptionsCustomization.AllowFilter = true;
            this.многоколоночноеПредставление.OptionsCustomization.AllowGroup = true;
            this.многоколоночноеПредставление.OptionsCustomization.AllowSort = true;
            this.многоколоночноеПредставление.OptionsMenu.EnableColumnMenu = true;
            this.многоколоночноеПредставление.OptionsMenu.EnableFooterMenu = true;
            this.многоколоночноеПредставление.OptionsMenu.EnableGroupPanelMenu = false;
            this.многоколоночноеПредставление.OptionsSelection.MultiSelect = true;
            this.многоколоночноеПредставление.OptionsView.ShowGroupPanel = true;
            this.многоколоночноеПредставление.OptionsView.ShowFooter = true;
            this.многоколоночноеПредставление.GroupFooterShowMode = GroupFooterShowMode.VisibleAlways;
            this.многоколоночноеПредставление.OptionsView.ShowIndicator = true;
            this.многоколоночноеПредставление.OptionsSelection.InvertSelection = false;
            this.многоколоночноеПредставление.OptionsFilter.UseNewCustomFilterDialog = true;
            this.многоколоночноеПредставление.OptionsPrint.AutoWidth = false;
            this.многоколоночноеПредставление.OptionsPrint.ExpandAllGroups = false;
            this.многоколоночноеПредставление.OptionsPrint.UsePrintStyles = true;
            this.многоколоночноеПредставление.OptionsPrint.PrintGroupFooter = true;
            this.многоколоночноеПредставление.ShowButtonMode = ShowButtonModeEnum.ShowOnlyInEditor;
            this.многоколоночноеПредставление.OptionsDetail.EnableMasterViewMode = false;
            this.многоколоночноеПредставление.FixedLineWidth = 1;
            this.многоколоночноеПредставление.KeyDown += new KeyEventHandler(this.ДинамическаяТаблица_KeyDown);
            this.многоколоночноеПредставление.CustomUnboundColumnData += new CustomColumnDataEventHandler(this.многоколоночноеПредставление_CustomUnboundColumnData);
            this.многоколоночноеПредставление.ShownEditor += new EventHandler(this.многоколоночноеПредставление_ShownEditor);
            this.многоколоночноеПредставление.MouseUp += new MouseEventHandler(this.многоколоночноеПредставление_MouseUp);
            this.многоколоночноеПредставление.CustomDrawCell += new RowCellCustomDrawEventHandler(this.многоколоночноеПредставление_CustomDrawCell);
            this.многоколоночноеПредставление.CustomDrawRowIndicator += new RowIndicatorCustomDrawEventHandler(this.многоколоночноеПредставление_CustomDrawRowIndicator);
            this.многоколоночноеПредставление.ActiveFilter.Changed += new EventHandler(this.многоколоночноеПредставление_ИзменениеФильтра);
            this.многоколоночноеПредставление.FocusedColumnChanged += new FocusedColumnChangedEventHandler(this.многоколоночноеПредставление_FocusedColumnChanged);
            this.многоколоночноеПредставление.FocusedRowChanged += new FocusedRowChangedEventHandler(this.многоколоночноеПредставление_FocusedRowChanged);
            this.многоколоночноеПредставление.CellValueChanged += new CellValueChangedEventHandler(this.многоколоночноеПредставление_CellValueChanged);
            this.LookAndFeel.UseDefaultLookAndFeel = true;
            КонтПунктМеню меню = null;
            меню = this.контПодменюИстория.ДобавитьПунктМеню("Для текущей ячейки", new ОбработчикСобытия(this.элементИстория_Click));
            if (меню != null)
            {
                меню.Tag = ТипПостроенияИсторииСборки.ПоСтолбцу;
            }
            меню = this.контПодменюИстория.ДобавитьПунктМеню("Для всей строки", new ОбработчикСобытия(this.элементИстория_Click));
            if (меню != null)
            {
                меню.Tag = ТипПостроенияИсторииСборки.ПоСтроке;
            }
            меню = this.контПодменюИстория.ДобавитьПунктМеню("Сравнение с текущими данными источника", new ОбработчикСобытия(this.элементИстория_Click));
            if (меню != null)
            {
                меню.Tag = ТипПостроенияИсторииСборки.ПоСтрокеСИсточником;
            }
            this.КонтекстноеМеню = this.МенюДинамическойТаблицы;
        }

        public ДинамическаяТаблица(Барс.Своды.ОтчетнаяФорма.ТаблицаМетаструктуры ТаблицаМетаструктуры, Барс.Своды.ОтчетнаяФорма.ТаблицаДанных ТаблицаДанных) : this()
        {
            this.таблицаМетаструктуры = ТаблицаМетаструктуры;
            this.таблицаДанных = ТаблицаДанных;
        }

        protected override void Dispose(bool disposing)
        {
            this.экраннаяФорма = null;
            this.таблицаМетаструктуры = null;
            this.таблицаДанных = null;
            this.зеркалоДанных = null;
            if (this.данные != null)
            {
                this.данные.ОсвободитьРесурсы();
            }
            this.данные = null;
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ДинамическаяТаблица));
            this.gridView1 = new GridView();
            this.МенюДинамическойТаблицы = new ContextMenuStrip(this.components);
            this.добавитьToolStripMenuItem = new ToolStripMenuItem();
            this.удалитьToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator4 = new ToolStripSeparator();
            this.свернутьСтрокиtoolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator3 = new ToolStripSeparator();
            this.копироватьВБуферToolStripMenuItem = new ToolStripMenuItem();
            this.вставитьИзБуфераToolStripMenuItem = new ToolStripMenuItem();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.контПодменюИстория = new КонтПодменю();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.настройкиToolStripMenuItem = new ToolStripMenuItem();
            this.удалитьНастройкиToolStripMenuItem = new ToolStripMenuItem();
            this.печатьToolStripMenuItem = new ToolStripMenuItem();
            this.gridView1.BeginInit();
            this.МенюДинамическойТаблицы.SuspendLayout();
            this.BeginInit();
            base.SuspendLayout();
            this.gridView1.GridControl = this;
            this.gridView1.Name = "gridView1";
            this.МенюДинамическойТаблицы.Items.AddRange(new ToolStripItem[] { this.добавитьToolStripMenuItem, this.удалитьToolStripMenuItem, this.toolStripSeparator4, this.свернутьСтрокиtoolStripMenuItem, this.toolStripSeparator3, this.копироватьВБуферToolStripMenuItem, this.вставитьИзБуфераToolStripMenuItem, this.toolStripSeparator1, this.контПодменюИстория, this.toolStripSeparator2, this.настройкиToolStripMenuItem, this.печатьToolStripMenuItem });
            this.МенюДинамическойТаблицы.Name = "МенюДинамическойТаблицы";
            this.МенюДинамическойТаблицы.Size = new Size(0xdb, 0xcc);
            this.МенюДинамическойТаблицы.Opening += new CancelEventHandler(this.МенюДинамическойТаблицы_Opening);
            this.добавитьToolStripMenuItem.Image = Resources.plus4;
            this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
            this.добавитьToolStripMenuItem.Size = new Size(0xda, 0x16);
            this.добавитьToolStripMenuItem.Text = "Добавить";
            this.добавитьToolStripMenuItem.Click += new EventHandler(this.добавитьToolStripMenuItem_Click);
            this.удалитьToolStripMenuItem.Image = Resources.cancel3;
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new Size(0xda, 0x16);
            this.удалитьToolStripMenuItem.Text = "Удалить";
            this.удалитьToolStripMenuItem.Click += new EventHandler(this.удалитьToolStripMenuItem_Click);
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new Size(0xd7, 6);
            this.свернутьСтрокиtoolStripMenuItem.Image = (Image) manager.GetObject("свернутьСтрокиtoolStripMenuItem.Image");
            this.свернутьСтрокиtoolStripMenuItem.Name = "свернутьСтрокиtoolStripMenuItem";
            this.свернутьСтрокиtoolStripMenuItem.Size = new Size(0xda, 0x16);
            this.свернутьСтрокиtoolStripMenuItem.Text = "Свернуть строки";
            this.свернутьСтрокиtoolStripMenuItem.Click += new EventHandler(this.свернутьСтрокиtoolStripMenuItem_Click);
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new Size(0xd7, 6);
            this.копироватьВБуферToolStripMenuItem.Image = (Image) manager.GetObject("копироватьВБуферToolStripMenuItem.Image");
            this.копироватьВБуферToolStripMenuItem.Name = "копироватьВБуферToolStripMenuItem";
            this.копироватьВБуферToolStripMenuItem.Size = new Size(0xda, 0x16);
            this.копироватьВБуферToolStripMenuItem.Text = "Копировать в буфер";
            this.копироватьВБуферToolStripMenuItem.Click += new EventHandler(this.копироватьВБуферToolStripMenuItem_Click);
            this.вставитьИзБуфераToolStripMenuItem.Image = (Image) manager.GetObject("вставитьИзБуфераToolStripMenuItem.Image");
            this.вставитьИзБуфераToolStripMenuItem.Name = "вставитьИзБуфераToolStripMenuItem";
            this.вставитьИзБуфераToolStripMenuItem.Size = new Size(0xda, 0x16);
            this.вставитьИзБуфераToolStripMenuItem.Text = "Вставить из буфера";
            this.вставитьИзБуфераToolStripMenuItem.Click += new EventHandler(this.вставитьИзБуфераToolStripMenuItem_Click);
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new Size(0xd7, 6);
            this.контПодменюИстория.Image = (Image) manager.GetObject("контПодменюИстория.Image");
            this.контПодменюИстория.Name = "контПодменюИстория";
            this.контПодменюИстория.Size = new Size(0xda, 0x16);
            this.контПодменюИстория.Text = "Показать историю сборки";
            this.контПодменюИстория.Заголовок = "Показать историю сборки";
            this.контПодменюИстория.Иконка = (Image) manager.GetObject("контПодменюИстория.Иконка");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new Size(0xd7, 6);
            this.настройкиToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { this.удалитьНастройкиToolStripMenuItem });
            this.настройкиToolStripMenuItem.Image = Resources.app_options;
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new Size(0xda, 0x16);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.удалитьНастройкиToolStripMenuItem.Image = Resources.cancel3;
            this.удалитьНастройкиToolStripMenuItem.Name = "удалитьНастройкиToolStripMenuItem";
            this.удалитьНастройкиToolStripMenuItem.Size = new Size(0xbd, 0x16);
            this.удалитьНастройкиToolStripMenuItem.Text = "Сбросить настройки";
            this.удалитьНастройкиToolStripMenuItem.Click += new EventHandler(this.удалитьНастройкиToolStripMenuItem_Click);
            this.печатьToolStripMenuItem.Image = Resources.print;
            this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            this.печатьToolStripMenuItem.Size = new Size(0xda, 0x16);
            this.печатьToolStripMenuItem.Text = "Экспорт в Excel";
            this.печатьToolStripMenuItem.Click += new EventHandler(this.печатьToolStripMenuItem_Click);
            this.EmbeddedNavigator.Name = "";
            base.MainView = this.gridView1;
            base.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            base.KeyUp += new KeyEventHandler(this.ДинамическаяТаблица_KeyUp);
            this.gridView1.EndInit();
            this.МенюДинамическойТаблицы.ResumeLayout(false);
            this.EndInit();
            base.ResumeLayout(false);
        }

        private void вставитьИзБуфераToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ВставитьСтрокиИзБуфера();
        }

        public void ВставитьСтрокиИзБуфера()
        {
            if (this.Данные != null)
            {
                string text = Clipboard.GetText();
                if (string.IsNullOrEmpty(text.Trim()))
                {
                    Сообщение.Показать("В буфере обмена нет данных.", "Буфер обмена");
                }
                else
                {
                    РезультатыВыполненияСверкиДанных данных = null;
                    this.Данные.ВставитьСтрокиИзБуфера(text);
                    this.ЭкраннаяФорма.ОтчетнаяФорма.ОбработатьСобытие_ПослеВставкиИзБО(this.Данные.ТаблицаДанных, out данных);
                    if (данных != null)
                    {
                        new ФормаОтчетаСверкиДанных(данных).ПоказатьДиалог();
                    }
                    this.Данные.ПересчитатьЗначенияАвтоблоков();
                    this.RefreshDataSource();
                    this.значенияИзменились = true;
                    this.многоколоночноеПредставление.FocusedRowHandle = this.многоколоночноеПредставление.GetRowHandle(this.Данные.Count - 1);
                    this.многоколоночноеПредставление.ClearSelection();
                    if (this.ПриИзмененииДанных != null)
                    {
                        this.ПриИзмененииДанных(this);
                    }
                }
            }
        }

        private object ВыборИзСправочника_ПолучениеИсточникаЗаписейДляВыбора(АргументыОтменяемогоСобытия Аргументы)
        {
            if ((this.ЭкраннаяФорма == null) || (this.ЭкраннаяФорма.ОтчетнаяФорма == null))
            {
                return null;
            }
            string str = "";
            string key = "";
            ОписаниеСсылкиНаСправочник справочник = null;
            try
            {
                str = this.многоколоночноеПредставление.GetDataSourceRowIndex(this.многоколоночноеПредставление.FocusedRowHandle).ToString();
                key = this.многоколоночноеПредставление.FocusedColumn.FieldName;
                if (this.ТаблицаМетаструктуры.Столбцы.ContainsKey(key))
                {
                    if (this.ТаблицаМетаструктуры.Столбцы[key].Тип.IsSubclassOf(typeof(СсылочныйТип)))
                    {
                        справочник = new ОписаниеСсылкиНаСправочник(this.ТаблицаМетаструктуры.Столбцы[key].Описание);
                    }
                }
                else
                {
                    return null;
                }
                if (!((справочник != null) && справочник.ДополнительнаяОбработкаПолученияИсточникаЗаписей))
                {
                    return null;
                }
                АргументыСобытияПолученияИсточникаЗаписейДляВыбораИзСправочника справочника = new АргументыСобытияПолученияИсточникаЗаписейДляВыбораИзСправочника();
                справочника.ОтменитьВыбор = Аргументы.ОтменитьДействие;
                справочника.ТаблицаДанных = this.РазмещатьНаЗакладке ? this.ТаблицаДанных : this.зеркалоДанных;
                справочника.КодСтроки = str;
                справочника.КодСтолбца = key;
                справочника.ОписаниеСсылки = справочник;
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

        private void ДинамическаяТаблица_KeyDown(object sender, KeyEventArgs e)
        {
            if ((this.ЭкраннаяФорма == null) || (this.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.РедакторУвязок))
            {
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        if ((this.ВариантОткрытия != ВариантОткрытияФормы.Чтение) && (this.ВариантОткрытия != ВариантОткрытияФормы.СводнаяФормаБезРедактирования))
                        {
                            this.ДобавитьСтроку();
                        }
                        break;

                    case Keys.Delete:
                        if ((this.ВариантОткрытия != ВариантОткрытияФормы.Чтение) && (this.ВариантОткрытия != ВариантОткрытияФормы.СводнаяФормаБезРедактирования))
                        {
                            this.УдалитьТекущиеСтроки();
                        }
                        break;

                    case Keys.Escape:
                        this.многоколоночноеПредставление.HideEditor();
                        break;

                    case Keys.Return:
                        if (e.Control)
                        {
                        }
                        break;
                }
            }
        }

        private void ДинамическаяТаблица_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.ЭкраннаяФорма.ОткрытьМетодическийСправочник(this);
            }
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ДобавитьСтроку();
        }

        public void ДобавитьОбъединение(ОбъединениеДинамическойТаблицы объединение)
        {
            объединение.AppearanceHeader.Font = this.Font;
            this.многоколоночноеПредставление.Bands.Add(объединение);
        }

        public void ДобавитьСтроку()
        {
            if (this.Данные != null)
            {
                this.Данные.ДобавитьСтроку();
                this.RefreshDataSource();
                this.значенияИзменились = true;
                this.многоколоночноеПредставление.FocusedRowHandle = this.многоколоночноеПредставление.GetRowHandle(this.Данные.Count - 1);
                this.многоколоночноеПредставление.ClearSelection();
                if (this.ПриИзмененииДанных != null)
                {
                    this.ПриИзмененииДанных(this);
                }
            }
        }

        public void ЗагрузитьИзXML(XPathNavigator Документ)
        {
            Документ.MoveToFirstChild();
            Документ.MoveToChild("Объединения", "");
            string attribute = Документ.GetAttribute("ФиксСтолбцов", "");
            if (!string.IsNullOrEmpty(attribute))
            {
                int.TryParse(attribute, out this.количествоФиксированныхСтолбцов);
            }
            this.ПостроитьОбъединения(Документ);
            if ((this.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (this.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФормаБезРедактирования))
            {
                this.РазрешитьРедактирование = false;
            }
            if (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.РедакторУвязок)
            {
                this.многоколоночноеПредставление.OptionsBehavior.Editable = false;
                this.многоколоночноеПредставление.OptionsCustomization.AllowColumnMoving = false;
                this.многоколоночноеПредставление.OptionsCustomization.AllowFilter = false;
                this.многоколоночноеПредставление.OptionsCustomization.AllowGroup = false;
                this.многоколоночноеПредставление.OptionsCustomization.AllowSort = false;
                this.многоколоночноеПредставление.OptionsMenu.EnableColumnMenu = false;
                this.многоколоночноеПредставление.OptionsMenu.EnableFooterMenu = false;
                this.многоколоночноеПредставление.OptionsMenu.EnableGroupPanelMenu = false;
                this.многоколоночноеПредставление.OptionsView.ShowGroupPanel = false;
                this.многоколоночноеПредставление.OptionsView.ShowFooter = false;
                this.многоколоночноеПредставление.GroupFooterShowMode = GroupFooterShowMode.Hidden;
            }
        }

        public void ЗагрузкаНастроек(System.Type ТипФормы, string НазваниеКомпонента)
        {
            if (((ТипФормы != null) && !string.IsNullOrEmpty(НазваниеКомпонента)) && ((this.ЭкраннаяФорма != null) && (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)))
            {
                this.типФормы = ТипФормы;
                System.Type type = ТипФормы;
                try
                {
                    string str = НазваниеКомпонента + ".Расположение";
                    string str2 = new ПутьФайлаНастроек(type, str).ПолучитьПутьКФайлуНастроек(Метод.Чтение);
                    if (!string.IsNullOrEmpty(str2))
                    {
                        base.MainView.BeginUpdate();
                        try
                        {
                            base.MainView.RestoreLayoutFromXml(str2);
                        }
                        finally
                        {
                            base.MainView.EndUpdate();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void копироватьВБуферToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.КопироватьТекущиеСтроки();
        }

        public void КопироватьТекущиеСтроки()
        {
            int[] numArray2;
            int[] selectedRows = this.многоколоночноеПредставление.GetSelectedRows();
            if (selectedRows.Length != 0)
            {
                numArray2 = new int[selectedRows.Length];
                for (int i = 0; i < selectedRows.Length; i++)
                {
                    numArray2[i] = this.многоколоночноеПредставление.GetDataSourceRowIndex(selectedRows[i]);
                }
                this.Данные.КопироватьСтроки(numArray2);
            }
            else
            {
                numArray2 = new int[] { this.многоколоночноеПредставление.GetDataSourceRowIndex(this.многоколоночноеПредставление.FocusedRowHandle) };
                this.Данные.КопироватьСтроки(numArray2);
            }
            Сообщение.Показать("Данные выгружены в буфер обмена.\nВыполните команду вставки в браузере отчетных форм или в Excel", "Буфер обмена");
            this.многоколоночноеПредставление.ClearSelection();
            this.RefreshDataSource();
        }

        private void МенюДинамическойТаблицы_Opening(object sender, CancelEventArgs e)
        {
            if ((this.ЭкраннаяФорма != null) && (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.РедакторУвязок))
            {
                e.Cancel = true;
            }
            else if ((this.ЭкраннаяФорма == null) || (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Разработка))
            {
                foreach (ToolStripItem item in this.МенюДинамическойТаблицы.Items)
                {
                    item.Enabled = false;
                }
            }
            else
            {
                if ((this.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (this.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФормаБезРедактирования))
                {
                    this.добавитьToolStripMenuItem.Enabled = false;
                    this.удалитьToolStripMenuItem.Enabled = false;
                    this.свернутьСтрокиtoolStripMenuItem.Enabled = false;
                    this.копироватьВБуферToolStripMenuItem.Enabled = true;
                    this.вставитьИзБуфераToolStripMenuItem.Enabled = false;
                    this.печатьToolStripMenuItem.Enabled = true;
                }
                else
                {
                    this.добавитьToolStripMenuItem.Enabled = true;
                    this.вставитьИзБуфераToolStripMenuItem.Enabled = true;
                    this.печатьToolStripMenuItem.Enabled = true;
                    if (base.DefaultView.RowCount > 0)
                    {
                        this.свернутьСтрокиtoolStripMenuItem.Enabled = base.DefaultView.RowCount > 1;
                        this.удалитьToolStripMenuItem.Enabled = true;
                        this.копироватьВБуферToolStripMenuItem.Enabled = true;
                        this.печатьToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        this.свернутьСтрокиtoolStripMenuItem.Enabled = false;
                        this.удалитьToolStripMenuItem.Enabled = false;
                        this.копироватьВБуферToolStripMenuItem.Enabled = false;
                        this.печатьToolStripMenuItem.Enabled = false;
                    }
                }
                this.контПодменюИстория.Enabled = this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр;
            }
        }

        private void многоколоночноеПредставление_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if ((e.Column != null) && (e.Column is СтолбецОбъединенияДинамическойТаблицы))
            {
                СтолбецОбъединенияДинамическойТаблицы column = e.Column as СтолбецОбъединенияДинамическойТаблицы;
                int dataSourceRowIndex = this.многоколоночноеПредставление.GetDataSourceRowIndex(e.RowHandle);
                this.Данные.УстановитьЯчейкаЗаполненаВерно(dataSourceRowIndex, column.FieldName);
            }
        }

        private void многоколоночноеПредставление_CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if ((e.Column != null) && (e.Column is СтолбецОбъединенияДинамическойТаблицы))
            {
                Rectangle bounds;
                СтолбецОбъединенияДинамическойТаблицы column = e.Column as СтолбецОбъединенияДинамическойТаблицы;
                int dataSourceRowIndex = this.многоколоночноеПредставление.GetDataSourceRowIndex(e.RowHandle);
                ТипЯчейки ячейки = this.Данные.ПолучитьЯчейкуДанных(dataSourceRowIndex, column.FieldName);
                Brush brush = null;
                if (column.ОбязателенДляЗаполнения && !((ячейки != null) && ячейки.ЗначениеЗаполнено))
                {
                    brush = new HatchBrush(HatchStyle.Percent10, Color.Gold, Color.Yellow);
                    bounds = e.Bounds;
                    e.Graphics.FillRectangle(brush, bounds);
                    bounds = e.Bounds;
                    e.Appearance.DrawString(e.Cache, e.DisplayText, bounds);
                    e.Handled = true;
                }
                else if (ячейки != null)
                {
                    if ((!ячейки.Статус.ЯчейкаЗаполненаВерно && !column.ВычисляемыйСтолбец) && !column.ОписаниеЯчейки.ТолькоЧтение)
                    {
                        Brush brush2;
                        if (ячейки.Статус.ЯчейкуМожноСохранить)
                        {
                            if (brush == null)
                            {
                                brush = new HatchBrush(HatchStyle.Percent10, Color.LawnGreen, Color.LightGreen);
                            }
                            bounds = e.Bounds;
                            e.Graphics.FillRectangle(brush, bounds);
                            bounds = e.Bounds;
                            brush2 = new SolidBrush(Color.Black);
                            e.Appearance.DrawString(e.Cache, e.DisplayText, bounds, brush2);
                            e.Handled = true;
                        }
                        else
                        {
                            if (brush == null)
                            {
                                brush = new HatchBrush(HatchStyle.Percent10, Color.LightSalmon, Color.LightPink);
                            }
                            bounds = e.Bounds;
                            e.Graphics.FillRectangle(brush, bounds);
                            bounds = e.Bounds;
                            brush2 = new SolidBrush(Color.Black);
                            e.Appearance.DrawString(e.Cache, e.DisplayText, bounds, brush2);
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        ячейки.Статус.СброситьСтатус();
                    }
                }
            }
        }

        private void многоколоночноеПредставление_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            int dataSourceRowIndex = this.многоколоночноеПредставление.GetDataSourceRowIndex(e.RowHandle);
            if (this.Данные.ТаблицаДанных.МатрицаЗначений.Строки.ContainsKey(dataSourceRowIndex.ToString()))
            {
                string str;
                switch (this.Данные.ТаблицаДанных.МатрицаЗначений.Строки[dataSourceRowIndex.ToString()].НеВерноЗаполненныеЯчейки())
                {
                    case 1:
                        str = "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";
                        e.Graphics.DrawImage(Image.FromStream(new MemoryStream(Convert.FromBase64String(str))), e.Bounds);
                        e.Handled = true;
                        break;

                    case 2:
                        str = "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";
                        e.Graphics.DrawImage(Image.FromStream(new MemoryStream(Convert.FromBase64String(str))), e.Bounds);
                        e.Handled = true;
                        break;
                }
            }
        }

        private void многоколоночноеПредставление_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                if (this.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.РедакторУвязок)
                {
                    if ((e.Column as СтолбецОбъединенияДинамическойТаблицы).ВычисляемыйСтолбец)
                    {
                        e.Value = this.Данные.ПолучитьЗначениеАвтоблока(e.ListSourceRowIndex, e.Column.FieldName);
                    }
                    else
                    {
                        e.Value = this.Данные[e.ListSourceRowIndex, e.Column.FieldName];
                    }
                }
                else
                {
                    e.Value = string.Format("${0}$", e.Column.FieldName);
                }
            }
            else if (e.IsSetData)
            {
                string fieldName = e.Column.FieldName;
                СтолбецМетаструктуры метаструктуры = null;
                this.Данные.ТаблицаМетаструктуры.Столбцы.TryGetValue(fieldName, out метаструктуры);
                object obj2 = e.Value;
                if ((метаструктуры != null) && (метаструктуры.Тип.IsSubclassOf(typeof(ЧисловойТип)) && (obj2 is decimal)))
                {
                    ОписаниеЧисловогоТипа типа = метаструктуры.ОписаниеТипаЯчейки as ОписаниеЧисловогоТипа;
                    obj2 = Math.Round((decimal) obj2, типа.Точность);
                }
                this.Данные[e.ListSourceRowIndex, fieldName] = obj2;
                if ((метаструктуры != null) && ((метаструктуры.Тип == typeof(СсылкаНаСправочник)) && (this.ПослеУстановкиЗначенияЯчейкиСправочника != null)))
                {
                    this.ПослеУстановкиЗначенияЯчейкиСправочника(this, new АргументыПослеУстановкиЗначенияЯчейки(this.Данные.ТаблицаДанных, e.ListSourceRowIndex.ToString(), e.Column.FieldName));
                }
                this.значенияИзменились = true;
                this.RefreshDataSource();
            }
        }

        private void многоколоночноеПредставление_FocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            this.ОтобразитьПодсказку();
        }

        private void многоколоночноеПредставление_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            this.ОтобразитьПодсказку();
        }

        private void многоколоночноеПредставление_MouseUp(object sender, MouseEventArgs e)
        {
            if (((this.ЭкраннаяФорма != null) && (this.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.Разработка)) && (this.КонтекстноеМеню != null))
            {
                BandedGridHitInfo info = this.многоколоночноеПредставление.CalcHitInfo(base.PointToClient(Control.MousePosition));
                if ((((!info.InColumnPanel && !info.InGroupColumn) && (!info.InFilterPanel && !info.InGroupPanel)) && !info.InBandPanel) && (e.Button == MouseButtons.Right))
                {
                    this.КонтекстноеМеню.Show(this, e.X, e.Y);
                }
            }
        }

        private void многоколоночноеПредставление_ShownEditor(object sender, EventArgs e)
        {
            if (this.многоколоночноеПредставление.ActiveEditor is Барс.Интерфейс.ВыборИзСправочника)
            {
                ТипЯчейки ячейки;
                Барс.Интерфейс.ВыборИзСправочника activeEditor = this.многоколоночноеПредставление.ActiveEditor as Барс.Интерфейс.ВыборИзСправочника;
                activeEditor.ПоказыватьКнопкуРедактирования = false;
                activeEditor.ПоказыватьКнопкуОчистки = false;
                activeEditor.ТипИзменения = Барс.Интерфейс.ВыборИзСправочника.ТипРедактирования.ТолькоВыбор;
                activeEditor.НазначитьКнопки();
                СтолбецСпискаВыбораИзСправочника справочника2 = new СтолбецСпискаВыбораИзСправочника();
                справочника2.ИмяПоляОбъекта = "Код";
                справочника2.Заголовок = "Код";
                справочника2.Width = 100;
                activeEditor.ДобавитьСтолбец(справочника2);
                СтолбецСпискаВыбораИзСправочника справочника3 = new СтолбецСпискаВыбораИзСправочника();
                справочника3.ИмяПоляОбъекта = "Наименование";
                справочника3.Заголовок = "Наименование";
                справочника3.Width = 400;
                activeEditor.ДобавитьСтолбец(справочника3);
                activeEditor.Properties.PopupFormWidth = 500;
                СтолбецОбъединенияДинамическойТаблицы focusedColumn = this.многоколоночноеПредставление.FocusedColumn as СтолбецОбъединенияДинамическойТаблицы;
                activeEditor.ТипЭлементаДляВыбора = focusedColumn.ТипЭлементаДляВыбора;
                if (focusedColumn.ИсточникДанных is ВыборИзУниверсальногоСправочника)
                {
                    ВыборИзУниверсальногоСправочника справочника4 = focusedColumn.ИсточникДанных as ВыборИзУниверсальногоСправочника;
                    ячейки = this.ВыбраннаяЯчейка;
                    if ((ячейки != null) && (ячейки.Значение is ЗаписьСправочника))
                    {
                        справочника4.ВыбраннаяЗапись = ячейки.Значение as ЗаписьСправочника;
                    }
                    else
                    {
                        справочника4.ВыбраннаяЗапись = null;
                    }
                    справочника4.Данные = this.ТаблицаДанных.ДанныеФормы;
                    activeEditor.ТипЭлементаДляВыбора = null;
                    activeEditor.ИсточникДанных = справочника4;
                    if (справочника4.ОписаниеСсылки.РучнойВводТекста)
                    {
                        activeEditor.ТипИзменения = Барс.Интерфейс.ВыборИзСправочника.ТипРедактирования.ВыборИВводТекста;
                    }
                    activeEditor.ПриПолученииИсточникаЗаписейДляВыбора += new Барс.Интерфейс.ВыборИзСправочника.СобытиеПолученияИсточникаЗаписейДляВыбора(this.ВыборИзСправочника_ПолучениеИсточникаЗаписейДляВыбора);
                }
                else if (focusedColumn.ИсточникДанных is ВыборЗаписейСправочника)
                {
                    ВыборЗаписейСправочника справочника5 = focusedColumn.ИсточникДанных as ВыборЗаписейСправочника;
                    справочника5.Данные = this.ТаблицаДанных.ДанныеФормы;
                    activeEditor.ТипЭлементаДляВыбора = null;
                    activeEditor.ИсточникДанных = справочника5;
                    if (справочника5.ОписаниеСсылки.РучнойВводТекста)
                    {
                        activeEditor.ТипИзменения = Барс.Интерфейс.ВыборИзСправочника.ТипРедактирования.ВыборИВводТекста;
                    }
                    ячейки = this.ВыбраннаяЯчейка;
                    if (ячейки != null)
                    {
                        if (ячейки is МножественнаяСсылкаНаСправочник)
                        {
                            if (справочника5.МножественныйВыбор)
                            {
                                справочника5.ВыбранныеЗаписи = (ячейки as МножественнаяСсылкаНаСправочник).ЗначениеКакСписокЗаписей;
                            }
                            else
                            {
                                справочника5.ВыбраннаяЗапись = (ячейки as МножественнаяСсылкаНаСправочник).ЗначениеКакЗаписьСправочника;
                            }
                        }
                        else if (ячейки is СсылкаНаСправочник)
                        {
                            справочника5.ВыбраннаяЗапись = (ячейки as СсылкаНаСправочник).ЗначениеКакЗаписьСправочника;
                        }
                    }
                    activeEditor.ПриПолученииИсточникаЗаписейДляВыбора += new Барс.Интерфейс.ВыборИзСправочника.СобытиеПолученияИсточникаЗаписейДляВыбора(this.ВыборИзСправочника_ПолучениеИсточникаЗаписейДляВыбора);
                }
                if (focusedColumn.ColumnEdit is Барс.Интерфейс.ЭлементыТаблицы.ВыборИзСправочника)
                {
                    Барс.Интерфейс.ЭлементыТаблицы.ВыборИзСправочника columnEdit = focusedColumn.ColumnEdit as Барс.Интерфейс.ЭлементыТаблицы.ВыборИзСправочника;
                    columnEdit.ОбработчикНажатияНаКнопку = new ButtonPressedEventHandler(activeEditor.ВыборИзСправочника_Properties_ButtonClick);
                    columnEdit.ОбработчикQueryPopUp = new CancelEventHandler(activeEditor.выборИзСправочника_QueryPopUp);
                    columnEdit.ОбработчикCloseUp = new CloseUpEventHandler(activeEditor.ВыборИзСправочника_CloseUp);
                    columnEdit.ОбработчикKeyPress = new KeyPressEventHandler(activeEditor.ВыборИзСправочника_KeyPress);
                }
                if ((((this.ЭкраннаяФорма == null) || (this.ВариантОткрытия == ВариантОткрытияФормы.Чтение)) || (this.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.Просмотр)) && !МенеджерБД.МенеджерИнициализирован)
                {
                    activeEditor.ТипИзменения = Барс.Интерфейс.ВыборИзСправочника.ТипРедактирования.Запрещено;
                }
            }
        }

        private void многоколоночноеПредставление_ИзменениеФильтра(object sender, EventArgs e)
        {
            if (this.ТаблицаДанных != null)
            {
                this.ТаблицаДанных.МатрицаЗначений.СтрокиФильтра.Clear();
                if (!this.многоколоночноеПредставление.ActiveFilter.IsEmpty)
                {
                    int dataSourceRowIndex = -1;
                    СтрокаДанных данных = null;
                    for (int i = 0; i < this.многоколоночноеПредставление.RowCount; i++)
                    {
                        dataSourceRowIndex = this.многоколоночноеПредставление.GetDataSourceRowIndex(i);
                        данных = this.ТаблицаДанных.МатрицаЗначений[dataSourceRowIndex];
                        if (данных != null)
                        {
                            this.ТаблицаДанных.МатрицаЗначений.СтрокиФильтра.Add(данных.КодСтроки);
                        }
                    }
                }
            }
        }

        public void ОбновитьЗначения()
        {
            if (this.РазмещатьНаЗакладке)
            {
                this.BeginUpdate();
                this.RefreshDataSource();
                this.EndUpdate();
            }
        }

        private void ОтобразитьПодсказку()
        {
            if ((this.ЭкраннаяФорма != null) && this.РазмещатьНаЗакладке)
            {
                if (this.РазмещатьНаЗакладке)
                {
                    this.ЭкраннаяФорма.ОтобразитьПодсказку("");
                }
                string str = "";
                if (this.многоколоночноеПредставление.FocusedColumn != null)
                {
                    if (this.многоколоночноеПредставление.FocusedColumn is СтолбецОбъединенияДинамическойТаблицы)
                    {
                        СтолбецОбъединенияДинамическойТаблицы focusedColumn = this.многоколоночноеПредставление.FocusedColumn as СтолбецОбъединенияДинамическойТаблицы;
                        if (focusedColumn == null)
                        {
                            return;
                        }
                        int focusedRowHandle = this.многоколоночноеПредставление.FocusedRowHandle;
                        focusedRowHandle = this.многоколоночноеПредставление.GetDataSourceRowIndex(focusedRowHandle);
                        ТипЯчейки ячейки = this.таблицаДанных[focusedRowHandle, focusedColumn.FieldName];
                        str = "Ячейка : " + focusedColumn.FieldName;
                        if (ячейки != null)
                        {
                            str = str + (string.IsNullOrEmpty(str) ? "" : " ") + "Значение : " + ячейки.ЗначениеСтрокой;
                        }
                        if (!string.IsNullOrEmpty(focusedColumn.Автоблок))
                        {
                            str = str + (string.IsNullOrEmpty(str) ? "" : " ") + "Автоблок : " + focusedColumn.Автоблок;
                        }
                        if (((ячейки != null) && (ячейки.Описание != null)) && ячейки.Описание.ОбязательноДляЗаполнения)
                        {
                            str = str + (string.IsNullOrEmpty(str) ? "" : " ") + "Обязательна для заполнения";
                        }
                        if (!((ячейки == null) || string.IsNullOrEmpty(ячейки.Описание.Комментарий)))
                        {
                            str = str + (string.IsNullOrEmpty(str) ? "" : " ") + string.Format("Комментарий : {0}", ячейки.Описание.Комментарий);
                        }
                        if (((ячейки != null) && (ячейки.Описание is ОписаниеСтроковогоТипаЯчейки)) && !string.IsNullOrEmpty((ячейки.Описание as ОписаниеСтроковогоТипаЯчейки).МаскаВвода))
                        {
                            str = str + (string.IsNullOrEmpty(str) ? "" : " ") + "Маска заполнения : " + (ячейки.Описание as ОписаниеСтроковогоТипаЯчейки).МаскаВвода;
                        }
                    }
                    if (this.РазмещатьНаЗакладке)
                    {
                        this.ЭкраннаяФорма.ОтобразитьПодсказку(str);
                    }
                }
            }
        }

        public void ПересчитатьАвтоблоки()
        {
            this.данные = new ДанныеДинамическойТаблицы(this.таблицаМетаструктуры, this.таблицаДанных);
            for (int i = 0; i < this.многоколоночноеПредставление.Columns.Count; i++)
            {
                СтолбецОбъединенияДинамическойТаблицы таблицы = (СтолбецОбъединенияДинамическойТаблицы) this.многоколоночноеПредставление.Columns[i];
                if (таблицы.ВычисляемыйСтолбец)
                {
                    this.данные.ДобавитьАвтоблок(таблицы.ИмяПоляИсточникаДанных, таблицы.Автоблок);
                }
            }
            this.данные.ПересчитатьЗначенияАвтоблоков();
            if (this.РазмещатьНаЗакладке)
            {
                this.BeginUpdate();
                this.RefreshDataSource();
                this.EndUpdate();
            }
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Файлы Excel | *.xls";
            if ((dialog.ShowDialog() == DialogResult.OK) && base.IsPrintingAvailable)
            {
                try
                {
                    base.ExportToXls(dialog.FileName, new XlsExportOptions(true, true));
                    ОткрытиеФайлов.ОткрытьФайл(dialog.FileName);
                }
                catch (TargetInvocationException exception)
                {
                    if ((exception.InnerException != null) && exception.InnerException.Message.ToUpper().Contains("NOT A LEGAL OLEAUT DATE"))
                    {
                        Сообщение.ПоказатьОшибку("Печать таблицы невозможна. Проверьте корректность введенных дат.");
                    }
                }
            }
        }

        private void ПолучитьОбъединение(ТаблицаОтчетнойФормы Таблица, ОбъединениеДинамическойТаблицы РодительскоеОбъединение, ref int Row, ref int Col, int RowIndex)
        {
            int num;
            int num2;
            int num3;
            string str;
            string str2;
            СтолбецОбъединенияДинамическойТаблицы таблицы2;
            ОбъединениеДинамическойТаблицы таблицы = new ОбъединениеДинамическойТаблицы();
            if (РодительскоеОбъединение == null)
            {
                this.ДобавитьОбъединение(таблицы);
            }
            else
            {
                РодительскоеОбъединение.ДобавитьОбъединение(таблицы);
            }
            GridRangeInfo range = null;
            Таблица.CoveredRanges.Find(Row, Col, out range);
            if (RowIndex == 2)
            {
                if (Таблица[Row, Col].CellValue != null)
                {
                    таблицы.Заголовок = Таблица[RowIndex + 1, Col].CellValue.ToString();
                    num = 0;
                    for (num2 = 0; num2 <= range.Height; num2++)
                    {
                        num += Таблица.RowHeights[Row + num2];
                    }
                    if (Таблица.RowHeights[RowIndex + 1] != 0)
                    {
                        таблицы.КоличествоСтрок = num / Таблица.RowHeights[RowIndex + 1];
                    }
                    else
                    {
                        таблицы.КоличествоСтрок = 1;
                    }
                }
                num3 = 0;
                while (num3 < range.Width)
                {
                    str = Таблица[RowIndex + 1, num3 + range.Left].CellValue.ToString();
                    str2 = Таблица[RowIndex, num3 + range.Left].CellValue.ToString();
                    таблицы2 = new СтолбецОбъединенияДинамическойТаблицы();
                    таблицы.ДобавитьСтолбец(таблицы2);
                    таблицы2.Заголовок = str;
                    таблицы2.ИмяПоляИсточникаДанных = str2;
                    таблицы2.ШиринаСтолбца = Таблица.ColWidths[num3 + range.Left];
                    if (Таблица[RowIndex + 2, 1].CellValue.ToString().Trim().ToLower() == "#шаблонстроки")
                    {
                        таблицы2.ВыставитьШаблон(Таблица[RowIndex + 2, num3 + range.Left].CellValue.ToString());
                    }
                    num3++;
                }
                Row = RowIndex;
                Col = range.Right;
            }
            else if (range.Bottom != (RowIndex - 1))
            {
                if (Таблица[Row, Col].CellValue != null)
                {
                    таблицы.Заголовок = Таблица[Row, Col].CellValue.ToString();
                }
                for (num3 = Col; num3 <= range.Right; num3++)
                {
                    Row = range.Bottom + 1;
                    this.ПолучитьОбъединение(Таблица, таблицы, ref Row, ref num3, RowIndex);
                }
                Col = range.Right;
            }
            else
            {
                if (Таблица[Row, Col].CellValue != null)
                {
                    таблицы.Заголовок = Таблица[Row, Col].CellValue.ToString();
                    num = 0;
                    for (num2 = 0; num2 <= range.Height; num2++)
                    {
                        num += Таблица.RowHeights[Row + num2];
                    }
                    if (Таблица.RowHeights[RowIndex + 1] != 0)
                    {
                        таблицы.КоличествоСтрок = num / Таблица.RowHeights[RowIndex + 1];
                    }
                    else
                    {
                        таблицы.КоличествоСтрок = 1;
                    }
                }
                for (num3 = 0; num3 < range.Width; num3++)
                {
                    str = Таблица[RowIndex + 1, num3 + range.Left].CellValue.ToString();
                    str2 = Таблица[RowIndex, num3 + range.Left].CellValue.ToString();
                    таблицы2 = new СтолбецОбъединенияДинамическойТаблицы();
                    таблицы.ДобавитьСтолбец(таблицы2);
                    таблицы2.Заголовок = str;
                    таблицы2.ИмяПоляИсточникаДанных = str2;
                    таблицы2.ШиринаСтолбца = Таблица.ColWidths[num3 + range.Left];
                    if (Таблица[RowIndex + 2, 1].CellValue.ToString().Trim().ToLower() == "#шаблонстроки")
                    {
                        таблицы2.ВыставитьШаблон(Таблица[RowIndex + 2, num3 + range.Left].CellValue.ToString());
                    }
                }
                Row = RowIndex;
                Col = range.Right;
            }
        }

        private void ПостроитьОбъединение(ОбъединениеДинамическойТаблицы Объединение, XPathNavigator Навигатор)
        {
            if (Навигатор.MoveToChild("Объединение", ""))
            {
                do
                {
                    ОбъединениеДинамическойТаблицы таблицы = new ОбъединениеДинамическойТаблицы();
                    Объединение.ДобавитьОбъединение(таблицы);
                    таблицы.Заголовок = Навигатор.GetAttribute("Заголовок", "");
                    string attribute = Навигатор.GetAttribute("КолвоСтрок", "");
                    if (!string.IsNullOrEmpty(attribute))
                    {
                        таблицы.КоличествоСтрок = int.Parse(attribute);
                    }
                    this.ПостроитьОбъединение(таблицы, Навигатор.CreateNavigator());
                }
                while (Навигатор.MoveToNext("Объединение", ""));
            }
            else if (Навигатор.MoveToChild("Столбец", ""))
            {
                СтолбецОбъединенияДинамическойТаблицы таблицы2 = new СтолбецОбъединенияДинамическойТаблицы();
                Объединение.ДобавитьСтолбец(таблицы2);
                таблицы2.Заголовок = Навигатор.GetAttribute("Заголовок", "");
                таблицы2.ИмяПоляИсточникаДанных = Навигатор.GetAttribute("Код", "");
                string str2 = Навигатор.GetAttribute("Ширина", "");
                if (!string.IsNullOrEmpty(str2))
                {
                    таблицы2.ШиринаСтолбца = int.Parse(str2);
                }
                string str3 = Навигатор.GetAttribute("Автоблок", "");
                if (!string.IsNullOrEmpty(str3))
                {
                    if (this.ЭкраннаяФорма != null)
                    {
                        str3 = this.ЭкраннаяФорма.ПроанализироватьСсылкиНаКонстанты(str3);
                    }
                    таблицы2.Автоблок = str3;
                    таблицы2.ВыставитьСтильВычисляемогоСтолбца();
                }
                if (((this.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.РедакторУвязок) && (this.ТаблицаМетаструктуры != null)) && this.ТаблицаМетаструктуры.Столбцы.ContainsKey(таблицы2.ИмяПоляИсточникаДанных))
                {
                    СтолбецМетаструктуры метаструктуры = this.ТаблицаМетаструктуры.Столбцы[таблицы2.ИмяПоляИсточникаДанных];
                    if (((метаструктуры != null) && (метаструктуры.ОписаниеТипаЯчейки != null)) && метаструктуры.ОписаниеТипаЯчейки.ТолькоЧтение)
                    {
                        таблицы2.ВыставитьСтильВычисляемогоСтолбца();
                    }
                    таблицы2.ВыставитьТипСтолбца(метаструктуры, this.ЭкраннаяФорма.ОтчетнаяФорма);
                }
            }
        }

        private void ПостроитьОбъединения(XPathNavigator Навигатор)
        {
            if (Навигатор.MoveToChild("Объединение", ""))
            {
                do
                {
                    ОбъединениеДинамическойТаблицы таблицы = new ОбъединениеДинамическойТаблицы();
                    this.ДобавитьОбъединение(таблицы);
                    if (this.многоколоночноеПредставление.Bands.Count <= this.КоличествоФиксированныхСтолбцов)
                    {
                        таблицы.Fixed = FixedStyle.Left;
                    }
                    таблицы.Заголовок = Навигатор.GetAttribute("Заголовок", "");
                    string attribute = Навигатор.GetAttribute("КолвоСтрок", "");
                    if (!string.IsNullOrEmpty(attribute))
                    {
                        таблицы.КоличествоСтрок = int.Parse(attribute);
                    }
                    this.ПостроитьОбъединение(таблицы, Навигатор.CreateNavigator());
                }
                while (Навигатор.MoveToNext("Объединение", ""));
            }
        }

        public void ПостроитьТаблицуПоМодели(ТаблицаОтчетнойФормы Таблица)
        {
            this.КодТаблицы = Таблица.КодТаблицы;
            this.КоличествоФиксированныхСтолбцов = Таблица.Cols.FrozenCount;
            this.ИмяЛиста = Таблица.ИмяЛиста;
            this.Наименование = Таблица.Наименование;
            int rowIndex = -1;
            int row = 0;
            while (row <= Таблица.RowCount)
            {
                if ((Таблица[row, 1].CellValue != null) && (Таблица[row, 1].CellValue.ToString().ToLower() == "#кодыстолбцов"))
                {
                    rowIndex = row;
                }
                row++;
            }
            if (rowIndex != -1)
            {
                for (int i = 2; i <= Таблица.ColCount; i++)
                {
                    row = 2;
                    this.ПолучитьОбъединение(Таблица, null, ref row, ref i, rowIndex);
                }
            }
        }

        public void ПроанализироватьЗапретыДоступаКЭлементамФормы(Dictionary<string, string> СписокОграничений)
        {
            if (СписокОграничений.ContainsKey("Таблица"))
            {
                if (СписокОграничений["Таблица"] == "ЗапретРедактирования")
                {
                    if (base.MainView is AdvBandedGridView)
                    {
                        foreach (BandedGridColumn column in ((AdvBandedGridView) base.MainView).Columns)
                        {
                            column.AppearanceCell.Options.UseBackColor = true;
                            column.AppearanceCell.BackColor = Color.FromArgb(210, 220, 0xff);
                            column.OptionsColumn.AllowEdit = false;
                        }
                    }
                }
                else if ((СписокОграничений["Таблица"] == "ЗапретПросмотра") && (base.MainView is AdvBandedGridView))
                {
                    foreach (BandedGridColumn column in ((AdvBandedGridView) base.MainView).Columns)
                    {
                        column.Visible = false;
                    }
                }
            }
            else
            {
                foreach (string str in СписокОграничений.Keys)
                {
                    if (base.MainView is AdvBandedGridView)
                    {
                        foreach (BandedGridColumn column in ((AdvBandedGridView) base.MainView).Columns)
                        {
                            if ((column is СтолбецОбъединенияДинамическойТаблицы) && (((СтолбецОбъединенияДинамическойТаблицы) column).ИмяПоляИсточникаДанных == str))
                            {
                                if (СписокОграничений[str] == "ЗапретПросмотра")
                                {
                                    column.OwnerBand.Visible = false;
                                    break;
                                }
                                if (СписокОграничений[str] == "ЗапретРедактирования")
                                {
                                    column.AppearanceCell.Options.UseBackColor = true;
                                    column.AppearanceCell.BackColor = Color.FromArgb(210, 220, 0xff);
                                    column.OptionsColumn.AllowEdit = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool ПроверитьЗаполненностьДанных()
        {
            Dictionary<string, List<string>> dictionary = null;
            bool flag = this.ПроверитьЗаполненностьДанных(out dictionary);
            dictionary = null;
            return flag;
        }

        public bool ПроверитьЗаполненностьДанных(out Dictionary<string, List<string>> КоординатыНезаполненныхЯчеек)
        {
            КоординатыНезаполненныхЯчеек = new Dictionary<string, List<string>>();
            if (((this.Данные == null) || (this.Данные.ТаблицаДанных == null)) || (this.Данные.ТаблицаДанных.МатрицаЗначений.Количество == 0))
            {
                return true;
            }
            bool flag = true;
            for (int i = 0; i < this.многоколоночноеПредставление.Columns.Count; i++)
            {
                if (this.многоколоночноеПредставление.Columns[i] is СтолбецОбъединенияДинамическойТаблицы)
                {
                    СтолбецОбъединенияДинамическойТаблицы таблицы = this.многоколоночноеПредставление.Columns[i] as СтолбецОбъединенияДинамическойТаблицы;
                    if (таблицы.ОбязателенДляЗаполнения)
                    {
                        List<string> list = null;
                        if (таблицы.ЕстьНезаполненныеЯчейки(this.Данные, out list))
                        {
                            flag = false;
                            КоординатыНезаполненныхЯчеек.Add(таблицы.FieldName, list);
                        }
                    }
                }
            }
            return flag;
        }

        public bool ПроверитьЗаполненностьДанных(out string Сообщение)
        {
            Сообщение = "";
            Dictionary<string, List<string>> dictionary = null;
            bool flag = this.ПроверитьЗаполненностьДанных(out dictionary);
            if (!flag)
            {
                foreach (KeyValuePair<string, List<string>> pair in dictionary)
                {
                    Сообщение = Сообщение + (string.IsNullOrEmpty(Сообщение) ? "" : "\r\n") + "Столбец " + pair.Key;
                    foreach (string str in pair.Value)
                    {
                        Сообщение = Сообщение + "\r\n\tСтрока " + str.ToString();
                    }
                    Сообщение = Сообщение + "\r\n";
                }
            }
            return flag;
        }

        public void СброситьНастройки()
        {
            System.Type type = this.типФормы;
            try
            {
                if (type != null)
                {
                    string str = this.Заголовок + ".Расположение";
                    string path = new ПутьФайлаНастроек(type, str).ПолучитьПутьКФайлуНастроек(Метод.Чтение);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                        this.настройкиСброшены = true;
                        Сообщение.Показать("Настройки таблицы сброшены! Расположение столбцов по умолчанию вступит в силу после следующего открытия этой формы");
                    }
                }
            }
            catch (Exception exception)
            {
                Сообщение.ПоказатьПредупреждение("Не удалось сбросить настройки. Причина: ", exception);
            }
        }

        private void СвернутьСтроки()
        {
            this.Данные.ТаблицаДанных.СвернутьСтроки();
            this.многоколоночноеПредставление.RefreshData();
            this.значенияИзменились = true;
            this.RefreshDataSource();
            if (this.ПриИзмененииДанных != null)
            {
                this.ПриИзмененииДанных(this);
            }
        }

        private void свернутьСтрокиtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Сообщение.ПоказатьВопрос("Вы действительно хотите свернуть строки таблицы ?") == РезультатДиалога.Да)
            {
                this.СвернутьСтроки();
            }
        }

        public XmlDocument СериализоватьВXML()
        {
            XmlDocument document = new XmlDocument();
            XmlElement newChild = document.CreateElement("ЭлементыТаблицы");
            document.AppendChild(newChild);
            XmlElement element2 = document.CreateElement("Объединения");
            newChild.AppendChild(element2);
            if (this.КоличествоФиксированныхСтолбцов > 0)
            {
                element2.SetAttribute("ФиксСтолбцов", this.КоличествоФиксированныхСтолбцов.ToString());
            }
            foreach (ОбъединениеДинамическойТаблицы таблицы in this.многоколоночноеПредставление.Bands)
            {
                element2.AppendChild(this.СериализоватьОбъединение(таблицы, document));
            }
            return document;
        }

        private XmlElement СериализоватьОбъединение(ОбъединениеДинамическойТаблицы Объединение, XmlDocument Файл)
        {
            XmlElement element = Файл.CreateElement("Объединение");
            element.SetAttribute("Заголовок", Объединение.Заголовок);
            element.SetAttribute("КолвоСтрок", Объединение.КоличествоСтрок.ToString());
            if (Объединение.Children.Count != 0)
            {
                foreach (ОбъединениеДинамическойТаблицы таблицы in Объединение.Children)
                {
                    element.AppendChild(this.СериализоватьОбъединение(таблицы, Файл));
                }
                return element;
            }
            foreach (СтолбецОбъединенияДинамическойТаблицы таблицы2 in Объединение.Columns)
            {
                XmlElement newChild = Файл.CreateElement("Столбец");
                element.AppendChild(newChild);
                newChild.SetAttribute("Заголовок", таблицы2.Заголовок);
                newChild.SetAttribute("Код", таблицы2.ИмяПоляИсточникаДанных);
                newChild.SetAttribute("Ширина", таблицы2.ШиринаСтолбца.ToString());
                newChild.SetAttribute("Автоблок", таблицы2.Автоблок);
            }
            return element;
        }

        public void СоздатьЗеркалоДанных()
        {
            if ((this.таблицаМетаструктуры != null) && (this.таблицаДанных != null))
            {
                if (this.РазмещатьНаЗакладке)
                {
                    this.данные = new ДанныеДинамическойТаблицы(this.таблицаМетаструктуры, this.таблицаДанных);
                }
                else
                {
                    this.зеркалоДанных = new Барс.Своды.ОтчетнаяФорма.ТаблицаДанных(this.таблицаДанных.ДанныеФормы, this.таблицаДанных.КодТаблицы);
                    this.зеркалоДанных.КопироватьТаблицу(this.таблицаДанных);
                    this.данные = new ДанныеДинамическойТаблицы(this.таблицаМетаструктуры, this.зеркалоДанных);
                }
                for (int i = 0; i < this.многоколоночноеПредставление.Columns.Count; i++)
                {
                    СтолбецОбъединенияДинамическойТаблицы таблицы = (СтолбецОбъединенияДинамическойТаблицы) this.многоколоночноеПредставление.Columns[i];
                    if (таблицы.ВычисляемыйСтолбец)
                    {
                        this.данные.ДобавитьАвтоблок(таблицы.ИмяПоляИсточникаДанных, таблицы.Автоблок);
                    }
                }
                this.данные.СортироватьАвтоблоки();
                this.данные.ПересчитатьЗначенияАвтоблоков();
                this.DataSource = this.данные;
                this.значенияИзменились = false;
                if (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.РедакторУвязок)
                {
                    this.ДобавитьСтроку();
                }
            }
        }

        public void СохранениеНастроек(System.Type ТипФормы, string НазваниеКомпонента)
        {
            if ((((this.ЭкраннаяФорма != null) && (this.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр)) && !this.настройкиСброшены) && ((ТипФормы != null) && !string.IsNullOrEmpty(НазваниеКомпонента)))
            {
                this.типФормы = ТипФормы;
                System.Type type = ТипФормы;
                try
                {
                    string str = НазваниеКомпонента + ".Расположение";
                    ПутьФайлаНастроек настроек = new ПутьФайлаНастроек(type, str);
                    string xmlFile = настроек.ПолучитьПутьКФайлуНастроек(Метод.Запись) + @"\" + str + ".xml";
                    base.MainView.SaveLayoutToXml(xmlFile);
                }
                catch (Exception)
                {
                }
            }
        }

        public void СохранитьЗеркалоДанных()
        {
            if (!this.РазмещатьНаЗакладке)
            {
                this.данные.ПересчитыватьАвтоблоки = false;
                this.зеркалоДанных.СортироватьДанные();
                this.таблицаДанных.КопироватьТаблицу(this.зеркалоДанных);
                this.значенияИзменились = false;
            }
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.УдалитьТекущиеСтроки();
        }

        private void удалитьНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.СброситьНастройки();
        }

        public void УдалитьТекущиеСтроки()
        {
            if (Сообщение.ПоказатьВопрос("Вы действительно хотите удалить строки?", "Удаление строки") == РезультатДиалога.Да)
            {
                int[] numArray2;
                int[] selectedRows = this.многоколоночноеПредставление.GetSelectedRows();
                if (selectedRows.Length != 0)
                {
                    numArray2 = new int[selectedRows.Length];
                    for (int i = 0; i < selectedRows.Length; i++)
                    {
                        numArray2[i] = this.многоколоночноеПредставление.GetDataSourceRowIndex(selectedRows[i]);
                    }
                    this.Данные.УдалитьСтроки(numArray2);
                }
                else
                {
                    numArray2 = new int[] { this.многоколоночноеПредставление.GetDataSourceRowIndex(this.многоколоночноеПредставление.FocusedRowHandle) };
                    this.Данные.УдалитьСтроки(numArray2);
                }
                this.многоколоночноеПредставление.ClearSelection();
                this.RefreshDataSource();
                if (this.ПриИзмененииДанных != null)
                {
                    this.ПриИзмененииДанных(this);
                }
                this.значенияИзменились = true;
            }
        }

        public void УстановитьНеверноЗаполненыеЯчейки(List<СтрокаОтчетаСверкиДанных> Список)
        {
            МатрицаЗначений значений = this.Данные.ТаблицаДанных.МатрицаЗначений;
            if ((Список != null) && (Список.Count != 0))
            {
                foreach (СтрокаОтчетаСверкиДанных данных in Список)
                {
                    if (данных.Субтаблица == this.Данные.ТаблицаДанных.КодТаблицы)
                    {
                        string[] strArray = данных.Строка.Replace(" ", "").Split(new char[] { ';' });
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            int num2 = Convert.ToInt32(strArray[i]);
                            if (значений.СодержитСтроку(num2))
                            {
                                if (!string.IsNullOrEmpty(данных.Столбец))
                                {
                                    значений[num2, данных.Столбец].Статус.ЯчейкаЗаполненаВерно = false;
                                    значений[num2, данных.Столбец].Статус.ТекстОшибкиЗаполнения = данных.ТипОшибки;
                                    значений[num2, данных.Столбец].Статус.ЯчейкуМожноСохранить = данных.СохранениеРазрешено;
                                }
                                else
                                {
                                    значений.Строки[strArray[i]].ОтметитьНеверноЗаполненныеКлючевыеСтолбцы(данных.ТипОшибки, данных.СохранениеРазрешено, false);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void элементИстория_Click(object Отправитель, АргументыСобытия Аргументы)
        {
            int focusedRowHandle = this.многоколоночноеПредставление.FocusedRowHandle;
            int dataSourceRowIndex = this.многоколоночноеПредставление.GetDataSourceRowIndex(focusedRowHandle);
            if (dataSourceRowIndex >= 0)
            {
                string str = this.Данные.ТаблицаДанных.ХешКодКлючевыхСтолбцов(dataSourceRowIndex.ToString(), false);
                string str2 = this.Данные.ТаблицаДанных.ХешКодКлючевыхСтолбцов(dataSourceRowIndex.ToString(), true);
                string fieldName = this.многоколоночноеПредставление.FocusedColumn.FieldName;
                ТипПостроенияИсторииСборки tag = (ТипПостроенияИсторииСборки) (Отправитель as ToolStripMenuItem).Tag;
                if (this.ТаблицаМетаструктуры != null)
                {
                    string str4 = "";
                    if (((this.ЭкраннаяФорма != null) && (this.ЭкраннаяФорма.ОтчетнаяФорма != null)) && (this.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура != null))
                    {
                        str4 = this.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура.Код;
                    }
                    ФормаИсторииСборки сборки2 = new ФормаИсторииСборки(str4);
                    сборки2.ТипПостроенияИстории = tag;
                    сборки2.СтолбецПостроения = fieldName;
                    if (сборки2.Загрузить(this.ЭкраннаяФорма.ОтчетнаяФорма, this.ТаблицаМетаструктуры.Идентификатор, str, str2) && сборки2.Построить())
                    {
                        сборки2.ShowDialog();
                    }
                    else
                    {
                        Сообщение.Показать("История сборки данной строки отсутствует.");
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

        public ТипЯчейки ВыбраннаяЯчейка
        {
            get
            {
                if (((this.Данные == null) || (this.Данные.ТаблицаДанных == null)) || (this.Данные.ТаблицаДанных.МатрицаЗначений.Количество == 0))
                {
                    return null;
                }
                if ((this.многоколоночноеПредставление.FocusedColumn == null) || (this.многоколоночноеПредставление.FocusedRowHandle < 0))
                {
                    return null;
                }
                int dataSourceRowIndex = this.многоколоночноеПредставление.GetDataSourceRowIndex(this.многоколоночноеПредставление.FocusedRowHandle);
                string fieldName = this.многоколоночноеПредставление.FocusedColumn.FieldName;
                return this.Данные.ТаблицаДанных[dataSourceRowIndex, fieldName];
            }
        }

        public int ВысотаЗаголовка
        {
            get
            {
                return this.многоколоночноеПредставление.BandPanelRowHeight;
            }
            set
            {
                this.многоколоночноеПредставление.BandPanelRowHeight = value;
            }
        }

        public ДанныеДинамическойТаблицы Данные
        {
            get
            {
                return this.данные;
            }
        }

        public string Заголовок
        {
            get
            {
                string str = this.КодТаблицы.Replace('_', ' ');
                if (!string.IsNullOrEmpty(this.Наименование))
                {
                    str = this.Наименование.Replace('_', ' ');
                }
                return str;
            }
        }

        public bool ЗначенияИзменились
        {
            get
            {
                return this.значенияИзменились;
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

        public int КоличествоФиксированныхСтолбцов
        {
            get
            {
                return this.количествоФиксированныхСтолбцов;
            }
            set
            {
                this.количествоФиксированныхСтолбцов = value;
            }
        }

        public ContextMenuStrip КонтекстноеМеню
        {
            get
            {
                return this.контекстноеМеню;
            }
            set
            {
                this.контекстноеМеню = value;
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
                return this.размещатьНаЗакладке;
            }
            set
            {
                this.размещатьНаЗакладке = value;
            }
        }

        public bool РазрешитьРедактирование
        {
            get
            {
                return this.многоколоночноеПредставление.OptionsBehavior.Editable;
            }
            set
            {
                this.многоколоночноеПредставление.OptionsBehavior.Editable = value;
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

