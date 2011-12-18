namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using DevExpress.XtraEditors.Mask;
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using Барс.Интерфейс;

    [Serializable]
    internal class РедакторЯчеек_ТекстСМаской : GridGenericControlCellModel
    {
        public РедакторЯчеек_ТекстСМаской(GridModel grid) : base(grid)
        {
            base.AllowFloating = false;
        }

        protected РедакторЯчеек_ТекстСМаской(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            return new РедакторЯчеек_ТекстСМаской_Renderer(control, this);
        }

        protected override Size OnQueryPrefferedClientSize(Graphics g, int rowIndex, int colIndex, GridStyleInfo style, GridQueryBounds queryBounds)
        {
            return new Size(base.OnQueryPrefferedClientSize(g, rowIndex, colIndex, style, queryBounds).Width, 40);
        }

        public static void ПроинициализироватьЗначение(ПолеВводаТекста Редактор, string МаскаRegEx, object Значение)
        {
            if (!string.IsNullOrEmpty(МаскаRegEx))
            {
                Редактор.Properties.Mask.MaskType = MaskType.RegEx;
                Редактор.Properties.Mask.EditMask = МаскаRegEx;
                Редактор.Properties.Mask.ShowPlaceHolders = true;
                Редактор.Properties.Mask.UseMaskAsDisplayFormat = true;
                Редактор.Properties.Mask.IgnoreMaskBlank = false;
            }
            else
            {
                Редактор.Properties.Mask.MaskType = MaskType.None;
            }
            Редактор.Текст = Значение.ToString();
        }
    }
}

