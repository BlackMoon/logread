<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_SdachaOtchetnosti_ConsolidateForm_ConsolidateForm" Codebehind="ConsolidateForm.ascx.cs" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

<script type="text/javascript" src="Resources/Scripts/FPSpread.js"></script>

<table width="100%" id="main_Table">
    <tr>
        <td colspan="2">
            <Bars:������� ID="�������_��������" runat="server" style="margin-left:5px;" Width="98%" AllowPaging="false">
            </Bars:�������>
        </td>
    </tr>
    <tr>
        <td align="left" style="height: 26px">
            <Bars:������ ID="������_���������" runat="server" �����="��������� ��������� � ���� ����� ������" ��������="true"/>
        </td>
        <td align="right" style="height: 26px">
            <Bars:������ runat="server" Text="�������" ID="������_��" ���������="������" OnClick="������_��_Click" />
            <Bars:������ runat="server" Text="������" ID="������_������" ���������="������" OnClientClick="Close();" />
        </td>
    </tr>
</table>
