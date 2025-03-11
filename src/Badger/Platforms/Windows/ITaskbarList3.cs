using System.Drawing;
using System.Runtime.InteropServices;

namespace ShyFoxStudio.Badger.Platforms.Windows;

// ITaskbarList3 interface and GUID
// C:\Program Files (x86)\Windows Kits\10\Include\[version]\um\ShObjldl_core.h
[ComImport]
[Guid("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface ITaskbarList3
{
    // ITaskbarList
    [PreserveSig]
    int HrInit();

    [PreserveSig]
    int AddTab(IntPtr hwnd);

    [PreserveSig]
    int DeleteTab(IntPtr hwnd);

    [PreserveSig]
    int ActiveTab(IntPtr hwnd);

    [PreserveSig]
    int SetActiveAlt(IntPtr hwnd);

    // ITaskbarList2
    [PreserveSig]
    int MarkFullscreenWindow(
        IntPtr hwnd,
        [MarshalAs(UnmanagedType.Bool)] bool fFullscreen
    );

    // ITaskbarList3
    [PreserveSig]
    int SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);

    [PreserveSig]
    int SetProgressState(IntPtr hwnd, int tbpFlags);

    [PreserveSig]
    int RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);

    [PreserveSig]
    int UnregisterTab(IntPtr hwndTab);

    [PreserveSig]
    int SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);

    [PreserveSig]
    int SetTabActive(IntPtr hwndTab, IntPtr hwndMDI, uint dwReserved);

    [PreserveSig]
    int ThumbBarAddButtons(
        IntPtr hwnd,
        uint cButtons,
        [MarshalAs(UnmanagedType.LPArray)] ThumbButton pButton
    );

    [PreserveSig]
    int ThumbBarUpdateButtons(
        IntPtr hwnd,
        uint cButtons,
        [MarshalAs(UnmanagedType.LPArray)] ThumbButton pButton
    );

    [PreserveSig]
    int ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);

    [PreserveSig]
    int SetOverlayIcon(
        IntPtr hwnd,
        IntPtr hIcon,
        [MarshalAs(UnmanagedType.LPWStr)] string pszDescription
    );

    [PreserveSig]
    int SetThumbnailTooltip(
        IntPtr hwnd,
        [MarshalAs(UnmanagedType.LPWStr)] string pszTip
    );

    [PreserveSig]
    int SetThumbnailClip(
        IntPtr hwnd,
        [MarshalAs(UnmanagedType.LPStruct)] Rectangle prcClip
    );
}
