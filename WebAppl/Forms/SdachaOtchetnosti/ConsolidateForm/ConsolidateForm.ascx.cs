using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ����.�������;
using ����.�������.���������;
using ����;
using ����.�����;
using ����.�����.�������������;
using ����.�����.�������������������;

public partial class Forms_SdachaOtchetnosti_ConsolidateForm_ConsolidateForm : ������������������������������
{
    public Forms_SdachaOtchetnosti_ConsolidateForm_ConsolidateForm()
        : base()
    {
        this.���������� = 700;
        this.���������� = 500;

        this.����������������� = "������� �����";
        this.������������� = "�������� ����� ��� ������ �������� ������";

        this.������������������������ += new ����.���������.�����������������(Forms_SdachaOtchetnosti_ConsolidateForm_ConsolidateForm_������������������������);
    }

    void Forms_SdachaOtchetnosti_ConsolidateForm_ConsolidateForm_������������������������(object �����������, ����.���������.���������������� ���������)
    {
        if (!IsPostBack)
        {
            ������������������� ������������� = ���������������������();

            if (������������� != null)
            {
                �������������.����������������������();

                List<������������������������������> ���������������� = new List<������������������������������>();

                foreach (������������������� ����� in �������������.�������������)
                {
                    ����������������.Add(new ������������������������������(�����));
                }

                �������_��������.��������������� = ����������������;
            }

            �������_��������.��������������������� = false;
            �������_��������.����������������� = false;
            �������_��������.������������������� = false;
            �������_��������.�������������������� = false;
            �������_��������.���������������� = false;
            �������_��������.AllowMultiRowSelection = true;

            Telerik.WebControls.GridClientSelectColumn ������������� = new Telerik.WebControls.GridClientSelectColumn();
            �������_��������.Columns.Add(�������������);
            �������������.HeaderStyle.Width = Unit.Pixel(25);
            �������������.Resizable = false;

            �������������� �������_����������� = new ��������������();
            �������_��������.���������������(�������_�����������);
            �������_�����������.��������� = "����������";
            �������_�����������.���������������������� = "����������������������";

            �������_��������.DataBind();
        }
    }

    protected void ������_��_Click(object sender, EventArgs e)
    {
        try
        {
            ������������������� ������������� = ���������������������();

            if (������������� != null)
            {
                if (�������_��������.SelectedIndexes.Count > 0)
                {
                    List<�������������������> �������������� = new List<�������������������>();

                    List<������������������������������> ���������������� = �������_��������.��������������� as List<������������������������������>;

                    foreach (string index in �������_��������.SelectedIndexes)
                    {
                        int ������ = int.Parse(index);
                        ��������������.Add(����������������[������].�����);
                    }

                    ����������������������������� ����������������� = new �����������������������������(�������������);
                    �����������������.�������������������(��������������);

                    if (������_���������.��������)
                    {
                        �������������.���������������(true);

                        �������������.������������������();
                    }

                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">CloseAndRebindSheet();</script>"));
                }
                else
                {
                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">alert('���������� ������� ����� ��� ��������������!');</script>"));
                }
            }
        }
        catch( Exception exc )
        {
            exc.ToString();
            Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">alert('�� ������� ��������� ������ ������� �����!');</script>"));
        }
    }
}
