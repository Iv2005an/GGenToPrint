using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.Drawables
{
    public static class CharacterSheet
    {
        static public void DrawSheet(
            ICanvas canvas,
            float left,
            float top,
            float right,
            float bottom,
            float smallestSide,
            float cellSize)
        {
            PathF border = new();
            border.MoveTo(left, bottom);
            border.LineTo(left, top);
            border.LineTo(right, top);
            border.LineTo(right, bottom);
            border.Close();
            canvas.DrawPath(border);

            PathF horizontalLines = new();
            for (byte i = 1; i < 4; i++)
            {
                horizontalLines.MoveTo(left, top + cellSize * i);
                horizontalLines.LineTo(right, top + cellSize * i);
            }
            horizontalLines.Close();
            canvas.DrawPath(horizontalLines);

            PathF verticalLines = new();
            for (byte i = 1; i < 4; i++)
            {
                verticalLines.MoveTo(left + cellSize * i, top);
                verticalLines.LineTo(left + cellSize * i, bottom);
            }
            verticalLines.Close();
            canvas.DrawPath(verticalLines);

            canvas.StrokeSize = 5;
            canvas.StrokeColor = Colors.LightBlue;
            canvas.DrawLine(left, top + cellSize * 2, right, top + cellSize * 2);
            canvas.DrawLine(left + cellSize * 2, top, left + cellSize * 2, bottom);

            canvas.FillColor = Colors.Red;
            canvas.Alpha = 0.2f;
            canvas.FillRectangle(left, top, smallestSide / 2, smallestSide);
            canvas.Alpha = 1f;
        }
        static public void DrawCharacter(ICanvas canvas, float left, float top, float cellSize, string GCode)
        {
            canvas.StrokeSize = cellSize / 10;
            canvas.StrokeColor = Colors.Blue;
            canvas.StrokeLineCap = LineCap.Round;
            if (GCode != null)
            {
                GCommand lastCommand = null;
                foreach (var gCommand in GCommand.ParseCommands(GCode))
                {
                    if (lastCommand is null || gCommand.GCode == "G0")
                    {
                        lastCommand = gCommand;
                    }
                    else
                    {
                        canvas.DrawLine(
                            left + lastCommand.XCoordinate * cellSize + 2 * cellSize,
                            top + lastCommand.YCoordinate * cellSize,
                            left + gCommand.XCoordinate * cellSize + 2 * cellSize,
                            top + gCommand.YCoordinate * cellSize);
                        lastCommand = gCommand;
                    }
                }
            }
        }
    }
}