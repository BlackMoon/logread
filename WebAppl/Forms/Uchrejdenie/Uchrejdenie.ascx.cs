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
using Ѕарс.¬ебядро.»нтерфейс;
using Ѕарс.—воды;
using Ѕарс;

public partial class Forms_Uchrejdenue_Uchrejdenie : Ѕарс.¬ебядро.»нтерфейс.¬еб‘орма
{
    public Forms_Uchrejdenue_Uchrejdenie()
        : base()
    {
        this.Ўапка—траницы = "";
        this.«аголовок—траницы = "ћое учреждение";

        this.ѕри»нициализации—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(Forms_Uchrejdenue_Uchrejdenie_ѕри»нициализации—траницы);
        this.ѕри«агрузке—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(Forms_Uchrejdenue_Uchrejdenie_ѕри«агрузке—траницы);
    }

    void Forms_Uchrejdenue_Uchrejdenie_ѕри«агрузке—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        if (!IsPostBack)
        {
            this.DataBind();
        }
    }

    void Forms_Uchrejdenue_Uchrejdenie_ѕри»нициализации—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        if (!Ѕарс.ядро.ћенеджерѕользователей.ѕроверитьѕраво(typeof(ћое”чреждение), typeof(Ѕарс.ядро.ѕравоЌаѕросмотр)))
        {
            this.Controls.AddAt( 0, new LiteralControl( string.Format( "<script>alert('{0}');Close();</script>", "” ¬ас нет прав на просмотр формы редактировани€ своего учреждени€." ) ) );
            return;
        }

        //if (!IsPostBack)
        {
            if (Ѕарс.ѕеременные—ессии.“екущее”чреждение != null)
            {
                this.–едактируемыйќбъект = Ѕарс.ѕеременные—ессии.“екущее”чреждение;
            }

            ќписаниеƒј—” описаниеƒј—” = ќписаниеƒј—”.ѕолучитьЁкземпл€р();

            List< омпозитќписание«начениејтрибута> —писок«наченийјтрибутов = ”правлениејтрибутами.ѕолучитьјктуальный—писок омпозитов( описаниеƒј—”,  Ѕарс.ѕеременные—ессии.“екущее”чреждение );

            foreach(  омпозитќписание«начениејтрибута описаниејтрибута in —писок«наченийјтрибутов )
            {
                HtmlTableRow row = new HtmlTableRow();
                row.Attributes.Add("param", описаниејтрибута.Ќаименованиејтрибута);

                HtmlTableCell cellCap = new HtmlTableCell();
                cellCap.Align = "right";
                cellCap.InnerText = описаниејтрибута.Ќаименованиејтрибута;

                row.Cells.Add( cellCap );

                HtmlTableCell cellValue = new HtmlTableCell();
                cellValue.Align = "left";

                ќписание–едакторајтрибута описание–едактора = ”правлениејтрибутами.ѕолучитьќписание–едакторајтрибута(описаниејтрибута.ќписаниејтрибута);

                if ((описание–едактора.–едактор == Ѕарс.»нтерфейс.Ёлементы“аблицы.–едакторячейки.Ќеќпределен) || !описание–едактора.–едактирование–азрешено)
                {
                    // 
                }
                else
                {
                    string идентификаторјтрибута = описаниејтрибута.Ќаименованиејтрибута.Replace(" ", "");

                    switch (описание–едактора.–едактор)
                    {
                        case Ѕарс.»нтерфейс.Ёлементы“аблицы.–едакторячейки.ѕоле¬вода“екста:
                            ѕоле¬вода“екста поле¬вода“екста = new ѕоле¬вода“екста();
                            поле¬вода“екста.ID = "поле¬вода“екста_" + идентификаторјтрибута;
                            поле¬вода“екста.Width = Unit.Pixel(200);
                            поле¬вода“екста.“екст = описаниејтрибута.«начениејтрибута;
                            cellValue.Controls.Add(поле¬вода“екста);

                            break;

                        case Ѕарс.»нтерфейс.Ёлементы“аблицы.–едакторячейки.ѕоле¬вода„исла:
                            ѕоле¬вода„исла поле¬вода„исла = new ѕоле¬вода„исла();
                            поле¬вода„исла.ID = "поле¬вода„исла_" + идентификаторјтрибута;
                            поле¬вода„исла.Width = Unit.Pixel(200);
                            
                            if (!string.IsNullOrEmpty(описаниејтрибута.«начениејтрибута))
                            {
                                поле¬вода„исла.«начение = decimal.Parse(описаниејтрибута.«начениејтрибута);
                            }
                            
                            cellValue.Controls.Add(поле¬вода„исла);
                            break;

                        case Ѕарс.»нтерфейс.Ёлементы“аблицы.–едакторячейки.ѕоле¬ыбораƒаты:
                            ¬ыборƒаты поле¬ыбораƒаты = new ¬ыборƒаты();
                            поле¬ыбораƒаты.ID = "поле¬ыбораƒаты_" + идентификаторјтрибута;
                            поле¬ыбораƒаты.Width = Unit.Pixel(200);

                            if (!string.IsNullOrEmpty(описаниејтрибута.«начениејтрибута))
                            {
                                поле¬ыбораƒаты.ƒата = DateTime.Parse(описаниејтрибута.«начениејтрибута);
                            }

                            cellValue.Controls.Add(поле¬ыбораƒаты);
                            break;

                        case Ѕарс.»нтерфейс.Ёлементы“аблицы.–едакторячейки.‘лажок:
                            ‘лажок поле¬вода‘лажок = new ‘лажок();
                            поле¬вода‘лажок.ID = "поле¬вода‘лажок_" + идентификаторјтрибута;
                            поле¬вода‘лажок.«начение = bool.Parse(описаниејтрибута.«начениејтрибута);
                            cellValue.Controls.Add(поле¬вода‘лажок);
                            break;

                        case Ѕарс.»нтерфейс.Ёлементы“аблицы.–едакторячейки.¬ыбор»з—правочника:
                            ¬ыбор»з—правочника выбор»з—правочника = new ¬ыбор»з—правочника();
                            выбор»з—правочника.ID = "выбор»з—правочника_" + идентификаторјтрибута;
                            выбор»з—правочника.Width = Unit.Pixel(170);
                            выбор»з—правочника.“екст = описаниејтрибута.«начениејтрибута;
                            выбор»з—правочника.Attributes.Add("Code", описаниејтрибута.ќписаниејтрибута.ќписание“ипа«начени€);
                            
                            cellValue.Controls.Add(выбор»з—правочника);

                            HiddenField inputValue = new HiddenField();
                            inputValue.ID = "выбор»з—правочникаValue_" + идентификаторјтрибута;

                            cellValue.Controls.Add(inputValue);

                            выбор»з—правочника.Attributes.Add("Value", inputValue.ClientID);

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

            // ѕоследний
            HtmlTableRow lastRow = new HtmlTableRow();

            HtmlTableCell cellLast = new HtmlTableCell();
            cellLast.Style.Add("height", "100%");

            lastRow.Cells.Add(cellLast);

            table_Attr.Rows.Add(lastRow);
        }
    }

    protected void  нопка_ќ _Click(object sender, EventArgs e)
    {
        try
        {
            ”чреждение редактируемое”чреждение = this.–едактируемыйќбъект as ”чреждение;

            Dictionary<object, object> Ќовые«начени€ =  онтроль—в€зывани€.ѕолучить«начени€—войств(this);

            редактируемое”чреждение.«аполнитьѕо“аблице«начений(Ќовые«начени€);

            // «аполнение атрибутов

            List<string> список«начений = new List<string>();

            foreach (Control control in table_Attr.Controls)
            {
                if (control is HtmlTableRow)
                {
                    HtmlTableRow row = control as HtmlTableRow;

                    if (row.Attributes["param"] != null)
                    {
                        HtmlTableCell cellValue = row.Cells[1];

                        Control редактор = cellValue.Controls[0];

                        string «начение = string.Empty;

                        if (редактор is Ѕарс.¬ебядро.»нтерфейс.‘лажок)
                        {
                            Ѕарс.¬ебядро.»нтерфейс.‘лажок флажок = (редактор as Ѕарс.¬ебядро.»нтерфейс.‘лажок);

                            «начение = флажок.«начение.ToString();
                        }
                        else if (редактор is Ѕарс.¬ебядро.»нтерфейс.¬ыпадающий—писок)
                        {
                            Ѕарс.¬ебядро.»нтерфейс.¬ыпадающий—писок веб¬ыпадающий—писок = (редактор as Ѕарс.¬ебядро.»нтерфейс.¬ыпадающий—писок);

                            «начение = веб¬ыпадающий—писок.“екст;
                        }
                        else if (редактор is Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода“екста)
                        {
                            ѕоле¬вода“екста поле¬вод“екста = (редактор as ѕоле¬вода“екста);

                            «начение = поле¬вод“екста.“екст;
                        }
                        else if (редактор is Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода„исла)
                        {
                            ѕоле¬вода„исла поле¬вода„исла = (редактор as ѕоле¬вода„исла);

                            «начение = поле¬вода„исла.«начение.ToString();
                        }
                        else if (редактор is Ѕарс.¬ебядро.»нтерфейс.¬ыборƒаты)
                        {
                            ¬ыборƒаты выборƒаты = (редактор as ¬ыборƒаты);

                            «начение = выборƒаты.ƒата.ToShortDateString();
                        }
                        else if (редактор is Ѕарс.¬ебядро.»нтерфейс.¬ыбор»з—правочника)
                        {
                            Control valueControl = cellValue.Controls[1];

                            if (valueControl is HiddenField)
                            {
                                «начение = (valueControl as HiddenField).Value;
                            }
                        }

                        список«начений.Add(«начение);
                    }

                }
            }

            редактируемое”чреждение.”становить—писок«наченийјтрибутов(список«начений.ToArray());

            редактируемое”чреждение.ѕроверить¬озможность—охранени€();

            редактируемое”чреждение.—охранить();

            ¬ыставл€ть–азмерыќкна = false;

            Controls.AddAt(0, new LiteralControl("<script type=\"text/javascript\">Close();</script>"));

            return;
        }
        catch (Exception exc)
        {
            —ообщение.ѕоказать»сключительную—итуацию(this, "Ќе удалось сохранить объект.", exc, !(exc is »сключениеѕроверки’ранимогоќбъекта));
        }
    }
}
