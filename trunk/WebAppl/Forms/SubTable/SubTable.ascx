<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_SubTable_SubTable" Codebehind="SubTable.ascx.cs" %>
<%@ Register Assembly="RadMenu.Net2" Namespace="Telerik.WebControls" TagPrefix="radM" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="¬ебЅраузерќтчетных‘орм" Namespace="Ѕарс.—воды.¬ебЅраузерќтчетных‘орм"
    TagPrefix="BarsForms" %>
<%@ Register Assembly="¬ебядро" Namespace="Ѕарс.¬ебядро.»нтерфейс" TagPrefix="Bars" %>

<script type="text/javascript" src="Resources/Scripts/FPSpread.js"></script>

<script type="text/javascript">
function DeleteRow()
{
    var table = document.getElementById('ctl02_Ќабор“аблиц‘ормы_субтаблица');
    
    table.CallBack( 'DeleteRow' );
}

function ShowCompareForm( SessionParam )
{
    ShowForm( 'Forms/MacroProcessing/CompareResult.ascx', SessionParam , '' );
}

function InsertRow()
{
    var table = document.getElementById('ctl02_Ќабор“аблиц‘ормы_субтаблица');
 
    table.CallBack( 'InsertRow' );
}

function SetSheetValue(Value)
{
    var table = document.getElementById('ctl02_Ќабор“аблиц‘ормы_субтаблица');
 
    table.EndEdit();
    
    var row = table.ActiveRow;
	
    if( row == null )
    {
        row = table.GetActiveRow();
    }
	
    var col = table.ActiveCol;
    
    if( col == null )
    {
        col = table.GetActiveCol();
    }
    
    table.SetValue(row, col, Value, false );
}

function SaveSubTableData()
{
    var subtable = document.getElementById('ctl02_Ќабор“аблиц‘ормы_субтаблица');
    
    subtable.UpdatePostbackData();
    
    subtable.CallBack( 'SaveData' );
}

function CloseFormAndShowLog( SessionParam )
{
    var wind = GetRadWindow().BrowserWindow;
    
    wind.ShowForm( 'Forms/MacroProcessing/CompareResult.ascx', SessionParam , '' );
    
    Close();
     
    wind.RebindSheets();
}
</script>

<radM:RadMenu ID="RadMenu_main" runat="server" IsContext="True" RadControlsDir="~/Resources/RadControls/" Skin="Outlook" ContextMenuElementID="ctl02_Ќабор“аблиц‘ормы_субтаблица_view">
    <Items>
        <radM:RadMenuItem ID="RadMenuItem1" runat="server" Text="ƒобавить" NavigateUrl="javascript:InsertRow();">
        </radM:RadMenuItem>
        <radM:RadMenuItem ID="RadMenuItem2" runat="server" Text="”далить" NavigateUrl="javascript:DeleteRow();">
        </radM:RadMenuItem>
    </Items>
</radM:RadMenu>
            
<table width="100%" id="main_Table">
    <tr>
        <td colspan="2">
            <BarsForms:Ќаборƒинамических“аблиц‘ормы style="margin-left:10px;" ID="Ќабор“аблиц‘ормы_субтаблица" runat="server" Height="500px" Width="98%"
                    VerticalScrollBarPolicy="Always" HorizontalScrollBarPolicy="Always">
            </BarsForms:Ќаборƒинамических“аблиц‘ормы>
        </td>
    </tr>
    <tr style="height: 26px">
        <td align="left">
            <Bars: нопка runat="server" Text="ѕересчитать" ID=" нопка_ѕересчитать" “ип нопки="ƒругое" OnClick=" нопка_ѕересчитать_Click"/>
        </td>
        <td align="right">
            &nbsp;<Bars: нопка runat="server" Text="ќ " ID=" нопка_ќ " “ип нопки="ќк" OnClick=" нопка_ќ _Click" />
            <Bars: нопка runat="server" Text="ќтмена" ID=" нопка_ќтмена" “ип нопки="ќтмена" OnClientClick="Close();" />
        </td>
    </tr>
</table>

