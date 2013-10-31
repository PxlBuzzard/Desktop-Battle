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
    [System.Xml.Serialization.XmlInclude(typeof(Room))]
    [Serializable()]
    public class Area 
    {
        #region Class Variables
        Random rnd = new Random();
        public List<Room> Rooms = new List<Room>(); //room list
        private int lastRoom = -1; //last known room number
        [System.Xml.Serialization.XmlIgnore]
        public int currentRoom; //current room number
        [System.Xml.Serialization.XmlIgnore]
        public List<string> roomTex = new List<string>(); //stores room textures
        [System.Xml.Serialization.XmlIgnore]
        public bool areaClear = false; //remembers the room cleared state
        [System.Xml.Serialization.XmlIgnore]
        public int killsInRoom; //tracks number of kills in the room
        [System.Xml.Serialization.XmlIgnore]
        public int enemiesSpawnedInRoom; //tracks number of enemies spawned
        #endregion

        /// <summary>
        /// Runs once at startup to load area information into memory
        /// </summary>
        public void LoadContent()
        {
            //load the default backgrounds if not in loaded file
            if (roomTex.Count == 0) roomTex.Add("pictures/room1");
            if (roomTex.Count == 1) roomTex.Add("pictures/room2");
            if (roomTex.Count == 2) roomTex.Add("pictures/room3");

            //removes rooms if there are rooms in the list already
            for (int i = Rooms.Count() - 1; i >= 0; i--)
            {
                Rooms.RemoveAt(i);
            }

            //builds the Rooms list based on texture count
            for (int i = 0; i <= roomTex.Count - 1; i++)
            {
                Rooms.Add(new Room(roomTex[i], (i + 15)));
                //sanity check on number of enemies spawning in room
                Rooms[i].totalEnemies = (int)MathHelper.Clamp(Rooms[i].totalEnemies, 1, 100);
            }

            //sanity check on Room variables
            currentRoom = (int)MathHelper.Clamp(currentRoom, 0, Rooms.Count - 1);
            if (lastRoom == -1) lastRoom = currentRoom; //makes equal on first run

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
        private void LoadRoom(bool loadNextRoom)
        {
            if (loadNextRoom) currentRoom++;
            lastRoom = currentRoom;
            areaClear = false;
            killsInRoom = 0;
            enemiesSpawnedInRoom = 0;
            Game1.cCombat.stopSpawn = false;
            if (loadNextRoom) Game1.cSave.GameSaveRequested = true; //saves Hero in new room

            //removes any enemies left onscreen
            for (int i = Game1.lEnemies.Count() - 1; i >= 0; i--)
            {
                Game1.lEnemies[i].newlyCreated = true;
                Game1.lEnemies[i].isAlive = true;
                Game1.qEnemies.enqueue(Game1.lEnemies[i]);
                Game1.lEnemies.Remove(Game1.lEnemies[i]);
            }

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
            //check to see if current room number has changed (because of file load)
            if (currentRoom != lastRoom)
            {
                LoadRoom(false);
            }

            //check to see if the enemies are all dead
            if (Rooms[currentRoom].totalEnemies <= killsInRoom && Game1.lEnemies.Count <= 0)
            {
                areaClear = true;
            }

            //stop enemies spawning once the total enemy count is hit
            else if (Rooms[currentRoom].totalEnemies <= killsInRoom)
            {
                Game1.cCombat.stopSpawn = true;
            }

            //checks for completion of the last room
            if (areaClear && currentRoom == Rooms.Count - 1
                && Game1.currentState != Menu.GameState.WinMenu)
            {
                Game1.currentState = Menu.GameState.WinMenu;
                Game1.cSave.GameSaveRequested = true;
            }

            //will load the next room if applicable
            else if (areaClear && Game1.cHero.Position.X >= (Game1.cHero.maxX - 10)) 
            {
                LoadRoom(true);
            }
        }

        public override string ToString()
        {
            return "Current Room: " + currentRoom;
        }
    }
}
