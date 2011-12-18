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
using ����.�������;

public partial class ErrorHandler : System.Web.UI.Page
{
    private bool ���������������� = false;

    protected void Page_Init(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bool isLocal = false;

            try
            {
                // ������ ������
                isLocal = Request.IsLocal;
            }
            catch
            {
            }

            ���������������� = isLocal;

            if (����������������)
            {
                Exception ������ = (Exception)Session["LastException"];

                if (������ != null)
                {
                    HtmlTableCell cellContent = (HtmlTableCell)navBar_detail.Groups[0].FindControl("content");

                    String �������� = "<b>��������� :</b>";
                    �������� += "<br/>";
                    �������� += ������.Message.Replace("\r\n", "<br/>");
                    �������� += "<br/>";
                    �������� += "<br/>";
                    �������� += "<b>����������� :</b>";
                    �������� += "<br/>";
                    �������� += ������.StackTrace.Replace("\r\n", "<br/>"); ;

                    cellContent.InnerHtml = ��������;
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
