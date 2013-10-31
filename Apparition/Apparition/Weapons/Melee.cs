using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopBattle
{
    abstract class Melee : Weapon
    {
        protected int DamagePerHit;
        public abstract void Swing();
    }
}
