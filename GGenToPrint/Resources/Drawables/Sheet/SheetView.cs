namespace GGenToPrint.Resources.Drawables.Sheet
{
    public class SheetView : GraphicsView
    {
        public double NumCellsOfVertical
        {
            get => (double)GetValue(NumCellsOfVerticalProperty);
            set => SetValue(NumCellsOfVerticalProperty, value);
        }
        public static readonly BindableProperty NumCellsOfVerticalProperty = BindableProperty.Create(
            nameof(NumCellsOfVertical), typeof(double), typeof(SheetView),
            propertyChanged: NumCellsOfVerticalPropertyChanged);
        public static void NumCellsOfVerticalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
            {
                return;
            }

            sheetDrawable.NumCellsOfVertical = (double)newValue;
            sheetView.Invalidate();
        }

        public double NumCellsOfHorizontal
        {
            get => (double)GetValue(NumCellsOfHorizontalProperty);
            set => SetValue(NumCellsOfHorizontalProperty, value);
        }
        public static readonly BindableProperty NumCellsOfHorizontalProperty = BindableProperty.Create(
            nameof(NumCellsOfHorizontal), typeof(double), typeof(SheetView),
            propertyChanged: NumCellsOfHorizontalPropertyChanged);
        public static void NumCellsOfHorizontalPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
            {
                return;
            }

            sheetDrawable.NumCellsOfHorizontal = (double)newValue;
            sheetView.Invalidate();
        }

        public double MarginSize
        {
            get => (double)GetValue(MarginSizeProperty);
            set => SetValue(MarginSizeProperty, value);
        }
        public static readonly BindableProperty MarginSizeProperty = BindableProperty.Create(
            nameof(MarginSize), typeof(double), typeof(SheetView),
            propertyChanged: NumCellsOfMarginPropertyChanged);
        public static void NumCellsOfMarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
            {
                return;
            }

            sheetDrawable.MarginSize = (double)newValue;
            sheetView.Invalidate();
        }

        public int SheetType
        {
            get => (int)GetValue(SheetTypeProperty);
            set => SetValue(SheetTypeProperty, value);
        }
        public static readonly BindableProperty SheetTypeProperty = BindableProperty.Create(
            nameof(SheetType), typeof(int), typeof(SheetView),
            propertyChanged: SheetTypePropertyChanged);
        public static void SheetTypePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
            {
                return;
            }

            sheetDrawable.SheetType = (int)newValue;
            sheetView.Invalidate();
        }

        public int SheetPosition
        {
            get => (int)GetValue(SheetPositionProperty);
            set => SetValue(SheetPositionProperty, value);
        }
        public static readonly BindableProperty SheetPositionProperty = BindableProperty.Create(
            nameof(SheetPosition), typeof(int), typeof(SheetView),
            propertyChanged: SheetPositionPropertyChanged);
        public static void SheetPositionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not SheetView { Drawable: SheetDrawable sheetDrawable } sheetView)
            {
                return;
            }

            sheetDrawable.SheetPosition = (int)newValue;
            sheetView.Invalidate();
        }
    }
}