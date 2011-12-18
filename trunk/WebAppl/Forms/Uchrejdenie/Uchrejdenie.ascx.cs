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
using ����;

public partial class Forms_Uchrejdenue_Uchrejdenie : ����.�������.���������.��������
{
    public Forms_Uchrejdenue_Uchrejdenie()
        : base()
    {
        this.������������� = "";
        this.����������������� = "��� ����������";

        this.������������������������ += new ����.���������.�����������������(Forms_Uchrejdenue_Uchrejdenie_������������������������);
        this.������������������� += new ����.���������.�����������������(Forms_Uchrejdenue_Uchrejdenie_�������������������);
    }

    void Forms_Uchrejdenue_Uchrejdenie_�������������������(object �����������, ����.���������.���������������� ���������)
    {
        if (!IsPostBack)
        {
            this.DataBind();
        }
    }

    void Forms_Uchrejdenue_Uchrejdenie_������������������������(object �����������, ����.���������.���������������� ���������)
    {
        if (!����.����.���������������������.��������������(typeof(�������������), typeof(����.����.���������������)))
        {
            this.Controls.AddAt( 0, new LiteralControl( string.Format( "<script>alert('{0}');Close();</script>", "� ��� ��� ���� �� �������� ����� �������������� ������ ����������." ) ) );
            return;
        }

        //if (!IsPostBack)
        {
            if (����.����������������.����������������� != null)
            {
                this.������������������� = ����.����������������.�����������������;
            }

            ������������ ������������ = ������������.�����������������();

            List<��������������������������������> ����������������������� = ��������������������.����������������������������������( ������������,  ����.����������������.����������������� );

            foreach( �������������������������������� ���������������� in ����������������������� )
            {
                HtmlTableRow row = new HtmlTableRow();
                row.Attributes.Add("param", ����������������.��������������������);

                HtmlTableCell cellCap = new HtmlTableCell();
                cellCap.Align = "right";
                cellCap.InnerText = ����������������.��������������������;

                row.Cells.Add( cellCap );

                HtmlTableCell cellValue = new HtmlTableCell();
                cellValue.Align = "left";

                ������������������������� ����������������� = ��������������������.���������������������������������(����������������.����������������);

                if ((�����������������.�������� == ����.���������.���������������.��������������.�����������) || !�����������������.�����������������������)
                {
                    // 
                }
                else
                {
                    string ��������������������� = ����������������.��������������������.Replace(" ", "");

                    switch (�����������������.��������)
                    {
                        case ����.���������.���������������.��������������.���������������:
                            ��������������� ��������������� = new ���������������();
                            ���������������.ID = "���������������_" + ���������������������;
                            ���������������.Width = Unit.Pixel(200);
                            ���������������.����� = ����������������.����������������;
                            cellValue.Controls.Add(���������������);

                            break;

                        case ����.���������.���������������.��������������.��������������:
                            �������������� �������������� = new ��������������();
                            ��������������.ID = "��������������_" + ���������������������;
                            ��������������.Width = Unit.Pixel(200);
                            
                            if (!string.IsNullOrEmpty(����������������.����������������))
                            {
                                ��������������.�������� = decimal.Parse(����������������.����������������);
                            }
                            
                            cellValue.Controls.Add(��������������);
                            break;

                        case ����.���������.���������������.��������������.��������������:
                            ��������� �������������� = new ���������();
                            ��������������.ID = "��������������_" + ���������������������;
                            ��������������.Width = Unit.Pixel(200);

                            if (!string.IsNullOrEmpty(����������������.����������������))
                            {
                                ��������������.���� = DateTime.Parse(����������������.����������������);
                            }

                            cellValue.Controls.Add(��������������);
                            break;

                        case ����.���������.���������������.��������������.������:
                            ������ ��������������� = new ������();
                            ���������������.ID = "���������������_" + ���������������������;
                            ���������������.�������� = bool.Parse(����������������.����������������);
                            cellValue.Controls.Add(���������������);
                            break;

                        case ����.���������.���������������.��������������.������������������:
                            ������������������ ������������������ = new ������������������();
                            ������������������.ID = "������������������_" + ���������������������;
                            ������������������.Width = Unit.Pixel(170);
                            ������������������.����� = ����������������.����������������;
                            ������������������.Attributes.Add("Code", ����������������.����������������.��������������������);
                            
                            cellValue.Controls.Add(������������������);

                            HiddenField inputValue = new HiddenField();
                            inputValue.ID = "������������������Value_" + ���������������������;

                            cellValue.Controls.Add(inputValue);

                            ������������������.Attributes.Add("Value", inputValue.ClientID);

                            break;

                        default:
                            break;
                    }
                }

                row.Cells.Add(cellValue);

                HtmlTableCell cellFree = new HtmlTableCell();
                cellFree.Width = "25%";
                row.Cells.Add(cellFree);

                cellFree = new HtmlTableCell();
                cellFree.Width = "25%";
                row.Cells.Add(cellFree);

                table_Attr.Rows.Add(row);
            }

            // ���������
            HtmlTableRow lastRow = new HtmlTableRow();

            HtmlTableCell cellLast = new HtmlTableCell();
            cellLast.Style.Add("height", "100%");

            lastRow.Cells.Add(cellLast);

            table_Attr.Rows.Add(lastRow);
        }
    }

    protected void ������_��_Click(object sender, EventArgs e)
    {
        try
        {
            ���������� ����������������������� = this.������������������� as ����������;

            Dictionary<object, object> ������������� = ������������������.�����������������������(this);

            �����������������������.��������������������������(�������������);

            // ���������� ���������

            List<string> �������������� = new List<string>();

            foreach (Control control in table_Attr.Controls)
            {
                if (control is HtmlTableRow)
                {
                    HtmlTableRow row = control as HtmlTableRow;

                    if (row.Attributes["param"] != null)
                    {
                        HtmlTableCell cellValue = row.Cells[1];

                        Control �������� = cellValue.Controls[0];

                        string �������� = string.Empty;

                        if (�������� is ����.�������.���������.������)
                        {
                            ����.�������.���������.������ ������ = (�������� as ����.�������.���������.������);

                            �������� = ������.��������.ToString();
                        }
                        else if (�������� is ����.�������.���������.����������������)
                        {
                            ����.�������.���������.���������������� ������������������� = (�������� as ����.�������.���������.����������������);

                            �������� = �������������������.�����;
                        }
                        else if (�������� is ����.�������.���������.���������������)
                        {
                            ��������������� �������������� = (�������� as ���������������);

                            �������� = ��������������.�����;
                        }
                        else if (�������� is ����.�������.���������.��������������)
                        {
                            �������������� �������������� = (�������� as ��������������);

                            �������� = ��������������.��������.ToString();
                        }
                        else if (�������� is ����.�������.���������.���������)
                        {
                            ��������� ��������� = (�������� as ���������);

                            �������� = ���������.����.ToShortDateString();
                        }
                        else if (�������� is ����.�������.���������.������������������)
                        {
                            Control valueControl = cellValue.Controls[1];

                            if (valueControl is HiddenField)
                            {
                                �������� = (valueControl as HiddenField).Value;
                            }
                        }

                        ��������������.Add(��������);
                    }

                }
            }

            �����������������������.���������������������������������(��������������.ToArray());

            �����������������������.������������������������������();

            �����������������������.���������();

            ��������������������� = false;

            Controls.AddAt(0, new LiteralControl("<script type=\"text/javascript\">Close();</script>"));

            return;
        }
        catch (Exception exc)
        {
            ���������.������������������������������(this, "�� ������� ��������� ������.", exc, !(exc is ����������������������������������));
        }
    }
}
