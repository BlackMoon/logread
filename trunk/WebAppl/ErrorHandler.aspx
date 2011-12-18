<%@ Page Language="C#" AutoEventWireup="true" Inherits="ErrorHandler" Codebehind="ErrorHandler.aspx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<%@ Register Assembly="DevExpress.Web.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxNavBar" TagPrefix="dxnb" %>

<%@ Register Assembly="DevExpress.Web.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>

<%@ Register Assembly="DevExpress.Web.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head ID="HtmlPageHead" runat="server">
    <title>��������� �� �������������� �������� �������</title>
</head>
<body class="BarsBody" style="width:100%;height:100%">
    <form id="form1" runat="server" style="width:100%;height:100%">
	    <table style="width:100%;height:100%">
	        <tr>
	            <td valign="center" align="center">
                    <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="200px" HeaderText="" ShowHeader="False">
                        <PanelCollection>
                            <dxp:PanelContent runat="server" _designerRegion="0">
                                <table width="550">
		                            <tr>
			                            <td width="150">
				                            <img align="top" src="Resources/Images/Common/error_handler.png"/>
			                            </td>
			                            <td align="left" width="400">
				                            <font color="darkred" size="+1"><b>� ������� �������� �������������� �������������� ��������</b></font>
				                            <br />
				                            <hr />
				                            <br />
				                            � ������� �������� <b>���������������</b> �������������� ��������, � �� ����������� � ��������� 
				                            �� �������������. �� �������� ��������� �� <b>��������� ����������</b>, ��������� ��� ������ �������.
                            				
				                            <br />
				                            <hr />
			                            </td>
		                            </tr>
		                        </table>
                                <dxnb:ASPxNavBar ID="navBar_detail" runat="server" Width="100%" ClientInstanceName="navbarDetail">
                                    <Groups>
                                        <dxnb:NavBarGroup Text="���������" Expanded="False">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td id="content" runat="server" align="left">
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </dxnb:NavBarGroup>
                                    </Groups>
                                    <ClientSideEvents Init="function(s, e) {
	s.CollapseAll();
}" />
                                </dxnb:ASPxNavBar>
		                    </dxp:PanelContent>
                        </PanelCollection>
                    
                    </dxrp:ASPxRoundPanel>
		        </td>
		    </tr>
    </table>
    </form>
</body>
</html>
