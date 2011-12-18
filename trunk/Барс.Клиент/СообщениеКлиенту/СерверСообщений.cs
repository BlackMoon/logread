using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Барс.Клиент.СообщениеКлиенту
{
	public class НастройкиСервисаСообщений
	{
		private static string адресСервисаКлиента = string.Empty;

		public static string АдресСервисаКлиента
		{
			get { return НастройкиСервисаСообщений.адресСервисаКлиента; }
			set { НастройкиСервисаСообщений.адресСервисаКлиента = value; }
		}
		
	}

	internal class ПараметрыСообщения
	{
		private string кто = "Администратор";

		public string Кто
		{
			get { return кто; }
			set { кто = value; }
		}

		private string сообщение = string.Empty;

		public string Сообщение
		{
			get { return сообщение; }
			set { сообщение = value; }
		}

		private string host = string.Empty;

		public string Host
		{
			get { return host; }
			set { host = value; }
		}

		public ПараметрыСообщения(string кто, string сообщение, string host)
		{
			Кто = кто;
			Сообщение = сообщение;
			Host = host;
		}
	}

	public class СерверСообщений : MarshalByRefObject, IСообщениеКлиенту
	{
		#region IСообщениеКлиенту Members

		public void ПослатьСообщениеКлиенту(string кто, string текстСообщения, string host)
		{
			ПараметрыСообщения параметры = new ПараметрыСообщения(кто, текстСообщения, host);

			Thread thread = new Thread(new ParameterizedThreadStart(ПоказатьОкноСообщения));
			thread.Start(параметры);
		}

		#endregion

		private void ПоказатьОкноСообщения(object объект)
		{
			if (объект is ПараметрыСообщения)
			{
				ПараметрыСообщения параметры = объект as ПараметрыСообщения;

				string заголовок = string.Format("{0} посылает Вам сообщение: ", параметры.Кто);
				ОкноСообщения окно = new ОкноСообщения(заголовок, параметры.Сообщение, параметры.Host);
				if (окно.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					DevExpress.XtraEditors.XtraMessageBox.Show("Сообщение было успешно отправлено.", "Сообщение", System.Windows.Forms.MessageBoxButtons.OK,
						System.Windows.Forms.MessageBoxIcon.Information);
				}

				//окно.Refresh();
			}
		}
	}
}
