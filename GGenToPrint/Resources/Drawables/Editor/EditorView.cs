namespace GGenToPrint.Resources.Drawables.Editor;

public class EditorView : GraphicsView
{
    public string Commands
    {
        get => (string)GetValue(CommandsProperty);
        set => SetValue(CommandsProperty, value);
    }
    public static readonly BindableProperty CommandsProperty = BindableProperty.Create(
        nameof(Commands), typeof(string), typeof(EditorView),
        propertyChanged: CommandsChanged);
    public static void CommandsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not EditorView { Drawable: EditorDrawable editorDrawable } editorView)
        {
            return;
        }
        editorDrawable.Commands = (string)newValue;
        editorView.Invalidate();
    }
}