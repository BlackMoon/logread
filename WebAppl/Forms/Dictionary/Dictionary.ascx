<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Dictionary_Dictionary" Codebehind="Dictionary.ascx.cs" %>
<%@ Register Assembly="ВебЯдро" Namespace="Барс.ВебЯдро.Интерфейс" TagPrefix="Bars" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="RadTreeView.Net2" Namespace="Telerik.WebControls" TagPrefix="radT" %>

<style type="text/css">
    .Disabled
    {
        color:#77746d;
        font-family:Tahoma;
        font-size:8pt;
        padding-left:3px;
        text-decoration:none;
    }
</style>

<script language="javascript" type="text/javascript">
function BeforeClientDoubleClickHandler(node)
{
    if( node.Attributes["Disable"] != null )
    {
        alert('Выбор данного элемента запрещен!');
        return false;
    }

    GetRadWindow().BrowserWindow.SetSheetValue(node.Value);

    Close();
}
</script>

<rad:RadAjaxPanel id="RadAjaxPanel1" runat="server" enableoutsidescripts="True" loadingpanelid="AjaxLoadingPanel1" scrollbars="Vertical" Width="100%" Height="100%">
    <radT:RadTreeView style="OVERFLOW: inherit; height:500px; vertical-align:top; text-align:left;" Width="100%" id="tree" LoadingMessage="(Загрузка...)" 
        OnNodeExpand="Дерево_справочника_NodeExpand" BeforeClientDoubleClick="BeforeClientDoubleClickHandler" 
        Skin="WebBlue" RadControlsDir="~/Resources/RadControls/" AutoPostBack="False" runat="server">
    </radT:RadTreeView>
</rad:RadAjaxPanel>

<rad:AjaxLoadingPanel ID="AjaxLoadingPanel1" runat="server" Height="75px" Width="75px" Transparency="50">
    <asp:Image ID="Image1" runat="server" AlternateText="Загрузка..." ImageUrl="~/Resources/RadControls/Ajax/Skins/Default/LoadingProgressBar.gif" />
</rad:AjaxLoadingPanel>

