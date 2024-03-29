using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.Drawables.Editor;

public class EditorDrawable : IDrawable
{
    public string GCode { get; set; }

    public float CellSize { get; set; }

    public float Top { get; set; }

    public void Draw(ICanvas canvas, RectF rectF)
    {
        // Get color for paint
        Color drawColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
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
        CharacterSheet.DrawCharacter(canvas, left, Top, CellSize, GCode);

        canvas.StrokeColor = drawColor;
        canvas.StrokeSize = 1;
    }
}