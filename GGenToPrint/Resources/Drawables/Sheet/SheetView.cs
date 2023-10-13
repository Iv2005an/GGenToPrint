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
            if (bindable is not SheetView { Drawable: SheetDrawable drawable } view)
            {
                return;
            }

            drawable.NumCellsOfVertical = (double)newValue;
            view.Invalidate();
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
            if (bindable is not SheetView { Drawable: SheetDrawable drawable } view)
            {
                return;
            }

            drawable.NumCellsOfHorizontal = (double)newValue;
            view.Invalidate();
        }

        public double NumCellsOfMargin
        {
            get => (double)GetValue(NumCellsOfMarginProperty);
            set => SetValue(NumCellsOfMarginProperty, value);
        }
        public static readonly BindableProperty NumCellsOfMarginProperty = BindableProperty.Create(
            nameof(NumCellsOfMargin), typeof(double), typeof(SheetView),
            propertyChanged: NumCellsOfMarginPropertyChanged);
        public static void NumCellsOfMarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is not SheetView { Drawable: SheetDrawable drawable } view)
            {
                return;
            }

            drawable.NumCellsOfMargin = (double)newValue;
            view.Invalidate();
        }
    }
}

