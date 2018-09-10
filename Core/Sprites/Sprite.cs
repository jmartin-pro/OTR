using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OTR
{
    class Sprite
    {
        public Texture2D texture;
        public Vector2 position;
        /*Ratio par lequel on étire nos images. 
         * Ratio 1 = Taille par default. 
         * Ratio 2 = Taille 2 fois plus grande.
         * Ratio 0.5 = Taille 2 fois plus petite
        */
        public float scale = 1.0f;
        public float alpha = 1.0f;

        public Sprite()
        {

        }

        public Sprite(String imageName)
        {
            texture = OTRGame.getInstance().Content.Load<Texture2D>(imageName);
        }

        public void render(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2), null, Color.White * alpha, 0, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 0);
        }
    }
}
