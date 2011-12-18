using System;
using System.IO;
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

using ����;
using ����.�����;
using ����.�����.�������������;
using ����.���������.���������������;
using ����.�������;

public partial class Forms_MacroProcessing_MacroParams : ������������������������������
{
    public Forms_MacroProcessing_MacroParams()
        : base()
    {
        this.������������� = "";
        this.����������������� = "��������� �������������� ���������";

        this.���������� = 700;
        this.���������� = 500;

        this.������������������� += new ����.���������.�����������������(Forms_MacroProcessing_MacroProcess_�������������������);
    }

    �������������������������������� ���������������� = null;
    ������������������� ������������� = null;
    string ������ = null;

    void Forms_MacroProcessing_MacroProcess_�������������������(object �����������, ����.���������.���������������� ���������)
    {
        if (!IsPostBack)
        {
            try
            {
                string ������������������ = HttpContext.Current.Server.MapPath("Upload");

                if (!Directory.Exists(������������������))
                {
                    Directory.CreateDirectory(������������������);
                }
            }
            catch
            {
            }
        }

        ������������� = ���������������������();

        ������ = string.Empty;

        if (this.���������������������.�������������("Params"))
        {
            ������ = this.���������������������["Params"];
        }

        if (!string.IsNullOrEmpty(������))
        {
            ���������������� = �������������.����������������������������������������(������);

            �������������������������������();
        }
    }

    private void �������������������������������()
    {
        foreach (�������������������������������� �������� in ����������������)
        {
            HtmlTableRow row = new HtmlTableRow();

            if (��������.������������������)
            {
                HtmlTableCell cell = new HtmlTableCell();
                cell.InnerText = ��������.���������;
                cell.ColSpan = 2;
                cell.BgColor = "#b8d4f2";
                cell.Style.Add("border", "solid 1px #72b0eb");

                row.Cells.Add(cell);
            }
            else
            {
                HtmlTableCell cellCaption = new HtmlTableCell();
                cellCaption.InnerText = ��������.���������;
                cellCaption.BgColor = "#e5f0ff";
                cellCaption.Style.Add("border", "solid 1px #72b0eb");

                HtmlTableCell cellValue = new HtmlTableCell();
                cellValue.Style.Add("width", "50%");
                cellValue.Style.Add("border", "solid 1px #72b0eb");

                if (��������.�������������� is ��������������)
                {
                    �������������� �������������� = (��������.�������������� as ��������������);

                    ����.�������.���������.�������������� ������������ = new ����.�������.���������.��������������();
                    ������������.�������� = (decimal)��������.�������������������;
                    ������������.Width = Unit.Pixel(300);

                    cellValue.Controls.Add(������������);
                }
                else if (��������.�������������� is ��������������)
                {
                    �������������� �������������� = (��������.�������������� as ��������������);

                    ����.�������.���������.��������� ������������ = new ����.�������.���������.���������();
                    ������������.���� = (DateTime)��������.�������������������;
                    ������������.Width = Unit.Pixel(300);

                    cellValue.Controls.Add(������������);
                }
                else if (��������.�������������� is ���������������)
                {
                    ��������������� ��������������� = (��������.�������������� as ���������������);

                    ����.�������.���������.��������������� ������������������ = new ����.�������.���������.���������������();
                    ������������������.����� = (string)��������.�������������������;
                    ������������������.Width = Unit.Pixel(300);

                    cellValue.Controls.Add(������������������);
                }
                else if (��������.�������������� is ������)
                {
                    ������ ������ = (��������.�������������� as ������);

                    ����.�������.���������.������ ��������� = new ����.�������.���������.������();
                    ���������.�������� = (bool)��������.�������������������;

                    cellValue.Controls.Add(���������);
                }
                else if (��������.�������������� is ����������������)
                {
                    ���������������� ���������������� = (��������.�������������� as ����������������);

                    ����.�������.���������.���������������� ������������������� = new ����.�������.���������.����������������();
                    �������������������.Width = Unit.Pixel(300);
                    �������������������.ID = "�������������������_" + ��������.����������������������;
                    �������������������.DataSource = ����������������.DataSource;
                    �������������������.DataBind();

                    cellValue.Controls.Add(�������������������);
                }
                else if (��������.�������������� is ��������������)
                {
                    RadUpload upload = new RadUpload();
                    upload.InitialFileInputsCount = 1;
                    upload.MaxFileInputsCount = 1;
                    upload.ControlObjectsVisibility = ControlObjectsVisibility.None;
                    upload.Skin = "WebBlue";
                    upload.TargetFolder = "Upload";
                    upload.OverwriteExistingFiles = true;
                    upload.RadControlsDir = "~/Resources/RadControls/";
                    upload.Width = Unit.Pixel(300);
                    cellValue.Controls.Add(upload);
                }

                row.Attributes.Add("param", ��������.����������������������);

                row.Cells.Add(cellCaption);
                row.Cells.Add(cellValue);
            }

            body_main.Controls.Add(row);
        }
    }

    protected void ������_��_Click(object sender, EventArgs e)
    {
        if( ���������������� == null )
        {
            return;
        }

        try
        {
            foreach (Control control in body_main.Controls)
            {
                if (control is HtmlTableRow)
                {
                    HtmlTableRow row = control as HtmlTableRow;

                    if (row.Attributes["param"] != null)
                    {
                        string ���������������������� = row.Attributes["param"];

                        HtmlTableCell cellValue = row.Cells[1];

                        Control �������� = cellValue.Controls[0];

                        if (�������� is ����.�������.���������.������)
                        {
                            ����.�������.���������.������ ������ = (�������� as ����.�������.���������.������);

                            ����������������[����������������������] = ������.��������;
                        }
                        else if (�������� is ����.�������.���������.��������������)
                        {
                            ����.�������.���������.�������������� �������������� = (�������� as ����.�������.���������.��������������);

                            ����������������[����������������������] = ��������������.��������;
                        }
                        else if (�������� is ����.�������.���������.���������������)
                        {
                            ����.�������.���������.��������������� ��������������� = (�������� as ����.�������.���������.���������������);

                            ����������������[����������������������] = ���������������.�����;
                        }
                        else if (�������� is ����.�������.���������.����������������)
                        {
                            ����.�������.���������.���������������� ������������������� = (�������� as ����.�������.���������.����������������);

                            ����������������[����������������������] = �������������������.�����;
                        }
                        else if (�������� is RadUpload)
                        {
                            RadUpload upload = �������� as RadUpload;

                            if (upload.UploadedFiles.Count > 0)
                            {
                                UploadedFile file = upload.UploadedFiles[0];

                                string ���������� = string.Format("{0}/{1}", upload.TargetFolder, file.FileName);

                                ���������� = HttpContext.Current.Server.MapPath(����������);

                                ����������������[����������������������] = ����������;
                            }
                        }
                    }
                }
            }

            ��������������������������� ��������� = null;

            try
            {
                ��������� = �������������.������������������������(������, ������);
            }
            catch (Exception exc)
            {
                if (exc.InnerException == null)
                {
                    this.Controls.Add(new LiteralControl(string.Format("<script type=\"text/javascript\">alert('�� ������� ��������� ��������! {0}');</script>", exc.Message)));
                }
                else
                {
                    this.Controls.Add(new LiteralControl(string.Format("<script type=\"text/javascript\">alert('�� ������� ��������� ��������! {0}');</script>", exc.InnerException.Message)));
                }
            }

            if (��������� != null)
            {
                if (���������.������������������������)
                {
                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">CloseAndRebindSheet();</script>"));
                }
                else
                {
                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">Close();</script>"));
                }

                if (���������.����������������� is ��������������������������������)
                {
                    �������������������������������� ��������������� = (���������.����������������� as ��������������������������������);

                    ����������������������������.��������������������������(this.�������������, "���������������", ���������������);

                    this.Controls.AddAt(3, new LiteralControl(
                        string.Format("<script type=\"text/javascript\">GetRadWindow().BrowserWindow.ShowForm( 'Forms/MacroProcessing/CompareResult.ascx', '{0}:{1}', '' );</script>",
                            this.�������������, "���������������")));


                }
            }
        }
        catch
        {
            Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">Close();</script>"));
        }
    }
}
