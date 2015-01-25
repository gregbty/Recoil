
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Recoil_2
{
    public class Splicer : Weapon
    {
        public Splicer(int dmg, int a, Texture2D t, Vector2 p)
            : base(dmg, a, t, p)
        {
            isPickedUp = false;
            weaponID = "Splicer";
        }

        public override void Use()
        {
            fireRate = 100;
            bulletMoveRate = 50;
            ammo--;
        }
    }
}


