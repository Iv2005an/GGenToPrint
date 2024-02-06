using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.Drawables.Editor;

public class EditorDrawable : IDrawable
{
    public string Commands { get; set; }

    public float CellSize { get; set; }

    public float Top { get; set; }

    public void Draw(ICanvas canvas, RectF rectF)
    {
        // Get color for paint
        AppTheme appTheme = Application.Current.RequestedTheme;
        Color drawColor = appTheme == AppTheme.Dark ? Colors.White : Colors.Black;
        canvas.StrokeColor = drawColor;
        canvas.FontColor = drawColor;

        // Get draw position
        float smallerSide = rectF.Width < rectF.Height ? rectF.Width : rectF.Height;
        Top = rectF.Height / 2 - smallerSide / 2;
        float bottom = Top + smallerSide;
        float left = rectF.Left;
        float right = left + smallerSide;

        CellSize = (right - left) / 4;

        CharacterSheet.DrawSheet(canvas, left, Top, right, bottom, smallerSide, CellSize);

        //Draw character
        canvas.StrokeSize = CellSize / 10;
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
                        Top + lastCommand.YCoordinate * CellSize,
                        left + command.XCoordinate * CellSize + 2 * CellSize,
                        Top + command.YCoordinate * CellSize);
                    lastCommand = command;
                }
            }
        }

        canvas.StrokeColor = drawColor;
        canvas.StrokeSize = 1;
    }
}