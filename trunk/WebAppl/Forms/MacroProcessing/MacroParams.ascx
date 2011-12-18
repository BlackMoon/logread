<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_MacroProcessing_MacroParams" Codebehind="MacroParams.ascx.cs" %>
<%@ Register Assembly="Âåáßäðî" Namespace="Áàðñ.Âåáßäðî.Èíòåðôåéñ" TagPrefix="Bars" %>

<script type="text/javascript" src="Resources/Scripts/FPSpread.js"></script>

<style type="text/css">
.ParamsTable
{
    border: solid 1px #72b0eb;
    width: 98%;
    height: 100%;
    text-align: left;
    margin : 4px;
    font-family : Tahoma;
    font-size : 12px;
}
</style>

<table cellspacing="0" cellpadding="1" class="ParamsTable">
    <tbody id="body_main" runat="server">
    </tbody>
    <tfoot style="height:100%">
        <tr style="height:50px">
            <td colspan="2">
            </td>
        </tr>
        <tr style="height:100%">
            <td colspan="2" align="right">
                <Bars:Êíîïêà runat="server" Text="OK" ID="Êíîïêà_ÎÊ" ÒèïÊíîïêè="Îê" OnClick="Êíîïêà_ÎÊ_Click"/>
                <Bars:Êíîïêà runat="server" Text="Îòìåíà" ID="Êíîïêà_Îòìåíà" ÒèïÊíîïêè="Îòìåíà" OnClientClick="Close();"/>
            </td>
        </tr>
    </tfoot>
</table>