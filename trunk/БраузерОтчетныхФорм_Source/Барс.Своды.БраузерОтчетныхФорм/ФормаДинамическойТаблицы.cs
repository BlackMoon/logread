namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Барс;
    using Барс.Интерфейс;
    using Барс.Клиент;
    using Барс.Своды;
    using Барс.Типы;

    public class ФормаДинамическойТаблицы : XtraForm
    {
        private Bar bar1;
        private Bar bar2;
        private Bar bar3;
        private BarButtonItem barButtonItem1;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private IContainer components;
        private EmptySpaceItem emptySpaceItem_bottom;
        private EmptySpaceItem emptySpaceItem_top;
        private LabelControl labelControl_заголовок;
        private LayoutControlItem layoutControlItem_заголовок;
        private LayoutControlItem layoutControlItem_отмена;
        private LayoutControlItem layoutControlItem_применить;
        private LayoutControlItem layoutControlItem_сохранить;
        private LayoutControl layoutMain;
        private LayoutControlGroup layoutMainGroup;
        private SimpleButton simpleButton_отмена;
        private SimpleButton simpleButton_применить;
        private SimpleButton simpleButton_сохранить;
        private Control начальныйРодительТаблицы = null;
        private ДинамическаяТаблица таблица = null;
        private LayoutControlItem таблицаItem = null;

        public ФормаДинамическойТаблицы()
        {
            base.WindowState = FormWindowState.Maximized;
            this.InitializeComponent();
            this.simpleButton_сохранить.Click += new EventHandler(this.simpleButton_сохранить_Click);
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Таблица.ЭкраннаяФорма.ОткрытьМетодическийСправочник(this as ИнтерфейсОтображаемойТаблицыЭкраннойФормы);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ФормаДинамическойТаблицы));
            this.layoutMain = new LayoutControl();
            this.simpleButton_сохранить = new SimpleButton();
            this.labelControl_заголовок = new LabelControl();
            this.simpleButton_отмена = new SimpleButton();
            this.simpleButton_применить = new SimpleButton();
            this.layoutMainGroup = new LayoutControlGroup();
            this.layoutControlItem_отмена = new LayoutControlItem();
            this.emptySpaceItem_bottom = new EmptySpaceItem();
            this.layoutControlItem_применить = new LayoutControlItem();
            this.emptySpaceItem_top = new EmptySpaceItem();
            this.layoutControlItem_заголовок = new LayoutControlItem();
            this.layoutControlItem_сохранить = new LayoutControlItem();
            this.barManager1 = new BarManager(this.components);
            this.bar1 = new Bar();
            this.bar2 = new Bar();
            this.barButtonItem1 = new BarButtonItem();
            this.bar3 = new Bar();
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.layoutMain.BeginInit();
            this.layoutMain.SuspendLayout();
            this.layoutMainGroup.BeginInit();
            this.layoutControlItem_отмена.BeginInit();
            this.emptySpaceItem_bottom.BeginInit();
            this.layoutControlItem_применить.BeginInit();
            this.emptySpaceItem_top.BeginInit();
            this.layoutControlItem_заголовок.BeginInit();
            this.layoutControlItem_сохранить.BeginInit();
            this.barManager1.BeginInit();
            base.SuspendLayout();
            this.layoutMain.Appearance.DisabledLayoutGroupCaption.ForeColor = SystemColors.GrayText;
            this.layoutMain.Appearance.DisabledLayoutGroupCaption.Options.UseForeColor = true;
            this.layoutMain.Appearance.DisabledLayoutItem.ForeColor = SystemColors.GrayText;
            this.layoutMain.Appearance.DisabledLayoutItem.Options.UseForeColor = true;
            this.layoutMain.Controls.Add(this.simpleButton_сохранить);
            this.layoutMain.Controls.Add(this.labelControl_заголовок);
            this.layoutMain.Controls.Add(this.simpleButton_отмена);
            this.layoutMain.Controls.Add(this.simpleButton_применить);
            this.layoutMain.Dock = DockStyle.Fill;
            this.layoutMain.Location = new Point(0, 0x33);
            this.layoutMain.Name = "layoutMain";
            this.layoutMain.Root = this.layoutMainGroup;
            this.layoutMain.Size = new Size(0x23f, 0x17f);
            this.layoutMain.TabIndex = 0;
            this.layoutMain.Text = "layoutControl1";
            this.layoutMain.KeyUp += new KeyEventHandler(this.layoutMain_KeyUp);
            this.simpleButton_сохранить.Location = new Point(260, 0x1f);
            this.simpleButton_сохранить.Name = "simpleButton_сохранить";
            this.simpleButton_сохранить.Size = new Size(0x67, 0x16);
            this.simpleButton_сохранить.StyleController = this.layoutMain;
            this.simpleButton_сохранить.TabIndex = 8;
            this.simpleButton_сохранить.Text = "Сохранить форму";
            this.labelControl_заголовок.Appearance.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
            this.labelControl_заголовок.Appearance.Options.UseFont = true;
            this.labelControl_заголовок.Location = new Point(7, 7);
            this.labelControl_заголовок.Name = "labelControl_заголовок";
            this.labelControl_заголовок.Size = new Size(0x3e, 13);
            this.labelControl_заголовок.StyleController = this.layoutMain;
            this.labelControl_заголовок.TabIndex = 7;
            this.labelControl_заголовок.Text = "Заголовок";
            this.simpleButton_отмена.DialogResult = DialogResult.Cancel;
            this.simpleButton_отмена.Location = new Point(0x1dd, 0x1f);
            this.simpleButton_отмена.Name = "simpleButton_отмена";
            this.simpleButton_отмена.Size = new Size(0x5c, 0x16);
            this.simpleButton_отмена.StyleController = this.layoutMain;
            this.simpleButton_отмена.TabIndex = 5;
            this.simpleButton_отмена.Text = "Отмена";
            this.simpleButton_отмена.KeyUp += new KeyEventHandler(this.simpleButton_отмена_KeyUp);
            this.simpleButton_применить.Location = new Point(0x176, 0x1f);
            this.simpleButton_применить.Name = "simpleButton_применить";
            this.simpleButton_применить.Size = new Size(0x5c, 0x16);
            this.simpleButton_применить.StyleController = this.layoutMain;
            this.simpleButton_применить.TabIndex = 6;
            this.simpleButton_применить.Text = "Применить";
            this.simpleButton_применить.Click += new EventHandler(this.simpleButton_применить_Click);
            this.simpleButton_применить.KeyUp += new KeyEventHandler(this.simpleButton_применить_KeyUp);
            this.layoutMainGroup.CustomizationFormText = "layoutControlGroup1";
            this.layoutMainGroup.Items.AddRange(new BaseLayoutItem[] { this.layoutControlItem_отмена, this.emptySpaceItem_bottom, this.layoutControlItem_применить, this.emptySpaceItem_top, this.layoutControlItem_заголовок, this.layoutControlItem_сохранить });
            this.layoutMainGroup.Location = new Point(0, 0);
            this.layoutMainGroup.Name = "layoutMainGroup";
            this.layoutMainGroup.Size = new Size(0x23f, 0x17f);
            this.layoutMainGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutMainGroup.Text = "layoutMainGroup";
            this.layoutMainGroup.TextVisible = false;
            this.layoutControlItem_отмена.Control = this.simpleButton_отмена;
            this.layoutControlItem_отмена.CustomizationFormText = "layoutControlItem_отмена";
            this.layoutControlItem_отмена.Location = new Point(470, 0x18);
            this.layoutControlItem_отмена.MaxSize = new Size(0x67, 0x21);
            this.layoutControlItem_отмена.MinSize = new Size(0x67, 0x21);
            this.layoutControlItem_отмена.Name = "layoutControlItem_отмена";
            this.layoutControlItem_отмена.Size = new Size(0x67, 0x165);
            this.layoutControlItem_отмена.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem_отмена.Text = "layoutControlItem_отмена";
            this.layoutControlItem_отмена.TextLocation = Locations.Left;
            this.layoutControlItem_отмена.TextSize = new Size(0, 0);
            this.layoutControlItem_отмена.TextToControlDistance = 0;
            this.layoutControlItem_отмена.TextVisible = false;
            this.emptySpaceItem_bottom.CustomizationFormText = "emptySpaceItem_bottom";
            this.emptySpaceItem_bottom.Location = new Point(0, 0x18);
            this.emptySpaceItem_bottom.Name = "emptySpaceItem_bottom";
            this.emptySpaceItem_bottom.Size = new Size(0xfd, 0x165);
            this.emptySpaceItem_bottom.Text = "emptySpaceItem_bottom";
            this.emptySpaceItem_bottom.TextSize = new Size(0, 0);
            this.layoutControlItem_применить.Control = this.simpleButton_применить;
            this.layoutControlItem_применить.CustomizationFormText = "layoutControlItem_применить";
            this.layoutControlItem_применить.Location = new Point(0x16f, 0x18);
            this.layoutControlItem_применить.MaxSize = new Size(0x67, 0x21);
            this.layoutControlItem_применить.MinSize = new Size(0x67, 0x21);
            this.layoutControlItem_применить.Name = "layoutControlItem_применить";
            this.layoutControlItem_применить.Size = new Size(0x67, 0x165);
            this.layoutControlItem_применить.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem_применить.Text = "layoutControlItem_применить";
            this.layoutControlItem_применить.TextLocation = Locations.Left;
            this.layoutControlItem_применить.TextSize = new Size(0, 0);
            this.layoutControlItem_применить.TextToControlDistance = 0;
            this.layoutControlItem_применить.TextVisible = false;
            this.emptySpaceItem_top.CustomizationFormText = "Субтаблица";
            this.emptySpaceItem_top.Location = new Point(0x49, 0);
            this.emptySpaceItem_top.Name = "emptySpaceItem_top";
            this.emptySpaceItem_top.Size = new Size(500, 0x18);
            this.emptySpaceItem_top.Text = "Субтаблица";
            this.emptySpaceItem_top.TextSize = new Size(0, 0);
            this.layoutControlItem_заголовок.Control = this.labelControl_заголовок;
            this.layoutControlItem_заголовок.CustomizationFormText = "layoutControlItem_заголовок";
            this.layoutControlItem_заголовок.Location = new Point(0, 0);
            this.layoutControlItem_заголовок.Name = "layoutControlItem_заголовок";
            this.layoutControlItem_заголовок.Size = new Size(0x49, 0x18);
            this.layoutControlItem_заголовок.Text = "layoutControlItem_заголовок";
            this.layoutControlItem_заголовок.TextLocation = Locations.Left;
            this.layoutControlItem_заголовок.TextSize = new Size(0, 0);
            this.layoutControlItem_заголовок.TextToControlDistance = 0;
            this.layoutControlItem_заголовок.TextVisible = false;
            this.layoutControlItem_сохранить.Control = this.simpleButton_сохранить;
            this.layoutControlItem_сохранить.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem_сохранить.Location = new Point(0xfd, 0x18);
            this.layoutControlItem_сохранить.MaxSize = new Size(0x72, 0x21);
            this.layoutControlItem_сохранить.MinSize = new Size(0x72, 0x21);
            this.layoutControlItem_сохранить.Name = "layoutControlItem_сохранить";
            this.layoutControlItem_сохранить.Size = new Size(0x72, 0x165);
            this.layoutControlItem_сохранить.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem_сохранить.Text = "layoutControlItem_Сохранить";
            this.layoutControlItem_сохранить.TextLocation = Locations.Left;
            this.layoutControlItem_сохранить.TextSize = new Size(0, 0);
            this.layoutControlItem_сохранить.TextToControlDistance = 0;
            this.layoutControlItem_сохранить.TextVisible = false;
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowShowToolbarsPopup = false;
            this.barManager1.Bars.AddRange(new Bar[] { this.bar1, this.bar2, this.bar3 });
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new BarItem[] { this.barButtonItem1 });
            this.barManager1.MainMenu = this.bar2;
            this.barManager1.MaxItemId = 1;
            this.barManager1.ShowScreenTipsInToolbars = false;
            this.barManager1.StatusBar = this.bar3;
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 1;
            this.bar1.DockStyle = BarDockStyle.Top;
            this.bar1.Text = "Tools";
            this.bar1.Visible = false;
            this.bar2.BarName = "MainMenu";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = BarDockStyle.Top;
            this.bar2.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.barButtonItem1) });
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DisableCustomization = true;
            this.bar2.OptionsBar.DrawDragBorder = false;
            this.bar2.OptionsBar.UseWholeRow = true;
            this.bar2.Text = "MainMenu";
            this.barButtonItem1.Caption = "Справка";
            this.barButtonItem1.Glyph = (Image) manager.GetObject("barButtonItem1.Glyph");
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.PaintStyle = BarItemPaintStyle.CaptionGlyph;
            this.barButtonItem1.ItemClick += new ItemClickEventHandler(this.barButtonItem1_ItemClick);
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            this.bar3.Visible = false;
            base.CancelButton = this.simpleButton_отмена;
            base.ClientSize = new Size(0x23f, 0x1c8);
            base.Controls.Add(this.layoutMain);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ФормаДинамическойТаблицы";
            base.KeyUp += new KeyEventHandler(this.ФормаДинамическойТаблицы_KeyUp);
            base.FormClosing += new FormClosingEventHandler(this.ФормаДинамическойТаблицы_FormClosing);
            this.layoutMain.EndInit();
            this.layoutMain.ResumeLayout(false);
            this.layoutMainGroup.EndInit();
            this.layoutControlItem_отмена.EndInit();
            this.emptySpaceItem_bottom.EndInit();
            this.layoutControlItem_применить.EndInit();
            this.emptySpaceItem_top.EndInit();
            this.layoutControlItem_заголовок.EndInit();
            this.layoutControlItem_сохранить.EndInit();
            this.barManager1.EndInit();
            base.ResumeLayout(false);
        }

        private void layoutMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.Таблица.ЭкраннаяФорма.ОткрытьМетодическийСправочник(this as ИнтерфейсОтображаемойТаблицыЭкраннойФормы);
            }
        }

        private void simpleButton_отмена_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.Таблица.ЭкраннаяФорма.ОткрытьМетодическийСправочник(this as ИнтерфейсОтображаемойТаблицыЭкраннойФормы);
            }
        }

        private void simpleButton_применить_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.OK;
            base.Close();
        }

        private void simpleButton_применить_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.Таблица.ЭкраннаяФорма.ОткрытьМетодическийСправочник(this as ИнтерфейсОтображаемойТаблицыЭкраннойФормы);
            }
        }

        private void simpleButton_сохранить_Click(object sender, EventArgs e)
        {
            this.simpleButton_сохранить.Focus();
            if ((this.Таблица.ЗначенияИзменились && (Сообщение.ПоказатьВопрос("Вы желаете сохранить изменения?", "Сохранение формы...") == РезультатДиалога.Да)) && this.СохранитьДинамическуюТаблицу())
            {
                if (((this.Таблица.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.Просмотр) || (this.Таблица.ЭкраннаяФорма.РежимРаботы == РежимРаботыЭкраннойФормы.ПредварительныйПросмотр)) && (this.Таблица.ЭкраннаяФорма.ВариантОткрытия != ВариантОткрытияФормы.Чтение))
                {
                    this.Таблица.ЭкраннаяФорма.ОбновитьДанные();
                }
                if (this.Таблица.ЭкраннаяФорма.ВариантОткрытия != ВариантОткрытияФормы.Чтение)
                {
                    this.Таблица.ЭкраннаяФорма.ПересчитатьАвтоблоки();
                }
                this.СохранитьФорму();
            }
            this.Таблица.СоздатьЗеркалоДанных();
            this.Таблица.RefreshDataSource();
        }

        private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Таблица.ДобавитьСтроку();
        }

        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Файлы Excel | *.xls";
            if ((dialog.ShowDialog() == DialogResult.OK) && this.Таблица.IsPrintingAvailable)
            {
                this.Таблица.ExportToXls(dialog.FileName, new XlsExportOptions(true, true));
                ОткрытиеФайлов.ОткрытьФайл(dialog.FileName);
            }
        }

        public DialogResult ПоказатьДиалог(ДинамическаяТаблица Таблица)
        {
            if (Таблица.РазмещатьНаЗакладке)
            {
                this.начальныйРодительТаблицы = Таблица.Parent;
            }
            this.Таблица = Таблица;
            if (((this.Таблица.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (this.Таблица.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФормаБезРедактирования)) || ((this.Таблица.ЭкраннаяФорма == null) || ((this.Таблица.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.Просмотр) && (this.Таблица.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.ПредварительныйПросмотр))))
            {
                this.Таблица.РазрешитьРедактирование = false;
                this.simpleButton_применить.Enabled = false;
                this.simpleButton_сохранить.Enabled = false;
            }
            this.Text = Таблица.Заголовок;
            try
            {
                this.labelControl_заголовок.Text = string.Format("Отчетная форма {0} : {1}", Таблица.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура.Код, Таблица.Заголовок);
            }
            catch (Exception)
            {
                this.labelControl_заголовок.Text = Таблица.Заголовок;
            }
            this.layoutMain.BeginInit();
            this.layoutMain.SuspendLayout();
            this.layoutMainGroup.BeginInit();
            base.SuspendLayout();
            this.таблицаItem = new LayoutControlItem();
            this.таблицаItem.Name = "Таблица";
            this.таблицаItem.TextVisible = false;
            this.таблицаItem.Control = Таблица;
            this.layoutMain.Controls.Add(Таблица);
            this.layoutMainGroup.AddItem(this.таблицаItem);
            this.emptySpaceItem_bottom.Move(this.таблицаItem, InsertType.Bottom);
            this.layoutControlItem_отмена.Move(this.emptySpaceItem_bottom, InsertType.Right);
            this.layoutControlItem_применить.Move(this.emptySpaceItem_bottom, InsertType.Right);
            this.layoutControlItem_сохранить.Move(this.emptySpaceItem_bottom, InsertType.Right);
            this.layoutMain.EndInit();
            this.layoutMain.ResumeLayout(false);
            this.layoutMainGroup.EndInit();
            base.ResumeLayout(false);
            if (!Таблица.РазмещатьНаЗакладке)
            {
                Таблица.СоздатьЗеркалоДанных();
            }
            if (!Приложение.Параметры.ПараметрЗадан("РежимТестировщика"))
            {
                Таблица.ЗагрузкаНастроек(base.GetType(), Таблица.Заголовок);
            }
            return base.ShowDialog();
        }

        private bool ПроверитьКорректностьДанных(ref List<СтрокаОтчетаСверкиДанных> списокОшибокЗаполнения, ref int количествоОшибок, ref int количествоПредупреждений)
        {
            if (списокОшибокЗаполнения == null)
            {
                списокОшибокЗаполнения = new List<СтрокаОтчетаСверкиДанных>();
            }
            bool flag = true;
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            if (!this.Таблица.ПроверитьЗаполненностьДанных(out dictionary))
            {
                foreach (KeyValuePair<string, List<string>> pair in dictionary)
                {
                    foreach (string str in pair.Value)
                    {
                        СтрокаОтчетаСверкиДанных item = new СтрокаОтчетаСверкиДанных();
                        item.Форма = this.Таблица.ЭкраннаяФорма.ОтчетнаяФорма.Метаструктура.Наименование;
                        item.Столбец = pair.Key;
                        item.Строка = str;
                        item.Субтаблица = this.Таблица.Наименование;
                        item.ТипОшибки = "Незаполненная ячейка";
                        item.СохранениеРазрешено = false;
                        списокОшибокЗаполнения.Add(item);
                    }
                }
            }
            списокОшибокЗаполнения.AddRange(this.Таблица.Данные.ТаблицаДанных.МатрицаЗначений.ПроверитьУникальностьСтрок());
            if (this.Таблица.Данные.ТаблицаДанных.ДанныеФормы.Идентификатор.КомпонентОтчетногоПериода.ОтчетныйПериод.ДатаНачала >= Конвертер.ВДату("01.01.2009"))
            {
                СверкаКлассификаторов классификаторов = this.Таблица.ЭкраннаяФорма.ОтчетнаяФорма.ПолучитьОбъектСверкиКлассификаторов();
                классификаторов.ПроверятьНаПустоту = false;
                классификаторов.УдалятьНеверныеСтроки = false;
                списокОшибокЗаполнения.AddRange(классификаторов.Проверить(this.Таблица.Данные.ТаблицаДанных));
            }
            foreach (СтрокаОтчетаСверкиДанных данных2 in списокОшибокЗаполнения)
            {
                if (данных2.СохранениеРазрешено)
                {
                    количествоПредупреждений++;
                }
                else
                {
                    количествоОшибок++;
                    flag = false;
                }
            }
            return flag;
        }

        public void СохранитьДанные()
        {
            if (!this.Таблица.ЭкраннаяФорма.ОтчетнаяФорма.ОбработатьСобытие_СохраненияСубтаблицы(this.Таблица.КодТаблицы, this.Таблица.Данные.ТаблицаДанных))
            {
                this.Таблица.СохранитьЗеркалоДанных();
                this.Таблица.ЭкраннаяФорма.ДанныеИзменились = true;
                this.Таблица.ЭкраннаяФорма.ОтчетнаяФорма.ОбработатьСобытие_ПослеСохраненияСубтаблицы(this.Таблица.КодТаблицы, this.Таблица.Данные.ТаблицаДанных);
            }
        }

        private bool СохранитьДинамическуюТаблицу()
        {
            РезультатыВыполненияСверкиДанных данных;
            if (this.Таблица.ЭкраннаяФорма.ОтчетнаяФорма.ОбработатьНеверноЗаполненныеЯчейки_ПередЗакрытиемСубТаблицы(this.Таблица.Данные.ТаблицаДанных).НайденыНеВерноЗаполненныеЯчейки)
            {
                this.Refresh();
                if (Сообщение.ПоказатьВопрос("Найдены неверно заполненные ячейки. \nПродолжить?") == РезультатДиалога.Нет)
                {
                    return false;
                }
            }
            List<СтрокаОтчетаСверкиДанных> list = new List<СтрокаОтчетаСверкиДанных>();
            Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();
            int num = 0;
            int num2 = 0;
            if (!this.ПроверитьКорректностьДанных(ref list, ref num, ref num2))
            {
                данных = new РезультатыВыполненияСверкиДанных();
                данных.Заголовок = string.Format("Во время проверки данных были обнаружены ошибки и предупреждения (количество ошибок: {0}, количество предупреждений: {1})", num, num2);
                данных.Подзаголовок = "Так как таблица содержит ошибки, таблица сохранена не будет.";
                данных.СтрокиСверки.AddRange(list);
                данных.СброситьСписокСтолбцов();
                данных.ДобавитьОтображаемыйСтолбец("Таблица", "Субтаблица");
                данных.ДобавитьОтображаемыйСтолбец("Столбец", "Столбец");
                данных.ДобавитьОтображаемыйСтолбец("Строка", "Строка");
                данных.ДобавитьОтображаемыйСтолбец("Условие", "Условие");
                данных.ДобавитьОтображаемыйСтолбец("Ошибка", "ТипОшибки");
                данных.ДобавитьОтображаемыйСтолбец("Сохранение разрешено", "СохранениеРазрешено");
                if (данных.СтрокиСверки.Count > 0)
                {
                    this.Таблица.УстановитьНеверноЗаполненыеЯчейки(данных.СтрокиСверки);
                }
                ФормаОтчетаСверкиДанных данных2 = new ФормаОтчетаСверкиДанных(данных);
                данных2.ПоказатьДиалог();
                return false;
            }
            if ((list != null) && (list.Count > 0))
            {
                данных = new РезультатыВыполненияСверкиДанных();
                данных.Заголовок = string.Format("Во время проверки данных были обнаружены предупреждения (количество предупреждений: {0})", num2);
                данных.Подзаголовок = "Так как таблица содержит только предупреждения, таблица будет сохранена.";
                данных.СтрокиСверки.AddRange(list);
                данных.СброситьСписокСтолбцов();
                данных.ДобавитьОтображаемыйСтолбец("Таблица", "Субтаблица");
                данных.ДобавитьОтображаемыйСтолбец("Столбец", "Столбец");
                данных.ДобавитьОтображаемыйСтолбец("Строка", "Строка");
                данных.ДобавитьОтображаемыйСтолбец("Условие", "Условие");
                данных.ДобавитьОтображаемыйСтолбец("Ошибка", "ТипОшибки");
                данных.ДобавитьОтображаемыйСтолбец("Сохранение разрешено", "СохранениеРазрешено");
                if (данных.СтрокиСверки.Count > 0)
                {
                    this.Таблица.УстановитьНеверноЗаполненыеЯчейки(данных.СтрокиСверки);
                }
                new ФормаОтчетаСверкиДанных(данных).ПоказатьДиалог();
            }
            this.СохранитьДанные();
            return true;
        }

        private void СохранитьФорму()
        {
            EventArgs e = new EventArgs();
            object sender = this;
            this.Таблица.ЭкраннаяФорма.simpleButton_применить_Click(sender, e);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Таблица.УдалитьТекущиеСтроки();
        }

        private void удалитьНастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Таблица.СброситьНастройки();
        }

        private void ФормаДинамическойТаблицы_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (((this.Таблица.ВариантОткрытия == ВариантОткрытияФормы.Чтение) || (this.Таблица.ВариантОткрытия == ВариантОткрытияФормы.СводнаяФормаБезРедактирования)) || ((this.Таблица.ЭкраннаяФорма == null) || ((this.Таблица.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.Просмотр) && (this.Таблица.ЭкраннаяФорма.РежимРаботы != РежимРаботыЭкраннойФормы.ПредварительныйПросмотр))))
            {
                base.DialogResult = DialogResult.Cancel;
            }
            else
            {
                bool flag = false;
                this.simpleButton_применить.Focus();
                if (base.DialogResult == DialogResult.OK)
                {
                    flag = true;
                }
                else if (this.Таблица.ЗначенияИзменились)
                {
                    if (base.DialogResult != DialogResult.OK)
                    {
                        РезультатДиалога диалога = Сообщение.Показать("Вы желаете сохранить изменения?", "Сохранение формы...", КнопкиСообщения.ДаНетОтмена);
                        if (диалога == РезультатДиалога.Отмена)
                        {
                            e.Cancel = true;
                            return;
                        }
                        if ((диалога == РезультатДиалога.Нет) && this.Таблица.ЭкраннаяФорма.ОтчетнаяФорма.ОбработатьСобытие_ЗакрытияСубтаблицыСОтказомОтСохранения(this.Таблица.КодТаблицы))
                        {
                            e.Cancel = true;
                            return;
                        }
                        if (диалога == РезультатДиалога.Да)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    if (!this.СохранитьДинамическуюТаблицу())
                    {
                        base.DialogResult = DialogResult.None;
                        e.Cancel = true;
                        return;
                    }
                    base.DialogResult = DialogResult.OK;
                }
            }
            if (!Приложение.Параметры.ПараметрЗадан("РежимТестировщика"))
            {
                this.Таблица.СохранениеНастроек(base.GetType(), this.Таблица.Заголовок);
            }
            this.layoutMainGroup.BeginUpdate();
            this.таблицаItem.Control = null;
            this.layoutMainGroup.Remove(this.таблицаItem);
            this.layoutMainGroup.EndUpdate();
            this.layoutMain.Controls.Remove(this.Таблица);
            if (this.Таблица.РазмещатьНаЗакладке)
            {
                this.Таблица.Parent = this.начальныйРодительТаблицы;
                if (this.начальныйРодительТаблицы != null)
                {
                    this.Таблица.Dock = DockStyle.Fill;
                }
            }
            this.Таблица = null;
            this.таблицаItem = null;
        }

        private void ФормаДинамическойТаблицы_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                this.Таблица.ЭкраннаяФорма.ОткрытьМетодическийСправочник(this as ИнтерфейсОтображаемойТаблицыЭкраннойФормы);
            }
        }

        public ДинамическаяТаблица Таблица
        {
            get
            {
                return this.таблица;
            }
            set
            {
                this.таблица = value;
            }
        }
    }
}

