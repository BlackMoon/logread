<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Default" Codebehind="Default.aspx.cs" %>

<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="ВебЯдро" Namespace="Барс.ВебЯдро.Интерфейс" TagPrefix="Bars" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
window.onbeforeunload = BeforeUnload;
window.onunload = Unload;

function BeforeUnload(evt)
{
    var RadWindowManager = GetRadWindowManager();
       
    var oActive = RadWindowManager.GetActiveWindow();

    if( oActive != null )
    {
		return '\nВсе несохраненные данные при этом будут утерены!\n';
    }
}

function Unload(evt)
{
    try
    {
        AjaxMethods.AbandSession();
    }
    catch(e)
    {
    }
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HtmlHeadPage" runat="server">
    <title>БАРС.Web-СВОДЫ</title>
    
    <link rel='Stylesheet' type='text/css' href='Resources_Design/Common/Common.css'/>
    <link rel='Stylesheet' type='text/css' href='#ThemePath#/Default.css'/>
    
    <script type="text/javascript" src="Resources/Scripts/Windows.js"></script>
    <script type="text/javascript" src="Resources/Scripts/MainMenu.js"></script>
</head>
<body class="DefaultPageBody">
    <form id="form1" runat="server" style="height:100%; width:100%;">
    
    <radA:RadAjaxPanel runat="server" ID="TimerPanel" EnableOutsideScripts="True" EnablePageHeadUpdate="false" RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="false">
		<radA:RadAjaxTimer ID="RadAjaxTimer_Messages" runat="server" InitialDelayTime="10000" Interval="60000" OnTick="RadAjaxTimer_Messages_Tick" />
		<asp:Panel ID="Panel_AdminMessages" runat="server"></asp:Panel>
	</radA:RadAjaxPanel>
    
    <div align="center" style="width:100%;height:100%;overflow:auto;" class="mainDiv" >
	    <table id="Table_main" runat="server" style="width:98%;height:98%;padding:1%">
            <tr style="height:55px">
                <td style="width:40px">
                    <a onclick="ShowURL('Forms/Profile/Profile.ascx')">
                        <img runat="server" id="Img_Profile" style="cursor:pointer" alt="Профиль"/>
                    </a>
                </td>
                <td style="width:140px;font-size:16px;font-weight:bold;" class="CaptionText">
                    Мой профиль
                </td>
                <td style="width:45px">
                    <a onclick="ShowURL('Forms/Uchrejdenie/Uchrejdenie.ascx')">
                        <img runat="server" id="Img_Uchrejdenie" style="cursor:pointer" alt="Мое учреждение"/>
                    </a>
                </td>
                <td style="width:160px;font-size:16px;font-weight:bold;" class="CaptionText">
                    Мое учреждение
                </td>
                <td runat="server" id="Row_Racheti" style="width:45px">
                    <a onclick="ShowURL('Forms/Racheti/Racheti_List.ascx')">
                        <img runat="server" id="Img_Racheti" style="cursor:pointer" alt="Пользователи" src="Resources_Design/Common/Images/Raschety.png"/>
                    </a>
                </td>
                <td runat="server" id="Td2" style="width:80px;font-size:16px;font-weight:bold;text-align:left;padding-left:10px" class="CaptionText">
                    Расчеты
                </td>
                <td runat="server" id="Row_Users" style="width:45px">
                    <a onclick="ShowURL('Forms/LoggedUsers/LoggedUsers_List.ascx')">
                        <img runat="server" id="Img_Users" style="cursor:pointer" alt="Пользователи"/>
                    </a>
                </td>
                <td runat="server" id="Row_Users2" style="width:250px;font-size:16px;font-weight:bold;text-align:left;padding-left:10px" class="CaptionText">
                    Работающие пользователи
                </td>
	            <td id="UserInfo" align="right" style="font-size:15px;padding-right:20px" class="CaptionText">
	            </td>
	            <td align="right" style="width:110px;">
	                <asp:ImageButton ID="ButtonExit" Width="90px" Height ="44px" OnClick="ImageButton_EndSession_Click" AlternateText="выход" runat = "server" />
	            </td>
	        </tr>
	        <tr style="height:70%">
	            <td align="center" style="vertical-align:middle" colspan="10">
	                <table id="Table_center" style="text-align:center;height:90%;">
	                    <tr style="vertical-align:top">
	                        <td colspan="3" align="center">
	                            <img id="Img_Caption" style="" alt="" runat="server" />
	                        </td>
	                    </tr>
	                    <tr style="height:50px;">
	                        <td colspan="3" align="center">
	                            <br />
	                        </td>
	                    </tr>
	                    <tr>
	                        <td>
	                            <a onclick="ShowURL('Forms/SdachaOtchetnosti/TekuschieOtchetnieFormi/TekuschieOtchetnieFormi_List.ascx')">
	                                <img id="Img_forms" style="cursor:pointer" alt="Отчетные формы" runat="server" />
	                            </a>
	                        </td>
	                        <td>
	                        </td>
	                        <td>
	                            <a onclick="ShowURL('Forms/Analytics/AnalyticExtracts_List.ascx')">
	                                <img id="Img_analitic" style="cursor:pointer" alt="Аналитические выборки" runat="server" />
	                            </a>
	                        </td>
	                    </tr>
	                    <tr>
	                        <td style="font-size:18px;font-weight:bold;padding-top:10px;" class="CaptionText">
	                            Отчетные формы
	                        </td>
	                        <td style="">
	                        </td>
	                        <td style="font-size:18px;font-weight:bold;padding-top:10px;" class="CaptionText">
	                            Аналитические выборки
	                        </td>
	                    </tr>
	                    <tr style="height:25%;">
	                        <td>
	                        </td>
	                    </tr>
	                </table>
                 </td>    
	        </tr>
	        <tr style="height:80px">
	            <td colspan="10" valign="bottom">
	                <table id="Table_bottom" style="height:80px;" align="right">
                        <tr>
                            <td align="right" style="">
                                <img id="BarsLogo" alt="Барс-Груп" src="Resources_Design/Common/Images/BarGroupLogo.jpg"/>
                            </td>
                            <td align="right" style="padding-left:15px;">
                                <img id="PlatformLogo" alt="Платформа БАРС" src="Resources_Design/Common/Images/platforma_logo.png"/>
                            </td>
                            <td align="right" style="padding-left:15px;">
                                <img alt="Oracle" src="Resources_Design/Common/Images/oracle_logo.gif"/>
                            </td>
                        </tr>
                    </table>
                </td>
	        </tr>
	    </table>
	</div>
	
	<div>
        <radW:RadWindowManager ID="RadWindowManager1" runat="server" RadControlsDir="~/Resources/RadControls/" Skin="BarsBlue" 
            InitialBehavior="Maximize" OnClientClose="OnClientClose" MinimizeZoneId="Window_MinimizeZone" MinimizeMode="MinimizeZone" 
            ReloadOnShow="false" ShowContentDuringLoad="true" DestroyOnClose="true" UseEmbeddedScripts="false">
        </radW:RadWindowManager>
    </div>
    
    </form>
</body>
</html>
