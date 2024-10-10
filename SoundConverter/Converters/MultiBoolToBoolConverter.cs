
using System.Globalization;
using System.Windows.Data;

namespace SoundConverter.Converters
{
    public class MultiBoolToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is bool isModelSelected &&
                values[1] is bool isAudioFile &&
                values[2] is bool isButtonEnabled)
            {
                return isModelSelected && isAudioFile && isButtonEnabled;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
