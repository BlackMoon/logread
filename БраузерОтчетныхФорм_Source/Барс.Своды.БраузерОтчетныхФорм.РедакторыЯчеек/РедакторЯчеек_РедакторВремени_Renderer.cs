namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using DevExpress.XtraEditors;
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;

    internal class РедакторЯчеек_РедакторВремени_Renderer : GridGenericControlCellRenderer
    {
        private TimeEdit drawРедакторВремени;
        private TimeEdit editРедакторВремени;

        public РедакторЯчеек_РедакторВремени_Renderer(GridControlBase grid, GridCellModelBase cellModel) : base(grid, cellModel)
        {
            this.drawРедакторВремени = new TimeEdit();
            this.editРедакторВремени = new TimeEdit();
            this.editРедакторВремени.EditValueChanged += new EventHandler(this.editПолеВыбораДаты_EditValueChanged);
            base.SupportsFocusControl = true;
            base.FixControlParent(this.drawРедакторВремени);
            base.SetControl(this.editРедакторВремени);
        }

        private void editПолеВыбораДаты_EditValueChanged(object sender, EventArgs e)
        {
            if (base.NotifyCurrentCellChanging())
            {
                this.ControlValue = this.editРедакторВремени.Time;
                base.NotifyCurrentCellChanged();
            }
        }

        protected override void InitializeControlText(object controlValue)
        {
            РедакторЯчеек_РедакторВремени.ПроинициализироватьЗначение(this.drawРедакторВремени, controlValue);
        }

        protected override void OnDraw(Graphics g, Rectangle clientRectangle, int rowIndex, int colIndex, GridStyleInfo style)
        {
            if (base.ShouldDrawFocused(rowIndex, colIndex))
            {
                style.Control = this.editРедакторВремени;
            }
            else
            {
                style.Control = this.drawРедакторВремени;
                РедакторЯчеек_РедакторВремени.ПроинициализироватьЗначение(this.drawРедакторВремени, style.CellValue);
                this.drawРедакторВремени.BackColor = style.BackColor;
                this.drawРедакторВремени.ForeColor = style.TextColor;
                this.drawРедакторВремени.Enabled = style.Enabled;
            }
            base.OnDraw(g, clientRectangle, rowIndex, colIndex, style);
        }

        protected override void OnInitialize(int rowIndex, int colIndex)
        {
            GridStyleInfo viewStyleInfo = base.Grid.GetViewStyleInfo(rowIndex, colIndex);
            if (viewStyleInfo.ReadOnly)
            {
                this.editРедакторВремени.Enabled = false;
            }
            РедакторЯчеек_РедакторВремени.ПроинициализироватьЗначение(this.editРедакторВремени, viewStyleInfo.CellValue);
        }

        protected override bool OnSaveChanges()
        {
            base.Grid.Model[base.RowIndex, base.ColIndex].CellValue = this.editРедакторВремени.Time;
            return true;
        }
    }
}

