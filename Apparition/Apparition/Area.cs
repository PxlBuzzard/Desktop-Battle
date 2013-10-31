#region Using Statements
using System;
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
    [Serializable()]
    public class Area 
    {
        #region Class Variables
        Random rnd = new Random();
        public Room[] Rooms = new Room[3]; //room array
        public int currentRoom; //remembers current room number
        protected string[] roomTex = new string[3]; //stores room textures
        public bool areaClear = false; //remembers the room cleared state
        public int killsInRoom; //tracks number of kills in the room
        public int enemiesSpawnedInRoom; //tracks number of enemies spawned
        #endregion

        /// <summary>
        /// Runs once at startup to load area information into memory
        /// </summary>
        public void LoadContent()
        {
            //load the backgrounds into the texture array
            roomTex[0] = "pictures/room1";
            roomTex[1] = "pictures/room2";
            roomTex[2] = "pictures/room3";
            for (int i = 0; i <= 2; i++) //make dynamic to handle levels in the future
            {
                Rooms[i] = new Room(roomTex[i], (i + 15));
            }
            //fills the enemy queue for the duration of the game
            Game1.cCombat.CreateEnemyQueue(20);
        }

        /// <summary>
        /// Puts the background texture on screen
        /// </summary>
        public void Draw() 
        {
            Rooms[currentRoom].DrawRoom(Game1.spriteBatch);
        }

        /// <summary>
        /// Creates the next room in the level. Called from the Update() method.
        /// </summary>
        private void LoadNewRoom()
        {
            currentRoom++;
            areaClear = false;
            killsInRoom = 0;
            enemiesSpawnedInRoom = 0;
            Game1.cCombat.stopSpawn = false;
            Game1.cSave.GameSaveRequested = true; //saves Hero in new room

            //removes any bullets from the screen on room change
            if (Game1.cHero.weapons[Game1.cHero.currentWeapon] is Gun)
            {
                foreach (Bullet bullet in Hero.lBullets)
                {
                    bullet.isAlive = false;
                }
            }
            Game1.cHero.Position.X = 10; //moves the Hero to the left side of screen
        }

        /// <summary>
        /// This checks to see if the room has been cleared. If yes, then load the new room.
        /// </summary>
        public void Update(GameTime gameTime) 
        {
            //checks to see if the enemies are all dead
            if (Rooms[currentRoom].totalEnemies <= killsInRoom && Game1.lEnemies.Count <= 0)
            {
                areaClear = true;
            }
            else if (Rooms[currentRoom].totalEnemies <= killsInRoom)
            {
                Game1.cCombat.stopSpawn = true;
            }
            //checks to see if player is in the last room
            if (areaClear && currentRoom == Rooms.Length)
            {
                //ADD FINISH LEVEL CODE
                //Game1.currentState = Menu.MenuState.WinMenu;
            }

            //will load the next room if applicable
            else if (areaClear && Game1.cHero.Position.X >= (Game1.cHero.maxX - 10)) 
            {
                LoadNewRoom();
            }
        }

        public override string ToString()
        {
            return "Current Room: " + currentRoom;
        }
    }
}
