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

using Ѕарс.—воды.ќтчетна€‘орма;
using Ѕарс.¬ебядро.»нтерфейс;
using Ѕарс.»мпортЁкспорт.»мпортƒанныхќтчетных‘орм;

public partial class Forms_FileImport_FileImport : ¬еб‘ормаќбработкиќтчетной‘ормы
{
    public Forms_FileImport_FileImport()
        : base()
    {
        this.«аголовок—траницы = "¬ыбор файла";

        this.Ўиринаќкна = 350;
        this.¬ысотаќкна = 150;

        this.ѕри»нициализации—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(Forms_FileImport_FileImport_ѕри»нициализации—траницы);
    }

    void Forms_FileImport_FileImport_ѕри»нициализации—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        if (!IsPostBack)
        {
            try
            {
                string ѕуть ¬ыходнойѕапке = HttpContext.Current.Server.MapPath(RadUpload_файл.TargetFolder);

                if( !Directory.Exists( ѕуть ¬ыходнойѕапке ) )
                {
                    Directory.CreateDirectory(ѕуть ¬ыходнойѕапке);
                }
            }
            catch
            {
            }
        }
    }

    protected void  нопка_OK_Click(object sender, EventArgs e)
    {
        try
        {
            »мпортерƒанныхќтчетных‘орм импортер = ѕолучить»мпортер();

            if (импортер != null && RadUpload_файл.UploadedFiles.Count > 0)
            {
                UploadedFile file = RadUpload_файл.UploadedFiles[0];

                string ѕуть ‘айлу = string.Format("{0}/{1}", RadUpload_файл.TargetFolder, file.GetName());

                ѕуть ‘айлу = HttpContext.Current.Server.MapPath(ѕуть ‘айлу);

                импортер.»м€‘айла = ѕуть ‘айлу;

                bool результат = импортер.»мпортировать();

                if (!результат)
                {
                    throw new Exception("ќшибка импорта!");
                }
            }

            this.Controls.Add(new LiteralControl("<script type='text/javascript'>CloseAndRebindSheets();</script>"));
        }
        catch (Exception exc)
        {
            string “екстќшибки = exc.Message;
            “екстќшибки = “екстќшибки.Replace("\"", "'");
            “екстќшибки = “екстќшибки.Replace("\n", "");
            “екстќшибки = “екстќшибки.Replace("\r", "");
            “екстќшибки = “екстќшибки.Replace("'", "");

            this.Controls.Add(new LiteralControl(string.Format("<script type=\"text/javascript\">alert('¬о врем€ выполнени€ импорта произошла ошибка! {0}');</script>", “екстќшибки)));
        }
    }

    private »мпортерƒанныхќтчетных‘орм ѕолучить»мпортер()
    {
        ќтчетна€‘ормаƒанных отчетна€‘орма = ѕолучитьќтчетную‘орму();

        if (отчетна€‘орма == null)
        {
            return null;
        }

        string Ќаименование»мпортера = string.Empty;

        if( this.ѕараметры√лавногоќкна.ѕараметр«адан("Params"))
        {
            Ќаименование»мпортера = this.ѕараметры√лавногоќкна["Params"];
        }

        if( string.IsNullOrEmpty( Ќаименование»мпортера ) )
        {
            return null;
        }

        List<»мпортерƒанныхќтчетных‘орм> список»мпортеров = —писок»мпортеровƒанныхќтчетных‘орм.ѕолучить—писок»мпортеровƒл€‘ормы(отчетна€‘орма);

        if (список»мпортеров.Count > 0)
        {
            foreach (»мпортерƒанныхќтчетных‘орм импортер in список»мпортеров)
            {
                if (импортер.Ќаименование() == Ќаименование»мпортера)
                {
                    return импортер;
                }
            }
        }

        return null;
    }
}
