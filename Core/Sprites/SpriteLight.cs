using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OTR.Core
{
    class SpriteLight : Sprite
    {
        private bool show = false;
        private Keys keyInput;

        public SpriteLight(int column)
        {
            texture = OTRGame.getInstance().Content.Load<Texture2D>("light");

            //On change la position des "lumieres" quand on clique sur une touche
            //X -> 4.5 = Taille entre chaque colonnes. 9 = Espace à gauche.
            //Y -> 156 = Distance avec la fin de la barre de la map
            position.X += (int)(texture.Width * column + 4.5 * column + 9 + GameMap.offset);
            position.Y = OTRGame.getInstance().getHeight()-156-texture.Height;

            //On enregistre les différentes touches et on prend celle qui correspond à notre colonne
            Keys[] inputs = new Keys[] {Keys.D, Keys.F, Keys.J, Keys.K};
            keyInput = inputs[column];
        }

        public void render(SpriteBatch batch)
        {
            //Si on clique sur la touche, on affiche l'image
            if (show)
                base.render(batch);
        }

        public void update(float delta)
        {
            //On met a jour l'etat de la touche pour savoir si on doit dessiner ou non l'image
             show = Keyboard.GetState().IsKeyDown(keyInput);
        }
    }
}
