#region Using Statements
using System;
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
    public class Sprite 
    {
        #region Class Variables
        public Vector2 Position = new Vector2(0, 0); //current position
        protected Vector2 spriteDirection = Vector2.Zero; //initial direction of the enemy
        protected Vector2 spriteSpeed = Vector2.Zero; //initial speed of the enemy
        protected float spriteAngle = 0.0f; //rotation of the sprite
        [System.Xml.Serialization.XmlIgnore]
        public Texture2D mSpriteTexture; //the texture being used
        [System.Xml.Serialization.XmlIgnore]
        public string spriteName; //name of the sprite
        static protected Random rnd = new Random();
        [System.Xml.Serialization.XmlIgnore]
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
                if (isAlive)
                {
                    MathHelper.Clamp(hp, 0, 100);
                    hp = value;
                    if (hp <= 0) isAlive = false;
                }
            }
        }
        private int hp; //the private hp variable
        public bool isAlive = true; //checks the "living" status of the object
        [System.Xml.Serialization.XmlIgnore]
        public bool newlyCreated = true; //checks for recent creation

        //this is the max value an object is allowed to reach (its personal screen edge)
        [System.Xml.Serialization.XmlIgnore]
        public int maxX { get { return MaxX; } set { MaxX = value; } }
        [System.Xml.Serialization.XmlIgnore]
        public int maxY { get { return MaxY; } set { MaxY = value; } }
        private int MaxX;
        private int MaxY;

        protected enum State { Moving, Idling, Dying } //keeps track of what state the sprite is in
        protected State sCurrentState;
        #endregion

        /// <summary>
        /// Used to bring a new sprite into existence at either startup or during play
        /// </summary>
        public void LoadContent(string assetName) 
        {
            mSpriteTexture = Game1.theContentManager.Load<Texture2D>(assetName); //stores the texture
            spriteName = assetName;

            //generates the "safe" area of the screen for the sprite
            maxX = Game1.graphics.GraphicsDevice.Viewport.Width - Size.Width;
            maxY = Game1.graphics.GraphicsDevice.Viewport.Height - Size.Height;
        }

        /// <summary>
        /// Makes a "randomized" starting location for the sprite.
        /// </summary>
        protected void GenerateStartingLocation()
        {
            Position.X = maxX - 30;
            Position.Y = maxY - 100 - rnd.Next(450);
        }

        /// <summary>
        /// Draw the sprite onto the screen
        /// </summary>
        public virtual void Draw() 
        {
            if (isAlive) 
            {
                Game1.spriteBatch.Draw(mSpriteTexture, Position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
                    Color.White, spriteAngle, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
        }

        /// <summary>
        /// Passes down the single argument Update() to child classes.
        /// </summary>
        public virtual void Update(GameTime gameTime) { }

        /// <summary>
        /// Passes down the no argument LoadContent() to child classes.
        /// </summary>
        public virtual void LoadContent() { }

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
            else if (Position.X <= 0) 
            {
                Position.X = 1;
            }
            else 
            {
                Position.X = maxX - 1;
            }

            if (Position.Y < maxY && Position.Y > 0) 
            {
                Position.Y += theDirection.Y * theSpeed.Y * (float)theGameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Position.Y <= 0) 
            {
                Position.Y = 1;
            }
            else 
            {
                Position.Y = maxY - 1;
            }
        }

        public override string ToString()
        {
            return "Sprite Name: " + spriteName;
        }
    }
}
