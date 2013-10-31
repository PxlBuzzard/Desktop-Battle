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
    [Serializable()]
    public abstract class Gun : Weapon
    {
        /// <summary>
        /// All guns must have their own method to fire bullets.
        /// </summary>
        public override void Shoot() { }

        /// <summary>
        /// Runs once every frame.
        /// </summary>
        public override void Update(GameTime gameTime, Vector2 heroPosition)
        {
            Position.X = heroPosition.X + 30;
            Position.Y = heroPosition.Y + 45;

            //updates and removes bullets if necessary
            for (int i = Hero.lBullets.Count() - 1; i >= 0; i--)
            {
                Hero.lBullets[i].Update(gameTime);
                if (!Hero.lBullets[i].isAlive)
                {
                    Hero.sBullets.push(Hero.lBullets[i]);
                    Hero.lBullets.Remove(Hero.lBullets[i]);
                }
            }

            //gets the angle of the gun to the mouse
            MouseState curMouse = Mouse.GetState();
            Vector2 mouseLoc = new Vector2(curMouse.X, curMouse.Y);
            Vector2 direction = -(heroPosition - mouseLoc);
            weaponAngle = (float)(Math.Atan2(direction.Y, direction.X)); 
        }

        /// <summary>
        /// Runs once every frame to draw the Gun onto the screen.
        /// </summary>
        public override void Draw()
        {
            Game1.spriteBatch.Draw(mWeaponTexture, Position, null, Color.White, weaponAngle,
            new Vector2(5, 20), 1.0f, base.SpriteEffect, 0f);
            foreach (var Bullet in Hero.lBullets)
            {
                Bullet.Draw();
            }
        }

        /// <summary>
        /// Creates a new Gun object given a gun name.
        /// </summary>
        /// <param name="assetName">The name of the gun in the file structure</param>
        public override void LoadContent(string assetName)
        {
            base.mWeaponTexture = Game1.theContentManager.Load<Texture2D>(assetName); //stores the texture
            theWeaponName = assetName; //stores the texture name

            //creates a rectangular bounding box around the gun
            Size = new Rectangle(0, 0, (int)(mWeaponTexture.Width), (int)(mWeaponTexture.Height));
        }

        public override string ToString()
        {
            return "Gun Name: " + theWeaponName;
        }
    }
}
