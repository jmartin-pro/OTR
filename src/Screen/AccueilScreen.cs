using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OTR.Core;
using System;

namespace OTR.Screen
{
    class AccueilScreen : IScreen
    {
        private Sprite accueil;
        private Sprite pressEnter;
        private float temps;
        private float tempsMax;
        private bool grossir = true;
        private bool fadeOutRequested = false;

        private float timeFadeOut = 1f/0.5f;

        Random rand = new Random();
        public override void initialize()
        {
            accueil = new Sprite("accueil");
            pressEnter = new Sprite("pressEnter");
            accueil.position.X = OTRGame.getInstance().getWidth() / 2 - accueil.texture.Width / 2;
            accueil.position.Y = OTRGame.getInstance().getHeight() / 2 - accueil.texture.Height / 2;
            accueil.scale = 1.5f;

            pressEnter.position.X = OTRGame.getInstance().getWidth() / 2 - pressEnter.texture.Width / 2;
            pressEnter.position.Y = accueil.position.Y + accueil.texture.Height + 160;
        }

        public override void UnloadContent()
        {

        }

        public override void update(float delta)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                fadeOutRequested = true;

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                OTRGame.getInstance().Exit();

            if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                temps += delta;
                if (temps > tempsMax)
                {
                    tempsMax = rand.Next(300)/1000f;
                    temps = 0;
                    //Passage du true au false en fonction de l'état
                    grossir = !grossir;
                }

                if(grossir)
                {
                    accueil.scale += 0.2f * delta;
                }
                else
                {
                    accueil.scale -= 0.2f * delta;
                }
            }

            if(fadeOutRequested)
            {
                accueil.alpha -= delta * timeFadeOut;
                pressEnter.alpha -= delta * timeFadeOut;
            }

            if(accueil.alpha <= 0)
            {

                OTRGame.getInstance().setScreen(new MenuScreen());
            }


        }

        public override void render(SpriteBatch batch)
        {
            //Couleur de fond
            OTRGame.getInstance().GraphicsDevice.Clear(new Color(0, 0, 0));

            batch.Begin();

            accueil.render(batch);
            pressEnter.render(batch);

            batch.End();
        }
    }
}