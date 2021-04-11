namespace AmpShell.WinShell
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct ShellExecuteInfo
    {
        public int CbSize;
        public uint FMask;
        public IntPtr Hwnd;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string LpVerb;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string LpFile;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string LpParameters;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string LpDirectory;

        public int NShow;
        public IntPtr HInstApp;
        public IntPtr LpIDList;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string LpClass;

        public IntPtr HkeyClass;
        public uint DwHotKey;
        public IntPtr HIcon;
        public IntPtr HProcess;
    }
}