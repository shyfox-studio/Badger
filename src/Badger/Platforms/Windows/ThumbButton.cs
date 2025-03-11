using System.Runtime.InteropServices;

namespace ShyFoxStudio.Badger.Platforms.Windows;

// THUMBBUTTON struct
// C:\Program Files (x86)\Windows Kits\10\Include\[version]\um\ShObjldl_core.h
[StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Auto)]
internal struct ThumbButton
{
    public uint dwMask;
    public uint iId;
    public uint iBitmap;
    public IntPtr hIcon;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
    public string szTip;
    public uint dwFlags;
}
