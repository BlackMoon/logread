using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Барс.ВебЯдро.Интерфейс;
using Барс.Своды;
using Барс.Ядро;
using Барс;

public partial class Forms_Profile_Profile : Барс.ВебЯдро.Интерфейс.ВебФорма
{
    public Forms_Profile_Profile()
        : base()
    {
        this.ШапкаСтраницы = "";
        this.ЗаголовокСтраницы = "Мой профиль";
        this.ШиринаОкна = 600;
        this.ВысотаОкна = 300;

        this.ПриИнициализацииСтраницы += new Барс.Интерфейс.ОбработчикСобытия(Forms_Profile_Profile_ПриИнициализацииСтраницы);
        this.ПриЗагрузкеСтраницы += new Барс.Интерфейс.ОбработчикСобытия(Forms_Profile_Profile_ПриЗагрузкеСтраницы);
    }

    void Forms_Profile_Profile_ПриИнициализацииСтраницы(object Отправитель, Барс.Интерфейс.АргументыСобытия Аргументы)
    {
        if (!IsPostBack)
        {
            Пользователь текущийПользователь = МенеджерПользователей.ТекущийПользователь;

            this.РедактируемыйОбъект = текущийПользователь;
        }
    }

    void Forms_Profile_Profile_ПриЗагрузкеСтраницы(object Отправитель, Барс.Интерфейс.АргументыСобытия Аргументы)
    {
        if (!IsPostBack)
        {
            this.DataBind();
        }
    }

    protected void Кнопка_ОК_Click(object sender, EventArgs e)
    {
        try
        {
            Пользователь текущийПользователь = this.РедактируемыйОбъект as Пользователь;

            Dictionary<object, object> НовыеЗначения = КонтрольСвязывания.ПолучитьЗначенияСвойств(this);

            string старыйПароль = (string)НовыеЗначения["СтарыйПароль"];

            Пользователь пользователь = new Пользователь();
            пользователь.Пароль = старыйПароль;

            if (текущийПользователь.Пароль != пользователь.Пароль)
            {
                throw new Exception("Вы ввели неправильный текущий пароль!");
            }

            string новыйПароль = (string)НовыеЗначения["НовыйПароль"];
            string новыйПароль2 = (string)НовыеЗначения["НовыйПароль2"];

            if (новыйПароль != новыйПароль2)
            {
                throw new Exception("Значения в полях Пароль и Подтверждение пароля не совпадают.");
            }

            try
            {
                текущийПользователь.Пароль = новыйПароль;
			    текущийПользователь.Заблокировать();
			    текущийПользователь.Сохранить();
			    текущийПользователь.СнятьБлокировку();
            }
            catch
            {
                текущийПользователь.ПеречитатьОбъект();
                throw;
            }

            ВыставлятьРазмерыОкна = false;

            Controls.AddAt(0, new LiteralControl("<script type=\"text/javascript\">alert('Профиль успешно изменен!');Close();</script>"));

            return;
        }
        catch( Exception exc )
        {
            Сообщение.ПоказатьИсключительнуюСитуацию(this, "Не удалось сохранить профиль.", exc);
        }
    }
}
