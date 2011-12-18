namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using DevExpress.XtraEditors;
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using System.Runtime.Serialization;

    [Serializable]
    internal class РедакторЯчеек_РедакторВремени : GridGenericControlCellModel
    {
        public РедакторЯчеек_РедакторВремени(GridModel grid) : base(grid)
        {
            base.AllowFloating = false;
        }

        protected РедакторЯчеек_РедакторВремени(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            return new РедакторЯчеек_РедакторВремени_Renderer(control, this);
        }

        protected override Size OnQueryPrefferedClientSize(Graphics g, int rowIndex, int colIndex, GridStyleInfo style, GridQueryBounds queryBounds)
        {
            return new Size(base.OnQueryPrefferedClientSize(g, rowIndex, colIndex, style, queryBounds).Width, 40);
        }

        public static void ПроинициализироватьЗначение(TimeEdit редакторВремени, object Значение)
        {
            редакторВремени.Time = (DateTime) Значение;
        }
    }
}

