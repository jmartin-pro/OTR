using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OTR.Core.Sprites
{
    class SpriteMenuItem : Sprite
    {
        public SpriteMenuItem(string imageName): base(imageName)
        { 
        }

        public bool contains(int x, int y)
        {
            if (x < position.X
                || x > position.X + texture.Width
                || y < position.Y
                || y > position.Y + texture.Height)
                return false;
            return true;
        }
    }
}
