<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Analytics_AnalyticExtracts_Setup" Codebehind="AnalyticExtracts_Setup.ascx.cs" %>

<%@ Register Assembly="RadComboBox.NET2" Namespace="Telerik.WebControls" TagPrefix="radcb" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="¬ебядро" Namespace="Ѕарс.¬ебядро.»нтерфейс" TagPrefix="Bars" %>

<script type="text/javascript">
        
function RedirectWindow(Url)
{
    var oWnd = GetRadWindow();
    
    oWnd.SetUrl( "MainForm.aspx?ID=" + oWnd.Id + "&Form=" + escape(Url) );
    
    oWnd.Maximize();
}

function ChooseComponent(SessionParam, Params)
{
    var oWnd = GetRadWindow();
    
    ShowForm( 'Forms/Analytics/ReportPeriods_List.ascx', SessionParam, Params );
}
</script>

<radA:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1">
    <AjaxSettings>
        <radA:AjaxSetting AjaxControlID="¬ыпадающий—писок_отчетныеѕериоды">
            <UpdatedControls>
                <radA:AjaxUpdatedControl ControlID="Ќадпись_предыдуща€—борка" />
                <radA:AjaxUpdatedControl ControlID=" нопка_посмотретьѕредыдущую—борку" />
            </UpdatedControls>
        </radA:AjaxSetting>
    </AjaxSettings>
</radA:RadAjaxManager>

<br />
<div align="center">
    <table width="650px">
        <tr>
	        <td align="left" width="150px">
	            јналитическа€ выборка:
	        </td>
	        <td colspan="3" align="left">
	            <Bars:Ќадпись ID="Ќадпись_аналитическа€¬ыборка" runat="server"></Bars:Ќадпись>
	        </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
	            ќтчетный период:
	        </td>
	        <td align="left" width="300px">
                <Bars:¬ыпадающий—писок ID="¬ыпадающий—писок_отчетныеѕериоды" runat="server" ShowToggleImage="True" Width="300px" 
                    AutoPostBack="true" OnSelectedIndexChanged="¬ыпадающий—писок_отчетныеѕериоды_SelectedIndexChanged">
                </Bars:¬ыпадающий—писок>
	        </td>
	        <td width="150px" colspan="2" align="left">
	            <Bars:‘лажок ID="‘лажок_показать¬се" runat="server" AutoPostBack="true" “екст="ѕоказывать все" OnCheckedChanged="‘лажок_показать¬се_CheckedChanged" />
	        </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
	        <td align="left">
	            ѕредыдуща€ сборка:
	        </td>
	        <td colspan="3" align="left">
	            <Bars:Ќадпись ID="Ќадпись_предыдуща€—борка" runat="server"></Bars:Ќадпись>
	        </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
	        <td colspan="4" align="right">
		        <Bars: нопка runat="server" Text="ѕред.сборка" ID=" нопка_посмотретьѕредыдущую—борку" “ип нопки="ƒругое" OnClick=" нопка_посмотретьѕредыдущую—борку_Click" />
		        <Bars: нопка runat="server" Text="—обрать" ID=" нопка_собрать" “ип нопки="ƒругое" OnClick=" нопка_собрать_Click" />		
		        <Bars: нопка runat="server" Text="ќтмена" ID=" нопка_отмена" “ип нопки="ƒругое" OnClick=" нопка_отмена_Click" />		
		    </td>
        </tr>
    </table>
</div>
