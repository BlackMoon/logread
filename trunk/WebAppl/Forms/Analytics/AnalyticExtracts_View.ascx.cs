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
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Web;


using ����;
using ����.����;
using ����.�������;
using ����.�������.���������;
using ����.�����;

public partial class Forms_Analytics_AnalyticExtracts_View : ��������
{
    public Forms_Analytics_AnalyticExtracts_View()
        : base()
    {
        this.����������������� = "������������� �������";

        this.������������������������ += new ����.���������.�����������������(Forms_Analytics_AnalyticExtracts_View_������������������������);
    }

    void Forms_Analytics_AnalyticExtracts_View_������������������������(object �����������, ����.���������.���������������� ���������)
    {
        ��������������������.Styles.FieldValueStyle.HorizontalAlign = HorizontalAlign.Left;

        if (Session["AnalyticExtract"] != null && Session["AnalyticExtract"] is ���������������������������)
        {
            DataTable ������������� = null;

            if (!IsPostBack)
            {
                DevExpress.XtraPivotGrid.Localization.PivotGridLocalizer.Active = new ����.�����������.XtraPivotGrid�������();
                ��������������������.OptionsLoadingPanel.Text = "��������...";
                ��������������������.OptionsPager.CurrentPageNumberFormat = "�������� {0}";
                
                ��������������������������� ��������������������������� = Session["AnalyticExtract"] as ���������������������������;
                
                ���������������������������� ���������������������������� = new ����������������������������(���������������������������);
                //����������������������������.�������������������();

                bool ������������� = true;

                if (Session["BuildAnew"] != null && Session["BuildAnew"] is bool)
                {
                    try
                    {
                        ������������� = (bool)Session["BuildAnew"];
                    }
                    catch
                    {
                    }
                }

                ������������� = ����������������������������.���������������������(�������������);

                Session["PivotGridDataSource"] = �������������;

                // ����
                DevExpress.Web.ASPxPivotGrid.PivotGridField ����������� = new DevExpress.Web.ASPxPivotGrid.PivotGridField();
                ��������������������.Fields.Add(�����������);
                �����������.Caption = "����������";
                �����������.FieldName = "Uchrej".ToUpper();
                �����������.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                �����������.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;
                �����������.Width = 100;

                foreach (����������������������� ���� in ���������������������������.����)
                {
                    ����������� = new DevExpress.Web.ASPxPivotGrid.PivotGridField();

                    ��������������������.Fields.Add(�����������);

                    �����������.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    �����������.CellFormat.FormatString = "N2";

                    �����������.Caption = ����.���������;
                    �����������.FieldName = ����.�������������.ToUpper();

                    if (����.�������OLAP != ����.�������OLAP����.�����������)
                    {
                        �����������.Area = ����������������(����.�������OLAP);
                    }
                    else
                    {
                        �����������.Visible = false;
                    }

                    if (����.����������������������� == ��������������������������������������������������.���������)
                    {
                        �����������.Options.ShowTotals = false;
                    }
                    else
                    {
                        �����������.SummaryType = (DevExpress.Data.PivotGrid.PivotSummaryType)((int)����.�����������������������);
                    }

                    �����������.Name = "Field" + (��������������������.Fields.Count + 1).ToString();
                }
            }
            else if (Session["PivotGridDataSource"] != null && Session["PivotGridDataSource"] is DataTable)
            {
                ������������� = Session["PivotGridDataSource"] as DataTable;
            }

            ��������������������.DataSource = �������������;
        }
    }

    private PivotArea ����������������(����.�������OLAP���� �������)
    {
        switch (�������)
        {
            case �������OLAP����.����������������:
                return PivotArea.ColumnArea;
            case �������OLAP����.���������������:
                return PivotArea.RowArea;
            case �������OLAP����.��������������:
                return PivotArea.FilterArea;
            case �������OLAP����.������:
                return PivotArea.DataArea;
            default:
                return PivotArea.DataArea;
        }
    }

    protected void ��������������������_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        if (Session["AnalyticExtract"] != null && Session["AnalyticExtract"] is ���������������������������)
        {
            ��������������������������� ��������������������������� = Session["AnalyticExtract"] as ���������������������������;

            ���������������������������� ���������������������������� = new ����������������������������(���������������������������);

            ����������������������������.���������������(e);
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        if (e.Item == null)
        {
            return;
        }

        if (e.Item.Text == "������")
        {
            string ���������� = ������������.�������������������������(".xls");

            ASPxPivotGridExporter_1.ExportToXls(����������, true);

            ������������.����������������������(����������, "Analitic.xls");
        }
    }
}
