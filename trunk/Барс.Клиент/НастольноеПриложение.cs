using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Барс.Клиент
{
	/// <summary>
	/// Статический класс, содержащий основные функции настольного приложения
	/// </summary>
	public sealed class НастольноеПриложение : Приложение
	{
		#region Открытое свойство: Ссылка на главное окно приложения
		internal static ГлавноеОкно главноеОкно = null;

		/// <summary>
		/// Метод доступа к главному окну настольного приложения
		/// </summary>
		public static ГлавноеОкно ГлавноеОкно
		{
			get 
			{ 
				return НастольноеПриложение.главноеОкно; 
			}
		}


		#endregion

		#region Открытое свойство: Метка приложения для заголовков окон
		private static string меткаПриложенияДляЗаголовковОкон = "";

		public static string МеткаПриложенияДляЗаголовковОкон
		{
			get 
			{
				if (меткаПриложенияДляЗаголовковОкон.Trim().ToLower() == "нет")
				{
					return "";
				}

				if (string.IsNullOrEmpty( меткаПриложенияДляЗаголовковОкон ))
				{
					string результат = РабочаяПапка;

					if (РабочаяПапка.Length > 40)
					{
						результат = результат.Substring(0, 10) + "..." + результат.Substring(РабочаяПапка.Length - 30, 30);
					}
					return результат;
				}
				return меткаПриложенияДляЗаголовковОкон; 
			}
			set { меткаПриложенияДляЗаголовковОкон = value; }
		}
		#endregion

		#region Метод авторизации в приложении
		/// <summary>
		/// Метод, с помощью которого происходит авторизация логического пользователя в системе
		/// </summary>
		/// <param name="БратьТекущегоПользователя"></param>
		/// <returns></returns>
		internal static bool ПодключитьсяКЯдру( bool БратьТекущегоПользователя )
		{
			string ИмяПользователя = "";
			string ПарольПользователя = "";

			if( БратьТекущегоПользователя )
			{
				ИмяПользователя = ТекущийПользователь.Логин;
			}
			else
			{
				ИмяПользователя = Параметры [ "Пользователь" ];
				ПарольПользователя = Параметры [ "Пароль" ];
			}

			string Профиль = Параметры [ "Профиль" ];
			bool черезДиалог = string.IsNullOrEmpty( ИмяПользователя ) || string.IsNullOrEmpty( ПарольПользователя );
			int количествоПопыток = 0;
			string ошибка = "";
			Type ПодключитьсяКЯдру = Type.GetType( "Барс.Ядро.МенеджерБД,Ядро" );
			Type ТипВводаПароля = Type.GetType( "Барс.ВыборПользователяПароляБарс,Ядро" );
			object ОбъектВводаПароля = Activator.CreateInstance( ТипВводаПароля );

			do
			{
				if( черезДиалог )
				{
					string [] новыйПароль = ( string [] ) ТипВводаПароля.InvokeMember( "Получить", System.Reflection.BindingFlags.InvokeMethod, null, ОбъектВводаПароля, new object [] { ИмяПользователя, ПарольПользователя } );
					if( новыйПароль == null )
					{
						return false;
					}
					ИмяПользователя = новыйПароль [ 0 ];
					ПарольПользователя = новыйПароль [ 1 ];
				}
				черезДиалог = true;
				ошибка = ( string ) ПодключитьсяКЯдру.InvokeMember( "ПодключитьсяКЯдру", System.Reflection.BindingFlags.InvokeMethod, null, null, new object [] { ИмяПользователя, ПарольПользователя, Профиль } );
				if( ошибка != null )
				{
					DevExpress.XtraEditors.XtraMessageBox.Show( ошибка, "БАРС", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error );
				}
				количествоПопыток++;

			} while( ошибка != null && количествоПопыток < 3 );
			return ошибка == null;
		}
		#endregion

		#region Работа со списками окон
		
		#region Открытое свойство: Список окон
		private static List<СписокОконТреда> списокОкон = new List<СписокОконТреда>();
		/// <summary>
		/// Список окон, открытых в системе
		/// </summary>
		public static List<СписокОконТреда> СписокОкон
		{
			get
			{
				List<СписокОконТреда> результат;
				lock( списокОкон )
				{
					результат = НастольноеПриложение.списокОкон;
				}
				return результат;
			}
		}
		#endregion

		#region Открытое свойство: Текущий список окон
		private static СписокОконТреда текущийСписокОкон = null;

		/// <summary>
		/// Список текущих окон потока
		/// </summary>
		public static СписокОконТреда ТекущийСписокОкон
		{
			get { return НастольноеПриложение.текущийСписокОкон; }
			set { НастольноеПриложение.текущийСписокОкон = value; }
		}
		#endregion

		/// <summary>
		/// Метод осущестляет поиск списка окон треда
		/// </summary>
		/// <param name="НомерТреда">Номер треда, для которого требуется найти окно</param>
		/// <returns>null в случае, если
		/// ничего не найдено</returns>
		private static СписокОконТреда НайтиСписокОконТреда( int НомерТреда )
		{
			lock( списокОкон )
			{
				foreach( СписокОконТреда окна in списокОкон )
				{
					if( окна.НомерТреда == НомерТреда )
					{
						return окна;
					}
				}
			}
			return null;
		}

		#region Операции добавления окон
		internal static void ДобавитьТред( Form ГлавноеОкно, string Раздел )
		{
			int номерТреда = System.Threading.Thread.CurrentThread.ManagedThreadId;
			СписокОконТреда списокОконТреда = НайтиСписокОконТреда( номерТреда );
			bool списокСуществует = ( списокОконТреда != null );

			lock( списокОкон )
			{
				if( списокОконТреда != null )
				{
					списокОконТреда.Очистить();
				}
				else
				{
					списокОконТреда = new СписокОконТреда();
				}
				списокОконТреда.номерТреда = номерТреда;
				списокОконТреда.главноеОкно = ГлавноеОкно;
				списокОконТреда.раздел = Раздел;
				списокОконТреда.списокОткрытыхОкон.Add( ГлавноеОкно );

				if( !списокСуществует )
				{
					списокОкон.Add( списокОконТреда );
				}
			}
		}

		public static void ДобавитьОкно( Form Окно )
		{
			int номерТреда = System.Threading.Thread.CurrentThread.ManagedThreadId;
			СписокОконТреда списокОконТреда = НайтиСписокОконТреда( номерТреда );

			lock( списокОкон )
			{
				if( списокОконТреда != null )
				{
					foreach( Form окно in списокОконТреда.списокОткрытыхОкон )
					{
						if( окно == Окно )
						{
							// ничего не делаем, окно уже зарезервированно
							return;
						}
					}
					списокОконТреда.списокОткрытыхОкон.Add( Окно );
				}
			}
		}
		#endregion

		#region Операции удаления окон
		internal static void ИсключитьТред()
		{
			int номерТреда = System.Threading.Thread.CurrentThread.ManagedThreadId;
			СписокОконТреда списокОконТреда = НайтиСписокОконТреда( номерТреда );

			lock( списокОкон )
			{
				if( списокОконТреда != null )
				{
					СписокОкон.Remove( списокОконТреда );
				}
			}
		}

		public static void ИсключитьОкно( Form Окно )
		{
			int номерТреда = System.Threading.Thread.CurrentThread.ManagedThreadId;
			СписокОконТреда списокОконТреда = НайтиСписокОконТреда( номерТреда );
			lock( списокОкон )
			{
				if( списокОконТреда != null )
				{
					списокОконТреда.списокОткрытыхОкон.Remove( Окно );
				}
			}
		}
		#endregion

		#region Операции по активации окна
		public static void АктивироватьОкно( Form Окно )
		{
			int номерПотока = System.Threading.Thread.CurrentThread.ManagedThreadId;
			СписокОконТреда окна = НайтиСписокОконТреда( номерПотока );

			if( окна != null && ГлавноеОкно != null )
			{
				ТекущийСписокОкон = окна;
				//ГлавноеОкно.lookUpEdit_ТекущееОкно.EditValue = ТекущийСписокОкон;
			}
		}
		#endregion

		#endregion
	}

	#region Класс: Список окон
	/// <summary>
	/// Класс для хранения списка открытых окон в треде
	/// </summary>
	public class СписокОконТреда
	{
		#region Свойства

		#region Открытое свойство: Номер треда
		internal int номерТреда;
		/// <summary>
		/// Номер треда, в котором открывается сей список окон
		/// </summary>
		public int НомерТреда
		{
			get
			{
				return номерТреда;
			}
		}


		#endregion

		#region Открытое свойство: Главное окно
		internal Form главноеОкно = null;
		/// <summary>
		/// Ссылка на главное окно треда
		/// </summary>
		public Form ГлавноеОкно
		{
			get
			{
				return главноеОкно;
			}
		}
		#endregion

		#region Открытое свойство: Раздел
		internal string раздел = "";
		/// <summary>
		/// Раздел системы, для которого открывается главное меню
		/// </summary>
		public string Раздел
		{
			get
			{
				return раздел;
			}
		}

		#endregion

		#region Открытое свойство: Открытые окна
		internal List<Form> списокОткрытыхОкон = new List<Form>();
		/// <summary>
		/// Cписок открытых в треде окон
		/// </summary>
		public List<Form> СписокОткрытыхОкон
		{
			get
			{
				return списокОткрытыхОкон;
			}
		}

		#endregion

		#region Открытое свойство: Текущее окно
		/// <summary>
		/// Текущее окно потока
		/// </summary>
		public Form ТекущееОкно
		{
			get
			{
				if( списокОткрытыхОкон.Count == 0 )
				{
					return главноеОкно;
				}
				return списокОткрытыхОкон [ списокОткрытыхОкон.Count - 1 ];
			}
		}
		#endregion

		#region Открытое свойство: Представление главного окна
		public string ПредставлениеГлавногоОкна
		{
			get
			{
				return главноеОкно.Text;
			}
		}
		#endregion

		#region Открытое свойство: Представление текущего окна
		public string ПредставлениеТекущегоОкна
		{
			get
			{
				if( ТекущееОкно != null )
				{
					return ТекущееОкно.Text;
				}
				return "";
			}
		}
		#endregion

		#region Открытое свойство: Текущее оформление
		private string текущийСтиль = "Money Twins";

		public string ТекущийСтиль
		{
			get
			{
				return текущийСтиль;
			}
			set
			{
				текущийСтиль = value;
			}
		}
		#endregion

		#endregion

		#region Методы

		internal void Очистить()
		{
			номерТреда = 0;
			главноеОкно = null;
			раздел = "";
			списокОткрытыхОкон.Clear();
		}

		#endregion

		/// <summary>
		/// Переопределенная функция перевода в строку
		/// </summary>
		public override string ToString()
		{
			if( ТекущееОкно != null )
			{
				return Раздел + " - " + ПредставлениеТекущегоОкна;
			}
			return "";
		}

		public string ТекстовоеПредставление
		{
			get
			{
				return ToString();
			}
		}
	}
	#endregion
}
