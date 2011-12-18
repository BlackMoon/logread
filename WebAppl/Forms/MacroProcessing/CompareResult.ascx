<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_MacroProcessing_CompareResult" Codebehind="CompareResult.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="ВебЯдро" Namespace="Барс.ВебЯдро.Интерфейс" TagPrefix="Bars" %>

<table width="100%" id="main_Table" cellpadding="10" cellspacing="10">
    <tr>
        <td align="left">
            <asp:Label runat="server" ID="заголовокРезультатовСверки" Visible="False" Font-Bold="True" />
        </td>
    </tr>
    <tr>
        <td align="left">
            <asp:Label runat="server" ID="подзаголовокРезультатовСверки" Visible="False" Font-Bold="True" />
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <dxwgv:ASPxGridView ID="Таблица_Результат" runat="server" AutoGenerateColumns="False"
                Width="100%">
                <Settings ShowFilterRow="True" ShowGroupPanel="True"
                    VerticalScrollableHeight="420" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Форма" FieldName="Форма" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Вкладка" FieldName="Вкладка" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Субтаблица" FieldName="Субтаблица" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Столбец" FieldName="Столбец" VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Строка" FieldName="Строка" VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Условие" FieldName="Условие" VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Тип ошибки" FieldName="ТипОшибки" VisibleIndex="6">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Значение" FieldName="Значение" VisibleIndex="7">
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
            <dxe:ASPxButton ID="ASPxButton_печать" runat="server" Text="Печать" Width="100px" OnClick="ASPxButton_печать_Click">
            </dxe:ASPxButton>
            
        </td>
        <td align="right">
            <dxe:ASPxButton ID="ASPxButton_закрыть" runat="server" Text="Закрыть" Width="100px" AutoPostBack="False">
                <ClientSideEvents Click="function(s, e) {
	Close();
}" />
            </dxe:ASPxButton>
        </td>
    </tr>
</table>