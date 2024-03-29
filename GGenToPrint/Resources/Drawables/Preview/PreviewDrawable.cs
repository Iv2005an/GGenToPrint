namespace GGenToPrint.Resources.Drawables.Preview;

public class PreviewDrawable : IDrawable
{
    public string GCode { get; set; }

    public void Draw(ICanvas canvas, RectF rectF)
    {
        // Brush settings
        canvas.StrokeColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
        canvas.StrokeSize = 1;

        // Get draw parameters
        float smallerSide = rectF.Width < rectF.Height ? rectF.Width : rectF.Height;
        float top = rectF.Height / 2 - smallerSide / 2;
        float bottom = top + smallerSide;
        float left = rectF.Left;
        float right = left + smallerSide;
        float cellSize = (right - left) / 4;

        CharacterSheet.DrawSheet(canvas, left, top, right, bottom, smallerSide, cellSize);
        CharacterSheet.DrawCharacter(canvas, left, top, cellSize, GCode);
    }
}