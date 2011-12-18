<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Forms_SdachaOtchetnosti_TekuschieOtchetnieFormi_TekuschieOtchetnieFormi_View" Codebehind="TekuschieOtchetnieFormi_View.ascx.cs" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadMenu.Net2" Namespace="Telerik.WebControls" TagPrefix="radM" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>
<%@ Register Assembly="����������������������" Namespace="����.�����.����������������������" TagPrefix="BarsSvody" %>

<script type="text/javascript" src="Resources/Scripts/FPSpread.js"></script>

<script type="text/javascript">
function AskForClose(text)
{
    if( !confirm( text ) )
    {
        Close();
    }
}

function init() 
{  
    var ss = document.getElementById("ctl02_����������������_����������������"); 

    if (document.all) { 

        ss.onCallBackStart = CallBackStart; 

        ss.onCallBackStopped = CallBackStopped; 

    } else { 

        ss.addEventListener("CallBackStart", CallBackStart, false); 

        ss.addEventListener("CallBackStopped", CallBackStopped, false); 

    }
}

function CallBackStart(event) 
{ 
    if (event==null) 
        event = window.event;
        
    var waitmsg = document.getElementById("waitmsg");     
    waitmsg.style["display"] = "inline";
}

function CallBackStopped() 
{ 
    var waitmsg = document.getElementById("waitmsg");
    waitmsg.style["display"] = "none";
}

function DeleteRow()
{
    var table = document.getElementById('ctl02_����������������_����������������');
    
    table.CallBack( 'DeleteRow' );
}

function InsertRow()
{
    var table = document.getElementById('ctl02_����������������_����������������');
 
    table.CallBack( 'InsertRow' );
}

function ClientTick(sender, eventArgs)
{
    var ctl01_Panel_AutoSave = document.getElementById("ctl02_Panel_AutoSave");
   
    ctl01_Panel_AutoSave.textContent = "���� ������� ���������� �����";
    
    var RadWindowManager = GetRadWindowManager();
       
    var oActive = RadWindowManager.GetActiveWindow();
    
    if( oActive != null )
    {
        if( oActive.GetContentFrame().contentWindow.SaveSubTableData != null )
        {
            oActive.GetContentFrame().contentWindow.SaveSubTableData();
        }
    }
}

function SetSheetValue(Value)
{
    if( ActiveSheetViewID == null )
    {
        ActiveSheetViewID = 'ctl02_����������������_����������������';
    }

    var table = document.getElementById(ActiveSheetViewID);
 
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

function CloseWithUpdate()
{
    var radWindow = GetRadWindow();

    Close();
    
    radWindow.BrowserWindow.AjaxRequestClickTreeView();
}

function ShowCompareForm( SessionParam )
{
    ShowForm( 'Forms/MacroProcessing/CompareResult.ascx', SessionParam , '' );
}

</script>

<radM:RadMenu ID="RadMenu_main" runat="server" IsContext="True" RadControlsDir="~/Resources/RadControls/" Skin="Outlook" ContextMenuElementID="ctl02_����������������_����������������_view">
    <Items>
        <radM:RadMenuItem ID="RadMenuItem1" runat="server" Text="��������" NavigateUrl="javascript:InsertRow();">
        </radM:RadMenuItem>
        <radM:RadMenuItem ID="RadMenuItem2" runat="server" Text="�������" NavigateUrl="javascript:DeleteRow();">
        </radM:RadMenuItem>
    </Items>
</radM:RadMenu>

<table id="main_Table" width="100%">
    <tr>
        <td colspan="3">
            <radM:RadMenu ID="RadMenu_�������" runat="server" Width="100%"
                            RadControlsDir="~/Resources/RadControls/" Skin="Default2006" 
                            SkinsPath="~/Resources/RadControls/Menu/Skins" CollapseDelay="200" OnItemClick="RadMenu_�������_ItemClick">
                <Items>
                    <radM:RadMenuItem ID="RadMenuItem_�������" runat="server" Text="�������" ImageUrl="~/Resources/Images/BrowserMenu/FormulaEvaluator.png">
                        <Items>
                            <radM:RadMenuItem ID="RadMenuItem_����������������" runat="server" Text="��������� ��������������� ������">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_����������������" runat="server" Text="��������� ������������ ������">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_�������" runat="server" Text="������� �����">
                                <Items>
                                    <radM:RadMenuItem ID="RadMenuItem_��������������" runat="server" Value="�������������������" Text="������� ������� �����" >
                                    </radM:RadMenuItem>
                                </Items>
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_�����������" runat="server" Text="����� ������" Visible="false">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_������������" runat="server" Text="������ ������">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_�������������" runat="server" Text="������� ������" Value="�������������">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_��������������������" runat="server" Text="�������������� ������" Value="��������������">
                            </radM:RadMenuItem>
                        </Items>
                    </radM:RadMenuItem>
                    <radM:RadMenuItem ID="RadMenuItem_�������������" runat="server" Text="�������� �����" Value="�������������" ImageUrl="~/Resources/Images/BrowserMenu/Print.png">
                    </radM:RadMenuItem>
                    <radM:RadMenuItem ID="RadMenuItem_���������" runat="server" Text="���������" Value="���������" ImageUrl="~/Resources/Images/BrowserMenu/Attachment.png">
                    </radM:RadMenuItem>
                </Items>
            </radM:RadMenu>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <BarsSvody:���������������� runat="server" ID="����������������">
            </BarsSvody:����������������>
        </td>
    </tr>
    <tr>
        <td align="left" style="height: 26px">
            <Bars:������ runat="server" Text="�����������" ID="������_���������" ���������="������" OnClick="������_���������_Click" />
        </td>
        <td align="left">
            <radA:RadAjaxPanel runat="server" ID="TimerPanel" EnableOutsideScripts="True">
	            <radA:RadAjaxTimer ID="RadAjaxTimer_AutoSave" runat="server" InitialDelayTime="150000" Interval="300000" OnTick="RadAjaxTimer_AutoSave_Tick" OnClientTick="ClientTick"/>
	            <asp:Panel ID="Panel_AutoSave" runat="server"></asp:Panel>
            </radA:RadAjaxPanel>
        </td>
        <td align="right" style="height: 26px">
            <Bars:������ runat="server" Text="���������" ID="������_Save" OnClick="������_Save_Click" ���������="������" />
            <Bars:������ runat="server" Text="��������� � �������" ID="������_��" OnClick="������_��_Click" ���������="������" style="width:150px; background-image:url('./Resources_Design/Common/Images/button_wide.gif');" />
            <Bars:������ runat="server" Text="�������" ID="������_������" ���������="������" />
        </td>
    </tr>
</table>

<span id="waitmsg" style="border: 1px solid darkblue; position: absolute; background-color: LightYellow; height: 25px; width: 120px; top: 250px; display: none;">
    <img align="absmiddle" vspace="5" src="Resources/Images/Common/wait.gif"/>����������...
</span>

<script type="text/javascript">

init();

</script>