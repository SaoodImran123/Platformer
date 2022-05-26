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
    class Mushi:Yahweh
    {
       
        
        
        
        public Mushi(Rectangle aRec, Texture2D aPic, Color aColor, Boolean aMoving, int aSpeed):base(aRec, aPic, aColor, aMoving, aSpeed)
        {

        }

        public void MoveTowardsYaweh(Yahweh player)
        {

  
                if(yawehrec.X > player.getRec().X)
                {
                    yawehrec.X -= 1;
                }
                if (yawehrec.X < player.getRec().X)
                {
                    yawehrec.X += 1;
                }

                if (yawehrec.Y > player.getRec().Y)
                {
                    yawehrec.Y -= 1;
                }

                if (yawehrec.Y < player.getRec().Y)
                {
                    yawehrec.Y += 1;
                }
        }
        

    }
}
