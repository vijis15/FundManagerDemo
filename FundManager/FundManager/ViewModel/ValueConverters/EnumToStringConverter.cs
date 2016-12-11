using System;
using System.Globalization;
using System.Windows.Data;

namespace FundManager.ViewModel.ValueConverters
{
    public class EnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Not required as we are not setting the radio button depending on some string property
            throw new NotSupportedException();
        }
    }
}
