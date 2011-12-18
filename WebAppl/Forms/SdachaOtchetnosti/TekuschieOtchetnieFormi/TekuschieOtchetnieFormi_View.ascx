<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="Forms_SdachaOtchetnosti_TekuschieOtchetnieFormi_TekuschieOtchetnieFormi_View" Codebehind="TekuschieOtchetnieFormi_View.ascx.cs" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadMenu.Net2" Namespace="Telerik.WebControls" TagPrefix="radM" %>
<%@ Register Assembly="¬ебядро" Namespace="Ѕарс.¬ебядро.»нтерфейс" TagPrefix="Bars" %>
<%@ Register Assembly="¬ебЅраузерќтчетных‘орм" Namespace="Ѕарс.—воды.¬ебЅраузерќтчетных‘орм" TagPrefix="BarsSvody" %>

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
    var ss = document.getElementById("ctl02_вебЁкранна€‘орма_набор“аблиц‘ормы"); 

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
    var table = document.getElementById('ctl02_вебЁкранна€‘орма_набор“аблиц‘ормы');
    
    table.CallBack( 'DeleteRow' );
}

function InsertRow()
{
    var table = document.getElementById('ctl02_вебЁкранна€‘орма_набор“аблиц‘ормы');
 
    table.CallBack( 'InsertRow' );
}

function ClientTick(sender, eventArgs)
{
    var ctl01_Panel_AutoSave = document.getElementById("ctl02_Panel_AutoSave");
   
    ctl01_Panel_AutoSave.textContent = "»дет попытка сохранени€ формы";
    
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
        ActiveSheetViewID = 'ctl02_вебЁкранна€‘орма_набор“аблицЎапка';
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

<radM:RadMenu ID="RadMenu_main" runat="server" IsContext="True" RadControlsDir="~/Resources/RadControls/" Skin="Outlook" ContextMenuElementID="ctl02_вебЁкранна€‘орма_набор“аблиц‘ормы_view">
    <Items>
        <radM:RadMenuItem ID="RadMenuItem1" runat="server" Text="ƒобавить" NavigateUrl="javascript:InsertRow();">
        </radM:RadMenuItem>
        <radM:RadMenuItem ID="RadMenuItem2" runat="server" Text="”далить" NavigateUrl="javascript:DeleteRow();">
        </radM:RadMenuItem>
    </Items>
</radM:RadMenu>

<table id="main_Table" width="100%">
    <tr>
        <td colspan="3">
            <radM:RadMenu ID="RadMenu_√лавное" runat="server" Width="100%"
                            RadControlsDir="~/Resources/RadControls/" Skin="Default2006" 
                            SkinsPath="~/Resources/RadControls/Menu/Skins" CollapseDelay="200" OnItemClick="RadMenu_√лавное_ItemClick">
                <Items>
                    <radM:RadMenuItem ID="RadMenuItem_функции" runat="server" Text="‘ункции" ImageUrl="~/Resources/Images/BrowserMenu/FormulaEvaluator.png">
                        <Items>
                            <radM:RadMenuItem ID="RadMenuItem_проверить¬нутриф" runat="server" Text="ѕроверить внутриформенные ув€зки">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_проверитьћежформ" runat="server" Text="ѕроверить межформенные ув€зки">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_сводна€" runat="server" Text="—водна€ форма">
                                <Items>
                                    <radM:RadMenuItem ID="RadMenuItem_собрать—водную" runat="server" Value="—обрать—водную‘орму" Text="—обрать сводную форму" >
                                    </radM:RadMenuItem>
                                </Items>
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_јрхивƒанных" runat="server" Text="јрхив данных" Visible="false">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_»мпортƒанных" runat="server" Text="»мпорт данных">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_Ёкспортƒанных" runat="server" Text="Ёкспорт данных" Value="Ёкспортƒанных">
                            </radM:RadMenuItem>
                            <radM:RadMenuItem ID="RadMenuItem_¬осстановлениеƒанных" runat="server" Text="¬осстановление данных" Value="¬осстановление">
                            </radM:RadMenuItem>
                        </Items>
                    </radM:RadMenuItem>
                    <radM:RadMenuItem ID="RadMenuItem_печатные‘ормы" runat="server" Text="ѕечатные формы" Value="ѕечатные‘ормы" ImageUrl="~/Resources/Images/BrowserMenu/Print.png">
                    </radM:RadMenuItem>
                    <radM:RadMenuItem ID="RadMenuItem_ќбработки" runat="server" Text="ќбработки" Value="ќбработки" ImageUrl="~/Resources/Images/BrowserMenu/Attachment.png">
                    </radM:RadMenuItem>
                </Items>
            </radM:RadMenu>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <BarsSvody:¬ебЁкранна€‘орма runat="server" ID="вебЁкранна€‘орма">
            </BarsSvody:¬ебЁкранна€‘орма>
        </td>
    </tr>
    <tr>
        <td align="left" style="height: 26px">
            <Bars: нопка runat="server" Text="ѕересчитать" ID=" нопка_автоблоки" “ип нопки="ƒругое" OnClick=" нопка_автоблоки_Click" />
        </td>
        <td align="left">
            <radA:RadAjaxPanel runat="server" ID="TimerPanel" EnableOutsideScripts="True">
	            <radA:RadAjaxTimer ID="RadAjaxTimer_AutoSave" runat="server" InitialDelayTime="150000" Interval="300000" OnTick="RadAjaxTimer_AutoSave_Tick" OnClientTick="ClientTick"/>
	            <asp:Panel ID="Panel_AutoSave" runat="server"></asp:Panel>
            </radA:RadAjaxPanel>
        </td>
        <td align="right" style="height: 26px">
            <Bars: нопка runat="server" Text="—охранить" ID=" нопка_Save" OnClick=" нопка_Save_Click" “ип нопки="ƒругое" />
            <Bars: нопка runat="server" Text="—охранить и закрыть" ID=" нопка_ќ " OnClick=" нопка_ќ _Click" “ип нопки="ƒругое" style="width:150px; background-image:url('./Resources_Design/Common/Images/button_wide.gif');" />
            <Bars: нопка runat="server" Text="«акрыть" ID=" нопка_ќтмена" “ип нопки="ќтмена" />
        </td>
    </tr>
</table>

<span id="waitmsg" style="border: 1px solid darkblue; position: absolute; background-color: LightYellow; height: 25px; width: 120px; top: 250px; display: none;">
    <img align="absmiddle" vspace="5" src="Resources/Images/Common/wait.gif"/>††«агрузка...
</span>

<script type="text/javascript">

init();

</script>