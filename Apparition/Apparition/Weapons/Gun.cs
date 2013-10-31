using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace DesktopBattle
{
    abstract class Gun : Weapon
    {
        public string theWeaponName; //weapon name
        public Rectangle Size; //the size of the weapon
        protected int DamagePerBullet; //how much damage the bullet does
        MouseState oldMouse; //state of the mouse on the previous frame
        public abstract void Shoot(MouseState aCurrentMouseState);

    }
}
