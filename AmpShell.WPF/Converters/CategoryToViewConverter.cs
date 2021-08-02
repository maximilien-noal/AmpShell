namespace AmpShell.WPF.Converters
{
    using AmpShell.Core.Model;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;

    [ValueConversion(typeof(View), typeof(ViewBase))]
    public class CategoryToViewConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is View view)
            {
                return view switch
                {
                    View.LargeIcon => Application.Current.MainWindow.FindResource("LargeIcon"),
                    View.Details => Application.Current.MainWindow.FindResource("Details"),
                    View.SmallIcon => Application.Current.MainWindow.FindResource("SmallIcon"),
                    View.List => Application.Current.MainWindow.FindResource("List"),
                    View.Tile => Application.Current.MainWindow.FindResource("Tile"),
                    _ => null,
                };
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Category();
        }
    }
}
