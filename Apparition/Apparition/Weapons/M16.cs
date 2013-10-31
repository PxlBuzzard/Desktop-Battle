#region Using Statements
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
    class M16 : Gun
    {
        /// <summary>
        /// Creates an M16 with basic parameters.
        /// </summary>
        public M16(ContentManager theContentManager, GraphicsDeviceManager graphics)
        {
            mContentManager = theContentManager;
            base.LoadContent("pictures/M16");
            base.DamagePerBullet = 10;
            graphicsManager = graphics;
        }

        /// <summary>
        /// Creates a new bullet object on screen.
        /// </summary>
        public override void Shoot()
        {
            Vector2 Position2;
            Position2.X = Position.X;
            Position2.Y = Position.Y - 40;
            Vector2 Position3;
            Position3.X = Position.X;
            Position3.Y = Position.Y + 40;
            lBullets.Add(new Bullet(mContentManager, graphicsManager, gunAngle, Position));
            lBullets.Add(new Bullet(mContentManager, graphicsManager, gunAngle, Position2));
            lBullets.Add(new Bullet(mContentManager, graphicsManager, gunAngle, Position3));
        }
    }
}
