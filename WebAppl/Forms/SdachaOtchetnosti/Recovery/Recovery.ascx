<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_SdachaOtchetnosti_Recovery_Recovery" Codebehind="Recovery.ascx.cs" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

<script type="text/javascript" src="Resources/Scripts/FPSpread.js"></script>

<table width="100%" id="main_Table">
    <tr>
        <td>
            <Bars:������� ID="�������_��������" runat="server" style="margin-left:5px;" Width="98%" AllowPaging="false">
            </Bars:�������>
        </td>
    </tr>
    <tr>
        <td align="right" style="height: 26px">
            <Bars:������ runat="server" Text="������������" ID="������_��" ���������="������" OnClick="������_��_Click" />
            <Bars:������ runat="server" Text="������" ID="������_������" ���������="������" OnClientClick="Close();" />
        </td>
    </tr>
</table>

