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
using Ѕарс.¬ебядро;
using Ѕарс.¬ебядро.»нтерфейс;
using Ѕарс;
using Ѕарс.—воды;
using Ѕарс.—воды.ќтчетна€‘орма;
using Ѕарс.—воды.Ѕраузерќтчетных‘орм;

public partial class Forms_SdachaOtchetnosti_ConsolidateForm_ConsolidateForm : ¬еб‘ормаќбработкиќтчетной‘ормы
{
    public Forms_SdachaOtchetnosti_ConsolidateForm_ConsolidateForm()
        : base()
    {
        this.Ўиринаќкна = 700;
        this.¬ысотаќкна = 500;

        this.«аголовок—траницы = "—водна€ форма";
        this.Ўапка—траницы = "¬ыберите формы дл€ сборки сводного отчета";

        this.ѕри»нициализации—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(Forms_SdachaOtchetnosti_ConsolidateForm_ConsolidateForm_ѕри»нициализации—траницы);
    }

    void Forms_SdachaOtchetnosti_ConsolidateForm_ConsolidateForm_ѕри»нициализации—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        if (!IsPostBack)
        {
            ќтчетна€‘ормаƒанных отчетна€‘орма = ѕолучитьќтчетную‘орму();

            if (отчетна€‘орма != null)
            {
                отчетна€‘орма.«агрузить»сходные‘ормы();

                List< омпозитƒл€¬ыбораќтчетной‘ормы> список омпозитов = new List< омпозитƒл€¬ыбораќтчетной‘ормы>();

                foreach (ќтчетна€‘ормаƒанных форма in отчетна€‘орма.»сходные‘ормы)
                {
                    список омпозитов.Add(new  омпозитƒл€¬ыбораќтчетной‘ормы(форма));
                }

                “аблица_элементы.»сточник«аписей = список омпозитов;
            }

            “аблица_элементы.–едактировать¬“аблице = false;
            “аблица_элементы.–азрешить”даление = false;
            “аблица_элементы.–азрешитьƒобавление = false;
            “аблица_элементы.–азрешить√руппировку = false;
            “аблица_элементы.јвтоподбор¬ысоты = false;
            “аблица_элементы.AllowMultiRowSelection = true;

            Telerik.WebControls.GridClientSelectColumn столбец¬ыбора = new Telerik.WebControls.GridClientSelectColumn();
            “аблица_элементы.Columns.Add(столбец¬ыбора);
            столбец¬ыбора.HeaderStyle.Width = Unit.Pixel(25);
            столбец¬ыбора.Resizable = false;

            —толбец“аблицы столбец_ омментарий = new —толбец“аблицы();
            “аблица_элементы.ƒобавить—толбец(столбец_ омментарий);
            столбец_ омментарий.«аголовок = "”чреждение";
            столбец_ омментарий.»м€ѕол€»сточникаƒанных = "Ќаименование”чреждени€";

            “аблица_элементы.DataBind();
        }
    }

    protected void  нопка_ќ _Click(object sender, EventArgs e)
    {
        try
        {
            ќтчетна€‘ормаƒанных отчетна€‘орма = ѕолучитьќтчетную‘орму();

            if (отчетна€‘орма != null)
            {
                if (“аблица_элементы.SelectedIndexes.Count > 0)
                {
                    List<ќтчетна€‘ормаƒанных> выбранные‘ормы = new List<ќтчетна€‘ормаƒанных>();

                    List< омпозитƒл€¬ыбораќтчетной‘ормы> список омпозитов = “аблица_элементы.»сточник«аписей as List< омпозитƒл€¬ыбораќтчетной‘ормы>;

                    foreach (string index in “аблица_элементы.SelectedIndexes)
                    {
                        int индекс = int.Parse(index);
                        выбранные‘ормы.Add(список омпозитов[индекс].‘орма);
                    }

                    Ѕазовыйќбработчик—водной‘ормы обработчик—водной = new Ѕазовыйќбработчик—водной‘ормы(отчетна€‘орма);
                    обработчик—водной.—вести»сходные‘ормы(выбранные‘ормы);

                    if (‘лажок_сохранить.«начение)
                    {
                        отчетна€‘орма.—охранитьƒанные(true);

                        отчетна€‘орма.«аблокировать‘орму();
                    }

                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">CloseAndRebindSheet();</script>"));
                }
                else
                {
                    Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">alert('Ќеобходимо выбрать архив дл€ восстановлени€!');</script>"));
                }
            }
        }
        catch( Exception exc )
        {
            exc.ToString();
            Controls.AddAt(3, new LiteralControl("<script type=\"text/javascript\">alert('Ќе удалось выполнить сборку сводной формы!');</script>"));
        }
    }
}
