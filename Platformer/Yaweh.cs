using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Mushi_Mushi
{
    class Yahweh
    {
        protected int speed = 0;
        protected Rectangle yawehrec;
        protected Texture2D yawehtext;
        protected Color yawehcolor;
        bool isMoving = false;
        int direction;
        

        #region Getters

        public Boolean getMoving()
        {
            return isMoving;
        }

        public int getSpeed()
        {
            return speed;
        }

        public Rectangle getRec()
        {
            return yawehrec;
        }

        public Texture2D getPic()
        {
            return yawehtext;
        }

        public Color getColor()
        {
            return yawehcolor;
        } 
        #endregion

        #region Setters

        public void setMoving(Boolean aMoving)
        {
            isMoving = aMoving;
        }

        public void setSpeed(int aSpeed)
        {
            speed = aSpeed;
        }

        public void setRec(Rectangle aRec)
        {
            yawehrec = aRec;
        }

        public void setPic(Texture2D aPic)
        {
            yawehtext = aPic;
        }

        public void setColor(Color aColor)
        {
            yawehcolor = aColor;
        } 
        #endregion

        #region Constructor
        public Yahweh(Rectangle aRec, Texture2D aPic, Color aColor, Boolean aMoving, int aSpeed)
        {
            setRec(aRec);
            setPic(aPic);
            setColor(aColor);
            setMoving(aMoving);
            setSpeed(aSpeed);
        }

        #endregion     

        #region Methods
        public void Update(ContentManager manager)
        {

        }

        public void Draw(SpriteBatch canvas)
        {
            canvas.Begin();
            canvas.Draw(yawehtext, yawehrec, yawehcolor);
            canvas.End();
        }

        public void Move(KeyboardState keys)
        {
            if (keys.IsKeyDown(Keys.Right))
            {
                yawehrec.X += (int)(speed * +2);
            }
            if (keys.IsKeyDown(Keys.Left))
            {
                yawehrec.X += (int)(speed * -2);
            }
            if (keys.IsKeyDown(Keys.Down))
            {
                yawehrec.Y += (int)(speed * +2);
            }
            if (keys.IsKeyDown(Keys.Up))
            {
                yawehrec.Y += (int)(speed * -2);
            }
        }

        public void Move(GamePadState pads , ContentManager manager)
        {
            if (pads.ThumbSticks.Left.X > 0 || pads.ThumbSticks.Left.Y < 0 || pads.ThumbSticks.Left.X < 0 || pads.ThumbSticks.Left.Y > 0)
            {
                direction = 1;
                if (Math.Abs(pads.ThumbSticks.Left.X) > Math.Abs(pads.ThumbSticks.Left.Y))
                {
                    yawehtext = manager.Load<Texture2D>("MaincharacterRight");
                }
                else
                {
                    yawehtext = manager.Load<Texture2D>("MaincharacterDown");
                }
                yawehrec.X += (int)(speed * pads.ThumbSticks.Left.X) * 2;
                yawehrec.Y -= (int)(speed * pads.ThumbSticks.Left.Y) * 2;
            }

            if (pads.ThumbSticks.Left.X < 0 || pads.ThumbSticks.Left.Y > 0)
            {
                direction = 2;

                if (Math.Abs(pads.ThumbSticks.Left.X) > Math.Abs(pads.ThumbSticks.Left.Y))
                {
                    yawehtext = manager.Load<Texture2D>("MaincharacterLeft");
                }

                else
                {
                    yawehtext = manager.Load<Texture2D>("MaincharacterForward");
                }
                yawehrec.X += (int)(speed * pads.ThumbSticks.Left.X) * 2;
                yawehrec.Y -= (int)(speed * pads.ThumbSticks.Left.Y) * 2;
            }

            

        }
        

      
        #endregion




    }
}
