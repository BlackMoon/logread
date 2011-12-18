using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for ApplicationManager
/// </summary>
public class ApplicationManager
{
    public static string currentProject = "BO";

    public static string GetProjectName()
    {
        return currentProject.ToUpper();
    }

	public static object ��������ApplicationState(string ���)
	{
		if (HttpContext.Current.Application[���] != null)
        {
			return HttpContext.Current.Application[���];
        }

		return null;
	}

	public static void ����������ApplicationState(string ���, object ��������)
	{
		if (HttpContext.Current.Application[���] != null)
		{
			HttpContext.Current.Application[���] = ��������;
		}
		else
		{
			HttpContext.Current.Application.Add(���, ��������);
		}
	}
}
