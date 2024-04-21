namespace GGenToPrint.Resources.Drawables.SymbolSheet;

public class SymbolSheetView : GraphicsView
{
    public string SymbolGCode
    {
        get => (string)GetValue(SymbolGCodeProperty);
        set => SetValue(SymbolGCodeProperty, value);
    }
    public static readonly BindableProperty SymbolGCodeProperty = BindableProperty.Create(
        nameof(SymbolGCode), typeof(string), typeof(SymbolSheetView),
        propertyChanged: SymbolGCodeChanged);
    public static void SymbolGCodeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not SymbolSheetView { Drawable: SymbolSheetDrawable symbolSheetDrawable } symbolSheetView)
        {
            return;
        }
        symbolSheetDrawable.SymbolGCode = (string)newValue;
        symbolSheetView.Invalidate();
    }
}