using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ConsistencyCalculator.Desktop.Assets.Converters
{
    public class IntToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not int total)
                return Brushes.Black;

            return total >= 40 && total <= 60
                ? Brushes.Green
                : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotSupportedException();
    }
}
