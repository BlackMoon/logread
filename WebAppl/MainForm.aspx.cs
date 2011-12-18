using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.WebControls;

using Барс.ВебЯдро.Интерфейс;
using Барс.ВебЯдро;

public partial class MainForm : ГлавноеВебОкно
{
    protected override void ИнициализацияОкна()
    {
		//Установка скина
        winManag.Skin = "BarsBlue";

        string Url = ПолучитьПереданныйURL();

		Control userControl = null;

		if( !string.IsNullOrEmpty( Url ) )
		{

			try
			{
#if RELEASE

				FormUrl = FormUrl.ToLower().Replace('/', '_').Replace('.', '_');

				string typeName = string.Format("ASP.{0},WebApp_deploy", FormUrl);

				Type type = Type.GetType(typeName);

				userControl = LoadControl(type, null);
#else
				userControl = LoadControl( Url );
#endif
			}
			catch( Exception exc )
			{
                throw new Exception("Не удалось загрузить данный раздел", exc);
			}
		}

        if (userControl == null)
        {
            this.Title = "Данный раздел не реализован";

			this.Controls.Add( new LiteralControl("<h4 class='middleheader'>Извините, данный раздел пока еще не реализован в системе</h4>"));
            return;
        }

        if (userControl is ВебФорма)
        {
            ВебФорма форма = (userControl as ВебФорма);

            this.Title = форма.ЗаголовокСтраницы;

            form_main.Controls.AddAt(4, userControl);
        }

        winManag.Behavior = RadWindowBehaviorFlags.Close | RadWindowBehaviorFlags.Reload;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(Bars.AjaxMethods));
        }
    }

    protected override void РендерОкна()
    {
        ВебФорма форма = null;
        Div_PageHeader.Style.Add("display", "none");

        foreach (Control control in form_main.Controls)
        {
            if (control is ВебФорма)
            {
                форма = (control as ВебФорма);
                break;
            }
        }

        if (форма != null)
        {
            this.Title = форма.ЗаголовокСтраницы;

            if (!string.IsNullOrEmpty(форма.ШапкаСтраницы))
            {
                Div_PageHeader.Style.Remove("display");
                Div_PageHeader.Visible = true;
                Div_PageHeader.InnerText = форма.ШапкаСтраницы;
            }
        }
    }
}
