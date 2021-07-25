using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AmpShell.Core.Platform
{
    internal static class PlatformDetector
    {
        public static bool IsNix() => Directory.Exists("/");

        public static bool IsWindows() => Directory.Exists(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System)));
    }
}