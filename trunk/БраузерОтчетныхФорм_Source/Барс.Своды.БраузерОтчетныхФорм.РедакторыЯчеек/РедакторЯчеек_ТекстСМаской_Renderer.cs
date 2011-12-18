namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using Барс.Интерфейс;

    internal class РедакторЯчеек_ТекстСМаской_Renderer : GridGenericControlCellRenderer
    {
        private ПолеВводаТекста drawРедакторСМаской;
        private ПолеВводаТекста editРедакторСМаской;

        public РедакторЯчеек_ТекстСМаской_Renderer(GridControlBase grid, GridCellModelBase cellModel) : base(grid, cellModel)
        {
            this.drawРедакторСМаской = new ПолеВводаТекста();
            this.editРедакторСМаской = new ПолеВводаТекста();
            this.editРедакторСМаской.EditValueChanged += new EventHandler(this.editТекстСМаской_EditValueChanged);
            base.SupportsFocusControl = true;
            base.FixControlParent(this.drawРедакторСМаской);
            base.SetControl(this.editРедакторСМаской);
        }

        private void editТекстСМаской_EditValueChanged(object sender, EventArgs e)
        {
            if (base.NotifyCurrentCellChanging())
            {
                this.ControlValue = this.editРедакторСМаской.Текст;
                base.NotifyCurrentCellChanged();
            }
        }

        protected override void InitializeControlText(object controlValue)
        {
            РедакторЯчеек_ТекстСМаской.ПроинициализироватьЗначение(this.drawРедакторСМаской, "", controlValue);
        }

        protected override void OnDraw(Graphics g, Rectangle clientRectangle, int rowIndex, int colIndex, GridStyleInfo style)
        {
            if (base.ShouldDrawFocused(rowIndex, colIndex))
            {
                style.Control = this.editРедакторСМаской;
            }
            else
            {
                style.Control = this.drawРедакторСМаской;
                РедакторЯчеек_ТекстСМаской.ПроинициализироватьЗначение(this.drawРедакторСМаской, style.Description, style.CellValue);
                this.drawРедакторСМаской.BackColor = style.BackColor;
                this.drawРедакторСМаской.ForeColor = style.TextColor;
                this.drawРедакторСМаской.Enabled = style.Enabled;
            }
            style.Control.BackColor = style.BackColor;
            base.OnDraw(g, clientRectangle, rowIndex, colIndex, style);
        }

        protected override void OnInitialize(int rowIndex, int colIndex)
        {
            GridStyleInfo viewStyleInfo = base.Grid.GetViewStyleInfo(rowIndex, colIndex);
            if (viewStyleInfo.ReadOnly)
            {
                this.editРедакторСМаской.Enabled = false;
            }
            РедакторЯчеек_ТекстСМаской.ПроинициализироватьЗначение(this.editРедакторСМаской, viewStyleInfo.Description, viewStyleInfo.CellValue);
        }

        protected override bool OnSaveChanges()
        {
            base.Grid.Model[base.RowIndex, base.ColIndex].CellValue = this.editРедакторСМаской.EditValue;
            return true;
        }
    }
}

