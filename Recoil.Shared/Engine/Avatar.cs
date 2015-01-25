using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Recoil_2
{
    public abstract class Avatar
    {
        protected Texture2D texture;
        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }
        public Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }

        }

        public int health_points;
        public int cellIndex;

        public abstract void Attack();
        public abstract void Animate();

        public Avatar(Texture2D t, Vector2 p, int hp)
        {
            texture = t;
            position = p;
            health_points = hp;
            cellIndex = 0;
        }
    }
}
