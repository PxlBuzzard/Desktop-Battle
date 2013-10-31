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
    /// Displays useful instructions.
    /// </summary>
    public class HelpMenu : Menu
    {
        public HelpMenu()
        {
            stateToLoadOnEscape = Menu.GameState.MainMenu;
            showHeaderImage = true;

            base.buttons.Add(new Button("Instructions",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Instructions").X / 2,
                    Game1.cGameUI.maxY / 2 - 40), Menu.GameState.HelpMenu));

            base.buttons.Add(new Button("- Use WASD or the arrow keys to move\n" +
            "- Use the mouse to aim and shoot\n" +
            "- Press Q to switch weapons\n" +
            "- Press Escape to pause\n",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("- Use WASD or the arrow keys to move").X / 2,
                    Game1.cGameUI.maxY / 2), Menu.GameState.HelpMenu));

            base.buttons.Add(new Button("Return to Main Menu",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Return to Main Menu").X / 2,
                    Game1.cGameUI.maxY - 100),
                    Menu.GameState.MainMenu));
        }

        public override void Draw()
        {
            base.Draw();
            Game1.spriteBatch.Draw(menuHeaderImage, headerRectangle, Color.White);
        }
    }
}
