<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_FileImport_FileImport" Codebehind="FileImport.ascx.cs" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>
<%@ Register Assembly="RadUpload.Net2" Namespace="Telerik.WebControls" TagPrefix="radU" %>

<script type="text/javascript">
function CloseAndRebindSheets()
{
    GetRadWindow().BrowserWindow.RebindSheets();
    
    Close();
}
</script>

<table width="300px">
    <tr>
        <td align="left">
            <radU:RadUpload ID="RadUpload_����" runat="server" Language="ru-RU" ControlObjectsVisibility="None" LocalizationPath="~/Resources/RadControls/Upload/Localization" RadControlsDir="~/Resources/RadControls/" Skin="WebBlue" OverwriteExistingFiles="True" TargetFolder="Upload" Width="300px" Height="25px"/>
        </td>
    </tr>
    <tr>
        <td align="right">
            <Bars:������ ID="������_OK" runat="server" Text="OK" OnClick="������_OK_Click" ���������="��" />
        </td>
    </tr>
</table>