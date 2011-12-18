namespace Барс.Клиент
{
	partial class ОкноИсключения
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.кнопка_Закрыть = new DevExpress.XtraEditors.SimpleButton();
			this.memo_СтекВызовов = new DevExpress.XtraEditors.MemoEdit();
			this.memo_ТекстСообщения = new DevExpress.XtraEditors.MemoEdit();
			this.memo_Приветствие = new DevExpress.XtraEditors.MemoEdit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.memo_СтекВызовов.Properties ) ).BeginInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.memo_ТекстСообщения.Properties ) ).BeginInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.memo_Приветствие.Properties ) ).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point( 12, 51 );
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size( 99, 13 );
			this.label1.TabIndex = 3;
			this.label1.Text = "Текст сообщения:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point( 12, 155 );
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size( 139, 13 );
			this.label2.TabIndex = 5;
			this.label2.Text = "Системный стек вызовов:";
			// 
			// кнопка_Закрыть
			// 
			this.кнопка_Закрыть.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.кнопка_Закрыть.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.кнопка_Закрыть.Location = new System.Drawing.Point( 404, 301 );
			this.кнопка_Закрыть.Name = "кнопка_Закрыть";
			this.кнопка_Закрыть.Size = new System.Drawing.Size( 105, 23 );
			this.кнопка_Закрыть.TabIndex = 0;
			this.кнопка_Закрыть.Text = "Закрыть";
			// 
			// memo_СтекВызовов
			// 
			this.memo_СтекВызовов.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.memo_СтекВызовов.Location = new System.Drawing.Point( 15, 171 );
			this.memo_СтекВызовов.Name = "memo_СтекВызовов";
			this.memo_СтекВызовов.Properties.ReadOnly = true;
			this.memo_СтекВызовов.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.memo_СтекВызовов.Properties.WordWrap = false;
			this.memo_СтекВызовов.Size = new System.Drawing.Size( 494, 120 );
			this.memo_СтекВызовов.TabIndex = 8;
			// 
			// memo_ТекстСообщения
			// 
			this.memo_ТекстСообщения.Location = new System.Drawing.Point( 15, 67 );
			this.memo_ТекстСообщения.Name = "memo_ТекстСообщения";
			this.memo_ТекстСообщения.Properties.ReadOnly = true;
			this.memo_ТекстСообщения.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.memo_ТекстСообщения.Size = new System.Drawing.Size( 494, 85 );
			this.memo_ТекстСообщения.TabIndex = 9;
			// 
			// memo_Приветствие
			// 
			this.memo_Приветствие.EditValue = "При работе системы БАРС возникла исключительная ситуация, которая не может быть к" +
				"орректно обработана.";
			this.memo_Приветствие.Location = new System.Drawing.Point( 15, 12 );
			this.memo_Приветствие.Name = "memo_Приветствие";
			this.memo_Приветствие.Properties.Appearance.Options.UseTextOptions = true;
			this.memo_Приветствие.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.memo_Приветствие.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.memo_Приветствие.Properties.ReadOnly = true;
			this.memo_Приветствие.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.memo_Приветствие.Size = new System.Drawing.Size( 494, 36 );
			this.memo_Приветствие.TabIndex = 10;
			// 
			// ОкноИсключения
			// 
			this.Appearance.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 204 ) ) );
			this.Appearance.Options.UseFont = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.кнопка_Закрыть;
			this.ClientSize = new System.Drawing.Size( 521, 336 );
			this.Controls.Add( this.memo_Приветствие );
			this.Controls.Add( this.memo_ТекстСообщения );
			this.Controls.Add( this.memo_СтекВызовов );
			this.Controls.Add( this.кнопка_Закрыть );
			this.Controls.Add( this.label2 );
			this.Controls.Add( this.label1 );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.LookAndFeel.SkinName = "Money Twins";
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ОкноИсключения";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Исключительная ситуация";
			this.Load += new System.EventHandler( this.ОкноИсключения_Load );
			( ( System.ComponentModel.ISupportInitialize ) ( this.memo_СтекВызовов.Properties ) ).EndInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.memo_ТекстСообщения.Properties ) ).EndInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.memo_Приветствие.Properties ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private DevExpress.XtraEditors.SimpleButton кнопка_Закрыть;
		private DevExpress.XtraEditors.MemoEdit memo_СтекВызовов;
		private DevExpress.XtraEditors.MemoEdit memo_ТекстСообщения;
		private DevExpress.XtraEditors.MemoEdit memo_Приветствие;
	}
}