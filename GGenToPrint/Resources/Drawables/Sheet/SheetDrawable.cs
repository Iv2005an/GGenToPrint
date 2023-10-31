namespace GGenToPrint.Resources.Drawables.Sheet;

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

        // Get draw position
        var top = rectF.Height / 2 - cellSize * NumCellsOfVertical / 2;
        var bottom = top + cellSize * NumCellsOfVertical;
        var left = rectF.Width / 2 - cellSize * NumCellsOfHorizontal / 2;
        var right = left + cellSize * NumCellsOfHorizontal;

        // Borders
        canvas.DrawLine(left, top, right, top);
        canvas.DrawLine(right, top, right, bottom);
        canvas.DrawLine(right, bottom, left, bottom);
        canvas.DrawLine(left, bottom, left, top);

        // Cells and rows
        if (SheetTypeIndex == 0)
        {
            for (byte i = 1; i < NumCellsOfHorizontal; i++)
            {
                canvas.DrawLine(left + cellSize * i, top, left + cellSize * i, bottom);
            }
            for (byte i = 1; i < NumCellsOfVertical; i++)
            {
                canvas.DrawLine(left, top + cellSize * i, right, top + cellSize * i);
            }
        }
        else if (SheetTypeIndex == 1)
        {
            for (byte i = 1; i < NumCellsOfVertical; i++)
            {
                canvas.DrawLine(left, top + cellSize * i, right, top + cellSize * i);
            }
        }

        // Margin
        if (NumCellsOfMargin > 0)
        {
            canvas.StrokeSize = 5;
            canvas.StrokeColor = Colors.Red;
            if (SheetPositionIndex == 0)
            {
                canvas.DrawLine(
                    right - cellSize * NumCellsOfMargin,
                    top,
                    right - cellSize * NumCellsOfMargin,
                    bottom);
            }
            else if (SheetPositionIndex == 1)
            {
                canvas.DrawLine(
                    cellSize * NumCellsOfMargin,
                    top,
                    cellSize * NumCellsOfMargin,
                    bottom);
            }
            canvas.StrokeSize = 1;
            canvas.StrokeColor = drawColor;
        }
    }
}