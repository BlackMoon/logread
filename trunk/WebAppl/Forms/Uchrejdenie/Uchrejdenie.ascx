<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Uchrejdenue_Uchrejdenie" Codebehind="Uchrejdenie.ascx.cs" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

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
            ���:
        </td>
        <td align="left">
            <Bars:��������������� runat="server" ID="���������_���" Width="300px" ����������������������="���">
            </Bars:���������������>
        </td>
    </tr>
    <tr>
        <td align="right">
            ������������:
        </td>
        <td align="left">
            <Bars:��������������� runat="server" ID="���������_������������" Width="300px" ����������������������="������������">
            </Bars:���������������>
        </td>
    </tr>
    <tr>
        <td align="right">
            ������ ������������:
        </td>
        <td align="left">
            <Bars:��������������� runat="server" ID="���������_������������������" Width="300px" ����������������������="������������������">
            </Bars:���������������>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="width:100%">
            <Bars:������������ ID="������������_���" runat="server" ��������������������������="������������_���" ����������������������=0>
                <�������>
                    <Bars:������� ID="�������_���������" ���������="���������" ���������������������="��������_���������">
                    </Bars:�������>
                    <Bars:������� ID="�������_����������" ���������="����������" ���������������������="��������_����������">
                    </Bars:�������>
                    <Bars:������� ID="�������_��������" ���������="��������" ���������������������="��������_��������">
                    </Bars:�������>
                    <Bars:������� ID="�������_���������������" ���������="���������������" ���������������������="��������_���������������">
                    </Bars:�������>
                    <Bars:������� ID="�������_���" ���������="�������������� ��������" ���������������������="��������_���">
                    </Bars:�������>
                </�������>
            </Bars:������������>
            <Bars:������������ ID="������������_���" runat="server" Width="100%" BorderStyle="Solid" BorderColor="LightBlue" BorderWidth="1px" Height="300px">
                <Bars:�������� ID="��������_���������" runat="server">
                    <table width="100%" height="100%" align="center">
                        <tr>
                            <td align="right">
                                ���:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_���" Width="300px" ����������������������="���">
                                </Bars:���������������>
                            </td>
                            <td align="right">
                                ���:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_���" Width="300px" ����������������������="���">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ���:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_���" Width="300px" ����������������������="���">
                                </Bars:���������������>
                            </td>
                            <td align="right">
                                ����� �������:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_������������" Width="300px" ����������������������="������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                �����:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_�����" Width="300px" ����������������������="�����">
                                </Bars:���������������>
                            </td>
                            <td align="right">
                                ����:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_����" Width="300px" ����������������������="����">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ����:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_����" Width="300px"  ����������������������="����">
                                </Bars:���������������>
                            </td>
                            <td align="right">
                                �����:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_�����" Width="300px"  ����������������������="�����">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                �����:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_�����" Width="300px" ����������������������="�����">
                                </Bars:���������������>
                            </td>
                            <td align="right">
                                �����:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="��������������" Width="300px" ����������������������="�����">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                �����:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_�����" Width="300px" ����������������������="�����">
                                </Bars:���������������>
                            </td>
                            <td align="right">
                                ����:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="�������������" Width="300px" ����������������������="����">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ����:
                            </td>
                            <td>
                                <Bars:��������������� runat="server" ID="���������_����" Width="300px" ����������������������="����">
                                </Bars:���������������>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr style="height:100%">
                            <td></td>
                        </tr>
                    </table>
                </Bars:��������>
                <Bars:�������� ID="��������_����������" runat="server">
                    <table width="100%" height="100%" align="center">
                        <tr>
                            <td align="right">
                                ������������:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_������������" Width="500px" ����������������������="���������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ��. ���������:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_�������" Width="500px" ����������������������="��������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ������������ ����.-������. ������:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_�����������������" Width="500px" ����������������������="�����������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ��������� ���������� ����������:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������������_����������" Width="500px" ����������������������="����������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr style="height:100%">
                            <td></td>
                        </tr>
                    </table>
                </Bars:��������>
                <Bars:�������� ID="��������_��������" runat="server">
                    <table width="100%" height="100%" align="center">
                         <tr>
                            <td align="right">
                                ������������ � ����������� ������:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_����������" Width="500px" ����������������������="���������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ������ ������������ � ����������� ������:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_��������������" Width="500px" ����������������������="���������������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ��� ����������:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_�������������" Width="500px" ����������������������="�������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ��� ����������:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_�������������" Width="500px" ����������������������="�������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ��� �� � ����3:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_����������3" Width="500px" ����������������������="����">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr style="height:100%">
                            <td></td>
                        </tr>
                    </table>
                </Bars:��������>
                <Bars:�������� ID="��������_���������������" runat="server">
                    <table width="100%" height="100%" align="center">
                        <tr>
                            <td align="right">
                                ������:
                            </td>
                            <td align="left" colspan="3">
                                <Bars:��������������� runat="server" ID="���������_������" Width="650px" ����������������������="������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ����������:
                            </td>
                            <td align="left" colspan="3">
                                <Bars:��������������� runat="server" ID="���������_����������" Width="650px" ����������������������="������������������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ��� ����������� ������:
                            </td>
                            <td align="left" style="width:200px">
                                <Bars:��������������� runat="server" ID="���������_���������" Width="200px" ����������������������="��������������������">
                                </Bars:���������������>
                            </td>
                            <td align="right" style="width:150px">
                                ���������� �����:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_��������" Width="300px" ����������������������="���������������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                �����:
                            </td>
                            <td align="left" colspan="3">
                                <Bars:��������������� runat="server" ID="���������_�����" Width="650px" ����������������������="�����">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                ��������:
                            </td>
                            <td align="left" style="width:200px">
                                <Bars:��������������� runat="server" ID="���������_��������" Width="200px" ����������������������="��������">
                                </Bars:���������������>
                            </td>
                            <td align="right" style="width:150px">
                                ������:
                            </td>
                            <td align="left">
                                <Bars:��������������� runat="server" ID="���������_������" Width="300px" ����������������������="������">
                                </Bars:���������������>
                            </td>
                        </tr>
                        <tr style="height:100%">
                            <td></td>
                        </tr>
                    </table>
                </Bars:��������>
                <Bars:�������� ID="��������_���" runat="server">
                    <table runat="server" id="table_Attr" width="100%" height="100%" align="center">
                    </table>
                </Bars:��������>
            </Bars:������������>
    </tr>
    <tr style="height: 26px">
        <td align="right" colspan="2">
            <Bars:������ runat="server" Text="��" ID="������_��" ���������="��" OnClick="������_��_Click" />
            <Bars:������ runat="server" Text="������" ID="������_������" ���������="������" OnClientClick="Close();" />
        </td>
    </tr>
</table>