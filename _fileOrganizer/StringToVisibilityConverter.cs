using System.Globalization;
using System.Windows;
using System.Windows.Data;
using yyLib;

namespace _fileOrganizer
{
    public class StringToVisibilityConverter: IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string xString && string.IsNullOrWhiteSpace (xString) == false)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) => throw new yyNotImplementedException ("This method should never be called.");
    }
}
