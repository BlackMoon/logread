<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Admin_LoggedUsers_List" Codebehind="LoggedUsers_List.ascx.cs" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

<br />
<div align="center">
	<Bars:������ ID="������_��������" runat="server" Text="��������" OnClick="������_��������_Click" />
</div>
<br />
<Bars:������� ID="�������_����������������������" runat="server">
</Bars:�������>
<br />
<br />
<div align="left">

<asp:Panel ID="Panel_Message_Plus" runat="server" GroupingText="����� ��������� �������������" Width="500px">
	<table width="500">
	<tr>
		<td colspan="2">
		������� ����� ���������, ������� ����� ��������� ���� ������������� (��������� ����� ������� �������������� � ������� ������)
		</td>
	</tr>
	<tr>
		<td width="150" align="right">
		����� ���������:	
		</td>
		<td width="350">
			<Bars:��������������� ID="���������������_��������������" runat="server" EmptyMessage="������� ����� ���������" Width="340px" ����������=�������������></Bars:���������������>		
		</td>
	</tr>
	<tr>
		<td align="right">
		���� �������� (���):
		</td>
		<td>
			<Bars:�������������� ID="��������������_������������" runat="server" �������������=0 Width="50px"></Bars:��������������>
		</td>
	</tr>
	<tr>
		<td colspan="2" align="right">
			<Bars:������ ID="������_�������������������" runat="server" Text="���������" OnClick="������_�������������������_Click"/>
		</td>
	</tr>
	</table>
</asp:Panel>

<asp:Panel ID="Panel_Message_Minus" runat="server" GroupingText="������� ��������� �������������" Width="500px">
	<table width="500px">
	<tr>
		<td colspan="2">
			<asp:Label ID="�������_����������������" runat="server"></asp:Label>
		</td>
	</tr>
	<tr>
		<td colspan="2" align="left">
			<Bars:������ ID="������_�����������������������" runat="server" Text="������" OnClick="������_�����������������������_Click"/>
		</td>
	</tr>
	</table>
</asp:Panel>
</div>


