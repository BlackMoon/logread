<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportForms_List.ascx.cs" Inherits="ReportForms_List" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.2, Version=8.2.4.0, Culture=neutral, PublicKeyToken=9b171c9fd64da1d1"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<%@ Register Assembly="RadAjax.Net2" Namespace="Telerik.WebControls" TagPrefix="radA" %>
<%@ Register Assembly="RadSplitter.Net2" Namespace="Telerik.WebControls" TagPrefix="radspl" %>
<%@ Register Assembly="RadTreeview.Net2" Namespace="Telerik.WebControls" TagPrefix="radt"%>
<%@ Register Assembly="RadMenu.Net2" Namespace="Telerik.WebControls" TagPrefix="radM" %>
<%@ Register Assembly="�������" Namespace="����.�������.���������" TagPrefix="Bars" %>

<script type="text/javascript" src="Resources/Scripts/SignData.js"></script>

<script type="text/javascript">
 
// ����������� ��������� �������� ����
function AttachEvent()
{
    var radWindow = GetRadWindow();
    
    if( radWindow != null )
    {
        radWindow.OnClientClose = function()
        {
            try
            {
                radWindow.BrowserWindow.BlockerManager.RemoveAllBlocks();
            }
            catch(e)
            {
            }
        }
    }
}

AttachEvent();

function TreeListAfterClickHandler( node )
{
    if( node.Value != null )
    {
        grid_Forms.PerformCallback( "Update:" + node.Value );
    }
}

function GridContextMenu(sender, args)
{
    sender.Hide();
}

function TableDblClick(Form, SessionID, ClientID)
{
    var table = null; //!
    
    var selectedRow;
    
    if(table != null && table.MasterTableView.SelectedRows[0] != null)
    {
        selectedRow = table.MasterTableView.SelectedRows[0];
    }
    
    var DataSourceIndex = selectedRow.Control.attributes['DataSourceIndex'].value;
    
    ShowEditForm( Form, DataSourceIndex, SessionID, ClientID );
}

function ClickElementMenu(Value)
{
    var menu = null; //!
    var table = null;
    
    var selectedRow;
    
    if(table != null && table.MasterTableView.SelectedRows[0] != null)
    {
        selectedRow = table.MasterTableView.SelectedRows[0];
    }
    
    var attributeScript;
    var Url;
    
    if(selectedRow != null)
    {
        switch(Value)
        {
            case "ProverkaIn": attributeScript = selectedRow.Control.attributes['ScriptProverkaIn']; Url = "Forms/ReportForms/Uviazki/UviazkiReport.ascx"; break;
            
            case "ProverkaOut": attributeScript = selectedRow.Control.attributes['ScriptProverkaOut']; Url = "Forms/ReportForms/Uviazki/UviazkiReport.ascx"; break;
            
            case "PrintForms": attributeScript = selectedRow.Control.attributes['ScriptPrintForms']; Url = "Forms/ReportForms/PrintForms/PrintForms.ascx"; break;
        }
        
    }
    
    if(attributeScript != null)
    {
        var id = generateGuid();
        
        var oWnd = window.radopen( attributeScript.value+"&ID="+id );
        oWnd.Argument = id;
        
        SetSavedSize(oWnd, Url);
    }
}  
       
function RowContextMenu(index, e)
{
    var menu = null; //!
    var table = null;
    
    var selectedRow;
    
    if(table != null && table.MasterTableView.SelectedRows[0] != null)
    {
        selectedRow = table.MasterTableView.SelectedRows[0];
    }
    
    var attributeElements = selectedRow.Control.attributes['elements'];
    
    if (attributeElements != null ) 
    {
        //menu.FindItemByValue("Expertiza").Hide();
        menu.FindItemByValue("Status").Hide();
        menu.FindItemByValue("StatusNone").Hide();
        menu.FindItemByValue("StatusChernovik").Hide();
        menu.FindItemByValue("StatusZapolneno").Hide();
        menu.FindItemByValue("StatusProvereno").Hide();
        menu.FindItemByValue("StatusExpert").Hide();
        menu.FindItemByValue("StatusUtverjdeno").Hide();
        menu.FindItemByValue("Separator2").Hide();
        menu.FindItemByValue("Data").Hide();
        menu.FindItemByValue("Clear").Hide();
        
        elements = attributeElements.value.split(",");
        
        for(var i = 0 ; i< elements.length; i++ )
        {            
            switch(elements[i])
            {
                //case "Expertiza":           menu.FindItemByValue("Expertiza").Show(); break;
                case "Status":              menu.FindItemByValue("Status").Show(); break;
                case "StatusNone":          menu.FindItemByValue("StatusNone").Show(); break;
                case "StatusZapolneno":     menu.FindItemByValue("StatusZapolneno").Show(); break;
                case "StatusProvereno":     menu.FindItemByValue("StatusProvereno").Show(); break;
                case "StatusChernovik":     menu.FindItemByValue("StatusChernovik").Show(); break;
                case "StatusExpert":        menu.FindItemByValue("StatusExpert").Show(); break;
                case "StatusUtverjdeno":    menu.FindItemByValue("StatusUtverjdeno").Show(); break;
                case "Data":                menu.FindItemByValue("Data").Show(); menu.FindItemByValue("Separator2").Show(); break;
                case "Clear":               menu.FindItemByValue("Clear").Show(); break;
            }    
        }
    }
    
    menu.FindItemByValue("���").Hide();
    menu.FindItemByValue("SingForm").Hide();
    menu.FindItemByValue("CoSingForm").Hide();
    menu.FindItemByValue("DelSign").Hide();
    menu.FindItemByValue("CheckSign").Hide();
    menu.FindItemByValue("ShowSign").Hide();
    
    var signData = selectedRow.Control.attributes['signData'];
   
    if( signData != null )
    {
        var signDataValue = signData.value;
    
        if( signDataValue == "AllowSign" )
        {
            menu.FindItemByValue("���").Show();
            
            menu.FindItemByValue("SingForm").Show();
        }
        else if( signDataValue == "Sign" )
        {
            menu.FindItemByValue("���").Show();
            
            menu.FindItemByValue("CoSingForm").Show();
            menu.FindItemByValue("DelSign").Show();
            menu.FindItemByValue("CheckSign").Show();
            menu.FindItemByValue("ShowSign").Show();
        }
    }
    
    menu.Show(e);
    
    e.cancelBubble = true;
    e.returnValue = false;

    if (e.stopPropagation)
    {
       e.stopPropagation();
       e.preventDefault();
    }
    
    /* If we have a new row, the this.Rows Property is not available */
    if (this.Rows)
		this.SelectRow(this.Rows[index].Control, true);
}

function AjaxRequestClickTreeView()
{
    var ajaxManager = <%= RadAjaxManager1.ClientID %>;

    ajaxManager.AjaxRequestWithTarget('ctl02:������_����������������', 'NodeClick#');
}

function SignForm(ID, Session)
{
    var selectedRow;
    
    var table = null; // !
    
    if(table != null && table.MasterTableView.SelectedRows[0] != null)
    {
        selectedRow = table.MasterTableView.SelectedRows[0];
    }
    
    if( selectedRow == null )
    {
        return;
    }
    
    var DataSourceIndex = selectedRow.Control.attributes['DataSourceIndex'].value;
    
    var data = ECPData.GetStoredData( ID, Session, DataSourceIndex );

    var SignData = Sign( data.value );
    
    ECPData.SaveSign( ID, Session, DataSourceIndex, SignData );
    
    alert( '�������� ������� ��������!' );
    
    AjaxRequestClickTreeView();
}

function CoSignForm(ID, Session)
{
    var selectedRow;
    
    var table = null; // !
    
    if(table != null && table.MasterTableView.SelectedRows[0] != null)
    {
        selectedRow = table.MasterTableView.SelectedRows[0];
    }
    
    if( selectedRow == null )
    {
        return;
    }
    
    var DataSourceIndex = selectedRow.Control.attributes['DataSourceIndex'].value;
    
    var data = ECPData.GetStoredData( ID, Session, DataSourceIndex );
  
    var sign = ECPData.GetSign( ID, Session, DataSourceIndex );

    var SignData = CoSign( data.value, sign.value );
    
    ECPData.SaveSign( ID, Session, DataSourceIndex, SignData );
    
    alert( '��������� ������� ���������!' );
}

function CheckSign(ID, Session)
{
    var selectedRow;
    
    var table = null; // !
    
    if(table != null && table.MasterTableView.SelectedRows[0] != null)
    {
        selectedRow = table.MasterTableView.SelectedRows[0];
    }
    
    if( selectedRow == null )
    {
        return;
    }
    
    var DataSourceIndex = selectedRow.Control.attributes['DataSourceIndex'].value;
    
    var check = ECPData.CheckSign( ID, Session, DataSourceIndex );

    if( !check.value )
    {
        alert( '������� ��� �������!!!.\n(�������� ��� �������)' );
    }
    else
    {
        alert( '������� ��� �����!' );
    }
}

function DelSign(ID, Session)
{
    var selectedRow;
    
    var table = null; // !
    
    if(table != null && table.MasterTableView.SelectedRows[0] != null)
    {
        selectedRow = table.MasterTableView.SelectedRows[0];
    }
    
    if( selectedRow == null )
    {
        return;
    }
    
    var DataSourceIndex = selectedRow.Control.attributes['DataSourceIndex'].value;
    
    var check = ECPData.DelSign( ID, Session, DataSourceIndex );
    
    if( !check.value )
    {
        alert( '�� ������� ������� �������!' );
    }
    else
    {
        alert( '������� ������� �������!' );
    }
    
    AjaxRequestClickTreeView();
}
</script>

<radA:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="AjaxLoadingPanel1"
    RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="False">
<AjaxSettings>
    <radA:AjaxSetting AjaxControlID="����������������_���������������">
        <UpdatedControls>
            <radA:AjaxUpdatedControl ControlID="treeElements" />
        </UpdatedControls>
    </radA:AjaxSetting>
</AjaxSettings>
</radA:RadAjaxManager>

<radA:AjaxLoadingPanel ID="AjaxLoadingPanel1" runat="server" Height="75px" Width="75px" RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="false" Transparency="50">
    <asp:Image ID="Image1" runat="server" AlternateText="��������..." ImageUrl="~/Resources/RadControls/Ajax/Skins/Default/LoadingProgressBar.gif" />
</radA:AjaxLoadingPanel>

<div align="center" style="padding-top:10px">
    <radspl:RadSplitter ID="RadSplitter1" runat="server" RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="false" Skin="WebBlue" Height="600px" Width="98%" EnableClientDebug="false" BorderSize="0"  BorderStyle="None"  >
        <radspl:RadPane id="RadPane1" Runat="server" Width="250px" Height="100%" RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="false" >
        
            <table align = "center" style="height:100%;width:100%">
                <tr style="height:20px">
                    <td style="vertical-align:top">
                        <Bars:���������������� ID="����������������_���������������" runat="server"  AutoPostBack="true" UseEmbeddedScripts="false" 
                            OnTextChanged="����������������_���������������_TextChanged" Width="200px">
                        </Bars:����������������>
                    </td>
                </tr>
                <tr  style="height:550px;">
                    <td style="vertical-align:top">
                        <radt:RadTreeView ID="treeElements" style="overflow: hidden;" runat="server" RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="false" OnNodeExpand="������_����������������_NodeExpand" Skin="WebBlue" AfterClientClick="TreeListAfterClickHandler" LoadingMessage="(�������� ...)" >
                        </radt:RadTreeView>
                    </td>
                </tr>
            </table>        
            
            
        </radspl:RadPane>
        
        <radspl:RadSplitBar id="RadSplitBar1" Runat="server" CollapseMode="Forward" RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="false">
        </radspl:RadSplitBar>
        
        <radspl:RadPane id="RadPane2" Runat="server" Height="100%" RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="false">
            <dxwgv:ASPxGridView ID="grid_Forms" runat="server" AutoGenerateColumns="False" Width="100%" ClientInstanceName="grid_Forms" OnBeforePerformDataSelect="grid_Forms_BeforePerformDataSelect" OnCustomCallback="grid_Forms_CustomCallback">
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="��������� ��������� �������" FieldName="��������������������������������"
                        VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="������" FieldName="������" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="�������� �����" FieldName="��������" VisibleIndex="2">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="���������" FieldName="�������������������"
                        VisibleIndex="3">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="������������ ������" FieldName="����������������������������������"
                        VisibleIndex="4">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="��������������� ������" FieldName="�������������������������������������"
                        VisibleIndex="5">
                    </dxwgv:GridViewDataTextColumn>
                </Columns>
                <SettingsBehavior ColumnResizeMode="NextColumn" />
            </dxwgv:ASPxGridView>
            
        </radspl:RadPane>
        
        
    </radspl:RadSplitter>
</div>

<radM:RadMenu ID="RadMenu1" runat="server" IsContext="true" RadControlsDir="~/Resources/RadControls/" UseEmbeddedScripts="false" Skin="Outlook" OnItemClick="RadMenu1_ItemClick" ContextMenuElementID="none">
    <Items>
        <radM:RadMenuItem Text="��������������� ������" ID="�������_�����������" Value="UviazkiIn" runat="server">
            <Items>
                <radM:RadMenuItem Text="��������� ������ �����" ID="���������_�����������" Value="ProverkaIn" runat="server"/>
                <radM:RadMenuItem Text="��������� ����������" ID="���������_���������������" Value="ResultIn" runat="server" Visible="false"/>
            </Items>
        </radM:RadMenuItem>
        <radM:RadMenuItem Text="������������ ������" ID="�������_���������" Value="UviazkiOut" runat="server">
            <Items>
                <radM:RadMenuItem Text="��������� ������ �����" ID="���������_�������������" Value="ProverkaOut" runat="server"/>
                <radM:RadMenuItem Text="��������� ����������" ID="���������_�����������������" Value="ResultOut" runat="server" Visible="false"/>
            </Items>
        </radM:RadMenuItem>
        <radM:RadMenuItem Text="�������� ������ ������" ID="���������_�������������������" Value="ListUviazok" runat="server" Visible="false"/>
        <radM:RadMenuItem Text="������� �������� ������" ID="���������_�������������" Value="ProverkaUviazok" runat="server" Visible="false"/>
        <radM:RadMenuItem Text="����������" ID="���������_����������" Value="Expertiza" runat="server" Visible="false"/>
        <radM:RadMenuItem IsSeparator="true" ID="�����������_������" value="Separator1" runat="server" Visible="false"/>
        <radM:RadMenuItem Text="���������� ���������" ID="�������_���������" Value="Status" runat="server">
            <Items>
                <radM:RadMenuItem Text="�����" Value="StatusNone"/>
                <radM:RadMenuItem Text="��������" Value="StatusChernovik"/>
                <radM:RadMenuItem Text="���������" Value="StatusZapolneno"/>
                <radM:RadMenuItem Text="���������" Value="StatusProvereno"/>
                <radM:RadMenuItem Text="����������" Value="StatusExpert"/>
                <radM:RadMenuItem Text="����������" Value="StatusUtverjdeno"/>
            </Items>
        </radM:RadMenuItem>
        <radM:RadMenuItem Text="������� ���������" ID="���������_����������������" Value="ChangeHistory" runat="server" Visible="false"/>
        <radM:RadMenuItem IsSeparator="true" ID="�����������_������������" Value="Separator3" runat="server"/>
        <radM:RadMenuItem Text="�������� �����" ID="�������_������" Value="PrintForms" runat="server"/>
        <radM:RadMenuItem IsSeparator="true" ID="�����������5" Value="Separator5" runat="server" Visible="false"/>
        <radM:RadMenuItem Text="���" ID="�������_���" Value="���" runat="server">
            <Items>
                <radM:RadMenuItem Text="��������� �����" ID="���������_���������" Value="SingForm" runat="server"/>
                <radM:RadMenuItem Text="�������� ���������" ID="���������_�����������" Value="CoSingForm" runat="server"/>
                <radM:RadMenuItem Text="������� �������" ID="���������_��������������" Value="DelSign" runat="server"/>
                <radM:RadMenuItem Text="��������� �������" ID="���������_����������������" Value="CheckSign" runat="server"/>
                <radM:RadMenuItem Text="�������� �������" ID="���������_���������������" Value="ShowSign" runat="server"/>
            </Items>
        </radM:RadMenuItem>
        <radM:RadMenuItem IsSeparator="true" ID="�����������" Value="Separator2" runat="server"/>
        <radM:RadMenuItem Text="������" ID="�������_������" Value="Data" runat="server" >
            <Items>
                <radM:RadMenuItem Text="��������" ID="���������_��������" Value="Clear" runat = "server"/>
            </Items>
        </radM:RadMenuItem>
    </Items>
</radM:RadMenu>