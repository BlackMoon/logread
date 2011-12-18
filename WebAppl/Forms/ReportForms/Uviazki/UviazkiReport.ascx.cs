using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Ѕарс.¬ебядро.»нтерфейс;
using Ѕарс.—воды;
using Ѕарс.—воды.ќтчетна€‘орма;
using Ѕарс.—воды.¬ебЅраузерќтчетных‘орм;
using Ѕарс.—воды.”в€зки;

public partial class Forms_ReportForms_Uviazki_UviazkiReport : ¬еб‘ормаќбработкиќтчетной‘ормы
{
    public Forms_ReportForms_Uviazki_UviazkiReport()
        : base()
    {
        this.Ўапка—траницы = "";
        this.«аголовок—траницы = "–езультаты проверки ув€зок";

        this.ѕри»нициализации—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(Forms_ReportForms_Uviazki_UviazkiReport_ѕри»нициализации—траницы);
    }

    –езультатѕроверки”в€зок результатѕроверки = null;

    void Forms_ReportForms_Uviazki_UviazkiReport_ѕри»нициализации—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        ќтчетна€‘ормаƒанных отчетна€‘орма = ѕолучитьќтчетную‘орму();

        if (отчетна€‘орма != null)
        {
            this.–едактируемыйќбъект = отчетна€‘орма;
        }

        bool ¬нутриформенные = true;

        if (this.ѕараметры√лавногоќкна.ѕараметр«адан("Params"))
        {
            ¬нутриформенные = this.ѕараметры√лавногоќкна["Params"] == "In";
        }

        if (¬нутриформенные)
        {
            результатѕроверки = отчетна€‘орма.ѕроверить¬нутриформенные”в€зки();
        }
        else
        {
            результатѕроверки = отчетна€‘орма.ѕроверитьћежформенные”в€зки();
        }

        if (результатѕроверки.ќшибкиѕроверки.Count == 0)
        {
            Div_Result.InnerText = "ќшибок или предупреждений по форме не обнаружено.";
            buttonPrint.ClientEnabled = false;
        }
        else
        {
            buttonPrint.ClientEnabled = true;
        }

        if (!IsPostBack)
        {
            try
            {
                ’ранилище–езультатовѕроверки”в€зок.—охранить–езультатѕроверки”в€зок(отчетна€‘орма.ƒанные.»дентификатор, результатѕроверки);
            }
            catch (ArgumentNullException)
            {
            }

            // Ћоклизаци€ таблицы
            gvUviazki.SettingsText.CommandCancel = "ќтмена";
            gvUviazki.SettingsText.CommandClearFilter = "ќчистить фильтр";
            gvUviazki.SettingsText.CommandDelete = "”далить";
            gvUviazki.SettingsText.CommandEdit = "»зменить";
            gvUviazki.SettingsText.CommandNew = "ƒобавить";
            gvUviazki.SettingsText.GroupPanel = "ќбласть дл€ группировки столбцов";
            gvUviazki.SettingsText.HeaderFilterShowAll = "(¬се)";
            gvUviazki.SettingsText.GroupContinuedOnNextPage = "√руппа продолжаетс€ на следующей странице";
            gvUviazki.SettingsText.ConfirmDelete = "¬ы уверены, что хотите удалить запись?";
            gvUviazki.SettingsText.CommandUpdate = "—охранить";
            gvUviazki.SettingsText.EmptyDataRow = "Ќет данных";
            gvUviazki.SettingsText.CustomizationWindowCaption = "¬ыбор столбцов";
            gvUviazki.SettingsLoadingPanel.Text = "«агрузка...";
            gvUviazki.SettingsPager.Summary.Text = "—траница {0} из {1} ({2} записей)";
                        
            gvUviazki.DataBind();

            this.Ўапка—траницы = string.Format("–езультат проверки {0} ув€зок формы {1}", ¬нутриформенные ? "внутриформенных" : "межформенных", отчетна€‘орма.ћетаструктура. од);
        }
    }

    protected void buttonPrint_Click(object sender, EventArgs e)
    {
        if (результатѕроверки != null)
        {
            Ѕарс.ѕечатна€‘орма–езультатовѕроверки”в€зок печатна€‘орма = new Ѕарс.ѕечатна€‘орма–езультатовѕроверки”в€зок(результатѕроверки);

            Ѕарс.¬ебядро.Ёкспорт‘айла.Ёкспортироватьќтчет(печатна€‘орма, "CheckResult.xls");
        }
    }

    protected void gvUviazki_BeforePerformDataSelect(object sender, EventArgs e)
    {
        gvUviazki.DataSource = результатѕроверки.ќшибкиѕроверки;
    }
}
