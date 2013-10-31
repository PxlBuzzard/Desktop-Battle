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
    /// Makes a room that will be one of many inside a level.
    /// </summary>
    public class Room : Area
    {
        #region Class Variables
        public Texture2D roomBackground; //the background texture
        public int totalEnemies; //number of enemies that will be in the room on first load.
        #endregion

        /// <summary>
        /// Creates a new room with a background texture and an initial number of enemies.
        /// </summary>
        /// <param name="roomTexture">The string of the background texture of the room</param>
        /// <param name="numEnemies">number of initial enemies to spawn in the room</param>
        public Room(string roomTexture, int numEnemies)
        {
            roomBackground = Game1.theContentManager.Load<Texture2D>(roomTexture);
            totalEnemies = numEnemies;
        }

        /// <summary>
        /// Runs once per room to draw the background texture.
        /// </summary>
        /// <param name="theSpriteBatch"></param>
        public void DrawRoom(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(roomBackground, Vector2.Zero, Color.White);
        }
    }
}
