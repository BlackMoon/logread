<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Racheti_Edit.ascx.cs" Inherits="Racheti_Edit" %>
<%@ Register Assembly="DevExpress.Web.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="ВебЯдро" Namespace="Барс.ВебЯдро.Интерфейс" TagPrefix="Bars" %>

<script type="text/javascript">
function updateComponents(s, e)
{
    comboboxComp.PerformCallback("Update");
    
    if( comboboxComp.GetText() != "" )
	{
		calcButton.SetEnabled( true );
	}	
}

var CallBackName = "";

function OnCalcClick(s, e)
{
    gridRes.PerformCallback("Calc");
    CallBackName = "Calc";
}

function EndCalcCallBack(s, e)
{
    if( CallBackName == "Calc" )
    {
        saveButton.SetEnabled(true);
        CallBackName = "";
    }
    else if( CallBackName == "Save" )
    {
        saveButton.SetEnabled(false);
        CallBackName = "";
    }
}

function OnSaveClick(s, e)
{
    gridRes.PerformCallback("Save");
    CallBackName = "Save";
}
</script>

<table width="100%" id="main_Table" cellpadding="10" cellspacing="10">
    <tr>
        <td colspan="3" align="left">
            <dxrp:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" Width="100%" HeaderText="Параметры расчета">
                <PanelCollection>
                    <dxp:PanelContent runat="server" _designerRegion="0">
                        <table>
                            <tr>
                                <td align="right" style="width:200px;">
                                    Отчетный период:
                                </td>
                                <td align="left">
                                    <dxe:ASPxComboBox ID="combobox_ОтчетныйПериод" runat="server" Width="200px" ValueType="System.String" ValueField="ИдентификаторОбъектаСтрокой">
                                        <ClientSideEvents SelectedIndexChanged="updateComponents" />
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Компонент периода
                                </td>
                                <td align="left">
                                    <dxe:ASPxComboBox ID="combobox_компонент" ClientInstanceName="comboboxComp" runat="server" Width="200px" LoadingPanelText="Загрузка..." ValueType="System.String" ValueField="ИдентификаторОбъектаСтрокой" OnCallback="combobox_компонент_Callback">
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Только моё учреждение
                                </td>
                                <td align="left">
                                    <dxe:ASPxCheckBox ID="Флажок_ТолькоМое" runat="server" >
                                    </dxe:ASPxCheckBox>
                                </td>
                            </tr>
                        </table>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxrp:ASPxRoundPanel>
        </td>
    </tr> 
    <tr>
        <td colspan="3" align="right" style="height: 46px">
            <dxe:ASPxButton ID="ASPxButton_Calc" runat="server" ClientInstanceName="calcButton" Text="Расчитать" Width="100px" AutoPostBack="False" >
                <ClientSideEvents Click="OnCalcClick" Init="function(s, e) {
	calcButton.SetEnabled(false);
}" />
            </dxe:ASPxButton>
        </td>
    </tr>
    <tr>
        <td colspan="3" align="left">
            <dxrp:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" HeaderText="Расчитаные формы">
                <PanelCollection>
                    <dxp:PanelContent runat="server" _designerRegion="0">
                        <dxwgv:ASPxGridView ID="Таблица_Результат" runat="server" AutoGenerateColumns="False" Width="100%" OnCustomUnboundColumnData="Таблица_Результат_CustomUnboundColumnData" OnCustomCallback="Таблица_Результат_CustomCallback" ClientInstanceName="gridRes">
                            <SettingsPager Visible="False" PageSize="5"></SettingsPager>
                            <Columns>
                            <dxwgv:GridViewDataTextColumn Caption="Переменная" VisibleIndex="0" FieldName="столбец_Переменная" UnboundType="String"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Учреждение" VisibleIndex="1" FieldName="столбец_Учреждение" UnboundType="String"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Отчетная форма" VisibleIndex="2" FieldName="столбец_Форма" UnboundType="String"></dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewDataTextColumn Caption="Компонент" VisibleIndex="3" FieldName="столбец_Компонент" UnboundType="String"></dxwgv:GridViewDataTextColumn>
                            </Columns>
                            <ClientSideEvents EndCallback="EndCalcCallBack" />
                            <Settings ShowVerticalScrollBar="True" VerticalScrollableHeight="170" />
                            </dxwgv:ASPxGridView>
                    </dxp:PanelContent>
                </PanelCollection>
            </dxrp:ASPxRoundPanel>
        </td>
    </tr>
    <tr>
        <td style="width:400px;"/>
        <td align="right">
            <dxe:ASPxButton ID="ASPxButton_Save" runat="server" Text="Сохранить" Width="100px" ClientInstanceName="saveButton" AutoPostBack="False">
                <ClientSideEvents Click="OnSaveClick" Init="function(s, e) {
	saveButton.SetEnabled(false);
}" />
            </dxe:ASPxButton>    
        </td>
        <td align="right">
            <dxe:ASPxButton ID="ASPxButton_Close" runat="server" Text="Закрыть" Width="100px">
                <ClientSideEvents Click="function(s, e) { Close(); }" />
            </dxe:ASPxButton>
           
        </td>
    </tr>
</table>