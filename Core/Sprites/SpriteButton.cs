using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OTR.Core
{
    class SpriteButton : Sprite
    {
        //Vitesse de défilement de nos boutons
        public static readonly float baseSpeed = 800.0f;
        public static float speed = baseSpeed;
        public int column;

        public SpriteButton(int column)
        {
            texture = OTRGame.getInstance().Content.Load<Texture2D>("button");
            //X -> 4.5 = Taille entre chaque colonne. 9 = Espace à gauche.
            position.X += (int)(texture.Width * column + 4.5 * column + 9 + GameMap.offset);
            this.column = column;
        }

        public static void resetSpeed()
        {
            speed = baseSpeed;
        }

        public void update(float delta)
        {
            //On descend de "speed" pixels par seconde
            position.Y +=speed * delta;
        }
    }
}
