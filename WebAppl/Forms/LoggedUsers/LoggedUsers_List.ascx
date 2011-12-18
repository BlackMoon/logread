<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Admin_LoggedUsers_List" Codebehind="LoggedUsers_List.ascx.cs" %>
<%@ Register Assembly="¬ебядро" Namespace="Ѕарс.¬ебядро.»нтерфейс" TagPrefix="Bars" %>

<br />
<div align="center">
	<Bars: нопка ID=" нопка_ќбновить" runat="server" Text="ќбновить" OnClick=" нопка_ќбновить_Click" />
</div>
<br />
<Bars:“аблица ID="“аблица_–аботающиеѕользователи" runat="server">
</Bars:“аблица>
<br />
<br />
<div align="left">

<asp:Panel ID="Panel_Message_Plus" runat="server" GroupingText="Ќовое сообщение пользовател€м" Width="500px">
	<table width="500">
	<tr>
		<td colspan="2">
		”кажите текст сообщени€, который будет отправлен всем пользовател€м (сообщение будет прин€то пользовател€ми в течении минуты)
		</td>
	</tr>
	<tr>
		<td width="150" align="right">
		“екст сообщени€:	
		</td>
		<td width="350">
			<Bars:ѕоле¬вода“екста ID="ѕоле¬вода“екста_“екст—ообщени€" runat="server" EmptyMessage="”кажите текст сообщени€" Width="340px" –ежим¬вода=ћногострочный></Bars:ѕоле¬вода“екста>		
		</td>
	</tr>
	<tr>
		<td align="right">
		—рок действи€ (мин):
		</td>
		<td>
			<Bars:ѕоле¬вода„исла ID="ѕоле¬вода„исла_—рокƒействи€" runat="server" “очность„исла=0 Width="50px"></Bars:ѕоле¬вода„исла>
		</td>
	</tr>
	<tr>
		<td colspan="2" align="right">
			<Bars: нопка ID=" нопка_”становить—ообщение" runat="server" Text="ќтправить" OnClick=" нопка_”становить—ообщение_Click"/>
		</td>
	</tr>
	</table>
</asp:Panel>

<asp:Panel ID="Panel_Message_Minus" runat="server" GroupingText="“екущее сообщение пользовател€м" Width="500px">
	<table width="500px">
	<tr>
		<td colspan="2">
			<asp:Label ID="Ќадпись_“екущее—ообщение" runat="server"></asp:Label>
		</td>
	</tr>
	<tr>
		<td colspan="2" align="left">
			<Bars: нопка ID=" нопка_”далить“екущее—ообщение" runat="server" Text="”брать" OnClick=" нопка_”далить“екущее—ообщение_Click"/>
		</td>
	</tr>
	</table>
</asp:Panel>
</div>


