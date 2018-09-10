using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OTR.Core;
using OTR.Screen;
using System;

namespace OTR
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class OTRGame : Game
    {
        //Le singleton empeche l'instanciation d'une classe (new MaClasse() ).
        //On utilise getInstance() pour récupérer la SEULE instance de la classe qui existe
        // ===== DEBUT SINGLETON =====
        private static OTRGame instance;

        public static OTRGame getInstance()
        {
            if(instance == null)
            {
                instance = new OTRGame();
            }
            return instance;
        }
        // ===== FIN SINGLETON =====

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private IScreen screen;

        private OTRGame()
        {
            graphics = new GraphicsDeviceManager(this);
            //Changement de la taille de la fenetre en fonction de celle de l'ecran et plein ecran
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

            this.IsMouseVisible = true;
            graphics.ToggleFullScreen();

            Content.RootDirectory = "Content";
        }
      
        protected override void Initialize()
        {
            base.Initialize();

            setScreen(new AccueilScreen());
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void UnloadContent()
        {
            screen.UnloadContent();
        }

        public void setScreen(IScreen screen)
        {
            if(this.screen != null)
                this.screen.UnloadContent();
            this.screen = screen;
            screen.initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            //Delta = temps depuis dernier update en secondes.
            float delta = gameTime.ElapsedGameTime.Milliseconds/1000.0f;

            screen.update(delta);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            screen.render(spriteBatch);
            base.Draw(gameTime);
        }

        public int getWidth()
        {
            return Window.ClientBounds.Width;
        }

        public int getHeight()
        {
            return Window.ClientBounds.Height;
        }
    }
}
