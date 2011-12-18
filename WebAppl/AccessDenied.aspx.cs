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
using Ѕарс.¬ебядро;

public partial class AccessDenied : System.Web.UI.Page
{
	public string SecurityMessage = "доступ к данному разделу.";

	protected void Page_Init( object sender, EventArgs e )
	{
		HtmlPageHead.Controls.Add( new LiteralControl( —тилевыеЌастройкиѕриложени€.ѕолучить—крипт”становки—тил€() ) );
	}

	protected void Page_Load( object sender, EventArgs e )
	{
		string описание = Request.QueryString [ "value" ];

		if( !string.IsNullOrEmpty( описание ) )
		{
			SecurityMessage = описание;
		}
	}
}
