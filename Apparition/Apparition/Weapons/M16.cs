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
    [Serializable()]
    public class M16 : Gun
    {
        /// <summary>
        /// Creates an M16 with basic parameters.
        /// </summary>
        public M16()
        {
            base.LoadContent("pictures/M16");
            base.DamagePerAttack = 10;
        }

        /// <summary>
        /// Creates a new bullet object on screen.
        /// </summary>
        public override void Shoot()
        {
            //bullet 1
            Bullet b1 = Hero.sBullets.peek();
            Hero.sBullets.Pop();
            b1.Position.Y = b1.Position.Y - 10;
            b1.LoadContent(weaponAngle, Position, DamagePerAttack);
            //bullet 2
            Vector2 Position2;
            Position2.X = Position.X;
            Position2.Y = Position.Y - 50;
            Bullet b2 = Hero.sBullets.peek();
            Hero.sBullets.Pop();
            b2.LoadContent(weaponAngle + 0.3f, Position2, DamagePerAttack);
            //bullet 3
            Vector2 Position3;
            Position3.X = Position.X;
            Position3.Y = Position.Y + 30;
            Bullet b3 = Hero.sBullets.peek();
            Hero.sBullets.Pop();
            b3.LoadContent(weaponAngle - 0.3f, Position3, DamagePerAttack);

            Hero.lBullets.Add(b1);
            Hero.lBullets.Add(b2);
            Hero.lBullets.Add(b3);
        }
    }
}
