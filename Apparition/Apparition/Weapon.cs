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
    /// Weapon covers projectile and melee based weapons.
    /// 
    /// NOTE: Make this into an abstract class after Milestone 2.
    /// </summary>
    interface Weapon
    {
        List<Bullet> lBullets { get; set; } //list of bullets fired from the gun
        //protected ContentManager mContentManager;
        void LoadContent(string assetName);

        void Shoot();

        void Update(GameTime gameTime, Vector2 heroPosition);

        void Draw(SpriteBatch spriteBatch);

        /*
        public Weapon(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
        }
        */
    }
}
