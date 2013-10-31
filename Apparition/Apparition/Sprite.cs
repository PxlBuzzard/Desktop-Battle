#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// The main class that draws individual objects to the screen.
    /// </summary>
    class Sprite 
    {
        #region Class Variables
        public Vector2 Position = new Vector2(0, 0); //current position
        protected Vector2 spriteDirection = Vector2.Zero; //initial direction of the enemy
        protected Vector2 spriteSpeed = Vector2.Zero; //initial speed of the enemy
        protected float spriteAngle; //rotation of the sprite
        protected Texture2D mSpriteTexture; //the texture being used
        public Rectangle Size //the size of the sprite
        {
            get
            {
                return new Rectangle(
                    (int)Position.X,
                    (int)Position.Y,
                    mSpriteTexture.Width,
                    mSpriteTexture.Height);
            }
        }
        public int HP { get { return hp; } //hit points
            set //caps the hit points from 0 to 100
            { 
                MathHelper.Clamp(hp, 0, 100);
                hp = value;
                if (hp == 0) isAlive = false;
            }
        }
        private int hp; //the private hp variable
        public bool isAlive = true; //checks the "living" status of the object
        public bool newlyCreated = true; //checks for recent creation

        //this is the max value an object is allowed to reach (its personal screen edge)
        public int maxX { get { return MaxX; } set { MaxX = value; } }
        public int maxY { get { return MaxY; } set { MaxY = value; } }
        private int MaxX;
        private int MaxY;

        protected enum State { Moving, Idling, Dying } //keeps track of what state the sprite is in
        protected State sCurrentState;
        #endregion

        /// <summary>
        /// Used to bring a new sprite into existence at either startup or during play
        /// </summary>
        public void LoadContent(ContentManager theContentManager, string assetName, GraphicsDeviceManager graphics) 
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(assetName); //stores the texture

            //generates the "safe" area of the screen for the sprite
            maxX = graphics.GraphicsDevice.Viewport.Width - Size.Width;
            maxY = graphics.GraphicsDevice.Viewport.Height - Size.Height;
        }

        /// <summary>
        /// Draw the sprite onto the screen
        /// </summary>
        public virtual void Draw(SpriteBatch theSpriteBatch) 
        {
            if (isAlive) 
            {
                theSpriteBatch.Draw(mSpriteTexture, Position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
                    Color.White, spriteAngle, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// Passes down the single argument Update() to child classes.
        /// </summary>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Passes down the double argument LoadContent() to child classes.
        /// </summary>
        public virtual void LoadContent(ContentManager theContentManager, GraphicsDeviceManager graphics) { }

        /// <summary>
        /// Handles movement around the screen, doesn't allow the sprite to go off-screen
        /// </summary>
        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection) 
        {
            if (Position.X < maxX && Position.X > 0) 
            {
                Position.X += theDirection.X * theSpeed.X * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            }
            //this handles the sprite trying to move off the screen
            else if (Position.X < 0) 
            {
                Position.X += 1;
            }
            else 
            {
                Position.X -= 1;
            }

            if (Position.Y < maxY && Position.Y > 0) 
            {
                Position.Y += theDirection.Y * theSpeed.Y * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Position.Y < 0) 
            {
                Position.Y += 1;
            }
            else 
            {
                Position.Y -= 1;
            }
        }

        public override string ToString()
        {
            return "Sprite Name: " + mSpriteTexture.Name;
        }
    }
}
