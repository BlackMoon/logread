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
using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPivotGrid.Web;


using Барс;
using Барс.Ядро;
using Барс.ВебЯдро;
using Барс.ВебЯдро.Интерфейс;
using Барс.Своды;

public partial class Forms_Analytics_AnalyticExtracts_View : ВебФорма
{
    public Forms_Analytics_AnalyticExtracts_View()
        : base()
    {
        this.ЗаголовокСтраницы = "Аналитическая выборка";

        this.ПриИнициализацииСтраницы += new Барс.Интерфейс.ОбработчикСобытия(Forms_Analytics_AnalyticExtracts_View_ПриИнициализацииСтраницы);
    }

    void Forms_Analytics_AnalyticExtracts_View_ПриИнициализацииСтраницы(object Отправитель, Барс.Интерфейс.АргументыСобытия Аргументы)
    {
        АналитическаяТаблица.Styles.FieldValueStyle.HorizontalAlign = HorizontalAlign.Left;

        if (Session["AnalyticExtract"] != null && Session["AnalyticExtract"] is ОписаниеПроизвольнойВыборки)
        {
            DataTable таблицаДанных = null;

            if (!IsPostBack)
            {
                DevExpress.XtraPivotGrid.Localization.PivotGridLocalizer.Active = new Барс.Локализация.XtraPivotGridРусский();
                АналитическаяТаблица.OptionsLoadingPanel.Text = "Загрузка...";
                АналитическаяТаблица.OptionsPager.CurrentPageNumberFormat = "Страница {0}";
                
                ОписаниеПроизвольнойВыборки описаниеПроизвольнойВыборки = Session["AnalyticExtract"] as ОписаниеПроизвольнойВыборки;
                
                ПровайдерПроизвольнойВыборки провайдерПроизвольнойВыборки = new ПровайдерПроизвольнойВыборки(описаниеПроизвольнойВыборки);
                //провайдерПроизвольнойВыборки.ЗагрузитьМетаданные();

                bool собратьЗаново = true;

                if (Session["BuildAnew"] != null && Session["BuildAnew"] is bool)
                {
                    try
                    {
                        собратьЗаново = (bool)Session["BuildAnew"];
                    }
                    catch
                    {
                    }
                }

                таблицаДанных = провайдерПроизвольнойВыборки.ПолучитьТаблицуДанных(собратьЗаново);

                Session["PivotGridDataSource"] = таблицаДанных;

                // поля
                DevExpress.Web.ASPxPivotGrid.PivotGridField полеТаблицы = new DevExpress.Web.ASPxPivotGrid.PivotGridField();
                АналитическаяТаблица.Fields.Add(полеТаблицы);
                полеТаблицы.Caption = "Учреждение";
                полеТаблицы.FieldName = "Uchrej".ToUpper();
                полеТаблицы.Area = DevExpress.XtraPivotGrid.PivotArea.RowArea;
                полеТаблицы.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;
                полеТаблицы.Width = 100;

                foreach (ПолеПроизвольнойВыборки поле in описаниеПроизвольнойВыборки.Поля)
                {
                    полеТаблицы = new DevExpress.Web.ASPxPivotGrid.PivotGridField();

                    АналитическаяТаблица.Fields.Add(полеТаблицы);

                    полеТаблицы.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    полеТаблицы.CellFormat.FormatString = "N2";

                    полеТаблицы.Caption = поле.Заголовок;
                    полеТаблицы.FieldName = поле.ИмяПеременной.ToUpper();

                    if (поле.ТипПоляOLAP != Барс.ТипПоляOLAPКуба.СкрытоеПоле)
                    {
                        полеТаблицы.Area = ПреобразоватьТип(поле.ТипПоляOLAP);
                    }
                    else
                    {
                        полеТаблицы.Visible = false;
                    }

                    if (поле.РежимФормированияИтогов == РежимФормированияИтоговСтолбцаАналитическойТаблицы.БезИтогов)
                    {
                        полеТаблицы.Options.ShowTotals = false;
                    }
                    else
                    {
                        полеТаблицы.SummaryType = (DevExpress.Data.PivotGrid.PivotSummaryType)((int)поле.РежимФормированияИтогов);
                    }

                    полеТаблицы.Name = "Field" + (АналитическаяТаблица.Fields.Count + 1).ToString();
                }
            }
            else if (Session["PivotGridDataSource"] != null && Session["PivotGridDataSource"] is DataTable)
            {
                таблицаДанных = Session["PivotGridDataSource"] as DataTable;
            }

            АналитическаяТаблица.DataSource = таблицаДанных;
        }
    }

    private PivotArea ПреобразоватьТип(Барс.ТипПоляOLAPКуба ТипПоля)
    {
        switch (ТипПоля)
        {
            case ТипПоляOLAPКуба.ИзмерениеСтолбец:
                return PivotArea.ColumnArea;
            case ТипПоляOLAPКуба.ИзмерениеСтрока:
                return PivotArea.RowArea;
            case ТипПоляOLAPКуба.ПолеФильтрации:
                return PivotArea.FilterArea;
            case ТипПоляOLAPКуба.Ресурс:
                return PivotArea.DataArea;
            default:
                return PivotArea.DataArea;
        }
    }

    protected void АналитическаяТаблица_CustomSummary(object sender, DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs e)
    {
        if (Session["AnalyticExtract"] != null && Session["AnalyticExtract"] is ОписаниеПроизвольнойВыборки)
        {
            ОписаниеПроизвольнойВыборки описаниеПроизвольнойВыборки = Session["AnalyticExtract"] as ОписаниеПроизвольнойВыборки;

            ПровайдерПроизвольнойВыборки провайдерПроизвольнойВыборки = new ПровайдерПроизвольнойВыборки(описаниеПроизвольнойВыборки);

            провайдерПроизвольнойВыборки.ПодсчитатьИтоги(e);
        }
    }

    protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e)
    {
        if (e.Item == null)
        {
            return;
        }

        if (e.Item.Text == "Печать")
        {
            string ПутьКФайлу = ЭкспортФайла.ПолучитьСлучайноеИмяФайла(".xls");

            ASPxPivotGridExporter_1.ExportToXls(ПутьКФайлу, true);

            ЭкспортФайла.ЭкспортироватьДокумент(ПутьКФайлу, "Analitic.xls");
        }
    }
}
