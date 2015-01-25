using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Recoil_2
{
    public class Pistol : Weapon
    {

        public Pistol(int dmg, int a, Texture2D t, Vector2 p)
            : base(dmg, a, t, p)
        {
            isPickedUp = true;
            weaponID = "Pistol";

        }

        public override void Use()
        {
            fireRate = 600;
            bulletMoveRate = 10;
            return;

        }
    }
}
