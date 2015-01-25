using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Recoil_2
{
    public class Player : Avatar
    {
        public List<Weapon> weapons;
        private int weaponIndex;
        private Weapon currentWeapon;
        public Weapon CurrentWeapon
        {
            get
            {
                return currentWeapon;
            }
            set
            {
                currentWeapon = value;
            }
        }

        public int lives;
        public bool isAttacking;
        public bool isRunning;
        public bool isFacingLeft;

        public Player(Texture2D t, Vector2 p, int hp)
            : base(t, p, hp)
        {
            weapons = new List<Weapon>();
            weaponIndex = 0;
            lives = 3;
        }

        public void AddWeapon(Weapon w)
        {
            // Define Collision Detection
            weapons.Add(w);
        }

        public void ActiveWeapon(int i)
        {
            if (i < weapons.Count)
                currentWeapon = weapons[i];
        }

        public void NextWeapon()
        {
            if (weaponIndex != -1)
            {
                weaponIndex++;
                if (weaponIndex >= weapons.Count)
                    weaponIndex = 0;
                currentWeapon = weapons[weaponIndex];
            }
        }

        public void PreviousWeapon()
        {
            if (weaponIndex != -1)
            {
                weaponIndex--;
                if (weaponIndex < 0)
                    weaponIndex = weapons.Count - 1;
                currentWeapon = weapons[weaponIndex];
            }
        }

        public override void Attack()
        {
            if (CurrentWeapon != null)
                CurrentWeapon.Use();
        }

        public override void Animate()
        {
            if (cellIndex < 10)
                cellIndex++;
            else
                cellIndex = 4;
        }

        public void AnimateHead()
        {
            if (cellIndex < 3)
                cellIndex++;
            else

                cellIndex = 0;
        }

        public void AnimateJump()
        {
            //Animate Player Jumping
        }
    }
}
