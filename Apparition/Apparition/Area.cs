using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DesktopBattle
{
    class Area 
    {
        #region Class Variables
        //Background textures for the various screens in the game
        Texture2D room1;
        Texture2D room2;
        Texture2D room3;
        Texture2D currentRoomTexture;
        static SpriteBatch mSpriteBatch;
        Hero mHero;
        List<Sprite> lEnemies;
        Random rnd = new Random();
        
        public bool[] currentRoom = new bool[3] { true, false, false };
        public bool areaClear;
        #endregion

        /// <summary>
        /// Runs once at startup to load area information into memory
        /// </summary>
        public void LoadContent(ContentManager theContentManager, SpriteBatch theSpriteBatch, Hero theHero, List<Sprite> enemies) 
        {

            //Load the screen backgrounds
            room1 = theContentManager.Load<Texture2D>("pictures/room1");
            room2 = theContentManager.Load<Texture2D>("pictures/room2");
            room3 = theContentManager.Load<Texture2D>("pictures/room3");

            //Store the hero
            mHero = theHero;
            lEnemies = enemies;
            mSpriteBatch = theSpriteBatch;

            //Initialize the screen state variables
            currentRoomTexture = room1;
            areaClear = false;
        }

        /// <summary>
        /// Puts the background texture on screen, called on new room load
        /// </summary>
        public void Draw() 
        {
            mSpriteBatch.Draw(currentRoomTexture, Vector2.Zero, Color.White);
        }

        /// <summary>
        /// Creates a list of enemies
        /// </summary>
        /// <param name="numEnemies">Number of enemies to return</param>
        /// <returns>List of enemies</returns>
        public List<Sprite> GenerateEnemies(int numEnemies)
        {
            List<Sprite> tempList = new List<Sprite>();
            for (int i = 1; i <= numEnemies; i++)
            {
                tempList.Add(new Clippy());
            }
            return tempList;
        }

        /// <summary>
        /// This checks to see if the room has been cleared. If yes, then load the new room.
        /// </summary>
        public void Update(GameTime gameTime) 
        {
            if (lEnemies.Count() == 0)
            {
                areaClear = true;
            }
            else
            {
                areaClear = false;
            }
            //will load room 2 if applicable
            if (areaClear && currentRoom[0] && mHero.Position.X >= (mHero.maxX - 10)) 
            {
                currentRoomTexture = room2;
                currentRoom[0] = false;
                currentRoom[1] = true;
                this.Draw();
                lEnemies = this.GenerateEnemies(3);
                areaClear = false;
                mHero.Position.X = 10;
            }

            //will load room 3 if applicable
            else if (areaClear && currentRoom[1] && mHero.Position.X >= (mHero.maxX - 10)) 
            {
                currentRoomTexture = room3;
                currentRoom[1] = false;
                currentRoom[2] = true;
                this.Draw();
                lEnemies = this.GenerateEnemies(4);
                areaClear = false;
                mHero.Position.X = 10;
            }
        }
    }
}
