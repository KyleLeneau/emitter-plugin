using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Bortle.NINA.Emitter.UI.GlobalOptions {

    /// <summary>
    /// Shows an element only when the bound string is non-null/non-empty. Used to show a sink's
    /// save-error message only while one is set.
    /// </summary>
    public class NullOrEmptyStringToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return string.IsNullOrEmpty(value as string) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }
    }
}