namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Data;
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Columns;
    using DevExpress.XtraGrid.Views.Base;
    using DevExpress.XtraGrid.Views.Grid;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Барс;
    using Барс.Интерфейс;
    using Барс.Своды.ОтчетнаяФорма;

    public class ФормаВыбораТеста : XtraForm
    {
        private SimpleButton button_ok;
        private SimpleButton button_отмена;
        private IContainer components = null;
        private EmptySpaceItem emptySpaceItem1;
        private EmptySpaceItem emptySpaceItem2;
        private GridColumn gridColumn_Наименование;
        private GridColumn gridColumn_проверить;
        private GridControl gridControl_тесты;
        private GridView gridView;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem3;
        private RepositoryItemCheckEdit repositoryItemCheckEdit_выбрать;
        private List<ОписаниеТеста> выбранныеТесты = null;
        private bool[] выбранныеЭлементы = null;
        private ИдентификаторМетаописанияФормы идентификаторМетаописания = null;

        public ФормаВыбораТеста()
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

        private void gridView_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "gridColumn_проверить")
            {
                if (e.IsGetData)
                {
                    e.Value = this.выбранныеЭлементы[e.ListSourceRowIndex];
                }
                else if (e.IsSetData)
                {
                    this.выбранныеЭлементы[e.ListSourceRowIndex] = (bool) e.Value;
                }
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ФормаВыбораТеста));
            this.layoutControl1 = new LayoutControl();
            this.button_отмена = new SimpleButton();
            this.gridControl_тесты = new GridControl();
            this.gridView = new GridView();
            this.gridColumn_Наименование = new GridColumn();
            this.gridColumn_проверить = new GridColumn();
            this.repositoryItemCheckEdit_выбрать = new RepositoryItemCheckEdit();
            this.button_ok = new SimpleButton();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.layoutControlItem1 = new LayoutControlItem();
            this.emptySpaceItem1 = new EmptySpaceItem();
            this.layoutControlItem3 = new LayoutControlItem();
            this.layoutControlItem2 = new LayoutControlItem();
            this.emptySpaceItem2 = new EmptySpaceItem();
            this.layoutControl1.BeginInit();
            this.layoutControl1.SuspendLayout();
            this.gridControl_тесты.BeginInit();
            this.gridView.BeginInit();
            this.repositoryItemCheckEdit_выбрать.BeginInit();
            this.layoutControlGroup1.BeginInit();
            this.layoutControlItem1.BeginInit();
            this.emptySpaceItem1.BeginInit();
            this.layoutControlItem3.BeginInit();
            this.layoutControlItem2.BeginInit();
            this.emptySpaceItem2.BeginInit();
            base.SuspendLayout();
            this.layoutControl1.Controls.Add(this.button_отмена);
            this.layoutControl1.Controls.Add(this.gridControl_тесты);
            this.layoutControl1.Controls.Add(this.button_ok);
            this.layoutControl1.Dock = DockStyle.Fill;
            this.layoutControl1.Location = new Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new Size(0x25a, 0x169);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            this.button_отмена.DialogResult = DialogResult.Cancel;
            this.button_отмена.Location = new Point(0x1ec, 0x14d);
            this.button_отмена.Name = "button_отмена";
            this.button_отмена.Size = new Size(0x68, 0x16);
            this.button_отмена.StyleController = this.layoutControl1;
            this.button_отмена.TabIndex = 7;
            this.button_отмена.Text = "Отмена";
            this.gridControl_тесты.EmbeddedNavigator.Name = "";
            this.gridControl_тесты.Location = new Point(7, 0x24);
            this.gridControl_тесты.MainView = this.gridView;
            this.gridControl_тесты.Name = "gridControl_тесты";
            this.gridControl_тесты.RepositoryItems.AddRange(new RepositoryItem[] { this.repositoryItemCheckEdit_выбрать });
            this.gridControl_тесты.Size = new Size(0x24d, 0x11e);
            this.gridControl_тесты.TabIndex = 4;
            this.gridControl_тесты.ViewCollection.AddRange(new BaseView[] { this.gridView });
            this.gridView.Columns.AddRange(new GridColumn[] { this.gridColumn_Наименование, this.gridColumn_проверить });
            this.gridView.GridControl = this.gridControl_тесты;
            this.gridView.Name = "gridView";
            this.gridView.OptionsCustomization.AllowFilter = false;
            this.gridView.OptionsCustomization.AllowGroup = false;
            this.gridView.OptionsCustomization.AllowRowSizing = true;
            this.gridView.OptionsDetail.EnableMasterViewMode = false;
            this.gridView.OptionsView.ShowIndicator = false;
            this.gridView.CustomUnboundColumnData += new CustomColumnDataEventHandler(this.gridView_CustomUnboundColumnData);
            this.gridColumn_Наименование.Caption = "Наименование";
            this.gridColumn_Наименование.FieldName = "Наименование";
            this.gridColumn_Наименование.Name = "gridColumn_Наименование";
            this.gridColumn_Наименование.OptionsColumn.AllowEdit = false;
            this.gridColumn_Наименование.OptionsColumn.ReadOnly = true;
            this.gridColumn_Наименование.Visible = true;
            this.gridColumn_Наименование.VisibleIndex = 1;
            this.gridColumn_Наименование.Width = 0x3a1;
            this.gridColumn_проверить.Caption = "Проверить";
            this.gridColumn_проверить.ColumnEdit = this.repositoryItemCheckEdit_выбрать;
            this.gridColumn_проверить.FieldName = "gridColumn_проверить";
            this.gridColumn_проверить.Name = "gridColumn_проверить";
            this.gridColumn_проверить.UnboundType = UnboundColumnType.Boolean;
            this.gridColumn_проверить.Visible = true;
            this.gridColumn_проверить.VisibleIndex = 0;
            this.gridColumn_проверить.Width = 0x97;
            this.repositoryItemCheckEdit_выбрать.AutoHeight = false;
            this.repositoryItemCheckEdit_выбрать.Name = "repositoryItemCheckEdit_выбрать";
            this.repositoryItemCheckEdit_выбрать.NullStyle = StyleIndeterminate.Inactive;
            this.button_ok.DialogResult = DialogResult.OK;
            this.button_ok.Location = new Point(0x179, 0x14d);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new Size(0x68, 0x16);
            this.button_ok.StyleController = this.layoutControl1;
            this.button_ok.TabIndex = 6;
            this.button_ok.Text = "OK";
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { this.layoutControlItem1, this.emptySpaceItem1, this.layoutControlItem3, this.layoutControlItem2, this.emptySpaceItem2 });
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new Size(0x25a, 0x169);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            this.layoutControlItem1.Control = this.gridControl_тесты;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new Point(0, 0x1d);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new Size(600, 0x129);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextLocation = Locations.Left;
            this.layoutControlItem1.TextSize = new Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new Point(0, 0x146);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new Size(370, 0x21);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new Size(0, 0);
            this.layoutControlItem3.Control = this.button_ok;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new Point(370, 0x146);
            this.layoutControlItem3.MaxSize = new Size(0x73, 0x21);
            this.layoutControlItem3.MinSize = new Size(0x73, 0x21);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new Size(0x73, 0x21);
            this.layoutControlItem3.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextLocation = Locations.Left;
            this.layoutControlItem3.TextSize = new Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            this.layoutControlItem2.Control = this.button_отмена;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new Point(0x1e5, 0x146);
            this.layoutControlItem2.MaxSize = new Size(0x73, 0x21);
            this.layoutControlItem2.MinSize = new Size(0x73, 0x21);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new Size(0x73, 0x21);
            this.layoutControlItem2.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextLocation = Locations.Left;
            this.layoutControlItem2.TextSize = new Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.emptySpaceItem2.CustomizationFormText = "Выберите тест для проверки :";
            this.emptySpaceItem2.Location = new Point(0, 0);
            this.emptySpaceItem2.MaxSize = new Size(0, 0x1d);
            this.emptySpaceItem2.MinSize = new Size(10, 0x1d);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new Size(600, 0x1d);
            this.emptySpaceItem2.SizeConstraintsType = SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "Выберите один или несколько тестов для проверки :";
            this.emptySpaceItem2.TextSize = new Size(0, 20);
            this.emptySpaceItem2.TextVisible = true;
            base.AcceptButton = this.button_ok;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.button_отмена;
            base.ClientSize = new Size(0x25a, 0x169);
            base.Controls.Add(this.layoutControl1);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.Name = "ФормаВыбораТеста";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Выбор теста формы";
            base.FormClosing += new FormClosingEventHandler(this.ФормаВыбораТеста_FormClosing);
            base.Load += new EventHandler(this.ФормаВыбораТеста_Load);
            this.layoutControl1.EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.gridControl_тесты.EndInit();
            this.gridView.EndInit();
            this.repositoryItemCheckEdit_выбрать.EndInit();
            this.layoutControlGroup1.EndInit();
            this.layoutControlItem1.EndInit();
            this.emptySpaceItem1.EndInit();
            this.layoutControlItem3.EndInit();
            this.layoutControlItem2.EndInit();
            this.emptySpaceItem2.EndInit();
            base.ResumeLayout(false);
        }

        private void ФормаВыбораТеста_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (base.DialogResult == DialogResult.OK)
            {
                this.выбранныеТесты = new List<ОписаниеТеста>();
                for (int i = 0; i < this.выбранныеЭлементы.Length; i++)
                {
                    if (this.выбранныеЭлементы[i])
                    {
                        this.выбранныеТесты.Add((this.gridControl_тесты.DataSource as СписокОписанийТестов)[i].ОписаниеТестаФормы);
                    }
                }
                if (this.выбранныеТесты.Count == 0)
                {
                    Сообщение.Показать("Необходимо выбрать хотя бы один тест!");
                    e.Cancel = true;
                }
            }
        }

        private void ФормаВыбораТеста_Load(object sender, EventArgs e)
        {
            if (this.ИдентификаторМетаописания != null)
            {
                СписокОписанийТестов тестов = new СписокОписанийТестов();
                тестов.Загрузить(this.ИдентификаторМетаописания);
                this.gridControl_тесты.DataSource = тестов;
                this.выбранныеЭлементы = new bool[тестов.Count];
            }
        }

        public List<ОписаниеТеста> ВыбранныеТесты
        {
            get
            {
                return this.выбранныеТесты;
            }
        }

        public ИдентификаторМетаописанияФормы ИдентификаторМетаописания
        {
            get
            {
                return this.идентификаторМетаописания;
            }
            set
            {
                this.идентификаторМетаописания = value;
            }
        }
    }
}

