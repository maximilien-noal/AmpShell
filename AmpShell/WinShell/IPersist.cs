namespace AmpShell.WinShell
{
    using System;
    using System.Runtime.InteropServices;

    [ComImport]
    [Guid("0000010c-0000-0000-c000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IPersist
    {
        [PreserveSig]
        void GetClassID(out Guid pClassID);
    }
}