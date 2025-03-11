using System.Runtime.InteropServices;

namespace ShyFoxStudio.Badger.Platforms.Windows;

internal static class SDL
{
    internal struct Version
    {
        public byte major;
        public byte minor;
        public byte patch;
    }

    internal struct SysWMInfo_Windows
    {
        public Version version;
        public int subsystem;
        public IntPtr window;
        public IntPtr hdc;
        public IntPtr hinstance;
    }

    [DllImport("SDL2.dll", CallingConvention = CallingConvention.Cdecl)]
    internal static extern bool SDL_GetWindowWMInfo(IntPtr window, ref SysWMInfo_Windows info);
}
