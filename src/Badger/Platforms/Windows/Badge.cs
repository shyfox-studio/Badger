#if WINDOWS
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.Versioning;
using Microsoft.Xna.Framework;
using ShyFoxStudio.Badger.Platforms.Windows;

using XnaColor = Microsoft.Xna.Framework.Color;
using SystemColor = System.Drawing.Color;

namespace ShyFoxStudio.Badger;

public partial class Badge
{

    private ITaskbarList3? _taskbarList;
    private GameWindow? _window;
    private IntPtr windowHandle;

    partial void InitializePlatform(GameWindow window)
    {
        _taskbarList = (ITaskbarList3)new TaskbarList();
        int hresult = _taskbarList.HrInit();

        if (hresult != 0)
        {
            throw new InvalidOperationException($"Failed to initialize taskbar list: HRESULT: 0x{hresult:X}");
        }

        _window = window;
    }

    [SupportedOSPlatform("windows")]
    partial void SetBadgePlatform(int count, XnaColor? backgroundColor, XnaColor? textColor)
    {
        if (_taskbarList == null || _window == null)
        {
            throw new InvalidOperationException($"Tasbar list is null, platform not initialized");
        }


        // Note:
        // For some reason, setting the window handle in InitializePlatform above work fine for MonoGame WindowsDX,
        // but for MonoGame DesktopGL, whatever window handle is given from Window.Handle is not the correct one (?)
        // or doesn't work.  By forcing to get active window here, it ensures that this works on both DX and GL
        // platforms on Windows.
        //
        // Need to investigate if there is a better way than forcing to get active window
        IntPtr windowHandle = Win32.GetActiveWindow();

        if (windowHandle == IntPtr.Zero)
        {
            return;
        }

        if (count > 0)
        {
            // Default colors
            SystemColor bgColor = backgroundColor.HasValue ?
                                  SystemColor.FromArgb(backgroundColor.Value.A, backgroundColor.Value.R, backgroundColor.Value.G, backgroundColor.Value.B) :
                                  SystemColor.Red;
            SystemColor txtColor = textColor.HasValue ?
                                   SystemColor.FromArgb(textColor.Value.A, textColor.Value.R, textColor.Value.G, textColor.Value.B) :
                                   SystemColor.White;


            IntPtr hIcon = CreateBadgeIcon(count, bgColor, txtColor, 32);

            if (hIcon != IntPtr.Zero)
            {
                string description = count.ToString(CultureInfo.InvariantCulture);
                int hresult = _taskbarList.SetOverlayIcon(windowHandle, hIcon, description);
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
        else
        {
            ClearBadgePlatform();
        }
    }

    partial void ClearBadgePlatform()
    {
        if (_taskbarList == null)
        {
            throw new InvalidOperationException($"Taskbar list is null. Platform not initialized");
        }

        // Note:
        // For some reason, setting the window handle in InitializePlatform above work fine for MonoGame WindowsDX,
        // but for MonoGame DesktopGL, whatever window handle is given from Window.Handle is not the correct one (?)
        // or doesn't work.  By forcing to get active window here, it ensures that this works on both DX and GL
        // platforms on Windows.
        //
        // Need to investigate if there is a better way than forcing to get active window
        IntPtr windowHandle = Win32.GetActiveWindow();

        int hresult = _taskbarList.SetOverlayIcon(windowHandle, IntPtr.Zero, string.Empty);

        if (hresult != 0)
        {
            throw new InvalidOperationException($"Failed to clear taskbar overlay: HRESULT 0x{hresult:X}");
        }
    }

    [SupportedOSPlatform("windows")]
    private static IntPtr CreateBadgeIcon(int count, SystemColor backgroundColor, SystemColor textColor, int size = 32)
    {
        string text = count > 99 ? "99+" : (count > 9 ? "9+" : count.ToString(CultureInfo.InvariantCulture));

        // Create a bitmap with 32-bit ARGB format for better transparency
        using Bitmap bitmap = new Bitmap(size, size, PixelFormat.Format32bppArgb);
        using Graphics graphics = Graphics.FromImage(bitmap);

        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
        graphics.Clear(SystemColor.Transparent);

        using SolidBrush brush = new SolidBrush(backgroundColor);
        graphics.FillEllipse(brush, 0, 0, size, size);

        float fontSize = size * 0.5f;
        if (text.Length > 1) fontSize *= 0.8f;

        using Font font = new Font("Arial", fontSize, FontStyle.Bold);
        using StringFormat format = new StringFormat();
        format.Alignment = StringAlignment.Center;
        format.LineAlignment = StringAlignment.Center;

        using SolidBrush textBrush = new SolidBrush(textColor);

        graphics.DrawString(text, font, textBrush, new RectangleF(0, 0, size, size), format);

        return bitmap.GetHicon();
    }

}
#endif
