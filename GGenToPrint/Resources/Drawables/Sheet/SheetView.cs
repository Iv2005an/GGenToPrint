namespace GGenToPrint.Resources.Drawables.Sheet
{
    public class SheetView : GraphicsView
    {
        public double NumCellsOfVertical
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

        public double NumCellsOfHorizontal
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

        public double NumCellsOfMargin
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

        public int SheetTypeIndex
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

        public int SheetPositionIndex
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
    }
}