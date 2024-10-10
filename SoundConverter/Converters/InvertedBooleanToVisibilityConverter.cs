using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace SoundConverter.Converters
{
    public class InvertedBooleanToVisibilityConverter : IValueConverter
    {
        // Преобразование из bool в Visibility с инверсией
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool b)
                flag = b;

            return flag ? Visibility.Collapsed : Visibility.Visible;
        }

        // Преобразование обратно из Visibility в bool с инверсией
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v)
                return v != Visibility.Visible;
            return false;
        }
    }
}
