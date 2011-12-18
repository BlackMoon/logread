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

	public static object ПолучитьApplicationState(string имя)
	{
		if (HttpContext.Current.Application[имя] != null)
        {
			return HttpContext.Current.Application[имя];
        }

		return null;
	}

	public static void УстановитьApplicationState(string имя, object значение)
	{
		if (HttpContext.Current.Application[имя] != null)
		{
			HttpContext.Current.Application[имя] = значение;
		}
		else
		{
			HttpContext.Current.Application.Add(имя, значение);
		}
	}
}
