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

using Telerik.WebControls;

using Ѕарс.¬ебядро;
using Ѕарс.¬ебядро.»нтерфейс;
using Ѕарс.—воды.ќтчетна€‘орма;
using Ѕарс.—воды;
using Ѕарс;
using Ѕарс.ядро;
using Ѕарс.–асчеты;

public partial class Racheti_List : ¬еб‘орма
{
    public Racheti_List()
        : base()
    {
        this.«аголовок—траницы = "–асчеты";
        this.Ўапка—траницы = "–асчеты";

        this.ѕри«агрузке—траницы += new Ѕарс.»нтерфейс.ќбработчик—обыти€(Racheti_List_ѕри«агрузке—траницы);
    }

    void Racheti_List_ѕри«агрузке—траницы(object ќтправитель, Ѕарс.»нтерфейс.јргументы—обыти€ јргументы)
    {
        —писок–асчетов список = new —писок–асчетов();
        
        “аблицаЁлементы.DataSource = список;
        “аблицаЁлементы.DataBind();

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
        }
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

