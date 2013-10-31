﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
#endregion

namespace DesktopBattle
{
    /// <summary>
    /// Makes a pistol weapon.
    /// </summary>
    class Pistol : Gun
    {
        /// <summary>
        /// Creates a pistol with basic parameters.
        /// </summary>
        public Pistol(ContentManager theContentManager, GraphicsDeviceManager graphics)
        {
            mContentManager = theContentManager;
            base.LoadContent("pictures/pistol");
            base.DamagePerBullet = 10;
            graphicsManager = graphics;
        }

        /// <summary>
        /// Creates a new bullet object on screen.
        /// </summary>
        public override void Shoot()
        {
            lBullets.Add(new Bullet(mContentManager, graphicsManager, gunAngle, Position));
        }
    }
}
