using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OTR.Core;
using OTR.Core.Sprites;
using System;

namespace OTR.Screen
{
    class MenuScreen : IScreen
    {
        private SpriteMenuItem quitterBouton;
        private SpriteMenuItem scoresBouton;
        private SpriteMenuItem jouerBouton;
        private SpriteMenuItem creditsBouton;
        
        private float timeAlpha = 1f/1.0f;

        private MouseState lastMouseState;
        private KeyboardState lastKeyboardState;

        public override void initialize()
        {
            float width = OTRGame.getInstance().getWidth();
            float height = OTRGame.getInstance().getHeight();

            jouerBouton = new SpriteMenuItem("jouer");
            float heightMenu = jouerBouton.texture.Height * 4 + 20 * 3;
            jouerBouton.position.X = width / 2 - jouerBouton.texture.Width / 2;
            jouerBouton.position.Y = height / 2 - heightMenu / 2;
            jouerBouton.alpha = 0;

            scoresBouton = new SpriteMenuItem("scores");
            scoresBouton.position.X = width / 2 - scoresBouton.texture.Width / 2;
            scoresBouton.position.Y = scoresBouton.texture.Height + jouerBouton.position.Y + 20;
            scoresBouton.alpha = 0;

            creditsBouton = new SpriteMenuItem("credits");
            creditsBouton.position.X = width / 2 - creditsBouton.texture.Width / 2;
            creditsBouton.position.Y = creditsBouton.texture.Height + scoresBouton.position.Y + 20;
            creditsBouton.alpha = 0;

            quitterBouton = new SpriteMenuItem("quitter");
            quitterBouton.position.X = width / 2 - quitterBouton.texture.Width / 2;
            quitterBouton.position.Y = quitterBouton.texture.Height + creditsBouton.position.Y + 20;
            quitterBouton.alpha = 0;

            lastMouseState = Mouse.GetState();
            lastKeyboardState = Keyboard.GetState();
        }

        public override void UnloadContent()
        {

        }


        public override void render(SpriteBatch batch)
        {

            //Couleur de fond
            OTRGame.getInstance().GraphicsDevice.Clear(new Color(0, 0, 0));

            batch.Begin();

            jouerBouton.render(batch);
            scoresBouton.render(batch);
            creditsBouton.render(batch);
            quitterBouton.render(batch);

            batch.End();
        }

        public override void update(float delta)
        {
            if (jouerBouton.contains(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                OTRGame.getInstance().setScreen(new GameScreen());

            if (scoresBouton.contains(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                OTRGame.getInstance().setScreen(new ScoresScreen());

            if (creditsBouton.contains(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                OTRGame.getInstance().setScreen(new CreditsScreen());

            if (quitterBouton.contains(Mouse.GetState().X, Mouse.GetState().Y) && Mouse.GetState().LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                OTRGame.getInstance().Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && lastKeyboardState.IsKeyUp(Keys.Escape))
                OTRGame.getInstance().Exit();

            jouerBouton.alpha += delta * timeAlpha;
            scoresBouton.alpha += delta * timeAlpha;
            creditsBouton.alpha += delta * timeAlpha;
            quitterBouton.alpha += delta * timeAlpha;

            lastMouseState = Mouse.GetState();
        }
    }
}
