using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Web.ASPxGridView;
using Ѕарс.¬ебядро.»нтерфейс;
using Ѕарс.—воды;
using DevExpress.Web;

public partial class Forms_MacroProcessing_CompareResult : ¬еб‘орма
{
    public Forms_MacroProcessing_CompareResult()
        : base()
    {
        this.Ўапка—траницы = "";
        this.«аголовок—траницы = "–езультаты сверки";

        this.Ўиринаќкна = 800;
        this.¬ысотаќкна = 600;

        this.ѕри»нициализации—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(Forms_MacroProcessing_CompareResult_ѕри»нициализации—траницы);
    }

    void Forms_MacroProcessing_CompareResult_ѕри»нициализации—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        –езультаты¬ыполнени€—веркиƒанных результат—верки = ѕолучить–езультат—верки();

        if (результат—верки != null)
        {
            // Ћоклизаци€ таблицы
            “аблица_–езультат.SettingsText.CommandCancel = "ќтмена";
            “аблица_–езультат.SettingsText.CommandClearFilter = "ќчистить фильтр";
            “аблица_–езультат.SettingsText.CommandDelete = "”далить";
            “аблица_–езультат.SettingsText.CommandEdit = "»зменить";
            “аблица_–езультат.SettingsText.CommandNew = "ƒобавить";
            “аблица_–езультат.SettingsText.GroupPanel = "ќбласть дл€ группировки столбцов";
            “аблица_–езультат.SettingsText.HeaderFilterShowAll = "(¬се)";
            “аблица_–езультат.SettingsText.GroupContinuedOnNextPage = "√руппа продолжаетс€ на следующей странице";
            “аблица_–езультат.SettingsText.ConfirmDelete = "¬ы уверены, что хотите удалить запись?";
            “аблица_–езультат.SettingsText.CommandUpdate = "—охранить";
            “аблица_–езультат.SettingsText.EmptyDataRow = "Ќет данных";
            “аблица_–езультат.SettingsText.CustomizationWindowCaption = "¬ыбор столбцов";
            “аблица_–езультат.SettingsLoadingPanel.Text = "«агрузка...";
            “аблица_–езультат.SettingsPager.Summary.Text = "—траница {0} из {1} ({2} записей)";

            if (результат—верки.«аголовок != "")
            {
                заголовок–езультатов—верки.Text = результат—верки.«аголовок;
                заголовок–езультатов—верки.Visible = true;
            }

            if (результат—верки.ѕодзаголовок != "")
            {
                подзаголовок–езультатов—верки.Text = результат—верки.ѕодзаголовок;
                подзаголовок–езультатов—верки.Visible = true;
            }

            if (!IsPostBack)
            {
                if (результат—верки.ќтображаемые—толбцы != null && результат—верки.ќтображаемые—толбцы.Count > 0)
                {
                    “аблица_–езультат.Columns.Clear();

                    foreach (KeyValuePair<string, string> отображаемый—толбец in результат—верки.ќтображаемые—толбцы)
                    {
                        GridViewDataColumn column = new GridViewDataColumn(отображаемый—толбец.Value, отображаемый—толбец.Key);

                        “аблица_–езультат.Columns.Add(column);
                    }
                }
            }

            “аблица_–езультат.DataSource = результат—верки.—троки—верки;
             
            “аблица_–езультат.DataBind();
        }
    }

    protected –езультаты¬ыполнени€—веркиƒанных ѕолучить–езультат—верки()
    {
        if (this.ѕараметры√лавногоќкна.ѕараметр«адан("SessionParam"))
        {
            string[] strings = ѕараметры√лавногоќкна["SessionParam"].Split(':');

            if (strings.Length == 2)
            {
                –езультаты¬ыполнени€—веркиƒанных результат—верки = null;

                object ќбъект—ессионнойѕеременной = Ѕарс.¬ебядро.ћенеджер—ессионныхѕеременных.ѕолучитьѕеременную»з—ессии(strings[0], strings[1]);

                if (ќбъект—ессионнойѕеременной is –езультаты¬ыполнени€—веркиƒанных)
                {
                    результат—верки = (–езультаты¬ыполнени€—веркиƒанных)ќбъект—ессионнойѕеременной;
                }

                return результат—верки;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    protected void ASPxButton_печать_Click(object sender, EventArgs e)
    {
        –езультаты¬ыполнени€—веркиƒанных результат—верки = ѕолучить–езультат—верки();

        Ѕарс.ќтчетЋог—веркиƒанных отчетЋога—верки = new Ѕарс.ќтчетЋог—веркиƒанных(результат—верки);

        Ѕарс.¬ебядро.Ёкспорт‘айла.Ёкспортироватьќтчет(отчетЋога—верки, "Sverka.xls");
    }
}
