using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Recoil_2
{
    public abstract class Enemy : Avatar
    {
        public string id;
        public bool isAttacking;
        public bool isFacingLeft;
        public bool isMoving;
        public double timeSinceLastAttack;
        public double timeSinceLastUpdate;

        public Enemy(Texture2D t, Vector2 p, int hp)
            : base(t, p, hp)
        {
            timeSinceLastUpdate = 0;
            timeSinceLastAttack = 0;

            isAttacking = false;
            isFacingLeft = true;
            isMoving = false;
        }

        public abstract override void Attack();
        public abstract void Move();
        public abstract override void Animate();
    }
}