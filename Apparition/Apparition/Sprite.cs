using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DesktopBattle
{
    class Sprite 
    {
        #region Class Variables
        public string theAssetName;
        public Rectangle Size; //the size of the sprite
        public Vector2 Position = new Vector2(0, 0); //current position
        public Color[] TextureData; // The color data for the images; used for per pixel collision
        private Texture2D mSpriteTexture; //the texture being used
        private Viewport vPort; //used for screen size
        public int HP { get { return hp; } //hit points
            set 
            { 
                hp = value;
                MathHelper.Clamp(hp, 0, 100);
                if (hp == 0) isAlive = false;
            }
        }
        private int hp;
        public bool isAlive = true;

        //this is the max value an object is allowed to reach (its personal screen edge)
        public int maxX { get { return MaxX; } set { MaxX = value; } }
        public int maxY { get { return MaxY; } set { MaxY = value; } }
        private int MaxX;
        private int MaxY;

        protected enum State { Moving, Dying } //keeps track of what state the sprite is in
        protected State mCurrentState;
        protected Combat mCombat;
        #endregion

        /// <summary>
        /// Used to bring a new sprite into existence at either startup or during play
        /// </summary>
        public void LoadContent(ContentManager theContentManager, string assetName) 
        {
            mSpriteTexture = theContentManager.Load<Texture2D>(assetName);
            theAssetName = assetName;

            //creates a rectangular bounding box around the sprite
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width), (int)(mSpriteTexture.Height));

            //generates the "safe" area of the screen for the sprite
            maxX = vPort.Width - Size.Width;
            maxY = vPort.Height - Size.Height;

            //gets the color information of the sprite
            TextureData = new Color[mSpriteTexture.Width * mSpriteTexture.Height];
            mSpriteTexture.GetData(TextureData);
        }

        /// <summary>
        /// Runs once at startup to get shared information into the class
        /// </summary>
        public void LoadOther(Combat combat, Viewport viewport) 
        {
            mCombat = combat;
            vPort = viewport;
        }

        /// <summary>
        /// Draw the sprite onto the screen
        /// </summary>
        public void Draw(SpriteBatch theSpriteBatch) 
        {
            if (isAlive) 
            {
                theSpriteBatch.Draw(mSpriteTexture, Position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
            }
        }

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
    }
}
