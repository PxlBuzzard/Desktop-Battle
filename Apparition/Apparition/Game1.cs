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
        public Menu.GameState currentState; //keeps track of the current game state

        //list of classes
        static public Hero cHero;
        static public Area cArea;
        static public GameUI cGameUI;
        static public Combat cCombat;
        static public List<Sprite> lEnemies = new List<Sprite>();
        static public Queue<Sprite> qEnemies = new Queue<Sprite>();
        static public Save cSave;
        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            theContentManager = Content;
            this.Components.Add(new GamerServicesComponent(this));
            IsMouseVisible = false; //shows the mouse ingame
            Content.RootDirectory = "Content";

            //Initialize screen size to an ideal resolution for the Xbox 360
            Game1.graphics.PreferredBackBufferWidth = 1280;
            Game1.graphics.PreferredBackBufferHeight = 720;
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
            currentState = Menu.GameState.Playing;
            cSave = new Save();

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
        }

        /// <summary>
        /// Runs once every frame to update the content on screen.
        /// </summary>
        protected override void Update(GameTime gameTime)
        {
            #region Exit Code
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape) ||
                gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                Exit();
            }
            #endregion
            #region Save/Load Buttons
            //save
            if (keyboardState.IsKeyDown(Keys.F1))
            {
                cSave.GameSaveRequested = true;
            }
            //load
            if (keyboardState.IsKeyDown(Keys.F2))
            {
                cSave.GameLoadRequested = true;
            }
            #endregion

            cSave.Update();
            switch (currentState)
            {
                case Menu.GameState.Playing:
                    cArea.Update(gameTime);
                    cHero.Update(gameTime);
                    foreach (Sprite Enemy in lEnemies)
                    {
                        Enemy.Update(gameTime);
                    }
                    cCombat.Update(gameTime);
                    break;

                case Menu.GameState.MainMenu:
                    //MENU UPDATE CODE
                    break;
            }
            base.Update(gameTime); //last line
        }

        /// <summary>
        /// Called every time a new frame is drawn
        /// </summary>
        protected override void Draw(GameTime gameTime) 
        {
            GraphicsDevice.Clear(Color.Black);
            Game1.spriteBatch.Begin(); //draws in z-index order
            switch (currentState) 
            {
                case Menu.GameState.Playing:
                    cArea.Draw();
                    foreach (Sprite Enemy in lEnemies)
                    {
                        Enemy.Draw();
                    }
                    cHero.Draw();
                    cGameUI.Draw(gameTime);
                    break;

                case Menu.GameState.MainMenu:
                    //MENU DRAW CODE
                    break;

                case Menu.GameState.PauseMenu:
                    //PAUSE MENU DRAW CODE
                    break;
            }
            Game1.spriteBatch.End();

            base.Draw(gameTime); //last line
        }
    }
}