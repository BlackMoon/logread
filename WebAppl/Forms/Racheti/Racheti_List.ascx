<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Racheti_List.ascx.cs" Inherits="Racheti_List" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxDataView" TagPrefix="dxdv" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

<script type="text/javascript">
function OnRowDblClick(s, e)
{
    var row = s.GetRow( e.visibleIndex );
    
    var keyValue = row.getAttribute("key");
    
    ShowForm("Forms/Racheti/Racheti_Edit.ascx", "", keyValue);
}
</script>

<table style="width:100%;" cellpadding="10" cellspacing="10">
<tr>
<td align="left">
    <dxwgv:ASPxGridView id="���������������" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid" KeyFieldName="���" Width="100%" OnHtmlRowCreated="���������������_HtmlRowCreated">
    <Settings ShowGroupPanel="True" ShowGroupedColumns="True"></Settings>
        <Columns>
            <dxwgv:GridViewDataTextColumn Caption="���" FieldName="���" Name="column_���" VisibleIndex="0">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="������������" FieldName="������������" Name="column_������������"
                VisibleIndex="1">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn Caption="������" FieldName="������" Name="column_������"
                VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
        </Columns>
        <SettingsBehavior AllowFocusedRow="True" ColumnResizeMode="NextColumn" />
        <ClientSideEvents RowDblClick="OnRowDblClick" />
        <SettingsCookies CookiesID="grid_Rascheti_List" Enabled="True" />
    </dxwgv:ASPxGridView>
</td>
</tr>
<tr>
    <td align="left" style="font-size:10px">
        *������ ������ ����������� ������� ������� �� ��������� ������.
    </td>
</tr>
</table>


