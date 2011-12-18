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

using Ѕарс.¬ебядро;
using Ѕарс.¬ебядро.»нтерфейс;
using Ѕарс.—воды.ќтчетна€‘орма;
using Ѕарс.—воды;
using Ѕарс;
using Ѕарс.ядро;

public partial class ReportForms_List : ¬еб‘орма
{
    public ReportForms_List()
        : base()
    {
        this.«аголовок—траницы = "—писок текущих отчетных форм";
        this.ѕри»нициализации—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(‘орма—писка_ѕри»нициализации—траницы);
        this.ѕри«агрузке—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(‘орма—писка_List_ѕри«агрузке—траницы);
    }

    #region —войство : —писокячеек
    private —писокячеек÷епочки —писокячеек
    {
        get
        {
            —писокячеек÷епочки списокячеек = (—писокячеек÷епочки)Ѕарс.¬ебядро.ћенеджер—ессионныхѕеременных.ѕолучитьѕеременную»з—ессии(this.»дентификатор, "—писокячеек÷епочки");

            if (списокячеек == null)
            {
                списокячеек = new —писокячеек÷епочки();

                Ѕарс.¬ебядро.ћенеджер—ессионныхѕеременных.—охранитьѕеременную¬—ессии(this.»дентификатор, "—писокячеек÷епочки", списокячеек);
            }

            return списокячеек;
        }
    }
    #endregion

    #region ќбработчик ѕри»нициализации—траницы
    void ‘орма—писка_ѕри»нициализации—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
    }
    #endregion

    #region ќбработчик ѕри«агрузке—траницы
    void ‘орма—писка_List_ѕри«агрузке—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        if (!this.IsPostBack && !treeElements.IsCallBack)
        {
            #region  онфигураци€ выпадающего списка
            if (ѕеременные—ессии.“екущее”чреждение != null)
            {
                —писокќтчетныхѕериодов списокќтчетныхѕериодов = new —писокќтчетныхѕериодов();
                списокќтчетныхѕериодов.«агрузить();

                this.¬ыпадающий—писок_отчетныеѕериоды. омпозитность—писка = false;
                this.¬ыпадающий—писок_отчетныеѕериоды.»сточник«аписей = списокќтчетныхѕериодов;
                this.¬ыпадающий—писок_отчетныеѕериоды.DataTextField = "Ќаименование";
                this.¬ыпадающий—писок_отчетныеѕериоды.DataValueField = " од";
                this.¬ыпадающий—писок_отчетныеѕериоды.DataBind();

                this.¬ыпадающий—писок_отчетныеѕериоды.SelectedIndex = списокќтчетныхѕериодов.Count - 1;
            }
            else
            {
                this.¬ыпадающий—писок_отчетныеѕериоды.“екст = "Ќе задано учреждение!";
            }
            #endregion

            #region  онфигураци€ дерева

            treeElements.DataFieldID = " люч»дентификатора";
            treeElements.DataFieldParentID = " люч–одител€";
            treeElements.DataTextField = "Ќаименование";
            treeElements.DataValueField = " люч»дентификатора";

            #endregion

            ѕодменю_внутр”в€зки.PostBack = false;
            ѕунктћеню_внутр”в€зки.NavigateUrl = "javascript:ClickElementMenu('ProverkaIn')";
            ѕунктћеню_внутр–езультаты.PostBack = false;

            ѕодменю_меж”в€зки.PostBack = false;
            ѕунктћеню_межформ”в€зки.NavigateUrl = "javascript:ClickElementMenu('ProverkaOut')";
            ѕунктћеню_межформ–езультаты.PostBack = false;

            ѕунктћеню_ѕоказать—псок”в€зок.PostBack = false;
            ѕунктћеню_истори€”в€зок.PostBack = false;
            ѕунктћеню_экспертиза.PostBack = false;
            –азделитель_”в€зки.PostBack = false;
            ѕодменю_состо€ние.PostBack = false;

            ѕунктћеню_истори€»зменений.PostBack = false;
            –азделитель_печатных‘орм.PostBack = false;
            ѕодменю_ѕечать.NavigateUrl = "javascript:ClickElementMenu('PrintForms')";
            –азделитель.PostBack = false;
            ѕодменю_данные.PostBack = false;

            this.ѕодменю_Ё÷ѕ.PostBack = false;
            //this.ѕунктћеню_подписать.PostBack = false;
            //this.ѕунктћеню_подписать.NavigateUrl = string.Format("javascript:SignForm('{0}','{1}_»сточник«аписей')", »дентификатор, “аблица_элементы.ClientID);
            //this.ѕунктћеню_соподписать.PostBack = false;
            //this.ѕунктћеню_соподписать.NavigateUrl = string.Format("javascript:CoSignForm('{0}','{1}_»сточник«аписей')", »дентификатор, “аблица_элементы.ClientID);
            //this.ѕунктћеню_проверитьѕодпись.PostBack = false;
            //this.ѕунктћеню_проверитьѕодпись.NavigateUrl = string.Format("javascript:CheckSign('{0}','{1}_»сточник«аписей')", »дентификатор, “аблица_элементы.ClientID);
            //this.ѕунктћеню_удалитьѕодпись.PostBack = false;
            //this.ѕунктћеню_удалитьѕодпись.NavigateUrl = string.Format("javascript:DelSign('{0}','{1}_»сточник«аписей')", »дентификатор, “аблица_элементы.ClientID);
            //this.ѕунктћеню_показатьѕодпись.PostBack = true;

            ¬ыпадающий—писок_отчетныеѕериоды_TextChanged(null, null);
        }

        —толбец¬ыпадающего—писка столбец¬ыпадающего—писка = new —толбец¬ыпадающего—писка();
        столбец¬ыпадающего—писка.Ўирина—толбца = 200;

        ¬ыпадающий—писок_отчетныеѕериоды.ƒобавить—толбец(столбец¬ыпадающего—писка);
    }
    #endregion

    #region ћетоды загрузки списка €чеек

    public void «агрузить—писокячеек(List<ячейка÷епочки> списокячеек)
    {
        «агрузить—писокячеек(списокячеек, null, 0);
    }

    public void «агрузить—писокячеек(List<ячейка÷епочки> списокячеек, RadTreeNode ParentNode, int  оличествоЁлементов)
    {
        if (ParentNode == null)
        {
            treeElements.Nodes.Clear();
        }
        else
        {
            ParentNode.Nodes.Clear();
        }

        RadTreeNode головной”зел = null;

        /*
         - ”зел
         - - √оловной”зел
         - ”зел
         - ”зел
         - - √оловной”зел
        */

        int i =  оличествоЁлементов + списокячеек.Count - 1;

        списокячеек.Reverse();

        foreach (ячейка÷епочки €чейка in списокячеек)
        {
            RadTreeNode node = new RadTreeNode();
            node.Text = €чейка.Ќаименование;

            node.Value = i.ToString();

            if (€чейка.Ќаличие¬етви && €чейка.–одитель != Guid.Empty)
            {
                node.ExpandMode = ExpandMode.ServerSideCallBack;
            }
            else
            {
                node.ExpandMode = ExpandMode.ClientSide;
            }

            if (€чейка.–одитель == Guid.Empty)
            {
                node.PostBack = false;
                node.Value = null;
            }

            if (ParentNode == null)
            {
                if (головной”зел != null && €чейка.–одитель != Guid.Empty)
                {
                    головной”зел.Nodes.Add(node);
                }
                else
                {
                    treeElements.Nodes.Add(node);

                    if (€чейка.–одитель == Guid.Empty)
                    {
                        головной”зел = node;
                    }
                }
            }
            else
            {
                ParentNode.Nodes.Add(node);
            }

            i--;
        }

        списокячеек.Reverse();
    }
    #endregion

    #region ћетод получени€ текущего отчетного периода
    private ќтчетныйѕериод ѕолучить“екущийќтчетныйѕериод()
    {
        if (!string.IsNullOrEmpty(¬ыпадающий—писок_отчетныеѕериоды.SelectedValue))
        {
            string  одќтчетногоѕериода = ¬ыпадающий—писок_отчетныеѕериоды.SelectedValue;

            if (!string.IsNullOrEmpty( одќтчетногоѕериода))
            {
                ¬ыборка<ќтчетныйѕериод> отчетныеѕериоды = new ¬ыборка<ќтчетныйѕериод>();
                отчетныеѕериоды.«апрос¬ыборки.ƒобавить”словиеќтбора(" од",  одќтчетногоѕериода);

                if (ѕеременные—ессии.“екущийѕрофиль != null)
                {
                    отчетныеѕериоды.«апрос.ƒополнительное”словиеќтбора = "t0.userprofile_id=:p or t0.userprofile_id is null";
                    отчетныеѕериоды.«апрос.ƒобавитьѕараметр«апроса(ѕеременные—ессии.“екущийѕрофиль);
                }

                отчетныеѕериоды.«агрузить();

                if (отчетныеѕериоды. оличество«аписей != 0)
                {
                    return отчетныеѕериоды[0];
                }
            }
        }

        return null;
    }
    #endregion

    #region ћетод обновлени€ дерева учреждений
    public void ќбновитьƒерево”чреждений(ќтчетныйѕериод “екущийќтчетныйѕериод)
    {
        if (“екущийќтчетныйѕериод == null)
        {
            —писокячеек.Clear();
            treeElements.Nodes.Clear();
            return;
        }

        —писокячеек÷епочки списокячеек = —писокячеек;

        try
        {
            if (ѕеременные—ессии.“екущее”чреждение == null)
            {
                списокячеек.ќчистить();

                списокячеек.«агрузить(“екущийќтчетныйѕериод);
            }
            else
            {
                списокячеек.«агрузить(“екущийќтчетныйѕериод, ѕеременные—ессии.“екущее”чреждение);
            }

            «агрузить—писокячеек(списокячеек);
        }
        catch
        {
        }
    }
    #endregion

    #region ћетод ќбновить»«агрузить‘орму
    public ќтчетна€‘ормаƒанных ќбновить»«агрузить‘орму(“екуща€ќтчетна€‘орма “екуща€‘орма)
    {
        ќтчетна€‘ормаƒанных отчетна€‘орма = null;

        try
        {
            »дентификаторћетаописани€‘ормы идентификатор = new »дентификаторћетаописани€‘ормы();
            идентификатор.»дентификаторћетаописани€ = “екуща€‘орма.»дентификаторћетаописани€;
            идентификатор.ƒатајктуальности = “екуща€‘орма.ќтчетныйѕериод.ƒатаЌачала;

            ’ранилищећетаописаний.ќбновитьЋокальноећетаописание(идентификатор);

            отчетна€‘орма = new ќтчетна€‘ормаƒанных();

            отчетна€‘орма.«агрузитьћетаструктуру(идентификатор);

            отчетна€‘орма. омпонентќтчетногоѕериода = “екуща€‘орма. омпонентќтчетногоѕериода;
            отчетна€‘орма.”чреждение = “екуща€‘орма.”чреждение;
            отчетна€‘орма.Ёлемент÷епочки = “екуща€‘орма.Ёлемент÷епочки.“ипЁлемента÷епочки;

            ’ранилищеƒанных‘орм.ќбновитьЋокальныеƒанные(отчетна€‘орма.ƒанные.»дентификатор);

            string —трокаќграничений = ѕроверкаƒоступа Ёлементамќтчетной‘ормы.ѕолучить—трокуќграниченийƒоступа Ёлементам(отчетна€‘орма.ƒанные.»дентификатор);

            отчетна€‘орма.—трокаќграничений = —трокаќграничений;

            отчетна€‘орма.«агрузитьƒанные();
        }
        catch (Exception)
        {
            отчетна€‘орма = null;
            throw new Exception("Ќе удалось обновить и загрузить отчетную форму");
        }

        return отчетна€‘орма;
    }
    #endregion

    #region ќбработчик “аблица_элементы_ItemDataBound
    //void “аблица_элементы_ItemDataBound(object sender, GridItemEventArgs e)
    //{
    //    try
    //    {
    //        if (e.Item is GridDataItem)
    //        {
    //            int DataSourceIndex = “аблица_элементы.ѕолучить»ндекс»сточника«аписей(e.Item.ItemIndex);

    //            if (DataSourceIndex != -1)
    //            {
    //                e.Item.Attributes.Add("DataSourceIndex", DataSourceIndex.ToString());

    //                “екуща€ќтчетна€‘орма форма = (“екуща€ќтчетна€‘орма)“аблица_элементы.ѕолучить«начение»сточника«аписей(e.Item.ItemIndex);

    //                if (форма != null)
    //                {
    //                    int »ндекс»сточникаƒанных = DataSourceIndex;

    //                    if (»ндекс»сточникаƒанных != -1)
    //                    {
    //                        #region  оманды на выполнение скриптов дл€ конкретной формы
    //                        string  омандаЌаѕроверку¬нутриформенных”в€зок = string.Format("MainForm.aspx?Form=Forms/ReportForms/Uviazki/UviazkiReport.ascx&Index={0}&SessionParam={1}:{2}&gridClientID={3}&Params=In", »ндекс»сточникаƒанных, this.»дентификатор, string.Format("{0}_»сточник«аписей", “аблица_элементы.ClientID), “аблица_элементы.ClientID);
    //                        e.Item.Attributes.Add("ScriptProverkaIn",  омандаЌаѕроверку¬нутриформенных”в€зок);

    //                        string  омандаЌаѕроверкућежформенных”в€зок = string.Format("MainForm.aspx?Form=Forms/ReportForms/Uviazki/UviazkiReport.ascx&Index={0}&SessionParam={1}:{2}&gridClientID={3}&Params=Out", »ндекс»сточникаƒанных, this.»дентификатор, string.Format("{0}_»сточник«аписей", “аблица_элементы.ClientID), “аблица_элементы.ClientID);
    //                        e.Item.Attributes.Add("ScriptProverkaOut",  омандаЌаѕроверкућежформенных”в€зок);

    //                        string  омандаЌаѕечатные‘ормы = string.Format("MainForm.aspx?Form=Forms/ReportForms/PrintForms/PrintForms.ascx&Index={0}&SessionParam={1}:{2}", »ндекс»сточникаƒанных, this.»дентификатор, string.Format("{0}_»сточник«аписей", “аблица_элементы.ClientID));
    //                        e.Item.Attributes.Add("ScriptPrintForms",  омандаЌаѕечатные‘ормы);
    //                        #endregion
    //                    }

    //                    string elements = "IsSuccess,";

    //                    List<—осто€ниеƒанных‘орм> возможныеѕереходы—осто€ни€ = null;
    //                    try
    //                    {
    //                        возможныеѕереходы—осто€ни€ =  онтроль—татуса‘ормы.ѕолучить¬озможные–учные—татусыѕеревода(форма.ƒанные‘ормы);
    //                    }
    //                    catch
    //                    {
    //                    }

    //                    if (возможныеѕереходы—осто€ни€ != null && возможныеѕереходы—осто€ни€.Count > 0)
    //                    {
    //                        elements += "Status,";
    //                        foreach (—осто€ниеƒанных‘орм состо€ние in возможныеѕереходы—осто€ни€)
    //                        {
    //                            switch (состо€ние)
    //                            {
    //                                case —осто€ниеƒанных‘орм.ѕусто: elements += "StatusNone,"; break;
    //                                case —осто€ниеƒанных‘орм.„ерновик: elements += "StatusChernovik,"; break;
    //                                case —осто€ниеƒанных‘орм.«аполнено: elements += "StatusZapolneno,"; break;
    //                                case —осто€ниеƒанных‘орм.ѕроверено: elements += "StatusProvereno,"; break;
    //                                case —осто€ниеƒанных‘орм.Ёкспертиза: elements += "StatusExpert,"; break;
    //                                case —осто€ниеƒанных‘орм.”тверждено: elements += "StatusUtverjdeno,"; break;
    //                            }
    //                        }
    //                    }

    //                    if (форма.ƒанные‘ормы != null)
    //                        elements += "Expertiza,";


    //                    if (форма.ƒанные‘ормы != null && форма.ƒанные‘ормы.—осто€ниеƒанных‘орм == —осто€ниеƒанных‘орм.„ерновик)
    //                    {
    //                        elements += "Data,Clear,";
    //                    }

    //                    if (!string.IsNullOrEmpty(elements))
    //                    {
    //                        e.Item.Attributes.Add("elements", elements.Substring(0, elements.Length - 1));
    //                    }


    //                    »дентификаторћетаописани€‘ормы идентификаторћетаописани€ = форма.ѕолучить»дентификаторћетаописани€‘ормы();

    //                    —писокћетаописанийѕечатных‘орм —писок‘орм = new —писокћетаописанийѕечатных‘орм();
    //                    try
    //                    {
    //                        —писок‘орм.«агрузить(идентификаторћетаописани€);
    //                    }
    //                    catch
    //                    {
    //                    }

    //                    string signData = string.Empty;

    //                    if (форма.ƒанные‘ормы != null)
    //                    {
    //                        if (форма.ƒанные‘ормы.—татусЁ÷ѕ)
    //                        {
    //                            signData = "Sign";
    //                        }
    //                        else if (форма.ƒанные‘ормы.—осто€ниеƒанных‘орм == —осто€ниеƒанных‘орм.ѕроверено)
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

    #region ќбработчик ¬ыпадающий—писок_отчетныеѕериоды_TextChanged
    protected void ¬ыпадающий—писок_отчетныеѕериоды_TextChanged(object sender, EventArgs e)
    {
        ќтчетныйѕериод отчетныйѕериод = ѕолучить“екущийќтчетныйѕериод();

        ќбновитьƒерево”чреждений(отчетныйѕериод);
    }
    #endregion

    #region ƒерево_Ёлементов÷епочки_NodeExpand
    protected void ƒерево_Ёлементов÷епочки_NodeExpand(object o, RadTreeNodeEventArgs e)
    {
        ячейка÷епочки €чейка = —писокячеек[int.Parse(e.NodeClicked.Value)];

        if (€чейка.Ќаличие¬етви && e.NodeClicked.Nodes.Count == 0)
        {
            int  оличествоячеек = —писокячеек.Count;

            List<ячейка÷епочки> новыеячейки = —писокячеек.¬ыполнить«агрузку÷епочек(€чейка.÷епочка—дачи, €чейка. люч, €чейка);

            «агрузить—писокячеек(новыеячейки, e.NodeClicked,  оличествоячеек);
        }

        e.NodeClicked.ExpandMode = ExpandMode.ClientSide;
    }
    #endregion

    #region ќбработчик RadMenu1_ItemClick
    protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
    {
        try
        {
            //int »ндекс¬ыделеной—троки = -1;

            //if (“аблица_элементы.SelectedItems[0] != null)
            //    »ндекс¬ыделеной—троки = “аблица_элементы.SelectedItems[0].DataSetIndex;

            //if (»ндекс¬ыделеной—троки == -1)
            //    return;

            //—осто€ниеƒанных‘орм состо€ние = —осто€ниеƒанных‘орм.ѕусто;

            //switch (e.Item.Value)
            //{
            //    case "StatusChernovik":
            //        {
            //            состо€ние = —осто€ниеƒанных‘орм.„ерновик;
            //        }
            //        break;

            //    case "StatusZapolneno":
            //        {
            //            состо€ние = —осто€ниеƒанных‘орм.«аполнено;
            //        }
            //        break;

            //    case "StatusProvereno":
            //        {
            //            состо€ние = —осто€ниеƒанных‘орм.ѕроверено;
            //        }
            //        break;

            //    case "StatusExpert":
            //        {
            //            состо€ние = —осто€ниеƒанных‘орм.Ёкспертиза;
            //        }
            //        break;

            //    case "StatusUtverjdeno":
            //        {
            //            состо€ние = —осто€ниеƒанных‘орм.”тверждено;
            //        }
            //        break;

            //    case "Clear":
            //        {
            //            “екуща€ќтчетна€‘орма форма = (“екуща€ќтчетна€‘орма)“аблица_элементы.ѕолучить«начение»сточника«аписей(»ндекс¬ыделеной—троки);

            //            if (форма.ƒанные‘ормы != null)
            //            {
            //                if (!ѕроверкаƒоступа ќтчетной‘орме.ѕроверитьƒоступ ‘орме(форма.ќтчетныйѕериод, форма. омпонентќтчетногоѕериода,
            //                форма.»дентификаторћетаописани€, форма.”чреждение, “ипƒоступа ќтчетной‘орме.–едактирование, форма.ƒополнительный—татус))
            //                {
            //                    //throw new Exception("»звините, у вас нет прав на редактировани€ этой формы.");
            //                    return;
            //                }

            //                ќтчетна€‘ормаƒанных отчетна€‘орма = ќбновить»«агрузить‘орму(форма);

            //                if (отчетна€‘орма != null)
            //                {
            //                    отчетна€‘орма.ƒанные.ќчиститьƒанные();

            //                    отчетна€‘орма.—охранитьƒанные();
            //                }

            //            }

            //            “аблица_элементы.Rebind();
            //        }
            //        break;

            //    case "ShowSign":
            //        “екуща€ќтчетна€‘орма текуща€‘орма = (“екуща€ќтчетна€‘орма)“аблица_элементы.ѕолучить«начение»сточника«аписей(»ндекс¬ыделеной—троки);

            //        try
            //        {
            //            Dictionary<string, string>[] —войства = текуща€‘орма.ƒанные‘ормы.—войстваЁ÷ѕ();

            //            if (—войства != null)
            //            {
            //                —писок—ертификатовЁ÷ѕ ќтчет = new —писок—ертификатовЁ÷ѕ(—войства);
            //                Ѕарс.¬ебядро.Ёкспорт‘айла.Ёкспортироватьќтчет(ќтчет);
            //            }
            //        }
            //        catch
            //        {
            //        }

            //        break;
            //}

            //if (состо€ние != —осто€ниеƒанных‘орм.ѕусто)
            //{
            //    “екуща€ќтчетна€‘орма форма = (“екуща€ќтчетна€‘орма)“аблица_элементы.ѕолучить«начение»сточника«аписей(»ндекс¬ыделеной—троки);

            //    if (форма.ƒанные‘ормы != null)
            //    {
            //        if (форма.ƒанные‘ормы != null && !форма.ƒанные‘ормы.”становить—татусƒанных(состо€ние))
            //        {

            //            throw new Exception("»зменение статуса невозможно.\n\n¬ данный момент отчетна€ форма находитс€ в работе на другой рабочей станции.");
            //        }
            //    }

            //    “аблица_элементы.Rebind();
            //}

            ////ќрабатчик дл€ печатной формы
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

        ячейка÷епочки €чейка = —писокячеек[nodeIndex];

        —писок“екущихќтчетных‘орм список‘орм = new —писок“екущихќтчетных‘орм();
        список‘орм.«агрузить(ѕолучить“екущийќтчетныйѕериод(), —писокячеек, €чейка);

        grid_Forms.DataSource = список‘орм;
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