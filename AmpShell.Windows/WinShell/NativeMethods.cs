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

        private const uint SEE_MASK_INVOKEIDLIST = 12;

        private const uint STGM_READ = 0;

        private const int SW_SHOW = 5;

        public static string ResolveShortcut(string filename)
        {
            ShellLink link = new ShellLink();
            ((IPersistFile)link).Load(filename, STGM_READ);
            StringBuilder sb = new StringBuilder(MAX_PATH);
            ((IShellLinkW)link).GetPath(sb, sb.Capacity, out _, 0);
            return sb.ToString();
        }

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

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);
    }
}