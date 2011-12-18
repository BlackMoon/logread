<%@ Page Language="C#" AutoEventWireup="true" Inherits="_Login" ValidateRequest="false" Codebehind="Login.aspx.cs" %>

<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadInput.Net2" Namespace="Telerik.WebControls" TagPrefix="radI" %>
<%@ Register Assembly="ВебЯдро" Namespace="Барс.ВебЯдро.Интерфейс" TagPrefix="Bars" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript"> 
function CheckForIE()
{
    if (/MSIE (\d+\.\d+);/.test(navigator.userAgent))
    {
        var ieversion = new Number(RegExp.$1)
        
        if (ieversion < 7)
        {
            window.location.href = 'BrowserUpdates.aspx';
        }
    }
}
</script>

<head id="HtmlHeadPage" runat="server">
    <title>WEB-СВОДЫ - авторизация в системе</title>
    
    <link rel='Stylesheet' type='text/css' href='Resources_Design/Common/Common.css'/>
    <link rel='Stylesheet' type='text/css' href='#ThemePath#/Login.css'/>
</head>

<body onload="CheckForIE();" class="LoginPageBody">
    <form id="main" runat="server" style="width:100%;height:100%">
        <radA:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1" EnablePageHeadUpdate="false" UseEmbeddedScripts="false" RadControlsDir="~/Resources/RadControls/" >
	        <AjaxSettings>
                <radA:AjaxSetting AjaxControlID="Button_Enter">
                    <UpdatedControls>
                        <radA:AjaxUpdatedControl ControlID="ResultsFields" LoadingPanelID="AjaxLoadingPanel1" />
                    </UpdatedControls>
                </radA:AjaxSetting>
	        </AjaxSettings>
	    </radA:RadAjaxManager>
    	
	    <radA:AjaxLoadingPanel ID="AjaxLoadingPanel1" runat="server">
	        <div>
	            <img src="Resources/Images/Common/Splash.gif" alt="Загрузка"/>
	            <span style="font-size:14px;">Подождите. Выполняется вход в систему..</span>
	        </div>
	    </radA:AjaxLoadingPanel>
    	
	    <table style="width:100%;height:100%">
	        <tr>
	            <td align="center">
	                <table id="Login_Fields" class="LoginPanelBackground" runat="server">
                        <tr class="LoginRow1">
                            <td colspan="2" ></td>
                        </tr>
                        <tr class="LoginRow2">
                            <td align="right" class="LoginCol1">
                            </td>
                            <td align="left" class="LoginCol2">
                                <input type="text" id="tb_User_text" runat="server" class="TextBox" tabindex="1" size="15"/>
                            </td>
                        </tr>
                        <tr class="LoginRow3">
                            <td align="right" class="LoginCol1">
                            </td>
                            <td align="left" class="LoginCol2">
                                <input type="password" id="tb_Pass_text" runat="server" class="TextBox" tabindex="1" size="15"/>
                            </td>
                        </tr>
                        <tr class="LoginRow4">
                            <td style="text-align:center" colspan="2">
                                <asp:ImageButton ID="Button_Enter" runat="server" OnClick="Button_Enter_Click" AlternateText="Вход" TabIndex="4"/>
                            </td>
                        </tr>
                        <tr class="LoginRow5">
                            <td colspan="2" ></td>
                        </tr>
                    </table>
                    <br />
                     <table id="ResultsFields" runat="server" cellspacing="0" cellpadding="0" style="bottom:0;left:0;height:30px; color:Red;font-size:11px;" class="LoginPanelWidth">
                        <tr>
                            <td style="color:Red;font-size:14px;font-style:inherit;" align="center">
                                <asp:Label ID="ResultCaption" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="color:Red;font-size:11px;font-style:inherit" align="center">
                                <asp:Label ID="ResultText" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
	            </td>
	        </tr>
	    </table>
	</form>
</body>
</html>
