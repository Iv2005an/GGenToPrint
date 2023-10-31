namespace GGenToPrint.Resources.Drawables.Preview;

public class PreviewView : GraphicsView
{
    public string Commands
    {
        get => (string)GetValue(CommandsProperty);
        set => SetValue(CommandsProperty, value);
    }
    public static readonly BindableProperty CommandsProperty = BindableProperty.Create(
        nameof(Commands), typeof(string), typeof(PreviewView),
        propertyChanged: CommandsChanged);
    public static void CommandsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not PreviewView { Drawable: PreviewDrawable previewDrawable } previewView)
        {
            return;
        }

        previewDrawable.Commands = (string)newValue;
        previewView.Invalidate();
    }
}