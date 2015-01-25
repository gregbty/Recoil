using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Recoil_2
{
    public abstract class Weapon : UsuableItem
    {
        protected int damage;
        public int ammo;
        public abstract void Use();
        public bool isPickedUp;
        public string weaponID;
        public float bulletMoveRate;
        public int fireRate;

        public int Damage
        {
            get
            {
                return damage;
            }
        }

        public Weapon(int dmg, int a, Texture2D t, Vector2 p)
            : base(t, p)
        {
            damage = dmg;
            ammo = a;
            isPickedUp = false;
            weaponID = null;
        }

    }
}
