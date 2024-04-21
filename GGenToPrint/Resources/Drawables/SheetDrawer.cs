using GGenToPrint.Resources.Models;

namespace GGenToPrint.Resources.Drawables;

public static class SheetDrawer
{
    public static Color BrushColor => Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;

    public static void DrawLayout(
        ICanvas canvas,
        float left, float top, float right, float bottom,
        float cellSize,
        byte numCellsOfHorizontal, byte numCellsOfVertical,
        bool linearType = false)
    {
        PathF layout = new();

        layout.MoveTo(left, top);
        layout.LineTo(right, top);
        layout.LineTo(right, bottom);
        layout.LineTo(left, bottom);
        layout.Close();

        for (byte i = 1; i < numCellsOfVertical; i++)
        {
            layout.MoveTo(left, top + cellSize * i);
            layout.LineTo(right, top + cellSize * i);
        }
        if (!linearType)
        {
            for (byte i = 1; i < numCellsOfHorizontal; i++)
            {
                layout.MoveTo(left + cellSize * i, top);
                layout.LineTo(left + cellSize * i, bottom);
            }
        }
        canvas.StrokeColor = BrushColor;
        canvas.Alpha = 0.5f;
        canvas.StrokeSize = 1;
        canvas.DrawPath(layout);
        canvas.Alpha = 1f;

        float strokeSize = 2;
        canvas.StrokeSize = strokeSize;
        canvas.StrokeColor = Colors.Green;
        canvas.DrawLine(left, top + strokeSize / 2, right, top + strokeSize / 2);
        canvas.StrokeColor = Colors.Red;
        canvas.DrawLine(left + strokeSize / 2, top, left + strokeSize / 2, bottom);
    }

    public static void DrawEditSymbolBackground(
        ICanvas canvas,
        float left, float top, float right, float bottom)
    {
        float width = right - left;
        float height = bottom - top;

        PathF crosshair = new();
        crosshair.MoveTo(left, top + height / 2);
        crosshair.LineTo(right, top + height / 2);
        crosshair.MoveTo(left + width / 2, top);
        crosshair.LineTo(left + width / 2, bottom);

        canvas.StrokeSize = 5;
        canvas.StrokeColor = Colors.LightBlue;
        canvas.DrawPath(crosshair);

        canvas.FillColor = Colors.Red;
        canvas.Alpha = 0.2f;
        canvas.FillRectangle(left, top, width / 2, height);
        canvas.Alpha = 1f;
    }
}