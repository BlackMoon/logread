namespace Барс.Своды.БраузерОтчетныхФорм.РедакторыЯчеек
{
    using Syncfusion.Windows.Forms.Grid;
    using System;
    using System.Drawing;
    using System.IO;
    using System.Runtime.Serialization;
    using Барс.Интерфейс;

    [Serializable]
    internal class РедакторЯчеек_РедакторИзображения : GridGenericControlCellModel
    {
        public РедакторЯчеек_РедакторИзображения(GridModel grid) : base(grid)
        {
            base.AllowFloating = false;
        }

        protected РедакторЯчеек_РедакторИзображения(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override GridCellRendererBase CreateRenderer(GridControlBase control)
        {
            return new РедакторЯчеек_РедакторИзображения_Renderer(control, this);
        }

        protected override Size OnQueryPrefferedClientSize(Graphics g, int rowIndex, int colIndex, GridStyleInfo style, GridQueryBounds queryBounds)
        {
            return new Size(base.OnQueryPrefferedClientSize(g, rowIndex, colIndex, style, queryBounds).Width, 40);
        }

        public static void ПроинициализироватьЗначение(Изображение редакторИзображения, object Значение)
        {
            if (!(!(Значение is string) || string.IsNullOrEmpty((string) Значение)))
            {
                редакторИзображения.Картинка = Image.FromStream(new MemoryStream(Convert.FromBase64String((string) Значение)));
            }
            else if (Значение is Image)
            {
                редакторИзображения.Картинка = (Image) Значение;
            }
            else
            {
                редакторИзображения.Картинка = null;
            }
        }
    }
}

