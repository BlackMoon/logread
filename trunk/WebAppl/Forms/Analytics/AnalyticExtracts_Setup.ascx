<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Analytics_AnalyticExtracts_Setup" Codebehind="AnalyticExtracts_Setup.ascx.cs" %>

<%@ Register Assembly="RadComboBox.NET2" Namespace="Telerik.WebControls" TagPrefix="radcb" %>
<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

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
        <radA:AjaxSetting AjaxControlID="����������������_���������������">
            <UpdatedControls>
                <radA:AjaxUpdatedControl ControlID="�������_����������������" />
                <radA:AjaxUpdatedControl ControlID="������_��������������������������" />
            </UpdatedControls>
        </radA:AjaxSetting>
    </AjaxSettings>
</radA:RadAjaxManager>

<br />
<div align="center">
    <table width="650px">
        <tr>
	        <td align="left" width="150px">
	            ������������� �������:
	        </td>
	        <td colspan="3" align="left">
	            <Bars:������� ID="�������_��������������������" runat="server"></Bars:�������>
	        </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td align="left">
	            �������� ������:
	        </td>
	        <td align="left" width="300px">
                <Bars:���������������� ID="����������������_���������������" runat="server" ShowToggleImage="True" Width="300px" 
                    AutoPostBack="true" OnSelectedIndexChanged="����������������_���������������_SelectedIndexChanged">
                </Bars:����������������>
	        </td>
	        <td width="150px" colspan="2" align="left">
	            <Bars:������ ID="������_�����������" runat="server" AutoPostBack="true" �����="���������� ���" OnCheckedChanged="������_�����������_CheckedChanged" />
	        </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
	        <td align="left">
	            ���������� ������:
	        </td>
	        <td colspan="3" align="left">
	            <Bars:������� ID="�������_����������������" runat="server"></Bars:�������>
	        </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
	        <td colspan="4" align="right">
		        <Bars:������ runat="server" Text="����.������" ID="������_��������������������������" ���������="������" OnClick="������_��������������������������_Click" />
		        <Bars:������ runat="server" Text="�������" ID="������_�������" ���������="������" OnClick="������_�������_Click" />		
		        <Bars:������ runat="server" Text="������" ID="������_������" ���������="������" OnClick="������_������_Click" />		
		    </td>
        </tr>
    </table>
</div>
