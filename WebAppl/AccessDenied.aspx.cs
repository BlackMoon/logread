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
using ����.�������;

public partial class AccessDenied : System.Web.UI.Page
{
	public string SecurityMessage = "������ � ������� �������.";

	protected void Page_Init( object sender, EventArgs e )
	{
		HtmlPageHead.Controls.Add( new LiteralControl( ���������������������������.����������������������������() ) );
	}

	protected void Page_Load( object sender, EventArgs e )
	{
		string �������� = Request.QueryString [ "value" ];

		if( !string.IsNullOrEmpty( �������� ) )
		{
			SecurityMessage = ��������;
		}
	}
}
