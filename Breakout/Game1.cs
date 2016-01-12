using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Breakout
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D brickTexture;

        Player player = new Player();
        Brick[,] bricks;
        public static Rectangle brickRect;

        Ball ball = new Ball();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 540;
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            brickTexture = Content.Load<Texture2D>("brick");

            player.LoadContent(Content);

            bricks = new Brick[10, 10];
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    brickRect = new Rectangle(x * brickTexture.Width, y * brickTexture.Height, brickTexture.Width, brickTexture.Height);
                    bricks[x, y] = new Brick(new Vector2(x * brickTexture.Width, y * brickTexture.Height));
                }
            }
            foreach (Brick brick in bricks)
            {
                brick.LoadContent(Content);
            }
            ball.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            player.Update(gameTime); 
            ball.Update(gameTime);
            foreach (Brick brick in bricks)
            {
                brick.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            player.Draw(spriteBatch);
            foreach (Brick brick in bricks)
            {
                brick.Draw(spriteBatch);
            }
            ball.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
