<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_FileImport_FileImport" Codebehind="FileImport.ascx.cs" %>
<%@ Register Assembly="¬ебядро" Namespace="Ѕарс.¬ебядро.»нтерфейс" TagPrefix="Bars" %>
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
            <radU:RadUpload ID="RadUpload_файл" runat="server" Language="ru-RU" ControlObjectsVisibility="None" LocalizationPath="~/Resources/RadControls/Upload/Localization" RadControlsDir="~/Resources/RadControls/" Skin="WebBlue" OverwriteExistingFiles="True" TargetFolder="Upload" Width="300px" Height="25px"/>
        </td>
    </tr>
    <tr>
        <td align="right">
            <Bars: нопка ID=" нопка_OK" runat="server" Text="OK" OnClick=" нопка_OK_Click" “ип нопки="ќк" />
        </td>
    </tr>
</table>