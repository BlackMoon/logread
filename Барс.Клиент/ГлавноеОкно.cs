using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Net;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars;
using System.Xml;
using Oracle.DataAccess.Client;

namespace Барс.Клиент
{
	/// <summary>
	/// Главное окно БАРС клиента
	/// </summary>
	public partial class ГлавноеОкно : RibbonForm
	{
		private XmlDocument ToolBarXml = null;

		private bool настройкиПанелиИзменились = false;

		public bool НастройкиПанелиИзменились
		{
			get { return настройкиПанелиИзменились; }
			set { настройкиПанелиИзменились = value; }
		}
		
		private const string ИМЯ_ФАЙЛА_С_КАРТИНКОЙ = "БарсКлиентЗагрузка.bmp";

        // Завершение работы
        private const string ИмяФайлаЗавершенияРаботы = "ЗавершениеРаботы.log";
        StreamWriter файлЗавершенияРаботы = null;

		// Сервис отправки сообщений
		private const int ПОРТ_СЕРВИСА_СООБЩЕНИЙ = 777;
		
		private const string ИМЯ_СЕРВИСА_СООБЩЕНИЙ = "BARSII/messages";

		private List<Form> СписокДочернихОкон;

		private int индексРаздела = -1;

		public int ИндексРаздела
		{
			get { return индексРаздела; }
			set { индексРаздела = value; }
		}
		
		private ImageList imageList_ДеревоМеню = new ImageList();

		public ImageList ImageList_ДеревоМеню
		{
			get { return imageList_ДеревоМеню; }
			set { imageList_ДеревоМеню = value; }
		}

		private List<ЭлементВыпадающегоМеню> списокЭлементовВыпадающегоМеню = new List<ЭлементВыпадающегоМеню>();

		public List<ЭлементВыпадающегоМеню> СписокЭлементовВыпадающегоМеню
		{
			get { return списокЭлементовВыпадающегоМеню; }
			set { списокЭлементовВыпадающегоМеню = value; }
		}
		
		private List<ЭлементыМеню> списокЭлементовМеню = new List<ЭлементыМеню>();

		public List<ЭлементыМеню> СписокЭлементовМеню
		{
			get { return списокЭлементовМеню; }
			set { списокЭлементовМеню = value; }
		}

		private List<ЭлементыМеню> списокСохраняемыхЭлементов = new List<ЭлементыМеню>();

		public List<ЭлементыМеню> СписокСохраняемыхЭлементов
		{
			get { return списокСохраняемыхЭлементов; }
			set { списокСохраняемыхЭлементов = value; }
		}

		private СловарьНазваний словарьНазванийЭлементов = new СловарьНазваний();

		public СловарьНазваний СловарьНазванийЭлементов
		{
			get { return словарьНазванийЭлементов; }
			set { словарьНазванийЭлементов = value; }
		}
        
		public System.ComponentModel.IContainer Components
		{
			get { return components; }
		}

		public DevExpress.LookAndFeel.DefaultLookAndFeel DefaultLookAndFeel
		{
			get
			{
				return defaultLookAndFeel;
			}
		}

		private DevExpress.XtraTreeList.TreeList деревоГлавногоМеню = new DevExpress.XtraTreeList.TreeList();

		public DevExpress.XtraTreeList.TreeList ДеревоГлавногоМеню
		{
			get { return деревоГлавногоМеню; }
			set { деревоГлавногоМеню = value; }
		}


		/// <summary>
		/// Конструктор главного окна БАРС клиента
		/// Помимо формирования
		/// </summary>
		public ГлавноеОкно()
		{
			СписокДочернихОкон = new List<Form>();
			InitializeComponent();
			НастольноеПриложение.главноеОкно = this;
			defaultLookAndFeel.LookAndFeel.StyleChanged += new EventHandler( LookAndFeel_StyleChanged );
            
        }

		

		/// <summary>
		/// Вывод сообщения на экран
		/// </summary>
		/// <param name="текстСообщения">Текст сообщения для показа</param>
		public void Сообщить(string текстСообщения)
		{
		}
		public static void ПерехватчикНеобработанныхИсключений(object sender, UnhandledExceptionEventArgs args)
		{
			ОкноИсключения окно = new ОкноИсключения(args.ExceptionObject as Exception);
			окно.ShowDialog();
		}

        #region Метод проверяет наличие пароля в строке соединения с БД.
        private bool ПарольУказан(OracleConnection соединениеСБазой)
        {
            if (соединениеСБазой.ConnectionString.ToLower().Contains("password=;"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region Метод возвращает пароль по ключу.
        public string ПолучитьПароль(string keyFile)
        {
            try
            {
                if (File.Exists(keyFile))
                {
                    StreamReader reader = File.OpenText(keyFile);
                    string ключ = reader.ReadToEnd();
                    reader.Close();

                    Барс.СерверАутентификации.КлиентСервераАутентификации клиент =
                        new Барс.СерверАутентификации.КлиентСервераАутентификации();

                    Барс.СерверАутентификации.ПараметрыСоединенияСБазойДанных соединения =
                        клиент.ПолучитьПараметрыСоединенияСБазойДанных(ключ);

                    return соединения.Пароль;
                }
                else
                    throw new Exception("Ошибка: не найден файл с ключем сервера");
            }
            catch (Exception err)
            {
                throw new Exception("Ошибка (" + err.GetType().FullName + "): " + err.Message);
            }
        }
        #endregion

		private void ГлавноеОкно_Load(object sender, EventArgs e)
		{
			ОкноПриветствияБарс окноПриветствия = new ОкноПриветствияБарс();
			// установка фильтра необработанных исключений
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ПерехватчикНеобработанныхИсключений);
			окноПриветствия = new ОкноПриветствияБарс();

            #region Подключение классов локализации
			DevExpress.XtraEditors.Controls.Localizer.Active = new Барс.Локализация.XtraEditorsРусский();
			DevExpress.XtraGrid.Localization.GridLocalizer.Active = new Барс.Локализация.XtraGridРусский();
			DevExpress.XtraBars.Localization.BarLocalizer.Active = new Барс.Локализация.XtraBarРусский();
			DevExpress.XtraLayout.Localization.LayoutLocalizer.Active = new Барс.Локализация.XtraLayoutРусский();
			DevExpress.XtraTreeList.Localization.TreeListLocalizer.Active = new Барс.Локализация.XtraTreeListРусский();
			DevExpress.XtraPivotGrid.Localization.PivotGridLocalizer.Active = new Барс.Локализация.XtraPivotGridРусский();
            #endregion
            #region Применение скина для заголовков окон
            DevExpress.Skins.SkinManager.EnableFormSkins();
			DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

			DevExpress.UserSkins.BonusSkins.Register();
			DevExpress.UserSkins.OfficeSkins.Register();

            #endregion
            #region Загрузка картинки окна приветствия
            try
			{
				Bitmap картинка;
				КомпонентСистемы компонент = Приложение.ПолучитьКомпонент( "Приложение" );
				if( компонент != null )
				{
					string ПутьКРесурсамПриложения = "";
					
					try
					{
						string ПутьККлиенту = Приложение.РабочаяПапка;

						string РабочаяПапкаКлиента = ПутьККлиенту;

						if (Приложение.РежимWebПриложения)
						{
							DirectoryInfo директория = Directory.GetParent(ПутьККлиенту);

							РабочаяПапкаКлиента = директория.FullName;
						}

						ПутьКРесурсамПриложения = Path.Combine(РабочаяПапкаКлиента, "Ресурсы");

					}
					catch(Exception)
					{ }
					if (File.Exists(ПутьКРесурсамПриложения + "\\Старт.bmp"))
					{
						string путьККартинке = Path.Combine(Path.GetTempPath(), Приложение.ИмяСхемы + "_" + ИМЯ_ФАЙЛА_С_КАРТИНКОЙ);
						if (File.Exists(путьККартинке))
						{
							File.SetAttributes(путьККартинке, FileAttributes.Normal);
							File.Delete(путьККартинке);
						}

						File.Copy(ПутьКРесурсамПриложения + "\\Старт.bmp", путьККартинке, true);
						System.Drawing.Size размерыКартинки = new System.Drawing.Size(400, 320);						
						картинка = new Bitmap(путьККартинке);
						картинка = new Bitmap(картинка, размерыКартинки);
						if (картинка != null )
						{
							окноПриветствия.pictureBox_Картинка.Image = картинка;
						}
					}
					else if( File.Exists( компонент.ПапкаРасположения + "\\старт.bmp" ) )
					{
						string путьККартинке = Path.Combine( Path.GetTempPath(), Приложение.ИмяСхемы + "_" + ИМЯ_ФАЙЛА_С_КАРТИНКОЙ );
						if( File.Exists( путьККартинке ) )
						{
							File.SetAttributes( путьККартинке, FileAttributes.Normal );
							File.Delete( путьККартинке );
						}

						File.Copy( компонент.ПапкаРасположения + "\\старт.bmp", путьККартинке, true );

						картинка = new Bitmap( путьККартинке );
						if( картинка != null && картинка.Width == 400 && картинка.Height == 320 )
						{
							окноПриветствия.pictureBox_Картинка.Image = картинка;
						}
					}
				}
			}
			catch( Exception )
			{
				// выставляем картинку по умолчанию
				окноПриветствия.pictureBox_Картинка.Image = global::Барс.Клиент.Properties.Resources.Старт;
			}

			#endregion

			try
			{
				#region Проверка того, что клиент вызван с целью обновления схемы проекта
				// Клиент запущен с целью обновления системы
				if( Приложение.Параметры.ПараметрЗадан( "Обновление" ) )
				{
					try
					{
						Приложение.ИнициироватьОбновлениеСхемыБД();
					}
					catch( Exception exc )
					{
						ФормаПоказаИсключительнойСитуации.Показать( "Не удалось выполнить обновление схемы базы данных.", exc );
						// выходим с аварийным флагом завершения
						Environment.Exit( 1 );
					}
					// Операции выполнены. Осуществляем выход из приложения
					this.Close();
					return;
				}
				#endregion

                //#region Получение пароля к схеме, есть параметр КлючДоступа
                //if (Приложение.Параметры.ПараметрЗадан("КлючДоступа"))
                //{
                //    Барс.Клиент.Приложение.ПарольСхемы=ПолучитьПароль(Приложение.Параметры["КлючДоступа"].ToString());
                    
                //}
                //#endregion

                #region Проверка на то, что клиент вызван с целью компиляции проекта
                if ( Приложение.Параметры.ПараметрЗадан( "РежимРазработчика" ) &&
					Приложение.Параметры.ПараметрЗадан( "КомпиляцияПроекта" ) )
				{
					КомпиляторПроекта компилятор = new КомпиляторПроекта();

					ФормаКомпилятора форма = new ФормаКомпилятора( компилятор );

					компилятор.ВыполнитьКомпиляцию( null );

					this.Close();
					return;
				}
				#endregion

				bool ПолноеОбновлениеКлиента = false;

				Application.DoEvents();

				#region Проверка флага аварийного завершения работы
                // Отключим пока проверку аварийного завершения работы
                if (File.Exists(ИмяФайлаЗавершенияРаботы))
                {
                    File.Delete(ИмяФайлаЗавершенияРаботы);
                }

				if( false && !Приложение.Параметры.ПараметрЗадан( "РежимРазработчика" ) )
				{
					if( File.Exists( ИмяФайлаЗавершенияРаботы ) )
					{
						try
						{
							файлЗавершенияРаботы = new StreamWriter( ИмяФайлаЗавершенияРаботы );
						}
						catch( Exception exc)
						{
							ФормаПоказаИсключительнойСитуации.Показать("Невозможно получить доступ к файлу завершения работы! Возможно данный файл используется другим экземпляром приложения.", exc);
							this.Close();
							Application.Exit();
							return;
						}

						if( XtraMessageBox.Show( "Работа приложения в прошлый раз была завершена некорректно.\nРекомендуется синхронизировать ваше рабочее место с сервером.\nВыполнить синхронизацию?", "Необходимо выполнить синхронизацию", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation ) == DialogResult.Yes )
						{
							ПолноеОбновлениеКлиента = true;
						}
					}
					else
					{
						try
						{
							файлЗавершенияРаботы = new StreamWriter( ИмяФайлаЗавершенияРаботы );
						}
						catch( Exception exc )
						{
							ФормаПоказаИсключительнойСитуации.Показать( "Невозможно получить доступ к файлу завершения работы! Возможно данный файл используется другим экземпляром приложения.", exc );
							Close();
							Application.Exit();
							return;
						}
					}
				}
				#endregion

				Application.DoEvents();

				окноПриветствия.Show();
				окноПриветствия.Текст.Text = "Загрузка прикладной подсистемы...";
				окноПриветствия.Refresh();

				Application.DoEvents();

				Screen screen = Screen.PrimaryScreen;
				// раздвигаем окошко на весь экран
				this.ClientSize = new System.Drawing.Size(screen.WorkingArea.Width - 10, this.ClientSize.Height + 1);

				// выполнение действий по подготовке списка каталогов
				if( !ПодготовитьСписокКаталогов() )
				{
					this.Close();
					return;
				}

				Application.DoEvents();

				bool необходимаКомпиляция = true;

				if( !Приложение.Параметры.ПараметрЗадан( "РежимРазработчика" ) )
				{
                    
					ОбновлениеИсходныхКодов обновлениеИсходныхТекстов = new ОбновлениеИсходныхКодов();
					ФормаПроцессаОбновленияСистемы формаОбновления = new ФормаПроцессаОбновленияСистемы( обновлениеИсходныхТекстов );

					bool требуетсяОбновление = false;

					#region Проверка на необходимость выполнения синхронизации с сервером
					try
					{
						требуетсяОбновление = обновлениеИсходныхТекстов.ПроверитьНаОбновление( ПолноеОбновлениеКлиента );
					}
					catch( ИсключениеСервераБазыДанных exc )
					{
						string текстСообщенияОбОшибке = "При проверке необходимости обновления произошла исключительная ситуация. Приложение загружено не будет.\n\nОписание: ";

						if( exc.ИсключениеСервераБД != null )
						{
							текстСообщенияОбОшибке += Барс.Локализация.ЛокализацияИсключенийСервераБД.ПолучитьОписание( exc.ИсключениеСервераБД.Number );
						}
						else
						{
							текстСообщенияОбОшибке += Барс.Локализация.ЛокализацияИсключенийСервераБД.ПолучитьОписание( exc.Message );
						}

						XtraMessageBox.Show( текстСообщенияОбОшибке, "Не удалось выполнить загрузку приложения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
						окноПриветствия.Close();
						this.Close();
						Application.Exit();
						return;
					}
					catch( Exception exc )
					{
						ФормаПоказаИсключительнойСитуации.Показать( "При проверке необходимости обновления произошла исключительная ситуация. Приложение загружено не будет. Описание ситуации недоступно.", exc );
						окноПриветствия.Close();
						this.Close();
						Application.Exit();
						return;
					}
					#endregion

					Application.DoEvents();

					if( требуетсяОбновление )
					{
                        if (!ПолноеОбновлениеКлиента)
                        {
							if( XtraMessageBox.Show( "На сервере произошло обновление прикладной подсистемы. Без обновления\nданного рабочего места продолжение работы в системе невозможно.\n\nВыполнить обновление?", "Необходимо выполнить обновление", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation ) != DialogResult.Yes )
							{
								окноПриветствия.Close();
								this.Close();
								Application.Exit();
								return;
							}
                        }

						необходимаКомпиляция = true;

						#region Выполнение обновления исходных текстов
						try
						{
							if( обновлениеИсходныхТекстов == null )
							{
								this.Close();
								Application.Exit();
								return;
							}
							формаОбновления.Show();
							обновлениеИсходныхТекстов.ЗагрузитьОбновление();
						}
						catch( ИсключениеСервераБазыДанных exc )
						{
							string текстСообщенияОбОшибке = "При проверке необходимости обновления произошла исключительная ситуация. Приложение загружено не будет.\n\nОписание: ";

							if( exc.ИсключениеСервераБД != null )
							{
								текстСообщенияОбОшибке += Барс.Локализация.ЛокализацияИсключенийСервераБД.ПолучитьОписание( exc.ИсключениеСервераБД.Number );
							}
							else
							{
								текстСообщенияОбОшибке += Барс.Локализация.ЛокализацияИсключенийСервераБД.ПолучитьОписание( exc.Message );
							}

							XtraMessageBox.Show( текстСообщенияОбОшибке, "Не удалось выполнить загрузку приложения", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
							окноПриветствия.Close();
							this.Close();
							Application.Exit();
							return;
						}
						catch( ИсключениеПриНевозможностиОчисткиПапки exc )
						{
							ФормаПоказаИсключительнойСитуации.Показать( 
								"При обновлении произошла исключительная ситуация. Приложение загружено не будет. Не удалось очистить папку '" + exc.Message + "'.", exc );
							окноПриветствия.Close();
							this.Close();
							Application.Exit();
							return;
						}
						catch( ИсключениеПриНевозможностиУдаленияФайла exc )
						{
							ФормаПоказаИсключительнойСитуации.Показать( 
								"При обновлении произошла исключительная ситуация. Приложение загружено не будет. Не удалось удалить файл '" + exc.Message + "'.", exc );
							окноПриветствия.Close();
							this.Close();
							Application.Exit();
							return;
						}
						catch( ОбщееИсключениеПриПопыткеОбновления exc )
						{
							ФормаПоказаИсключительнойСитуации.Показать( 
								"При обновлении произошла исключительная ситуация. Приложение загружено не будет. Описание ситуации: " + exc.Message + ".", exc );
							окноПриветствия.Close();
							this.Close();
							Application.Exit();
							return;
						}
						catch( Exception exc )
						{
							ФормаПоказаИсключительнойСитуации.Показать( 
								"При обновлении произошла исключительная ситуация. Приложение загружено не будет. Описание ситуации недоступно.", exc );
							окноПриветствия.Close();
							this.Close();
							Application.Exit();
							return;
						}
						finally
						{
							формаОбновления.Close();
						}
						#endregion

						Application.DoEvents();
					}
					else
					{
						необходимаКомпиляция = false;
					}
				}
				окноПриветствия.Текст.Text = "Загрузка прикладной подсистемы: проверка компонентов системы...";
				окноПриветствия.Refresh();

				Application.DoEvents();

				if( !необходимаКомпиляция )
				{
					// проверяем, а существуют ли необходимые библиотеки
					foreach( КомпонентСистемы компонент in Приложение.ПолучитьВсеКомпоненты() )
					{
						if( !File.Exists( компонент.Имя + ".dll" ) )
						{
							необходимаКомпиляция = true;
							break;
						}
					}
				}

                // компиляция исходных текстов
                if ( необходимаКомпиляция )
                {
					окноПриветствия.Текст.Text = "Загрузка прикладной подсистемы: построение проекта...";
					окноПриветствия.Refresh();

                    КомпиляторПроекта компилятор = new КомпиляторПроекта();
					ФормаКомпилятора форма = new ФормаКомпилятора( компилятор, !Приложение.Параметры.ПараметрЗадан( "РежимРазработчика" ) );

                    if ( !компилятор.ВыполнитьКомпиляцию( null ) )
                    {
                        this.Close();
						Application.Exit();
						return;
                    }
                }

				Application.DoEvents();

				if( Приложение.Параметры.ПараметрЗадан( "Документация" ) )
				{
					this.Close();
					Application.Exit();
					return;
				}

				Application.DoEvents();

				// Загрузка сервиса отправки сообщений клиентам
				окноПриветствия.Текст.Text = "Загрузка прикладной подсистемы: загрузка сервиса отправки сообщений...";
				окноПриветствия.Refresh();

				Application.DoEvents();

				int портСервисаСообщений = ПОРТ_СЕРВИСА_СООБЩЕНИЙ;
				string имяСервисаСообщений = ИМЯ_СЕРВИСА_СООБЩЕНИЙ;

				if( Приложение.Параметры.ПараметрЗадан( "ПортСервисаСообщений" ) )
				{
					try
					{
						портСервисаСообщений = int.Parse( Приложение.Параметры [ "ПортСервисаСообщений" ] );
					}
					catch
					{
						портСервисаСообщений = ПОРТ_СЕРВИСА_СООБЩЕНИЙ;
					}
				}

				

				if( Приложение.Параметры.ПараметрЗадан( "ИмяСервисаСообщений" ) )
				{
					try
					{
						имяСервисаСообщений = Приложение.Параметры [ "ИмяСервисаСообщений" ];
					}
					catch
					{
						имяСервисаСообщений = ИМЯ_СЕРВИСА_СООБЩЕНИЙ;
					}
				}

				try
				{
					ChannelServices.RegisterChannel(new TcpChannel(портСервисаСообщений), false);
					RemotingConfiguration.RegisterWellKnownServiceType(typeof(Барс.Клиент.СообщениеКлиенту.СерверСообщений),
						имяСервисаСообщений, WellKnownObjectMode.Singleton);

					Барс.Клиент.СообщениеКлиенту.НастройкиСервисаСообщений.АдресСервисаКлиента =
						string.Format("tcp://{0}:{1}/{2}", Dns.GetHostName(), портСервисаСообщений, имяСервисаСообщений);
				}
				catch
				{
					окноПриветствия.Текст.Text = "Загрузка прикладной подсистемы: сервис отправки сообщений не стартовал.";
					окноПриветствия.Refresh();
				}

				Application.DoEvents();

				#region Указываем заголовок окна
				if (Приложение.Параметры.ПараметрЗадан("Метка"))
				{
					НастольноеПриложение.МеткаПриложенияДляЗаголовковОкон = Приложение.Параметры["Метка"];
				}
				if (!string.IsNullOrEmpty(НастольноеПриложение.МеткаПриложенияДляЗаголовковОкон))
				{
					this.Text += "       (" + НастольноеПриложение.МеткаПриложенияДляЗаголовковОкон + ")";
				}
				#endregion

				// загрузка сборок
				System.Reflection.Assembly.Load( "Ядро" );
				Application.DoEvents();
				System.Reflection.Assembly.Load( "Платформа" );
				Application.DoEvents();
				System.Reflection.Assembly.Load( "Приложение" );
				Application.DoEvents();

				// загрузка менеджера пользователей
				Type ТипМенеджераПользователей = Type.GetType( "Барс.Ядро.МенеджерПользователей,Ядро" );

				окноПриветствия.Текст.Text = "Загрузка прикладной подсистемы: соединение с сервером базы данных...";
				окноПриветствия.Refresh();

				Application.DoEvents();


				// инициализация менеджера базы данных
				Приложение.УстановитьСоединениеССервером();

				окноПриветствия.Текст.Text = "Загрузка прикладной подсистемы: авторизация...";
				окноПриветствия.Refresh();

				Application.DoEvents();
				if( !НастольноеПриложение.ПодключитьсяКЯдру( false ) )
				{
					this.Close();
					Application.Exit();
					return;
				}

				Application.DoEvents();

				// попытка загрузить главное меню
				МенюГлавногоОкна менюГлавногоОкна = new МенюГлавногоОкна(ПанельМеню);

				КомпонентСистемы настройкаКомпонента = Приложение.ПолучитьКомпонент( "Приложение" );

				окноПриветствия.Текст.Text = "Загрузка прикладной подсистемы: загрузка главного меню...";
				окноПриветствия.Refresh();
				if (настройкаКомпонента == null || !менюГлавногоОкна.ЗагрузитьXML(настройкаКомпонента.ПапкаРасположения + @"\ГлавноеМеню.xml"))
				{
					this.Close();
					Application.Exit();
					return;
				}

				Application.DoEvents();

				СписокЭлементовМеню = менюГлавногоОкна.СписокЭлементовМеню;
				СловарьНазванийЭлементов = менюГлавногоОкна.СловарьНазванийЭлементов;
				СписокЭлементовВыпадающегоМеню = менюГлавногоОкна.СписокЭлементовВыпадающегоМеню;
				ImageList_ДеревоМеню = менюГлавногоОкна.ImageList_ДеревоМеню;

				ЗагрузитьНастройкиГлавногоМеню();

				Application.DoEvents();

				foreach (ЭлементВыпадающегоМеню пункт in СписокЭлементовВыпадающегоМеню)
				{
					НазначитьшрифтДляВыпадающегоСписка(пункт.Элемент);
					выпадающееМеню.ItemLinks.Add(пункт.Элемент, пункт.НоваяГруппа);
				}
				
				ПанельМеню.Manager.AllowCustomization = false;

				#region Указываем заголовок окна
				if (Приложение.Параметры.ПараметрЗадан("Метка"))
				{
					НастольноеПриложение.МеткаПриложенияДляЗаголовковОкон = Приложение.Параметры["Метка"];
				}
				if (!string.IsNullOrEmpty(НастольноеПриложение.МеткаПриложенияДляЗаголовковОкон))
				{
					this.Text += "       (" + НастольноеПриложение.МеткаПриложенияДляЗаголовковОкон + ")";
				}
				#endregion

				this.Show();
				this.Refresh();
				окноПриветствия.Close();

				Application.DoEvents();

				//Сообщить("БАРС начал работу");

				buttonEdit_ТекущийПользователь.Text = ПолучитьТекстовоеПредставлениеТекущегоПользователя();
				lookUpEdit_ТекущееОкно.Properties.DataSource = НастольноеПриложение.СписокОкон;
                Счетчик.Start();
			}
			catch ( Exception исключение )
			{
				ФормаПоказаИсключительнойСитуации.Показать( "Не удалось выполнить загрузку прикладной подсистемы.", "Невозможно запустить прикладную подсистему.", исключение );
				//ПоказатьОкноИсключения(исключение);
				this.Close();
				Application.Exit();
				return;
			}
			finally
			{
				if (окноПриветствия.Visible)
				{
					окноПриветствия.Close();
				}
			}

			#region генерим событие перехода в цикл ожидания
			try
			{
				Type типСобытий = Type.GetType( "Барс.Ядро.СобытияИнициализацииПриложения,Ядро" );
				if( типСобытий != null )
				{
					типСобытий.InvokeMember( "ВызватьСобытиеПередНачаломЦиклаОжидания", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.InvokeMethod | System.Reflection.BindingFlags.Static, null, null, new object [] { } );
				}
			}
			catch( Exception )
			{
			}
			#endregion
		}
		
		private void НазначитьшрифтДляВыпадающегоСписка(BarItem item)
		{
			if (item is BarSubItem)
			{
				BarSubItem подменю = item as BarSubItem;
				подменю.OwnFont = ПанельМеню.Font;
				подменю.UseOwnFont = true;
				подменю.ShowMenuCaption = true;
				подменю.MenuAppearance.Menu.Font = ПанельМеню.Font;
				подменю.MenuAppearance.Menu.Options.UseFont = true;
				подменю.MenuAppearance.MenuBar.Font = ПанельМеню.Font;
				подменю.MenuAppearance.MenuBar.Options.UseFont = true;
				подменю.MenuAppearance.MenuCaption.Font = ПанельМеню.Font;
				подменю.MenuAppearance.MenuCaption.Options.UseFont = true;
				подменю.MenuAppearance.SideStrip.Font = ПанельМеню.Font;
				подменю.MenuAppearance.SideStrip.Options.UseFont = true;
				подменю.MenuAppearance.SideStripNonRecent.Font = ПанельМеню.Font;
				подменю.MenuAppearance.SideStripNonRecent.Options.UseFont = true;
				

				foreach (LinkPersistInfo linkInfo in подменю.LinksPersistInfo)
				{
					НазначитьшрифтДляВыпадающегоСписка(linkInfo.Item);
				}

			}
		}

		#region Служебные функции обеспечания многопоточности и MDI интерфейса

		public void ДобавитьОкноТреда(Form дочернееОкно)
		{
			lock (СписокДочернихОкон)
			{
				СписокДочернихОкон.Add(дочернееОкно);
			}
		}

		public void ИсключитьОкноТреда(Form ЗакрываемоеОкно)
		{
			lock (СписокДочернихОкон)
			{
				int i;
				for (i = СписокДочернихОкон.Count - 1; i >= 0; i--)
				{
					if (СписокДочернихОкон[i].Equals(ЗакрываемоеОкно))
					{
						СписокДочернихОкон.RemoveAt(i);
					}
				}
			}
		}

		#endregion

		private void ГлавноеОкно_FormClosing(object sender, FormClosingEventArgs e)
		{
            
			if ( НастольноеПриложение.СписокОкон.Count > 0)
			{
				XtraMessageBox.Show(this.LookAndFeel, "Некоторые процессы еще работают. Необходимо их закрыть перед выходом!\n\nСписок открытых окон можно посмотреть в главном окне приложения.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				//foreach (СписокОконТреда списокОкон in Приложение.СписокОкон)
				//{
				//	списокОкон.ТекущееОкно.Activate();
				//}
				e.Cancel = true;
                return;
			}

			if (НастройкиПанелиИзменились || ИндексРаздела != ПанельМеню.Pages.IndexOf(ПанельМеню.SelectedPage))
			{
				СохранитьНастройкиГлавногоМеню();
			}

            if (файлЗавершенияРаботы != null)
            {
                switch (e.CloseReason)
                {
                    //case CloseReason.ApplicationExitCall: файлЗавершенияРаботы.Write("Приложение закрыто функцией Exit"); файлЗавершенияРаботы.Close(); break;
                    //case CloseReason.None: файлЗавершенияРаботы.Write("Неизвестная причина закрытия"); файлЗавершенияРаботы.Close(); break;
                    case CloseReason.TaskManagerClosing: файлЗавершенияРаботы.Write("Приложение было закрыто из Диспетчера задач ОС Windows"); файлЗавершенияРаботы.Close(); break;
                    default:
                        {
                            файлЗавершенияРаботы.Close();
                            File.Delete(ИмяФайлаЗавершенияРаботы);
                            break;
                        }
                }
            }
		}

		/// <summary>
		/// Выполнение действий по созданию каталогов и прочее
		/// </summary>
		/// <returns></returns>
		private bool ПодготовитьСписокКаталогов()
		{
			try
			{
				// Создаем серверную папку
				Directory.CreateDirectory("Сервер");
				Directory.CreateDirectory(@"Сервер\ИсходныеТексты");
				Directory.CreateDirectory(@"Сервер\ИсходныеТексты\Ядро");
				Directory.CreateDirectory(@"Сервер\ИсходныеТексты\Платформа");
				Directory.CreateDirectory(@"Сервер\ИсходныеТексты\Приложение");
			}
			catch (Exception исключение)
			{
				ГлавноеОкно.ПоказатьОкноИсключения(исключение);
				return false;
			}
			return true;
		}

		static public void ПоказатьОкноИсключения(Exception исключение)
		{
			ОкноИсключения окно = new ОкноИсключения(исключение);
			окно.ShowDialog();
		}

        private void buttonEdit_ТекущийПользователь_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
		{
			
			e.DisplayText = ПолучитьТекстовоеПредставлениеТекущегоПользователя();
		}

		private string ПолучитьТекстовоеПредставлениеТекущегоПользователя()
		{
			string текст = "";
			if( !string.IsNullOrEmpty( Приложение.ТекущийПользователь.Логин ) )
			{
				текст = Приложение.ТекущийПользователь.Логин;
				if( !string.IsNullOrEmpty( Приложение.ТекущийПользователь.Наименование ) &&
					Приложение.ТекущийПользователь.Наименование.Trim().ToLower() != Приложение.ТекущийПользователь.Логин.Trim().ToLower() )
				{
					текст += " - " + Приложение.ТекущийПользователь.Наименование;
				}
				текст += " [" + Приложение.ТекущийПользователь.НаименованиеРоли + "]";
			}
			else
			{
				текст = "[Нет текущего пользователя..]";
			}
			return текст;
		}

		private void buttonEdit_ТекущийПользователь_ButtonClick( object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e )
		{
			СменитьПользователяСистемы();
		}

		#region Операция смены пользователя системы
		private void СменитьПользователяСистемы()
		{
			if( this.СписокДочернихОкон.Count > 0 )
			{
				XtraMessageBox.Show( "Для смены текущего пользователя необходимо закрыть все открытые окна (кроме главного).", "Смена пользователя системы", MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( XtraMessageBox.Show( "Действительно хотите сменить пользователя?.", "Смена пользователя системы", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2 ) != DialogResult.Yes )
			{
				return;
			}
			if( !НастольноеПриложение.ПодключитьсяКЯдру( true ) )
			{
				return;
			}
			buttonEdit_ТекущийПользователь.Text = ПолучитьТекстовоеПредставлениеТекущегоПользователя();
		}
		#endregion

		private void ГлавноеОкно_Activated( object sender, EventArgs e )
		{

		}

        private void lookUpEdit_ТекущееОкно_CustomDisplayText(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
		{
			if( НастольноеПриложение.ТекущийСписокОкон != null )
			{
				e.DisplayText = НастольноеПриложение.ТекущийСписокОкон.ТекстовоеПредставление;
			}
		}

		private void lookUpEdit_ТекущееОкно_GetNotInListValue( object sender, DevExpress.XtraEditors.Controls.GetNotInListValueEventArgs e )
		{
			e.Value = "";
			if( НастольноеПриложение.ТекущийСписокОкон != null )
			{
				e.Value = НастольноеПриложение.ТекущийСписокОкон.ТекстовоеПредставление;
			}
		}

		#region Методы обеспечения выставления стиля для окна
		/// <summary>
		/// Метод установки стиля приложения (используется при вызове из ядра для установки настроек)
		/// </summary>
		/// <param name="ИмяСтиля"></param>
		/// <param name="ИмяСкина"></param>
		/// <param name="ИспользоватьWinXPСтиль"></param>
		public void УстановитьСтильПриложения( string ИмяСтиля, string ИмяСкина, bool ИспользоватьWinXPСтиль, Font Шрифт )
		{
			lock( defaultLookAndFeel )
			{
				defaultLookAndFeel.LookAndFeel.Reset();
				switch( ИмяСтиля )
				{
					case "Skin":
						{
							this.defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
						}
						break;

					case "Flat":
						{
							this.defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
						}
						break;

					case "Office2003":
						{
							this.defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
						}
						break;

					case "Style3D":
						{
							this.defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
						}
						break;

					case "UltraFlat":
						{
							this.defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.UltraFlat;
						}
						break;
				}
				defaultLookAndFeel.LookAndFeel.SkinName = ИмяСкина;
				defaultLookAndFeel.LookAndFeel.UseWindowsXPTheme = ИспользоватьWinXPСтиль;
				DevExpress.Skins.SkinManager.EnableFormSkins();
				DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();

				
				ПанельМеню.Font = Шрифт;
				выпадающееМеню.MenuAppearance.Menu.Font = Шрифт;
				выпадающееМеню.MenuAppearance.MenuBar.Font  = Шрифт;
				выпадающееМеню.MenuAppearance.MenuCaption.Font = Шрифт;
				выпадающееМеню.MenuAppearance.SideStrip.Font = Шрифт;
				выпадающееМеню.MenuAppearance.SideStripNonRecent.Font = Шрифт;
				barAndDockingController1.AppearancesBar.ItemsFont = Шрифт;
				barAndDockingController1.AppearancesBar.Bar.Font = Шрифт;
				barAndDockingController1.AppearancesBar.Dock.Font = Шрифт;
				barAndDockingController1.AppearancesBar.MainMenu.Font = Шрифт;
				barAndDockingController1.AppearancesDocking.FloatFormCaption.Font = Шрифт;
				barAndDockingController1.AppearancesDocking.FloatFormCaptionActive.Font = Шрифт;
				barAndDockingController1.AppearancesDocking.Panel.Font = Шрифт;
				barAndDockingController1.AppearancesDocking.PanelCaption.Font = Шрифт;
				barAndDockingController1.AppearancesDocking.PanelCaptionActive.Font = Шрифт;
				barAndDockingController1.AppearancesRibbon.FormCaption.Font = Шрифт;
				barAndDockingController1.AppearancesRibbon.Item.Font = Шрифт;
				barAndDockingController1.AppearancesRibbon.PageGroupCaption.Font = Шрифт;
				barAndDockingController1.AppearancesRibbon.PageHeader.Font = Шрифт;
				this.Font = Шрифт;
				layoutControlGroup1.AppearanceGroup.Font = Шрифт;
				layoutControlGroup1.AppearanceItemCaption.Font = Шрифт;
				layoutControlGroup1.AppearanceTabPage.Header.Font = Шрифт;
				layoutControlGroup1.AppearanceTabPage.HeaderActive.Font = Шрифт;
				layoutControlGroup1.AppearanceTabPage.PageClient.Font = Шрифт;
				layoutControlGroup1.AppearanceTabPage.HeaderDisabled.Font = Шрифт;
				layoutControlItem1.AppearanceItemCaption.Font = Шрифт;
				layoutControlItem2.AppearanceItemCaption.Font = Шрифт;
				lookUpEdit_ТекущееОкно.Font = Шрифт;
				buttonEdit_ТекущийПользователь.Font = Шрифт;
				

				//this.LookAndFeel.Style = defaultLookAndFeel.LookAndFeel.Style;
				//this.LookAndFeel.SkinName = defaultLookAndFeel.LookAndFeel.SkinName;
				//this.LookAndFeel.UseWindowsXPTheme = defaultLookAndFeel.LookAndFeel.UseWindowsXPTheme;
				//DevExpress.LookAndFeel.LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
				this.Refresh();
			}
		}

		void LookAndFeel_StyleChanged( object sender, EventArgs e )
		{
		}
		#endregion

		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			ФормаНастройкиПанелиБыстрогоЗапуска форма = new ФормаНастройкиПанелиБыстрогоЗапуска();

			форма.СписокЭлементовМеню = СписокЭлементовМеню;
			форма.ImageList_ДеревоМеню = ImageList_ДеревоМеню;

			форма.Font = this.Font;
			
			if (форма.ShowDialog() == DialogResult.OK)
			{

				СписокСохраняемыхЭлементов = new List<ЭлементыМеню>();

				НастройкиПанелиИзменились = true;

				this.ПанельМеню.Toolbar.ItemLinks.Clear();

				foreach (ЭлементыМеню текущийЭлемент in СписокЭлементовМеню)
				{
					if (текущийЭлемент.Переключатель && текущийЭлемент.Элемент != null)
					{
						if (string.IsNullOrEmpty(текущийЭлемент.Элемент.Hint))
						{
							текущийЭлемент.Элемент.Hint = текущийЭлемент.Элемент.Caption;
						}

						СписокСохраняемыхЭлементов.Add(текущийЭлемент);

						this.ПанельМеню.Toolbar.ItemLinks.Add(текущийЭлемент.Элемент, true);
					}
				}
			}
		}

		private void СохранитьНастройкиГлавногоМеню()
		{
			try
			{

				ToolBarXml = new XmlDocument();

				XmlElement главныйЭлемент = ToolBarXml.CreateElement("НастройкиГлавногоМеню");

				ToolBarXml.AppendChild(главныйЭлемент);

				//Сосздаем элемент хранящий настройки состояния RibbonGroup
				XmlElement состояние = ToolBarXml.CreateElement("Состояние");

				//элемент определяющий свернуто или развернуто меню
				XmlElement узел = ToolBarXml.CreateElement("МенюСвернуто");

				узел.InnerText = ПанельМеню.Minimized.ToString();

				состояние.AppendChild(узел);

				//элемент определяющий над или под меню находится панель быстрого запуска
				узел = ToolBarXml.CreateElement("ПанельВВерху");

				if (ПанельМеню.ToolbarLocation == RibbonQuickAccessToolbarLocation.Above)
				{
					узел.InnerText = "True";
				}
				else
				{
					узел.InnerText = "False";
				}

				состояние.AppendChild(узел);

				узел = ToolBarXml.CreateElement("ВыбранныйРаздел");

				узел.InnerText = ПанельМеню.Pages.IndexOf(ПанельМеню.SelectedPage).ToString();

				состояние.AppendChild(узел);

				главныйЭлемент.AppendChild(состояние);

				//Сосздаем элемент хранящий информацию об элементах панели быстрого запуска
				XmlElement element = ToolBarXml.CreateElement("ЭлементыГлавногоМеню");

				foreach (ЭлементыМеню текущийЭлемент in СписокСохраняемыхЭлементов)
				{
					XmlElement дочерний = ToolBarXml.CreateElement("ЭлементМеню");
					дочерний.SetAttribute("NS", текущийЭлемент.Путь);
					дочерний.InnerText = текущийЭлемент.Название;
					element.AppendChild(дочерний);
				}

				главныйЭлемент.AppendChild(element);

				object[] args = new object[2];
				args[0] = Type.GetType("Барс.ГлобальныеНастройкиПользователя,Ядро");
				args[1] = "НастройкиГлавногоМеню";

				object ОбъектПутиФайла = Activator.CreateInstance(Type.GetType("Барс.ПутьФайлаНастроек,Ядро"), args);
				string путь = ОбъектПутиФайла.GetType().InvokeMember("ПолучитьПутьКФайлуНастроек", System.Reflection.BindingFlags.InvokeMethod, null, ОбъектПутиФайла, new object[] { (int)Метод.Запись }) as string;
				путь += "\\НастройкиГлавногоМеню.xml";

				ToolBarXml.Save(@путь);
			}
			catch(Exception исключение)
			{
	
			}
		}

		private void ЗагрузитьНастройкиГлавногоМеню()
		{

			try
			{
				СписокСохраняемыхЭлементов = new List<ЭлементыМеню>();
				object[] args = new object[2];
				args[0] = Type.GetType("Барс.ГлобальныеНастройкиПользователя,Ядро");
				args[1] = "НастройкиГлавногоМеню";

				Type тип = Type.GetType("Барс.ПутьФайлаНастроек, Ядро");
				object ОбъектПутиФайла = Activator.CreateInstance(тип, args);
				string путь = ОбъектПутиФайла.GetType().InvokeMember("ПолучитьПутьКФайлуНастроек", System.Reflection.BindingFlags.InvokeMethod, null, ОбъектПутиФайла, new object[] { (int)Метод.Чтение }) as string;
				if (string.IsNullOrEmpty(путь))
				{
					return;
				}

				ToolBarXml = new XmlDocument();

				ToolBarXml.Load(@путь);

				ПанельМеню.Toolbar.ItemLinks.Clear();

				XmlNode родитель = ToolBarXml.FirstChild;

				//Распарсиваем xml с настройками главного меню
				foreach (XmlNode узел in родитель.ChildNodes)
				{
					if (узел.Name == "Состояние")
					{
						foreach (XmlNode текущаяНастройка in узел.ChildNodes)
						{
							switch (текущаяНастройка.Name)
							{
								case "МенюСвернуто":
									{
										bool temp = false;

										if (bool.TryParse(текущаяНастройка.InnerText, out temp))
										{
											ПанельМеню.Minimized = temp;
										}
									}
									break;

								case "ПанельВВерху":
									{
										bool temp = false;

										if (bool.TryParse(текущаяНастройка.InnerText, out temp))
										{
											ПанельМеню.ToolbarLocation = (temp ? RibbonQuickAccessToolbarLocation.Above : RibbonQuickAccessToolbarLocation.Below);
										}
									}
									break;

								case "ВыбранныйРаздел":
									{
										int index = 0;

										if (int.TryParse(текущаяНастройка.InnerText, out index))
										{
											if (ПанельМеню.Pages.Count >= index + 1)
											{
												ИндексРаздела = index;
												ПанельМеню.SelectedPage = ПанельМеню.Pages[index];
											}
										}
									}
									break;
							}
						}
					}
					else if (узел.Name == "ЭлементыГлавногоМеню")
					{
						foreach (XmlNode текущийЭлемент in узел.ChildNodes)
						{
							string Путь = (текущийЭлемент.Attributes["NS"] as XmlAttribute).Value;

							if (СловарьНазванийЭлементов.ContainsKey(текущийЭлемент.InnerText.Trim()))
							{
								if (СловарьНазванийЭлементов[текущийЭлемент.InnerText.Trim()].ContainsKey(Путь))
								{
									ЭлементыМеню элемент = СловарьНазванийЭлементов[текущийЭлемент.InnerText.Trim()][Путь] as ЭлементыМеню;
									элемент.Переключатель = true;
									СписокСохраняемыхЭлементов.Add(элемент);

									if (string.IsNullOrEmpty(элемент.Элемент.Hint))
									{
										элемент.Элемент.Hint = элемент.Элемент.Caption;
									}

									this.ПанельМеню.Toolbar.ItemLinks.Add(элемент.Элемент, true);
								}
							}
						}
					}
				}
			}
			catch (Exception исключение)
			{
			}
		}

		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			НастройкиПанелиИзменились = true;

			if (ПанельМеню.ToolbarLocation == RibbonQuickAccessToolbarLocation.Above)
			{
				ПанельМеню.ToolbarLocation = RibbonQuickAccessToolbarLocation.Below;
			}
			else
			{
				ПанельМеню.ToolbarLocation = RibbonQuickAccessToolbarLocation.Above;
			}
		}

		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			НастройкиПанелиИзменились = true;

			if (ПанельМеню.Minimized)
			{
				ПанельМеню.Minimized = false;
			}
			else
			{
				ПанельМеню.Minimized = true;
			}
		}

		private void popupMenu1_BeforePopup(object sender, CancelEventArgs e)
		{

			if (!ПанельМеню.Minimized)
			{
				popupMenu1.ItemLinks[2].Caption = "Свернуть главное меню";
			}
			else
			{
				popupMenu1.ItemLinks[2].Caption = "Развернуть главное меню";
			}

			if(ПанельМеню.ToolbarLocation == RibbonQuickAccessToolbarLocation.Above)
			{
				popupMenu1.ItemLinks[1].Caption = "Переместить панель быстрого запуска ниже главного меню";
				barButtonItem2.Glyph = System.Drawing.Image.FromStream(new System.IO.MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAwtJREFUOE9tknsslXEYx39vLhVyi5SSWoosplpxmsmlXCprxXEbFQ65ReQ2lyOzpRutWDbTxWmRmUUuczkcRzhsOm25pFX+0G1DI/dbvv3eE2dZnu3zvs/7vN/n+/x+v/dlyBqhGUAM1ykQB0VlcowwhFmaI22/Z4lw9BkZXEsvr231J7p6UUTgUmA9k/Gaj4fSB8h5cw/Jojg45B+d0Y9mBNu8ic6aJuyLPXyND7lv70M4WI/iPgEKuwvwpDsfhT0FeP6+EEmvY2GYqtav50u2rDYJJkoGCRsk2V23UNRXiEzJddyQpOHbxFcMjH2iOR+3OtORI72L0AZ/7EhQlpDjRFFuohdEvEJqLuBRdx4SxVfBp5PS2xMwMT+OkZlh8FvjcL0tHmn0ntGRAsdiDvQvEw+5gVGSWkW29CbimsMRLryIyKYAJLZcwdjsKDUYQrw4FNEiHqKaAmkejoB6LxgmKpWvGDCc7L0Dye3X6PL80PGjFS/6n4JX746fdPrI9BB4DW6o+FyCd8NSRDfzwBN6Yf9NnY9aXKJBCJco2OSafgsUeiJU6IuVeNybg+9TX2QI+vL+lpeAiCY/nKu2g/kdvUH5YVpl7e49W3Uc1iXGiBWHYGp+WqZfXFqUwcb47Dhi6HS7MlM4vDSHWaZODx2+UbYNkzR1gVetE+zLDsC6dA+S2iIwv7ggX80CzZPbwmFfboLTrw7DtdoKxqmqT+WHqOVEOK6lluDW2uJM5WE4V5ohRRKGyblJzC7MIaMzGq41FnCrtYKP0B4upRbQdiZWq/6F7e7riy6KHeHXeBIedRycrzuEhHZ/pHQEw0N4BL6NNghqOYXAVmcYeKoU/9vM0Ad6msRsJ1e1OVDkjHipL6I63BDS7oIwiQviujyR2u2PsFZX7OJuElGtOUWTwvbKLroUSwpXzYJU2d42GY8UuYEvvSQjRszFiSzTX+oHmSpWs6xle9atrESJJtqUfRR2bz7MZpKuYsTksrA5rXlTOMsaVsv2/BesozKF/TxqFPVl2Jytse/kU9nuPwvOqzuRbUmMAAAAAElFTkSuQmCC")));
			}
			else
			{
				popupMenu1.ItemLinks[1].Caption = "Переместить панель быстрого запуска выше главного меню";
				barButtonItem2.Glyph = System.Drawing.Image.FromStream(new System.IO.MemoryStream(Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAxVJREFUOE9tU3swlFEcvShF2kq1dlWT/mjKoxRTnjurpaYXSl7Z2JayQtrYktGDylCTGT2m6THFH02mSZk8l0VtiZiKRVFbGaUMFqXCP/Z0v8WOhjtzvnvv+Z1z7u/77nwGZJphtpcsmmVC3A2MyRqmDC1pNv5NqrtyiWY6vZ5bEEDmsWPJNf61dYOJCiky6zJwsTYNUnkU3K7YDnJozVxIWNOGcOmpK06ymi/Vp0PxVY68D7m403QDt1XXce99Du615kD29DCskk2bFosIZ0qIpcyw8kL9eTz6+ADptSnIeJWKll4VGrvf4ELdWd3+ekMWIsvCQLVV/wWwJUQYWbwPOS23cEIZh5SXifj8U42JoR5oQ2pNIk5Vy5BWdwqb77uAG0VC9CErT7JKrjZmQvYsGnGVEWjubdCbJxbvNCrIlBIcV8bgYIUQVkmzi8YC+GSG+2XrjtO1xyApF6Kx57XO097/BYMjv9A/3A913ycd19rXDOnTCEgqhbC7yG7niMlisjCczBXcXK85UBGEI1URgJaa+9oRowhD19/v6BnqQnRFKNQaNUa1o0hQHoR/iRfss7gaTjhZTogjmcm7uvqbTwEPnnnrEK+QQFzqh2C5AN1DP9A/okFIuRfC5D44REO35K/Hpvw1sM/kdFjsI2zdW6w9t7AooNQLHnm2EDy2wdYCe/gWb6QddEIz3IPdJU7wLnLANspvf+IA72In2KTOK9R/RO5+EhJQvImaXLCz0FFn2CN3RuefDgzQDvYqeAgsc4VfqROdedhV6AxLEQmefJVGq+Lm14hfbEdwGR/+chcEljvjbssV3G+7BWGVG0Iq3CGq8kJk7U5YHzWvoWajyQGmdONsJ2WrY+v9EF8fjEPVOyBS8iF+7oG4V75IaghFQkMQ1sZbfCQziCvVMx4DJoR5WFBsIIYkYpmfqSow102b/FaEtNYoHc6owiF8yNcu95+rohqxTjvmMZzownicsKOzgBgTKcvWIHuJwKxwKQXLziib4WjNk8J2XMt4pgwmkSmYUMyhYP48Bsya4Zia/lTG/Q85Oq2tyNWrpQAAAABJRU5ErkJggg==")));
			}
		}

		enum Метод
		{
			Чтение = 0,
			Запись = 1
		}

        private void Счетчик_Tick(object sender, EventArgs e)
        {
            try
            {
                Счетчик.Stop();
                List<string[]> списокСообщений = Type.GetType("Барс.Ядро.МенеджерСообщенийАдминистратора, Ядро").
                    InvokeMember("ПолучитьСообщения", System.Reflection.BindingFlags.Static |
                    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.InvokeMethod,
                    null, null, null) as List<string[]>;

                if ((списокСообщений != null) && (списокСообщений.Count > 0))
                {
                    СообщениеКлиенту.ОкноСообщенияЧерезБД окноСообщения = new СообщениеКлиенту.ОкноСообщенияЧерезБД();
                    
                    List<string> сообщения = new List<string>();

                    foreach(string[] текст in списокСообщений)
                    {
                        // сообщения.Add(string.Format("{0} : Сообщение ({1} - {2}) :", текст[1], текст[2], текст[3]));
                        // сообщения.Add(текст[0]);
                        // сообщения.Add(" ");

						сообщения.Add( "Текст сообщения: " + текст[ 0 ] );
						сообщения.Add( "" );
						сообщения.Add( string.Format( "Время размещения сообщения: {0}", текст[ 1 ] ) );
						сообщения.Add( string.Format( "Текущее время на сервере: {0}", текст [ 2 ] ) );

						сообщения.Add( "----------------------" );
						
                    }

                    окноСообщения.МассивСтрок = сообщения.ToArray();
                    окноСообщения.ShowDialog();
                }
            }
            catch
            {
            }
            finally
            {
                Счетчик.Start();
            }
        }

        private void КонтейнерРазмещенияЭлементов_ShowCustomization(object sender, EventArgs e)
        {
            КонтейнерРазмещенияЭлементов.CustomizationForm.Location = new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
        }

	}
}
