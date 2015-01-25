using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Recoil_2
{
    class Zombie : Enemy
    {
        public Zombie(Texture2D t, Vector2 p, int hp)
            : base(t, p, hp)
        {
            id = "zombie";
        }

        public override void Attack()
        {
            if (isMoving == false)
            {
                if (timeSinceLastAttack > 100)
                {
                    timeSinceLastAttack = 0;
                    Animate();
                }
            }
        }

        public override void Move()
        {
            if (isAttacking == false)
            {
                if (timeSinceLastUpdate > 250)
                {
                    timeSinceLastUpdate = 0;
                    if (isFacingLeft == false)
                    {
                        position.X += 3;
                        Animate();
                    }

                    else
                    {
                        position.X -= 3;
                        Animate();
                    }
                }
            }
        }

        public override void Animate()
        {
            if (isMoving == true)
            {
                if (cellIndex < 4)
                    cellIndex++;

                else
                    cellIndex = 2;
            }

            else
            {
                if (cellIndex < 7)
                    cellIndex++;
                else
                    cellIndex = 4;
            }
        }
    }
}
