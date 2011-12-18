namespace Барс.Клиент
{
	partial class ФормаПроцессаОбновленияСистемы
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( ФормаПроцессаОбновленияСистемы ) );
			this.местоДляКартинки = new DevExpress.XtraEditors.PictureEdit();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.прогрессОбновления = new DevExpress.XtraEditors.MarqueeProgressBarControl();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.текстШагаОбновления = new DevExpress.XtraEditors.LabelControl();
			( ( System.ComponentModel.ISupportInitialize ) ( this.местоДляКартинки.Properties ) ).BeginInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.прогрессОбновления.Properties ) ).BeginInit();
			this.SuspendLayout();
			// 
			// местоДляКартинки
			// 
			this.местоДляКартинки.EditValue = ( ( object ) ( resources.GetObject( "местоДляКартинки.EditValue" ) ) );
			this.местоДляКартинки.Location = new System.Drawing.Point( 12, 12 );
			this.местоДляКартинки.Name = "местоДляКартинки";
			this.местоДляКартинки.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.местоДляКартинки.Properties.Appearance.Options.UseBackColor = true;
			this.местоДляКартинки.Size = new System.Drawing.Size( 100, 96 );
			this.местоДляКартинки.TabIndex = 0;
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point( 118, 12 );
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size( 310, 13 );
			this.labelControl1.TabIndex = 1;
			this.labelControl1.Text = "На сервере произошло обновление прикладной подсистемы.";
			// 
			// прогрессОбновления
			// 
			this.прогрессОбновления.EditValue = 0;
			this.прогрессОбновления.Location = new System.Drawing.Point( 118, 79 );
			this.прогрессОбновления.Name = "прогрессОбновления";
			this.прогрессОбновления.Size = new System.Drawing.Size( 319, 29 );
			this.прогрессОбновления.TabIndex = 2;
			// 
			// labelControl2
			// 
			this.labelControl2.Location = new System.Drawing.Point( 119, 29 );
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size( 250, 13 );
			this.labelControl2.TabIndex = 3;
			this.labelControl2.Text = "В данный момент выполняется загрузка файлов.";
			// 
			// labelControl3
			// 
			this.labelControl3.Location = new System.Drawing.Point( 119, 60 );
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size( 88, 13 );
			this.labelControl3.TabIndex = 4;
			this.labelControl3.Text = "Шаг обновления:";
			// 
			// текстШагаОбновления
			// 
			this.текстШагаОбновления.Location = new System.Drawing.Point( 214, 60 );
			this.текстШагаОбновления.Name = "текстШагаОбновления";
			this.текстШагаОбновления.Size = new System.Drawing.Size( 63, 13 );
			this.текстШагаОбновления.TabIndex = 5;
			this.текстШагаОбновления.Text = "[0/4] начало";
			// 
			// ФормаПроцессаОбновленияСистемы
			// 
			this.ClientSize = new System.Drawing.Size( 443, 120 );
			this.ControlBox = false;
			this.Controls.Add( this.текстШагаОбновления );
			this.Controls.Add( this.labelControl3 );
			this.Controls.Add( this.labelControl2 );
			this.Controls.Add( this.прогрессОбновления );
			this.Controls.Add( this.labelControl1 );
			this.Controls.Add( this.местоДляКартинки );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ФормаПроцессаОбновленияСистемы";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Выполняется обновление прикладной подсистемы";
			( ( System.ComponentModel.ISupportInitialize ) ( this.местоДляКартинки.Properties ) ).EndInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.прогрессОбновления.Properties ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		#endregion

		private DevExpress.XtraEditors.PictureEdit местоДляКартинки;
		private DevExpress.XtraEditors.LabelControl labelControl1;
		private DevExpress.XtraEditors.MarqueeProgressBarControl прогрессОбновления;
		private DevExpress.XtraEditors.LabelControl labelControl2;
		private DevExpress.XtraEditors.LabelControl labelControl3;
		private DevExpress.XtraEditors.LabelControl текстШагаОбновления;




	}
}