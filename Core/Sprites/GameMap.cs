using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using OTR.Screen;

namespace OTR.Core
{
    class GameMap : Sprite
    {
        //Décalage en X 
        public static float offset = OTRGame.getInstance().getWidth() / 2;

        //Temps accumulé depuis le dernier spawn d'un bouton
        protected float lastSpawnTime = 0.0f;
        //Temps avant le prochain spawn
        protected float spawnTime = 0.25f;

        //Temps accumulé depuis le dernier changement des temps de spawn
        protected float lastChange = 0.0f;
        //Temps avant le prochain changement des temps de spawn
        protected float changeTime = 0.0f;

        //Le score actuel du niveau
        protected long score = 0;
        protected int fails = 0;
        protected int maxFails = 5;

        //Liste des boutons
        protected List<SpriteButton> buttons = new List<SpriteButton>();
        //Liste des 4 effets quand on appuie sur une touche
        protected List<SpriteLight> lightSprites = new List<SpriteLight>();
        protected Random rand = new Random();

        //Permet de savoir l'etat du clavier à l'update précédente. Utile pour savoir si on vient d'appuyer
        protected KeyboardState lastState;

        protected SpriteFont font;

        public GameMap() : base("map")
        {
            //On récupère l'image de fond pour la map
            OTRGame game = OTRGame.getInstance();

            offset = OTRGame.getInstance().getWidth() / 2 - texture.Width / 2;

            //On la place tout en bas de l'écran
            position.Y = (game.getHeight() - (texture.Height));
            position.X = offset;

            font = OTRGame.getInstance().Content.Load<SpriteFont>("font");

            //On crée les 4 effets en leur donnant une colonne cible
            for (int i = 0; i < 4; i++)
                lightSprites.Add(new SpriteLight(i));
        }

        public void update(float delta)
        {
            //On ajoute delta aux chronos
            lastSpawnTime += delta;
            lastChange += delta;
            float timeRatio = (SpriteButton.speed / SpriteButton.baseSpeed);
            if (lastSpawnTime * timeRatio > spawnTime)
            {
                //On doit ajouter un bouton
                addButton();
                lastSpawnTime = 0;
            }

            if(lastChange * timeRatio > changeTime)
            {
                //On doit changer le temps entre chaque spawn de bouton
                //On sélectionne un nombre au hasard entre 0.30 et 0.33 secondes
                spawnTime = Math.Max((float)rand.Next(200)/1000.0f, 0.33f);
                //On change la durée de la phase avant le prochain changement 
                changeTime = (float)rand.Next(10000)/1000.0f;
                lastChange = 0;
            }

            //On regarde si les touches sont appuyées quand le bouton est proche de la fin
            Keys[] inputs = new Keys[] { Keys.D, Keys.F, Keys.J, Keys.K };
            //Les boutons les plus proches sont ceux qui sont les premiers dans la liste
            //On vérifie les 4 premier boutons au cas où on en aurait 4 en même temps
            List<SpriteButton> premiersBoutons = new List<SpriteButton>();
            float lastY = 0;
            for (int i = 0; i < Math.Min(4, buttons.Count); i++)
            {
                if (i == 0)
                {
                    lastY = buttons[i].position.Y;
                    premiersBoutons.Add(buttons[i]);
                }

                if (buttons[i].position.Y == lastY)
                {
                    premiersBoutons.Add(buttons[i]);
                }
            }

            for (int j = 0; j < inputs.Count(); j++)
            {
                //Si la touche est appuyée à l'instant on regarde
                if (Keyboard.GetState().IsKeyDown(inputs[j]) && !lastState.IsKeyDown(inputs[j]))
                {
                    for (int i = 0; i < premiersBoutons.Count(); i++)
                    {
                        if (premiersBoutons[i].column == j)
                        {
                            float distance = OTRGame.getInstance().getHeight() - (premiersBoutons[i].position.Y + premiersBoutons[i].texture.Height + 156);
                            //On a appuyé sur la touche correspondante au bouton, on vérifie si il est proche (- de 150px)
                            if (distance < 150)
                            {
                                //Il est proche. On calcule le score que l'on obtient.
                                int scoreTouche = Math.Abs(50 * ((int)(distance % 3)+1));
                                score += scoreTouche;

                                fails = (fails <= 0) ? 0 : fails-1;

                                buttons.Remove(premiersBoutons[i]);
                                break;
                            }
                        }
                    }
                }
            }


            //On met à jour les boutons et on vérifie que les boutons ne descendent pas trop bas, sinon on les détruits
            for (int i = buttons.Count-1; i >= 0; i--)
            {
                SpriteButton button = buttons[i];
                button.update(delta);

                if ((button.position.Y) > (OTRGame.getInstance().getHeight() - 156))
                {
                    buttons.Remove(button);
                    fails++;

                    if (fails >= maxFails)
                        OTRGame.getInstance().setScreen(new PerduScreen(score));
                }
            }

            //On met à jour les effets des touches
            foreach (SpriteLight light in lightSprites)
            {
                light.update(delta);
            }

            //On change la vitesse du niveau si on vient d'appuyer sur F3 (baisser vitesse) ou F4 (augmenter vitesse)
            float pallier = (50.0f);
            if (Keyboard.GetState().IsKeyDown(Keys.F3) && !lastState.IsKeyDown(Keys.F3))
            {
                if (SpriteButton.speed <= (50.0f))
                    SpriteButton.speed = (50.0f);
                else
                    SpriteButton.speed -= pallier;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.F4) && !lastState.IsKeyDown(Keys.F4))
            {
                SpriteButton.speed += pallier;
            }

            lastState = Keyboard.GetState();
        }

        public void addButton()
        {
            //nb = nombre de boutons à spawn en même temps
            int nb = 1;
            //On sélectionne un nombre entre 0 et 100...
            float probaNombreSpawn = (float)(rand.Next(10000))/100.0f;
            //... on a 0.5% de chance d'avoir 4 touches en même temps...
            if(probaNombreSpawn > 99.5f)
            {
                nb = 4;
            }
            //... 5% d'en avoir 3...
            else if(probaNombreSpawn > 94.5f)
            {
                nb = 3;
            }
            //... 7% d'en avoir 2...
            else if (probaNombreSpawn > 84.5f)
            {
                nb = 2;
            }
            //... 87.5% d'en avoir 1 (on ne met pas la valeur car elle est déjà à 1).

            //On fait un tableau avec les numéros des colonnes possibles
            List<int> position = new List<int>() { 0, 1, 2, 3 };
            //On ajoute nb boutons
            for (int i = 0; i < nb; i++)
            {
                //On selectionne une colonne au hasard
                int column = position[rand.Next(position.Count)];
                //On crée le bouton
                SpriteButton button = new SpriteButton(column);
                buttons.Add(button);
                //On enlève la colonne de la liste des colonnes dispo
                position.Remove(column);
            }

        }

        public void render(SpriteBatch batch)
        {
            Vector2 tailleScore = font.MeasureString("Score : " + score);
            Vector2 tailleFailed = font.MeasureString("Echecs : " + fails + "/"+ maxFails); 

            //On dessine la map
            base.render(batch);
            //On dessine d'abord les effets pour qu'ils ne soient pas au dessus des boutons
            foreach (SpriteLight light in lightSprites)
            {
                light.render(batch);
            }

            //On dessine les boutons
            foreach (SpriteButton button in buttons)
            {
                button.render(batch);
            }

            batch.DrawString(font, "Score : " + score, new Vector2(OTRGame.getInstance().getWidth() - tailleScore.X - 50, 20), Color.White);
            batch.DrawString(font, "Echecs : " + fails + "/" + maxFails, new Vector2(OTRGame.getInstance().getWidth() - tailleFailed.X - 50, tailleScore.Y + 20 + 20), Color.White); 
        }
    }
}
