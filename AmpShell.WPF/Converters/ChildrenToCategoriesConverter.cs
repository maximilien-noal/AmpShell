namespace AmpShell.WPF.Converters
{
    using AmpShell.Core.Model;

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    [ValueConversion(typeof(List<object>), typeof(IEnumerable<Category>))]
    internal class ChildrenToCategoriesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is List<object> children)
            {
                return children.Cast<Category>();
            }
            return new List<Category>();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is IEnumerable<Category> categories)
            {
                return categories.Cast<object>();
            }
            return new List<object>();
        }
    }
}
