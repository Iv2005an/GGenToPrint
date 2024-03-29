namespace GGenToPrint.Resources.Drawables.Preview;

public class PreviewView : GraphicsView
{
    public string GCode
    {
        get => (string)GetValue(GCodeProperty);
        set => SetValue(GCodeProperty, value);
    }
    public static readonly BindableProperty GCodeProperty = BindableProperty.Create(
        nameof(GCode), typeof(string), typeof(PreviewView),
        propertyChanged: GCodeChanged);
    public static void GCodeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not PreviewView { Drawable: PreviewDrawable previewDrawable } previewView)
        {
            return;
        }

        previewDrawable.GCode = (string)newValue;
        previewView.Invalidate();
    }
}