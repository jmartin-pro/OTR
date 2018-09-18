using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System;

using OTR.Core;

namespace OTR.Screen
{
    class GameScreen : IScreen
    {
        private Song music;
        private GameMap gameMap;
        public override void initialize()
        {
            gameMap = new GameMap();
            music = OTRGame.getInstance().Content.Load<Song>("Tobu - Higher");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(music);
            SpriteButton.resetSpeed();
        }

        public override void UnloadContent()
        {
            MediaPlayer.Stop();
        }

        public override void update(float delta)
        {
            gameMap.update(delta);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                OTRGame.getInstance().setScreen(new MenuScreen());
            }
        }
       
        public override void render(SpriteBatch batch)
        {
            //Couleur de fond
            OTRGame.getInstance().GraphicsDevice.Clear(new Color(0, 0, 0));

            batch.Begin();

            gameMap.render(batch);

            batch.End();
        }
    }
}
