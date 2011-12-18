namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraGrid.Columns;
    using DevExpress.XtraGrid.Views.BandedGrid;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using DevExpress.XtraTab;
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml.XPath;
    using Барс.Интерфейс;
    using Барс.Своды.ОтчетнаяФорма;

    public class РедакторФормул : XtraForm
    {
        private IContainer components = null;
        private EmptySpaceItem emptySpaceItem1;
        private EmptySpaceItem emptySpaceItem2;
        private GridColumn gridColumn_Идентификатор;
        private GridColumn gridColumn_Код;
        private GridColumn gridColumn_Наименование;
        private GridColumn gridColumn_НачалоДействия;
        private GridColumn gridColumn_ОкончаниеДействия;
        private GridView gridLookUpEdit1View;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem3;
        private LayoutControlItem layoutControlItem4;
        private LayoutControlItem layoutControlItem5;
        private SimpleButton simpleButton_Cancel;
        private SimpleButton simpleButton_Ok;
        private XtraTabControl tabТаблицы;
        private TextEdit textEditФормула;
        private GridLookUpEdit ВыборОтчетнойФормы;
        private string идентификаторФормы;
        private bool использоватьТолькоОднуФорму = true;
        private bool нажатиеCtrl = false;
        private bool разрешатьВыборФормы = true;
        private List<char> СимволыОператоров = new List<char>();
        private СписокРедактируемыхФорм списокФорм = new СписокРедактируемыхФорм();
        private string формула;

        public РедакторФормул()
        {
            this.InitializeComponent();
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
            ComponentResourceManager manager = new ComponentResourceManager(typeof(РедакторФормул));
            this.layoutControl1 = new LayoutControl();
            this.ВыборОтчетнойФормы = new GridLookUpEdit();
            this.gridLookUpEdit1View = new GridView();
            this.gridColumn_Код = new GridColumn();
            this.gridColumn_Идентификатор = new GridColumn();
            this.gridColumn_Наименование = new GridColumn();
            this.gridColumn_НачалоДействия = new GridColumn();
            this.gridColumn_ОкончаниеДействия = new GridColumn();
            this.simpleButton_Cancel = new SimpleButton();
            this.simpleButton_Ok = new SimpleButton();
            this.textEditФормула = new TextEdit();
            this.tabТаблицы = new XtraTabControl();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.layoutControlItem2 = new LayoutControlItem();
            this.layoutControlItem3 = new LayoutControlItem();
            this.emptySpaceItem1 = new EmptySpaceItem();
            this.emptySpaceItem2 = new EmptySpaceItem();
            this.layoutControlItem4 = new LayoutControlItem();
            this.layoutControlItem5 = new LayoutControlItem();
            this.layoutControlItem1 = new LayoutControlItem();
            this.layoutControl1.BeginInit();
            this.layoutControl1.SuspendLayout();
            this.ВыборОтчетнойФормы.Properties.BeginInit();
            this.gridLookUpEdit1View.BeginInit();
            this.textEditФормула.Properties.BeginInit();
            this.tabТаблицы.BeginInit();
            this.layoutControlGroup1.BeginInit();
            this.layoutControlItem2.BeginInit();
            this.layoutControlItem3.BeginInit();
            this.emptySpaceItem1.BeginInit();
            this.emptySpaceItem2.BeginInit();
            this.layoutControlItem4.BeginInit();
            this.layoutControlItem5.BeginInit();
            this.layoutControlItem1.BeginInit();
            base.SuspendLayout();
            this.layoutControl1.Controls.Add(this.ВыборОтчетнойФормы);
            this.layoutControl1.Controls.Add(this.simpleButton_Cancel);
            this.layoutControl1.Controls.Add(this.simpleButton_Ok);
            this.layoutControl1.Controls.Add(this.textEditФормула);
            this.layoutControl1.Controls.Add(this.tabТаблицы);
            this.layoutControl1.Dock = DockStyle.Fill;
            this.layoutControl1.Location = new Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new Size(0x282, 0x1e4);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            this.layoutControl1.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
            this.layoutControl1.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
            this.ВыборОтчетнойФормы.Location = new Point(0x61, 7);
            this.ВыборОтчетнойФормы.Name = "ВыборОтчетнойФормы";
            this.ВыборОтчетнойФормы.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton(ButtonPredefines.Combo) });
            this.ВыборОтчетнойФормы.Properties.NullText = "";
            this.ВыборОтчетнойФормы.Properties.PopupFormMinSize = new Size(600, 0);
            this.ВыборОтчетнойФормы.Properties.View = this.gridLookUpEdit1View;
            this.ВыборОтчетнойФормы.Size = new Size(0xdb, 20);
            this.ВыборОтчетнойФормы.StyleController = this.layoutControl1;
            this.ВыборОтчетнойФормы.TabIndex = 9;
            this.ВыборОтчетнойФормы.EditValueChanged += new EventHandler(this.ВыборОтчетнойФормы_EditValueChanged);
            this.ВыборОтчетнойФормы.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
            this.ВыборОтчетнойФормы.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
            this.gridLookUpEdit1View.Columns.AddRange(new GridColumn[] { this.gridColumn_Код, this.gridColumn_Идентификатор, this.gridColumn_Наименование, this.gridColumn_НачалоДействия, this.gridColumn_ОкончаниеДействия });
            this.gridLookUpEdit1View.FocusRectStyle = DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            this.gridLookUpEdit1View.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
            this.gridLookUpEdit1View.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
            this.gridColumn_Код.Caption = "Код формы";
            this.gridColumn_Код.FieldName = "КодФормы";
            this.gridColumn_Код.Name = "gridColumn_Код";
            this.gridColumn_Код.Visible = true;
            this.gridColumn_Код.VisibleIndex = 0;
            this.gridColumn_Код.Width = 0x3a;
            this.gridColumn_Идентификатор.Caption = "Идентификатор";
            this.gridColumn_Идентификатор.FieldName = "Идентификатор";
            this.gridColumn_Идентификатор.Name = "gridColumn_Идентификатор";
            this.gridColumn_Идентификатор.Visible = true;
            this.gridColumn_Идентификатор.VisibleIndex = 1;
            this.gridColumn_Идентификатор.Width = 0x3a;
            this.gridColumn_Наименование.Caption = "Наименование";
            this.gridColumn_Наименование.FieldName = "Наименование";
            this.gridColumn_Наименование.Name = "gridColumn_Наименование";
            this.gridColumn_Наименование.Visible = true;
            this.gridColumn_Наименование.VisibleIndex = 2;
            this.gridColumn_Наименование.Width = 0x4d;
            this.gridColumn_НачалоДействия.Caption = "Начало действия";
            this.gridColumn_НачалоДействия.FieldName = "НачалоДействия";
            this.gridColumn_НачалоДействия.Name = "gridColumn_НачалоДействия";
            this.gridColumn_НачалоДействия.Visible = true;
            this.gridColumn_НачалоДействия.VisibleIndex = 3;
            this.gridColumn_НачалоДействия.Width = 0x7a;
            this.gridColumn_ОкончаниеДействия.Caption = "Окончание действия";
            this.gridColumn_ОкончаниеДействия.FieldName = "ОкончаниеДействия";
            this.gridColumn_ОкончаниеДействия.Name = "gridColumn_ОкончаниеДействия";
            this.gridColumn_ОкончаниеДействия.Visible = true;
            this.gridColumn_ОкончаниеДействия.VisibleIndex = 4;
            this.gridColumn_ОкончаниеДействия.Width = 0x53;
            this.simpleButton_Cancel.DialogResult = DialogResult.Cancel;
            this.simpleButton_Cancel.Location = new Point(0x215, 0x1c8);
            this.simpleButton_Cancel.Name = "simpleButton_Cancel";
            this.simpleButton_Cancel.Size = new Size(0x67, 0x16);
            this.simpleButton_Cancel.StyleController = this.layoutControl1;
            this.simpleButton_Cancel.TabIndex = 8;
            this.simpleButton_Cancel.Text = "Отменить";
            this.simpleButton_Cancel.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
            this.simpleButton_Cancel.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
            this.simpleButton_Ok.DialogResult = DialogResult.OK;
            this.simpleButton_Ok.Location = new Point(0x1a3, 0x1c8);
            this.simpleButton_Ok.Name = "simpleButton_Ok";
            this.simpleButton_Ok.Size = new Size(0x67, 0x16);
            this.simpleButton_Ok.StyleController = this.layoutControl1;
            this.simpleButton_Ok.TabIndex = 7;
            this.simpleButton_Ok.Text = "Применить";
            this.simpleButton_Ok.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
            this.simpleButton_Ok.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
            this.textEditФормула.Location = new Point(0x61, 0x26);
            this.textEditФормула.Name = "textEditФормула";
            this.textEditФормула.Size = new Size(0x21b, 20);
            this.textEditФормула.StyleController = this.layoutControl1;
            this.textEditФормула.TabIndex = 6;
            this.textEditФормула.DragDrop += new DragEventHandler(this.textEditФормула_DragDrop);
            this.textEditФормула.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
            this.textEditФормула.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
            this.tabТаблицы.Location = new Point(7, 0x45);
            this.tabТаблицы.Name = "tabТаблицы";
            this.tabТаблицы.Size = new Size(0x275, 0x178);
            this.tabТаблицы.TabIndex = 5;
            this.tabТаблицы.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
            this.tabТаблицы.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { this.layoutControlItem2, this.layoutControlItem3, this.emptySpaceItem1, this.emptySpaceItem2, this.layoutControlItem4, this.layoutControlItem5, this.layoutControlItem1 });
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new Size(0x282, 0x1e4);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            this.layoutControlItem2.Control = this.tabТаблицы;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new Point(0, 0x3e);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new Size(640, 0x183);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextLocation = Locations.Left;
            this.layoutControlItem2.TextSize = new Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem3.Control = this.textEditФормула;
            this.layoutControlItem3.CustomizationFormText = "Формула";
            this.layoutControlItem3.Location = new Point(0, 0x1f);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new Size(640, 0x1f);
            this.layoutControlItem3.Text = "Формула";
            this.layoutControlItem3.TextLocation = Locations.Left;
            this.layoutControlItem3.TextSize = new Size(0x55, 0x19);
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new Point(320, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new Size(320, 0x1f);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new Size(0, 0);
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new Point(0, 0x1c1);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new Size(0x19c, 0x21);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new Size(0, 0);
            this.layoutControlItem4.Control = this.simpleButton_Ok;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new Point(0x19c, 0x1c1);
            this.layoutControlItem4.MaxSize = new Size(0x72, 0x21);
            this.layoutControlItem4.MinSize = new Size(0x72, 0x21);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new Size(0x72, 0x21);
            this.layoutControlItem4.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextLocation = Locations.Left;
            this.layoutControlItem4.TextSize = new Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            this.layoutControlItem5.Control = this.simpleButton_Cancel;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new Point(0x20e, 0x1c1);
            this.layoutControlItem5.MaxSize = new Size(0x72, 0x21);
            this.layoutControlItem5.MinSize = new Size(0x72, 0x21);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new Size(0x72, 0x21);
            this.layoutControlItem5.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextLocation = Locations.Left;
            this.layoutControlItem5.TextSize = new Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            this.layoutControlItem1.Control = this.ВыборОтчетнойФормы;
            this.layoutControlItem1.CustomizationFormText = "Отчетная форма";
            this.layoutControlItem1.Location = new Point(0, 0);
            this.layoutControlItem1.MaxSize = new Size(320, 0x1f);
            this.layoutControlItem1.MinSize = new Size(320, 0x1f);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new Size(320, 0x1f);
            this.layoutControlItem1.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Отчетная форма";
            this.layoutControlItem1.TextLocation = Locations.Left;
            this.layoutControlItem1.TextSize = new Size(0x55, 20);
            base.AcceptButton = this.simpleButton_Ok;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.simpleButton_Cancel;
            base.ClientSize = new Size(0x282, 0x1e4);
            base.Controls.Add(this.layoutControl1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "РедакторФормул";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Редактор формул";
            base.WindowState = FormWindowState.Maximized;
            base.Load += new EventHandler(this.РедакторФормул_Load);
            base.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
            base.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
            this.layoutControl1.EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.ВыборОтчетнойФормы.Properties.EndInit();
            this.gridLookUpEdit1View.EndInit();
            this.textEditФормула.Properties.EndInit();
            this.tabТаблицы.EndInit();
            this.layoutControlGroup1.EndInit();
            this.layoutControlItem2.EndInit();
            this.layoutControlItem3.EndInit();
            this.emptySpaceItem1.EndInit();
            this.emptySpaceItem2.EndInit();
            this.layoutControlItem4.EndInit();
            this.layoutControlItem5.EndInit();
            this.layoutControlItem1.EndInit();
            base.ResumeLayout(false);
        }

        private void MainView_Click(object sender, EventArgs e)
        {
            if (this.нажатиеCtrl)
            {
                this.нажатиеCtrl = false;
                AdvBandedGridView view = sender as AdvBandedGridView;
                List<string> list = new List<string>();
                string str = (view.GridControl as ДинамическаяТаблица).КодТаблицы;
                string fieldName = view.FocusedColumn.FieldName;
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    list.Add(view.Columns[i].FieldName);
                }
                if ((view != null) && (view.FocusedRowHandle == 0))
                {
                    try
                    {
                        new РедакторМаски(list, this, str, fieldName).ShowDialog();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Не удалось запустить форму Редактора Маски. Информация о причине невозможности запуска: \n\nТип исключения: " + exception.GetType().Name + ".\nТекст: " + exception.Message + ".", "Не удалось запустить Конструктор форм", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void MainView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                this.нажатиеCtrl = true;
            }
            else
            {
                this.нажатиеCtrl = false;
            }
        }

        private void MainView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 0x11)
            {
                this.нажатиеCtrl = false;
            }
        }

        private void textEditФормула_DragDrop(object sender, DragEventArgs e)
        {
            this.ДобавитьПеременнуюКФормуле(e.Data.GetData(DataFormats.Text).ToString());
        }

        private void ВыборОтчетнойФормы_EditValueChanged(object sender, EventArgs e)
        {
            РедактируемаяФорма форма = this.ВыбранныйЭлемент;
            if (форма != null)
            {
                XtraTabPage page2;
                ФормаИндикаторПроцесса процесса = new ФормаИндикаторПроцесса();
                процесса.Показать();
                процесса.УстановитьЗначениеИндикатора(0, "Построение модели формы");
                Application.DoEvents();
                this.ИдентификаторФормы = форма.Идентификатор;
                ЭкраннаяФорма форма2 = new ЭкраннаяФорма();
                форма2.РежимРаботы = РежимРаботыЭкраннойФормы.РедакторУвязок;
                форма2.ОтчетнаяФорма.ЗагрузитьМетаструктуру(форма.ПутьКФайлуМетаструктуры, форма.НачалоДействия);
                XPathDocument document = new XPathDocument(ПровайдерФайловФормы.ПолучитьПутьКФайлуЭкраннойФормы(форма.ПутьКФайлуМетаструктуры));
                форма2.ЗагрузитьФорму(document);
                процесса.УстановитьЗначениеИндикатора(50, "Построение модели формы");
                this.tabТаблицы.BeginUpdate();
                this.tabТаблицы.SelectedTabPageIndex = 0;
                this.tabТаблицы.TabPages.Clear();
                ШапкаЭкраннойФормы формы = форма2.Шапка;
                if (формы != null)
                {
                    XtraTabPage page = this.tabТаблицы.TabPages.Add();
                    формы.Dock = DockStyle.Fill;
                    формы.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
                    формы.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
                    формы.CellClick += new GridCellClickEventHandler(this.шапка_CellClick);
                    page.Controls.Add(формы);
                    page.Text = "Шапка";
                }
                foreach (ТаблицаОтчетнойФормы формы2 in форма2.Таблицы)
                {
                    page2 = this.tabТаблицы.TabPages.Add();
                    формы2.Dock = DockStyle.Fill;
                    формы2.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
                    формы2.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
                    формы2.CellClick += new GridCellClickEventHandler(this.таблица_CellClick);
                    page2.Controls.Add(формы2);
                    page2.Text = формы2.ИмяЛиста;
                }
                foreach (ДинамическаяТаблица таблица in форма2.ДинамическиеТаблицы.Values)
                {
                    page2 = this.tabТаблицы.TabPages.Add();
                    таблица.Dock = DockStyle.Fill;
                    таблица.MainView.KeyUp += new KeyEventHandler(this.MainView_KeyUp);
                    таблица.MainView.KeyDown += new KeyEventHandler(this.MainView_KeyDown);
                    таблица.MainView.Click += new EventHandler(this.MainView_Click);
                    page2.Controls.Add(таблица);
                    page2.Text = таблица.КодТаблицы;
                }
                this.tabТаблицы.EndUpdate();
                if (this.ИспользоватьТолькоОднуФорму)
                {
                    this.textEditФормула.Text = string.Empty;
                }
                this.textEditФормула.Focus();
                this.textEditФормула.Select(this.textEditФормула.Text.Length, 0);
                процесса.УстановитьЗначениеИндикатора(100, "Построение модели формы");
                процесса.Закрыть();
            }
        }

        public void ДобавитьПеременнуюКФормуле(string ПеременнаяЯчейки)
        {
            string str = this.ПолучитьФормулуЯчейки(ПеременнаяЯчейки);
            if (this.textEditФормула.Text.Length != 0)
            {
                char item = this.textEditФормула.Text[this.textEditФормула.Text.Length - 1];
                if (!this.СимволыОператоров.Contains(item))
                {
                    this.textEditФормула.Text = this.textEditФормула.Text + "+";
                }
            }
            this.textEditФормула.Text = this.textEditФормула.Text + str;
            this.textEditФормула.Focus();
            this.textEditФормула.Select(this.textEditФормула.Text.Length, 0);
        }

        public bool ОткрытьФорму(string ПутьККэшуФорм)
        {
            this.списокФорм = new СписокРедактируемыхФорм();
            this.списокФорм.Загрузить(ПутьККэшуФорм);
            this.ВыборОтчетнойФормы.Properties.DataSource = this.списокФорм;
            bool flag = base.ShowDialog() == DialogResult.OK;
            this.Формула = this.textEditФормула.Text;
            return flag;
        }

        private string ПолучитьФормулуЯчейки(string ПеременнаяЯчейки)
        {
            string str = ПеременнаяЯчейки;
            if (str.StartsWith("$"))
            {
                str = str.Substring(1, str.Length - 2);
            }
            if (!str.StartsWith("СуммаПоСубтаблице"))
            {
                if (this.ТекущаяТаблица.КодТаблицы == null)
                {
                    str = string.Format("this.Форма.СвободныеЯчейки[\"{0}\"]", str);
                }
                else
                {
                    str = string.Format("{0}:{1}", this.ТекущаяТаблица.КодТаблицы, str);
                }
            }
            if (this.ИспользоватьТолькоОднуФорму)
            {
                return str;
            }
            return string.Format("{0}:{1}", this.ВыбранныйЭлемент.Идентификатор, str);
        }

        private void РедакторФормул_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.ИдентификаторФормы))
            {
                foreach (РедактируемаяФорма форма in this.списокФорм)
                {
                    if (форма.Идентификатор == this.ИдентификаторФормы)
                    {
                        this.ВыборОтчетнойФормы.EditValue = форма;
                        break;
                    }
                }
            }
            this.textEditФормула.DataBindings.Add("EditValue", this, "Формула");
            if (!this.РазрешатьВыборФормы)
            {
                this.ВыборОтчетнойФормы.Enabled = false;
            }
            else
            {
                this.ВыборОтчетнойФормы.Enabled = true;
            }
            this.СимволыОператоров.Add('+');
            this.СимволыОператоров.Add('-');
            this.СимволыОператоров.Add('*');
            this.СимволыОператоров.Add('/');
        }

        private void таблица_CellClick(object sender, GridCellClickEventArgs e)
        {
            if (this.нажатиеCtrl && (sender is ТаблицаОтчетнойФормы))
            {
                ТаблицаОтчетнойФормы формы = sender as ТаблицаОтчетнойФормы;
                int rowIndex = формы.CurrentCell.RowIndex;
                int colIndex = формы.CurrentCell.ColIndex;
                GridStyleInfo info = формы[e.RowIndex, e.ColIndex];
                if (info.Tag != null)
                {
                    this.ДобавитьПеременнуюКФормуле(info.Tag.ToString());
                }
            }
        }

        private void шапка_CellClick(object sender, GridCellClickEventArgs e)
        {
            if (this.нажатиеCtrl && (sender is ТаблицаОтчетнойФормы))
            {
                ТаблицаОтчетнойФормы формы = sender as ТаблицаОтчетнойФормы;
                int rowIndex = формы.CurrentCell.RowIndex;
                int colIndex = формы.CurrentCell.ColIndex;
                GridStyleInfo info = формы[e.RowIndex, e.ColIndex];
                if (info.Tag != null)
                {
                    this.ДобавитьПеременнуюКФормуле(info.Tag.ToString());
                }
            }
        }

        public РедактируемаяФорма ВыбранныйЭлемент
        {
            get
            {
                return (this.ВыборОтчетнойФормы.EditValue as РедактируемаяФорма);
            }
        }

        public string ИдентификаторФормы
        {
            get
            {
                return this.идентификаторФормы;
            }
            set
            {
                this.идентификаторФормы = value;
            }
        }

        public bool ИспользоватьТолькоОднуФорму
        {
            get
            {
                return this.использоватьТолькоОднуФорму;
            }
            set
            {
                this.использоватьТолькоОднуФорму = value;
            }
        }

        public bool РазрешатьВыборФормы
        {
            get
            {
                return this.разрешатьВыборФормы;
            }
            set
            {
                this.разрешатьВыборФормы = value;
            }
        }

        public ТаблицаОтчетнойФормы ТекущаяТаблица
        {
            get
            {
                return (this.tabТаблицы.SelectedTabPage.Controls[0] as ТаблицаОтчетнойФормы);
            }
        }

        public string Формула
        {
            get
            {
                return this.формула;
            }
            set
            {
                this.формула = value;
            }
        }
    }
}

