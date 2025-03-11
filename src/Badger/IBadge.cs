using System.Drawing;
using Microsoft.Xna.Framework;

using XnaColor = Microsoft.Xna.Framework.Color;

namespace ShyFoxStudio.Badger;

public interface IBadge
{
    /// <summary>
    /// Initializes the taskbar badge system.
    /// </summary>
    /// <param name="window"></param>
    void Initialize(GameWindow window);

    /// <summary>
    /// Sets the notification badge with a count.
    /// </summary>
    /// <param name="count">The notification count to show.  Use 0 to remove.</param>
    /// <param name="backgroundColor">The background color for the badge.</param>
    /// <param name="textColor">The text color for the badge.</param>
    void SetBadge(int count, XnaColor? backgroundColor = null, XnaColor? textColor = null);

    /// <summary>
    /// Clears the notification badge
    /// </summary>
    void ClearBadge();
}
