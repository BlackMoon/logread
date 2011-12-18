using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Xml;
using DevExpress.XtraEditors;
using System.Windows.Forms;


namespace Барс.Клиент
{
	#region GUI Компилятора
	/// <summary>
	/// Отображает окно процесса компиляции
	/// </summary>
	public class ФормаКомпилятора : DevExpress.XtraEditors.XtraForm
	{
		private DevExpress.XtraEditors.ListBoxControl listBoxControl1;
		private DevExpress.Utils.ToolTipController toolTipController;
		private System.ComponentModel.IContainer components;

		private КомпиляторПроекта компилятор = null;
		private ListBoxControl СписокСообщений;
		private LabelControl labelControl1;
		private ProgressBarControl индикатор;

		#region Внутреннее свойство: Показать форму модально при ошибке компилятора
		private bool показатьФормуМодальноПриОшибке = false;
		#endregion
		

		public ФормаКомпилятора( КомпиляторПроекта Компилятор ) : this( Компилятор, false )
		{
			
			
		}

		public ФормаКомпилятора( КомпиляторПроекта Компилятор, bool ПоказыватьОкноТолькоПриОшибке )
		{
			InitializeComponent();

			if( Компилятор != null )
			{
				this.компилятор = Компилятор;

				показатьФормуМодальноПриОшибке = ПоказыватьОкноТолькоПриОшибке;

				if( !ПоказыватьОкноТолькоПриОшибке )
				{
					Компилятор.ФормаКомпилятора = this;
				}
				// цепляемся к событию компилятора
				Компилятор.СобытиеПроцессаКомпиляции += new КомпиляторПроекта.ОбработчикСобытияКомпилятора( компилятор_СобытиеПроцессаКомпиляции );
			}

			ДобавитьСообщение( "Процесс построения проекта запущен", true );
		}

		void компилятор_СобытиеПроцессаКомпиляции( object Отправитель, КомпиляторПроекта.АргументыСобытияКомпилятора Аргументы )
		{
			Application.DoEvents();

			if( Аргументы.ТипСобытия == КомпиляторПроекта.ТипСобытияКомпилятора.НачалоКомпиляцииКомпонента )
			{
				ДобавитьСообщение( "Сборка компонента '" + Аргументы.НазваниеКомпонента + "'...", true );
			}
			else if( Аргументы.ТипСобытия == КомпиляторПроекта.ТипСобытияКомпилятора.КонецКомпиляцииКомпонента )
			{
				индикатор.Increment( 1 );
				ДобавитьСообщение( "Сборка компонента '" + Аргументы.НазваниеКомпонента + "' успешно завершена", true );
			}
			else if( Аргументы.ТипСобытия == КомпиляторПроекта.ТипСобытияКомпилятора.КонецКомпиляцииПроекта )
			{
				ДобавитьСообщение( "Проект успешно построен", true );
			}
			else if( Аргументы.ТипСобытия == КомпиляторПроекта.ТипСобытияКомпилятора.НачалоКомпиляцииПроекта )
			{
				индикатор.Properties.Minimum = 0;
				индикатор.Properties.Maximum = компилятор.КоличествоКомпонентовПроекта;
				индикатор.Properties.Step = 1;

			}
			else if( Аргументы.ТипСобытия == КомпиляторПроекта.ТипСобытияКомпилятора.ОшибкаКомпиляции )
			{
				ДобавитьСообщение( "Не удалось выполнить сборку проекта. Ниже приведены ошибки компилятора:", true );
				if( Отправитель != null && Отправитель is КомпиляторПроекта )
				{
					КомпиляторПроекта компилятор = Отправитель as КомпиляторПроекта;
					int i = 0;
					foreach( System.CodeDom.Compiler.CompilerError ошибкаКомпиляции in компилятор.СписокОшибок )
					{
						if( i++ == 20 )
						{
							// выводим только 20 первых сообщений
							ДобавитьСообщение( "есть еще ошибки...", false );
							break;
						}
						ДобавитьСообщение( ошибкаКомпиляции.ErrorNumber + " " + ошибкаКомпиляции.ErrorText + " " + ошибкаКомпиляции.FileName + " " + ошибкаКомпиляции.Line.ToString(), false );
					}
				}

				Application.DoEvents();

				// в случае, если произошла ошибка, то необходимо показать форму в диалоговом режиме
				//if( показатьФормуМодальноПриОшибке  )
				//{
					this.Visible = false;
					this.ShowDialog();
				//}
			}
			Application.DoEvents();
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.listBoxControl1 = new DevExpress.XtraEditors.ListBoxControl();
			this.toolTipController = new DevExpress.Utils.ToolTipController( this.components );
			this.СписокСообщений = new DevExpress.XtraEditors.ListBoxControl();
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.индикатор = new DevExpress.XtraEditors.ProgressBarControl();
			( ( System.ComponentModel.ISupportInitialize ) ( this.listBoxControl1 ) ).BeginInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.СписокСообщений ) ).BeginInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.индикатор.Properties ) ).BeginInit();
			this.SuspendLayout();
			// 
			// listBoxControl1
			// 
			this.listBoxControl1.Location = new System.Drawing.Point( 0, 0 );
			this.listBoxControl1.Name = "listBoxControl1";
			this.listBoxControl1.Size = new System.Drawing.Size( 120, 95 );
			this.listBoxControl1.TabIndex = 0;
			// 
			// toolTipController
			// 
			this.toolTipController.AutoPopDelay = 10000;
			this.toolTipController.Rounded = true;
			this.toolTipController.ShowBeak = true;
			this.toolTipController.ToolTipType = DevExpress.Utils.ToolTipType.SuperTip;
			// 
			// СписокСообщений
			// 
			this.СписокСообщений.Location = new System.Drawing.Point( 12, 12 );
			this.СписокСообщений.Name = "СписокСообщений";
			this.СписокСообщений.Size = new System.Drawing.Size( 632, 205 );
			this.СписокСообщений.TabIndex = 0;
			this.СписокСообщений.ToolTipController = this.toolTipController;
			this.СписокСообщений.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Asterics;
			this.СписокСообщений.ToolTipTitle = "Сообщение компилятора";
			this.СписокСообщений.MouseMove += new System.Windows.Forms.MouseEventHandler( this.СписокСообщений_MouseMove );
			this.СписокСообщений.SelectedIndexChanged += new System.EventHandler( this.СписокСообщений_SelectedIndexChanged );
			// 
			// labelControl1
			// 
			this.labelControl1.Location = new System.Drawing.Point( 12, 228 );
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size( 261, 13 );
			this.labelControl1.TabIndex = 6;
			this.labelControl1.Text = "Выполняется построение прикладной подсистемы:";
			// 
			// индикатор
			// 
			this.индикатор.Location = new System.Drawing.Point( 279, 223 );
			this.индикатор.Name = "индикатор";
			this.индикатор.Size = new System.Drawing.Size( 365, 23 );
			this.индикатор.TabIndex = 7;
			// 
			// ФормаКомпилятора
			// 
			this.Appearance.Font = new System.Drawing.Font( "Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 204 ) ) );
			this.Appearance.Options.UseBackColor = true;
			this.Appearance.Options.UseFont = true;
			this.ClientSize = new System.Drawing.Size( 656, 260 );
			this.Controls.Add( this.индикатор );
			this.Controls.Add( this.labelControl1 );
			this.Controls.Add( this.СписокСообщений );
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ФормаКомпилятора";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.toolTipController.SetSuperTip( this, null );
			this.Text = "Выполняется построение прикладной подсистемы";
			this.Load += new System.EventHandler( this.ФормаКомпилятора_Load );
			( ( System.ComponentModel.ISupportInitialize ) ( this.listBoxControl1 ) ).EndInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.СписокСообщений ) ).EndInit();
			( ( System.ComponentModel.ISupportInitialize ) ( this.индикатор.Properties ) ).EndInit();
			this.ResumeLayout( false );
			this.PerformLayout();

		}

		public void ДобавитьСообщение( string текстСообщения, bool ДобавитьДатаВремя )
		{
			СписокСообщений.Items.Add( ( ДобавитьДатаВремя ? DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " " : "" ) + текстСообщения );
			this.Refresh();
		}

		private void СписокСообщений_SelectedIndexChanged( object sender, EventArgs e )
		{
			СписокСообщений.ToolTip = "";
			if( СписокСообщений.SelectedIndex >= 0 && this.Visible )
			{
				bool этоОшибка = СписокСообщений.Items [ СписокСообщений.SelectedIndex ].ToString().Trim().ToLower().StartsWith( "cs" );
				if( этоОшибка )
				{
					toolTipController.ShowHint( СписокСообщений.Items [ СписокСообщений.SelectedIndex ].ToString(), "Ошибка компиляции проекта" );
				}
				else
				{
					toolTipController.ShowHint( СписокСообщений.Items [ СписокСообщений.SelectedIndex ].ToString(), "Сообщение" );
				}
			}
			
		}

		private void СписокСообщений_MouseMove( object sender, System.Windows.Forms.MouseEventArgs e )
		{
			int index = СписокСообщений.SelectedIndex;
			if( index >= 0 )
			{
			}
		}

		private void ФормаКомпилятора_Load( object sender, EventArgs e )
		{
		}
	}
	#endregion
}
