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
            float cellSize = rectF.Width / NumCellsOfHorizontal;
            if (NumCellsOfVertical * cellSize > rectF.Height)
            {
                cellSize = rectF.Height / NumCellsOfVertical;
            }

            // Borders
            canvas.DrawLine(rectF.Left, rectF.Top, cellSize * NumCellsOfHorizontal, rectF.Top);
            canvas.DrawLine(cellSize * NumCellsOfHorizontal, rectF.Top, cellSize * NumCellsOfHorizontal, cellSize * NumCellsOfVertical);
            canvas.DrawLine(cellSize * NumCellsOfHorizontal, cellSize * NumCellsOfVertical, rectF.Left, cellSize * NumCellsOfVertical);
            canvas.DrawLine(rectF.Left, cellSize * NumCellsOfVertical, rectF.Left, rectF.Top);

            // Cells and rows
            if (SheetTypeIndex == 0)
            {
                for (int i = 1; i < NumCellsOfHorizontal; i++)
                {
                    canvas.DrawLine(cellSize * i, rectF.Top, cellSize * i, cellSize * NumCellsOfVertical);
                }
                for (int i = 1; i < NumCellsOfVertical; i++)
                {
                    canvas.DrawLine(rectF.Left, cellSize * i, cellSize * NumCellsOfHorizontal, cellSize * i);
                }
            }
            else if (SheetTypeIndex == 1)
            {
                for (int i = 1; i < NumCellsOfVertical; i++)
                {
                    canvas.DrawLine(rectF.Left, cellSize * i, cellSize * NumCellsOfHorizontal, cellSize * i);
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
                        cellSize * NumCellsOfHorizontal - cellSize * NumCellsOfMargin,
                        rectF.Top,
                        cellSize * NumCellsOfHorizontal - cellSize * NumCellsOfMargin,
                        cellSize * NumCellsOfVertical);
                }
                else if (SheetPositionIndex == 1)
                {
                    canvas.DrawLine(
                        cellSize * NumCellsOfMargin,
                        rectF.Top,
                        cellSize * NumCellsOfMargin,
                        cellSize * NumCellsOfVertical);
                }
                canvas.StrokeSize = 1;
                canvas.StrokeColor = drawColor;
            }
        }
    }
}