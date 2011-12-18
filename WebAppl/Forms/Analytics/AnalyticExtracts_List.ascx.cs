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
using System.Collections.Generic;

using Ѕарс;
using Ѕарс.¬ебядро;
using Ѕарс.¬ебядро.»нтерфейс;
using Ѕарс.—воды;

using Telerik.WebControls;

public partial class Forms_Analytics_AnalyticExtracts_List : ¬еб‘орма
{
    public Forms_Analytics_AnalyticExtracts_List()
        : base()
    {
        this.«аголовок—траницы = "—писок аналитических выборок";

        this.ѕри»нициализации—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(Forms_Analytics_AnalyticExtracts_List_ѕри»нициализации—траницы);
    }

    void Forms_Analytics_AnalyticExtracts_List_ѕри»нициализации—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        if (!IsPostBack)
        {
            “аблицаЁлементы.SettingsText.CommandCancel = "ќтмена";
            “аблицаЁлементы.SettingsText.CommandClearFilter = "ќчистить фильтр";
            “аблицаЁлементы.SettingsText.CommandDelete = "”далить";
            “аблицаЁлементы.SettingsText.CommandEdit = "»зменить";
            “аблицаЁлементы.SettingsText.CommandNew = "ƒобавить";
            “аблицаЁлементы.SettingsText.GroupPanel = "ќбласть дл€ группировки столбцов";
            “аблицаЁлементы.SettingsText.HeaderFilterShowAll = "(¬се)";
            “аблицаЁлементы.SettingsText.GroupContinuedOnNextPage = "√руппа продолжаетс€ на следующей странице";
            “аблицаЁлементы.SettingsText.ConfirmDelete = "¬ы уверены, что хотите удалить запись?";
            “аблицаЁлементы.SettingsText.CommandUpdate = "—охранить";
            “аблицаЁлементы.SettingsText.EmptyDataRow = "Ќет данных";
            “аблицаЁлементы.SettingsText.CustomizationWindowCaption = "¬ыбор столбцов";
            “аблицаЁлементы.SettingsLoadingPanel.Text = "«агрузка...";
            “аблицаЁлементы.SettingsPager.Summary.Text = "—траница {0} из {1} ({2} записей)";

            Cache[ObjectDataSource_list.CacheKeyDependency] = new object();
        }

        ѕроверкаЋицензионныхќграничений.ќписаниеѕричиныќграничени€‘ункционала описание = ѕроверкаЋицензионныхќграничений.ѕроверить¬озможность«апуска(“ип–абочегоћеста.Ѕэк_ќфис, ѕроверкаЋицензионныхќграничений. ќћѕЋ≈ “ј÷»я_—“јЌƒј–“Ќјя);

        if (описание.“ипќграничени€ == ѕроверкаЋицензионныхќграничений.“ипќграничени€‘ункционала.Ќе«аданќператор”чреждени€)
        {
            this.Ўапка—траницы = Ѕарс.—воды.—ообщениеЋицензионного люча—водов.ѕостроить“екст—ообщени€Ќе«аданќператор”чреждений("дл€ аналитических выборок");
        }
        else if (описание.“ипќграничени€ == ѕроверкаЋицензионныхќграничений.“ипќграничени€‘ункционала.Ќесоответствие омплектации)
        {
            this.Ўапка—траницы = Ѕарс.—воды.—ообщениеЋицензионного люча—водов.ѕостроить“екст—ообщени€ќбќграничении омплектации("дл€ аналитических выборок", ѕроверкаЋицензионныхќграничений. ќћѕЋ≈ “ј÷»я_—“јЌƒј–“Ќјя);
        }
        else if (описание.“ипќграничени€ == ѕроверкаЋицензионныхќграничений.“ипќграничени€‘ункционала.Ќесоответствие“ипа–абочегоћеста)
        {
            this.Ўапка—траницы = Ѕарс.—воды.—ообщениеЋицензионного люча—водов.ѕостроить“екст—ообщени€ќбќграничении“ипа–абочегоћеста("дл€ аналитических выборок", “ип–абочегоћеста.Ѕэк_ќфис);
        }
        else
        {
            if (!IsPostBack)
            {
                “аблицаЁлементы.Enabled = true;

                —писокќписанийѕроизвольных¬ыборок списокќписанийѕроизвольных¬ыборок = new —писокќписанийѕроизвольных¬ыборок();

                Session["AnalyticExtractsDataSource"] = списокќписанийѕроизвольных¬ыборок;
            }
        }
    }

    public DataSet GetExtracts()
    {
        DataSet dataSet = new DataSet();
        DataTable table = dataSet.Tables.Add();

        table.Columns.Add("ID");
        table.Columns.Add(" од");
        table.Columns.Add("Ќаименование");
        table.Columns.Add("“ип");
        table.Columns.Add("√руппа");

        —писокќписанийѕроизвольных¬ыборок списокќписанийѕроизвольных¬ыборок = new —писокќписанийѕроизвольных¬ыборок();

        int index = 0;

        foreach (ќписаниеѕроизвольной¬ыборки описание in списокќписанийѕроизвольных¬ыборок)
        {
            table.Rows.Add(new object[] { index.ToString(), описание. од, описание.Ќаименование, описание.“ипѕредставлени€, описание.√руппа });
            index++;
        }

        return dataSet;
    }

    protected void “аблицаЁлементы_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType != DevExpress.Web.ASPxGridView.GridViewRowType.Data)
        {
            return;
        }

        e.Row.Attributes.Add("Key", e.KeyValue.ToString());
    }
}
