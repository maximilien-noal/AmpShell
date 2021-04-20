namespace AmpShell.WinShell
{
    using System;
    using System.Drawing;
    using System.IO;

    using AmpShell.Core.AutoConfig;
    using AmpShell.Core.Model;

    public class FileIconLoader
    {
        public static Image GetIconFromGame(Game game)
        {
            if (StringExt.IsNullOrWhiteSpace(game.Icon) || File.Exists(game.Icon) == false)
            {
                return Windows.Properties.Resources.Generic_Application1;
            }
            var iconRealLocation = game.Icon.Replace("AppPath", PathFinder.GetStartupPath());
            var bitmap = GetIconFromFile(iconRealLocation);
            if (!(bitmap is null))
            {
                return bitmap;
            }
            try
            {
                return Image.FromFile(iconRealLocation, true);
            }
            catch
            {
            }
            return Windows.Properties.Resources.Generic_Application1;
        }

        public static Image GetIconFromFile(string filePath)
        {
            if (StringExt.IsNullOrWhiteSpace(filePath) || File.Exists(filePath) == false)
            {
                return null;
            }
            if (FileExtensionIsExe(filePath))
            {
                try
                {
                    return Icon.ExtractAssociatedIcon(filePath)?.ToBitmap();
                }
                catch
                {
                }
            }
            try
            {
                return Image.FromFile(filePath);
            }
            catch
            {
            }
            return null;
        }

        public static bool FileExtensionIsExe(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            if (StringExt.IsNullOrWhiteSpace(extension))
            {
                return false;
            }
            return extension.ToUpperInvariant() == ".EXE";
        }
    }
}