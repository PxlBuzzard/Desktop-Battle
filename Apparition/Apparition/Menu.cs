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
    public abstract class Menu
    {

        public bool isVisible = false;
        public enum GameState
        {
            MainMenu,
            PauseMenu,
            HelpMenu,
            GameOver,
            WinMenu,
            Playing,
            LevelSelect
        }
        //public static MenuState currentMenuState { get; set; } //stores the current state

        /// <summary>
        /// Runs once a frame to draw the menu on the screen.
        /// </summary>

        public void Update(GameTime gameTime) { }

        public void Draw() { }


    }
}
