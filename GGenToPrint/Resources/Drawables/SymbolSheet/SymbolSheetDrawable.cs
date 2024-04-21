using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.Drawables.SymbolSheet;

public class SymbolSheetDrawable : IDrawable
{
    public string SymbolGCode { get; set; }

    public float CellSize { get; set; }

    public float Left { get; set; }
    public float Top { get; set; }

    public void Draw(ICanvas canvas, RectF rectF)
    {
        byte numCellsOfHorizontal = 4;
        byte numCellsOfVertical = 4;

        float padding = 16;
        float width = rectF.Width - padding * 2;
        float height = rectF.Height - padding * 2;
        CellSize = width / numCellsOfHorizontal;
        if (numCellsOfVertical * CellSize > height)
        {
            CellSize = height / numCellsOfVertical;
        }
        Left = width / 2 - CellSize * 2 + padding;
        Top = height / 2 - CellSize * 2 + padding;
        float right = Left + CellSize * 4;
        float bottom = Top + CellSize * 4;

        SheetDrawer.DrawLayout(canvas, Left, Top, right, bottom, CellSize, 4, 4);
        SheetDrawer.DrawEditSymbolBackground(canvas, Left, Top, right, bottom);

        PathF drawPath = new();
        PathF startPointPath = new();
        foreach (GCommand gCommand in GCommand.ParseCommands(SymbolGCode))
        {
            float x = Top + gCommand.YCoordinate * CellSize + CellSize * 2;
            float y = Left + gCommand.XCoordinate * CellSize + CellSize * 2;
            if (gCommand.GType == 0)
            {
                drawPath.MoveTo(x, y);
                startPointPath.AppendCircle(x, y, CellSize / 50);
            }
            else if (gCommand.GType == 1)
            {
                drawPath.LineTo(x, y);
            }
        }
        canvas.StrokeSize = CellSize / 10;
        canvas.StrokeColor = Colors.Blue;
        canvas.StrokeLineCap = LineCap.Round;
        canvas.StrokeLineJoin = LineJoin.Round;
        
        canvas.DrawPath(drawPath);
        canvas.StrokeColor = Colors.Green;
        canvas.DrawPath(startPointPath);
    }
}