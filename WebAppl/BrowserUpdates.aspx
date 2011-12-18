<%@ Page Language="C#" AutoEventWireup="true" Inherits="BrowserUpdates" Codebehind="BrowserUpdates.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head ID="HtmlPageHead" runat="server">
    <title>���������� ���������� ��������</title>
</head>
<body>
    <form id="form_main" runat="server">
    <div style="left:10px;top:10px;position:absolute;width:70%;">
		<div align="justify">
		<p>
			<font size=+2>�������������� ���������</font>
			<br />
			<br />
			<font size=+1>��������, ���������� ������������� ����� �����������, ��������� �� ���������� ���������� 
			� ��� ���������� ������� ���� ����������� ������ � �������.
			</font>
		</p>
		
		<p>
			�� ������ ��� ��������� ������, ��� ���� ������� �������� ������ ���������� ��������� ����������,
			��� ������������ ���� ������� � (���) ���� �� ��� ����������� �� ������������� ������� ��������� ������������.
			��� ������������ ���������� ������� <b>Mozilla Firefox</b>, ���� �������� <b>Internet Explorer</b> �� ������ 7.0 ��� ����.
		</p>
		
		<p><font size=+1><a href='<%=Page.ResolveUrl( "redist/Firefox_Setup.exe" ) %>'>���������� Mozilla Firefox</a></font></p>
		
		<p>
			���� ����������� ������������� �� ��������� ��� ������� ����� � ����������� *.exe, �� ����� ����������� ������� �����������
			�������� �� ��������� <a href='<%=Page.ResolveUrl( "redist/Firefox_Setup.bin" ) %>'>������</a>. ����� ���������� ����������
			�������������� ���� � Firefox_Setup.exe � ��������� ������� ���������.
		</p>
		
		<p>
		���������� �� ������������ ���������: 
		</p>
		<p>
			<li>������������ �������: <b><%=Request.Browser.Platform %></b></li>
			<li>������ "User Agent": <b><%=Request.UserAgent%></b></li>
			<li>�������: <b><%=Request.Browser.Browser + " " + Request.Browser.Version + " (" + Request.Browser.Type + ")"%></b></li>
			<li>����: <b><%=Request.ServerVariables[ "HTTP_ACCEPT_LANGUAGE" ] %></b></li>
		</p>
		
		<p>
		����������� ������������� ������� �������� ����� ���� ������������ ����������� ��� ��� � ����� �����������. 
		� ����� � ����, ��� ��������� �������� � ������� �� ��� ���, ���� �� �� ��������� ������������� ����������.
		</p>
		
		<p>
		<font size=+1>��� ���������� ���������� ��������� ����������:</font>
		</p>
		<p>
		<div runat="server" id="Updates_Table">
		</div>
		</p>
		<p>
		�������� �� ��, ��� �� ������� ���� ����� ������������ ������ ��� �������, ������� �� �����������, 
		�� ����������� ��� ���������� ������� <a href='<%=Page.ResolveUrl( "redist/Firefox_Setup.exe" ) %>'><b>Mozilla Firefox</b></a> ��� �������� <b>Internet Explorer</b> �� ������ 7.0 ��� ����.
		</p>
		</div>
		
		
    </div>
    </form>
</body>
</html>
