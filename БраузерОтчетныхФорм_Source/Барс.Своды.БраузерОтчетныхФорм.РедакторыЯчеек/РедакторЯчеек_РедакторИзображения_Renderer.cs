namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using Барс.Интерфейс;

    internal class РедакторЯчеек_РедакторИзображения_Renderer : GridGenericControlCellRenderer
    {
        private Изображение drawредакторИзображения;
        private Изображение editредакторИзображения;

        public РедакторЯчеек_РедакторИзображения_Renderer(GridControlBase grid, GridCellModelBase cellModel) : base(grid, cellModel)
        {
            this.drawредакторИзображения = new Изображение();
            this.editредакторИзображения = new Изображение();
            this.editредакторИзображения.EditValueChanged += new EventHandler(this.editредакторИзображения_EditValueChanged);
            base.SupportsFocusControl = true;
            base.FixControlParent(this.drawредакторИзображения);
            base.SetControl(this.editредакторИзображения);
        }

        private void editредакторИзображения_EditValueChanged(object sender, EventArgs e)
        {
            if (base.NotifyCurrentCellChanging())
            {
                this.ControlValue = this.editредакторИзображения.Картинка;
                base.NotifyCurrentCellChanged();
            }
        }

        protected override void InitializeControlText(object controlValue)
        {
            РедакторЯчеек_РедакторИзображения.ПроинициализироватьЗначение(this.drawредакторИзображения, controlValue);
        }

        protected override void OnDraw(Graphics g, Rectangle clientRectangle, int rowIndex, int colIndex, GridStyleInfo style)
        {
            if (base.ShouldDrawFocused(rowIndex, colIndex))
            {
                style.Control = this.editредакторИзображения;
            }
            else
            {
                style.Control = this.drawредакторИзображения;
                РедакторЯчеек_РедакторИзображения.ПроинициализироватьЗначение(this.drawредакторИзображения, style.CellValue);
                this.drawредакторИзображения.BackColor = style.BackColor;
                this.drawредакторИзображения.ForeColor = style.TextColor;
                this.drawредакторИзображения.Enabled = style.Enabled;
            }
            base.OnDraw(g, clientRectangle, rowIndex, colIndex, style);
        }

        protected override void OnInitialize(int rowIndex, int colIndex)
        {
            GridStyleInfo viewStyleInfo = base.Grid.GetViewStyleInfo(rowIndex, colIndex);
            if (viewStyleInfo.ReadOnly)
            {
                this.editредакторИзображения.Enabled = false;
            }
            РедакторЯчеек_РедакторИзображения.ПроинициализироватьЗначение(this.editредакторИзображения, viewStyleInfo.CellValue);
        }

        protected override bool OnSaveChanges()
        {
            ImageConverter converter = new ImageConverter();
            byte[] inArray = (byte[]) converter.ConvertTo(this.editредакторИзображения.Картинка, Type.GetType("System.Byte[]"));
            base.Grid.Model[base.RowIndex, base.ColIndex].CellValue = Convert.ToBase64String(inArray);
            return true;
        }
    }
}

