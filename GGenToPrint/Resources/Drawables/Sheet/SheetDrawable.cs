namespace GGenToPrint.Resources.Drawables.Sheet
{
    public class SheetDrawable : IDrawable
    {
        public byte NumCellsOfVertical { get; set; }
        public byte NumCellsOfHorizontal { get; set; }
        public byte NumCellsOfMargin { get; set; }
        public byte SheetTypeIndex { get; set; }
        public byte SheetPositionIndex { get; set; }

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

            // Borders
            canvas.DrawLine(rectF.Left, rectF.Top, cellSize * (float)NumCellsOfHorizontal, rectF.Top);
            canvas.DrawLine(cellSize * (float)NumCellsOfHorizontal, rectF.Top, cellSize * (float)NumCellsOfHorizontal, cellSize * (float)NumCellsOfVertical);
            canvas.DrawLine(cellSize * (float)NumCellsOfHorizontal, cellSize * (float)NumCellsOfVertical, rectF.Left, cellSize * (float)NumCellsOfVertical);
            canvas.DrawLine(rectF.Left, cellSize * (float)NumCellsOfVertical, rectF.Left, rectF.Top);

            // Cells and rows
            if (SheetTypeIndex == 0)
            {
                for (int i = 1; i < NumCellsOfHorizontal; i++)
                {
                    canvas.DrawLine(cellSize * i, rectF.Top, cellSize * i, cellSize * (float)NumCellsOfVertical);
                }
                for (int i = 1; i < NumCellsOfVertical; i++)
                {
                    canvas.DrawLine(rectF.Left, cellSize * i, cellSize * (float)NumCellsOfHorizontal, cellSize * i);
                }
            }
            else if (SheetTypeIndex == 1)
            {
                for (int i = 1; i < NumCellsOfVertical; i++)
                {
                    canvas.DrawLine(rectF.Left, cellSize * i, cellSize * (float)NumCellsOfHorizontal, cellSize * i);
                }
            }

            // Margin
            if (NumCellsOfMargin > 0)
            {
                canvas.StrokeSize = 3;
                canvas.StrokeColor = Colors.Red;
                if (SheetPositionIndex == 0)
                {
                    canvas.DrawLine(
                        cellSize * (float)NumCellsOfHorizontal - cellSize * (float)NumCellsOfMargin,
                        rectF.Top,
                        cellSize * (float)NumCellsOfHorizontal - cellSize * (float)NumCellsOfMargin,
                        cellSize * (float)NumCellsOfVertical);
                }
                else if (SheetPositionIndex == 1)
                {
                    canvas.DrawLine(
                        cellSize * (float)NumCellsOfMargin,
                        rectF.Top,
                        cellSize * (float)NumCellsOfMargin,
                        cellSize * (float)NumCellsOfVertical);
                }
                canvas.StrokeSize = 1;
                canvas.StrokeColor = drawColor;
            }
        }
    }
}