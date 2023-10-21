namespace GGenToPrint.Resources.Controls;

public class SliderOption : ContentView
{
    public static readonly BindableProperty OptionNameProperty = BindableProperty.Create(nameof(OptionName), typeof(string), typeof(SliderOption));
    public string OptionName
    {

        get => (string)GetValue(OptionNameProperty);
        set => SetValue(OptionNameProperty, value);
    }

    public static readonly BindableProperty SliderMinimumProperty = BindableProperty.Create(nameof(SliderMinimum), typeof(byte), typeof(SliderOption));
    public byte SliderMinimum
    {

        get => (byte)GetValue(SliderMinimumProperty);
        set => SetValue(SliderMinimumProperty, value);
    }
    public static readonly BindableProperty SliderMaximumProperty = BindableProperty.Create(nameof(SliderMaximum), typeof(byte), typeof(SliderOption));
    public byte SliderMaximum
    {

        get => (byte)GetValue(SliderMaximumProperty);
        set => SetValue(SliderMaximumProperty, value);
    }
    public static readonly BindableProperty SliderValueProperty = BindableProperty.Create(nameof(SliderValue), typeof(byte), typeof(SliderOption), defaultBindingMode: BindingMode.TwoWay);
    public byte SliderValue
    {

        get => (byte)GetValue(SliderValueProperty);
        set => SetValue(SliderValueProperty, value);
    }
}