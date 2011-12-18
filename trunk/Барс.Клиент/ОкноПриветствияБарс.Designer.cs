namespace Барс.Клиент
{
	partial class ОкноПриветствияБарс
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
			this.pictureBox_Картинка = new System.Windows.Forms.PictureBox();
			this.Текст = new DevExpress.XtraEditors.LabelControl();
			( ( System.ComponentModel.ISupportInitialize ) ( this.pictureBox_Картинка ) ).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox_Картинка
			// 
			this.pictureBox_Картинка.Image = global::Барс.Клиент.Properties.Resources.Старт;
			this.pictureBox_Картинка.Location = new System.Drawing.Point( 0, 0 );
			this.pictureBox_Картинка.Name = "pictureBox_Картинка";
			this.pictureBox_Картинка.Size = new System.Drawing.Size( 400, 320 );
			this.pictureBox_Картинка.TabIndex = 0;
			this.pictureBox_Картинка.TabStop = false;
			// 
			// Текст
			// 
			this.Текст.Appearance.BackColor = System.Drawing.Color.Lavender;
			this.Текст.Appearance.Options.UseBackColor = true;
			this.Текст.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.Текст.Location = new System.Drawing.Point( 1, 320 );
			this.Текст.Name = "Текст";
			this.Текст.Padding = new System.Windows.Forms.Padding( 2, 0, 0, 0 );
			this.Текст.Size = new System.Drawing.Size( 399, 14 );
			this.Текст.TabIndex = 1;
			// 
			// ОкноПриветствияБарс
			// 
			this.ClientSize = new System.Drawing.Size( 400, 335 );
			this.ControlBox = false;
			this.Controls.Add( this.Текст );
			this.Controls.Add( this.pictureBox_Картинка );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ОкноПриветствияБарс";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ОкноПриветствияБарс";
			( ( System.ComponentModel.ISupportInitialize ) ( this.pictureBox_Картинка ) ).EndInit();
			this.ResumeLayout( false );

		}

		#endregion

		public DevExpress.XtraEditors.LabelControl Текст;
		public System.Windows.Forms.PictureBox pictureBox_Картинка;

	}
}