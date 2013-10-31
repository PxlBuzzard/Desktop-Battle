#region Using Statements
using System;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// Weapon covers projectile and melee based weapons.
    /// </summary>
    [Serializable()]
    [System.Xml.Serialization.XmlInclude(typeof(Gun))]
    [System.Xml.Serialization.XmlInclude(typeof(Melee))]
    [System.Xml.Serialization.XmlInclude(typeof(Pistol))]
    [System.Xml.Serialization.XmlInclude(typeof(M16))]
    [System.Xml.Serialization.XmlInclude(typeof(Shotgun))]
    public abstract class Weapon
    {
        #region Class Variables
        [System.Xml.Serialization.XmlIgnore]
        public string theWeaponName; //the name of the weapon file
        [System.Xml.Serialization.XmlIgnore]
        public Rectangle Size; //the size of the weapon, set at creation of weapon
        [System.Xml.Serialization.XmlIgnore]
        public Vector2 Position; //current position of the weapon
        protected float weaponAngle; //angle of the weapon to the mouse
        public int DamagePerAttack; //how much damage each bullet or melee swing does
        protected Texture2D mWeaponTexture; //the texture being used
        public SpriteEffects SpriteEffect //flips the gun to face the mouse
        {
            get
            {
                if (Game1.cHero.isFlipped) return SpriteEffects.FlipVertically;
                else return SpriteEffects.None;
            }
        }
        [System.Xml.Serialization.XmlIgnore]
        public SoundEffect shootSound;
        #endregion

        /// <summary>
        /// Creates a new Weapon object given a weapon name.
        /// </summary>
        /// <param name="assetName">The name of the weapon in the file structure</param>
        public virtual void LoadContent(string assetName)
        {
            mWeaponTexture = Game1.theContentManager.Load<Texture2D>(assetName); //stores the texture
            theWeaponName = mWeaponTexture.Name;

            //creates a rectangular bounding box around the weapon
            Size = new Rectangle(0, 0, (int)(mWeaponTexture.Width), (int)(mWeaponTexture.Height));
        }

        public virtual void Shoot() { }

        public virtual void Swing() { }

        public static Stack<Bullet> GenerateBulletStack(int numBullets)
        {
            Stack<Bullet> sBullets = new Stack<Bullet>(numBullets);
            for (int i = 0; i < numBullets; i++)
            {
                sBullets.push(new Bullet());
            }
            return sBullets;
        }

        public virtual void Update(GameTime gameTime, Vector2 heroPosition) { }

        /// <summary>
        /// Runs once every frame to draw the weapon onto the screen.
        /// </summary>
        public virtual void Draw()
        {
            Game1.spriteBatch.Draw(mWeaponTexture, Position, null, Color.White, weaponAngle,
            new Vector2(5, 20), 1.0f, SpriteEffect, 0f);
        }
    }
}
