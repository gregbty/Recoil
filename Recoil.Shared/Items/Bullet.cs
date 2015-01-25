using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Recoil_2
{
    public class Bullet : UsuableItem
    {
        public Vector2 velocity;

        public Bullet(Texture2D t, Vector2 p)
            : base(t, p)
        {
            velocity = new Vector2() ;
        }

        public void Move()
        {
            position += velocity;
        }

    }
}
