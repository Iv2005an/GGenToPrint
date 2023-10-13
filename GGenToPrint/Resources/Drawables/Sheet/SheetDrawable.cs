namespace GGenToPrint.Resources.Drawables.Sheet
{
    public class SheetDrawable : IDrawable
    {
        public double NumCellsOfVertical { get; set; }
        public double NumCellsOfHorizontal { get; set; }
        public double NumCellsOfMargin { get; set; }

        public void Draw(ICanvas canvas, RectF rectF)
        {
            AppTheme appTheme = Application.Current.RequestedTheme;
            canvas.StrokeColor = appTheme == AppTheme.Dark ? Colors.White : Colors.Black;

            canvas.StrokeSize = 1;

            // Border
            canvas.DrawLine(rectF.Left, rectF.Top, rectF.Right, rectF.Top);
            canvas.DrawLine(rectF.Right, rectF.Top, rectF.Right, rectF.Bottom);
            canvas.DrawLine(rectF.Right, rectF.Bottom, rectF.Left, rectF.Bottom);
            canvas.DrawLine(rectF.Left, rectF.Bottom, rectF.Left, rectF.Top);
        }
    }
}

