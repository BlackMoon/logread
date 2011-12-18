namespace Барс.Интерфейс
{
	partial class ДиалогВыбораКаталога
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ДиалогВыбораКаталога ) );
			this.кнопкаOK = new DevExpress.XtraEditors.SimpleButton();
			this.кнопкаОтмена = new DevExpress.XtraEditors.SimpleButton();
			this.toolTip = new System.Windows.Forms.ToolTip( this.components );
			this.imageList = new System.Windows.Forms.ImageList( this.components );
			this.деревоКаталогов = new DevExpress.XtraTreeList.TreeList();
			this.treeListColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
			this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip( this.components );
			this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.создатьКаталогToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.переименоватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.каталог = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.кнопкаСоздать = new DevExpress.XtraEditors.SimpleButton();
			( ( System.ComponentModel.ISupportInitialize ) ( this.деревоКаталогов ) ).BeginInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.repositoryItemTextEdit1 ) ).BeginInit();
			this.contextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// кнопкаOK
			// 
			this.кнопкаOK.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.кнопкаOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.кнопкаOK.Location = new System.Drawing.Point( 427, 407 );
			this.кнопкаOK.Name = "кнопкаOK";
			this.кнопкаOK.Size = new System.Drawing.Size( 89, 22 );
			this.кнопкаOK.TabIndex = 6;
			this.кнопкаOK.Text = "OK";
			this.кнопкаOK.Click += new System.EventHandler( this.кнопкаOK_Click );
			// 
			// кнопкаОтмена
			// 
			this.кнопкаОтмена.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.кнопкаОтмена.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.кнопкаОтмена.Location = new System.Drawing.Point( 522, 407 );
			this.кнопкаОтмена.Name = "кнопкаОтмена";
			this.кнопкаОтмена.Size = new System.Drawing.Size( 83, 22 );
			this.кнопкаОтмена.TabIndex = 5;
			this.кнопкаОтмена.Text = "Отмена";
			// 
			// toolTip
			// 
			this.toolTip.Popup += new System.Windows.Forms.PopupEventHandler( this.toolTip_Popup );
			// 
			// imageList
			// 
			this.imageList.ImageStream = ( ( System.Windows.Forms.ImageListStreamer ) ( resources.GetObject( "imageList.ImageStream" ) ) );
			this.imageList.TransparentColor = System.Drawing.Color.Black;
			this.imageList.Images.SetKeyName( 0, "" );
			this.imageList.Images.SetKeyName( 1, "" );
			this.imageList.Images.SetKeyName( 2, "" );
			this.imageList.Images.SetKeyName( 3, "" );
			this.imageList.Images.SetKeyName( 4, "" );
			this.imageList.Images.SetKeyName( 5, "" );
			this.imageList.Images.SetKeyName( 6, "" );
			// 
			// деревоКаталогов
			// 
			this.деревоКаталогов.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.деревоКаталогов.Columns.AddRange( new DevExpress.XtraTreeList.Columns.TreeListColumn [] {
            this.treeListColumn} );
			this.деревоКаталогов.ContextMenuStrip = this.contextMenuStrip;
			this.деревоКаталогов.Location = new System.Drawing.Point( 12, 12 );
			this.деревоКаталогов.Name = "деревоКаталогов";
			this.деревоКаталогов.OptionsBehavior.AllowIncrementalSearch = true;
			this.деревоКаталогов.OptionsBehavior.Editable = false;
			this.деревоКаталогов.OptionsBehavior.ShowEditorOnMouseUp = true;
			this.деревоКаталогов.OptionsSelection.InvertSelection = true;
			this.деревоКаталогов.OptionsView.ShowHorzLines = false;
			this.деревоКаталогов.OptionsView.ShowIndicator = false;
			this.деревоКаталогов.OptionsView.ShowVertLines = false;
			this.деревоКаталогов.RepositoryItems.AddRange( new DevExpress.XtraEditors.Repository.RepositoryItem [] {
            this.repositoryItemTextEdit1} );
			this.деревоКаталогов.SelectImageList = this.imageList;
			this.деревоКаталогов.Size = new System.Drawing.Size( 593, 389 );
			this.деревоКаталогов.TabIndex = 4;
			this.деревоКаталогов.BeforeCollapse += new DevExpress.XtraTreeList.BeforeCollapseEventHandler( this.деревоКаталогов_BeforeCollapse );
			this.деревоКаталогов.FocusedNodeChanged += new DevExpress.XtraTreeList.FocusedNodeChangedEventHandler( this.деревоКаталогов_FocusedNodeChanged );
			this.деревоКаталогов.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler( this.деревоКаталогов_BeforeExpand );
			// 
			// treeListColumn
			// 
			this.treeListColumn.ColumnEdit = this.repositoryItemTextEdit1;
			this.treeListColumn.MinWidth = 27;
			this.treeListColumn.Name = "treeListColumn";
			this.treeListColumn.OptionsColumn.AllowSort = false;
			this.treeListColumn.Visible = true;
			this.treeListColumn.VisibleIndex = 0;
			this.treeListColumn.Width = 92;
			// 
			// repositoryItemTextEdit1
			// 
			this.repositoryItemTextEdit1.AutoHeight = false;
			this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
			// 
			// contextMenuStrip
			// 
			this.contextMenuStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem [] {
            this.открытьToolStripMenuItem,
            this.создатьКаталогToolStripMenuItem,
            this.переименоватьToolStripMenuItem,
            this.удалитьToolStripMenuItem} );
			this.contextMenuStrip.Name = "contextMenuStrip";
			this.contextMenuStrip.Size = new System.Drawing.Size( 162, 92 );
			this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler( this.contextMenuStrip_Opening );
			// 
			// открытьToolStripMenuItem
			// 
			this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
			this.открытьToolStripMenuItem.Size = new System.Drawing.Size( 161, 22 );
			this.открытьToolStripMenuItem.Text = "Открыть";
			this.открытьToolStripMenuItem.Click += new System.EventHandler( this.открытьToolStripMenuItem_Click );
			// 
			// создатьКаталогToolStripMenuItem
			// 
			this.создатьКаталогToolStripMenuItem.Name = "создатьКаталогToolStripMenuItem";
			this.создатьКаталогToolStripMenuItem.Size = new System.Drawing.Size( 161, 22 );
			this.создатьКаталогToolStripMenuItem.Text = "Создать каталог";
			this.создатьКаталогToolStripMenuItem.Click += new System.EventHandler( this.кнопкаСоздать_Click );
			// 
			// переименоватьToolStripMenuItem
			// 
			this.переименоватьToolStripMenuItem.Name = "переименоватьToolStripMenuItem";
			this.переименоватьToolStripMenuItem.Size = new System.Drawing.Size( 161, 22 );
			this.переименоватьToolStripMenuItem.Text = "Переименовать";
			this.переименоватьToolStripMenuItem.Click += new System.EventHandler( this.переименоватьToolStripMenuItem_Click );
			// 
			// удалитьToolStripMenuItem
			// 
			this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
			this.удалитьToolStripMenuItem.Size = new System.Drawing.Size( 161, 22 );
			this.удалитьToolStripMenuItem.Text = "Удалить";
			this.удалитьToolStripMenuItem.Click += new System.EventHandler( this.удалитьToolStripMenuItem_Click );
			// 
			// каталог
			// 
			this.каталог.Name = "каталог";
			this.каталог.OptionsColumn.AllowEdit = false;
			this.каталог.OptionsColumn.AllowMove = false;
			this.каталог.OptionsColumn.AllowMoveToCustomizationForm = false;
			this.каталог.OptionsColumn.AllowSort = false;
			this.каталог.OptionsColumn.ReadOnly = true;
			this.каталог.OptionsColumn.ShowInCustomizationForm = false;
			this.каталог.Visible = true;
			this.каталог.VisibleIndex = 0;
			// 
			// кнопкаСоздать
			// 
			this.кнопкаСоздать.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.кнопкаСоздать.Location = new System.Drawing.Point( 12, 407 );
			this.кнопкаСоздать.Name = "кнопкаСоздать";
			this.кнопкаСоздать.Size = new System.Drawing.Size( 75, 23 );
			this.кнопкаСоздать.TabIndex = 7;
			this.кнопкаСоздать.Text = "Создать...";
			this.кнопкаСоздать.Click += new System.EventHandler( this.кнопкаСоздать_Click );
			// 
			// ДиалогВыбораКаталога
			// 
			this.AcceptButton = this.кнопкаOK;
			this.CancelButton = this.кнопкаОтмена;
			this.ClientSize = new System.Drawing.Size( 617, 441 );
			this.Controls.Add( this.кнопкаСоздать );
			this.Controls.Add( this.деревоКаталогов );
			this.Controls.Add( this.кнопкаOK );
			this.Controls.Add( this.кнопкаОтмена );
			this.HelpButton = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size( 333, 347 );
			this.Name = "ДиалогВыбораКаталога";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Обзор папок";
			this.HelpRequested += new System.Windows.Forms.HelpEventHandler( this.ДиалогВыбораКаталога_HelpRequested );
			this.Load += new System.EventHandler( this.ДиалогВыбораКаталога_Load );
			( ( System.ComponentModel.ISupportInitialize ) ( this.деревоКаталогов ) ).EndInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.repositoryItemTextEdit1 ) ).EndInit();
			this.contextMenuStrip.ResumeLayout( false );
			this.ResumeLayout( false );

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton кнопкаOK;
		private DevExpress.XtraEditors.SimpleButton кнопкаОтмена;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.ImageList imageList;
		private DevExpress.XtraTreeList.TreeList деревоКаталогов;
		private DevExpress.XtraTreeList.Columns.TreeListColumn катлог;
		private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
		private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraTreeList.Columns.TreeListColumn каталог;
        private DevExpress.XtraEditors.SimpleButton кнопкаСоздать;
        private System.Windows.Forms.ToolStripMenuItem создатьКаталогToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem переименоватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
	}
}