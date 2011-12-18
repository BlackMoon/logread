<%@ Application Language="C#" %>

<script runat="server">
    
    void Application_Start(object sender, EventArgs e)
    {
        System.Collections.Generic.List<string> параметры = new System.Collections.Generic.List<string>();        
        
        #if DEBUG
        параметры.Add( "/РежимРазработчика" );
        #endif

        Барс.Клиент.Приложение.ЗагрузитьПараметрыПриложения(параметры);
        
        //ОбновлениеWebКлиента.ОбновитьКлиент();
        
        // Code that runs on application startup
        Ajax.Utility.HandlerPath = "BarsWrapper";
        
        Барс.ВебЯдро.РаботающиеПользователи.СписокРаботающихПользователей списокРаботающихПользователей = new Барс.ВебЯдро.РаботающиеПользователи.СписокРаботающихПользователей();
        
        ApplicationManager.УстановитьApplicationState("РаботающиеПользователи", списокРаботающихПользователей);
            
		Барс.Клиент.СистемныйЖурналСобытий.ЗаписатьСообщение( "Приложение начало работу.", Барс.Клиент.ТипСообщенияЖурналаСобытий.Информация );

        // логирование
        Барс.СистемноеЛогирование логированиеВходовВыходов = new Барс.СистемноеЛогирование("Лог входов-выходов", "../Логи");
        ApplicationManager.УстановитьApplicationState("СистемноеЛогирование_ВходВыход", логированиеВходовВыходов);
        логированиеВходовВыходов.НачатьСеансЛогирования();

        Барс.СистемноеЛогирование логированиеРаботыСФормой  = new Барс.СистемноеЛогирование("Лог работы с формой", "../ЛогиРаботыСФормой");
        ApplicationManager.УстановитьApplicationState("СистемноеЛогирование_РаботаСФормой", логированиеРаботыСФормой);
        логированиеРаботыСФормой.НачатьСеансЛогирования();
        
        Барс.СистемноеЛогирование логированиеУдалений = new Барс.СистемноеЛогирование("Лог удалений", "../Логи");
        ApplicationManager.УстановитьApplicationState("СистемноеЛогирование_Удаление", логированиеУдалений);
        логированиеУдалений.НачатьСеансЛогирования();

        string profile = System.Configuration.ConfigurationManager.AppSettings.Get("Profile");

        if (!string.IsNullOrEmpty(profile))
        {
            ApplicationManager.currentProject = profile;
        }
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
		Барс.Клиент.СистемныйЖурналСобытий.ЗаписатьСообщение( "Приложение закончило работу.", Барс.Клиент.ТипСообщенияЖурналаСобытий.Информация );

        // логирование
        if (Application["СистемноеЛогирование_ВходВыход"] != null
			&& Application["СистемноеЛогирование_ВходВыход"] is Барс.СистемноеЛогирование)
        {
            Барс.СистемноеЛогирование логированиеВходовВыходов = Application["СистемноеЛогирование_ВходВыход"] as Барс.СистемноеЛогирование;
            логированиеВходовВыходов.ЗакончитьСеансЛогирования();
        }

        if (Application["СистемноеЛогирование_Удаление"] != null
			&& Application["СистемноеЛогирование_Удаление"] is Барс.СистемноеЛогирование)
        {
            Барс.СистемноеЛогирование логированиеУдалений = Application["СистемноеЛогирование_Удаление"] as Барс.СистемноеЛогирование;
            логированиеУдалений.ЗакончитьСеансЛогирования();
        }
    }
        
    void Application_Error(object sender, EventArgs e) 
    {
		Барс.Клиент.СистемныйЖурналСобытий.ЗаписатьИсключительнуюСитуацию( "В системе возникло необработанное исключение.", Server.GetLastError() );

		Exception ошибка = Server.GetLastError();

		if( ошибка != null && ошибка is System.Web.HttpUnhandledException && ошибка.InnerException != null )
		{
			ошибка = ошибка.InnerException;
		}

        if (ошибка != null && ошибка is Барс.ИсключениеПулСоединенийНеИнициализирован)
        {
            Response.Redirect("ErrorAppNotInit.aspx");
        }
        else
		{
			Response.Redirect( "~/ErrorHandler.aspx" );
            Session["LastException"] = ошибка;
		}

        Server.ClearError();
    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

        // ApplicationManager не используется, потому HttpContext.Current == null 
        try
		{
            Барс.Ядро.МенеджерБД.ОсвободитьРесурсыТреда( Session.SessionID );

			Application.Lock();
			
            Барс.ВебЯдро.РаботающиеПользователи.СписокРаботающихПользователей списокРаботающихПользователей = Application [ "РаботающиеПользователи" ] as Барс.ВебЯдро.РаботающиеПользователи.СписокРаботающихПользователей;

            Барс.ВебЯдро.РаботающиеПользователи.ОписаниеРаботающегоПользователя описаниеПользователя = списокРаботающихПользователей.ПолучитьПользователя(Session.SessionID);
            
            if (описаниеПользователя != null)
            {
                if (Application["СистемноеЛогирование_ВходВыход"] != null
					&& Application["СистемноеЛогирование_ВходВыход"] is Барс.СистемноеЛогирование)
                {
                    Барс.СистемноеЛогирование логированиеВходовВыходов = Application["СистемноеЛогирование_ВходВыход"] as Барс.СистемноеЛогирование;
                    логированиеВходовВыходов.ДобавитьСообщение(string.Format("Сессия завершилась (имя пользователя: {0}, хост: {1}, роли: {2})", описаниеПользователя.ИмяПользователя, описаниеПользователя.Хост, описаниеПользователя.РолиПользователя));
                }
            }  
                 
            списокРаботающихПользователей.Удалить( Session.SessionID );
		}
		catch( Exception )
		{
		}
		finally
		{
			Application.UnLock();
		}
        
        GC.Collect();
    }
       
</script>
