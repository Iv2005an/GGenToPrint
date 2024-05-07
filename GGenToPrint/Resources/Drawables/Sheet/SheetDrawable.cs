using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.Drawables.Sheet;

public class SheetDrawable : IDrawable
{
    public byte NumCellsOfVertical { get; set; }
    public byte NumCellsOfHorizontal { get; set; }
    public byte NumCellsOfMargin { get; set; }
    public byte SheetTypeIndex { get; set; }
    public byte SheetPositionIndex { get; set; }
    public string Text { get; set; }
    public IEnumerable<Symbol> Symbols { get; set; }

    public void Draw(ICanvas canvas, RectF rectF)
    {
        float padding = 16;
        float width = rectF.Width - padding * 2;
        float height = rectF.Height - padding * 2;
        float cellSize = width / NumCellsOfHorizontal;
        if (NumCellsOfVertical * cellSize > height)
            cellSize = height / NumCellsOfVertical;
        float left = width / 2 - cellSize * NumCellsOfHorizontal / 2 + padding;
        float top = height / 2 - cellSize * NumCellsOfVertical / 2 + padding;
        float right = left + cellSize * NumCellsOfHorizontal;
        float bottom = top + cellSize * NumCellsOfVertical;

        SheetDrawer.DrawLayout(
            canvas,
            left, top, right, bottom,
            cellSize,
            NumCellsOfHorizontal, NumCellsOfVertical,
            SheetTypeIndex == 1);

        if (NumCellsOfMargin > 0)
        {
            canvas.StrokeSize = cellSize / 4;
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
                    left + cellSize * NumCellsOfMargin,
                    top,
                    left + cellSize * NumCellsOfMargin,
                    bottom);
            }
        }

        if (Text is not null && Symbols is not null)
        {
            float xOffset = 0;
            float yOffset = cellSize * 2;
            PathF textPath = new();
            PathF startPointPath = new();
            PathF outPath = new();
            PointF lastOut = new();
            bool firstOut = true;
            foreach (char s in Text)
            {
                bool firstDraw = true;
                switch (s)
                {
                    case ' ':
                        xOffset += cellSize * 1;
                        break;
                    case '\t':
                        xOffset += cellSize * 4;
                        break;
                    case '\n' or '\r':
                        xOffset = 0;
                        yOffset += cellSize * 2;
                        break;
                    default:
                        float maxX = 0;
                        Symbol currentSymbol = Symbols.FirstOrDefault(symbol => symbol.Sign == s.ToString());
                        if (currentSymbol is not null)
                        {
                            foreach (GCommand gCommand in GCommand.ParseCommands(currentSymbol.GCode))
                            {
                                float x = left + gCommand.YCoordinate * cellSize + xOffset;
                                float y = top + gCommand.XCoordinate * cellSize + yOffset;

                                if (maxX < gCommand.YCoordinate) maxX = gCommand.YCoordinate;

                                if (x > right || y > bottom || x < left || y < top)
                                {
                                    if (x < left) x = left;
                                    if (y < top) y = top;
                                    if (x > right) x = right;
                                    if (y > bottom) y = bottom;
                                    if (firstOut || x != lastOut.X && y != lastOut.Y) outPath.MoveTo(x, y);
                                    else outPath.LineTo(x, y);
                                    lastOut.X = x;
                                    lastOut.Y = y;
                                    firstOut = false;
                                }
                                else
                                {
                                    if (gCommand.GType == 0)
                                    {
                                        textPath.MoveTo(x, y);
                                        startPointPath.AppendCircle(x, y, cellSize / 50);
                                    }
                                    else if (firstDraw) textPath.MoveTo(x, y);
                                    else if (gCommand.GType == 1) textPath.LineTo(x, y);
                                    firstDraw = false;
                                    firstOut = true;
                                }
                            }
                            xOffset += maxX * cellSize + cellSize / 4;
                        }
                        break;
                }
            }
            canvas.StrokeSize = cellSize / 10;
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeLineCap = LineCap.Round;
            canvas.StrokeLineJoin = LineJoin.Round;
            canvas.DrawPath(textPath);
            canvas.StrokeColor = Colors.Green;
            canvas.DrawPath(startPointPath);
            canvas.StrokeColor = Colors.Red;
            canvas.DrawPath(outPath);
        }
    }
}