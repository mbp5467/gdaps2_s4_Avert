using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Avert
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    //Making the enumerations for the game phases.
    //These are the main menu, controls, level select,
    //the actual level, and game over
    enum GameStates
    {
        Menu,
        Control,
        Select,
        Stage,
        Failure
    }
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Making the various fields, each representing
        //various elements used in the game.
        SpriteFont gameFont;
        Texture2D imageTexture;
        private Vector2 imageLocation;
        private Rectangle imageRectangle;
        private Rectangle controlRectangle;
        private Rectangle levelRectangle;
        private Rectangle gameRectangle; //Fields for the fonts, textures, and Vectors/Rectangles


        private SpriteFont mainFont;
        private SpriteFont controlFont;
        private Texture2D mirrorTexture;
        private Texture2D gridTexture;
        private Texture2D redBox; //Fields for fonts and images

        const float xBoundary = 600f;
        const float yBoundary = 600f;
        float xMovement;
        float yMovement; //Fields for the X/Y boundaries and movememnts

        KeyboardState previousKeyboardState;
        MouseState previousMouseState;
        GameStates currentState; //Fields for keyboard and mouse states for input
                                 //as well as a GameStates enum

        bool loadLevel;
        private List<Rectangle> spots;

        //Used to determine if the object is being dragged by the mouse.
        bool isDragAndDropping;

        //Life and total life fields
        int life = 5;
        const int Total_Life = 5;

        //timer is set to the constant
        //time and is subtracted from during the levels
        double timer = 10.000;
        const double Time = 10.000;

        private Mirror mirror;
        private MoveableShape moveableShape;
         
        GameConfig setup = new GameConfig();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Changing the width and height of the screen to 500x600
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            previousKeyboardState = Keyboard.GetState();
            previousMouseState = Mouse.GetState();
            currentState = GameStates.Menu;
            imageRectangle = new Rectangle(405, 505, 50, 50);
            controlRectangle = new Rectangle(40, 330, 130, 50);
            levelRectangle = new Rectangle(340, 330, 130, 50);
            gameRectangle = new Rectangle(180, 330, 130, 50);
            isDragAndDropping = false;
            this.IsMouseVisible = true;
            loadLevel = false;
            base.Initialize();
        }
        
        private void ProcessInput()
        {
            //Making the keyboard and mouse states to be used
            KeyboardState kbState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            //Switch statement for processing input, allowing the transitions between states.
            //Menu, Controls, Select and Failure allow keyboard inputs that can make the states switch
            //between each other. The stage statement is where the controls of the game take place.

            switch (currentState)
            {
                  case GameStates.Menu:
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                        && mState.Position.X > controlRectangle.X && mState.Position.X < (controlRectangle.X + controlRectangle.Width)
                        && mState.Position.Y > controlRectangle.Y && mState.Position.Y < (controlRectangle.Y + controlRectangle.Height)
                        ||kbState.IsKeyDown(Keys.C))
                    {
                        currentState = GameStates.Control;
                    }
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                       && mState.Position.X > levelRectangle.X && mState.Position.X < (levelRectangle.X + levelRectangle.Width)
                       && mState.Position.Y > levelRectangle.Y && mState.Position.Y < (levelRectangle.Y + levelRectangle.Height)
                       || kbState.IsKeyDown(Keys.L))
                    {
                        currentState = GameStates.Select;
                    }
                    break;

                 case GameStates.Control:

                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                         && mState.Position.X > gameRectangle.X && mState.Position.X < (gameRectangle.X + gameRectangle.Width)
                         && mState.Position.Y > gameRectangle.Y && mState.Position.Y < (gameRectangle.Y + gameRectangle.Height)
                         ||(kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space)))
                    {
                        currentState = GameStates.Stage;
                    }
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                       && mState.Position.X > levelRectangle.X && mState.Position.X < (levelRectangle.X + levelRectangle.Width)
                       && mState.Position.Y > levelRectangle.Y && mState.Position.Y < (levelRectangle.Y + levelRectangle.Height)
                       || kbState.IsKeyDown(Keys.L))
                    {
                        life = Total_Life;
                        currentState = GameStates.Select;
                    }
                    break;

                case GameStates.Select:
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                        && mState.Position.X > controlRectangle.X && mState.Position.X < (controlRectangle.X + controlRectangle.Width)
                        && mState.Position.Y > controlRectangle.Y && mState.Position.Y < (controlRectangle.Y + controlRectangle.Height)
                        || kbState.IsKeyDown(Keys.C))
                    {
                        currentState = GameStates.Control;
                    }
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                          && mState.Position.X > gameRectangle.X && mState.Position.X < (gameRectangle.X + gameRectangle.Width)
                          && mState.Position.Y > gameRectangle.Y && mState.Position.Y < (gameRectangle.Y + gameRectangle.Height)
                          ||(kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space)))
                    {
                        life = Total_Life;
                        currentState = GameStates.Stage;
                    }
                    break;
               
                case GameStates.Stage:
                    
                    //Restart the game
                    if (kbState.IsKeyDown(Keys.R))
                   {
                        life--;
                      currentState = GameStates.Failure;
                   }
                    //Game over if your life is out or there's no time left
                    if (life == 0 || timer <= 0) 
                    {
                        currentState = GameStates.Failure;
                    }

                   break;

                case GameStates.Failure:
                   if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                       && mState.Position.X > levelRectangle.X && mState.Position.X < (levelRectangle.X + levelRectangle.Width)
                       && mState.Position.Y > levelRectangle.Y && mState.Position.Y < (levelRectangle.Y + levelRectangle.Height)
                        || kbState.IsKeyDown(Keys.L))
                   {
                      currentState = GameStates.Select;
                   }

                   if (kbState.IsKeyDown(Keys.Escape))
                   {
                     currentState = GameStates.Menu;
                   }
                    if (life > 0)
                    {
                        if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                               && mState.Position.X > gameRectangle.X && mState.Position.X < (gameRectangle.X + gameRectangle.Width)
                               && mState.Position.Y > gameRectangle.Y && mState.Position.Y < (gameRectangle.Y + gameRectangle.Height)
                               || kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                        {
                            timer = Time; //Resets the timer
                            currentState = GameStates.Stage;
                        }
                    }
                   break;

            }
            previousKeyboardState = kbState;
            previousMouseState = mState;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mainFont = Content.Load<SpriteFont>("ControlText");

            // TODO: use this.Content to load your game content here
            mirrorTexture = Content.Load<Texture2D>("textures\\objects\\mirror");
            mirror = new Mirror(mirrorTexture, imageRectangle);
            gridTexture = Content.Load<Texture2D>("gridTexture");
            redBox = Content.Load<Texture2D>("redBox"); //Soon to be replaced with drawings
            spots = new List<Rectangle>();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            // timer system
            if (currentState == GameStates.Stage)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (loadLevel == false)
                {
                    setup.LoadLevel();
                    imageRectangle.Width = setup.ShapeSize();
                    imageRectangle.Height = setup.ShapeSize();
                    mirror.LoadLevel();
                    loadLevel = true;
                }
                mirror.Update(gameTime);
            }
            else if (currentState != GameStates.Stage)
            {
                timer = 10.000;
                loadLevel = false;
            }
            ProcessInput();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            // Testing the game states before they are replaced
            //with drawings of the game's artstyle, using switch statements
            //to draw the content in each screen.
            switch (currentState)
            {
                case GameStates.Menu:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.DrawString(mainFont, "Welcome to Avert.\n", new Vector2(100f, 100f), Color.Red);
                    spriteBatch.Draw(redBox, controlRectangle, Color.White);
                    spriteBatch.Draw(redBox, levelRectangle, Color.White);
                    spriteBatch.DrawString(mainFont, "Control", new Vector2(50f,350f), Color.Red);
                    spriteBatch.DrawString(mainFont, "Level", new Vector2(350f,350f),Color.Red);
                    break;

                case GameStates.Control:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.DrawString(mainFont, "Control", new Vector2(200f, 100f), Color.Red);
                    spriteBatch.DrawString(mainFont,"Click and drag the objects with the mouse,\n" +
                        " shoot the laser with the space bar, and \n" +
                        "hit the target to clear the level!",new Vector2(50f,200f),Color.Red);
                    spriteBatch.Draw(redBox, levelRectangle, Color.White);
                    spriteBatch.DrawString(mainFont, "Level", new Vector2(350f, 350f), Color.Red);
                    spriteBatch.Draw(redBox, gameRectangle, Color.White);
                    spriteBatch.DrawString(mainFont, "Game", new Vector2(200f,350f), Color.Red);
                    break;

                case GameStates.Select:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.DrawString(mainFont, "Level", new Vector2(200f, 100f), Color.Red);
                    spriteBatch.Draw(redBox, gameRectangle, Color.White);
                    spriteBatch.DrawString(mainFont, "Game", new Vector2(200f, 350f), Color.Red);
                    spriteBatch.Draw(redBox, levelRectangle, Color.White);
                    spriteBatch.DrawString(mainFont, "Level", new Vector2(350f, 350f), Color.Red);
                    break;

                case GameStates.Stage:
                    //Drawing the grid
                    setup.Draw(spriteBatch, gridTexture);
                    GraphicsDevice.Clear(Color.Yellow);
                    spriteBatch.DrawString(mainFont, String.Format("{0:0.000}", timer) + "\n" + "life: "+life.ToString(), new Vector2(10f, 510f), Color.Black);
                    if (mirror.Active == true)
                    {
                        mirror.Draw(spriteBatch);
                    }
                    break;

                case GameStates.Failure:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.DrawString(mainFont, "FAILED!", new Vector2(100f, 100f), Color.Red);
                    spriteBatch.DrawString(mainFont, "Score: ", new Vector2(100f, 200f), Color.Red);
                    spriteBatch.Draw(redBox, levelRectangle, Color.White);
                    spriteBatch.DrawString(mainFont, "Level", new Vector2(350f, 350f), Color.Red);
                    if (life > 0) 
                    {
                        spriteBatch.Draw(redBox, gameRectangle, Color.White);
                        spriteBatch.DrawString(mainFont, "Restart", new Vector2(190f, 350f), Color.Red);
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
