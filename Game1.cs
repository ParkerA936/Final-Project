using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace Final_Project
{
    enum Screen
    {
        //Defined the screens
        Intro,
        Menu,
        Gameplay,
        End
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Screen screen;
        Texture2D backTexture, blockTexture, circleTexture, lineTexture, ballTexture, introTexture;
        Texture2D tgtr;
        Rectangle window, blockRect, blockRect1, circleRect, lineRect, ballRect, twoPlayRect, quitRect;
        Vector2 ballSpeed;
        Random gen;
        MouseState mouseState, prevMouseState;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.Title = "Lesson 4 - Sound and Time";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            window = new Rectangle(0, 0, 1000, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            ballSpeed = new Vector2(0, 0);

            twoPlayRect = new Rectangle(436, 366, 126, 29);
            quitRect = new Rectangle(435, 404, 126, 29);
            ballRect = new Rectangle(477, 276, 50, 50);
            lineRect = new Rectangle(498, 0, 10, 600);
            circleRect = new Rectangle(452, 250, 100, 100);
            blockRect = new Rectangle(10, 225, 20, 150);
            blockRect1 = new Rectangle(970, 225, 20, 150);
            base.Initialize();
            gen = new Random();

        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("Ball (1)");
            backTexture = Content.Load<Texture2D>("Blue vs Red");
            blockTexture = Content.Load<Texture2D>("White box");
            circleTexture = Content.Load<Texture2D>("Circle_ (1)");
            lineTexture = Content.Load<Texture2D>("Line (1)");
            introTexture = Content.Load<Texture2D>("PongIntro");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            this.Window.Title = mouseState.Position.ToString();
            if (screen == Screen.Intro)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && twoPlayRect.Contains(mouseState.Position))
                {
                    screen = Screen.Gameplay;
                }
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && quitRect.Contains(mouseState.Position))
                {
                    Exit();
                }
            }
            int start;
            start = gen.Next(1);
            int right = 0, left = 1;

            if (screen == Screen.Gameplay)
            {
                if (ballRect.Intersects(blockRect)|| ballRect.Intersects(blockRect1))
                {
                 
                    ballSpeed.X *= -1;
                }
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {

                }
                if (start == right)
                {
                    ballRect.X += (int)ballSpeed.X;
                    ballSpeed.X = 2;
                }
                else if (start == left)
                {
                    ballRect.X -= (int)ballSpeed.X;
                    ballSpeed.X = -2;
                    // ballRect.Y += (int)ballSpeed.Y;
                }
                // ballRect.Y += (int)ballSpeed.Y;
            }
            
            base.Update(gameTime);
        }


                // TODO: Add your update logic here

              
     

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introTexture, window, Color.White);
            }
                if (screen == Screen.Gameplay)
            {
                _spriteBatch.Draw(backTexture, window, Color.White);
                _spriteBatch.Draw(blockTexture, blockRect, Color.White);
                _spriteBatch.Draw(blockTexture, blockRect1, Color.White);
                _spriteBatch.Draw(circleTexture, circleRect, Color.White);
                _spriteBatch.Draw(lineTexture, lineRect, Color.White);
                _spriteBatch.Draw(ballTexture, ballRect, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
