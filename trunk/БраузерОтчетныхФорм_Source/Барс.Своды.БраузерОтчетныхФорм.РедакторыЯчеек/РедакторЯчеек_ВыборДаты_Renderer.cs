namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using DevExpress.XtraEditors.Mask;
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using Барс.Интерфейс;

    internal class РедакторЯчеек_ВыборДаты_Renderer : GridGenericControlCellRenderer
    {
        private ПолеВыбораДаты drawПолеВыбораДаты;
        private ПолеВыбораДаты editПолеВыбораДаты;

        public РедакторЯчеек_ВыборДаты_Renderer(GridControlBase grid, GridCellModelBase cellModel) : base(grid, cellModel)
        {
            this.drawПолеВыбораДаты = new ПолеВыбораДаты();
            this.editПолеВыбораДаты = new ПолеВыбораДаты();
            this.drawПолеВыбораДаты.НазначитьКнопки();
            this.editПолеВыбораДаты.EditValueChanged += new EventHandler(this.editПолеВыбораДаты_EditValueChanged);
            this.editПолеВыбораДаты.НазначитьКнопки();
            base.SupportsFocusControl = true;
            base.FixControlParent(this.drawПолеВыбораДаты);
            base.SetControl(this.editПолеВыбораДаты);
        }

        private void editПолеВыбораДаты_EditValueChanged(object sender, EventArgs e)
        {
            if (base.NotifyCurrentCellChanging())
            {
                this.ControlValue = this.editПолеВыбораДаты.Дата;
                base.NotifyCurrentCellChanged();
            }
        }

        protected override void InitializeControlText(object controlValue)
        {
            РедакторЯчеек_ВыборДаты.ПроинициализироватьЗначение(this.drawПолеВыбораДаты, controlValue);
        }

        protected override void OnDraw(Graphics g, Rectangle clientRectangle, int rowIndex, int colIndex, GridStyleInfo style)
        {
            if (base.ShouldDrawFocused(rowIndex, colIndex))
            {
                style.Control = this.editПолеВыбораДаты;
            }
            else
            {
                style.Control = this.drawПолеВыбораДаты;
                РедакторЯчеек_ВыборДаты.ПроинициализироватьЗначение(this.drawПолеВыбораДаты, style.CellValue);
                this.drawПолеВыбораДаты.BackColor = style.BackColor;
                this.drawПолеВыбораДаты.ForeColor = style.TextColor;
                this.drawПолеВыбораДаты.Enabled = style.Enabled;
            }
            base.OnDraw(g, clientRectangle, rowIndex, colIndex, style);
        }

        protected override void OnInitialize(int rowIndex, int colIndex)
        {
            GridStyleInfo viewStyleInfo = base.Grid.GetViewStyleInfo(rowIndex, colIndex);
            if (viewStyleInfo.ReadOnly)
            {
                this.editПолеВыбораДаты.Enabled = false;
            }
            if (!string.IsNullOrEmpty(viewStyleInfo.Description))
            {
                this.editПолеВыбораДаты.Properties.Mask.MaskType = MaskType.DateTime;
                this.editПолеВыбораДаты.Properties.Mask.EditMask = viewStyleInfo.Description;
                this.editПолеВыбораДаты.Properties.Mask.UseMaskAsDisplayFormat = true;
                this.drawПолеВыбораДаты.Properties.Mask.MaskType = MaskType.DateTime;
                this.drawПолеВыбораДаты.Properties.Mask.EditMask = viewStyleInfo.Description;
                this.drawПолеВыбораДаты.Properties.Mask.UseMaskAsDisplayFormat = true;
            }
            РедакторЯчеек_ВыборДаты.ПроинициализироватьЗначение(this.editПолеВыбораДаты, viewStyleInfo.CellValue);
        }

        protected override bool OnSaveChanges()
        {
            base.Grid.Model[base.RowIndex, base.ColIndex].CellValue = this.editПолеВыбораДаты.Дата;
            return true;
        }
    }
}

