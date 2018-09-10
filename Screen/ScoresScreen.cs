using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OTR.Core;

namespace OTR.Screen
{
    class ScoresScreen : IScreen
    {
        SpriteFont font;

        public override void initialize()
        {
            font = OTRGame.getInstance().Content.Load<SpriteFont>("font");
        }

        public override void UnloadContent()
        {

        }

        public override void update(float delta)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                OTRGame.getInstance().setScreen(new MenuScreen());
        }
       
        public override void render(SpriteBatch batch)
        {
            //Couleur de fond
            OTRGame.getInstance().GraphicsDevice.Clear(new Color(0, 0, 0));

            batch.Begin();
            

            batch.End();
        }
    }
}
