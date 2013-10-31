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
    /// Holds the abstract methods for every Gun in the game.
    /// </summary>
    abstract class Gun : Weapon
    {
        #region Class Variables
        public string theWeaponName; //the name of the weapon file
        protected static GraphicsDeviceManager graphicsManager;
        protected static ContentManager mContentManager;
        public Rectangle Size; //the size of the weapon, set at creation of weapon
        public Vector2 Position; //current position of the gun
        protected float gunAngle; //angle of the gun to the mouse
        public int DamagePerBullet; //how much damage the bullet does
        protected List<Bullet> listBullets;
        public List<Bullet> lBullets //list of bullets fired from the gun
        {
            get { return listBullets; }
            set { listBullets = value; }
        }
        private Texture2D mGunTexture; //the texture being used
        #endregion

        /// <summary>
        /// All guns must have their own method to fire bullets.
        /// </summary>
        public abstract void Shoot();

        /// <summary>
        /// Runs once every frame.
        /// </summary>
        public virtual void Update(GameTime gameTime, Vector2 heroPosition)
        {
            Position.X = heroPosition.X + 30;
            Position.Y = heroPosition.Y + 45;

            //updates and removes bullets if necessary
            for (int i = lBullets.Count() - 1; i >= 0; i--)
            {
                lBullets[i].Update(gameTime);
                if (!lBullets[i].isAlive)
                {
                    lBullets.Remove(lBullets[i]);
                }
            }

            //gets the angle of the gun to the mouse
            MouseState curMouse = Mouse.GetState();
            Vector2 mouseLoc = new Vector2(curMouse.X, curMouse.Y);
            Vector2 direction = -(heroPosition - mouseLoc);
            gunAngle = (float)(Math.Atan2(direction.Y, direction.X)); 
        }

        /// <summary>
        /// Runs once every frame to draw the Gun onto the screen.
        /// </summary>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mGunTexture, Position, null, Color.White, gunAngle,
            new Vector2(5, 20), 1.0f, SpriteEffects.None, 0f);
            foreach (var Bullet in lBullets)
            {
                Bullet.Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Creates a new Gun object given a gun name.
        /// </summary>
        /// <param name="assetName">The name of the gun in the file structure</param>
        public void LoadContent(string assetName)
        {
            lBullets = new List<Bullet>();
            mGunTexture = mContentManager.Load<Texture2D>(assetName); //stores the texture
            theWeaponName = mGunTexture.Name; //stores the texture name

            //creates a rectangular bounding box around the gun
            Size = new Rectangle(0, 0, (int)(mGunTexture.Width), (int)(mGunTexture.Height));
        }

        public override string ToString()
        {
            return "Gun Name: " + theWeaponName;
        }
    }
}
