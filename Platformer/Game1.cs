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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        //GAMESTATE
        int gamestate = 1;

        //FOR MAIN CHARACTER
        #region Main Character
        Rectangle playerRec = new Rectangle(10, 10, 100, 100);
        Color playerColor = Color.White;
        Texture2D LeftGod, RightGod, UpGod, DownGod;
        Yahweh maincharacter;
        GamePadState pad;
        GamePadState oldpad;
        KeyboardState key;
        #endregion

        //BULLETS
        Rectangle bulletrec;
        Texture2D bulletpic;
        int bulletdirection;
        int bulletnumber;
        List<Bullet> bullets = new List<Bullet>();


        //FOR ENEMIES
        #region Mushi's
        Rectangle[] mushiRec = new Rectangle[30];
        Color mushiColor = Color.White;
        Texture2D mushiPic;
        Mushi[] enemymushi = new Mushi[15];
        Random sidegen = new Random();
        int sidenumber;
        int positionintx;
        int positioninty;
        int mushixrec = 50;
        int mushiyrec = 50;
        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 950;
            graphics.PreferredBackBufferWidth = 1250;
        }                              


        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Main Character
            #region Main character

            LeftGod = Content.Load<Texture2D>("MaincharacterLeft");
            maincharacter = new Yahweh(playerRec, LeftGod, playerColor, true, 5);
            #endregion
            //bullet picture
            bulletpic = Content.Load<Texture2D>("bullet");

            mushiPic = this.Content.Load<Texture2D>("mushi");
            Addenemy();



        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            pad = GamePad.GetState(PlayerIndex.One);
            key = Keyboard.GetState();
            maincharacter.Move(key);
            maincharacter.Move(pad, Content);

            if (oldpad.Buttons.Y == ButtonState.Released && pad.Buttons.Y == ButtonState.Pressed)
            {
                Shoot();
            }

            //bullets get shot
            for (int i = 0; i < bullets.Count; i++)
            {
                bullets[i].BulletMove();
            }
            
            //checks if yahweh has died
            for (int i = 0; i < enemymushi.Length; i++)
            {
                if (enemymushi[i].getRec().Intersects(maincharacter.getRec()))
                {
                    
                    break;
                }
            }


            //mushies move to yahweh
            for (int i = 0; i < enemymushi.Length; i++)
            {
                enemymushi[i].MoveTowardsYaweh(maincharacter);
            }

            //checks if bullets hit an enemy
            for(int i = 0; i < bullets.Count; i++)
            {
                for (int j = 0; j < enemymushi.Length; j++)
                {
                    if (bullets[i].getRec().Intersects(enemymushi[j].getRec()))
                    {
                        bulletnumber = j;
                        bullets.RemoveAt(i);
                        Unspawn();
                        break;
                    }
                }
            }


            //oldpad is set to current pad
            oldpad = pad;
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //drawing maincharacter
            maincharacter.Draw(spriteBatch);

            //drawing mushi's
            #region Drawing Bullets and Mushis
            spriteBatch.Begin();

            //draws enemy mushies
            for (int i = 0; i < enemymushi.Length; i++)
            {
                spriteBatch.Draw(enemymushi[i].getPic(), enemymushi[i].getRec(), Color.White);
            }
            //spawn the list of bullets
            for (int i = 0; i < bullets.Count; i++ )
            {
                bullets[i].Draw(spriteBatch);
            }

           spriteBatch.End(); 
            #endregion

            base.Draw(gameTime);
        }


        #region AddEnemy
        public void Addenemy()
        {


            for (int i = 0; i < enemymushi.Length; i++)
            {

                sidenumber = sidegen.Next(0, 4);
                positioninty = sidegen.Next(0, 921);
                positionintx = sidegen.Next(0, 1221);

                //0 means spawn from the left
                if (sidenumber == 0)
                {
                    mushiRec[i] = new Rectangle(GraphicsDevice.Viewport.Width - (GraphicsDevice.Viewport.Width + mushixrec + 1), positioninty, mushixrec, mushiyrec);
                    enemymushi[i] = new Mushi(mushiRec[i], mushiPic, mushiColor, true, 5);
                }
                //1 means spawn from the top
                if (sidenumber == 1)
                {
                    mushiRec[i] = new Rectangle(positionintx, GraphicsDevice.Viewport.Height - (GraphicsDevice.Viewport.Height + mushiyrec + 1), mushixrec, mushiyrec);
                    enemymushi[i] = new Mushi(mushiRec[i], mushiPic, mushiColor, true, 5);
                }
                //2 means spawn from the right
                if (sidenumber == 2)
                {
                    mushiRec[i] = new Rectangle(GraphicsDevice.Viewport.Width + mushixrec + 1, positioninty, mushiyrec, mushiyrec);
                    enemymushi[i] = new Mushi(mushiRec[i], mushiPic, mushiColor, true, 5);
                }
                //3 means spawn from the bottom
                if (sidenumber == 3)
                {
                    mushiRec[i] = new Rectangle(positionintx, GraphicsDevice.Viewport.Height + (mushiyrec + 1), mushixrec, mushiyrec);
                    enemymushi[i] = new Mushi(mushiRec[i], mushiPic, mushiColor, true, 5);
                }
            }

        } 
        #endregion

        #region SpawnBullets
        public void Shoot()
        {
            if (pad.ThumbSticks.Left.Y > 0 || maincharacter.getPic() == Content.Load<Texture2D>("MaincharacterForward"))
            {
                bulletdirection = 2;
            }

            if (pad.ThumbSticks.Left.Y < 0 || maincharacter.getPic() == Content.Load<Texture2D>("MaincharacterDown"))
            {
                bulletdirection = 4;
            }

            if (pad.ThumbSticks.Left.X > 0 || maincharacter.getPic() == Content.Load<Texture2D>("MaincharacterRight"))
            {
                bulletdirection = 3;
            }

            if (pad.ThumbSticks.Left.X < 0 || maincharacter.getPic() == Content.Load<Texture2D>("MaincharacterLeft"))
            {
                bulletdirection = 1;
            }
            bulletrec = new Rectangle(maincharacter.getRec().X, maincharacter.getRec().Y, 10, 10);
            bullets.Add(new Bullet(bulletrec, bulletpic, playerColor, true, bulletdirection, 6));

        } 
        #endregion

        public void Unspawn()
        {

          

                sidenumber = sidegen.Next(0, 4);
                positioninty = sidegen.Next(0, 921);
                positionintx = sidegen.Next(0, 1221);

                //0 means spawn from the left
                if (sidenumber == 0)
                {
                    mushiRec[bulletnumber] = new Rectangle(GraphicsDevice.Viewport.Width - (GraphicsDevice.Viewport.Width + mushixrec + 1), positioninty, mushixrec, mushiyrec);
                    enemymushi[bulletnumber] = new Mushi(mushiRec[bulletnumber], mushiPic, mushiColor, true, 5);
                }
                //1 means spawn from the top
                if (sidenumber == 1)
                {
                    mushiRec[bulletnumber] = new Rectangle(positionintx, GraphicsDevice.Viewport.Height - (GraphicsDevice.Viewport.Height + mushiyrec + 1), mushixrec, mushiyrec);
                    enemymushi[bulletnumber] = new Mushi(mushiRec[bulletnumber], mushiPic, mushiColor, true, 5);
                }
                //2 means spawn from the right
                if (sidenumber == 2)
                {
                    mushiRec[bulletnumber] = new Rectangle(GraphicsDevice.Viewport.Width + mushixrec + 1, positioninty, mushiyrec, mushiyrec);
                    enemymushi[bulletnumber] = new Mushi(mushiRec[bulletnumber], mushiPic, mushiColor, true, 5);
                }
                //3 means spawn from the bottom
                if (sidenumber == 3)
                {
                    mushiRec[bulletnumber] = new Rectangle(positionintx, GraphicsDevice.Viewport.Height + (mushiyrec + 1), mushixrec, mushiyrec);
                    enemymushi[bulletnumber] = new Mushi(mushiRec[bulletnumber], mushiPic, mushiColor, true, 5);
                }
            
        }

        
    }
}
