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
    /// Displays when the player pauses or the game loses focus.
    /// </summary>
    public class GameOver : Menu
    {
        public GameOver()
        {
            stateToLoadOnEscape = Menu.GameState.MainMenu;
            showHeaderImage = true;

            base.buttons.Add(new Button("Game Over!",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Game Over!").X / 2,
                    Game1.cGameUI.maxY / 2 - 60), Menu.GameState.GameOver));

            base.buttons.Add(new Button("Load",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Load").X / 2,
                    Game1.cGameUI.maxY / 2), Menu.GameState.Playing, "load"));

            base.buttons.Add(new Button("Exit to Menu",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Exit to Menu").X / 2,
                    Game1.cGameUI.maxY / 2 + 40), Menu.GameState.MainMenu));

            base.buttons.Add(new Button("Exit Game",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Exit Game").X / 2,
                    Game1.cGameUI.maxY / 2 + 80), Menu.GameState.Exit));
        }
    }
}