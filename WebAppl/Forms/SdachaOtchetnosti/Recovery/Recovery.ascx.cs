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
using ����.�����;
using ����.�����.�������������;
using ����.�����.�������������������;

public partial class Forms_SdachaOtchetnosti_Recovery_Recovery : ������������������������������
{
    public Forms_SdachaOtchetnosti_Recovery_Recovery()
        : base()
    {
        this.���������� = 700;
        this.���������� = 600;

        this.����������������� = "�������������� ������ �� ������";
        this.������������� = "��� �������������� ������ �������� ����� � ������� ������ \"������������\"";

        this.������������������������ += new ����.���������.�����������������(Forms_SdachaOtchetnosti_Recovery_Recovery_������������������������);
    }

    void Forms_SdachaOtchetnosti_Recovery_Recovery_������������������������(object �����������, ����.���������.���������������� ���������)
    {
        if (!IsPostBack)
        {
            ������������������� ������������� = ���������������������();

            if (������������� != null)
            {
                ��������������������������������� �������������� = new ���������������������������������(�������������);
                List<���������������������������������.�������������������> ����� = ��������������.�����������������������������������();

                �������_��������.��������������� = �����;
            }

            �������_��������.��������������������� = false;
            �������_��������.����������������� = false;
            �������_��������.������������������� = false;
            �������_��������.�������������������� = false;
            �������_��������.���������������� = false;

            �������������� �������_��������� = new ��������������();
            �������_��������.���������������(�������_���������);
            �������_���������.��������� = "���� � ����� ��������";
            �������_���������.���������������������� = "�����������������";

            �������������� �������_����������� = new ��������������();
            �������_��������.���������������(�������_�����������);
            �������_�����������.��������� = "�����������";
            �������_�����������.���������������������� = "�����������";

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
                    int index = int.Parse(�������_��������.SelectedIndexes[0]);

                    ���������������������������������.������������������� �������������� = (���������������������������������.�������������������)�������_��������.��������������������������������(index);

                    if (�������������� != null)
                    {
                        �������������.������.��������������();
                        �������������.������.����������������(��������������.���������������);
                    }

                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">CloseAndRebindSheet();</script>"));
                }
                else
                {
                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">alert('���������� ������� ����� ��� ��������������!');</script>"));
                }
            }
        }
        catch
        {
            Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">alert('�� ������� ������������ �����!');</script>"));
        }
    }
}