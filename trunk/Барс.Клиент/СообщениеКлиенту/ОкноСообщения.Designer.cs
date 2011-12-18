namespace Барс.Клиент.СообщениеКлиенту
{
	partial class ОкноСообщения
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ОкноСообщения));
			this.btnClose = new DevExpress.XtraEditors.SimpleButton();
			this.заголовок = new DevExpress.XtraEditors.LabelControl();
			this.текстСообщения = new DevExpress.XtraEditors.MemoEdit();
			this.defaultLookAndFeel1 = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.textReply = new DevExpress.XtraEditors.MemoEdit();
			this.btnOk = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			((System.ComponentModel.ISupportInitialize)(this.текстСообщения.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textReply.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(493, 297);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(75, 23);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Закрыть";
			// 
			// заголовок
			// 
			this.заголовок.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.заголовок.Appearance.ForeColor = System.Drawing.Color.Red;
			this.заголовок.Appearance.Options.UseFont = true;
			this.заголовок.Appearance.Options.UseForeColor = true;
			this.заголовок.Location = new System.Drawing.Point(13, 13);
			this.заголовок.Name = "заголовок";
			this.заголовок.Size = new System.Drawing.Size(248, 13);
			this.заголовок.TabIndex = 1;
			this.заголовок.Text = "Администратор посылает Вам сообщение:";
			// 
			// текстСообщения
			// 
			this.текстСообщения.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.текстСообщения.Location = new System.Drawing.Point(13, 33);
			this.текстСообщения.Name = "текстСообщения";
			this.текстСообщения.Properties.ReadOnly = true;
			this.текстСообщения.Size = new System.Drawing.Size(555, 111);
			this.текстСообщения.TabIndex = 2;
			// 
			// defaultLookAndFeel1
			// 
			this.defaultLookAndFeel1.LookAndFeel.SkinName = "Money Twins";
			// 
			// textReply
			// 
			this.textReply.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textReply.Location = new System.Drawing.Point(13, 169);
			this.textReply.Name = "textReply";
			this.textReply.Size = new System.Drawing.Size(555, 111);
			this.textReply.TabIndex = 3;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(412, 297);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ответить";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControl1.Appearance.ForeColor = System.Drawing.Color.Blue;
			this.labelControl1.Appearance.Options.UseFont = true;
			this.labelControl1.Appearance.Options.UseForeColor = true;
			this.labelControl1.Location = new System.Drawing.Point(13, 150);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(39, 13);
			this.labelControl1.TabIndex = 5;
			this.labelControl1.Text = "Ответ:";
			// 
			// ОкноСообщения
			// 
			this.AcceptButton = this.btnOk;
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(580, 332);
			this.Controls.Add(this.labelControl1);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.textReply);
			this.Controls.Add(this.текстСообщения);
			this.Controls.Add(this.заголовок);
			this.Controls.Add(this.btnClose);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ОкноСообщения";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Сообщение";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.текстСообщения.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textReply.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton btnClose;
		private DevExpress.XtraEditors.LabelControl заголовок;
		private DevExpress.XtraEditors.MemoEdit текстСообщения;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel1;
		private DevExpress.XtraEditors.MemoEdit textReply;
		private DevExpress.XtraEditors.SimpleButton btnOk;
		private DevExpress.XtraEditors.LabelControl labelControl1;
	}
}