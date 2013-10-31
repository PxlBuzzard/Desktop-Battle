#region Using Statements
using System;
using System.Collections.Generic;
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
    /// Holds the abstract methods for every Melee weapon in the game.
    /// </summary>
    abstract class Melee : Weapon
    {
        #region Class Variables
        public string theWeaponName; //the name of the weapon file
        protected static GraphicsDeviceManager graphicsManager;
        protected static ContentManager mContentManager;
        public Rectangle Size; //the size of the weapon, set at creation of weapon
        public Vector2 Position; //current position of the gun
        private float RotationAngle; //current angle of the gun to the mouse
        protected int DamagePerSwing = 20; //how much damage the bullet does
        private Texture2D mSpriteTexture; //the texture being used
        protected List<Bullet> listBullets;
        public List<Bullet> lBullets //list of bullets fired from the gun
        {
            get { return listBullets; }
            set { listBullets = value; }
        }
        //MouseState oldMouse; //state of the mouse on the previous frame
        #endregion

        /// <summary>
        /// All melee weapons must have their own method to swing.
        /// </summary>
        public abstract void Shoot();

        /// <summary>
        /// Runs once every frame.
        /// </summary>
        public virtual void Update(GameTime gameTime, Vector2 heroPosition)
        {
            Position.X = heroPosition.X + 10;
            Position.Y = heroPosition.Y + 50;

            //Calculates the angle of the weapon to the mouse
            float circle = MathHelper.Pi * 2;
            RotationAngle = RotationAngle % circle;
        }

        /// <summary>
        /// Runs once every frame to draw the weapon onto the screen.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSpriteTexture, Position, null, Color.White, RotationAngle,
            Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
        }

        /// <summary>
        /// Creates a new Melee object given a weapon name.
        /// </summary>
        /// <param name="assetName">The name of the weapon in the file structure</param>
        public void LoadContent(string assetName)
        {
            mSpriteTexture = mContentManager.Load<Texture2D>(assetName); //stores the texture

            //creates a rectangular bounding box around the weapon
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width), (int)(mSpriteTexture.Height));
        }
    }
}
