using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace ShyFoxStudio.Badger;

public partial class Badge
{
    private static Badge? s_instance;
    private static int s_count;

    /// <summary>
    /// Gets or Sets the current count of the badge.
    /// </summary>
    public static int Count
    {
        get => s_count;
        set => SetBadgeCount(value);
    }

    /// <summary>
    /// Gets or Sets the maximum count value to display on the badge. Default value is 9.
    /// </summary>
    /// <remarks>
    /// If the badge count goes above this number, then the badge count will show this number with a + symbol (e.g. "9+").
    /// </remarks>
    public static int MaxCount { get; set; } = 9;

    /// <summary>
    /// Gets or Sets the color to sue for the background of the badge.
    /// Default value is R:230 G:60 B:0 A:255
    /// </summary>
    public static Color BackgroundColor { get; set; } = Color.FromArgb(255, 230, 60, 0);

    /// <summary>
    /// Gets or Sets the color to use for the text of the badge.
    /// Default value is <see cref="Color.White"/>
    /// </summary>
    public static Color TextColor { get; set; } = Color.White;

    /// <summary>
    /// Initializes the badge system.
    /// </summary>
    /// <param name="windowHandle">The handle of the window this badge is for.</param>
    public static void Initialize(IntPtr windowHandle)
    {
        if (s_instance != null)
        {
            return;
        }

        s_instance = new Badge();
        s_instance.InitializePlatform(windowHandle);
    }

    [MemberNotNull(nameof(s_instance))]
    private static void ThrowIfNotInitialized()
    {
        if (s_instance is null)
        {
            throw new InvalidOperationException("You must initialize the badge system before using");
        }
    }

    /// <summary>
    /// Set the count for the badge.
    /// </summary>
    /// <param name="count">The count to display on the badge.</param>
    public static void SetBadgeCount(int count)
    {
        ThrowIfNotInitialized();
        s_count = Math.Max(0, count);

        if (count <= 0)
        {
            s_instance.ClearBadgePlatform();
        }
        else
        {
            s_instance.SetBadgePlatform();
        }
    }

    /// <summary>
    /// Increments the count for the badge.
    /// </summary>
    public static void IncrementCount() => SetBadgeCount(s_count + 1);

    /// <summary>
    /// Decrements the count for the badge.
    /// </summary>
    public static void DecrementCount() => SetBadgeCount(s_count - 1);

    /// <summary>
    /// Clears the count for the badge.
    /// </summary>
    public static void ClearBadge() => SetBadgeCount(0);


    partial void InitializePlatform(IntPtr windowHandle);
    partial void SetBadgePlatform();
    partial void ClearBadgePlatform();
}

