namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors.Mask;
    using DevExpress.XtraEditors.Repository;
    using System;

    public class РедакторДатыДинамическойТаблицы : RepositoryItemDateEdit
    {
        public РедакторДатыДинамическойТаблицы()
        {
            base.VistaDisplayMode = DefaultBoolean.True;
            this.Mask.MaskType = MaskType.DateTimeAdvancingCaret;
            this.NullText = "";
            this.NullDate = DateTime.MinValue;
        }
    }
}

