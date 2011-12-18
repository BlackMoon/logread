<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Uchrejdenue_Uchrejdenie" Codebehind="Uchrejdenie.ascx.cs" %>
<%@ Register Assembly="ВебЯдро" Namespace="Барс.ВебЯдро.Интерфейс" TagPrefix="Bars" %>

<script type="text/javascript">
function OnButtonEditClick( TypeName, SessionParam, ButtonEdit )
{
    ButtonEditID = ButtonEdit;
    
    var control = window[ ButtonEditID ];
    
    if( control.TextBoxElement.attributes["Code"] != null )
    {
        var code = control.TextBoxElement.attributes["Code"];
        
        OpenDictionary(code.value);
    }
}

function OpenDictionary(Code)
{
    var id = generateGuid();
    
    var oWnd = window.radopen("Forms/Dictionary/Dictionary.aspx?ID=" + id + "&Dictionary=" + escape( Code ) );
    oWnd.Argument = id;
}

function SetSheetValue(Value)
{
    RebindButtonEdit( Value );
    
    var control = window[ ButtonEditID ];
    
    var valueInputID = control.TextBoxElement.attributes["Value"].value;
    
    var valueInput = document.getElementById( "ctl02_" + valueInputID );
    
    valueInput.value = Value;
}
</script>

<table width="100%">
    <tr>
        <td style="width:100px;" align="right">
            Код:
        </td>
        <td align="left">
            <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_Код" Width="300px" ИмяПоляИсточникаДанных="Код">
            </Bars:ПолеВводаТекста>
        </td>
    </tr>
    <tr>
        <td align="right">
            Наименование:
        </td>
        <td align="left">
            <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_Наименование" Width="300px" ИмяПоляИсточникаДанных="Наименование">
            </Bars:ПолеВводаТекста>
        </td>
    </tr>
    <tr>
        <td align="right">
            Полное наименование:
        </td>
        <td align="left">
            <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ПолноеНаименование" Width="300px" ИмяПоляИсточникаДанных="ПолноеНаименование">
            </Bars:ПолеВводаТекста>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="width:100%">
            <Bars:НаборВкладок ID="НаборВкладок_учр" runat="server" ИдентификаторНабораСтраниц="НаборСтраниц_учр" ИндексВыбраннойВкладки=0>
                <Вкладки>
                    <Bars:Вкладка ID="Вкладка_Реквизиты" Заголовок="Реквизиты" ИдентификаторСтраницы="Страница_Реквизиты">
                    </Bars:Вкладка>
                    <Bars:Вкладка ID="Вкладка_Персоналии" Заголовок="Персоналии" ИдентификаторСтраницы="Страница_Персоналии">
                    </Bars:Вкладка>
                    <Bars:Вкладка ID="Вкладка_Атрибуты" Заголовок="Атрибуты" ИдентификаторСтраницы="Страница_Атрибуты">
                    </Bars:Вкладка>
                    <Bars:Вкладка ID="Вкладка_Местонахождение" Заголовок="Местонахождение" ИдентификаторСтраницы="Страница_Местонахождение">
                    </Bars:Вкладка>
                    <Bars:Вкладка ID="Вкладка_Доп" Заголовок="Дополнительные атрибуты" ИдентификаторСтраницы="Страница_Доп">
                    </Bars:Вкладка>
                </Вкладки>
            </Bars:НаборВкладок>
            <Bars:НаборСтраниц ID="НаборСтраниц_учр" runat="server" Width="100%" BorderStyle="Solid" BorderColor="LightBlue" BorderWidth="1px" Height="300px">
                <Bars:Страница ID="Страница_Реквизиты" runat="server">
                    <table width="100%" height="100%" align="center">
                        <tr>
                            <td align="right">
                                ИНН:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ИНН" Width="300px" ИмяПоляИсточникаДанных="ИНН">
                                </Bars:ПолеВводаТекста>
                            </td>
                            <td align="right">
                                КПП:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_КПП" Width="300px" ИмяПоляИсточникаДанных="КПП">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ПФР:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ПФР" Width="300px" ИмяПоляИсточникаДанных="ПФР">
                                </Bars:ПолеВводаТекста>
                            </td>
                            <td align="right">
                                Номер филиала:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_НомерФилиала" Width="300px" ИмяПоляИсточникаДанных="НомерФилиала">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ОКОНХ:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ОКОНХ" Width="300px" ИмяПоляИсточникаДанных="ОКОНХ">
                                </Bars:ПолеВводаТекста>
                            </td>
                            <td align="right">
                                ОКПО:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ОКПО" Width="300px" ИмяПоляИсточникаДанных="ОКПО">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ОКУД:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ОКУД" Width="300px"  ИмяПоляИсточникаДанных="ОКУД">
                                </Bars:ПолеВводаТекста>
                            </td>
                            <td align="right">
                                ОКАТО:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ОКАТО" Width="300px"  ИмяПоляИсточникаДанных="ОКАТО">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ОКВЭД:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ОКВЭД" Width="300px" ИмяПоляИсточникаДанных="ОКВЭД">
                                </Bars:ПолеВводаТекста>
                            </td>
                            <td align="right">
                                ОКОГУ:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВводаОКОГУ" Width="300px" ИмяПоляИсточникаДанных="ОКОГУ">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ОКОПФ:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ОКОПФ" Width="300px" ИмяПоляИсточникаДанных="ОКОПФ">
                                </Bars:ПолеВводаТекста>
                            </td>
                            <td align="right">
                                ОКФС:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВводаОКФС" Width="300px" ИмяПоляИсточникаДанных="ОКФС">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ОКИН:
                            </td>
                            <td>
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ОКИН" Width="300px" ИмяПоляИсточникаДанных="ОКИН">
                                </Bars:ПолеВводаТекста>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr style="height:100%">
                            <td></td>
                        </tr>
                    </table>
                </Bars:Страница>
                <Bars:Страница ID="Страница_Персоналии" runat="server">
                    <table width="100%" height="100%" align="center">
                        <tr>
                            <td align="right">
                                Руководитель:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_Руководитель" Width="500px" ИмяПоляИсточникаДанных="РуководительФИО">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Гл. бухгалтер:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ГлавБух" Width="500px" ИмяПоляИсточникаДанных="ГлБухгалтерФИО">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Руководитель план.-эконом. службы:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_РукПланЭконСлужбы" Width="500px" ИмяПоляИсточникаДанных="РуковПланЭкСлужбы">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Начальник бюджетного управления:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВводаТекста_начБюджУпр" Width="500px" ИмяПоляИсточникаДанных="НачальникБюджУпр">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr style="height:100%">
                            <td></td>
                        </tr>
                    </table>
                </Bars:Страница>
                <Bars:Страница ID="Страница_Атрибуты" runat="server">
                    <table width="100%" height="100%" align="center">
                         <tr>
                            <td align="right">
                                Наименование в родительном падеже:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_НаимВРодит" Width="500px" ИмяПоляИсточникаДанных="НаименованиеРод">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Полное наименование в родительном падеже:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ПолноеНаимВРод" Width="500px" ИмяПоляИсточникаДанных="ПолноеНаименованиеРод">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Вид учреждения:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ВидУчреждения" Width="500px" ИмяПоляИсточникаДанных="ВидУчреждения">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Тип учреждения:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ТипУчреждения" Width="500px" ИмяПоляИсточникаДанных="ТипУчреждения">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Код МО в СКИФ3:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_КодМОВСкиф3" Width="500px" ИмяПоляИсточникаДанных="Скиф">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr style="height:100%">
                            <td></td>
                        </tr>
                    </table>
                </Bars:Страница>
                <Bars:Страница ID="Страница_Местонахождение" runat="server">
                    <table width="100%" height="100%" align="center">
                        <tr>
                            <td align="right">
                                Регион:
                            </td>
                            <td align="left" colspan="3">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_Регион" Width="650px" ИмяПоляИсточникаДанных="Регион">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Территория:
                            </td>
                            <td align="left" colspan="3">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_Территория" Width="650px" ИмяПоляИсточникаДанных="ТерриториальноеОтношение">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Тип населенного пункта:
                            </td>
                            <td align="left" style="width:200px">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_ТипПункта" Width="200px" ИмяПоляИсточникаДанных="ТипНаселенногоПункта">
                                </Bars:ПолеВводаТекста>
                            </td>
                            <td align="right" style="width:150px">
                                Населенный пункт:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_НасПункт" Width="300px" ИмяПоляИсточникаДанных="НаселенныйПункт">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Улица:
                            </td>
                            <td align="left" colspan="3">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_Улица" Width="650px" ИмяПоляИсточникаДанных="Улица">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Строение:
                            </td>
                            <td align="left" style="width:200px">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_Строение" Width="200px" ИмяПоляИсточникаДанных="Строение">
                                </Bars:ПолеВводаТекста>
                            </td>
                            <td align="right" style="width:150px">
                                Корпус:
                            </td>
                            <td align="left">
                                <Bars:ПолеВводаТекста runat="server" ID="ПолеВвода_Корпус" Width="300px" ИмяПоляИсточникаДанных="Корпус">
                                </Bars:ПолеВводаТекста>
                            </td>
                        </tr>
                        <tr style="height:100%">
                            <td></td>
                        </tr>
                    </table>
                </Bars:Страница>
                <Bars:Страница ID="Страница_Доп" runat="server">
                    <table runat="server" id="table_Attr" width="100%" height="100%" align="center">
                    </table>
                </Bars:Страница>
            </Bars:НаборСтраниц>
    </tr>
    <tr style="height: 26px">
        <td align="right" colspan="2">
            <Bars:Кнопка runat="server" Text="ОК" ID="Кнопка_ОК" ТипКнопки="Ок" OnClick="Кнопка_ОК_Click" />
            <Bars:Кнопка runat="server" Text="Отмена" ID="Кнопка_Отмена" ТипКнопки="Отмена" OnClientClick="Close();" />
        </td>
    </tr>
</table>