using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        Rules,
        Oneplay,
        Gameplay,
        Pause,
        EndRed,
        EndBlue
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Screen screen;
        Texture2D backTexture, blockTexture, circleTexture, lineTexture, ballTexture, introTexture;
        Texture2D  pauseTexture, oneTexture, twoTexture, threeTexture, menuTexture, rulesTexture;
        Texture2D speedUpTexture,speedDownTexture, paddleUpTexure, paddleDownTexture, powerUpTexture;
        Texture2D redWin, blueWin, metalTexture;
        Rectangle window, blockRect, blockRect1, circleRect, lineRect, ballRect, twoPlayRect, quitRect;
        Rectangle rulesRect, speedUpRect, speedDownRect, powerUpRect, metalRect;
        Vector2 ballLocation, blockLocation, blockLocation1;
        SpriteFont font, menuFont;
        Vector2 ballSpeed, blockSpeed, blockSpeed1;
        Random gen;
        KeyboardState keyboardState, prevKeyState;
        MouseState mouseState, prevMouseState;
        bool begin, intersect, intersect1, timer, stopwatch1;
        int time, clock = 0;
        int score = 0, score1 = 0, stopwatch = 0;
        int powerUps=0, powerUpLocation;
        int speedUp = 1, speedDown = 2, paddleUp = 3, paddleDown = 4, powertime;
        SoundEffect paddleBallSound, wallBallSound, jeopardySound, introSound, endSound;
        SoundEffectInstance instancePaddleBallSound, instanceWallBallSound, instanceJeopardySound, introSoundInstance, endSoundInstance;
        bool powerup;
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

            //speedDownRect = new Rectangle(0,0,100,100);
            //speedUpRect = new Rectangle(0,0, 100,100);
            powerUpRect = new Rectangle(-100, -100, 100, 100);

            ballSpeed = new Vector2(0, 2);
            blockSpeed = new Vector2(0, 0);
            blockSpeed1 = new Vector2(0, 0);

            twoPlayRect = new Rectangle(430, 336, 136, 29);
            quitRect = new Rectangle(430, 375, 136, 29);

            ballRect = new Rectangle(477, 276, 50, 50);
            ballLocation = ballRect.Location.ToVector2();

            lineRect = new Rectangle(498, 0, 10, 600);
            circleRect = new Rectangle(452, 250, 100, 100);

            blockRect = new Rectangle(10, 225, 20, 150);
            blockRect1 = new Rectangle(970, 225, 20, 150);

            rulesRect = new Rectangle(399,495,200,50);
            metalRect = new Rectangle(400, 225, 200, 300);
            //blockCRect = new Rectangle(blockRect.X,blockRect.Y,0,150);
            //blockCRect1 = new Rectangle(blockRect.X, blockRect.Y, 0, 150);

            blockLocation = blockRect.Location.ToVector2();
            blockLocation1 = blockRect1.Location.ToVector2();
            powerup = false;
            time = 0;
            intersect1 = true;
            begin = true;
            timer = false;
            intersect = true;
            stopwatch1 = false;
            powertime = 0;
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
            introTexture = Content.Load<Texture2D>("IntroPong(3)");
            pauseTexture = Content.Load<Texture2D>("pause1");
            oneTexture = Content.Load<Texture2D>("one1");
            twoTexture = Content.Load<Texture2D>("two1"); 
            threeTexture = Content.Load<Texture2D>("three1");
            menuTexture = Content.Load<Texture2D>("menu1");
            rulesTexture = Content.Load<Texture2D>("Rules");
            speedUpTexture = Content.Load<Texture2D>("SpeedUp");
            powerUpTexture = Content.Load<Texture2D>("MarioPower");
            redWin = Content.Load<Texture2D>("RedWin");
            blueWin = Content.Load<Texture2D>("BlueWin");
            metalTexture = Content.Load<Texture2D>("Gold_Medal");

            menuFont = Content.Load<SpriteFont>("menuFont");
            font = Content.Load<SpriteFont>("Font");
            paddleBallSound = Content.Load<SoundEffect>("Boing");
            instancePaddleBallSound = paddleBallSound.CreateInstance();
            wallBallSound = Content.Load<SoundEffect>("TopBounce");
             instanceWallBallSound = wallBallSound.CreateInstance();
            jeopardySound = Content.Load<SoundEffect>("Jeopardy");
            instanceJeopardySound = jeopardySound.CreateInstance();
            // TODO: use this.Content to load your game content here

            introSound = Content.Load<SoundEffect>("Cantina Band");

            endSound = Content.Load<SoundEffect>("Winning sound");

            introSoundInstance = introSound.CreateInstance();
            endSoundInstance = endSound.CreateInstance();
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
                introSoundInstance.Play();
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && twoPlayRect.Contains(mouseState.Position))
                {
                    screen = Screen.Gameplay;
                    score = 0;
                    score1 = 0;
                    clock = 0;
                    blockLocation.X = 10;
                    blockLocation.Y = 225;
                    blockLocation1.X = 970;
                    blockLocation1.Y = 225;
                    powerUpRect.X = -100;
                    powerUpRect.Y = -100;
                    introSoundInstance.Stop();

                }
                else if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && rulesRect.Contains(mouseState.Position))
                {
                    screen = Screen.Rules;

                }
                else if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && quitRect.Contains(mouseState.Position))
                {
                    Exit();
                }
                if (keyboardState.IsKeyDown(Keys.M) && prevKeyState.IsKeyUp(Keys.M))
                {
                    screen = Screen.Rules;
                }
               
            }
            else if (screen == Screen.EndBlue)
            {
                instanceJeopardySound.Stop();
                endSoundInstance.Play();
                if (keyboardState.IsKeyDown(Keys.I) && prevKeyState.IsKeyUp(Keys.I))
                {
                    screen = Screen.Intro;
                    endSoundInstance.Stop();
                }
            }
            else if (screen == Screen.EndRed)
            {
                instanceJeopardySound.Stop();
                endSoundInstance.Play();
                if (keyboardState.IsKeyDown(Keys.I) && prevKeyState.IsKeyUp(Keys.I))
                {
                    screen = Screen.Intro;
                    endSoundInstance.Play();
                }
            }
            else if (screen == Screen.Rules)
            {
                if (keyboardState.IsKeyDown(Keys.M) && prevKeyState.IsKeyUp(Keys.M))
                {
                    screen = Screen.Intro;
                }
                
            }
            else if (screen == Screen.Menu)
            {
                if (keyboardState.IsKeyDown(Keys.M) && prevKeyState.IsKeyUp(Keys.M))
                {
                    screen = Screen.Pause;
                }
                if (keyboardState.IsKeyDown(Keys.Space) && prevKeyState.IsKeyUp(Keys.Space))
                {
                    screen = Screen.Pause;
                }
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
                if (keyboardState.IsKeyDown(Keys.M) && prevKeyState.IsKeyUp(Keys.M) && !stopwatch1)
                {
                    screen = Screen.Menu;
                }
            }




            else if (screen == Screen.Gameplay)
            {
                
                instanceJeopardySound.Play();
                clock += 1;
                


                if (clock == 600)
                {

                    powerUps = gen.Next(1, 3); //chose what powerUp

                    powerUpLocation = gen.Next(1, 9);//chose location

                    if (powerUpLocation == 1)
                    {
                        powerUpRect.X = 100;
                        powerUpRect.Y = 25;

                    }
                    else if (powerUpLocation == 2)
                    {
                        powerUpRect.X = 171;
                        powerUpRect.Y = 289;

                    }
                    else if (powerUpLocation == 3)
                    {
                        powerUpRect.X = 365;
                        powerUpRect.Y = 421;

                    }
                    else if (powerUpLocation == 4)
                    {
                        powerUpRect.X = 121;
                        powerUpRect.Y = 479;

                    }
                    else if (powerUpLocation == 5)
                    {
                        powerUpRect.X = 705;
                        powerUpRect.Y = 89;

                    }
                    else if (powerUpLocation == 6)
                    {
                        powerUpRect.X = 833;
                        powerUpRect.Y = 477;

                    }
                    else if (powerUpLocation == 7)
                    {
                        powerUpRect.X = 591;
                        powerUpRect.Y = 432;

                    }
                    else if (powerUpLocation == 8)
                    {
                        powerUpRect.X = 728;
                        powerUpRect.Y = 225;

                    }



                }
                if (ballRect.Intersects(powerUpRect))
                {
                    if (powerUps == speedUp)
                    {
                        ballSpeed.X *= 1.5f;//Needs to be temporary
                        ballSpeed.Y *= 1.5f;
                    }
                    else if (powerUps == speedDown)
                    {
                        ballSpeed.X /= 2f;//Needs to be temporary
                        ballSpeed.Y /= 2f;
                    }
                   
                    powerUpRect.X = -200;
                    powerUpRect.Y = -200;
                    powerup = true;


                }
                if (powerup)
                {
                    powertime += 1;
                }

                if (powertime == 360 )
                {



                    if (powerUps == speedUp)
                    {
                        ballSpeed.X /= 1.5f;//Needs to be temporary
                        ballSpeed.Y /= 1.5f;
                    }
                    else if (powerUps == speedDown)
                    {
                        ballSpeed.X *= 2f;//Needs to be temporary
                        ballSpeed.Y *= 2f;
                    }
                    powertime = 0;
                    clock = 0;
                    powerup = false;
                }
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
                    intersect = false;
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
                    intersect1 = false;
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
                    //if (blockRect.Top == 0)
                    //{
                    //    blockSpeed1.Y = 0;
                    //}
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

                //if (ballRect.Intersects(blockRect) || ballRect.Intersects(blockRect1))
                //{
                //    ballLocation.X -= ballSpeed.X;
                //    UpdateRectangle();
                //    intersect = true;
                //    ballSpeed.X *= -1;
                //    ballSpeed.Y += (blockSpeed.Y / 8);
                //}
                if (ballRect.Intersects(blockRect) && intersect)
                {
                    ballLocation.X -= ballSpeed.X;
                    UpdateRectangle();
                    intersect = false;


                    instancePaddleBallSound.Play();

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
                    instancePaddleBallSound.Play();
                }


                //ballSpeed.X *= (3/2);
                if (ballRect.Intersects(blockRect) || ballRect.Intersects(blockRect1))
                {
                    //ballSound.Play();
                    if (ballRect.Left > blockRect.Right)
                    {
                        if (ballRect.Intersects(blockRect))
                        {
                            ballSpeed.X = -4;
                            //ballSpeed.X -= ballRect.Center.X;
                        }
                    }
                    if (ballRect.Right > blockRect1.Left)
                    {
                        if (ballRect.Intersects(blockRect1))
                        {
                            ballSpeed.X = 4;
                            //ballSpeed.X -= ballRect.Center.X;
                        }
                    }

                    instancePaddleBallSound.Play();
                }




                ballLocation.Y += ballSpeed.Y;
                UpdateRectangle();

                if (ballRect.Intersects(blockRect) || ballRect.Intersects(blockRect1))
                {

                    // If ball hits paddle 1
                    if (ballLocation.X < window.Center.X)
                    {
                        intersect = false;

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
                        intersect1 = false;
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
                    instancePaddleBallSound.Play();

                    //UpdateRectangle();
                    //intersect = true;

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
                    instanceWallBallSound.Play();
                }
                else if (ballRect.Bottom >= 600)
                {
                    ballSpeed.Y *= -1;
                    instanceWallBallSound.Play();

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
                //if (keyboardState.IsKeyDown(Keys.P))
                //{
                //    screen = Screen.EndBlue;
                //}
                //if (keyboardState.IsKeyDown(Keys.O))
                //{
                //    screen = Screen.EndRed;
                //}

                if (score == 5)
                {
                    screen = Screen.EndRed;
                    instanceJeopardySound.Stop();
                }
                else if (score1 == 5)
                {
                    screen = Screen.EndBlue;
                    instanceJeopardySound.Stop();
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
                _spriteBatch.Draw(rulesTexture, rulesRect, Color.White);
            }
            else if (screen == Screen.Menu)
            {
                _spriteBatch.Draw(menuTexture, window, Color.White);
                _spriteBatch.DrawString(menuFont, "Rules", new Vector2(463, 80), Color.White);
                _spriteBatch.DrawString(menuFont, "Pong is a two-player game where each player controls \na paddle. The left player uses the W key to move up \nand the S key to move down. The right player uses the \nUp Arrow key to move up and the Down Arrow key \nto move down. Players try to hit the ball back and forth \nacross the screen. If a player misses the ball and it goes \npast their paddle, the other player scores a point. The \ngame continues until a player reaches the winning score.", new Vector2(100, 160), Color.White);
            }
            else if (screen == Screen.Rules)
            {
                _spriteBatch.Draw(menuTexture, window, Color.White);
                _spriteBatch.DrawString(menuFont, "Rules", new Vector2(463, 80), Color.White);
                _spriteBatch.DrawString(menuFont, "Pong is a two-player game where each player controls \na paddle. The left player uses the W key to move up \nand the S key to move down. The right player uses the \nUp Arrow key to move up and the Down Arrow key \nto move down. Players try to hit the ball back and forth \nacross the screen. If a player misses the ball and it goes \npast their paddle, the other player scores a point. The \ngame continues until a player reaches the winning score.", new Vector2(100, 160), Color.White);
                _spriteBatch.DrawString(menuFont, "Space is Pause     M is Rules ", new Vector2(275, 500), Color.White);
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

                _spriteBatch.Draw(powerUpTexture, powerUpRect, Color.White);


                _spriteBatch.DrawString(font, "" + score, new Vector2(340, 60), Color.Black);
                _spriteBatch.DrawString(font, "" + score1, new Vector2(585, 60), Color.Black);
                _spriteBatch.Draw(ballTexture, ballRect, Color.White);
                

            }
            else if (screen == Screen.EndRed)
            {
                _spriteBatch.Draw(redWin, window, Color.White);
                _spriteBatch.DrawString(font, "You Win", new Vector2(200, 80), Color.White);
                _spriteBatch.Draw(metalTexture, metalRect, Color.White);
                _spriteBatch.DrawString(menuFont, "!Press I to go back to main menu! ", new Vector2(270, 530), Color.White);
            }
            else if (screen == Screen.EndBlue)
            {
                _spriteBatch.Draw(blueWin, window, Color.White);
                _spriteBatch.DrawString(font, "You Win", new Vector2(200, 80), Color.White);
                _spriteBatch.Draw(metalTexture, metalRect, Color.White);
                _spriteBatch.DrawString(menuFont, "!Press I to go back to main menu! ", new Vector2(270, 530), Color.White);
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
