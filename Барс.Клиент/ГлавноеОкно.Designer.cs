namespace Барс.Клиент
{
	partial class ГлавноеОкно
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ГлавноеОкно));
            this.gridColumn_Время = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn_Сообщение = new DevExpress.XtraGrid.Columns.GridColumn();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.ПанельМеню = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.выпадающееМеню = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.barAndDockingController1 = new DevExpress.XtraBars.BarAndDockingController(this.components);
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.КонтейнерРазмещенияЭлементов = new DevExpress.XtraLayout.LayoutControl();
            this.lookUpEdit_ТекущееОкно = new DevExpress.XtraEditors.LookUpEdit();
            this.buttonEdit_ТекущийПользователь = new DevExpress.XtraEditors.ButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Счетчик = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.выпадающееМеню)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.КонтейнерРазмещенияЭлементов)).BeginInit();
            this.КонтейнерРазмещенияЭлементов.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_ТекущееОкно.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit_ТекущийПользователь.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // gridColumn_Время
            // 
            this.gridColumn_Время.Caption = "Дата, время";
            this.gridColumn_Время.Name = "gridColumn_Время";
            this.gridColumn_Время.Visible = true;
            this.gridColumn_Время.VisibleIndex = 0;
            this.gridColumn_Время.Width = 154;
            // 
            // gridColumn_Сообщение
            // 
            this.gridColumn_Сообщение.Caption = "Название окна";
            this.gridColumn_Сообщение.Name = "gridColumn_Сообщение";
            this.gridColumn_Сообщение.Visible = true;
            this.gridColumn_Сообщение.VisibleIndex = 1;
            this.gridColumn_Сообщение.Width = 715;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // ПанельМеню
            // 
            this.ПанельМеню.ApplicationButtonDropDownControl = this.выпадающееМеню;
            this.ПанельМеню.ApplicationButtonKeyTip = "";
            this.ПанельМеню.ApplicationIcon = ((System.Drawing.Bitmap)(resources.GetObject("ПанельМеню.ApplicationIcon")));
            this.ПанельМеню.Controller = this.barAndDockingController1;
            this.ПанельМеню.Images = this.imageCollection1;
            this.ПанельМеню.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barButtonItem1,
            this.barButtonItem2,
            this.barButtonItem3});
            this.ПанельМеню.Location = new System.Drawing.Point(0, 0);
            this.ПанельМеню.MaxItemId = 4;
            this.ПанельМеню.Name = "ПанельМеню";
            this.ПанельМеню.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ПанельМеню.SetPopupContextMenu(this.ПанельМеню, this.popupMenu1);
            this.ПанельМеню.SelectedPage = this.ribbonPage1;
            this.ПанельМеню.Size = new System.Drawing.Size(981, 147);
            this.ПанельМеню.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // выпадающееМеню
            // 
            this.выпадающееМеню.BottomPaneControlContainer = null;
            this.выпадающееМеню.Name = "выпадающееМеню";
            this.выпадающееМеню.Ribbon = this.ПанельМеню;
            this.выпадающееМеню.RightPaneControlContainer = null;
            this.выпадающееМеню.RightPaneWidth = 240;
            // 
            // barAndDockingController1
            // 
            this.barAndDockingController1.PropertiesBar.AllowLinkLighting = false;
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Настройки панели быстрого запуска";
            this.barButtonItem1.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem1.Glyph")));
            this.barButtonItem1.Id = 1;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem1_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Caption = "Переместить панель быстрого запуска ниже главного меню";
            this.barButtonItem2.Glyph = ((System.Drawing.Image)(resources.GetObject("barButtonItem2.Glyph")));
            this.barButtonItem2.Id = 2;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem2_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Свернуть главное меню";
            this.barButtonItem3.Id = 3;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.KeyTip = "";
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "ribbonPage1";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.KeyTip = "";
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // popupMenu1
            // 
            this.popupMenu1.ItemLinks.Add(this.barButtonItem1, true);
            this.popupMenu1.ItemLinks.Add(this.barButtonItem2, true);
            this.popupMenu1.ItemLinks.Add(this.barButtonItem3, true);
            this.popupMenu1.Name = "popupMenu1";
            this.popupMenu1.Ribbon = this.ПанельМеню;
            this.popupMenu1.BeforePopup += new System.ComponentModel.CancelEventHandler(this.popupMenu1_BeforePopup);
            // 
            // КонтейнерРазмещенияЭлементов
            // 
            this.КонтейнерРазмещенияЭлементов.Controls.Add(this.lookUpEdit_ТекущееОкно);
            this.КонтейнерРазмещенияЭлементов.Controls.Add(this.buttonEdit_ТекущийПользователь);
            this.КонтейнерРазмещенияЭлементов.Dock = System.Windows.Forms.DockStyle.Fill;
            this.КонтейнерРазмещенияЭлементов.Location = new System.Drawing.Point(0, 147);
            this.КонтейнерРазмещенияЭлементов.Name = "КонтейнерРазмещенияЭлементов";
            this.КонтейнерРазмещенияЭлементов.Root = this.layoutControlGroup1;
            this.КонтейнерРазмещенияЭлементов.Size = new System.Drawing.Size(981, 77);
            this.КонтейнерРазмещенияЭлементов.TabIndex = 4;
            this.КонтейнерРазмещенияЭлементов.Text = "Панель размещения элементов";
            this.КонтейнерРазмещенияЭлементов.ShowCustomization += new System.EventHandler(this.КонтейнерРазмещенияЭлементов_ShowCustomization);
            // 
            // lookUpEdit_ТекущееОкно
            // 
            this.lookUpEdit_ТекущееОкно.Location = new System.Drawing.Point(89, 8);
            this.lookUpEdit_ТекущееОкно.Name = "lookUpEdit_ТекущееОкно";
            this.lookUpEdit_ТекущееОкно.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)});
            this.lookUpEdit_ТекущееОкно.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Раздел", "Раздел", 10),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ПредставлениеГлавногоОкна", "Главное окно", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ПредставлениеТекущегоОкна", "Текущее окно", 20, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None)});
            this.lookUpEdit_ТекущееОкно.Properties.NullText = "";
            this.lookUpEdit_ТекущееОкно.Size = new System.Drawing.Size(396, 20);
            this.lookUpEdit_ТекущееОкно.StyleController = this.КонтейнерРазмещенияЭлементов;
            this.lookUpEdit_ТекущееОкно.TabIndex = 5;
            this.lookUpEdit_ТекущееОкно.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.lookUpEdit_ТекущееОкно_CustomDisplayText);
            this.lookUpEdit_ТекущееОкно.GetNotInListValue += new DevExpress.XtraEditors.Controls.GetNotInListValueEventHandler(this.lookUpEdit_ТекущееОкно_GetNotInListValue);
            // 
            // buttonEdit_ТекущийПользователь
            // 
            this.buttonEdit_ТекущийПользователь.Location = new System.Drawing.Point(577, 8);
            this.buttonEdit_ТекущийПользователь.Name = "buttonEdit_ТекущийПользователь";
            this.buttonEdit_ТекущийПользователь.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down, "Сменить пользователя", -1, true, true, false, DevExpress.Utils.HorzAlignment.Center, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), "Смена текущего пользователя")});
            this.buttonEdit_ТекущийПользователь.Properties.ReadOnly = true;
            this.buttonEdit_ТекущийПользователь.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.buttonEdit_ТекущийПользователь.Size = new System.Drawing.Size(397, 20);
            this.buttonEdit_ТекущийПользователь.StyleController = this.КонтейнерРазмещенияЭлементов;
            this.buttonEdit_ТекущийПользователь.TabIndex = 4;
            this.buttonEdit_ТекущийПользователь.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.buttonEdit_ТекущийПользователь_ButtonClick);
            this.buttonEdit_ТекущийПользователь.CustomDisplayText += new DevExpress.XtraEditors.Controls.CustomDisplayTextEventHandler(this.buttonEdit_ТекущийПользователь_CustomDisplayText);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem5,
            this.layoutControlItem1,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(981, 77);
            this.layoutControlGroup1.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 31);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(977, 42);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.buttonEdit_ТекущийПользователь;
            this.layoutControlItem1.CustomizationFormText = "Пользователь:";
            this.layoutControlItem1.Location = new System.Drawing.Point(488, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(178, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(489, 31);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "Пользователь:";
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(76, 20);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.lookUpEdit_ТекущееОкно;
            this.layoutControlItem2.CustomizationFormText = "Текущее окно:";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(488, 31);
            this.layoutControlItem2.Text = "Текущее окно:";
            this.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(76, 20);
            // 
            // Счетчик
            // 
            this.Счетчик.Interval = 30000;
            this.Счетчик.Tick += new System.EventHandler(this.Счетчик_Tick);
            // 
            // ГлавноеОкно
            // 
            this.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 224);
            this.Controls.Add(this.КонтейнерРазмещенияЭлементов);
            this.Controls.Add(this.ПанельМеню);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(989, 225);
            this.Name = "ГлавноеОкно";
            this.Ribbon = this.ПанельМеню;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Платформа БАРС. Прикладная подсистема.";
            this.Load += new System.EventHandler(this.ГлавноеОкно_Load);
            this.Activated += new System.EventHandler(this.ГлавноеОкно_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ГлавноеОкно_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.выпадающееМеню)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barAndDockingController1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.КонтейнерРазмещенияЭлементов)).EndInit();
            this.КонтейнерРазмещенияЭлементов.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lookUpEdit_ТекущееОкно.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonEdit_ТекущийПользователь.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Время;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumn_Сообщение;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraBars.Ribbon.RibbonControl ПанельМеню;
        private DevExpress.XtraLayout.LayoutControl КонтейнерРазмещенияЭлементов;
		private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
		private DevExpress.XtraEditors.ButtonEdit buttonEdit_ТекущийПользователь;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
		private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
		private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
		public DevExpress.XtraEditors.LookUpEdit lookUpEdit_ТекущееОкно;
		private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
		private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
		private DevExpress.XtraBars.BarAndDockingController barAndDockingController1;
		private DevExpress.XtraBars.BarButtonItem barButtonItem1;
		private DevExpress.XtraBars.PopupMenu popupMenu1;
		private DevExpress.XtraBars.BarButtonItem barButtonItem2;
		private DevExpress.XtraBars.BarButtonItem barButtonItem3;
		private DevExpress.Utils.ImageCollection imageCollection1;
		private DevExpress.XtraBars.Ribbon.ApplicationMenu выпадающееМеню;
        private System.Windows.Forms.Timer Счетчик;
	}
}

	