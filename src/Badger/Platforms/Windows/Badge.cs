#if WINDOWS
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Globalization;
using System.Runtime.Versioning;
using ShyFoxStudio.Badger.Platforms.Windows;

namespace ShyFoxStudio.Badger;

[SupportedOSPlatform("windows")]
public partial class Badge
{
    private Win32.ITaskbarList3? _taskbarList;
    private IntPtr? _windowHandle;

    partial void InitializePlatform(IntPtr windowHandle)
    {
        _taskbarList = (Win32.ITaskbarList3)new Win32.TaskbarList();
        int hresult = _taskbarList.HrInit();

        if (hresult != 0)
        {
            throw new InvalidOperationException($"Failed to initialize taskbar list: HRESULT: 0x{hresult:X}");
        }

        if (IsSdlAvailable())
        {
            IntPtr sdlWindowHandle = GetWin32WindowHandle(windowHandle);

            if (sdlWindowHandle != IntPtr.Zero)
            {
                _windowHandle = sdlWindowHandle;
            }
            else
            {
                _windowHandle = windowHandle;
            }
        }
        else
        {
            _windowHandle = windowHandle;
        }
    }

    partial void SetBadgePlatform()
    {
        if (_taskbarList == null || _windowHandle == null)
        {
            throw new InvalidOperationException($"Taskbar list is null, platform not initialized");
        }

        IntPtr hIcon = CreateBadgeIcon();

        if (hIcon != IntPtr.Zero)
        {
            string description = s_count.ToString(CultureInfo.InvariantCulture);
            int hresult = _taskbarList.SetOverlayIcon(_windowHandle.Value, hIcon, description);
            if (hresult != 0)
            {
                throw new InvalidOperationException($"SetOverlayIcon failed with HRESULT: 0x{hresult:X}");
            }
        }
        else
        {
            throw new InvalidOperationException("Failed ot create bade icon (hIcon is IntPtr.Zero)");
        }

        if (hIcon != IntPtr.Zero)
        {
            Win32.DestroyIcon(hIcon);
        }
    }

    partial void ClearBadgePlatform()
    {
        if (_taskbarList == null)
        {
            throw new InvalidOperationException($"Taskbar list is null. Platform not initialized");
        }

        int hresult = _taskbarList.SetOverlayIcon(_windowHandle!.Value, IntPtr.Zero, string.Empty);

        if (hresult != 0)
        {
            throw new InvalidOperationException($"Failed to clear taskbar overlay: HRESULT 0x{hresult:X}");
        }
    }

    private static bool IsSdlAvailable()
    {
        try
        {
            IntPtr sdlModule = Kernal32.LoadLibrary("SDL2.dll");
            if (sdlModule == IntPtr.Zero)
            {
                return false;
            }
            Kernal32.FreeLibrary(sdlModule);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static IntPtr GetWin32WindowHandle(IntPtr sdlWindowHandle)
    {
        try
        {
            SDL.SysWMInfo_Windows info = new SDL.SysWMInfo_Windows();
            info.version.major = 2;
            info.version.minor = 0;
            info.version.patch = 0;

            if (!SDL.SDL_GetWindowWMInfo(sdlWindowHandle, ref info))
            {
                return IntPtr.Zero;
            }

            return info.window;
        }
        catch
        {
            return IntPtr.Zero;
        }
    }

    private static IntPtr CreateBadgeIcon()
    {
        string text = s_count > MaxCount ?
                      $"{MaxCount}+" :
                      s_count.ToString(CultureInfo.InvariantCulture);

        int size = 32;

        using (Bitmap bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb))
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                graphics.Clear(Color.Transparent);

                using (SolidBrush brush = new SolidBrush(BackgroundColor))
                {
                    graphics.FillEllipse(brush, 0, 0, size, size);
                }

                float fontSize = size * 0.4f;
                if(text.Length > 1)
                {
                    fontSize = size * 0.25f;
                }

                using (Font font = new Font("Arial", fontSize, FontStyle.Bold))
                {
                    using (StringFormat format = new StringFormat())
                    {
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;

                        using (SolidBrush textBrush = new SolidBrush(TextColor))
                        {
                            graphics.DrawString(text, font, textBrush, new Rectangle(0, 0, size, size), format);
                        }
                    }
                }
            }

            return bitmap.GetHicon();
        }
    }
}
#endif
