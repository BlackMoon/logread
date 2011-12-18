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
	public partial class ОкноИсключения : XtraForm
	{
		public Exception исключение;

		public ОкноИсключения( Exception исключение )
		{
			InitializeComponent();

			this.исключение = исключение;
		}

		private void ОкноИсключения_Load(object sender, EventArgs e)
		{
			if( this.исключение != null )
			{
				memo_ТекстСообщения.Text = исключение.GetType().ToString() + ": " + исключение.Message;
				memo_СтекВызовов.Text = исключение.StackTrace;
			}
		}
	}
}