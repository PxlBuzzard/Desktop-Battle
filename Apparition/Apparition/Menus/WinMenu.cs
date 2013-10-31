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
    /// Displays when the players wins the game.
    /// </summary>
    public class WinMenu : Menu
    {
        public Texture2D winImage;
        public Rectangle winRectangle;

        public WinMenu()
        {
            stateToLoadOnEscape = Menu.GameState.MainMenu;
            showHeaderImage = false;
            winImage = Game1.theContentManager.Load<Texture2D>("pictures/winner");
            winRectangle = new Rectangle(400, 0, winImage.Width, winImage.Height);

            base.buttons.Add(new Button("Exit to Menu",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Exit to Menu").X / 2,
                    Game1.cGameUI.maxY / 2 + 80), Menu.GameState.MainMenu));

            base.buttons.Add(new Button("Exit Game",
                new Vector2(Game1.cGameUI.maxX / 2 - Game1.font.MeasureString("Exit Game").X / 2,
                    Game1.cGameUI.maxY / 2 + 120), Menu.GameState.Exit));
        }

        public override void Draw()
        {
            base.Draw();
            Game1.spriteBatch.Draw(winImage, winRectangle, Color.White);
        }
    }
}