using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
        Hint,
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

        private SpriteFont mainFont;
        private SpriteFont controlFont;
        private Texture2D sample;

        const float xBoundary = 500f;
        const float yBoundary = 600f;
        float xMovement;
        float yMovement;

        KeyboardState previousKeyboardState;
        MouseState previousMouseState;
        GameStates currentState;

        //Used to determine if the object is being dragged by the mouse.
        bool isDragAndDropping;

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
            imageRectangle = new Rectangle(200, 250, 50, 50);
            isDragAndDropping = false;
            this.IsMouseVisible = true;

            base.Initialize();
        }
        
        private void ProcessInput()
        {
            //Making the keyboard and mouse states to be used
            KeyboardState kbState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            switch (currentState)
            {
                  case GameStates.Menu:
                    if (kbState.IsKeyDown(Keys.C))
                    {
                     currentState = GameStates.Control;
                    }
                    if (kbState.IsKeyDown(Keys.L))
                    {
                     currentState = GameStates.Select;
                    }
                    break;

                 case GameStates.Control:
                   if (kbState.IsKeyDown(Keys.L))
                   {
                      currentState = GameStates.Select;
                   }

                   if (kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                   {
                      currentState = GameStates.Stage;
                   }
                   break;

                case GameStates.Select:
                   if (kbState.IsKeyDown(Keys.C))
                   {
                      currentState = GameStates.Control;
                   }
                

                   if (kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                   {
                      currentState = GameStates.Stage;
                   }
                   break;
               
                case GameStates.Stage:
                    //Determines if the mouse button is being held down and if the mouse is hovering over the object.
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                        && mState.Position.X > imageRectangle.X && mState.Position.X < (imageRectangle.X + imageRectangle.Width)
                        && mState.Position.Y > imageRectangle.Y && mState.Position.Y < (imageRectangle.Y + imageRectangle.Height))
                    {
                        isDragAndDropping = true;
                    }

                    //Drags the object in accordance to the mouse's changing position.
                    //I set up the dragging effect so the object moves in the direction of the mouse's current position from the mouse's previous position.
                    if (isDragAndDropping)
                    {
                        int xDifference = mState.Position.X - previousMouseState.Position.X;
                        int yDifference = mState.Position.Y - previousMouseState.Position.Y;
                        imageRectangle.X += xDifference;
                        imageRectangle.Y += yDifference;
                    }

                    //Turns off the dragging effect if the mouse button is released.
                    if (mState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Released)
                    {
                        isDragAndDropping = false;
                    }
                    
                    if (kbState.IsKeyDown(Keys.R))
                   {
                      currentState = GameStates.Failure;
                   }
                   if (kbState.IsKeyDown(Keys.H))
                   {
                      currentState = GameStates.Hint;
                   }
                   break;

                case GameStates.Failure:
                   if (kbState.IsKeyDown(Keys.L))
                   {
                      currentState = GameStates.Select;
                   }

                   if (kbState.IsKeyDown(Keys.Escape))
                   {
                     currentState = GameStates.Menu;
                   }
                
                   if (kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                   {
                      currentState = GameStates.Stage;
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
            sample = Content.Load<Texture2D>("Circle");
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

            // testing the game state working
            switch (currentState)
            {
                case GameStates.Menu:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.DrawString(mainFont, "Welcome to Avert.\nPress C to go to control.\nPress L to go to Level", new Vector2(100f, 100f), Color.Red);
                    break;

                case GameStates.Control:
                    GraphicsDevice.Clear(Color.Red);
                    spriteBatch.DrawString(mainFont, "Here is Control Menu", new Vector2(100f, 100f), Color.Black);

                    break;

                case GameStates.Select:
                    GraphicsDevice.Clear(Color.Blue);
                    spriteBatch.DrawString(mainFont, "Here is Level Menu", new Vector2(100f, 100f), Color.Black);
                    break;

                case GameStates.Stage:
                    GraphicsDevice.Clear(Color.Yellow);
                    spriteBatch.DrawString(mainFont, "Here is the Game itself", new Vector2(100f, 100f), Color.Black);
                    spriteBatch.Draw(sample, imageRectangle, Color.Black);
                    break;

                case GameStates.Failure:
                    GraphicsDevice.Clear(Color.Green);
                    spriteBatch.DrawString(mainFont, "Here is game over", new Vector2(100f, 100f), Color.Black);
                    break;
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
