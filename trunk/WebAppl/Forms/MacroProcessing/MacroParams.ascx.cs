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

using Ѕарс;
using Ѕарс.—воды;
using Ѕарс.—воды.ќтчетна€‘орма;
using Ѕарс.»нтерфейс.Ёлементы“аблицы;
using Ѕарс.¬ебядро;

public partial class Forms_MacroProcessing_MacroParams : ¬еб‘ормаќбработкиќтчетной‘ормы
{
    public Forms_MacroProcessing_MacroParams()
        : base()
    {
        this.Ўапка—траницы = "";
        this.«аголовок—траницы = "ѕараметры дополнительной обработки";

        this.Ўиринаќкна = 700;
        this.¬ысотаќкна = 500;

        this.ѕри«агрузке—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(Forms_MacroProcessing_MacroProcess_ѕри«агрузке—траницы);
    }

    —писокѕользовательскихѕараметров списокѕараметров = null;
    ќтчетна€‘ормаƒанных отчетна€‘орма = null;
    string макрос = null;

    void Forms_MacroProcessing_MacroProcess_ѕри«агрузке—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        if (!IsPostBack)
        {
            try
            {
                string ѕуть ¬ыходнойѕапке = HttpContext.Current.Server.MapPath("Upload");

                if (!Directory.Exists(ѕуть ¬ыходнойѕапке))
                {
                    Directory.CreateDirectory(ѕуть ¬ыходнойѕапке);
                }
            }
            catch
            {
            }
        }

        отчетна€‘орма = ѕолучитьќтчетную‘орму();

        макрос = string.Empty;

        if (this.ѕараметры√лавногоќкна.ѕараметр«адан("Params"))
        {
            макрос = this.ѕараметры√лавногоќкна["Params"];
        }

        if (!string.IsNullOrEmpty(макрос))
        {
            списокѕараметров = отчетна€‘орма.ѕолучить—писокѕользовательскихѕараметров(макрос);

            ѕостроить‘орму«апросаѕараметров();
        }
    }

    private void ѕостроить‘орму«апросаѕараметров()
    {
        foreach (ѕользовательскийѕараметрƒл€¬вода параметр in списокѕараметров)
        {
            HtmlTableRow row = new HtmlTableRow();

            if (параметр.Ёто—трока атегории)
            {
                HtmlTableCell cell = new HtmlTableCell();
                cell.InnerText = параметр.«аголовок;
                cell.ColSpan = 2;
                cell.BgColor = "#b8d4f2";
                cell.Style.Add("border", "solid 1px #72b0eb");

                row.Cells.Add(cell);
            }
            else
            {
                HtmlTableCell cellCaption = new HtmlTableCell();
                cellCaption.InnerText = параметр.«аголовок;
                cellCaption.BgColor = "#e5f0ff";
                cellCaption.Style.Add("border", "solid 1px #72b0eb");

                HtmlTableCell cellValue = new HtmlTableCell();
                cellValue.Style.Add("width", "50%");
                cellValue.Style.Add("border", "solid 1px #72b0eb");

                if (параметр.–едактор—троки is ѕоле¬вода„исла)
                {
                    ѕоле¬вода„исла поле¬вода„исла = (параметр.–едактор—троки as ѕоле¬вода„исла);

                    Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода„исла вебѕоле¬вода = new Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода„исла();
                    вебѕоле¬вода.«начение = (decimal)параметр.«начениеѕо”молчанию;
                    вебѕоле¬вода.Width = Unit.Pixel(300);

                    cellValue.Controls.Add(вебѕоле¬вода);
                }
                else if (параметр.–едактор—троки is ѕоле¬ыбораƒаты)
                {
                    ѕоле¬ыбораƒаты поле¬ыбораƒаты = (параметр.–едактор—троки as ѕоле¬ыбораƒаты);

                    Ѕарс.¬ебядро.»нтерфейс.¬ыборƒаты веб¬ыборƒаты = new Ѕарс.¬ебядро.»нтерфейс.¬ыборƒаты();
                    веб¬ыборƒаты.ƒата = (DateTime)параметр.«начениеѕо”молчанию;
                    веб¬ыборƒаты.Width = Unit.Pixel(300);

                    cellValue.Controls.Add(веб¬ыборƒаты);
                }
                else if (параметр.–едактор—троки is ѕоле¬вода“екста)
                {
                    ѕоле¬вода“екста поле¬вода“екста = (параметр.–едактор—троки as ѕоле¬вода“екста);

                    Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода“екста вебѕоле¬вода“екста = new Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода“екста();
                    вебѕоле¬вода“екста.“екст = (string)параметр.«начениеѕо”молчанию;
                    вебѕоле¬вода“екста.Width = Unit.Pixel(300);

                    cellValue.Controls.Add(вебѕоле¬вода“екста);
                }
                else if (параметр.–едактор—троки is ‘лажок)
                {
                    ‘лажок флажок = (параметр.–едактор—троки as ‘лажок);

                    Ѕарс.¬ебядро.»нтерфейс.‘лажок веб‘лажок = new Ѕарс.¬ебядро.»нтерфейс.‘лажок();
                    веб‘лажок.«начение = (bool)параметр.«начениеѕо”молчанию;

                    cellValue.Controls.Add(веб‘лажок);
                }
                else if (параметр.–едактор—троки is ¬ыпадающий—писок)
                {
                    ¬ыпадающий—писок выпадающий—писок = (параметр.–едактор—троки as ¬ыпадающий—писок);

                    Ѕарс.¬ебядро.»нтерфейс.¬ыпадающий—писок веб¬ыпадающий—писок = new Ѕарс.¬ебядро.»нтерфейс.¬ыпадающий—писок();
                    веб¬ыпадающий—писок.Width = Unit.Pixel(300);
                    веб¬ыпадающий—писок.ID = "веб¬ыпадающий—писок_" + параметр.»дентификаторѕараметра;
                    веб¬ыпадающий—писок.DataSource = выпадающий—писок.DataSource;
                    веб¬ыпадающий—писок.DataBind();

                    cellValue.Controls.Add(веб¬ыпадающий—писок);
                }
                else if (параметр.–едактор—троки is  нопка–едактор)
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

                row.Attributes.Add("param", параметр.»дентификаторѕараметра);

                row.Cells.Add(cellCaption);
                row.Cells.Add(cellValue);
            }

            body_main.Controls.Add(row);
        }
    }

    protected void  нопка_ќ _Click(object sender, EventArgs e)
    {
        if( списокѕараметров == null )
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
                        string идентификаторѕараметра = row.Attributes["param"];

                        HtmlTableCell cellValue = row.Cells[1];

                        Control редактор = cellValue.Controls[0];

                        if (редактор is Ѕарс.¬ебядро.»нтерфейс.‘лажок)
                        {
                            Ѕарс.¬ебядро.»нтерфейс.‘лажок флажок = (редактор as Ѕарс.¬ебядро.»нтерфейс.‘лажок);

                            списокѕараметров[идентификаторѕараметра] = флажок.«начение;
                        }
                        else if (редактор is Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода„исла)
                        {
                            Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода„исла поле¬вода„исла = (редактор as Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода„исла);

                            списокѕараметров[идентификаторѕараметра] = поле¬вода„исла.«начение;
                        }
                        else if (редактор is Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода“екста)
                        {
                            Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода“екста поле¬вода“екста = (редактор as Ѕарс.¬ебядро.»нтерфейс.ѕоле¬вода“екста);

                            списокѕараметров[идентификаторѕараметра] = поле¬вода“екста.“екст;
                        }
                        else if (редактор is Ѕарс.¬ебядро.»нтерфейс.¬ыпадающий—писок)
                        {
                            Ѕарс.¬ебядро.»нтерфейс.¬ыпадающий—писок веб¬ыпадающий—писок = (редактор as Ѕарс.¬ебядро.»нтерфейс.¬ыпадающий—писок);

                            списокѕараметров[идентификаторѕараметра] = веб¬ыпадающий—писок.“екст;
                        }
                        else if (редактор is RadUpload)
                        {
                            RadUpload upload = редактор as RadUpload;

                            if (upload.UploadedFiles.Count > 0)
                            {
                                UploadedFile file = upload.UploadedFiles[0];

                                string ѕуть ‘айлу = string.Format("{0}/{1}", upload.TargetFolder, file.FileName);

                                ѕуть ‘айлу = HttpContext.Current.Server.MapPath(ѕуть ‘айлу);

                                списокѕараметров[идентификаторѕараметра] = ѕуть ‘айлу;
                            }
                        }
                    }
                }
            }

            –езультат¬ыполнени€ќперации результат = null;

            try
            {
                результат = отчетна€‘орма.¬ыполнитьќперациюћакроса(макрос, макрос);
            }
            catch (Exception exc)
            {
                if (exc.InnerException == null)
                {
                    this.Controls.Add(new LiteralControl(string.Format("<script type=\"text/javascript\">alert('Ќе удалось выполнить операцию! {0}');</script>", exc.Message)));
                }
                else
                {
                    this.Controls.Add(new LiteralControl(string.Format("<script type=\"text/javascript\">alert('Ќе удалось выполнить операцию! {0}');</script>", exc.InnerException.Message)));
                }
            }

            if (результат != null)
            {
                if (результат.Ќеобходимоќбновитьƒанные)
                {
                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">CloseAndRebindSheet();</script>"));
                }
                else
                {
                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">Close();</script>"));
                }

                if (результат.–езультатќперации is –езультаты¬ыполнени€—веркиƒанных)
                {
                    –езультаты¬ыполнени€—веркиƒанных результат—верки = (результат.–езультатќперации as –езультаты¬ыполнени€—веркиƒанных);

                    ћенеджер—ессионныхѕеременных.—охранитьѕеременную¬—ессии(this.»дентификатор, "–езультат—верки", результат—верки);

                    this.Controls.AddAt(3, new LiteralControl(
                        string.Format("<script type=\"text/javascript\">GetRadWindow().BrowserWindow.ShowForm( 'Forms/MacroProcessing/CompareResult.ascx', '{0}:{1}', '' );</script>",
                            this.»дентификатор, "–езультат—верки")));


                }
            }
        }
        catch
        {
            Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">Close();</script>"));
        }
    }
}
