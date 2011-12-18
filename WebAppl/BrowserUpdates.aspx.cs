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
using ����.�������.���������;
using ����.�������;

public partial class BrowserUpdates : System.Web.UI.Page
{
	protected void Page_Init( object sender, EventArgs e )
	{
		������������������������ ��������� = new ������������������������( Request.QueryString );

		HtmlPageHead.Controls.AddAt( 0, new LiteralControl( ���������������������������.����������������������������() ) );

		if( ���������.�������������( "info" ) )
		{
			string [] ��������������� = ��������� [ "info" ].Split( new char [] { '-' } );

			if( ���������������.Length > 0 )
			{
				foreach( string ��������� in ��������������� )
				{
					Table table = new Table();

					table.Attributes.Add( "border", "1" );
					table.Attributes.Add( "cellpadding", "2" );
					table.Attributes.Add( "cellspacing", "2" );
					table.Attributes.Add( "width", "100%" );

					TableHeaderRow header = new TableHeaderRow();


					TableHeaderCell header_cell = new TableHeaderCell();
					header_cell.Text = "&nbsp ����";
					header_cell.Attributes.Add( "width", "25%" );
					header.Cells.Add( header_cell );

					header_cell = new TableHeaderCell();
					header_cell.Attributes.Add( "width", "9%" );
					header_cell.Text = "&nbsp ������";
					header.Cells.Add( header_cell );

					header_cell = new TableHeaderCell();
					header_cell.Attributes.Add( "width", "33%" );
					header_cell.Text = "&nbsp ��������";
					header.Cells.Add( header_cell );

					header_cell = new TableHeaderCell();
					header_cell.Attributes.Add( "width", "33%" );
					header_cell.Text = "&nbsp �����������";
					header.Cells.Add( header_cell );

					table.Rows.Add( header );

					if( ��������� == "jscript_8820" )
					{
                        ProcessError_jscript_8820(table);
					}

					Updates_Table.Controls.Add( table );
				}
			}
		}
	}

	private void ProcessError_jscript_8820( Table table )
	{
		TableRow row = new TableRow();
		TableCell cell;

		cell = new TableCell();
		if( Request.Browser.Platform == "WinXP" )
		{
			cell.Text = @"<a href='" + this.ResolveUrl( "redist/WindowsXP-KB917344-x86-RUS.exe" ) + "'>WindowsXP-KB917344-x86-RUS.exe</a>";
		}
		row.Cells.Add( cell );

		cell = new TableCell();
		cell.Text = "771 216";
		cell.Attributes.Add( "align", "right" );
		row.Cells.Add( cell );

		cell = new TableCell();
		cell.Text = "����������� ���������� �������� Internet Explorer ������ 6.0. �������� � ���� 2006 �. ���������� � Microsoft JScript ������ ��������� ��������� ������ ������������ ����. <b>�������������� ����������� Microsoft � ����������� ���������.</b>";
		row.Cells.Add( cell );

		cell = new TableCell();
		cell.Text = "������������ � ���������� ������������� ���������� Microsoft ����� <a href='http://support.microsoft.com/kb/917344/'>�����</a>.";
		row.Cells.Add( cell );

		table.Rows.Add( row );

		row = new TableRow();

		cell = new TableCell();
		if( Request.Browser.Platform == "WinXP" )
		{
			cell.Text = @"&nbsp <a href='" + this.ResolveUrl( "redist/WindowsXP-KB933566-x86-RUS.exe") + "'>WindowsXP-KB933566-x86-RUS.exe</a>";
		}
		row.Cells.Add( cell );

		cell = new TableCell();
		cell.Text = "4 706 184";
		cell.Attributes.Add( "align", "right" );
		row.Cells.Add( cell );

		cell = new TableCell();
		cell.Text = "����������� ���������� �������� Internet Explorer ������ 6.0. �������� � ���� 2007 �. ���������� � Microsoft JScript ������ ��������� ��������� ������ ������������ ����. <b>�������������� ����������� Microsoft � ����������� ���������.</b>";
		row.Cells.Add( cell );

		cell = new TableCell();
		cell.Text = "������������ � ���������� ������������� ���������� Microsoft ����� <a href='http://support.microsoft.com/kb/933566/'>�����</a>.<br><br><b>��������� ��������� ���������� ������������ ����������.</b>";
		row.Cells.Add( cell );

		table.Rows.Add( row );

	}
}
