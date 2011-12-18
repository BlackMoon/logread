namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using Барс.Интерфейс;

    internal class РедакторЯчеек_МногострочныйРедактор_Renderer : GridGenericControlCellRenderer
    {
        private ВыпадающийТекст drawПолеМногострРедактора;
        private ВыпадающийТекст editПолеМногострРедактора;

        public РедакторЯчеек_МногострочныйРедактор_Renderer(GridControlBase grid, GridCellModelBase cellModel) : base(grid, cellModel)
        {
            this.drawПолеМногострРедактора = new ВыпадающийТекст();
            this.editПолеМногострРедактора = new ВыпадающийТекст();
            this.editПолеМногострРедактора.EditValueChanged += new EventHandler(this.editПолеМногострРедактора_EditValueChanged);
            base.SupportsFocusControl = true;
            base.FixControlParent(this.drawПолеМногострРедактора);
            base.SetControl(this.editПолеМногострРедактора);
        }

        private void editПолеМногострРедактора_EditValueChanged(object sender, EventArgs e)
        {
            if (base.NotifyCurrentCellChanging())
            {
                this.ControlValue = this.editПолеМногострРедактора.Текст;
                base.NotifyCurrentCellChanged();
            }
        }

        protected override void InitializeControlText(object controlValue)
        {
            РедакторЯчеек_МногострочныйРедактор.ПроинициализироватьЗначение(this.drawПолеМногострРедактора, controlValue);
        }

        protected override void OnDraw(Graphics g, Rectangle clientRectangle, int rowIndex, int colIndex, GridStyleInfo style)
        {
            if (base.ShouldDrawFocused(rowIndex, colIndex))
            {
                style.Control = this.editПолеМногострРедактора;
            }
            else
            {
                style.Control = this.drawПолеМногострРедактора;
                РедакторЯчеек_МногострочныйРедактор.ПроинициализироватьЗначение(this.drawПолеМногострРедактора, style.CellValue);
                this.drawПолеМногострРедактора.BackColor = style.BackColor;
                this.drawПолеМногострРедактора.ForeColor = style.TextColor;
                this.drawПолеМногострРедактора.Enabled = style.Enabled;
            }
            style.Control.BackColor = style.BackColor;
            base.OnDraw(g, clientRectangle, rowIndex, colIndex, style);
        }

        protected override void OnInitialize(int rowIndex, int colIndex)
        {
            GridStyleInfo viewStyleInfo = base.Grid.GetViewStyleInfo(rowIndex, colIndex);
            if (viewStyleInfo.ReadOnly)
            {
                this.editПолеМногострРедактора.Enabled = false;
            }
            РедакторЯчеек_МногострочныйРедактор.ПроинициализироватьЗначение(this.editПолеМногострРедактора, viewStyleInfo.CellValue);
        }

        protected override bool OnSaveChanges()
        {
            base.Grid.Model[base.RowIndex, base.ColIndex].CellValue = this.editПолеМногострРедактора.Текст;
            return true;
        }
    }
}

