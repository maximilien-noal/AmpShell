namespace AmpShell.WinShell
{
    using AmpShell.Core.Model;
    using System;
    using System.Drawing;
    using System.IO;

    public class FileIconLoader
    {
        public static bool FileExtensionIsExe(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            if (StringExt.IsNullOrWhiteSpace(extension))
            {
                return false;
            }
            return extension.ToUpperInvariant() == ".EXE";
        }

        public static Image GetGameIconAsImage(Game game)
        {
            if (StringExt.IsNullOrWhiteSpace(game.Icon) || File.Exists(game.Icon) == false)
            {
                return Windows.Properties.Resources.Generic_Application1;
            }
            var bitmap = GetImageFromFile(game.Icon);
            if (!(bitmap is null))
            {
                return bitmap;
            }
            try
            {
                return Image.FromFile(game.Icon, true);
            }
            catch
            {
            }
            return Windows.Properties.Resources.Generic_Application1;
        }

        public static Icon GetIconFromGame(Game game)
        {
            string path = StringExt.IsNullOrWhiteSpace(game.Icon) == false && File.Exists(game.Icon) ? game.Icon : FileExtensionIsExe(game.DOSEXEPath) ? game.DOSEXEPath : null;
            if (StringExt.IsNullOrWhiteSpace(path) && File.Exists(path) == false)
            {
                return null;
            }
            try
            {
                return Icon.ExtractAssociatedIcon(path);
            }
            catch
            {
            }
            return null;
        }

        public static Image GetImageFromFile(string filePath)
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
    }
}