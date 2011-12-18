namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using Барс.Интерфейс;

    internal class РедакторЯчеек_ВыборИзСправочника_Renderer : GridGenericControlCellRenderer
    {
        private ВыборИзСправочника drawВыборИзСправочника;
        private ВыборИзСправочника editВыборИзСправочника;
        private ПараметрыСозданияРедактора_ВыборИзСправочника параметрыСоздания;

        public РедакторЯчеек_ВыборИзСправочника_Renderer(GridControlBase grid, GridCellModelBase cellModel, ПараметрыСозданияРедактора_ВыборИзСправочника ПараметрыСозданияРедактора) : base(grid, cellModel)
        {
            this.параметрыСоздания = null;
            this.drawВыборИзСправочника = new ВыборИзСправочника();
            this.editВыборИзСправочника = new ВыборИзСправочника();
            this.параметрыСоздания = ПараметрыСозданияРедактора;
            this.drawВыборИзСправочника.ПоказыватьКнопкуРедактирования = false;
            this.drawВыборИзСправочника.НазначитьКнопки();
            this.editВыборИзСправочника.ПоказыватьКнопкуРедактирования = false;
            this.editВыборИзСправочника.НазначитьКнопки();
            if ((this.параметрыСоздания != null) && (this.параметрыСоздания.ОбработкаВыбораИзСправочника != null))
            {
                this.editВыборИзСправочника.ПриПолученииИсточникаЗаписейДляВыбора += this.параметрыСоздания.ОбработкаВыбораИзСправочника;
            }
            base.SupportsFocusControl = true;
            base.FixControlParent(this.drawВыборИзСправочника);
            base.SetControl(this.editВыборИзСправочника);
            this.editВыборИзСправочника.ПриИзмененииВыбранногоЭлемента = (ОбработчикСобытия) Delegate.Combine(this.editВыборИзСправочника.ПриИзмененииВыбранногоЭлемента, new ОбработчикСобытия(this.выборИзСправочника_ИзменениеВыбранногоОбъекта));
        }

        protected override void InitializeControlText(object controlValue)
        {
            РедакторЯчеек_ВыборИзСправочника.ПроинициализироватьЗначение(this.editВыборИзСправочника, controlValue);
        }

        protected override void OnDraw(Graphics g, Rectangle clientRectangle, int rowIndex, int colIndex, GridStyleInfo style)
        {
            if (base.ShouldDrawFocused(rowIndex, colIndex))
            {
                style.Control = this.editВыборИзСправочника;
            }
            else
            {
                style.Control = this.drawВыборИзСправочника;
                РедакторЯчеек_ВыборИзСправочника.ПроинициализироватьВыборИзСправочника(this.drawВыборИзСправочника, style, this.параметрыСоздания);
                this.drawВыборИзСправочника.BackColor = style.BackColor;
                this.drawВыборИзСправочника.ForeColor = style.TextColor;
                this.drawВыборИзСправочника.Enabled = style.Enabled;
            }
            base.OnDraw(g, clientRectangle, rowIndex, colIndex, style);
        }

        protected override void OnInitialize(int rowIndex, int colIndex)
        {
            if (base.Grid.Model[rowIndex, colIndex].ReadOnly)
            {
                this.editВыборИзСправочника.Enabled = false;
            }
            РедакторЯчеек_ВыборИзСправочника.ПроинициализироватьВыборИзСправочника(this.editВыборИзСправочника, base.Grid.GetViewStyleInfo(rowIndex, colIndex), this.параметрыСоздания);
        }

        protected override bool OnSaveChanges()
        {
            base.Grid.Model[base.RowIndex, base.ColIndex].CellValue = this.editВыборИзСправочника.ВыбранныйОбъект;
            return true;
        }

        private void выборИзСправочника_ИзменениеВыбранногоОбъекта(object sender, АргументыСобытия Аргументы)
        {
            if (base.NotifyCurrentCellChanging())
            {
                this.ControlValue = this.editВыборИзСправочника.ВыбранныйОбъект;
                base.NotifyCurrentCellChanged();
            }
        }
    }
}

