namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.XtraGrid.Views.BandedGrid;
    using System;

    public class ОбъединениеДинамическойТаблицы : GridBand
    {
        public ОбъединениеДинамическойТаблицы()
        {
            base.OptionsBand.AllowHotTrack = true;
            base.OptionsBand.AllowMove = true;
            base.OptionsBand.AllowPress = true;
        }

        public void ДобавитьОбъединение(ОбъединениеДинамическойТаблицы объединение)
        {
            объединение.AppearanceHeader.Font = base.AppearanceHeader.Font;
            this.Children.Add(объединение);
        }

        public void ДобавитьСтолбец(СтолбецОбъединенияДинамическойТаблицы Столбец)
        {
            Столбец.AppearanceCell.Font = base.AppearanceHeader.Font;
            Столбец.AppearanceHeader.Font = base.AppearanceHeader.Font;
            this.View.Columns.Add(Столбец);
            this.Columns.Add(Столбец);
        }

        public string Заголовок
        {
            get
            {
                return this.Caption;
            }
            set
            {
                this.Caption = value;
            }
        }

        public int КоличествоСтрок
        {
            get
            {
                return this.RowCount;
            }
            set
            {
                this.RowCount = value;
            }
        }
    }
}

