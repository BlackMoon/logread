namespace Барс.Клиент
{
	partial class ФормаНастройкиПанелиБыстрогоЗапуска
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ФормаНастройкиПанелиБыстрогоЗапуска ) );
			this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
			this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
			this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
			this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
			this.treeList_ЭлементыМеню = new DevExpress.XtraTreeList.TreeList();
			this.col1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.col2 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
			this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip( this.components );
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			( ( System.ComponentModel.ISupportInitialize ) ( this.panelControl1 ) ).BeginInit();
			this.panelControl1.SuspendLayout();
			( ( System.ComponentModel.ISupportInitialize ) ( this.panelControl2 ) ).BeginInit();
			this.panelControl2.SuspendLayout();
			( ( System.ComponentModel.ISupportInitialize ) ( this.treeList_ЭлементыМеню ) ).BeginInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.repositoryItemCheckEdit1 ) ).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelControl1
			// 
			this.panelControl1.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.panelControl1.Appearance.Options.UseBackColor = true;
			this.panelControl1.Controls.Add( this.simpleButton2 );
			this.panelControl1.Controls.Add( this.simpleButton1 );
			this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelControl1.Location = new System.Drawing.Point( 0, 537 );
			this.panelControl1.Name = "panelControl1";
			this.panelControl1.Size = new System.Drawing.Size( 441, 42 );
			this.panelControl1.TabIndex = 0;
			this.panelControl1.Text = "panelControl1";
			// 
			// simpleButton2
			// 
			this.simpleButton2.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.simpleButton2.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButton2.Appearance.Options.UseForeColor = true;
			this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButton2.Location = new System.Drawing.Point( 328, 7 );
			this.simpleButton2.Name = "simpleButton2";
			this.simpleButton2.Size = new System.Drawing.Size( 100, 23 );
			this.simpleButton2.TabIndex = 1;
			this.simpleButton2.Text = "Отмена";
			// 
			// simpleButton1
			// 
			this.simpleButton1.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.simpleButton1.Appearance.ForeColor = System.Drawing.Color.Black;
			this.simpleButton1.Appearance.Options.UseForeColor = true;
			this.simpleButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.simpleButton1.Location = new System.Drawing.Point( 221, 7 );
			this.simpleButton1.Name = "simpleButton1";
			this.simpleButton1.Size = new System.Drawing.Size( 100, 23 );
			this.simpleButton1.TabIndex = 0;
			this.simpleButton1.Text = "OK";
			// 
			// panelControl2
			// 
			this.panelControl2.Appearance.BackColor = System.Drawing.SystemColors.Control;
			this.panelControl2.Appearance.Options.UseBackColor = true;
			this.panelControl2.Controls.Add( this.treeList_ЭлементыМеню );
			this.panelControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelControl2.Location = new System.Drawing.Point( 0, 0 );
			this.panelControl2.Name = "panelControl2";
			this.panelControl2.Size = new System.Drawing.Size( 441, 537 );
			this.panelControl2.TabIndex = 1;
			this.panelControl2.Text = "panelControl2";
			// 
			// treeList_ЭлементыМеню
			// 
			this.treeList_ЭлементыМеню.Columns.AddRange( new DevExpress.XtraTreeList.Columns.TreeListColumn [] {
            this.col1,
            this.col2} );
			this.treeList_ЭлементыМеню.ContextMenuStrip = this.contextMenuStrip1;
			this.treeList_ЭлементыМеню.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeList_ЭлементыМеню.Location = new System.Drawing.Point( 2, 2 );
			this.treeList_ЭлементыМеню.Name = "treeList_ЭлементыМеню";
			this.treeList_ЭлементыМеню.RepositoryItems.AddRange( new DevExpress.XtraEditors.Repository.RepositoryItem [] {
            this.repositoryItemCheckEdit1} );
			this.treeList_ЭлементыМеню.Size = new System.Drawing.Size( 437, 533 );
			this.treeList_ЭлементыМеню.TabIndex = 0;
			// 
			// col1
			// 
			this.col1.FieldName = "Название";
			this.col1.MinWidth = 46;
			this.col1.Name = "col1";
			this.col1.OptionsColumn.AllowEdit = false;
			this.col1.Visible = true;
			this.col1.VisibleIndex = 0;
			this.col1.Width = 280;
			// 
			// col2
			// 
			this.col2.ColumnEdit = this.repositoryItemCheckEdit1;
			this.col2.FieldName = "Переключатель";
			this.col2.Name = "col2";
			this.col2.Visible = true;
			this.col2.VisibleIndex = 1;
			this.col2.Width = 62;
			// 
			// repositoryItemCheckEdit1
			// 
			this.repositoryItemCheckEdit1.AutoHeight = false;
			this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange( new System.Windows.Forms.ToolStripItem [] {
            this.toolStripMenuItem1} );
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size( 246, 26 );
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Image = ( ( System.Drawing.Image ) ( resources.GetObject( "toolStripMenuItem1.Image" ) ) );
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size( 245, 22 );
			this.toolStripMenuItem1.Text = "Очистить все выбранные пункты";
			this.toolStripMenuItem1.Click += new System.EventHandler( this.toolStripMenuItem1_Click );
			// 
			// ФормаНастройкиПанелиБыстрогоЗапуска
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size( 441, 579 );
			this.Controls.Add( this.panelControl2 );
			this.Controls.Add( this.panelControl1 );
			this.Icon = ( ( System.Drawing.Icon ) ( resources.GetObject( "$this.Icon" ) ) );
			this.Name = "ФормаНастройкиПанелиБыстрогоЗапуска";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Настройки панели быстрого запуска";
			this.Load += new System.EventHandler( this.ФормаНастройкиПанелиБыстрогоЗапуска_Load );
			( ( System.ComponentModel.ISupportInitialize ) ( this.panelControl1 ) ).EndInit();
			this.panelControl1.ResumeLayout( false );
			( ( System.ComponentModel.ISupportInitialize ) ( this.panelControl2 ) ).EndInit();
			this.panelControl2.ResumeLayout( false );
			( ( System.ComponentModel.ISupportInitialize ) ( this.treeList_ЭлементыМеню ) ).EndInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.repositoryItemCheckEdit1 ) ).EndInit();
			this.contextMenuStrip1.ResumeLayout( false );
			this.ResumeLayout( false );

		}

		#endregion

		private DevExpress.XtraEditors.PanelControl panelControl1;
		private DevExpress.XtraEditors.PanelControl panelControl2;
		private DevExpress.XtraTreeList.TreeList treeList_ЭлементыМеню;
		private DevExpress.XtraEditors.SimpleButton simpleButton1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn col1;
		private DevExpress.XtraTreeList.Columns.TreeListColumn col2;
		private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
		private DevExpress.XtraEditors.SimpleButton simpleButton2;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

	}
}