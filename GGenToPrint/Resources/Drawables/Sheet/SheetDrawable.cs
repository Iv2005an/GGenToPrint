﻿using GGenToPrint.Resources.Models;

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
        Color drawColor = Application.Current.RequestedTheme == AppTheme.Dark ? Colors.White : Colors.Black;
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

        // Borders
        canvas.DrawLine(left, top, right, top);
        canvas.DrawLine(right, top, right, bottom);
        canvas.DrawLine(right, bottom, left, bottom);
        canvas.DrawLine(left, bottom, left, top);

        // Cells and rows
        if (SheetTypeIndex == 0)
        {
            for (byte i = 1; i < NumCellsOfHorizontal; i++)
            {
                canvas.DrawLine(left + cellSize * i, top, left + cellSize * i, bottom);
            }
        }
        for (byte i = 1; i < NumCellsOfVertical; i++)
        {
            canvas.DrawLine(left, top + cellSize * i, right, top + cellSize * i);
        }

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
                if (letter is not null && letter.GCode is not null)
                {
                    float maxX = 0;
                    GCommand lastCommand = null;
                    foreach (var gCommand in GCommand.ParseCommands(letter.GCode))
                    {
                        canvas.StrokeColor = Colors.Blue;
                        canvas.StrokeSize = cellSize / 10;
                        var x = gCommand.XCoordinate * cellSize + xOffset;
                        if (x > NumCellsOfHorizontal * cellSize - cellSize / 20)
                        {
                            x = NumCellsOfHorizontal * cellSize - cellSize / 20;
                            canvas.StrokeColor = Colors.Red;
                        }
                        var y = gCommand.YCoordinate * cellSize + yOffset;
                        if (y > NumCellsOfVertical * cellSize - cellSize / 20)
                        {
                            y = NumCellsOfVertical * cellSize - cellSize / 20;
                            canvas.StrokeColor = Colors.Red;
                        }

                        if (x > maxX) maxX = x;

                        if (lastCommand is null || gCommand.GCode == "G0")
                        {
                            lastCommand = gCommand;
                        }
                        else
                        {
                            canvas.DrawLine(
                                left + x,
                                top + y,
                                left + x,
                                top + y);
                            lastCommand = gCommand;
                        }
                    }
                    xOffset = maxX;
                }
            }
        }
    }
}