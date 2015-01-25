using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// Used menu from my game project in CSE 1301

namespace Recoil_2
{
    class Menu
    {
        private static int MAX = 6;
        private int menuItemCount;
        private int curMenuItem;
        private string[] menuItems;
        private Vector2[] pos;
        private double[] scale;
        private Color unselected;
        private Color selected;
        private SpriteFont font;

        public Menu(Color unslectedColor, Color selectedColor, SpriteFont sp)
        {
            font = sp;
            menuItems = new string[MAX];
            pos = new Vector2[MAX];
            scale = new double[MAX];
            unselected = unslectedColor;
            selected = selectedColor;
            menuItemCount = 0;
            curMenuItem = 0;
        }

        public void addMenuItem(string name, Vector2 p)
        {
            if (menuItemCount < MAX)
            {
                menuItems[menuItemCount] = name;
                scale[menuItemCount] = 1.0f;
                pos[menuItemCount++] = p;
            }
        }

        public void selectNext()
        {
            if (curMenuItem < menuItemCount - 1)
            {
                curMenuItem++;
            }
            else
            {
                curMenuItem = 0;
            }
        }

        public void selectPrev()
        {
            if (curMenuItem > 0)
            {
                curMenuItem--;
            }
            else
            {
                curMenuItem = menuItemCount - 1;
            }
        }
        public int getSelectedNum()
        {
            return curMenuItem;
        }

        public string getSelectedName()
        {
            return menuItems[curMenuItem];
        }

        public void Update(GameTime gameTime)
        {
            for (int x = 0; x < menuItemCount; x++)
            {
                if (x == curMenuItem)
                {
                    if (scale[x] < 2.0f)
                    {
                        scale[x] += 0.04 + 10.0f * gameTime.ElapsedGameTime.Seconds;
                    }
                }
                else if (scale[x] > 1.0f && x != curMenuItem)
                {
                    scale[x] -= 0.04 + 10.0f * gameTime.ElapsedGameTime.Seconds;
                }
            }
        }

        bool menuUp = false;
        bool menuDown = false;
        public void getUserInput(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && menuUp)
            {
                selectPrev();
                menuUp = false;
            }
            else if (keys.IsKeyUp(Keys.Up))
            {
                menuUp = true;
            }
            if (keys.IsKeyDown(Keys.Down) && menuDown)
            {
                selectNext();
                menuDown = false;
            }
            else if (keys.IsKeyUp(Keys.Down))
            {
                menuDown = true;
            }
            Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < menuItemCount; x++)
            {
                if (x == curMenuItem)
                {
                    Vector2 p = pos[x];
                    p.X -= (float)(22 * scale[x] / 2);
                    p.Y -= (float)(22 * scale[x] / 2);
                    spriteBatch.DrawString(font,
                                       menuItems[x],
                                       p,
                                       selected,
                                       0.0f,
                                       new Vector2(0, 0),
                                       (float)scale[x],
                                       SpriteEffects.None,
                                       0);
                }
                else
                {
                    Vector2 p = pos[x];
                    p.X -= (float)(22 * scale[x] / 2);
                    p.Y -= (float)(22 * scale[x] / 2);
                    spriteBatch.DrawString(font,
                                       menuItems[x],
                                       p,
                                       unselected,
                                       0.0f,
                                       new Vector2(0, 0),
                                       (float)scale[x],
                                       SpriteEffects.None,
                                       0);
                }
            }
        }
    }
}