using System;
using System.Data;
using System.ComponentModel;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.RadTreeViewUtils;
using Telerik.WebControls;

using ����.�������;
using ����.�������.���������;
using ����.�����.�������������;
using ����.�����;
using ����;
using ����.����;

public partial class ReportForms_List : ��������
{
    public ReportForms_List()
        : base()
    {
        this.����������������� = "������ ������� �������� ����";
        this.������������������������ += new ����.���������.�����������������(�����������_������������������������);
        this.������������������� += new ����.���������.�����������������(�����������_List_�������������������);
    }

    #region �������� : �����������
    private ������������������ �����������
    {
        get
        {
            ������������������ ����������� = (������������������)����.�������.����������������������������.��������������������������(this.�������������, "������������������");

            if (����������� == null)
            {
                ����������� = new ������������������();

                ����.�������.����������������������������.��������������������������(this.�������������, "������������������", �����������);
            }

            return �����������;
        }
    }
    #endregion

    #region ���������� ������������������������
    void �����������_������������������������(object �����������, ����.���������.���������������� ���������)
    {
    }
    #endregion

    #region ���������� �������������������
    void �����������_List_�������������������(object �����������, ����.���������.���������������� ���������)
    {
        if (!this.IsPostBack && !treeElements.IsCallBack)
        {
            #region ������������ ����������� ������
            if (����������������.����������������� != null)
            {
                ���������������������� ���������������������� = new ����������������������();
                ����������������������.���������();

                this.����������������_���������������.������������������� = false;
                this.����������������_���������������.��������������� = ����������������������;
                this.����������������_���������������.DataTextField = "������������";
                this.����������������_���������������.DataValueField = "���";
                this.����������������_���������������.DataBind();

                this.����������������_���������������.SelectedIndex = ����������������������.Count - 1;
            }
            else
            {
                this.����������������_���������������.����� = "�� ������ ����������!";
            }
            #endregion

            #region ������������ ������

            treeElements.DataFieldID = "������������������";
            treeElements.DataFieldParentID = "������������";
            treeElements.DataTextField = "������������";
            treeElements.DataValueField = "������������������";

            #endregion

            �������_�����������.PostBack = false;
            ���������_�����������.NavigateUrl = "javascript:ClickElementMenu('ProverkaIn')";
            ���������_���������������.PostBack = false;

            �������_���������.PostBack = false;
            ���������_�������������.NavigateUrl = "javascript:ClickElementMenu('ProverkaOut')";
            ���������_�����������������.PostBack = false;

            ���������_�������������������.PostBack = false;
            ���������_�������������.PostBack = false;
            ���������_����������.PostBack = false;
            �����������_������.PostBack = false;
            �������_���������.PostBack = false;

            ���������_����������������.PostBack = false;
            �����������_������������.PostBack = false;
            �������_������.NavigateUrl = "javascript:ClickElementMenu('PrintForms')";
            �����������.PostBack = false;
            �������_������.PostBack = false;

            this.�������_���.PostBack = false;
            //this.���������_���������.PostBack = false;
            //this.���������_���������.NavigateUrl = string.Format("javascript:SignForm('{0}','{1}_���������������')", �������������, �������_��������.ClientID);
            //this.���������_�����������.PostBack = false;
            //this.���������_�����������.NavigateUrl = string.Format("javascript:CoSignForm('{0}','{1}_���������������')", �������������, �������_��������.ClientID);
            //this.���������_����������������.PostBack = false;
            //this.���������_����������������.NavigateUrl = string.Format("javascript:CheckSign('{0}','{1}_���������������')", �������������, �������_��������.ClientID);
            //this.���������_��������������.PostBack = false;
            //this.���������_��������������.NavigateUrl = string.Format("javascript:DelSign('{0}','{1}_���������������')", �������������, �������_��������.ClientID);
            //this.���������_���������������.PostBack = true;

            ����������������_���������������_TextChanged(null, null);
        }

        ������������������������ ������������������������ = new ������������������������();
        ������������������������.������������� = 200;

        ����������������_���������������.���������������(������������������������);
    }
    #endregion

    #region ������ �������� ������ �����

    public void ��������������������(List<�������������> �����������)
    {
        ��������������������(�����������, null, 0);
    }

    public void ��������������������(List<�������������> �����������, RadTreeNode ParentNode, int �������������������)
    {
        if (ParentNode == null)
        {
            treeElements.Nodes.Clear();
        }
        else
        {
            ParentNode.Nodes.Clear();
        }

        RadTreeNode ������������ = null;

        /*
         - ����
         - - ������������
         - ����
         - ����
         - - ������������
        */

        int i = ������������������� + �����������.Count - 1;

        �����������.Reverse();

        foreach (������������� ������ in �����������)
        {
            RadTreeNode node = new RadTreeNode();
            node.Text = ������.������������;

            node.Value = i.ToString();

            if (������.������������ && ������.�������� != Guid.Empty)
            {
                node.ExpandMode = ExpandMode.ServerSideCallBack;
            }
            else
            {
                node.ExpandMode = ExpandMode.ClientSide;
            }

            if (������.�������� == Guid.Empty)
            {
                node.PostBack = false;
                node.Value = null;
            }

            if (ParentNode == null)
            {
                if (������������ != null && ������.�������� != Guid.Empty)
                {
                    ������������.Nodes.Add(node);
                }
                else
                {
                    treeElements.Nodes.Add(node);

                    if (������.�������� == Guid.Empty)
                    {
                        ������������ = node;
                    }
                }
            }
            else
            {
                ParentNode.Nodes.Add(node);
            }

            i--;
        }

        �����������.Reverse();
    }
    #endregion

    #region ����� ��������� �������� ��������� �������
    private �������������� �����������������������������()
    {
        if (!string.IsNullOrEmpty(����������������_���������������.SelectedValue))
        {
            string ������������������� = ����������������_���������������.SelectedValue;

            if (!string.IsNullOrEmpty(�������������������))
            {
                �������<��������������> ��������������� = new �������<��������������>();
                ���������������.�������������.���������������������("���", �������������������);

                if (����������������.�������������� != null)
                {
                    ���������������.������.��������������������������� = "t0.userprofile_id=:p or t0.userprofile_id is null";
                    ���������������.������.�����������������������(����������������.��������������);
                }

                ���������������.���������();

                if (���������������.����������������� != 0)
                {
                    return ���������������[0];
                }
            }
        }

        return null;
    }
    #endregion

    #region ����� ���������� ������ ����������
    public void ������������������������(�������������� ���������������������)
    {
        if (��������������������� == null)
        {
            �����������.Clear();
            treeElements.Nodes.Clear();
            return;
        }

        ������������������ ����������� = �����������;

        try
        {
            if (����������������.����������������� == null)
            {
                �����������.��������();

                �����������.���������(���������������������);
            }
            else
            {
                �����������.���������(���������������������, ����������������.�����������������);
            }

            ��������������������(�����������);
        }
        catch
        {
        }
    }
    #endregion

    #region ����� �����������������������
    public ������������������� �����������������������(�������������������� ������������)
    {
        ������������������� ������������� = null;

        try
        {
            ������������������������������ ������������� = new ������������������������������();
            �������������.������������������������� = ������������.�������������������������;
            �������������.���������������� = ������������.��������������.����������;

            ���������������������.�����������������������������(�������������);

            ������������� = new �������������������();

            �������������.����������������������(�������������);

            �������������.������������������������� = ������������.�������������������������;
            �������������.���������� = ������������.����������;
            �������������.�������������� = ������������.��������������.������������������;

            �������������������.�����������������������(�������������.������.�������������);

            string ����������������� = ��������������������������������������.������������������������������������������(�������������.������.�������������);

            �������������.����������������� = �����������������;

            �������������.���������������();
        }
        catch (Exception)
        {
            ������������� = null;
            throw new Exception("�� ������� �������� � ��������� �������� �����");
        }

        return �������������;
    }
    #endregion

    #region ���������� �������_��������_ItemDataBound
    //void �������_��������_ItemDataBound(object sender, GridItemEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Item is GridDataItem)
    //        {
    //            int DataSourceIndex = �������_��������.������������������������������(e.Item.ItemIndex);

    //            if (DataSourceIndex != -1)
    //            {
    //                e.Item.Attributes.Add("DataSourceIndex", DataSourceIndex.ToString());

    //                �������������������� ����� = (��������������������)�������_��������.��������������������������������(e.Item.ItemIndex);

    //                if (����� != null)
    //                {
    //                    int ��������������������� = DataSourceIndex;

    //                    if (��������������������� != -1)
    //                    {
    //                        #region ������� �� ���������� �������� ��� ���������� �����
    //                        string �������������������������������������� = string.Format("MainForm.aspx?Form=Forms/ReportForms/Uviazki/UviazkiReport.ascx&Index={0}&SessionParam={1}:{2}&gridClientID={3}&Params=In", ���������������������, this.�������������, string.Format("{0}_���������������", �������_��������.ClientID), �������_��������.ClientID);
    //                        e.Item.Attributes.Add("ScriptProverkaIn", ��������������������������������������);

    //                        string ����������������������������������� = string.Format("MainForm.aspx?Form=Forms/ReportForms/Uviazki/UviazkiReport.ascx&Index={0}&SessionParam={1}:{2}&gridClientID={3}&Params=Out", ���������������������, this.�������������, string.Format("{0}_���������������", �������_��������.ClientID), �������_��������.ClientID);
    //                        e.Item.Attributes.Add("ScriptProverkaOut", �����������������������������������);

    //                        string ���������������������� = string.Format("MainForm.aspx?Form=Forms/ReportForms/PrintForms/PrintForms.ascx&Index={0}&SessionParam={1}:{2}", ���������������������, this.�������������, string.Format("{0}_���������������", �������_��������.ClientID));
    //                        e.Item.Attributes.Add("ScriptPrintForms", ����������������������);
    //                        #endregion
    //                    }

    //                    string elements = "IsSuccess,";

    //                    List<�������������������> �������������������������� = null;
    //                    try
    //                    {
    //                        �������������������������� = ��������������������.��������������������������������������(�����.�����������);
    //                    }
    //                    catch
    //                    {
    //                    }

    //                    if (�������������������������� != null && ��������������������������.Count > 0)
    //                    {
    //                        elements += "Status,";
    //                        foreach (������������������� ��������� in ��������������������������)
    //                        {
    //                            switch (���������)
    //                            {
    //                                case �������������������.�����: elements += "StatusNone,"; break;
    //                                case �������������������.��������: elements += "StatusChernovik,"; break;
    //                                case �������������������.���������: elements += "StatusZapolneno,"; break;
    //                                case �������������������.���������: elements += "StatusProvereno,"; break;
    //                                case �������������������.����������: elements += "StatusExpert,"; break;
    //                                case �������������������.����������: elements += "StatusUtverjdeno,"; break;
    //                            }
    //                        }
    //                    }

    //                    if (�����.����������� != null)
    //                        elements += "Expertiza,";


    //                    if (�����.����������� != null && �����.�����������.������������������� == �������������������.��������)
    //                    {
    //                        elements += "Data,Clear,";
    //                    }

    //                    if (!string.IsNullOrEmpty(elements))
    //                    {
    //                        e.Item.Attributes.Add("elements", elements.Substring(0, elements.Length - 1));
    //                    }


    //                    ������������������������������ ������������������������� = �����.��������������������������������������();

    //                    ������������������������������ ���������� = new ������������������������������();
    //                    try
    //                    {
    //                        ����������.���������(�������������������������);
    //                    }
    //                    catch
    //                    {
    //                    }

    //                    string signData = string.Empty;

    //                    if (�����.����������� != null)
    //                    {
    //                        if (�����.�����������.���������)
    //                        {
    //                            signData = "Sign";
    //                        }
    //                        else if (�����.�����������.������������������� == �������������������.���������)
    //                        {
    //                            signData = "AllowSign";
    //                        }
    //                    }

    //                    e.Item.Attributes.Add("signData", signData);
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception)
    //    {
    //    }
    //}

    #endregion

    #region ���������� ����������������_���������������_TextChanged
    protected void ����������������_���������������_TextChanged(object sender, EventArgs e)
    {
        �������������� �������������� = �����������������������������();

        ������������������������(��������������);
    }
    #endregion

    #region ������_����������������_NodeExpand
    protected void ������_����������������_NodeExpand(object o, RadTreeNodeEventArgs e)
    {
        ������������� ������ = �����������[int.Parse(e.NodeClicked.Value)];

        if (������.������������ && e.NodeClicked.Nodes.Count == 0)
        {
            int ��������������� = �����������.Count;

            List<�������������> ����������� = �����������.������������������������(������.������������, ������.����, ������);

            ��������������������(�����������, e.NodeClicked, ���������������);
        }

        e.NodeClicked.ExpandMode = ExpandMode.ClientSide;
    }
    #endregion

    #region ���������� RadMenu1_ItemClick
    protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
    {
        try
        {
            //int ��������������������� = -1;

            //if (�������_��������.SelectedItems[0] != null)
            //    ��������������������� = �������_��������.SelectedItems[0].DataSetIndex;

            //if (��������������������� == -1)
            //    return;

            //������������������� ��������� = �������������������.�����;

            //switch (e.Item.Value)
            //{
            //    case "StatusChernovik":
            //        {
            //            ��������� = �������������������.��������;
            //        }
            //        break;

            //    case "StatusZapolneno":
            //        {
            //            ��������� = �������������������.���������;
            //        }
            //        break;

            //    case "StatusProvereno":
            //        {
            //            ��������� = �������������������.���������;
            //        }
            //        break;

            //    case "StatusExpert":
            //        {
            //            ��������� = �������������������.����������;
            //        }
            //        break;

            //    case "StatusUtverjdeno":
            //        {
            //            ��������� = �������������������.����������;
            //        }
            //        break;

            //    case "Clear":
            //        {
            //            �������������������� ����� = (��������������������)�������_��������.��������������������������������(���������������������);

            //            if (�����.����������� != null)
            //            {
            //                if (!�����������������������������.���������������������(�����.��������������, �����.�������������������������,
            //                �����.�������������������������, �����.����������, ������������������������.��������������, �����.��������������������))
            //                {
            //                    //throw new Exception("��������, � ��� ��� ���� �� �������������� ���� �����.");
            //                    return;
            //                }

            //                ������������������� ������������� = �����������������������(�����);

            //                if (������������� != null)
            //                {
            //                    �������������.������.��������������();

            //                    �������������.���������������();
            //                }

            //            }

            //            �������_��������.Rebind();
            //        }
            //        break;

            //    case "ShowSign":
            //        �������������������� ������������ = (��������������������)�������_��������.��������������������������������(���������������������);

            //        try
            //        {
            //            Dictionary<string, string>[] �������� = ������������.�����������.�����������();

            //            if (�������� != null)
            //            {
            //                ��������������������� ����� = new ���������������������(��������);
            //                ����.�������.������������.�������������������(�����);
            //            }
            //        }
            //        catch
            //        {
            //        }

            //        break;
            //}

            //if (��������� != �������������������.�����)
            //{
            //    �������������������� ����� = (��������������������)�������_��������.��������������������������������(���������������������);

            //    if (�����.����������� != null)
            //    {
            //        if (�����.����������� != null && !�����.�����������.����������������������(���������))
            //        {

            //            throw new Exception("��������� ������� ����������.\n\n� ������ ������ �������� ����� ��������� � ������ �� ������ ������� �������.");
            //        }
            //    }

            //    �������_��������.Rebind();
            //}

            ////��������� ��� �������� �����
            //RadMenuItem parentItem = e.Item.Parent as RadMenuItem;

            //if (parentItem != null && parentItem.Value == "Print")
            //{
            //}
        }
        catch
        {
        }
    }
    #endregion

    protected void grid_Forms_BeforePerformDataSelect(object sender, EventArgs e)
    {
        string NodeValue = grid_Forms.Attributes["SelectedNode"];

        if (string.IsNullOrEmpty(NodeValue))
        {
            return;
        }

        int nodeIndex = int.Parse(NodeValue);

        ������������� ������ = �����������[nodeIndex];

        ������������������������� ���������� = new �������������������������();
        ����������.���������(�����������������������������(), �����������, ������);

        grid_Forms.DataSource = ����������;
    }

    protected void grid_Forms_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters.StartsWith("Update") )
        {
            string SelectedNodeValue = e.Parameters.Split(':')[1];

            grid_Forms.Attributes["SelectedNode"] = SelectedNodeValue;

            grid_Forms.DataBind();
        }
    }
}