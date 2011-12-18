using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.WebControls;

using ����.�����.�������������;
using ����.�������.���������;
using ����.�������������.������������������������;

public partial class Forms_FileImport_FileImport : ������������������������������
{
    public Forms_FileImport_FileImport()
        : base()
    {
        this.����������������� = "����� �����";

        this.���������� = 350;
        this.���������� = 150;

        this.������������������������ += new ����.���������.�����������������(Forms_FileImport_FileImport_������������������������);
    }

    void Forms_FileImport_FileImport_������������������������(object �����������, ����.���������.���������������� ���������)
    {
        if (!IsPostBack)
        {
            try
            {
                string ������������������ = HttpContext.Current.Server.MapPath(RadUpload_����.TargetFolder);

                if( !Directory.Exists( ������������������ ) )
                {
                    Directory.CreateDirectory(������������������);
                }
            }
            catch
            {
            }
        }
    }

    protected void ������_OK_Click(object sender, EventArgs e)
    {
        try
        {
            �������������������������� �������� = ����������������();

            if (�������� != null && RadUpload_����.UploadedFiles.Count > 0)
            {
                UploadedFile file = RadUpload_����.UploadedFiles[0];

                string ���������� = string.Format("{0}/{1}", RadUpload_����.TargetFolder, file.GetName());

                ���������� = HttpContext.Current.Server.MapPath(����������);

                ��������.�������� = ����������;

                bool ��������� = ��������.�������������();

                if (!���������)
                {
                    throw new Exception("������ �������!");
                }
            }

            this.Controls.Add(new LiteralControl("<script type='text/javascript'>CloseAndRebindSheets();</script>"));
        }
        catch (Exception exc)
        {
            string ����������� = exc.Message;
            ����������� = �����������.Replace("\"", "'");
            ����������� = �����������.Replace("\n", "");
            ����������� = �����������.Replace("\r", "");
            ����������� = �����������.Replace("'", "");

            this.Controls.Add(new LiteralControl(string.Format("<script type=\"text/javascript\">alert('�� ����� ���������� ������� ��������� ������! {0}');</script>", �����������)));
        }
    }

    private �������������������������� ����������������()
    {
        ������������������� ������������� = ���������������������();

        if (������������� == null)
        {
            return null;
        }

        string ��������������������� = string.Empty;

        if( this.���������������������.�������������("Params"))
        {
            ��������������������� = this.���������������������["Params"];
        }

        if( string.IsNullOrEmpty( ��������������������� ) )
        {
            return null;
        }

        List<��������������������������> ���������������� = ����������������������������������.��������������������������������(�������������);

        if (����������������.Count > 0)
        {
            foreach (�������������������������� �������� in ����������������)
            {
                if (��������.������������() == ���������������������)
                {
                    return ��������;
                }
            }
        }

        return null;
    }
}
