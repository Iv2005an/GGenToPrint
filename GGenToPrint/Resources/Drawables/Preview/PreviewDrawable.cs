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

        var cellSize = (right - left) / 6;

        // Borders
        canvas.DrawLine(left, top, right, top);
        canvas.DrawLine(right, top, right, bottom);
        canvas.DrawLine(right, bottom, left, bottom);
        canvas.DrawLine(left, bottom, left, top);
        canvas.DrawString(Commands, rectF.Width / 2, rectF.Height / 2, HorizontalAlignment.Center);

        // Cells
        for (byte i = 1; i <= 6; i++)
        {
            canvas.DrawLine(left, top + cellSize * i, right, top + cellSize * i);
            canvas.DrawLine(left + cellSize * i, top, left + cellSize * i, bottom);
        }
    }
}