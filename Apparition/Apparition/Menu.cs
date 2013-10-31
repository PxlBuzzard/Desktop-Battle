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
    public class Menu
    {
        #region Class Variables
        public enum GameState //tracks the current state of the game
        {
            MainMenu,
            PauseMenu,
            HelpMenu,
            GameOver,
            WinMenu,
            Playing,
            ResetStats,
            Exit
        }

        //all the menu classes
        public MainMenu mainMenu;
        public HelpMenu helpMenu;
        public PauseMenu pauseMenu;
        public WinMenu winMenu;
        public GameOver gameOverMenu;

        private Rectangle bgSize = new Rectangle(0, 0, Game1.cGameUI.maxX, Game1.cGameUI.maxY);
        public static Texture2D bgTexture; //the menu background texture
        public static Texture2D menuHeaderImage;
        public static Rectangle headerRectangle;
        protected bool showHeaderImage;

        public List<Button> buttons = new List<Button>(); //list of buttons in a menu
        public Menu.GameState stateToLoadOnEscape; //the logical state to load if user presses escape key
        #endregion

        /// <summary>
        /// Runs once on game load to initialize the menus.
        /// </summary>
        public void LoadContent()
        {
            mainMenu = new MainMenu();
            helpMenu = new HelpMenu();
            pauseMenu = new PauseMenu();
            winMenu = new WinMenu();
            gameOverMenu = new GameOver();

            bgTexture = Game1.theContentManager.Load<Texture2D>("pictures/menubg");
            menuHeaderImage = Game1.theContentManager.Load<Texture2D>("pictures/menuheader");
            headerRectangle = new Rectangle(400, 0, menuHeaderImage.Width, menuHeaderImage.Height);
        }

        /// <summary>
        /// Runs once per frame to update the buttons and checks for state change.
        /// </summary>
        public virtual void Update(GameTime gameTime) 
        {
            foreach (Button button in buttons)
            {
                button.Update();
            }
            if (Game1.currentKeyboardState.IsKeyDown(Keys.Escape) &&
                !Game1.lastKeyboardState.IsKeyDown(Keys.Escape))
            {
                Game1.currentState = stateToLoadOnEscape;
            }
        }

        /// <summary>
        /// Runs once a frame to draw the menu on the screen.
        /// </summary>
        public virtual void Draw() 
        {
            Game1.spriteBatch.Draw(Menu.bgTexture, bgSize, Color.White);
            foreach (Button button in buttons)
            {
                button.Draw();
            }
            if (showHeaderImage)
            {
                Game1.spriteBatch.Draw(menuHeaderImage, headerRectangle, Color.White);
            }
        }

        /// <summary>
        /// Runs once a frame to draw the current menu to the screen.
        /// </summary>
        public void DrawCurrentMenu()
        {
            if (Game1.currentState != Menu.GameState.Playing)
            {
                if (Game1.currentState == Menu.GameState.MainMenu)
                {
                    Game1.cMenu.mainMenu.Draw();
                }
                else if (Game1.currentState == Menu.GameState.PauseMenu)
                {
                    Game1.cMenu.pauseMenu.Draw();
                }
                else if (Game1.currentState == Menu.GameState.HelpMenu)
                {
                    Game1.cMenu.helpMenu.Draw();
                }
                else if (Game1.currentState == Menu.GameState.WinMenu)
                {
                    Game1.cMenu.winMenu.Draw();
                }
                else if (Game1.currentState == Menu.GameState.GameOver)
                {
                    Game1.cMenu.gameOverMenu.Draw();
                }
            }
        }
    }
}
