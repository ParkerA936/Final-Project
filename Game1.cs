using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Net.Security;
using System.Threading;

namespace Final_Project
{
    enum Screen
    {
        //Defined the screens
        Intro,
        Menu,
        Gameplay,
        Pause,
        End
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Screen screen;
        Texture2D backTexture, blockTexture, circleTexture, lineTexture, ballTexture, introTexture;
        Texture2D  pauseTexture, oneTexture, twoTexture, threeTexture, menuTexture;
        Rectangle window, blockRect, blockRect1, circleRect, lineRect, ballRect, twoPlayRect, quitRect;
        Vector2 ballLocation, blockLocation, blockLocation1;
        SpriteFont font, menuFont;
        Vector2 ballSpeed, blockSpeed, blockSpeed1;
        Random gen;
        KeyboardState keyboardState, prevKeyState;
        MouseState mouseState, prevMouseState;
        bool begin, intersect, intersect1, timer, stopwatch1;
        int time;
        int score = 0, score1 = 0, stopwatch = 0;
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

            ballSpeed = new Vector2(0, 2);
            blockSpeed = new Vector2(0, 0);
            blockSpeed1 = new Vector2(0, 0);

            twoPlayRect = new Rectangle(436, 366, 126, 29);
            quitRect = new Rectangle(435, 404, 126, 29);

            ballRect = new Rectangle(477, 276, 50, 50);
            ballLocation = ballRect.Location.ToVector2();

            lineRect = new Rectangle(498, 0, 10, 600);
            circleRect = new Rectangle(452, 250, 100, 100);

            blockRect = new Rectangle(10, 225, 20, 150);
            blockRect1 = new Rectangle(970, 225, 20, 150);

            //blockCRect = new Rectangle(blockRect.X,blockRect.Y,0,150);
            //blockCRect1 = new Rectangle(blockRect.X, blockRect.Y, 0, 150);

            blockLocation = blockRect.Location.ToVector2();
            blockLocation1 = blockRect1.Location.ToVector2();
            
            time = 0;
            intersect1 = true;
            begin = true;
            timer = false;
            intersect = true;
            stopwatch1 = false;
            base.Initialize();
            gen = new Random();

        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ballTexture = Content.Load<Texture2D>("Ball (1)");
            backTexture = Content.Load<Texture2D>("Blue vs Red");
            blockTexture = Content.Load<Texture2D>("White box (1)");
            circleTexture = Content.Load<Texture2D>("Circle_ (1)");
            lineTexture = Content.Load<Texture2D>("Line (1)");
            introTexture = Content.Load<Texture2D>("PongIntro");
            pauseTexture = Content.Load<Texture2D>("pause1");
            oneTexture = Content.Load<Texture2D>("one1");
            twoTexture = Content.Load<Texture2D>("two1"); 
            threeTexture = Content.Load<Texture2D>("three1");
            menuTexture = Content.Load<Texture2D>("menu1");

            menuFont = Content.Load<SpriteFont>("menuFont");
            font = Content.Load<SpriteFont>("Font");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            prevKeyState = keyboardState;
            keyboardState = Keyboard.GetState();
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            blockSpeed.Y = 0;
            blockSpeed1.Y = 0;

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
            else if (screen == Screen.Menu)
            {

            }

            else if (screen == Screen.Pause)

            {
                if (keyboardState.IsKeyDown(Keys.Space) && prevKeyState.IsKeyUp(Keys.Space))
                {
                    stopwatch1 = true;
                }
                if (stopwatch1)
                {
                    stopwatch += 1;

                }

                if (stopwatch >= 240)
                {
                    screen = Screen.Gameplay;
                    stopwatch = 0;
                    stopwatch1 = false;
                }
                if (keyboardState.IsKeyDown(Keys.M) && prevKeyState.IsKeyUp(Keys.M))
                {
                    screen = Screen.Menu;
                }
            }
            else if (screen == Screen.Gameplay)
            {
                if (keyboardState.IsKeyDown(Keys.Space) && prevKeyState.IsKeyUp(Keys.Space))
                {
                    screen = Screen.Pause;


                }
                int start, start1;
                start1 = gen.Next(0, 2);
                start = gen.Next(0, 2);
                int right = 0, left = 1;

                if (start == right && begin)
                {
                    intersect1 = true;
                    intersect = true;
                    ballSpeed.X = 4;

                    if (start1 == 1)
                    {
                        ballSpeed.Y = 4;
                    }
                    if (start1 == 0)
                    {
                        ballSpeed.Y = -4;
                    }


                    begin = false;
                }
                if (start == left && begin)
                {
                    intersect1 = true;
                    intersect = true;

                    ballSpeed.X = -4;

                    if (start1 == 1)
                    {
                        ballSpeed.Y = 4;
                    }
                    if (start1 == 0)
                    {
                        ballSpeed.Y = -4;
                    }
                    begin = false;
                }

                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    blockSpeed1.Y += -5;
                    if (blockRect.Top == 0)
                    {
                        blockSpeed1.Y = 0;
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Down))
                {
                    blockSpeed1.Y += 5;
                }


                if (keyboardState.IsKeyDown(Keys.W))
                {
                    blockSpeed.Y += -5;
                }

                if (keyboardState.IsKeyDown(Keys.S))
                {
                    blockSpeed.Y += 5;
                }

                blockLocation.Y += blockSpeed.Y;
                blockLocation1.Y += blockSpeed1.Y;


                // Horizontal Movement
                ballLocation.X += ballSpeed.X;
                UpdateRectangle();

                //if (ballRect.Intersects(blockRect)|| ballRect.Intersects(blockRect1))
                //{
                //    ballLocation.X -= ballSpeed.X;
                //    UpdateRectangle();
                //    intersect= true;
                //    ballSpeed.X *= -1;
                //    ballSpeed.Y += (blockSpeed.Y/8);
                //}
                if (ballRect.Intersects(blockRect) && intersect)
                {
                    ballLocation.X -= ballSpeed.X;
                    UpdateRectangle();
                    intersect = false;




                    intersect1 = true;
                    ballSpeed.X *= -1;
                    ballSpeed.Y += (blockSpeed.Y / 8);
                }
                else if (ballRect.Intersects(blockRect1) && intersect1)
                {
                    ballLocation.X -= ballSpeed.X;
                    UpdateRectangle();
                    intersect = true;
                    intersect1 = false;
                    ballSpeed.X *= -1;
                    ballSpeed.Y += (blockSpeed.Y / 8);
                }


                //ballSpeed.X *= (3/2);





                ballLocation.Y += ballSpeed.Y;
                UpdateRectangle();

                if (ballRect.Intersects(blockRect) || ballRect.Intersects(blockRect1))
                {

                    // If ball hits paddle 1
                    if (ballLocation.X < window.Center.X)
                    {
                        // Hits Bottom
                        if (ballRect.Center.Y > blockRect.Center.Y)
                        {
                            ballLocation.Y = blockRect.Bottom;
                            UpdateRectangle();
                        }
                        // Hits Top
                        else
                        {
                            ballLocation.Y = blockRect.Top - ballRect.Height;
                            UpdateRectangle();
                        }
                    }

                    // If ball hits paddle 2
                    else if (ballLocation.X > window.Center.X)
                    {
                        if (ballRect.Center.Y > blockRect1.Center.Y)
                        {
                            ballLocation.Y = blockRect1.Bottom;
                            UpdateRectangle();
                        }
                        else
                        {
                            ballLocation.Y = blockRect1.Top - ballRect.Height;
                            UpdateRectangle();
                        }
                    }


                    UpdateRectangle();
                    intersect = true;

                    ballSpeed.Y *= -1;
                    //ballSpeed.Y += (blockSpeed.Y / 8);

                }
                if (blockRect.Top <= 0)
                {
                    blockLocation.Y = 0;
                }
                if (blockRect.Bottom >= 600)
                {
                    blockLocation.Y = 450;
                }
                if (blockRect1.Top <= 0)
                {
                    blockLocation1.Y = 0;
                }
                if (blockRect1.Bottom >= 600)
                {
                    blockLocation1.Y = 450;
                }

                if (ballRect.Top <= 0)
                {
                    ballSpeed.Y *= -1;
                }
                else if (ballRect.Bottom >= 600)
                {
                    ballSpeed.Y *= -1;
                }

                if (ballRect.Left >= 1000)
                {
                    ballSpeed.X = 0; ballSpeed.Y = 0;
                    ballLocation.X = 477; ballLocation.Y = 276;
                    timer = true;
                    score += 1;

                }
                else if (ballRect.Right <= 0)
                {
                    ballSpeed.X = 0; ballSpeed.Y = 0;
                    ballLocation.X = 477; ballLocation.Y = 276;
                    timer = true;
                    score1 += 1;

                }
                if (timer)
                {
                    time += 1;
                }

                if (time == 180)
                {
                    begin = true;
                    timer = false;
                    time = 0;
                }

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
            else if (screen == Screen.Menu)
            {
                _spriteBatch.Draw(menuTexture, window, Color.White);
                _spriteBatch.DrawString(menuFont, "Menu", new Vector2(463, 80), Color.White);
            }
            

            else if (screen == Screen.Pause)
            {
                if (stopwatch <= 60)
                {
                    _spriteBatch.Draw(pauseTexture, window, Color.White);

                }
                else if (stopwatch <= 120)
                {
                    _spriteBatch.Draw(threeTexture, window, Color.White);
                }
                else if (stopwatch <= 180)
                {
                    _spriteBatch.Draw(twoTexture, window, Color.White);
                }
                else if (stopwatch <= 240)
                {
                    _spriteBatch.Draw(oneTexture, window, Color.White);
                }
            }
            else if (screen == Screen.Gameplay)
            {
                _spriteBatch.Draw(backTexture, window, Color.White);
                _spriteBatch.Draw(blockTexture, blockRect, Color.White);
                _spriteBatch.Draw(blockTexture, blockRect1, Color.White);
                _spriteBatch.Draw(circleTexture, circleRect, Color.White);
                _spriteBatch.Draw(lineTexture, lineRect, Color.White);
                _spriteBatch.DrawString(font, "" + score, new Vector2(340, 60), Color.Black);
                _spriteBatch.DrawString(font, "" + score1, new Vector2(585, 60), Color.Black);
                _spriteBatch.Draw(ballTexture, ballRect, Color.White);

            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void UpdateRectangle()
        {
            ballRect.Location = ballLocation.ToPoint();
            blockRect.Location = blockLocation.ToPoint(); 
            blockRect1.Location = blockLocation1.ToPoint();
        }

    }
}
