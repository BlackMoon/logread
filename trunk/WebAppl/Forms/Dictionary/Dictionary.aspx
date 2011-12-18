<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dictionary.aspx.cs" Inherits="WebAppl.Forms.Dictionary.Dictionary" %>

<%@ Register Assembly="DevExpress.Web.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxCallback" TagPrefix="dxcb" %>

<%@ Register Assembly="ВебЯдро" Namespace="Барс.ВебЯдро.Интерфейс" TagPrefix="Bars" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="rad" %>
<%@ Register Assembly="RadTreeView.Net2" Namespace="Telerik.WebControls" TagPrefix="radT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    
    <link rel='Stylesheet' type='text/css' href='~/Resources_Design/Common/Common.css'/>
    
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
    
    <script type="text/javascript" src="~/Resources/Scripts/Windows.js"></script>
    <script type="text/javascript" src="~/Resources/Scripts/FPSpread.js"></script>
    <script type="text/javascript">
         function ClosePageAndReturnValues()
         {
            var rTree = document.getElementById("tree");
            var outputValue = "";
           
            for (var i=0; i<rTree.AllNodes.length;i++)   
            {         
                 if ( rTree.AllNodes[i].Checked)
               
                {
                    outputValue = outputValue+ rTree.AllNodes[i].Value+"|"; 
                }
            }
            
            if (outputValue.length !=0)
            {
               outputValue = outputValue.substring(0,outputValue.length-1);
            }
            GetRadWindow().BrowserWindow.SetSheetValue(outputValue);
            Close();
        }     
   
        function BeforeClientDoubleClickHandler(node)
        {
            if( node.Attributes["Disable"] != null )
            {
                alert('Выбор данного элемента запрещен!');
                return false;
            }
            
            var outputValue = node.Value;
             
            CallBack.PerformCallback( escape(node.Text) );
            
            GetRadWindow().BrowserWindow.SetSheetValue(outputValue);
        }
        
        function PadRight(TotalWidth,InputString)
        {
	        var out = "";
	        if (InputString.length > TotalWidth)
	        {
		        return InputString.substring(0,3) ;	 
	        }
	        else
	        {
		        out = InputString;
		        for (j=0;j<TotalWidth-InputString.length;j++)
		        {
		    	    out = out + '0';
		        }
		        return out;
	        }
        }   
     
        function FormatForF110FO(ActivityType,Account)
        {
            var out = "";
	        out = ActivityType + PadRight(8,Account);
		    return out;
        }  
        
        
        function FormatForF110(ActivityType,Account)
        {
            var out = "";
            if ((Account.indexOf("30404",0) >= 0) | (Account.indexOf("30405",0) >= 0) 
            | ( Account.indexOf("21002",0) >= 0))
            {
                if (Account.length >= 5)
                {
                    out = ActivityType + Account.substring(0,5) + "000";     
                }
            }
            else
            {
				    out = ActivityType + PadRight(8,Account);
            }
		    return out;
        }
    </script>   
</head>

<body style="text-align:left">
    <form id="form_main" runat="server" style="overflow:auto;width:100%;height:100%" >

        <table style="overflow:visible">
            <tr> 
                <td>
                     <radT:RadTreeView style="vertical-align:top; text-align:left;" Width="100%" id="tree" LoadingMessage="(Загрузка...)" 
                         OnNodeExpand="Дерево_справочника_NodeExpand" BeforeClientDoubleClick="BeforeClientDoubleClickHandler" 
                         Skin="WebBlue" RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="false" AutoPostBack="False" runat="server" ExpandDelay="0">
                     </radT:RadTreeView>
                     
                    <!-- скрипт динамический, внутри панели ибо аякс !-->
                    <script type="text/javascript">
                    function FormatValue(node)
                    {
                        <asp:Literal ID="JSHolder" runat="server"></asp:Literal>
                        BeforeClientDoubleClickHandler(node);
                    }
                    </script>
                </td>
            </tr>
            <tr>
                <td style="text-align:right">
                  <Bars:Кнопка runat="server" Text="Выбрать" ID="Кнопка_выбрать" Visible="False" Width="110px"
                    OnClientClick="ClosePageAndReturnValues()" /> 
                </td>
            </tr>
        </table>      
        
        <dxcb:ASPxCallback ID="CallBack" runat="server" ClientInstanceName="CallBack" OnCallback="CallBack_Callback">
            <ClientSideEvents EndCallback="function(s, e) {
	Close();
}" />
        </dxcb:ASPxCallback>
        
    </form> 
</body>
</html>
