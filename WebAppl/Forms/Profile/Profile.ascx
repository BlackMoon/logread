<%@ Control Language="C#" AutoEventWireup="true" Inherits="Forms_Profile_Profile" Codebehind="Profile.ascx.cs" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

<table width="100%">
    <tr>
        <td style="width:200px;" align="right">
            ��� ������������:
        </td>
        <td align="left">
            <Bars:��������������� runat="server" ID="���������_������������" Width="300px" ����������������������="������������" ReadOnly="true" BackColor=LightGrey>
            </Bars:���������������>
        </td>
    </tr>
    <tr>
        <td align="right">
            �����:
        </td>
        <td align="left">
            <Bars:��������������� runat="server" ID="���������_�����" Width="300px" ����������������������="�����" ReadOnly="true" BackColor=LightGrey>
            </Bars:���������������>
        </td>
    </tr>
    <tr>
        <td align="right">
            ������ ������:
        </td>
        <td align="left">
            <Bars:��������������� runat="server" ID="���������_������������" Width="300px" ����������������������="������������" TextMode="Password" Text="">
            </Bars:���������������>
        </td>
    </tr>
    <tr>
        <td align="right">
            ����� ������:
        </td>
        <td align="left">
            <Bars:��������������� runat="server" ID="���������������_�����������" Width="300px" ����������������������="�����������" TextMode="Password" Text="">
            </Bars:���������������>
        </td>
    </tr>
    <tr>
        <td align="right">
            ������������� ������:
        </td>
        <td align="left">
            <Bars:��������������� runat="server" ID="���������������_�����������2" Width="300px" ����������������������="�����������2" TextMode="Password" Text="">
            </Bars:���������������>
        </td>
    </tr>
    <tr style="height: 26px">
        <td align="right" colspan="2">
            <Bars:������ runat="server" Text="��" ID="������_��" ���������="��" OnClick="������_��_Click" />
            <Bars:������ runat="server" Text="������" ID="������_������" ���������="������" OnClientClick="Close();" />
        </td>
    </tr>
</table>