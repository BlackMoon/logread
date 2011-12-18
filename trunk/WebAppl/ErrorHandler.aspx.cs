using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxNavBar;
using Ѕарс.¬ебядро;

public partial class ErrorHandler : System.Web.UI.Page
{
    private bool выводитьќписание = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool isLocal = false;

            try
            {
                // ѕадает иногда
                isLocal = Request.IsLocal;
            }
            catch
            {
            }

            выводитьќписание = isLocal;

            if (выводитьќписание)
            {
                Exception ошибка = (Exception)Session["LastException"];

                if (ошибка != null)
                {
                    HtmlTableCell cellContent = (HtmlTableCell)navBar_detail.Groups[0].FindControl("content");

                    String сведени€ = "<b>—ообщение :</b>";
                    сведени€ += "<br/>";
                    сведени€ += ошибка.Message.Replace("\r\n", "<br/>");
                    сведени€ += "<br/>";
                    сведени€ += "<br/>";
                    сведени€ += "<b>“рассировка :</b>";
                    сведени€ += "<br/>";
                    сведени€ += ошибка.StackTrace.Replace("\r\n", "<br/>"); ;

                    cellContent.InnerHtml = сведени€;
                }

                Server.ClearError();
            }
            else
            {
                navBar_detail.Visible = false;
            }
        }
    }
}
