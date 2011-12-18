using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Telerik.WebControls;

using ����.�������;
using ����.�������.���������;
using ����.�����.�������������;
using ����.�����;
using ����;
using ����.����;
using ����.�������;

public partial class Racheti_List : ��������
{
    public Racheti_List()
        : base()
    {
        this.����������������� = "�������";
        this.������������� = "�������";

        this.������������������� += new ����.���������.�����������������(Racheti_List_�������������������);
    }

    void Racheti_List_�������������������(object �����������, ����.���������.���������������� ���������)
    {
        �������������� ������ = new ��������������();
        
        ���������������.DataSource = ������;
        ���������������.DataBind();

        if (!IsPostBack)
        {
            ���������������.SettingsText.CommandCancel = "������";
            ���������������.SettingsText.CommandClearFilter = "�������� ������";
            ���������������.SettingsText.CommandDelete = "�������";
            ���������������.SettingsText.CommandEdit = "��������";
            ���������������.SettingsText.CommandNew = "��������";
            ���������������.SettingsText.GroupPanel = "������� ��� ����������� ��������";
            ���������������.SettingsText.HeaderFilterShowAll = "(���)";
            ���������������.SettingsText.GroupContinuedOnNextPage = "������ ������������ �� ��������� ��������";
            ���������������.SettingsText.ConfirmDelete = "�� �������, ��� ������ ������� ������?";
            ���������������.SettingsText.CommandUpdate = "���������";
            ���������������.SettingsText.EmptyDataRow = "��� ������";
            ���������������.SettingsText.CustomizationWindowCaption = "����� ��������";
            ���������������.SettingsLoadingPanel.Text = "��������...";
            ���������������.SettingsPager.Summary.Text = "�������� {0} �� {1} ({2} �������)"; 
        }
    }

    protected void ���������������_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            return;
        }

        e.Row.Attributes.Add("Key", e.KeyValue.ToString());
    }

}

