namespace GGenToPrint.Resources.Drawables.Sheet
{
    public class SheetDrawable : IDrawable
    {
        public double NumCellsOfVertical { get; set; }
        public double NumCellsOfHorizontal { get; set; }
        public double NumCellsOfMargin { get; set; }

        public void Draw(ICanvas canvas, RectF rectF)
        {
            // Get color for paint
            AppTheme appTheme = Application.Current.RequestedTheme;
            Color drawColor = appTheme == AppTheme.Dark ? Colors.White : Colors.Black;
            canvas.StrokeColor = drawColor;

            // Get cell size
            float cellSize = rectF.Width / (float)NumCellsOfHorizontal;
            if (NumCellsOfVertical * cellSize > rectF.Height)
            {
                cellSize = rectF.Height / (float)NumCellsOfVertical;
            }

            // Border
            canvas.DrawLine(rectF.Left, rectF.Top, cellSize * (float)NumCellsOfHorizontal, rectF.Top);
            canvas.DrawLine(cellSize * (float)NumCellsOfHorizontal, rectF.Top, cellSize * (float)NumCellsOfHorizontal, cellSize * (float)NumCellsOfVertical);
            canvas.DrawLine(cellSize * (float)NumCellsOfHorizontal, cellSize * (float)NumCellsOfVertical, rectF.Left, cellSize * (float)NumCellsOfVertical);
            canvas.DrawLine(rectF.Left, cellSize * (float)NumCellsOfVertical, rectF.Left, rectF.Top);

            // Cells
            for (int i = 1; i < NumCellsOfHorizontal; i++)
            {
                canvas.DrawLine(cellSize * i, rectF.Top, cellSize * i, cellSize * (float)NumCellsOfVertical);
            }
            for (int i = 1; i < NumCellsOfVertical; i++)
            {
                canvas.DrawLine(rectF.Left, cellSize * i, cellSize * (float)NumCellsOfHorizontal, cellSize * i);
            }

            // Margin
            if (NumCellsOfMargin > 0)
            {
                canvas.StrokeSize = 3;
                canvas.StrokeColor = Colors.Red;
                canvas.DrawLine(
                    cellSize * (float)NumCellsOfHorizontal - cellSize * (float)NumCellsOfMargin,
                    rectF.Top,
                    cellSize * (float)NumCellsOfHorizontal - cellSize * (float)NumCellsOfMargin,
                    cellSize * (float)NumCellsOfVertical);
            }
        }
    }
}