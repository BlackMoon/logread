using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Барс.Клиент
{
	public static class ПотокиПриложения
	{
		#region Открытые методы запуска потока
		/// <summary>
		/// Метод запуска нового потока приложения с помощью указания типа объекта - точки входа потока
		/// </summary>
		/// <param name="ТипОбъектаТочкиВходаПотока"></param>
		public static void ЗапуститьНовыйПоток( Type ТипОбъектаТочкиВходаПотока )
		{
			if (ТипОбъектаТочкиВходаПотока == null)
			{
				throw new ArgumentNullException( "Не задан тип объекта - точки входа нового потока приложения." );
			}

			ПодготовитьОбъектНовогоПотока().Start(ТипОбъектаТочкиВходаПотока);
		}

		/// <summary>
		/// Метод запуска нового потока приложения с помощью указания главной формы потока
		/// </summary>
		/// <param name="ГлавнаяФормаПотока"></param>
		private static void ЗапуститьНовыйПоток(System.Windows.Forms.Form ГлавнаяФормаПотока)
		{
			if (ГлавнаяФормаПотока == null)
			{
				throw new ArgumentNullException("Не задана главная форма нового потока приложения.");
			}

			ПодготовитьОбъектНовогоПотока().Start(ГлавнаяФормаПотока);
		}

		/// <summary>
		/// Обобщенный метод запуска нового потока приложения с помощью указания либо
		/// главной формы потока, либо объекта, поддерживающего интерфейс точки входа потока
		/// </summary>
		/// <param name="ОбъектТочкиВходаПотока"></param>
		public static void ЗапуститьНовыйПоток(object ОбъектТочкиВходаПотока)
		{
			if (ОбъектТочкиВходаПотока == null)
			{
				throw new ArgumentNullException("Не задан объект точки входа нового потока приложения.");
			}
			else if (ОбъектТочкиВходаПотока.GetType().GetInterface("ИнтерфейсТочкиВходаПотока") != null)
			{
				// точка входа потока поддерживает интерфейс точки входа. пытаемся получить экранную форму оттуда
				ПодготовитьОбъектНовогоПотока().Start( ОбъектТочкиВходаПотока );
			}
			else
			{
				throw new ArgumentException("Объект, передаваемый в качестве точки входа потока приложения либо типом, либо должен поддерживать интерфейс точки входа потока.");
			}
		}
		#endregion

		#region Внутренний метод подготовки треда для запуска 
		private static System.Threading.Thread ПодготовитьОбъектНовогоПотока()
		{
			System.Threading.Thread новыйПоток = new System.Threading.Thread( new System.Threading.ParameterizedThreadStart( ПотокиПриложения.СтартоватьТред ) );

			новыйПоток.SetApartmentState(System.Threading.ApartmentState.STA);

			return новыйПоток;
		}
		#endregion

		#region Внутренний метод старта потока приложения
		/// <summary>
		/// Метод инициализации нового треда
		/// </summary>
		/// <param name="ПараметрТреда"></param>
		private static void СтартоватьТред(object ПараметрТреда)
		{
			// мы уже находимся в новом треде

			System.Windows.Forms.Form ГлавнаяФормаПотока = null;
			try
			{
				// берем домен текущего приложения
				AppDomain доменПриложения = System.Threading.Thread.GetDomain();
				// назначаем перехватчик необработанных исключений потока
				Application.ThreadException += new System.Threading.ThreadExceptionEventHandler( ПотокиПриложения.ПриНеобработанномИсключенииПотока );

				

				if (ПараметрТреда is System.Windows.Forms.Form)
				{
					ГлавнаяФормаПотока = ПараметрТреда as System.Windows.Forms.Form;
				}
				else if( ПараметрТреда is Type )
				{
					Type типСтартовогоОбъекта = ПараметрТреда as Type;
					if( типСтартовогоОбъекта == typeof( System.Windows.Forms.Form ) ||
						типСтартовогоОбъекта.IsSubclassOf( typeof( System.Windows.Forms.Form ) ) )
					{
						ГлавнаяФормаПотока = Activator.CreateInstance( типСтартовогоОбъекта ) as System.Windows.Forms.Form;
					}
					else if( типСтартовогоОбъекта.GetInterface( "ИнтерфейсТочкиВходаПотока" ) != null )
					{
						object объектТочкиВходаПотока = Activator.CreateInstance( типСтартовогоОбъекта );
						if( объектТочкиВходаПотока != null )
						{
							ГлавнаяФормаПотока = типСтартовогоОбъекта.InvokeMember( "ТочкаВходаПотока", BindingFlags.InvokeMethod, null, объектТочкиВходаПотока, new object [] { } ) as System.Windows.Forms.Form;
						}
					}
				}
				else if( ПараметрТреда.GetType().GetInterface( "ИнтерфейсТочкиВходаПотока" ) != null )
				{
					ГлавнаяФормаПотока = ПараметрТреда.GetType().InvokeMember( "ТочкаВходаПотока", BindingFlags.InvokeMethod, null, ПараметрТреда, new object [] { } ) as System.Windows.Forms.Form;
				}

				if( ГлавнаяФормаПотока == null )
				{
					throw new ИсключениеБарсПриложения( "Не удалось получить главную форму нового потока приложения." );
				}

				bool результатУстановкиПараметровОкна = false;

				try
				{
					результатУстановкиПараметровОкна = ( bool ) ГлавнаяФормаПотока.GetType().InvokeMember( "УстановитьПараметрыОкна", BindingFlags.InvokeMethod, null, ГлавнаяФормаПотока, new string [] { "" } );
				}
				catch( Exception )
				{
					результатУстановкиПараметровОкна = true;
				}

				if( результатУстановкиПараметровОкна )
				{
					// регистрируем окно в нашем реестре

					НастольноеПриложение.ДобавитьТред( ГлавнаяФормаПотока, "" );

					ApplicationContext контексТреда = new ApplicationContext( ГлавнаяФормаПотока );

					Application.Run( контексТреда );

					Type тип = Type.GetType( "Барс.Ядро.МенеджерБД,Ядро" );
					if( тип != null )
					{
						тип.InvokeMember( "ОсвободитьРесурсыТреда", BindingFlags.InvokeMethod, null, null, null );
					}
				}
			}
			catch (Exception exc)
			{
				ФормаПоказаИсключительнойСитуации.Показать(exc.Message, exc);
			}
			finally
			{
				if (ГлавнаяФормаПотока != null)
				{
					НастольноеПриложение.ИсключитьТред();
				}
			}

		}
		#endregion

		#region Внутренний метод реакции на необработанные исключительные ситуации потока приложения
		private static void ПриНеобработанномИсключенииПотока(object sender, System.Threading.ThreadExceptionEventArgs t)
		{
			if (!Барс.Клиент.Приложение.РежимWebПриложения)
			{
				ФормаПоказаИсключительнойСитуации.Показать( "При выполнении прикладной подсистемы произошла необработанная исключительная ситуация.", t.Exception );
			}
		}
		#endregion
	}
}
