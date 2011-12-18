<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Analytics_AnalyticExtracts_View" Codebehind="AnalyticExtracts_View.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v8.2.Export, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.XtraPivotGrid.Web" TagPrefix="dxpgw" %>
<%@ Register Assembly="RadMenu.Net2" Namespace="Telerik.WebControls" TagPrefix="radM" %>

<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dxwpg" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>

<table cellpadding="0" cellspacing="0" width="100%">
    <tr>
        <td align="left">
            <dxm:ASPxMenu ID="ASPxMenu1" runat="server" OnItemClick="ASPxMenu1_ItemClick" ItemAutoWidth="False" Width="100%">
                <Items>
                    <dxm:MenuItem Name="item_печать" Text="Печать">
                        <Image Url="~/Resources/Images/BrowserMenu/Print.png" />
                    </dxm:MenuItem>
                </Items>
            </dxm:ASPxMenu>
        </td>
    </tr>
    <tr>
        <td align="left">
            <dxwpg:ASPxPivotGrid ID="АналитическаяТаблица" runat="server" OnCustomSummary="АналитическаяТаблица_CustomSummary" Width="100%" Height="100%">
                <OptionsView DataHeadersDisplayMode="Popup" />
                <OptionsPager RowsPerPage="40">
                </OptionsPager>
            </dxwpg:ASPxPivotGrid>

            <dxpgw:ASPxPivotGridExporter id="ASPxPivotGridExporter_1" runat="server" ASPxPivotGridID="АналитическаяТаблица">
            </dxpgw:ASPxPivotGridExporter>
        </td>
    </tr>
</table>



