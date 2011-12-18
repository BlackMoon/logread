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
using ����.�������.���������;
using ����.�����;
using ����.�����.�������������;
using ����.�����.����������������������;
using ����.�����.������;

public partial class Forms_ReportForms_Uviazki_UviazkiReport : ������������������������������
{
    public Forms_ReportForms_Uviazki_UviazkiReport()
        : base()
    {
        this.������������� = "";
        this.����������������� = "���������� �������� ������";

        this.������������������������ += new ����.���������.�����������������(Forms_ReportForms_Uviazki_UviazkiReport_������������������������);
    }

    ����������������������� ����������������� = null;

    void Forms_ReportForms_Uviazki_UviazkiReport_������������������������(object �����������, ����.���������.���������������� ���������)
    {
        ������������������� ������������� = ���������������������();

        if (������������� != null)
        {
            this.������������������� = �������������;
        }

        bool ��������������� = true;

        if (this.���������������������.�������������("Params"))
        {
            ��������������� = this.���������������������["Params"] == "In";
        }

        if (���������������)
        {
            ����������������� = �������������.������������������������������();
        }
        else
        {
            ����������������� = �������������.���������������������������();
        }

        if (�����������������.��������������.Count == 0)
        {
            Div_Result.InnerText = "������ ��� �������������� �� ����� �� ����������.";
            buttonPrint.ClientEnabled = false;
        }
        else
        {
            buttonPrint.ClientEnabled = true;
        }

        if (!IsPostBack)
        {
            try
            {
                ����������������������������������.��������������������������������(�������������.������.�������������, �����������������);
            }
            catch (ArgumentNullException)
            {
            }

            // ���������� �������
            gvUviazki.SettingsText.CommandCancel = "������";
            gvUviazki.SettingsText.CommandClearFilter = "�������� ������";
            gvUviazki.SettingsText.CommandDelete = "�������";
            gvUviazki.SettingsText.CommandEdit = "��������";
            gvUviazki.SettingsText.CommandNew = "��������";
            gvUviazki.SettingsText.GroupPanel = "������� ��� ����������� ��������";
            gvUviazki.SettingsText.HeaderFilterShowAll = "(���)";
            gvUviazki.SettingsText.GroupContinuedOnNextPage = "������ ������������ �� ��������� ��������";
            gvUviazki.SettingsText.ConfirmDelete = "�� �������, ��� ������ ������� ������?";
            gvUviazki.SettingsText.CommandUpdate = "���������";
            gvUviazki.SettingsText.EmptyDataRow = "��� ������";
            gvUviazki.SettingsText.CustomizationWindowCaption = "����� ��������";
            gvUviazki.SettingsLoadingPanel.Text = "��������...";
            gvUviazki.SettingsPager.Summary.Text = "�������� {0} �� {1} ({2} �������)";
                        
            gvUviazki.DataBind();

            this.������������� = string.Format("��������� �������� {0} ������ ����� {1}", ��������������� ? "���������������" : "������������", �������������.�������������.���);
        }
    }

    protected void buttonPrint_Click(object sender, EventArgs e)
    {
        if (����������������� != null)
        {
            ����.�������������������������������������� ������������� = new ����.��������������������������������������(�����������������);

            ����.�������.������������.�������������������(�������������, "CheckResult.xls");
        }
    }

    protected void gvUviazki_BeforePerformDataSelect(object sender, EventArgs e)
    {
        gvUviazki.DataSource = �����������������.��������������;
    }
}
