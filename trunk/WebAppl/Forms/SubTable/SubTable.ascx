<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_SubTable_SubTable" Codebehind="SubTable.ascx.cs" %>
<%@ Register Assembly="RadMenu.Net2" Namespace="Telerik.WebControls" TagPrefix="radM" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="����������������������" Namespace="����.�����.����������������������"
    TagPrefix="BarsForms" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

<script type="text/javascript" src="Resources/Scripts/FPSpread.js"></script>

<script type="text/javascript">
function DeleteRow()
{
    var table = document.getElementById('ctl02_����������������_����������');
    
    table.CallBack( 'DeleteRow' );
}

function ShowCompareForm( SessionParam )
{
    ShowForm( 'Forms/MacroProcessing/CompareResult.ascx', SessionParam , '' );
}

function InsertRow()
{
    var table = document.getElementById('ctl02_����������������_����������');
 
    table.CallBack( 'InsertRow' );
}

function SetSheetValue(Value)
{
    var table = document.getElementById('ctl02_����������������_����������');
 
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
    var subtable = document.getElementById('ctl02_����������������_����������');
    
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

<radM:RadMenu ID="RadMenu_main" runat="server" IsContext="True" RadControlsDir="~/Resources/RadControls/" Skin="Outlook" ContextMenuElementID="ctl02_����������������_����������_view">
    <Items>
        <radM:RadMenuItem ID="RadMenuItem1" runat="server" Text="��������" NavigateUrl="javascript:InsertRow();">
        </radM:RadMenuItem>
        <radM:RadMenuItem ID="RadMenuItem2" runat="server" Text="�������" NavigateUrl="javascript:DeleteRow();">
        </radM:RadMenuItem>
    </Items>
</radM:RadMenu>
            
<table width="100%" id="main_Table">
    <tr>
        <td colspan="2">
            <BarsForms:���������������������������� style="margin-left:10px;" ID="����������������_����������" runat="server" Height="500px" Width="98%"
                    VerticalScrollBarPolicy="Always" HorizontalScrollBarPolicy="Always">
            </BarsForms:����������������������������>
        </td>
    </tr>
    <tr style="height: 26px">
        <td align="left">
            <Bars:������ runat="server" Text="�����������" ID="������_�����������" ���������="������" OnClick="������_�����������_Click"/>
        </td>
        <td align="right">
            &nbsp;<Bars:������ runat="server" Text="��" ID="������_��" ���������="��" OnClick="������_��_Click" />
            <Bars:������ runat="server" Text="������" ID="������_������" ���������="������" OnClientClick="Close();" />
        </td>
    </tr>
</table>

