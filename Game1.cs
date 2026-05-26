using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D backTexture, blockTexture, circleTexture, lineTexture, ballTexture;
        Rectangle window, blockRect, blockRect1, circleRect, lineRect, ballRect;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 1000, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            ballRect = new Rectangle(477,276,50,50);
            lineRect = new Rectangle(498,0,10,600);
            circleRect = new Rectangle(452,250,100,100);
            blockRect = new Rectangle(10,225,20,150);
            blockRect1 = new Rectangle(970, 225, 20, 150);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("Ball (1)");
            backTexture = Content.Load<Texture2D>("Blue vs Red");
            blockTexture = Content.Load<Texture2D>("White box");
            circleTexture = Content.Load<Texture2D>("Circle_ (1)");
            lineTexture = Content.Load<Texture2D>("Line (1)");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            _spriteBatch.Draw(backTexture, window, Color.White);
            _spriteBatch.Draw(blockTexture, blockRect, Color.White);
            _spriteBatch.Draw(blockTexture, blockRect1, Color.White);
            _spriteBatch.Draw(circleTexture, circleRect, Color.White);
            _spriteBatch.Draw(lineTexture, lineRect, Color.White);
            _spriteBatch.Draw(ballTexture, ballRect, Color.White);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
