using System.Globalization;
using System.Windows.Data;
using yyLib;

namespace _fileOrganizer
{
    public class SelectedItemToIsEnabledConverter: IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture) => value != null;

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) => throw new yyNotImplementedException ("This method should never be called.");
    }
}
