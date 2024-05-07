using GGenToPrint.Resources.Models;

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
        propertyChanged: OnNumCellsOfVerticalPropertyChanged);
    public static void OnNumCellsOfVerticalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView) return;
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
        propertyChanged: OnNumCellsOfHorizontalPropertyChanged);
    public static void OnNumCellsOfHorizontalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView) return;
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
        propertyChanged: OnNumCellsOfMarginPropertyChanged);
    public static void OnNumCellsOfMarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView) return;
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
        propertyChanged: OnSheetTypePropertyChanged);
    public static void OnSheetTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView) return;
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
        propertyChanged: OnSheetPositionIndexPropertyChanged);
    public static void OnSheetPositionIndexPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView) return;
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
        propertyChanged: OnTextPropertyChanged);
    public static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView) return;
        sheetDrawable.Text = newValue.ToString().TrimEnd();
        sheetView.Invalidate();
    }

    public IEnumerable<Symbol> Symbols
    {
        get => (IEnumerable<Symbol>)GetValue(SymbolsProperty);
        set => SetValue(SymbolsProperty, value);
    }
    public static readonly BindableProperty SymbolsProperty = BindableProperty.Create(
        nameof(Symbols), typeof(IEnumerable<Symbol>), typeof(SheetView),
        propertyChanged: OnSymbolsPropertyChanged);
    public static void OnSymbolsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView) return;
        sheetDrawable.Symbols = (IEnumerable<Symbol>)newValue;
        sheetView.Invalidate();
    }
}