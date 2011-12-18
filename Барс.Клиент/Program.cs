using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Барс.Клиент
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main( string [] args )
		{
			// чтение параметров командной строки
			try
			{
				// загружаем настройки приложения
				List<string> параметрыЗапуска = new List<string>();
				foreach( string параметрЗапуска in args )
				{
					параметрыЗапуска.Add( параметрЗапуска );
				}

				НастольноеПриложение.ЗагрузитьПараметрыПриложения( параметрыЗапуска );
                
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new ГлавноеОкно());
			}
			catch( Exception ошибка )
			{
				DevExpress.XtraEditors.XtraMessageBox.Show(ошибка.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}