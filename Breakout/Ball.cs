using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    enum BallState
    {
        MOVING, STILL
    }

    class Ball
    {
        Texture2D texture;
        Vector2 position;
        public float speed = 5f;
        public static Rectangle ballRect;
        int ballDir;

        BallState bs;

        public Ball()
        {
            texture = null;
            position = new Vector2(470, 485);
            bs = BallState.STILL;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("ball");
        }

        public void Update(GameTime gameTime)
        {
            ballRect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            KeyboardState ks = Keyboard.GetState();

            if (bs == BallState.STILL)
            {
                if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D))
                {
                    ballDir = 2;
                    bs = BallState.MOVING;
                }
                if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A))
                {
                    ballDir = 1;
                    bs = BallState.MOVING;
                }
            }

            if (bs == BallState.MOVING)
            {
                if (ballDir == 1)
                {
                    if (position.X <= 0)
                    {
                        ballDir = 2;
                    }
                    if (position.Y <= 0)
                    {
                        ballDir = 3;
                    }
                    position.X -= speed;
                    position.Y -= speed;
                }
                if (ballDir == 2)
                {
                    if (position.X + texture.Width >= 960)
                    {
                        ballDir = 1;
                    }
                    if (position.Y <= 0)
                    {
                        ballDir = 4;
                    }
                    position.X += speed;
                    position.Y -= speed;
                }
                if (ballDir == 3)
                {
                    if (position.X <= 0)
                    {
                        ballDir = 4;
                    }
                    if (position.Y >= 540)
                    {
                        resetBall();
                    }
                    position.X -= speed;
                    position.Y += speed;
                }
                if (ballDir == 4)
                {
                    if (position.X + texture.Height >= 960)
                    {
                        ballDir = 3;
                    }
                    if (position.Y >= 540)
                    {
                        resetBall();
                    }
                    position.X += speed;
                    position.Y += speed;
                }
                if (CollidesWith(ballRect, Player.paddleRect))
                {
                    if (ballDir == 3)
                    {
                        ballDir = 1;
                        position.X -= speed;
                        position.Y -= speed;
                    }
                    if (ballDir == 4)
                    {
                        ballDir = 2;
                        position.X += speed;
                        position.Y -= speed;
                    }
                }

                if (bottomCollide(ballRect, Brick.rect))
                {
                    if (ballDir == 3)
                    {
                        ballDir = 1;
                    }
                    if (ballDir == 4)
                    {
                        ballDir = 2;
                    }
                }

                if (topCollide(ballRect, Brick.rect))
                {
                    if (ballDir == 1)
                    {
                        ballDir = 3;
                    }
                    if (ballDir == 2)
                    {
                        ballDir = 4;
                    }
                }
            }


            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public bool CollidesWith(Rectangle rect1, Rectangle rect2)
        {
            if (rect1.Intersects(rect2))
            {
                return true;
            }
            return false;
        }

        public bool bottomCollide(Rectangle rect1, Rectangle rect2)
        {
            if (rect1.X >= rect2.X && rect1.X + rect1.Width <= rect2.X + rect2.Width)
            {
                if (rect1.Y + rect1.Height >= rect2.Y && rect1.Y + rect1.Height <= rect2.Y + rect2.Height)
                {
                    return true;
                }
            }
            return false;
        }

        public bool topCollide(Rectangle rect1, Rectangle rect2)
        {
            if (rect1.X >= rect2.X && rect1.X + rect1.Width <= rect2.X + rect2.Width)
            {
                if (rect1.Y <= rect2.Y + rect2.Height && rect1.Y >= rect2.Y)
                {
                    return true;
                }
            }
            return false;
        }

        public void resetBall()
        {
            position.X = Player.playerPosition.X + 47;
            position.Y = 480;
            bs = BallState.STILL;
        }
    }
}
