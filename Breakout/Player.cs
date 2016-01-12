using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    class Player
    {
        Texture2D texture;
        public static Vector2 playerPosition;
        float speed;
        public static Rectangle paddleRect;

        public Player()
        {
            texture = null;
            playerPosition = new Vector2(425, 500);
            speed = 4f;

        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("paddle");
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            paddleRect = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, texture.Width, texture.Height);

            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D))
            {
                if (playerPosition.X + texture.Width >= 960)
                {
                    playerPosition.X += 0;
                }
                else
                {
                    playerPosition.X += speed;
                }
            }
            if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A))
            {
                if (playerPosition.X <= 0)
                {
                    playerPosition.X += 0;
                }
                else
                {
                    playerPosition.X -= speed;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, playerPosition, Color.White);
        }
    }
}
