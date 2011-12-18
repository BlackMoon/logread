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
using System.Collections.Generic;

using ����;
using ����.�������;
using ����.�������.���������;
using ����.�����;

using Telerik.WebControls;

public partial class Forms_Analytics_AnalyticExtracts_List : ��������
{
    public Forms_Analytics_AnalyticExtracts_List()
        : base()
    {
        this.����������������� = "������ ������������� �������";

        this.������������������������ += new ����.���������.�����������������(Forms_Analytics_AnalyticExtracts_List_������������������������);
    }

    void Forms_Analytics_AnalyticExtracts_List_������������������������(object �����������, ����.���������.���������������� ���������)
    {
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

            Cache[ObjectDataSource_list.CacheKeyDependency] = new object();
        }

        �������������������������������.������������������������������������� �������� = �������������������������������.���������������������������(����������������.���_����, �������������������������������.������������_�����������);

        if (��������.�������������� == �������������������������������.�������������������������.�������������������������)
        {
            this.������������� = ����.�����.���������������������������������.������������������������������������������������("��� ������������� �������");
        }
        else if (��������.�������������� == �������������������������������.�������������������������.��������������������������)
        {
            this.������������� = ����.�����.���������������������������������.������������������������������������������������("��� ������������� �������", �������������������������������.������������_�����������);
        }
        else if (��������.�������������� == �������������������������������.�������������������������.�������������������������������)
        {
            this.������������� = ����.�����.���������������������������������.�����������������������������������������������������("��� ������������� �������", ����������������.���_����);
        }
        else
        {
            if (!IsPostBack)
            {
                ���������������.Enabled = true;

                ��������������������������������� ��������������������������������� = new ���������������������������������();

                Session["AnalyticExtractsDataSource"] = ���������������������������������;
            }
        }
    }

    public DataSet GetExtracts()
    {
        DataSet dataSet = new DataSet();
        DataTable table = dataSet.Tables.Add();

        table.Columns.Add("ID");
        table.Columns.Add("���");
        table.Columns.Add("������������");
        table.Columns.Add("���");
        table.Columns.Add("������");

        ��������������������������������� ��������������������������������� = new ���������������������������������();

        int index = 0;

        foreach (��������������������������� �������� in ���������������������������������)
        {
            table.Rows.Add(new object[] { index.ToString(), ��������.���, ��������.������������, ��������.����������������, ��������.������ });
            index++;
        }

        return dataSet;
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
