namespace AmpShell.WPF.Converters
{
    using AmpShell.Core.Model;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class GameToCustomConfigDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Game game)
            {
                return game.GetCustomConfigDescription();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Game();
        }
    }
}
