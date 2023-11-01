using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.Drawables.Preview;

public class PreviewDrawable : IDrawable
{
    public string Commands { get; set; }

    public void Draw(ICanvas canvas, RectF rectF)
    {
        // Get color for paint
        AppTheme appTheme = Application.Current.RequestedTheme;
        Color drawColor = appTheme == AppTheme.Dark ? Colors.White : Colors.Black;
        canvas.StrokeColor = drawColor;

        // Get draw position
        var smallerSide = rectF.Width < rectF.Height ? rectF.Width : rectF.Height;
        var top = rectF.Height / 2 - smallerSide / 2;
        var bottom = top + smallerSide;
        var left = rectF.Left;
        var right = left + smallerSide;

        var cellSize = (right - left) / 4;

        // Borders
        canvas.DrawLine(left, top, right, top);
        canvas.DrawLine(right, top, right, bottom);
        canvas.DrawLine(right, bottom, left, bottom);
        canvas.DrawLine(left, bottom, left, top);

        // Cells
        for (byte i = 1; i <= 4; i++)
        {
            canvas.DrawLine(left, top + cellSize * i, right, top + cellSize * i);
            canvas.DrawLine(left + cellSize * i, top, left + cellSize * i, bottom);
        }

        // Template
        canvas.StrokeSize = 5;
        canvas.StrokeColor = Colors.LightBlue;
        canvas.DrawLine(left, top + cellSize * 2, right, top + cellSize * 2);
        canvas.DrawLine(left + cellSize * 2, top, left + cellSize * 2, bottom);
        canvas.FillColor = Colors.Red;
        canvas.Alpha = 0.2f;
        canvas.FillRectangle(left, top, smallerSide / 2, smallerSide);
        canvas.Alpha = 1f;

        //Draw character
        canvas.StrokeSize = cellSize / 10;
        canvas.StrokeColor = Colors.Blue;
        canvas.StrokeLineCap = LineCap.Round;
        if (Commands != null)
        {
            Gcommand lastCommand = null;
            foreach (var command in Gcommand.ParseCommands(Commands))
            {
                if (lastCommand is null || command.Gcode == "G0")
                {
                    lastCommand = command;
                }
                else
                {
                    canvas.DrawLine(
                        left + lastCommand.XCoordinate * cellSize + 2 * cellSize,
                        top + lastCommand.YCoordinate * cellSize,
                        left + command.XCoordinate * cellSize + 2 * cellSize,
                        top + command.YCoordinate * cellSize);
                    lastCommand = command;
                }
            }
        }
        canvas.StrokeColor = drawColor;
        canvas.StrokeSize = 1;
    }
}