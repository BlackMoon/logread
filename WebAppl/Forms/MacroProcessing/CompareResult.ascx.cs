using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using ����.�������.���������;
using ����.�����;
using DevExpress.Web;

public partial class Forms_MacroProcessing_CompareResult : ��������
{
    public Forms_MacroProcessing_CompareResult()
        : base()
    {
        this.������������� = "";
        this.����������������� = "���������� ������";

        this.���������� = 800;
        this.���������� = 600;

        this.������������������������ += new ����.���������.�����������������(Forms_MacroProcessing_CompareResult_������������������������);
    }

    void Forms_MacroProcessing_CompareResult_������������������������(object �����������, ����.���������.���������������� ���������)
    {
        �������������������������������� ��������������� = �����������������������();

        if (��������������� != null)
        {
            // ���������� �������
            �������_���������.SettingsText.CommandCancel = "������";
            �������_���������.SettingsText.CommandClearFilter = "�������� ������";
            �������_���������.SettingsText.CommandDelete = "�������";
            �������_���������.SettingsText.CommandEdit = "��������";
            �������_���������.SettingsText.CommandNew = "��������";
            �������_���������.SettingsText.GroupPanel = "������� ��� ����������� ��������";
            �������_���������.SettingsText.HeaderFilterShowAll = "(���)";
            �������_���������.SettingsText.GroupContinuedOnNextPage = "������ ������������ �� ��������� ��������";
            �������_���������.SettingsText.ConfirmDelete = "�� �������, ��� ������ ������� ������?";
            �������_���������.SettingsText.CommandUpdate = "���������";
            �������_���������.SettingsText.EmptyDataRow = "��� ������";
            �������_���������.SettingsText.CustomizationWindowCaption = "����� ��������";
            �������_���������.SettingsLoadingPanel.Text = "��������...";
            �������_���������.SettingsPager.Summary.Text = "�������� {0} �� {1} ({2} �������)";

            if (���������������.��������� != "")
            {
                ��������������������������.Text = ���������������.���������;
                ��������������������������.Visible = true;
            }

            if (���������������.������������ != "")
            {
                �����������������������������.Text = ���������������.������������;
                �����������������������������.Visible = true;
            }

            if (!IsPostBack)
            {
                if (���������������.������������������� != null && ���������������.�������������������.Count > 0)
                {
                    �������_���������.Columns.Clear();

                    foreach (KeyValuePair<string, string> ������������������� in ���������������.�������������������)
                    {
                        GridViewDataColumn column = new GridViewDataColumn(�������������������.Value, �������������������.Key);

                        �������_���������.Columns.Add(column);
                    }
                }
            }

            �������_���������.DataSource = ���������������.������������;
             
            �������_���������.DataBind();
        }
    }

    protected �������������������������������� �����������������������()
    {
        if (this.���������������������.�������������("SessionParam"))
        {
            string[] strings = ���������������������["SessionParam"].Split(':');

            if (strings.Length == 2)
            {
                �������������������������������� ��������������� = null;

                object �������������������������� = ����.�������.����������������������������.��������������������������(strings[0], strings[1]);

                if (�������������������������� is ��������������������������������)
                {
                    ��������������� = (��������������������������������)��������������������������;
                }

                return ���������������;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    protected void ASPxButton_������_Click(object sender, EventArgs e)
    {
        �������������������������������� ��������������� = �����������������������();

        ����.�������������������� ��������������� = new ����.��������������������(���������������);

        ����.�������.������������.�������������������(���������������, "Sverka.xls");
    }
}
