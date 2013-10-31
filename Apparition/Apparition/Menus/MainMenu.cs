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
    /// Shows on startup and when applicable.
    /// </summary>
    public class MainMenu : Menu
    {
        private bool addedContinueButton;
        public MainMenu()
        {
            stateToLoadOnEscape = Menu.GameState.MainMenu;
            showHeaderImage = true;

            base.buttons.Add(new Button("Start New Game",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Start New Game").X / 2,
                Game1.cGameUI.maxY / 2),
                Menu.GameState.ResetStats));

            base.buttons.Add(new Button("Total Kills: " + Game1.cHero.totalEnemiesKilled,
                    new Vector2(Game1.cGameUI.maxX / 2 + 230,
                    Game1.cGameUI.maxY / 2 - 40),
                    Menu.GameState.MainMenu, "updatekillcount"));

            //I have to let "updatekillsleft" handle the text here
            base.buttons.Add(new Button("",
            new Vector2(Game1.cGameUI.maxX / 2 + 200,
            Game1.cGameUI.maxY / 2),
            Menu.GameState.MainMenu, "updatekillsleft"));

            base.buttons.Add(new Button("How to Play",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("How to Play").X / 2,
                Game1.cGameUI.maxY / 2 + 40),
                Menu.GameState.HelpMenu));

            base.buttons.Add(new Button("Exit",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Exit").X / 2,
                Game1.cGameUI.maxY / 2 + 80),
                Menu.GameState.Exit));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!addedContinueButton && Game1.cHero.totalEnemiesKilled != 0)
            {
                base.buttons.Add(new Button("Continue",
                    new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Continue").X / 2,
                    Game1.cGameUI.maxY / 2 - 40),
                    Menu.GameState.Playing));
                addedContinueButton = true;
            }
        }
    }
}
