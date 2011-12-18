namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using System;

    public class РедакторФлажокДинамическойТаблицы : RepositoryItemCheckEdit
    {
        public РедакторФлажокДинамическойТаблицы()
        {
            base.NullStyle = StyleIndeterminate.Unchecked;
        }
    }
}

