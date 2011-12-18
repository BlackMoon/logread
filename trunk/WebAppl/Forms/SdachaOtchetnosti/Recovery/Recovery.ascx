<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_SdachaOtchetnosti_Recovery_Recovery" Codebehind="Recovery.ascx.cs" %>
<%@ Register Assembly="ВебЯдро" Namespace="Барс.ВебЯдро.Интерфейс" TagPrefix="Bars" %>

<script type="text/javascript" src="Resources/Scripts/FPSpread.js"></script>

<table width="100%" id="main_Table">
    <tr>
        <td>
            <Bars:Таблица ID="Таблица_элементы" runat="server" style="margin-left:5px;" Width="98%" AllowPaging="false">
            </Bars:Таблица>
        </td>
    </tr>
    <tr>
        <td align="right" style="height: 26px">
            <Bars:Кнопка runat="server" Text="Восстановить" ID="Кнопка_ОК" ТипКнопки="Другое" OnClick="Кнопка_ОК_Click" />
            <Bars:Кнопка runat="server" Text="Отмена" ID="Кнопка_Отмена" ТипКнопки="Отмена" OnClientClick="Close();" />
        </td>
    </tr>
</table>

