using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Xml;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Telerik.WebControls;

using Барс.Интерфейс;
using Барс.ВебЯдро;

public partial class _Default : Page 
{
	protected void Page_Init( object sender, EventArgs e )
	{
        PageOptimizer.CombineCss(this);

        //Установка скина
        string ImagesDir = string.Format("~/Resources_Design/{0}/Images/", ApplicationManager.GetProjectName());

        ButtonExit.ImageUrl = ImagesDir + "Exit.png";
        Img_Profile.Src = ImagesDir + "Button_profile.png";
        Img_Uchrejdenie.Src = ImagesDir + "Button_uchr.png";
        Img_Users.Src = ImagesDir + "Button_users.png";
        Img_Caption.Src = ImagesDir + "Caption.png";
        Img_forms.Src = ImagesDir + "Button_forms.jpg";
        Img_analitic.Src = ImagesDir + "Button_analitic.jpg";

	    RadWindowManager1.Skin = "BarsBlue";
            
		if( Барс.Ядро.МенеджерПользователей.ТекущийПользователь == null )
		{
			Response.Redirect( "~/Login.aspx" );
			return;
		}

        string названиеУчреждения = "Учреждение не указано";

        if (Барс.ПеременныеСессии.ТекущееУчреждение != null)
        {
            названиеУчреждения = Барс.ПеременныеСессии.ТекущееУчреждение.Наименование;
        }

        RadWindowManager1.Behavior = RadWindowBehaviorFlags.Close | RadWindowBehaviorFlags.Reload;

        UserInfo.Controls.Add(new LiteralControl(названиеУчреждения + "/<br/>" + Барс.Ядро.МенеджерПользователей.ТекущийПользователь.Наименование));
       
        Row_Users.Visible = Барс.Ядро.МенеджерПользователей.ТекущийПользователь.РольПользователя == "Администратор";
        Row_Users2.Visible = Барс.Ядро.МенеджерПользователей.ТекущийПользователь.РольПользователя == "Администратор";
    }

	protected void Page_Load( object sender, EventArgs e )
    {
		if( !IsPostBack )
		{
            Ajax.Utility.RegisterTypeForAjax(typeof(Bars.AjaxMethods));

            Bars.AjaxMethods.RemoveAllBlocks();
		}
    }

	protected void ImageButton_EndSession_Click( object sender, ImageClickEventArgs e )
	{
		Барс.Ядро.МенеджерБД.ОсвободитьРесурсыТреда();

        Bars.AjaxMethods.RemoveAllBlocks();

        Bars.AjaxMethods.ClearSession();

		Session.Abandon();
		
		Response.Redirect( "~/Login.aspx" );
	}

    protected void RadAjaxTimer_Messages_Tick(object sender, TickEventArgs e)
    {
        if (Барс.ВебЯдро.РаботающиеПользователи.СообщениеАдминистратора.ЕстьСообщение)
        {
            Panel_AdminMessages.Controls.Add(new LiteralControl("<script type='text/javascript'>alert('" + Барс.ВебЯдро.РаботающиеПользователи.СообщениеАдминистратора.ПолныйТекстСообщения + "')</script>"));
        }
    }
}
