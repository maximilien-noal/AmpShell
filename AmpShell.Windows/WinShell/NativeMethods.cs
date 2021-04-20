[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AmpShell.WinForms")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("AmpShell.WPF")]

namespace AmpShell.WinShell
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class NativeMethods
    {
        public const int MAX_PATH = 260;
        private const uint STGM_READ = 0;
        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;

        public static bool ShowFileProperties(string Filename)
        {
            ShellExecuteInfo info = default;
            info.CbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
            info.LpVerb = "properties";
            info.LpFile = Filename;
            info.NShow = SW_SHOW;
            info.FMask = SEE_MASK_INVOKEIDLIST;
            return ShellExecuteEx(ref info);
        }

        public static string ResolveShortcut(string filename)
        {
            ShellLink link = new ShellLink();
            ((IPersistFile)link).Load(filename, STGM_READ);
            StringBuilder sb = new StringBuilder(MAX_PATH);
            ((IShellLinkW)link).GetPath(sb, sb.Capacity, out _, 0);
            return sb.ToString();
        }

        [DllImport("kernel32.dll")]
        internal static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        internal static extern bool AttachConsole(int pid);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool FreeConsole();

        [DllImport("shfolder.dll", CharSet = CharSet.Unicode)]
        private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, int dwFlags, StringBuilder lpszPath);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);
    }
}