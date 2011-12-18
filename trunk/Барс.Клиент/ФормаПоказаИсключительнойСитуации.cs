using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Барс.Клиент
{
	public partial class ФормаПоказаИсключительнойСитуации : XtraForm
	{
		#region Открытое свойство: Исключение
		private Exception исключение;

		public Exception Исключение
		{
			get { return исключение; }
			set { исключение = value; }
		}

		#endregion

		#region Открытое свойство: Сообщение
		private string сообщение = "";

		public string Сообщение
		{
			get { return сообщение; }
			set { сообщение = value; }
		}
		#endregion

		#region Открытое свойство: Заголовок 
		private string заголовок = "";

		public string Заголовок
		{
			get { return заголовок; }
			set { заголовок = value; }
		}
		#endregion

		public ФормаПоказаИсключительнойСитуации()
		{
			InitializeComponent();
		}

		private void ФормаПоказаИсключительнойСитуации_Load( object sender, EventArgs e )
		{
			if( !string.IsNullOrEmpty( Заголовок ) )
			{
				this.Text = Заголовок;
			}
			if( !string.IsNullOrEmpty( Сообщение ) )
			{
				this.memoEdit_Сообщение.Text = Сообщение;
			}
			if( Исключение != null )
			{
				this.memoExEdit_Подробно.Text = Исключение.Message + "\r\n\r\n";
				this.memoExEdit_Подробно.Text += "Стек вызовов:\r\n" + Исключение.StackTrace;

				if( Исключение.InnerException != null )
				{
					this.memoExEdit_Подробно.Text += "\r\n\r\n>>>> Внутреннее исключение:\r\n" + Исключение.InnerException.Message + "\r\n\r\n";
					this.memoExEdit_Подробно.Text += "Стек вызовов внутреннего исключения:\r\n" + Исключение.InnerException.StackTrace;
				}
			}
		}

		public static void Показать( string ТекстСообщения, string Заголовок, Exception Исключение )
		{
			try
			{
				ФормаПоказаИсключительнойСитуации форма = new ФормаПоказаИсключительнойСитуации();
				форма.Сообщение = ТекстСообщения;
				форма.Заголовок = Заголовок;
				форма.Исключение = Исключение;

				форма.ShowDialog();
			}
			catch( Exception )
			{
			}
		}

		public static void Показать( string ТекстСообщения, Exception Исключение)
		{
			ФормаПоказаИсключительнойСитуации.Показать( ТекстСообщения, "", Исключение );
		}
	}
}