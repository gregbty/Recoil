using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Recoil_2
{
    public class UsuableItem
    {
        protected Texture2D texture;
        public Vector2 position;

        public Texture2D Texture
        {
            get
            {
                return texture;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public UsuableItem(Texture2D t, Vector2 p)
        {
            texture = t;
            position = p;
        }
    }
}
