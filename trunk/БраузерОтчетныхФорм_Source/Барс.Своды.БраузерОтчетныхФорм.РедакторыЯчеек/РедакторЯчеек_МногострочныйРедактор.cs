namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;
    using Барс.Интерфейс;

    [Serializable]
    internal class РедакторЯчеек_МногострочныйРедактор : GridGenericControlCellModel
    {
        public РедакторЯчеек_МногострочныйРедактор(GridModel grid) : base(grid)
        {
            base.AllowFloating = false;
        }

        protected РедакторЯчеек_МногострочныйРедактор(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            return new РедакторЯчеек_МногострочныйРедактор_Renderer(control, this);
        }

        protected override Size OnQueryPrefferedClientSize(Graphics g, int rowIndex, int colIndex, GridStyleInfo style, GridQueryBounds queryBounds)
        {
            return new Size(base.OnQueryPrefferedClientSize(g, rowIndex, colIndex, style, queryBounds).Width, 40);
        }

        public static void ПроинициализироватьЗначение(ВыпадающийТекст полеМногострРедактора, object Значение)
        {
            полеМногострРедактора.Текст = Значение.ToString();
        }
    }
}

