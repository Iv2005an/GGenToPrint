using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.Drawables.Editor;

public class EditorDrawable : IDrawable
{
    public string Commands { get; set; }

    public float CellSize { get; set; }

    public void Draw(ICanvas canvas, RectF rectF)
    {
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

        CellSize = (right - left) / 4;

        // Borders
        canvas.DrawLine(left, top, right, top);
        canvas.DrawLine(right, top, right, bottom);
        canvas.DrawLine(right, bottom, left, bottom);
        canvas.DrawLine(left, bottom, left, top);

        // Cells
        for (byte i = 1; i <= 4; i++)
        {
            canvas.DrawLine(left, top + CellSize * i, right, top + CellSize * i);
            canvas.DrawLine(left + CellSize * i, top, left + CellSize * i, bottom);
        }

        // Template
        canvas.StrokeSize = 5;
        canvas.StrokeColor = Colors.LightBlue;
        canvas.DrawLine(left, top + CellSize * 2, right, top + CellSize * 2);
        canvas.DrawLine(left + CellSize * 2, top, left + CellSize * 2, bottom);
        canvas.FillColor = Colors.Red;
        canvas.Alpha = 0.2f;
        canvas.FillRectangle(left, top, right / 2, bottom);
        canvas.Alpha = 1f;

        //Draw character
        canvas.StrokeSize = 10;
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
                        left + lastCommand.XCoordinate * CellSize + 2 * CellSize,
                        top + lastCommand.YCoordinate * CellSize,
                        left + command.XCoordinate * CellSize + 2 * CellSize,
                        top + command.YCoordinate * CellSize);
                    lastCommand = command;
                }
            }
        }

        canvas.StrokeColor = drawColor;
        canvas.StrokeSize = 1;
    }
}