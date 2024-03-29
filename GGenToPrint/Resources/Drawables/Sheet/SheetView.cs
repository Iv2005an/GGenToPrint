using GGenToPrint.Resources.Services;

namespace GGenToPrint.Resources.Drawables.Sheet;

public class SheetView : GraphicsView
{
    public byte NumCellsOfVertical
    {
        get => (byte)GetValue(NumCellsOfVerticalProperty);
        set => SetValue(NumCellsOfVerticalProperty, value);
    }
    public static readonly BindableProperty NumCellsOfVerticalProperty = BindableProperty.Create(
        nameof(NumCellsOfVertical), typeof(byte), typeof(SheetView),
        propertyChanged: NumCellsOfVerticalPropertyChanged);
    public static void NumCellsOfVerticalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
        {
            return;
        }

        sheetDrawable.NumCellsOfVertical = (byte)newValue;
        sheetView.Invalidate();
    }

    public byte NumCellsOfHorizontal
    {
        get => (byte)GetValue(NumCellsOfHorizontalProperty);
        set => SetValue(NumCellsOfHorizontalProperty, value);
    }
    public static readonly BindableProperty NumCellsOfHorizontalProperty = BindableProperty.Create(
        nameof(NumCellsOfHorizontal), typeof(byte), typeof(SheetView),
        propertyChanged: NumCellsOfHorizontalPropertyChanged);
    public static void NumCellsOfHorizontalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
        {
            return;
        }

        sheetDrawable.NumCellsOfHorizontal = (byte)newValue;
        sheetView.Invalidate();
    }

    public byte NumCellsOfMargin
    {
        get => (byte)GetValue(NumCellsOfMarginProperty);
        set => SetValue(NumCellsOfMarginProperty, value);
    }
    public static readonly BindableProperty NumCellsOfMarginProperty = BindableProperty.Create(
        nameof(NumCellsOfMargin), typeof(byte), typeof(SheetView),
        propertyChanged: NumCellsOfMarginPropertyChanged);
    public static void NumCellsOfMarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
        {
            return;
        }

        sheetDrawable.NumCellsOfMargin = (byte)newValue;
        sheetView.Invalidate();
    }

    public byte SheetTypeIndex
    {
        get => (byte)GetValue(SheetTypeIndexProperty);
        set => SetValue(SheetTypeIndexProperty, value);
    }
    public static readonly BindableProperty SheetTypeIndexProperty = BindableProperty.Create(
        nameof(SheetTypeIndex), typeof(byte), typeof(SheetView),
        propertyChanged: SheetTypePropertyChanged);
    public static void SheetTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
        {
            return;
        }

        sheetDrawable.SheetTypeIndex = (byte)newValue;
        sheetView.Invalidate();
    }

    public byte SheetPositionIndex
    {
        get => (byte)GetValue(SheetPositionIndexProperty);
        set => SetValue(SheetPositionIndexProperty, value);
    }
    public static readonly BindableProperty SheetPositionIndexProperty = BindableProperty.Create(
        nameof(SheetPositionIndex), typeof(byte), typeof(SheetView),
        propertyChanged: SheetPositionIndexPropertyChanged);
    public static void SheetPositionIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
        {
            return;
        }

        sheetDrawable.SheetPositionIndex = (byte)newValue;
        sheetView.Invalidate();
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(TextProperty), typeof(string), typeof(SheetView),
        propertyChanged: TextPropertyChanged);
    public static async void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
        {
            return;
        }

        sheetDrawable.Text = (string)newValue;
        sheetDrawable.Letters = await (await LetterDatabase.GetInstance()).GetLetters((await (await FontDatabase.GetInstance()).GetCurrentFont()).FontId);
        sheetView.Invalidate();
    }
}