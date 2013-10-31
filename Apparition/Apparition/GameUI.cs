#region Using Statements
using System;

using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// Handles all the non-menu UI functions while the game is being played.
    /// </summary>
    public class GameUI 
    {
        #region Class Variables
        public int maxX; //actual screen size
        public int maxY; //actual screen size
        private Texture2D cursorTex; //custom cursor texture
        private Vector2 cursorPos; //cursor position
        private int timeToClear;
        private int nextClear = 3000;
        private string[] customStrings = new string[5]; //list of strings to draw
        #endregion

        public GameUI()
        {
            maxX = Game1.graphics.PreferredBackBufferWidth;
            maxY = Game1.graphics.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Runs once at startup to load information into the class.
        /// </summary>
        public void LoadContent() 
        {
            cursorTex = Game1.theContentManager.Load<Texture2D>("pictures/crosshair");

            //fill the custom string array
            int count = customStrings.Count() - 1;
            for (int i = 0; i <= count; i++)
            {
                customStrings[i] = "";
            }
        }

        /// <summary>
        /// Updates/Draws the user interface on every frame
        /// </summary>
        public void Draw(GameTime gameTime) 
        {
            //shows either the mouse or the crosshair
            if (Game1.currentState == Menu.GameState.Playing)
            {
                Game1.isMouseVisible = false;
                cursorPos = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
                Game1.spriteBatch.Draw(cursorTex, cursorPos, Color.White);
            }
            else 
            { 
                Game1.isMouseVisible = true; 
            }
            
            Game1.spriteBatch.DrawString(Game1.font, "HP: " + Game1.cHero.HP, new Vector2(70, maxY - 50), Color.White);
            Game1.spriteBatch.DrawString(Game1.font, "Total Kills: " + Game1.cHero.totalEnemiesKilled, new Vector2((maxX / 2 - 100), maxY - 50), Color.White);
            Game1.spriteBatch.DrawString(Game1.font, "Enemies left to kill: " +
                (Game1.cArea.Rooms[Game1.cArea.currentRoom].totalEnemies - Game1.cArea.killsInRoom), new Vector2(maxX - 350, maxY - 50), Color.White);
            
            //Draws a custom message for 3 seconds
            timeToClear += gameTime.ElapsedGameTime.Milliseconds;
            if (timeToClear >= nextClear)
            {
                int count = customStrings.Count() - 1;
                for (int i = 0; i <= count; i++)
                {
                    customStrings[i] = "";
                }
            }
            else
            {
                int count = customStrings.Count() - 1;
                for (int i = 0; i <= count; i++)
                {
                    Game1.spriteBatch.DrawString(Game1.font, customStrings[i], new Vector2(maxX / 2 - 200, maxY / 2 + 20 + 20 * i), Color.White);
                }
            }

            //shows text if the room is cleared
            if (Game1.cArea.areaClear) 
            {
                Game1.spriteBatch.DrawString(Game1.font, "Room Clear, continue on to next room ---->", new Vector2(maxX / 2 - 170, maxY / 2), Color.White);
            }
        }

        /// <summary>
        /// Adds a string to be drawn for 3 seconds
        /// </summary>
        /// <param name="textToDraw">string to be drawn</param>
        public void Draw(string textToDraw)
        {
            int count = customStrings.Count() - 1;
            for (int i = 0; i <= count; i++)
            {
                if (customStrings[i] == "")
                {
                    customStrings[i] = textToDraw;
                    break;
                }
            }
            timeToClear = 0;
        }
    }
}
