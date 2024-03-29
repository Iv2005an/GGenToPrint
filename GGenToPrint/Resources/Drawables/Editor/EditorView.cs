namespace GGenToPrint.Resources.Drawables.Editor;

public class EditorView : GraphicsView
{
    public string GCode
    {
        get => (string)GetValue(GCodeProperty);
        set => SetValue(GCodeProperty, value);
    }
    public static readonly BindableProperty GCodeProperty = BindableProperty.Create(
        nameof(GCode), typeof(string), typeof(EditorView),
        propertyChanged: GCodeChanged);
    public static void GCodeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not EditorView { Drawable: EditorDrawable editorDrawable } editorView)
        {
            return;
        }
        editorDrawable.GCode = (string)newValue;
        editorView.Invalidate();
    }
}