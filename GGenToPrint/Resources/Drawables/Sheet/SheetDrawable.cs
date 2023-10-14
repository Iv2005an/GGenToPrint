namespace GGenToPrint.Resources.Drawables.Sheet
{
    public class SheetDrawable : IDrawable
    {
        public double NumCellsOfVertical { get; set; }
        public double NumCellsOfHorizontal { get; set; }
        public double MarginSize { get; set; }
        public int SheetType { get; set; }
        public int SheetPosition { get; set; }

        public void Draw(ICanvas canvas, RectF rectF)
        {
            // Get color for paint
            AppTheme appTheme = Application.Current.RequestedTheme;
            Color drawColor = appTheme == AppTheme.Dark ? Colors.White : Colors.Black;
            canvas.StrokeColor = drawColor;

            // Get cell size
            float cellSize = rectF.Width / (float)NumCellsOfHorizontal;
            if (NumCellsOfVertical * cellSize > rectF.Height || SheetType == 1)
            {
                cellSize = rectF.Height / (float)NumCellsOfVertical;
            }
            float cellHorizontalSize = 0;
            if (SheetType == 1)
            {
                cellHorizontalSize = rectF.Width / (float)NumCellsOfHorizontal;
            }

            // Borders
            if (SheetType == 0)
            {
                canvas.DrawLine(rectF.Left, rectF.Top, cellSize * (float)NumCellsOfHorizontal, rectF.Top);
                canvas.DrawLine(cellSize * (float)NumCellsOfHorizontal, rectF.Top, cellSize * (float)NumCellsOfHorizontal, cellSize * (float)NumCellsOfVertical);
                canvas.DrawLine(cellSize * (float)NumCellsOfHorizontal, cellSize * (float)NumCellsOfVertical, rectF.Left, cellSize * (float)NumCellsOfVertical);
                canvas.DrawLine(rectF.Left, cellSize * (float)NumCellsOfVertical, rectF.Left, rectF.Top);
            }
            else if (SheetType == 1)
            {
                canvas.DrawLine(rectF.Left, rectF.Top, rectF.Right, rectF.Top);
                canvas.DrawLine(rectF.Right, rectF.Top, rectF.Right, rectF.Bottom);
                canvas.DrawLine(rectF.Right, rectF.Bottom, rectF.Left, rectF.Bottom);
                canvas.DrawLine(rectF.Left, rectF.Bottom, rectF.Left, rectF.Top);
            }

            // Cells and rows
            if (SheetType == 0)
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
            else if (SheetType == 1)
            {
                for (int i = 1; i < NumCellsOfVertical; i++)
                {
                    canvas.DrawLine(rectF.Left, cellSize * i, rectF.Right, cellSize * i);
                }
            }

            // Margin
            if (MarginSize > 0)
            {
                canvas.StrokeSize = 3;
                canvas.StrokeColor = Colors.Red;
                if (SheetType == 0)
                {
                    if (SheetPosition == 0)
                    {
                        canvas.DrawLine(
                            cellSize * (float)NumCellsOfHorizontal - cellSize * (float)MarginSize,
                            rectF.Top,
                            cellSize * (float)NumCellsOfHorizontal - cellSize * (float)MarginSize,
                            cellSize * (float)NumCellsOfVertical);
                    }
                    else if (SheetPosition == 1)
                    {
                        canvas.DrawLine(
                            cellSize * (float)MarginSize,
                            rectF.Top,
                            cellSize * (float)MarginSize,
                            cellSize * (float)NumCellsOfVertical);
                    }
                }
                else if (SheetType == 1)
                {
                    if (SheetPosition == 0)
                    {
                        canvas.DrawLine(
                            rectF.Width - cellHorizontalSize * (float)MarginSize,
                            rectF.Top,
                            rectF.Width - cellHorizontalSize * (float)MarginSize,
                            rectF.Bottom);
                    }
                    else if (SheetPosition == 1)
                    {
                        canvas.DrawLine(
                            cellHorizontalSize * (float)MarginSize,
                            rectF.Top,
                            cellHorizontalSize * (float)MarginSize,
                            rectF.Bottom);
                    }
                }
            }
        }
    }
}