#region Using Statements
using System;
using System.Collections.Generic;
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
    class GameUI 
    {
        #region Class Variables
        Hero cHero;
        Area cArea;
        int maxX; //actual screen size
        int maxY; //actual screen size
        #endregion

        /// <summary>
        /// Runs once at startup to load information into the class.
        /// </summary>
        public void LoadContent(Hero theHero, Area theArea, GraphicsDeviceManager graphics) 
        {
            cHero = theHero;
            cArea = theArea;
            maxX = graphics.PreferredBackBufferWidth;
            maxY = graphics.PreferredBackBufferHeight;
        }

        /// <summary>
        /// Updates the user interface on every frame
        /// </summary>
        public void Draw(SpriteBatch theSpriteBatch, SpriteFont font,ContentManager theContentManager ,GraphicsDeviceManager graphics) 
        {
            string HP = Convert.ToString(cHero.HP);
            theSpriteBatch.DrawString(font, "HP: " + HP, new Vector2(20, maxY - 50), Color.White);
            if (cHero.HP <= 0)
            {
                theSpriteBatch.DrawString(font, "Game Over, press ESCAPE to exit", new Vector2(maxX / 2 - 140, maxY / 2), Color.White);

            }
            if (cArea.currentRoom == 1 && cArea.areaClear)
            {
                cHero.weapons[1] = new M16(theContentManager, graphics);
                theSpriteBatch.DrawString(font, "You unlocked the M16! Press Q to cycle weapons.", new Vector2(maxX / 2 - 180, maxY / 2), Color.White);
            }
            else if (cArea.currentRoom == 2 && cArea.areaClear)  //MAKE THIS LOAD A MENU SCREEN LATER, OPPOSITE OF GAME OVER
            {
                theSpriteBatch.DrawString(font, "Press ESCAPE to exit. Thanks for playing!", new Vector2(maxX / 2 - 180, maxY / 2), Color.White);
            }
            else if (cArea.areaClear) 
            {
                theSpriteBatch.DrawString(font, "Area Clear, move to next room ---->", new Vector2(maxX / 2 - 140, maxY / 2), Color.White);
            }
        }
    }
}
