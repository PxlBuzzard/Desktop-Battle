using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DesktopBattle
{
    class GameUI 
    {
        #region Class Variables
        Hero cHero;
        Area cArea;
        int maxX;
        int maxY;
        #endregion

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
        public void Draw(SpriteBatch theSpriteBatch, SpriteFont font) 
        {
            string HP = Convert.ToString(cHero.HP);
            theSpriteBatch.DrawString(font, "HP: " + HP, new Vector2(20, maxY - 50), Color.White);
            if (cArea.currentRoom[2] && cArea.areaClear) 
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
