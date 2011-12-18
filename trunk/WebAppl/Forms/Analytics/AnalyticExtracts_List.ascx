<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Analytics_AnalyticExtracts_List" Codebehind="AnalyticExtracts_List.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<script type="text/javascript">
function OnRowDblClick(s, e)
{
    var row = s.GetRow( e.visibleIndex );
    
    var keyValue = row.getAttribute("key");
    
    if( keyValue != null )
    {
        ShowForm( "~/Forms/Analytics/AnalyticExtracts_Setup.ascx", "", keyValue );
    }
}
</script>

<table style="width:100%">
    <tr>
        <td align="left">
            <dxwgv:ASPxGridView ID="ТаблицаЭлементы" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="ObjectDataSource_list" KeyFieldName="ID" OnHtmlRowCreated="ТаблицаЭлементы_HtmlRowCreated" Enabled="False">
                <Settings ShowGroupPanel="True" ShowFilterRow="True" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="Наименование" FieldName="Наименование" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Тип" FieldName="Тип" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Группа" FieldName="Группа" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedRow="True" />
                <SettingsPager PageSize="30">
                </SettingsPager>
                <SettingsCookies CookiesID="extracts_grid" Enabled="True" />
                <ClientSideEvents RowDblClick="OnRowDblClick" />
            </dxwgv:ASPxGridView>
            
        </td>
    </tr>
</table>

<asp:ObjectDataSource ID="ObjectDataSource_list" runat="server" SelectMethod="GetExtracts" TypeName="Forms_Analytics_AnalyticExtracts_List" CacheDuration="0" CacheKeyDependency="extracts_list" EnableCaching="True"></asp:ObjectDataSource>

