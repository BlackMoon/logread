<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_MacroProcessing_CompareResult" Codebehind="CompareResult.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

<table width="100%" id="main_Table" cellpadding="10" cellspacing="10">
    <tr>
        <td align="left">
            <asp:Label runat="server" ID="��������������������������" Visible="False" Font-Bold="True" />
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label runat="server" ID="�����������������������������" Visible="False" Font-Bold="True" />
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <dxwgv:ASPxGridView ID="�������_���������" runat="server" AutoGenerateColumns="False"
                Width="100%">
                <Settings ShowFilterRow="True" ShowGroupPanel="True"
                    VerticalScrollableHeight="420" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="�����" FieldName="�����" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="�������" FieldName="�������" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="����������" FieldName="����������" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="�������" FieldName="�������" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="������" FieldName="������" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="�������" FieldName="�������" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="��� ������" FieldName="���������" VisibleIndex="6">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="��������" FieldName="��������" VisibleIndex="7">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsPager PageSize="7">
                </SettingsPager>
                <SettingsCookies CookiesID="CompareResultGrid" Enabled="True" />
            </dxwgv:ASPxGridView>
        </td>
    </tr>
    <tr>
        <td align="left">
            <dxe:ASPxButton ID="ASPxButton_������" runat="server" Text="������" Width="100px" OnClick="ASPxButton_������_Click">
            </dxe:ASPxButton>
            
        </td>
        <td align="right">
            <dxe:ASPxButton ID="ASPxButton_�������" runat="server" Text="�������" Width="100px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	Close();
}" />
            </dxe:ASPxButton>
        </td>
    </tr>
</table>