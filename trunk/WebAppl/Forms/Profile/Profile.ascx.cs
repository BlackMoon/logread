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
using ����.�������.���������;
using ����.�����;
using ����.����;
using ����;

public partial class Forms_Profile_Profile : ����.�������.���������.��������
{
    public Forms_Profile_Profile()
        : base()
    {
        this.������������� = "";
        this.����������������� = "��� �������";
        this.���������� = 600;
        this.���������� = 300;

        this.������������������������ += new ����.���������.�����������������(Forms_Profile_Profile_������������������������);
        this.������������������� += new ����.���������.�����������������(Forms_Profile_Profile_�������������������);
    }

    void Forms_Profile_Profile_������������������������(object �����������, ����.���������.���������������� ���������)
    {
        if (!IsPostBack)
        {
            ������������ ������������������� = ���������������������.�������������������;

            this.������������������� = �������������������;
        }
    }

    void Forms_Profile_Profile_�������������������(object �����������, ����.���������.���������������� ���������)
    {
        if (!IsPostBack)
        {
            this.DataBind();
        }
    }

    protected void ������_��_Click(object sender, EventArgs e)
    {
        try
        {
            ������������ ������������������� = this.������������������� as ������������;

            Dictionary<object, object> ������������� = ������������������.�����������������������(this);

            string ������������ = (string)�������������["������������"];

            ������������ ������������ = new ������������();
            ������������.������ = ������������;

            if (�������������������.������ != ������������.������)
            {
                throw new Exception("�� ����� ������������ ������� ������!");
            }

            string ����������� = (string)�������������["�����������"];
            string �����������2 = (string)�������������["�����������2"];

            if (����������� != �����������2)
            {
                throw new Exception("�������� � ����� ������ � ������������� ������ �� ���������.");
            }

            try
            {
                �������������������.������ = �����������;
			    �������������������.�������������();
			    �������������������.���������();
			    �������������������.���������������();
            }
            catch
            {
                �������������������.����������������();
                throw;
            }

            ��������������������� = false;

            Controls.AddAt(0, new LiteralControl("<script type=\"text/javascript\">alert('������� ������� �������!');Close();</script>"));

            return;
        }
        catch( Exception exc )
        {
            ���������.������������������������������(this, "�� ������� ��������� �������.", exc);
        }
    }
}
