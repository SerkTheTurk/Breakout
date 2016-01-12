using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Breakout
{
    class Brick
    {
        Texture2D texture;
        Vector2 position;
        public static Rectangle rect;
        bool drawBrick;

        public Brick(Vector2 position)
        {
            texture = null;
            this.position = position;
        }

        public void LoadContent(ContentManager Content)
        {
            texture = Content.Load<Texture2D>("brick");
            drawBrick = true;
            
        }

        public void Update(GameTime gameTime)
        {
            if (drawBrick)
            {
                rect = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }

            if (CollidesWith(rect, Ball.ballRect))
            {
                drawBrick = false;
                Destroy();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (drawBrick)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }

        public void Destroy()
        {
            texture = null;
            position = Vector2.Zero;
        }

        public bool CollidesWith(Rectangle rect1, Rectangle rect2)
        {
            if (rect1.Intersects(rect2))
            {
                return true;
            }
            return false;
        }
    }
}
