//Daniel Jost
//GSD2 Milestone 2

//Note to self: Make Rooms into a linked list for Milestone 3.

//The structure for the menus is there, but I didn't have the time to get
//them properly implemented.

//The 4 compile warnings are because of the Melee class which has no child classes yet,
//hence the null warnings

//HUGE PROBLEM: All the Clippys share the same variables.

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
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace DesktopBattle 
{
    public class Game1 : Microsoft.Xna.Framework.Game 
    {
        #region Class Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        //list of classes
        static Hero cHero;
        static Area cArea;
        static GameUI cGameUI;
        static Combat cCombat;
        static List<Sprite> lEnemies = new List<Sprite>();
        #endregion

        public Game1() 
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true; //shows the mouse ingame
            Content.RootDirectory = "Content";

            //Initialize screen size to an ideal resolution for the Xbox 360
            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;
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

            base.Initialize();
        }

        /// <summary>
        /// Runs once at startup to load objects into memory
        /// </summary>
        protected override void LoadContent() 
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("fonts/SpriteFont1");

            cHero.LoadContent(this.Content, this.graphics);
            cCombat.LoadContent(cHero, lEnemies);
            cArea.LoadContent(this.Content, cCombat, cHero, lEnemies);
            foreach (var Enemy in lEnemies)
            {
                Enemy.LoadContent(this.Content, this.graphics);
            }
            cGameUI.LoadContent(cHero, cArea, this.graphics);
        }

        /// <summary>
        /// Runs once every frame to update the content on screen.
        /// </summary>
        protected override void Update(GameTime gameTime) 
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Escape) ||
                gamePadState.Buttons.Back == ButtonState.Pressed)
            {
                Exit();
            }
            cArea.Update(gameTime);
            cHero.Update(gameTime);
            foreach (var Enemy in lEnemies)
            {
                if (Enemy.newlyCreated)
                {
                    Enemy.LoadContent(this.Content, this.graphics);
                }
                Enemy.Update(gameTime);
            }
            cCombat.Update(gameTime);
           
            base.Update(gameTime); //last line
        }

        /// <summary>
        /// Called every time a new frame is drawn
        /// </summary>
        protected override void Draw(GameTime gameTime) 
        {
            this.spriteBatch.Begin(); //draws in z-index order
            cArea.Draw(this.spriteBatch);
            foreach (var Enemy in lEnemies)
            {
                Enemy.Draw(this.spriteBatch);
            }
            cHero.Draw(this.spriteBatch);
            cGameUI.Draw(this.spriteBatch, font, this.Content, this.graphics);
            this.spriteBatch.End();

            base.Draw(gameTime); //last line
        }
    }
}