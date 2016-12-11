﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace FundManager.ViewModel.ValueConverters
{
    [ValueConversion(typeof(Enum), typeof(bool))]
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return false;
            string enumValue = value.ToString();
            string targetValue = parameter.ToString();
            bool outputValue = enumValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
            return outputValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? Enum.Parse(targetType, parameter.ToString(), true) : Binding.DoNothing;
        }
    }
}
