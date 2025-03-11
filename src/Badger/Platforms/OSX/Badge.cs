#if OSX
using Microsoft.Xna.Framework;

using XnaColor = Microsoft.Xna.Framework.Color;

namespace ShyFoxStudio.Badger;

public partial class Badge
{
    partial void InitializePlatform(GameWindow window) => throw new NotImplementedException();
    partial void SetBadgePlatform() => throw new NotImplementedException();
    partial void ClearBadgePlatform() => throw new NotImplementedException();
}
#endif
