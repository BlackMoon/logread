<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Profile_Profile" Codebehind="Profile.ascx.cs" %>
<%@ Register Assembly="¬ебядро" Namespace="Ѕарс.¬ебядро.»нтерфейс" TagPrefix="Bars" %>

<table width="100%">
    <tr>
        <td style="width:200px;" align="right">
            »м€ пользовател€:
        </td>
        <td align="left">
            <Bars:ѕоле¬вода“екста runat="server" ID="ѕоле¬вода_Ќаименование" Width="300px" »м€ѕол€»сточникаƒанных="Ќаименование" ReadOnly="true" BackColor=LightGrey>
            </Bars:ѕоле¬вода“екста>
        </td>
    </tr>
    <tr>
        <td align="right">
            Ћогин:
        </td>
        <td align="left">
            <Bars:ѕоле¬вода“екста runat="server" ID="ѕоле¬вода_Ћогин" Width="300px" »м€ѕол€»сточникаƒанных="Ћогин" ReadOnly="true" BackColor=LightGrey>
            </Bars:ѕоле¬вода“екста>
        </td>
    </tr>
    <tr>
        <td align="right">
            —тарый пароль:
        </td>
        <td align="left">
            <Bars:ѕоле¬вода“екста runat="server" ID="ѕоле¬вода_—тарыйѕароль" Width="300px" »м€ѕол€»сточникаƒанных="—тарыйѕароль" TextMode="Password" Text="">
            </Bars:ѕоле¬вода“екста>
        </td>
    </tr>
    <tr>
        <td align="right">
            Ќовый пароль:
        </td>
        <td align="left">
            <Bars:ѕоле¬вода“екста runat="server" ID="ѕоле¬вода“екста_Ќовыйѕароль" Width="300px" »м€ѕол€»сточникаƒанных="Ќовыйѕароль" TextMode="Password" Text="">
            </Bars:ѕоле¬вода“екста>
        </td>
    </tr>
    <tr>
        <td align="right">
            ѕодтверждение парол€:
        </td>
        <td align="left">
            <Bars:ѕоле¬вода“екста runat="server" ID="ѕоле¬вода“екста_Ќовыйѕароль2" Width="300px" »м€ѕол€»сточникаƒанных="Ќовыйѕароль2" TextMode="Password" Text="">
            </Bars:ѕоле¬вода“екста>
        </td>
    </tr>
    <tr style="height: 26px">
        <td align="right" colspan="2">
            <Bars: нопка runat="server" Text="ќ " ID=" нопка_ќ " “ип нопки="ќк" OnClick=" нопка_ќ _Click" />
            <Bars: нопка runat="server" Text="ќтмена" ID=" нопка_ќтмена" “ип нопки="ќтмена" OnClientClick="Close();" />
        </td>
    </tr>
</table>