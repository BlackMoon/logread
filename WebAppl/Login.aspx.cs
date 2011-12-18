using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Барс;
using Барс.Ядро;
using Барс.Клиент;
using Барс.ВебЯдро;
using Барс.ВебЯдро.Интерфейс;

public partial class _Login : System.Web.UI.Page 
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        PageOptimizer.CombineCss(this);

        //Установка стиля
        string ImagesDir = string.Format("~/Resources_Design/{0}/Images/", ApplicationManager.GetProjectName());

        Button_Enter.ImageUrl = ImagesDir + "Button_enter.png";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

	protected void Button_Enter_Click( object sender, EventArgs e )
	{
        try
        {
            System.Reflection.Assembly.Load("БарсКлиент");

            // загружаем параметры приложения
            Барс.Клиент.Приложение.ЗагрузитьПараметрыПриложения(null);

            // пробуем инициализировать подключение к базе данных
            Барс.Клиент.Приложение.УстановитьСоединениеССервером();

            Барс.Ядро.МенеджерПользователей.АвторизацияWeb(tb_User_text.Value.Trim(), tb_Pass_text.Value.Trim());

            #region получаем текущее учреждение
            Выборка<ОператорУчреждения> операторы = new Выборка<ОператорУчреждения>();
            операторы.Запрос.ДобавитьУсловиеОтбора("СистемныйПользователь", МенеджерПользователей.ТекущийПользователь);
            операторы.Загрузить();
            if (операторы.КоличествоЗаписей > 0)
            {
                ПеременныеСессии.ТекущееУчреждение = операторы[0].РабочееУчреждение;
            }
            else
            {
                ПеременныеСессии.ТекущееУчреждение = null;
            }
            #endregion

            try
            {
                Application.Lock();

                //Барс.ВебЯдро.РаботающиеПользователи.СписокРаботающихПользователей списокРаботающихПользователей = Application [ "РаботающиеПользователи" ] as Барс.ВебЯдро.РаботающиеПользователи.СписокРаботающихПользователей;
                Барс.ВебЯдро.РаботающиеПользователи.СписокРаботающихПользователей списокРаботающихПользователей = ApplicationManager.ПолучитьApplicationState("РаботающиеПользователи") as Барс.ВебЯдро.РаботающиеПользователи.СписокРаботающихПользователей;

                Барс.ВебЯдро.РаботающиеПользователи.ОписаниеРаботающегоПользователя описаниеНовогоПользователя = new Барс.ВебЯдро.РаботающиеПользователи.ОписаниеРаботающегоПользователя();
                описаниеНовогоПользователя.ИмяПользователя = Барс.Ядро.МенеджерПользователей.НаименованиеТекущегоПользователя + " (" + Барс.Ядро.МенеджерПользователей.ЛогинТекущегоПользователя + ")";
                описаниеНовогоПользователя.РолиПользователя = "";
                foreach (Барс.Ядро.Роль роль in Барс.Ядро.МенеджерПользователей.РолиТекущегоПользователя)
                {
                    описаниеНовогоПользователя.РолиПользователя += роль.Наименование + ", ";
                }
                if (!string.IsNullOrEmpty(описаниеНовогоПользователя.РолиПользователя))
                {
                    описаниеНовогоПользователя.РолиПользователя = описаниеНовогоПользователя.РолиПользователя.Substring(0, описаниеНовогоПользователя.РолиПользователя.Length - 2);
                }

                списокРаботающихПользователей.Добавить(HttpContext.Current.Session.SessionID, описаниеНовогоПользователя);

                if (описаниеНовогоПользователя != null)
                {
                    if (ApplicationManager.ПолучитьApplicationState("СистемноеЛогирование_ВходВыход") != null
                        && ApplicationManager.ПолучитьApplicationState("СистемноеЛогирование_ВходВыход") is Барс.СистемноеЛогирование)
                    {
                        Барс.СистемноеЛогирование логированиеВходовВыходов = ApplicationManager.ПолучитьApplicationState("СистемноеЛогирование_ВходВыход") as Барс.СистемноеЛогирование;
                        логированиеВходовВыходов.ДобавитьСообщение(string.Format("Сессия началась (имя пользователя: {0}, хост: {1}, роли: {2})", описаниеНовогоПользователя.ИмяПользователя, описаниеНовогоПользователя.Хост, описаниеНовогоПользователя.РолиПользователя));
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Application.UnLock();
            }

            Response.Redirect("~/Default.aspx", true);

        }
        catch (TypeInitializationException exc)
        {
            ResultCaption.Text = "Не удалось загрузить необходимую библиотеку.";

            Exception исключение = Барс.ИсключениеБарс.ПреобразоватьИсключение(exc.InnerException);

            ResultText.Text = исключение.Message;
        }
        catch (Exception exc)
        {
            if (exc.InnerException != null)
            {
                if (exc.InnerException is System.IO.FileNotFoundException)
                {
                    ResultCaption.Text = "Не удалось загрузить необходимую библиотеку.";

                    Exception внутреннееИсключение = Барс.ИсключениеБарс.ПреобразоватьИсключение(exc.InnerException);

                    ResultText.Text = внутреннееИсключение.Message;

                    return;
                }
            }
            
            ResultCaption.Text = "Не удалось выполнить соединение с сервером базы данных.";

            Exception исключение = Барс.ИсключениеБарс.ПреобразоватьИсключение(exc);

            ResultText.Text = исключение.Message;
        }
	}
}
