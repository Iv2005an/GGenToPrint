public class Cells
{
    static public void Draw(
        ICanvas canvas,
        float left,
        float top,
        float right,
        float bottom,
        float cellSize,
        byte numCellsOfHorizontal,
        byte numCellsOfVertical,
        bool isCells = true)
    {
        PathF border = new();
        border.MoveTo(left, bottom);
        border.LineTo(left, top);
        border.LineTo(right, top);
        border.LineTo(right, bottom);
        border.Close();
        canvas.DrawPath(border);
        for (byte i = 1; i < numCellsOfVertical; i++)
        {
            canvas.DrawLine(left, top + cellSize * i, right, top + cellSize * i);
        }
        if (isCells)
        {
            for (byte i = 1; i < numCellsOfHorizontal; i++)
            {
                canvas.DrawLine(left + cellSize * i, top, left + cellSize * i, bottom);
            }
        }
    }
}