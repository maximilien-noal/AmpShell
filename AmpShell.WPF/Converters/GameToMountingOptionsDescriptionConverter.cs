namespace AmpShell.WPF.Converters
{
    using AmpShell.Core.Model;
    using System;
    using System.Globalization;
    using System.Windows.Data;

    [ValueConversion(typeof(Game), typeof(string))]
    public class GameToMountingOptionsDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is Game game)
            {
                return game.GetMountingOptionsDescription();
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Game();
        }
    }
}
