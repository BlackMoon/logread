<%@ Page Language="C#" AutoEventWireup="true" Inherits="MainForm" Theme="Office2003 Blue" EnableEventValidation="false" Codebehind="MainForm.aspx.cs" %>

<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadWindow.Net2" Namespace="Telerik.WebControls" TagPrefix="radW" %>
<%@ Register Assembly="¬ебядро" Namespace="Ѕарс.¬ебядро.»нтерфейс" TagPrefix="Bars" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="HtmlPageHead" runat="server">
    <title></title>
    <link rel='Stylesheet' type='text/css' href='Resources_Design/Common/Common.css'/>
</head>
<body>
        
    <script type="text/javascript" src="Resources/Scripts/TextEdit.js"></script>
    <script type="text/javascript" src="Resources/Scripts/Grid.js"></script>
    <script type="text/javascript" src="Resources/Scripts/Cookie.js"></script>
    <script type="text/javascript" src="Resources/Scripts/ComboBox.js"></script>
    <script type="text/javascript" src="Resources/Scripts/ButtonEdit.js"></script>
    <script type="text/javascript" src="Resources/Scripts/DatePicker.js"></script>
    <script type="text/javascript" src="Resources/Scripts/Windows.js"></script>
    
    <form id="form_main" runat="server" style="overflow:auto;width:100%;height:100%" >
        <radW:RadWindowManager  ID="winManag" runat="server" Modal="True" RadControlsDir="~/Resources/RadControls/" 
            Skin="BarsGreen" OnClientClose="OnClientClose" InitialBehavior="Maximize" ShowContentDuringLoad="true" 
            ReloadOnShow="false" DestroyOnClose="true" UseEmbeddedScripts="false">
        </radW:RadWindowManager>
        
        <div runat="server" id="Div_PageHeader" class="BarsPageHeader">
        </div> 
        
        
    </form>
</body>
</html>
