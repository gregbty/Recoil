using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Recoil_2
{
    class Sloth : Enemy
    {
        public Sloth(Texture2D t, Vector2 p, int hp)
            : base(t, p, hp)
        {
            id = "sloth";
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
                        position.X += 5;
                        Animate();
                    }

                    else
                    {
                        position.X -= 5;
                        Animate();
                    }
                }
            }
        }

        public override void Animate()
        {
            if (isMoving == true)
            {
                if (cellIndex < 7)
                    cellIndex++;

                else
                    cellIndex = 4;
            }

            else
            {
                cellIndex = 8;
            }
        }
    }
}
