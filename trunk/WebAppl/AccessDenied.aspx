<%@ Page Language="C#" AutoEventWireup="true" Inherits="AccessDenied" Codebehind="AccessDenied.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head ID="HtmlPageHead" runat="server">
    <title>Сообщение системы безопасности</title>
</head>
<body class="BarsBody">
    <form id="form1" runat="server">
    
    <div style="position:relative;top:200px;">
    <table align="center" width="500px">
    <tr>
		<td width="60px">
			<img align="top" src="Resources/Images/Common/access_denied.png"/>
		</td>
		<td align="left" width="440px">
			<font color="Red" size=+1><b>Сообщение системы безопасности</b></font>
			<br />
			<hr />
			<br />
			У Вас недостаточно прав на <% =this.SecurityMessage %>
			<br />
			<hr />
		</td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
