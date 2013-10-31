//Daniel Jost
//GSD2 Homework 2

//I blew hours trying get to get Clippy to load in a list, and now an array...
//I want to use a list in the next version, but I had to make sacrifices

//variable prefix key:
//c = class
//m = method and/or general
//s = state
//i = integer
//b = boolean
//l = list

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

namespace DesktopBattle 
{
    public class Game1 : Microsoft.Xna.Framework.Game 
    {
        #region Class Variables
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Viewport viewport;

        //list of classes
        static Hero cHero;
        Area cArea;
        static GameUI cGameUI;
        static Combat cCombat;
        static List<Sprite> lEnemies = new List<Sprite>();

        //this enum stores the current state of the game
        public enum State { MainMenu, InGame, GameOver }
        public State sCurrentState;
        #endregion

        public Game1() 
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
            viewport = new Viewport();

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
            //mClippy1 = new Clippy();
            lEnemies = new List<Sprite>();
            cArea = new Area();
            cGameUI = new GameUI();
            cCombat = new Combat();

            base.Initialize();
        }

        protected override void LoadContent() 
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("fonts/SpriteFont1");

            cHero.LoadContent(this.Content);
            //mClippy1.LoadContent(this.Content); //make a list
            cCombat.LoadContent(cHero, lEnemies);
            cHero.LoadOther(cCombat, viewport);
            cArea.LoadContent(this.Content, this.spriteBatch, cHero, lEnemies);
            cGameUI.LoadContent(cHero, cArea, this.graphics);
        }

        protected override void Update(GameTime gameTime) 
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            cArea.Update(gameTime);
            cHero.Update(gameTime);
            //mClippy1.Update(gameTime); //make list
           
            base.Update(gameTime);
        }

        /// <summary>
        /// Called every time a new frame is drawn
        /// </summary>
        protected override void Draw(GameTime gameTime) 
        {
            this.spriteBatch.Begin(); //draws in z-index order
            cHero.Draw(this.spriteBatch);
            foreach (Sprite Enemy in lEnemies)
            {
                Enemy.Draw(this.spriteBatch);
            }
            cCombat.Update();
            cGameUI.Draw(this.spriteBatch, font);
            this.spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}