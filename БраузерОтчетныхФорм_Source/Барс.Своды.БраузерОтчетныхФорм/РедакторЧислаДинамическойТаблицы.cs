namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using System;
    using System.Windows.Forms;

    public class РедакторЧислаДинамическойТаблицы : RepositoryItemCalcEdit
    {
        public РедакторЧислаДинамическойТаблицы()
        {
            base.ShowCloseButton = true;
            this.CloseUpKey = new KeyShortcut(Keys.Shift | Keys.Return);
            base.Spin += new SpinEventHandler(this.РедакторЧислаДинамическойТаблицы_Spin);
        }

        private void РедакторЧислаДинамическойТаблицы_Spin(object sender, SpinEventArgs e)
        {
            e.Handled = true;
        }
    }
}

