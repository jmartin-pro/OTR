using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OTR.Core;

namespace OTR.Screen
{
    class CreditsScreen : IScreen
    {
        private Sprite credits;
        public override void initialize()
        {
            credits = new Sprite("creditsEcran");
            credits.position.X = OTRGame.getInstance().getWidth() / 2 - credits.texture.Width / 2;
            credits.position.Y = OTRGame.getInstance().getHeight() / 2 - credits.texture.Height / 2;
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

            credits.render(batch);

            batch.End();
        }
    }
}
