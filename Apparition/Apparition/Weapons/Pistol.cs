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

    public class Pistol : Gun
    {
        /// <summary>
        /// Creates a pistol with basic parameters.
        /// </summary>
        public Pistol()
        {
            base.LoadContent("pictures/pistol");
            base.DamagePerAttack = 10;
        }

        /// <summary>
        /// Creates a new bullet object on screen.
        /// </summary>
        public override void Shoot()
        {
            shootSound.Play();
            Bullet b = Hero.sBullets.peek();
            Hero.sBullets.Pop();
            b.LoadContent(base.weaponAngle, base.Position, DamagePerAttack);
            Hero.lBullets.Add(b);
        }
    }
}
