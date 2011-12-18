<%@ Page Language="C#" AutoEventWireup="true" Inherits="BrowserUpdates" Codebehind="BrowserUpdates.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head ID="HtmlPageHead" runat="server">
    <title>Необходимо обновление браузера</title>
</head>
<body>
    <form id="form_main" runat="server">
    <div style="left:10px;top:10px;position:absolute;width:70%;">
		<div align="justify">
		<p>
			<font size=+2>ИНФОРМАЦИОННОЕ СООБЩЕНИЕ</font>
			<br />
			<br />
			<font size=+1>Прочтите, пожалуйста нижеследующий текст внимательно, поскольку от выполнения изложенных 
			в нем требований зависит Ваша возможность работы в системе.
			</font>
		</p>
		
		<p>
			Вы видите это сообщение потому, что наша система контроля версий клиентских браузеров определила,
			что используемый Вами браузер и (или) один из его компонентов не удовлетворяет должным критериям безопасности.
			Вам предлагается установить браузер <b>Mozilla Firefox</b>, либо обновить <b>Internet Explorer</b> до версии 7.0 или выше.
		</p>
		
		<p><font size=+1><a href='<%=Page.ResolveUrl( "redist/Firefox_Setup.exe" ) %>'>Установить Mozilla Firefox</a></font></p>
		
		<p>
			Если ограничения безопастности не разрешает вам скачать файлы с расширением *.exe, то можно попробовать скачать дистрибутив
			браузера по следующей <a href='<%=Page.ResolveUrl( "redist/Firefox_Setup.bin" ) %>'>ссылке</a>. После скачивания необходимо
			переименования файл в Firefox_Setup.exe и запустить процесс установки.
		</p>
		
		<p>
		Информация об используемой платформе: 
		</p>
		<p>
			<li>Операционная система: <b><%=Request.Browser.Platform %></b></li>
			<li>Строка "User Agent": <b><%=Request.UserAgent%></b></li>
			<li>Браузер: <b><%=Request.Browser.Browser + " " + Request.Browser.Version + " (" + Request.Browser.Type + ")"%></b></li>
			<li>Язык: <b><%=Request.ServerVariables[ "HTTP_ACCEPT_LANGUAGE" ] %></b></li>
		</p>
		
		<p>
		Продолжение использования данного браузера может быть потенциально небезопасно для Вас и Вашей организации. 
		В связи с этим, Вам запрещено работать в системе до тех пор, пока Вы не выполните нижеследующие требования.
		</p>
		
		<p>
		<font size=+1>Вам необходимо установить следующие обновления:</font>
		</p>
		<p>
		<div runat="server" id="Updates_Table">
		</div>
		</p>
		<p>
		Несмотря на то, что мы уважаем Ваше право использовать именно тот браузер, который Вы используете, 
		мы рекомендуем Вам установить браузер <a href='<%=Page.ResolveUrl( "redist/Firefox_Setup.exe" ) %>'><b>Mozilla Firefox</b></a> или обновить <b>Internet Explorer</b> до версии 7.0 или выше.
		</p>
		</div>
		
		
    </div>
    </form>
</body>
</html>
