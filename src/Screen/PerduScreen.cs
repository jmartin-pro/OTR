using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OTR.Core;
using System;
using System.Data.SqlClient;

namespace OTR.Screen
{
    class PerduScreen : IScreen
    {
        private Sprite loose;
        private Sprite tryAgain;
        private float temps;
        private float tempsMax;
        private bool grossir = true;
        private long score = 0;
        private String playerName = "";
        private int playerNameLengthMax = 25;
        private SpriteFont font;
        private KeyboardState lastKeyboardState;

        Random rand = new Random();

        public PerduScreen(long score)
        {
            this.score = score;
        }

        public override void initialize()
        {
            loose = new Sprite("perdu");
            tryAgain = new Sprite("tryAgain");
            loose.position.X = OTRGame.getInstance().getWidth() / 2 - loose.texture.Width / 2;
            loose.position.Y = OTRGame.getInstance().getHeight() / 2 - loose.texture.Height / 2;
            loose.scale = 1.5f;

            tryAgain.position.X = OTRGame.getInstance().getWidth() / 2 - tryAgain.texture.Width / 2;
            tryAgain.position.Y = loose.position.Y + tryAgain.texture.Height + 160;

            font = OTRGame.getInstance().Content.Load<SpriteFont>("font");

            lastKeyboardState = Keyboard.GetState();
        }

        public override void UnloadContent()
        {
            //TODO connexion à la base de données et envoie du score
            /*SqlConnection connexion = new SqlConnection("Data Source=(LocalDB)/MSSQLLocalDB; Integrated Security=True");
            connexion.Open();

            SqlTransaction transaction = connexion.BeginTransaction();
            SqlCommand cmd = new SqlCommand("INSERT INTO scores VALUES(NULL, "+playerName+", " + score + ")", connexion, transaction);
            transaction.Commit();*/
        }

        public override void update(float delta)
        {

            temps += delta;
            if (temps > tempsMax)
            {
                tempsMax = rand.Next(300) / 1000f;
                temps = 0;
                //Passage du true au false en fonction de l'état
                grossir = !grossir;
            }

            if (grossir)
            {
                loose.scale += 0.2f * delta;
            }
            else
            {
                loose.scale -= 0.2f * delta;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                OTRGame.getInstance().setScreen(new GameScreen());

            else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                OTRGame.getInstance().setScreen(new MenuScreen());

            else if (Keyboard.GetState().IsKeyDown(Keys.Back) && lastKeyboardState.IsKeyUp(Keys.Back))
            {
                if (playerName.Length > 0)
                    playerName = playerName.Substring(0, playerName.Length - 1);
            }

            for (int i = 0; i < Keyboard.GetState().GetPressedKeys().Length; i++)
            {
                Keys key = Keyboard.GetState().GetPressedKeys()[i];
                if (lastKeyboardState.IsKeyUp(key) && !isControlKey(key))
                {
                    if(playerName.Length < playerNameLengthMax)
                        playerName += key.ToString();
                }
            }


            lastKeyboardState = Keyboard.GetState();
        }

        public override void render(SpriteBatch batch)
        {
            //Couleur de fond
            OTRGame.getInstance().GraphicsDevice.Clear(new Color(0, 0, 0));

            batch.Begin();

            loose.render(batch);
            tryAgain.render(batch);
            Vector2 playerNameSize = font.MeasureString(playerName);
            batch.DrawString(font, playerName, new Vector2(OTRGame.getInstance().getWidth() / 2 - playerNameSize.X / 2, tryAgain.position.Y + tryAgain.texture.Height + 50), Color.White);

            batch.End();
        }

        private bool isControlKey(Keys key)
        {
            //Si la touche appuyé est avant le 65 (A) ou après 90 (Z) on ne gère pas la touche car c'est une touche de "controle" (ctrl, lshift, ...)
            return ((int)key < 65 || (int)key > 90);
        }
    }
}