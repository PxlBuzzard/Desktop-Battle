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
    /// Makes a clickable button
    /// </summary>
    public class Button
    {
        private Vector2 Position; //position the button starts at
        private Rectangle BoundingBox; //the size of the button
        private Vector2 textPosition; //position the text starts inside the button
        private string Text; //text of the button
        public Menu.GameState stateToLoad; //game state to load on button click
        private string extraAction; //action other than state load (such as saving/loading)

        /// <summary>
        /// Creates a button.
        /// </summary>
        /// <param name="theText">text inside the button</param>
        /// <param name="thePosition">position of the button onscreen</param>
        /// <param name="theState">Game state to load on click</param>
        public Button(string theText, Vector2 thePosition, Menu.GameState theState)
        {
            Position = thePosition;
            textPosition = new Vector2(Position.X + 5, Position.Y + 5);
            Text = theText;
            stateToLoad = theState;
            BoundingBox = new Rectangle((int)thePosition.X, (int)thePosition.Y, (int)Game1.font.MeasureString(Text).X + 10, (int)Game1.font.MeasureString(Text).Y + 10);
        }

        /// <summary>
        /// Creates a button specifically for saving or loading.
        /// </summary>
        /// <param name="theText">text inside the button</param>
        /// <param name="thePosition">position of the button onscreen</param>
        /// <param name="saveOrLoad">Save = true, Load = false</param>
        public Button(string theText, Vector2 thePosition, Menu.GameState theState, string action)
        {
            Position = thePosition;
            textPosition = new Vector2(Position.X + 5, Position.Y + 5);
            Text = theText;
            extraAction = action.ToLower();
            stateToLoad = theState;
            BoundingBox = new Rectangle((int)thePosition.X, (int)thePosition.Y, (int)Game1.font.MeasureString(Text).X + 10, (int)Game1.font.MeasureString(Text).Y + 10);
        }

        /// <summary>
        /// Runs once per frame to draw the button to the screen.
        /// </summary>
        public void Draw()
        {
            Game1.spriteBatch.DrawString(Game1.font, Text, textPosition, Color.White);
        }

        /// <summary>
        /// Runs once per frame to check mouse state and click if necessary.
        /// </summary>
        /// <param name="mouseState"></param>
        public void Update()
        {
            if (CheckMouse())
            {
                Click();
            }

            if (extraAction == "updatekillcount")
            {
                Text = "Total Kills: " + Game1.cHero.totalEnemiesKilled;
            }
            else if (extraAction == "updatekillsleft")
            {
                if (Game1.cHero.totalEnemiesKilled < 100)
                    Text = "Unlock the shotgun in\n   " + (100 - Game1.cHero.totalEnemiesKilled) + " more kills!";
                else
                    Text = "  Shotgun unlocked!";
            }
        }

        /// <summary>
        /// Switches game state and handles extra actions on button click.
        /// </summary>
        public void Click() 
        {
            Game1.currentState = stateToLoad;
            if (extraAction == "save")
            {
                Game1.cSave.GameSaveRequested = true;
            }
            else if (extraAction == "load")
            {
                Game1.cSave.GameLoadRequested = true;
            }
        }

        /// <summary>
        /// Return true if mouse is inside of button's bounding box.
        /// </summary>
        /// <param name="mouseState">current state of the mouse</param>
        /// <returns>true if mouse is inside bounding box</returns>
        public bool CheckMouse()
        {
            if (Game1.currentMouseState.LeftButton == ButtonState.Pressed &&
                Game1.lastMouseState.LeftButton == ButtonState.Released &&
                Game1.currentMouseState.X <= BoundingBox.Right &&
                Game1.currentMouseState.X >= BoundingBox.Left &&
                Game1.currentMouseState.Y <= BoundingBox.Bottom &&
                Game1.currentMouseState.Y >= BoundingBox.Top)
            {
                return true;
            }
            return false;
        }
    }
}
