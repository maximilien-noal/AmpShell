namespace AmpShell.WPF.Converters
{
    using AmpShell.Core.Model;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    [ValueConversion(typeof(string), typeof(BitmapSource))]
    public class GameToBitmapSourceConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Game game)
            {
                var icon = WinShell.FileIconLoader.GetIconFromGame(game);
                if (icon is null)
                {
                    var source = new BitmapImage(new Uri("pack://application:,,,/Resources/Generic_Application.png"));
                    return source;
                }
                ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                    icon.Handle,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

                return imageSource;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Game();
        }
    }
}