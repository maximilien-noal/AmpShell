namespace AmpShell.WinShell
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class NativeMethods
    {
        private const uint STGM_READ = 0;

        private const int MAX_PATH = 260;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2010:Always consume the value returned by methods marked with PreserveSigAttribute", Justification = "We don't care about it")]
        public static string ResolveShortcut(string filename)
        {
            ShellLink link = new ShellLink();
            ((IPersistFile)link).Load(filename, STGM_READ);
            StringBuilder sb = new StringBuilder(MAX_PATH);
            ((IShellLinkW)link).GetPath(sb, sb.Capacity, out _, 0);
            return sb.ToString();
        }

        [DllImport("shfolder.dll", CharSet = CharSet.Unicode)]
        private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);
    }
}