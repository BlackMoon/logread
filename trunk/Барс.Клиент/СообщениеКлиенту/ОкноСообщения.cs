using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Барс.Клиент.СообщениеКлиенту
{
	public partial class ОкноСообщения : DevExpress.XtraEditors.XtraForm
	{
		private string host = string.Empty;

		public ОкноСообщения(string заголовок, string текст, string host)
		{
			InitializeComponent();

			this.заголовок.Text = заголовок;
			this.текстСообщения.Text = текст;
			this.host = host;
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(host))
				{
					Барс.Клиент.СообщениеКлиенту.IСообщениеКлиенту _сообщение =
						(Барс.Клиент.СообщениеКлиенту.IСообщениеКлиенту)Activator.GetObject(typeof(Барс.Клиент.СообщениеКлиенту.IСообщениеКлиенту), host);

					_сообщение.ПослатьСообщениеКлиенту(Приложение.ТекущийПользователь.Наименование, textReply.Text, НастройкиСервисаСообщений.АдресСервисаКлиента);
				}
			}
			catch( Exception exc )
			{
				
				XtraMessageBox.Show("Ошибка при отправке сообщения.", this.Text, MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}
	}
}