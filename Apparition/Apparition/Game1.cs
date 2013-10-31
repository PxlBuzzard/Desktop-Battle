//Daniel Jost
//GSD2 Milestone 2

//The structure for the menus is there, but I didn't have the time to get
//them properly implemented, they're coming in Milestone 4.

#region Variable Prefix Key
//variable prefix key:
//c = class
//m = method and/or general
//s = state
//i = integer
//b = boolean
//l = list
#endregion

#region Using Statements
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace DesktopBattle 
{
    public class Game1 : Microsoft.Xna.Framework.Game 
    {
        #region Class Variables
        static public GraphicsDeviceManager graphics;
        static public ContentManager theContentManager;
        static public SpriteBatch spriteBatch;
        static public SpriteFont font;
        static public Menu.GameState currentState; //keeps track of the current game state
        static public bool isMouseVisible;
        static public KeyboardState currentKeyboardState;
        static public MouseState currentMouseState;
        static public KeyboardState lastKeyboardState;
        static public MouseState lastMouseState;

        //list of classes
        static public Hero cHero;
        static public Area cArea;
        static public GameUI cGameUI;
        static public Combat cCombat;
        static public List<Sprite> lEnemies = new List<Sprite>();
        static public Queue<Sprite> qEnemies = new Queue<Sprite>();
        static public Save cSave;
        static public Menu cMenu;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            theContentManager = Content;
            IsMouseVisible = false; //shows the mouse ingame
            Content.RootDirectory = "Content";

            //Initialize screen size to an ideal resolution for the Xbox 360
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        /// <summary>
        /// This method is loaded once at the beginning of runtime
        /// </summary>
        protected override void Initialize() 
        {
            cHero = new Hero();
            lEnemies = new List<Sprite>();
            cArea = new Area();
            cGameUI = new GameUI();
            cCombat = new Combat();
            cSave = new Save();
            cMenu = new Menu();
            currentState = Menu.GameState.MainMenu;

            base.Initialize(); //last line
        }

        /// <summary>
        /// Runs once at startup to load objects into memory
        /// </summary>
        protected override void LoadContent() 
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("fonts/Exo");

            cHero.LoadContent();
            cArea.LoadContent();
            cGameUI.LoadContent();
            cMenu.LoadContent();
            cCombat.LoadContent();

            //loads the game save if a save file exists
            if (cSave.DoesSaveFileExist())
            {
                cSave.GameLoadRequested = true;
            }
        }

        /// <summary>
        /// Runs once every frame to update the content on screen.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                currentMouseState = Mouse.GetState();
                currentKeyboardState = Keyboard.GetState();

                cSave.Update();
                switch (currentState)
                {
                    case Menu.GameState.Playing:
                        //see if escape key is pressed
                        if (currentKeyboardState.IsKeyDown(Keys.Escape) &&
                            !lastKeyboardState.IsKeyDown(Keys.Escape))
                        {
                            currentState = Menu.GameState.PauseMenu;
                        }
                        //see if game window is visible
                        if (!IsActive)
                        {
                            currentState = Menu.GameState.PauseMenu;
                        }
                        cArea.Update(gameTime);
                        cHero.Update(gameTime);
                        cCombat.Update(gameTime);
                        foreach (Sprite Enemy in lEnemies)
                        {
                            Enemy.Update(gameTime);
                        }
                        break;
                    case Menu.GameState.MainMenu:
                        cMenu.mainMenu.Update(gameTime);
                        break;
                    case Menu.GameState.HelpMenu:
                        cMenu.helpMenu.Update(gameTime);
                        break;
                    case Menu.GameState.PauseMenu:
                        cMenu.pauseMenu.Update(gameTime);
                        break;
                    case Menu.GameState.WinMenu:
                        cMenu.winMenu.Update(gameTime);
                        break;
                    case Menu.GameState.GameOver:
                        cMenu.gameOverMenu.Update(gameTime);
                        break;
                    case Menu.GameState.ResetStats:
                        cSave.DeleteExisting(cSave.saveFileHero);
                        cSave.DeleteExisting(cSave.saveFileRooms);
                        cHero.totalEnemiesKilled = 0;
                        cHero.Position.X = cHero.startPositionX;
                        cHero.Position.Y = cHero.startPositionY;
                        cArea.currentRoom = 0;
                        currentState = Menu.GameState.Playing;
                        break;
                    case Menu.GameState.Exit:
                        Exit();
                        break;
                }

                lastKeyboardState = currentKeyboardState;
                lastMouseState = currentMouseState;
                base.Update(gameTime); //last line
            }
        }

        /// <summary>
        /// Called every time a new frame is drawn
        /// </summary>
        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(); //draws in z-index order

            //show mouse or crosshair cursor
            if (!isMouseVisible)
                IsMouseVisible = false;
            else IsMouseVisible = true;

            cArea.Draw();
            foreach (Sprite Enemy in lEnemies)
            {
                Enemy.Draw();
            }
            cHero.Draw();
            cGameUI.Draw(gameTime);
            cMenu.DrawCurrentMenu();
            spriteBatch.End();

            base.Draw(gameTime); //last line
        }
    }
}