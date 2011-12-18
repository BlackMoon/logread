namespace Барс.Своды.БраузерОтчетныхФорм
{
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraGrid;
    using DevExpress.XtraGrid.Columns;
    using DevExpress.XtraGrid.Views.Grid;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Барс.Интерфейс;

    public class ФормаПоказаАвтосохраненныхФайлов : XtraForm
    {
        private IContainer components = null;
        private GridColumn gridColumn_ДатаВремя;
        private GridColumn gridColumn_Комментарий;
        private GridControl gridControl_СписокФайлов;
        private GridView gridView1;
        private LabelControl labelControl1;
        private SimpleButton simpleButton_Восстановить;
        private SimpleButton simpleButton_Закрыть;
        private АвтосохранениеДанныхОтчетнойФормы.АвтосохраненныйФайл выбранныйФайл = null;
        private List<АвтосохранениеДанныхОтчетнойФормы.АвтосохраненныйФайл> списокАвтосохраненныхФайлов = new List<АвтосохранениеДанныхОтчетнойФормы.АвтосохраненныйФайл>();

        public ФормаПоказаАвтосохраненныхФайлов()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.gridControl_СписокФайлов = new GridControl();
            this.gridView1 = new GridView();
            this.gridColumn_ДатаВремя = new GridColumn();
            this.gridColumn_Комментарий = new GridColumn();
            this.simpleButton_Закрыть = new SimpleButton();
            this.simpleButton_Восстановить = new SimpleButton();
            this.labelControl1 = new LabelControl();
            this.gridControl_СписокФайлов.BeginInit();
            this.gridView1.BeginInit();
            base.SuspendLayout();
            this.gridControl_СписокФайлов.EmbeddedNavigator.Name = "";
            this.gridControl_СписокФайлов.Location = new Point(12, 0x21);
            this.gridControl_СписокФайлов.MainView = this.gridView1;
            this.gridControl_СписокФайлов.Name = "gridControl_СписокФайлов";
            this.gridControl_СписокФайлов.Size = new Size(0x275, 0x11e);
            this.gridControl_СписокФайлов.TabIndex = 0;
            this.gridControl_СписокФайлов.ViewCollection.AddRange(new BaseView[] { this.gridView1 });
            this.gridView1.Columns.AddRange(new GridColumn[] { this.gridColumn_ДатаВремя, this.gridColumn_Комментарий });
            this.gridView1.GridControl = this.gridControl_СписокФайлов;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridColumn_ДатаВремя.Caption = "Дата и время создания";
            this.gridColumn_ДатаВремя.DisplayFormat.FormatString = "F";
            this.gridColumn_ДатаВремя.DisplayFormat.FormatType = FormatType.DateTime;
            this.gridColumn_ДатаВремя.FieldName = "ДатаВремяСоздания";
            this.gridColumn_ДатаВремя.Name = "gridColumn_ДатаВремя";
            this.gridColumn_ДатаВремя.Visible = true;
            this.gridColumn_ДатаВремя.VisibleIndex = 0;
            this.gridColumn_ДатаВремя.Width = 0xa9;
            this.gridColumn_Комментарий.Caption = "Комментарий";
            this.gridColumn_Комментарий.FieldName = "Комментарий";
            this.gridColumn_Комментарий.Name = "gridColumn_Комментарий";
            this.gridColumn_Комментарий.Visible = true;
            this.gridColumn_Комментарий.VisibleIndex = 1;
            this.gridColumn_Комментарий.Width = 0x1b7;
            this.simpleButton_Закрыть.DialogResult = DialogResult.Cancel;
            this.simpleButton_Закрыть.Location = new Point(0x235, 0x147);
            this.simpleButton_Закрыть.Name = "simpleButton_Закрыть";
            this.simpleButton_Закрыть.Size = new Size(0x4b, 0x17);
            this.simpleButton_Закрыть.TabIndex = 1;
            this.simpleButton_Закрыть.Text = "Закрыть";
            this.simpleButton_Восстановить.DialogResult = DialogResult.OK;
            this.simpleButton_Восстановить.Location = new Point(0x13c, 0x147);
            this.simpleButton_Восстановить.Name = "simpleButton_Восстановить";
            this.simpleButton_Восстановить.Size = new Size(0xf3, 0x17);
            this.simpleButton_Восстановить.TabIndex = 2;
            this.simpleButton_Восстановить.Text = "Восстановить данные формы из копии";
            this.simpleButton_Восстановить.Click += new EventHandler(this.simpleButton_Восстановить_Click);
            this.labelControl1.Appearance.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0xcc);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new Point(13, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new Size(0x123, 13);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "Список автосохраненных копий отчетной формы:";
            base.AcceptButton = this.simpleButton_Восстановить;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.simpleButton_Закрыть;
            base.ClientSize = new Size(0x28d, 0x16a);
            base.Controls.Add(this.labelControl1);
            base.Controls.Add(this.simpleButton_Восстановить);
            base.Controls.Add(this.simpleButton_Закрыть);
            base.Controls.Add(this.gridControl_СписокФайлов);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ФормаПоказаАвтосохраненныхФайлов";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Список автосохраненных файлов";
            base.FormClosing += new FormClosingEventHandler(this.ФормаПоказаАвтосохраненныхФайлов_FormClosing);
            base.Load += new EventHandler(this.ФормаПоказаАвтосохраненныхФайлов_Load);
            this.gridControl_СписокФайлов.EndInit();
            this.gridView1.EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void simpleButton_Восстановить_Click(object sender, EventArgs e)
        {
        }

        private void ФормаПоказаАвтосохраненныхФайлов_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (base.DialogResult == DialogResult.OK)
            {
                this.выбранныйФайл = null;
                int dataSourceRowIndex = this.gridView1.GetDataSourceRowIndex(this.gridView1.FocusedRowHandle);
                if ((dataSourceRowIndex >= 0) && (dataSourceRowIndex < this.СписокАвтосохраненныхФайлов.Count))
                {
                    this.выбранныйФайл = this.СписокАвтосохраненныхФайлов[dataSourceRowIndex];
                }
                if (this.выбранныйФайл == null)
                {
                    Сообщение.ПоказатьПредупреждение("Для восстановления резервной копии данных необходимо ее (копию) выбрать из вышеуказанного списка.");
                    e.Cancel = true;
                }
                else if (Сообщение.ПоказатьВопрос(string.Format("{0}: {1}\n\nПри восстановлении данных из копии ВСЕ ТЕКУЩИЕ ЗНАЧЕНИЯ В ЯЧЕЙКАХ ФОРМЫ БУДУТ ЗАМЕНЕНЫ.\n\nВосстановить значения из автоматически созданной копии?", this.выбранныйФайл.Комментарий, this.выбранныйФайл.ДатаВремяСоздания.ToString("F"))) != РезультатДиалога.Да)
                {
                    e.Cancel = true;
                }
            }
        }

        private void ФормаПоказаАвтосохраненныхФайлов_Load(object sender, EventArgs e)
        {
            this.gridControl_СписокФайлов.DataSource = this.СписокАвтосохраненныхФайлов;
            this.gridControl_СписокФайлов.RefreshDataSource();
        }

        public АвтосохранениеДанныхОтчетнойФормы.АвтосохраненныйФайл ВыбранныйФайл
        {
            get
            {
                return this.выбранныйФайл;
            }
        }

        public List<АвтосохранениеДанныхОтчетнойФормы.АвтосохраненныйФайл> СписокАвтосохраненныхФайлов
        {
            get
            {
                return this.списокАвтосохраненныхФайлов;
            }
            set
            {
                this.списокАвтосохраненныхФайлов = value;
            }
        }
    }
}

