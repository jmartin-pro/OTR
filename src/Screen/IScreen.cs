using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OTR.Core
{
    public abstract class IScreen
    {
        public abstract void render(SpriteBatch batch);
        public abstract void update(float delta);
        public abstract void UnloadContent();
        public abstract void initialize();
    }
}
