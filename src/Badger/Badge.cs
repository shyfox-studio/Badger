using System.Drawing;
using Microsoft.Xna.Framework;

using XnaColor = Microsoft.Xna.Framework.Color;

namespace ShyFoxStudio.Badger;

public partial class Badge : IBadge
{
    private static Badge? _instance;

    /// <summary>
    /// Gets the singleton instance of the Badge
    /// </summary>
    public static Badge Instance => _instance ??= new Badge();

    /// <summary>
    /// Initializes the badge system
    /// </summary>
    public void Initialize(GameWindow window) => InitializePlatform(window);

    /// <summary>
    /// Sets the notification badge with a count
    /// </summary>
    public void SetBadge(int count, XnaColor? backgroundColor = null, XnaColor? textColor = null) => SetBadgePlatform(count, backgroundColor, textColor);

    /// <summary>
    /// Clears the notification badge
    /// </summary>
    public void ClearBadge() => ClearBadgePlatform();

    partial void InitializePlatform(GameWindow window);
    partial void SetBadgePlatform(int count, XnaColor? backgroundColor, XnaColor? textColor);
    partial void ClearBadgePlatform();
}

