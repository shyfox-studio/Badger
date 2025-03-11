using System.Runtime.InteropServices;

namespace ShyFoxStudio.Badger.Platforms.Windows;

public static class Win32
{
    [DllImport("user32.dll")]
    internal static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    internal static extern IntPtr DestroyIcon(IntPtr hIcon);
}
