namespace Барс.Клиент
{
	partial class ФормаПоказаИсключительнойСитуации
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ФормаПоказаИсключительнойСитуации ) );
			this.simpleButton_Закрыть = new DevExpress.XtraEditors.SimpleButton();
			this.memoEdit_Сообщение = new DevExpress.XtraEditors.MemoEdit();
			this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
			this.memoExEdit_Подробно = new DevExpress.XtraEditors.MemoExEdit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.memoEdit_Сообщение.Properties ) ).BeginInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.pictureEdit1.Properties ) ).BeginInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.memoExEdit_Подробно.Properties ) ).BeginInit();
			this.SuspendLayout();
			// 
			// simpleButton_Закрыть
			// 
			this.simpleButton_Закрыть.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.simpleButton_Закрыть.Location = new System.Drawing.Point( 374, 79 );
			this.simpleButton_Закрыть.Name = "simpleButton_Закрыть";
			this.simpleButton_Закрыть.Size = new System.Drawing.Size( 98, 22 );
			this.simpleButton_Закрыть.TabIndex = 0;
			this.simpleButton_Закрыть.Text = "OK";
			// 
			// memoEdit_Сообщение
			// 
			this.memoEdit_Сообщение.EditValue = "";
			this.memoEdit_Сообщение.Location = new System.Drawing.Point( 72, 2 );
			this.memoEdit_Сообщение.Name = "memoEdit_Сообщение";
			this.memoEdit_Сообщение.Properties.AllowFocused = false;
			this.memoEdit_Сообщение.Properties.Appearance.Options.UseTextOptions = true;
			this.memoEdit_Сообщение.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.memoEdit_Сообщение.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
			this.memoEdit_Сообщение.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.memoEdit_Сообщение.Properties.HideSelection = false;
			this.memoEdit_Сообщение.Properties.ReadOnly = true;
			this.memoEdit_Сообщение.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.memoEdit_Сообщение.Size = new System.Drawing.Size( 400, 46 );
			this.memoEdit_Сообщение.TabIndex = 2;
			this.memoEdit_Сообщение.ToolTip = "Текст сообщения";
			// 
			// pictureEdit1
			// 
			this.pictureEdit1.EditValue = global::Барс.Клиент.Properties.Resources.Предупреждение;
			this.pictureEdit1.Location = new System.Drawing.Point( 2, 2 );
			this.pictureEdit1.Name = "pictureEdit1";
			this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
			this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.pictureEdit1.Size = new System.Drawing.Size( 64, 72 );
			this.pictureEdit1.TabIndex = 1;
			// 
			// memoExEdit_Подробно
			// 
			this.memoExEdit_Подробно.EditValue = "";
			this.memoExEdit_Подробно.Location = new System.Drawing.Point( 72, 54 );
			this.memoExEdit_Подробно.Name = "memoExEdit_Подробно";
			this.memoExEdit_Подробно.Properties.Buttons.AddRange( new DevExpress.XtraEditors.Controls.EditorButton [] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down)} );
			this.memoExEdit_Подробно.Properties.PopupStartSize = new System.Drawing.Size( 400, 200 );
			this.memoExEdit_Подробно.Properties.ReadOnly = true;
			this.memoExEdit_Подробно.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.memoExEdit_Подробно.Properties.WordWrap = false;
			this.memoExEdit_Подробно.Size = new System.Drawing.Size( 400, 20 );
			this.memoExEdit_Подробно.TabIndex = 3;
			this.memoExEdit_Подробно.ToolTip = "Подробнее об исключительной ситуации";
			// 
			// ФормаПоказаИсключительнойСитуации
			// 
			this.AcceptButton = this.simpleButton_Закрыть;
			this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.simpleButton_Закрыть;
			this.ClientSize = new System.Drawing.Size( 477, 105 );
			this.Controls.Add( this.pictureEdit1 );
			this.Controls.Add( this.memoEdit_Сообщение );
			this.Controls.Add( this.simpleButton_Закрыть );
			this.Controls.Add( this.memoExEdit_Подробно );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ( ( System.Drawing.Icon ) ( resources.GetObject( "$this.Icon" ) ) );
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ФормаПоказаИсключительнойСитуации";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Исключительная ситуация";
			this.Load += new System.EventHandler( this.ФормаПоказаИсключительнойСитуации_Load );
			( ( System.ComponentModel.ISupportInitialize ) ( this.memoEdit_Сообщение.Properties ) ).EndInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.pictureEdit1.Properties ) ).EndInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.memoExEdit_Подробно.Properties ) ).EndInit();
			this.ResumeLayout( false );

		}

		#endregion

		private DevExpress.XtraEditors.SimpleButton simpleButton_Закрыть;
		private DevExpress.XtraEditors.MemoEdit memoEdit_Сообщение;
		private DevExpress.XtraEditors.PictureEdit pictureEdit1;
		private DevExpress.XtraEditors.MemoExEdit memoExEdit_Подробно;
	}
}