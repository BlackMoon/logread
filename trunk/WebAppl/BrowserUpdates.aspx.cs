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
using Барс.ВебЯдро.Интерфейс;
using Барс.ВебЯдро;

public partial class BrowserUpdates : System.Web.UI.Page
{
	protected void Page_Init( object sender, EventArgs e )
	{
		ПараметрыГлавногоВебОкна Параметры = new ПараметрыГлавногоВебОкна( Request.QueryString );

		HtmlPageHead.Controls.AddAt( 0, new LiteralControl( СтилевыеНастройкиПриложения.ПолучитьСкриптУстановкиСтиля() ) );

		if( Параметры.ПараметрЗадан( "info" ) )
		{
			string [] списокНеполадок = Параметры [ "info" ].Split( new char [] { '-' } );

			if( списокНеполадок.Length > 0 )
			{
				foreach( string неполадка in списокНеполадок )
				{
					Table table = new Table();

					table.Attributes.Add( "border", "1" );
					table.Attributes.Add( "cellpadding", "2" );
					table.Attributes.Add( "cellspacing", "2" );
					table.Attributes.Add( "width", "100%" );

					TableHeaderRow header = new TableHeaderRow();


					TableHeaderCell header_cell = new TableHeaderCell();
					header_cell.Text = "&nbsp Файл";
					header_cell.Attributes.Add( "width", "25%" );
					header.Cells.Add( header_cell );

					header_cell = new TableHeaderCell();
					header_cell.Attributes.Add( "width", "9%" );
					header_cell.Text = "&nbsp Размер";
					header.Cells.Add( header_cell );

					header_cell = new TableHeaderCell();
					header_cell.Attributes.Add( "width", "33%" );
					header_cell.Text = "&nbsp Описание";
					header.Cells.Add( header_cell );

					header_cell = new TableHeaderCell();
					header_cell.Attributes.Add( "width", "33%" );
					header_cell.Text = "&nbsp Комментарий";
					header.Cells.Add( header_cell );

					table.Rows.Add( header );

					if( неполадка == "jscript_8820" )
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
		cell.Text = "Критическое обновление браузера Internet Explorer версии 6.0. Выпущено в июне 2006 г. Уязвимость в Microsoft JScript делает возможным удаленный запуск программного кода. <b>Рекоммендовано Корпорацией Microsoft к немедленной установке.</b>";
		row.Cells.Add( cell );

		cell = new TableCell();
		cell.Text = "Ознакомиться с бюллетенем безопастности Корпорации Microsoft можно <a href='http://support.microsoft.com/kb/917344/'>здесь</a>.";
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
		cell.Text = "Критическое обновление браузера Internet Explorer версии 6.0. Выпущено в июне 2007 г. Уязвимость в Microsoft JScript делает возможным удаленный запуск программного кода. <b>Рекоммендовано Корпорацией Microsoft к немедленной установке.</b>";
		row.Cells.Add( cell );

		cell = new TableCell();
		cell.Text = "Ознакомиться с бюллетенем безопастности Корпорации Microsoft можно <a href='http://support.microsoft.com/kb/933566/'>здесь</a>.<br><br><b>Установка потребует дальнейшей перезагрузки компьютера.</b>";
		row.Cells.Add( cell );

		table.Rows.Add( row );

	}
}
