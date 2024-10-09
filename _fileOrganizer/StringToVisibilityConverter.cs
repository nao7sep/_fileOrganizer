﻿using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace _fileOrganizer
{
    [ValueConversion (typeof (string), typeof (Visibility))]
    public class StringToVisibilityConverter: IValueConverter
    {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string xString && string.IsNullOrWhiteSpace (xString) == false)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException ();
    }
}
