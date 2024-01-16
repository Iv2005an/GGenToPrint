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
    public IEnumerable<Letter> Letters { get; set; }

    public void Draw(ICanvas canvas, RectF rectF)
    {
        canvas.StrokeSize = 1;

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

        Cells.Draw(canvas,
            left, 
            top, 
            right, 
            bottom, 
            cellSize, 
            NumCellsOfHorizontal, 
            NumCellsOfVertical, 
            SheetTypeIndex == 0);

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
                    left + cellSize * NumCellsOfMargin,
                    top,
                    left + cellSize * NumCellsOfMargin,
                    bottom);
            }
            canvas.StrokeSize = 1;
            canvas.StrokeColor = drawColor;
        }

        // Text
        if (Text is not null && Letters is not null)
        {
            canvas.StrokeLineCap = LineCap.Round;

            float xOffset = 0;
            float yOffset = 0;

            foreach (var character in Text)
            {
                var letter = Letters.Where(letter => letter.Character[0] == character).FirstOrDefault();
                if (letter is not null && letter.Commands is not null)
                {
                    float maxX = 0;
                    Gcommand lastCommand = null;
                    foreach (var command in Gcommand.ParseCommands(letter.Commands))
                    {
                        canvas.StrokeColor = Colors.Blue;
                        canvas.StrokeSize = cellSize / 10;
                        var x = command.XCoordinate * cellSize + xOffset;
                        if (x > NumCellsOfHorizontal * cellSize - cellSize / 20)
                        {
                            x = NumCellsOfHorizontal * cellSize - cellSize / 20;
                            canvas.StrokeColor = Colors.Red;
                        }
                        var y = command.YCoordinate * cellSize + yOffset;
                        if (y > NumCellsOfVertical * cellSize - cellSize / 20)
                        {
                            y = NumCellsOfVertical * cellSize - cellSize / 20;
                            canvas.StrokeColor = Colors.Red;
                        }

                        if (x > maxX) maxX = x;

                        if (lastCommand is null || command.Gcode == "G0")
                        {
                            lastCommand = command;
                        }
                        else
                        {
                            canvas.DrawLine(
                                left + x,
                                top + y,
                                left + x,
                                top + y);
                            lastCommand = command;
                        }
                    }
                    xOffset = maxX;
                }
            }
        }
    }
}