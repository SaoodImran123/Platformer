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
    class Bullet
    {
        protected int speed = 1;
        protected int direction;
        protected Rectangle bulletrec;
        protected Texture2D bullettext;
        protected Color bulletcolor;
        protected bool bulletismoving;


        //getters
        #region Getters
        public int getSpeed()
        {
            return speed;
        }

        public Rectangle getRec()
        {
            return bulletrec;
        }

        public Texture2D getPic()
        {
            return bullettext;
        }

        public Color getColor()
        {
            return bulletcolor;
        }  

        public bool getIsMoving()
        {
            return bulletismoving;
        }

        public int getDirection()
        {
            return direction;
        }
        #endregion

        //setters
        #region Setters
        public void setSpeed(int aSpeed)
        {
            speed = aSpeed;
        }

        public void setRec(Rectangle aRec)
        {
            bulletrec = aRec;
        }

        public void setPic(Texture2D aPic)
        {
            bullettext = aPic;
        }

        public void setColor(Color aColor)
        {
            bulletcolor = aColor;
        } 

        public void setIsMoving(bool aisMoving)
        {
            bulletismoving = aisMoving;
        }

        public void setDirection(int aDirection)
        {
            direction = aDirection;
        }
        #endregion 


        //constructor
        public Bullet(Rectangle aRec, Texture2D aPic, Color aColor, bool aisMoving , int aDirection,int aSpeed)
        {
            setRec(aRec);
            setPic(aPic);
            setColor(aColor);
            setIsMoving(aisMoving);
            setSpeed(aSpeed);
            setDirection(aDirection);
        }

        public void BulletMove()
        {
            if (bulletismoving == true)
            {

                if (direction == 1)
                {
                    bulletrec.X -= 2 * speed;
                }

                if (direction == 2)
                {
                    bulletrec.Y -= 2 * speed;
                }

                if (direction == 3)
                {
                    bulletrec.X += 2 * speed;
                }

                if (direction == 4)
                {
                    bulletrec.Y += 2 * speed;
                }
            }
        }


        public void Draw(SpriteBatch bulletdraw)
        {
            bulletdraw.Draw(bullettext, bulletrec, bulletcolor);
        }


    }
}
