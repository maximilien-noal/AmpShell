namespace AmpShell.WinShell
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct WIN32_FIND_DATAW
    {
        public uint DwFileAttributes;

        public long FtCreationTime;

        public long FtLastAccessTime;

        public long FtLastWriteTime;

        public uint NFileSizeHigh;

        public uint NFileSizeLow;

        public uint DwReserved0;

        public uint DwReserved1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string CFileName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
        public string CAlternateFileName;
    }
}