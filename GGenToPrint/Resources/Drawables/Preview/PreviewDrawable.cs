using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.Drawables.Preview;

public class PreviewDrawable : IDrawable
{
    public string Commands { get; set; }

    public void Draw(ICanvas canvas, RectF rectF)
    {
#if DEBUG
        Commands =
            "G0 X2 Y1\n" +
            "G1 X2 Y4\n" +
            "G1 X3 Y5\n" +
            "G1 X4 Y4\n" +
            "G1 X4 Y1\n" +
            "G1 X4 Y5\n" +
            "G0 X2 Y0\n" +
            "G1 X3 Y1\n" +
            "G1 X4 Y0\n" +
            "G0 X4 Y5";
#endif
        // Get color for paint
        AppTheme appTheme = Application.Current.RequestedTheme;
        Color drawColor = appTheme == AppTheme.Dark ? Colors.White : Colors.Black;
        canvas.StrokeColor = drawColor;
        canvas.FontColor = drawColor;

        // Get draw position
        var smallerSide = rectF.Width < rectF.Height ? rectF.Width : rectF.Height;
        var top = rectF.Height / 2 - smallerSide / 2;
        var bottom = top + smallerSide;
        var left = rectF.Left;
        var right = left + smallerSide;

        var cellSize = (right - left) / 6;

        // Borders
        canvas.DrawLine(left, top, right, top);
        canvas.DrawLine(right, top, right, bottom);
        canvas.DrawLine(right, bottom, left, bottom);
        canvas.DrawLine(left, bottom, left, top);

        // Cells
        for (byte i = 1; i <= 6; i++)
        {
            canvas.DrawLine(left, top + cellSize * i, right, top + cellSize * i);
            canvas.DrawLine(left + cellSize * i, top, left + cellSize * i, bottom);
        }
        canvas.StrokeColor = Colors.LightBlue;
        canvas.DrawLine(left, top + cellSize * 3, right, top + cellSize * 3);

        //Draw character
        canvas.StrokeColor = Colors.Blue;
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
                        left + lastCommand.XCoordinate * cellSize,
                        top + lastCommand.YCoordinate * cellSize,
                        left + command.XCoordinate * cellSize,
                        top + command.YCoordinate * cellSize);
                    lastCommand = command;

                }
            }
        }
        canvas.StrokeColor = drawColor;
    }
}