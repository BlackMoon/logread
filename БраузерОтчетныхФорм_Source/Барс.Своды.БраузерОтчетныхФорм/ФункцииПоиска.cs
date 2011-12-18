namespace Барс.Своды.БраузерОтчетныхФорм
{
    using Syncfusion.Windows.Forms.Grid;
    using System;

    public class ФункцииПоиска
    {
        private GridFindReplaceDialogSink findDialog;
        private GridFindReplaceEventArgs findEvents;
        private GridRangeInfo findLocation = GridRangeInfo.Table();
        private ТаблицаОтчетнойФормы таблицаФормы;

        public ФункцииПоиска(ТаблицаОтчетнойФормы ТаблицаФормы)
        {
            this.таблицаФормы = ТаблицаФормы;
            this.findDialog = new GridFindReplaceDialogSink(this.таблицаФормы);
        }

        public void Найти(string ИскомоеЗначение, bool УчитыватьРегистр, bool ИскатьПредыдущее, bool ТолькоПоСтолбцу)
        {
            this.таблицаФормы.Selections.Clear();
            GridFindTextOptions none = GridFindTextOptions.None;
            if (ТолькоПоСтолбцу)
            {
                none = GridFindTextOptions.ColumnOnly;
            }
            else
            {
                none = GridFindTextOptions.WholeTable;
            }
            if (УчитыватьРегистр)
            {
                none |= GridFindTextOptions.MatchCase;
            }
            if (ИскатьПредыдущее)
            {
                none |= GridFindTextOptions.SearchUp;
            }
            this.findEvents = new GridFindReplaceEventArgs(ИскомоеЗначение, "", none, this.findLocation);
            this.findDialog.Find(this.findEvents);
        }
    }
}

