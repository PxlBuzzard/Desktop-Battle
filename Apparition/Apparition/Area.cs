#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// Handles anything relating to the rooms that the player plays in.
    /// </summary>
    class Area 
    {
        #region Class Variables
        Hero mHero;
        Combat mCombat;
        List<Sprite> lEnemies;
        Random rnd = new Random();
        public Room[] Rooms = new Room[3]; //room array
        public int currentRoom; //remembers current room number
        protected string[] roomTex = new string[3]; //stores room textures
        public bool areaClear = false; //remembers the room cleared state
        protected bool inNewRoom = false; //checks to see if player wants to load next room
        #endregion

        /// <summary>
        /// Runs once at startup to load area information into memory
        /// </summary>
        public void LoadContent(ContentManager theContentManager, Combat theCombat, Hero theHero, List<Sprite> enemies)
        {
            //load the backgrounds into the texture array
            roomTex[0] = "pictures/room1";
            roomTex[1] = "pictures/room2";
            roomTex[2] = "pictures/room3";
            for (int i = 0; i <= 2; i++) //make dynamic to handle levels in the future
            {
                Rooms[i] = new Room(theContentManager, roomTex[i], (i + 2));
            }
            //Store the incoming classes
            mCombat = theCombat;
            mCombat.CreateEnemies(Rooms[currentRoom].initialEnemyCount); //fills the enemy list for the first room
            mHero = theHero;
            lEnemies = enemies;
        }

        /// <summary>
        /// Puts the background texture on screen, called on new room load
        /// </summary>
        public void Draw(SpriteBatch theSpriteBatch) 
        {
            Rooms[currentRoom].DrawRoom(theSpriteBatch);
            if (inNewRoom)
            {
                LoadNewRoom(theSpriteBatch);
            }
        }

        /// <summary>
        /// Creates the next room in the level. Called from the Draw() method.
        /// </summary>
        private void LoadNewRoom(SpriteBatch theSpriteBatch)
        {
            currentRoom++;
            areaClear = false;
            inNewRoom = false;
        }

        /// <summary>
        /// This checks to see if the room has been cleared. If yes, then load the new room.
        /// </summary>
        public void Update(GameTime gameTime) 
        {
            //checks to see if the enemies are all dead
            if (lEnemies.Count() == 0)
            {
                areaClear = true;
            }
            //will load room 2 if applicable
            if (areaClear && currentRoom == 0 && mHero.Position.X >= (mHero.maxX - 10)) 
            {
                inNewRoom = true;
                mCombat.CreateEnemies(Rooms[currentRoom + 1].initialEnemyCount);
                mHero.Position.X = 10;
            }

            //will load room 3 if applicable
            else if (areaClear && currentRoom == 1 && mHero.Position.X >= (mHero.maxX - 10)) 
            {
                inNewRoom = true;
                mCombat.CreateEnemies(Rooms[currentRoom + 1].initialEnemyCount);
                mHero.Position.X = 10;
            }
        }

        public override string ToString()
        {
            return "Current Room: " + currentRoom;
        }
    }
}
