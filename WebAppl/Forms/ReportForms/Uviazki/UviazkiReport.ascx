<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_ReportForms_Uviazki_UviazkiReport" Codebehind="UviazkiReport.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="ВебЯдро" Namespace="Барс.ВебЯдро.Интерфейс" TagPrefix="Bars" %>

<div runat="server" ID="Div_Result" style="font-size:large;color:Green;">
</div>

<table width="100%" id="main_Table" cellpadding="10" cellspacing="10">
    <tr>
        <td colspan="2" align="left">
            <dxwgv:ASPxGridView ID="gvUviazki" runat="server" Width="100%" AutoGenerateColumns="False" OnBeforePerformDataSelect="gvUviazki_BeforePerformDataSelect">
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Тип ошибки" FieldName="ТипОшибкиПроверкиСтрокой"
                        VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Сообщение" FieldName="Сообщение" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Расхождение" FieldName="Расхождение" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsPager NumericButtonCount="15">
                </SettingsPager>
                <Settings ShowGroupPanel="True" ShowVerticalScrollBar="True" VerticalScrollableHeight="400" />
            </dxwgv:ASPxGridView>
        </td>
    </tr>
    <tr style="height: 26px">
        <td align="left" style="height: 26px">
            <dxe:ASPxButton ID="buttonPrint" runat="server" OnClick="buttonPrint_Click" Text="Печать"
                Width="100px">
            </dxe:ASPxButton>
        </td>
        <td align="right" style="height: 26px">
            <dxe:ASPxButton ID="buttonClose" runat="server" Text="Закрыть" Width="100px">
                <ClientSideEvents Click="function(s, e) {
	Close();
}" />
            </dxe:ASPxButton>
        </td>
    </tr>
</table>