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
    public class Shotgun : Gun
    {
        /// <summary>
        /// Creates an M16 with basic parameters.
        /// </summary>
        public Shotgun()
        {
            base.LoadContent("pictures/shotgun");
            base.DamagePerAttack = 10;
        }

        /// <summary>
        /// Creates a new bullet object on screen.
        /// </summary>
        public override void Shoot()
        {
            shootSound.Play();
            //bullet 1
            Bullet b1 = Hero.sBullets.peek();
            Hero.sBullets.Pop();
            b1.Position.Y = b1.Position.Y - 10;
            b1.LoadContent(weaponAngle, Position, DamagePerAttack);
            //bullet 2
            Vector2 Position2;
            Position2.X = Position.X;
            Position2.Y = Position.Y - 30;
            Bullet b2 = Hero.sBullets.peek();
            Hero.sBullets.Pop();
            b2.LoadContent(weaponAngle + 0.15f, Position2, DamagePerAttack);
            //bullet 3
            Vector2 Position3;
            Position3.X = Position.X;
            Position3.Y = Position.Y + 10;
            Bullet b3 = Hero.sBullets.peek();
            Hero.sBullets.Pop();
            b3.LoadContent(weaponAngle - 0.15f, Position3, DamagePerAttack);
            //bullet 4
            Vector2 Position4;
            Position4.X = Position.X;
            Position4.Y = Position.Y - 50;
            Bullet b4 = Hero.sBullets.peek();
            Hero.sBullets.Pop();
            b4.LoadContent(weaponAngle + 0.3f, Position2, DamagePerAttack);
            //bullet 5
            Vector2 Position5;
            Position5.X = Position.X;
            Position5.Y = Position.Y + 30;
            Bullet b5 = Hero.sBullets.peek();
            Hero.sBullets.Pop();
            b5.LoadContent(weaponAngle - 0.3f, Position3, DamagePerAttack);

            Hero.lBullets.Add(b1);
            Hero.lBullets.Add(b2);
            Hero.lBullets.Add(b3);
            Hero.lBullets.Add(b4);
            Hero.lBullets.Add(b5);
        }
    }
}
