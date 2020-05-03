using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Avert
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    #region Enums
    //Making the enumerations for the game phases.
    //These are the main menu, controls, level select,
    //the actual level, game over, and win
    enum GameStates
    {
        Menu,
        Control,
        Select,
        Stage,
        Failure,
        Wins
    }
    #endregion

    public class Game1 : Game
    {
        #region Fields
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Making the various fields, each representing
        //various elements used in the game.

        //Rectangles used for everything, from buttons, to game objects
        private Rectangle imageRectangle;
        private Rectangle controlRectangle;
        private Rectangle levelRectangle;
        private Rectangle gameRectangle; //Fields for the fonts, textures, and Vectors/Rectangles
        private Rectangle titleRectangle;
        private Rectangle rotateCWRectangle;
        private Rectangle rotateCCWRectangle;
        private Rectangle numberRectangle;
        private Rectangle numberRectangleTwo;
        private Rectangle numberRectangleThree;
        private Rectangle numberRectangleFour;
        private Rectangle numberRectangleFive;

        //Textures for everything in this game
        private SpriteFont mainFont;
        private Texture2D mirrorTextureBR;
        private Texture2D mirrorTextureBL;
        private Texture2D mirrorTextureTL;
        private Texture2D mirrorTextureTR;
        private Texture2D gridTexture;
        private Texture2D box; //Fields for fonts and images
        private Texture2D boxSelected;
        private Texture2D title;
        private Texture2D background;
        private Texture2D backgroundRed;
        private Texture2D wallBlue;
        private Texture2D wallRed;
        private Texture2D startUp;
        private Texture2D startDown;
        private Texture2D startLeft;
        private Texture2D startRight;
        private Texture2D target;
        private Texture2D targetFilled;
        private Texture2D laser;
        private Texture2D laserH;
        private Texture2D boxRed;
        private Texture2D boxRedSelected;
        private Texture2D rotateCW;
        private Texture2D rotateCCW;

        //Fields for keyboard and mouse states for input
        //as well as a GameStates enum
        KeyboardState previousKeyboardState;
        MouseState previousMouseState;
        GameStates currentState;

        //All of these are used in Update for loading levels 
        //and making the laser work properly
        int levelNumber;
        bool loadLevel;
        bool isLaserShoot;
        bool laserAnimation;
        bool hitWall;
        bool hitTarget;

        //Life and total life fields
        int life = 5;
        const int Total_Life = 5;

        //timer is set to the constant time
        //and is subtracted from during the levels
        double timer = 10.000;
        const double Time = 10.000;
        //Delays the changestate so players can see if they hit the target or not
        double shootTimer = 1.500;

        //score system
        int score = 0;

        //Fields for the created objects
        private Mirror mirror;
        private Wall walls;
        private Target targets;
        private Laser lasers;
        private LaserBeam laserBeam;

        GameConfig setup;
        #endregion

        public static int screenSize_W { get; set; } = 1920;
        public static int screenSize_H { get; set; } = 1080;
        #region Constructor
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //Changing the width and height of the screen to 1920 x 1080 (fullscreen)
            graphics.PreferredBackBufferWidth = screenSize_W;
            graphics.PreferredBackBufferHeight = screenSize_H;
            graphics.IsFullScreen = false;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

        }
        #endregion

        #region Initialize
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //Initializing all of the needed fields for the Game1 class
            previousKeyboardState = Keyboard.GetState();
            previousMouseState = Mouse.GetState();
            currentState = GameStates.Menu;
            setup = new GameConfig();
            imageRectangle = new Rectangle(405, 505, 50, 50);
            //All of the following rectangles are buttons for menu navigation
            controlRectangle = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 400,
                graphics.PreferredBackBufferHeight / 2 + 290, 175, 50);
            levelRectangle = new Rectangle(graphics.PreferredBackBufferWidth / 2 + 200,
                graphics.PreferredBackBufferHeight / 2 + 290, 130, 60);
            gameRectangle = new Rectangle(graphics.PreferredBackBufferWidth / 2 - 100,
                graphics.PreferredBackBufferHeight / 2 + 290, 255, 60);
            titleRectangle = new Rectangle((graphics.PreferredBackBufferWidth - 320) / 2,
                graphics.PreferredBackBufferHeight / 3, 320, 180);
            numberRectangle = new Rectangle((graphics.PreferredBackBufferWidth - 300) / 2,
                graphics.PreferredBackBufferHeight / 3 + 50, 50, 50);
            numberRectangleTwo = new Rectangle((graphics.PreferredBackBufferWidth - 300) / 2 + 60,
                graphics.PreferredBackBufferHeight / 3 + 50, 50, 50);
            numberRectangleThree = new Rectangle((graphics.PreferredBackBufferWidth - 300) / 2 + 120,
                graphics.PreferredBackBufferHeight / 3 + 50, 50, 50);
            numberRectangleFour = new Rectangle((graphics.PreferredBackBufferWidth - 300) / 2 + 180,
                graphics.PreferredBackBufferHeight / 3 + 50, 50, 50);
            numberRectangleFive = new Rectangle((graphics.PreferredBackBufferWidth - 300) / 2 + 240,
                graphics.PreferredBackBufferHeight / 3 + 50, 50, 50);
            IsMouseVisible = true;
            loadLevel = false;
            mirror = new Mirror(mirrorTextureBR, imageRectangle);
            lasers = new Laser(startUp);
            walls = new Wall(wallBlue);
            targets = new Target(target);
            laserBeam = new LaserBeam(laser);
            base.Initialize();
        }
        #endregion

        #region SingleKeyPress
        /// <summary>
        /// Checks for single key press
        /// </summary>
        /// <param name="key"></param>
        /// <param name="current"></param>
        /// <returns></returns>
        private bool SingleKeyPress(Keys key, KeyboardState current)
        {
            if (current.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        //Checks if mouse is hovering over given rectangle
        private bool IsHovering(MouseState msState, Rectangle Rect)
        {
            if (Rect.Contains(msState.X, msState.Y))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region ProcessInput
        /// <summary>
        /// Helper method for checking every input in every screen
        /// </summary>
        private void ProcessInput()
        {
            //Making the keyboard and mouse states to be used
            KeyboardState kbState = Keyboard.GetState();
            MouseState mState = Mouse.GetState();

            //Toggles full screen mode
            if (SingleKeyPress(Keys.F1, kbState))
            {
                graphics.ToggleFullScreen();
            }

            //Exits the game
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();

            //Switch statement for processing input, allowing the transitions between states.
            //Menu, Controls, Select and Failure allow keyboard inputs that can make the states switch
            //between each other. The stage statement is where the controls of the game take place.

            switch (currentState)
            {
                case GameStates.Menu:
                    //Goes to the controls screen
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                        && mState.Position.X > controlRectangle.X && mState.Position.X < (controlRectangle.X + controlRectangle.Width)
                        && mState.Position.Y > controlRectangle.Y && mState.Position.Y < (controlRectangle.Y + controlRectangle.Height)
                        || kbState.IsKeyDown(Keys.C))
                    {
                        currentState = GameStates.Control;
                    }
                    //Goes to the level select screen
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                       && mState.Position.X > levelRectangle.X && mState.Position.X < (levelRectangle.X + levelRectangle.Width)
                       && mState.Position.Y > levelRectangle.Y && mState.Position.Y < (levelRectangle.Y + levelRectangle.Height)
                       || kbState.IsKeyDown(Keys.L))
                    {
                        currentState = GameStates.Select;
                    }
                    break;

                case GameStates.Control:
                    //Starts level 1
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                         && mState.Position.X > gameRectangle.X && mState.Position.X < (gameRectangle.X + gameRectangle.Width)
                         && mState.Position.Y > gameRectangle.Y && mState.Position.Y < (gameRectangle.Y + gameRectangle.Height)
                         || (kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space)))
                    {
                        levelNumber = 1;
                        setup.Level = levelNumber;
                        mirror.Level = levelNumber;
                        lasers.Level = levelNumber;
                        targets.Level = levelNumber;
                        walls.Level = levelNumber;
                        laserBeam.Level = levelNumber;
                        life = Total_Life;
                        isLaserShoot = false;
                        laserAnimation = false;
                        laserBeam.HitMirrorSide = false;
                        currentState = GameStates.Stage;
                    }
                    //Goes to the level select screen
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                       && mState.Position.X > levelRectangle.X && mState.Position.X < (levelRectangle.X + levelRectangle.Width)
                       && mState.Position.Y > levelRectangle.Y && mState.Position.Y < (levelRectangle.Y + levelRectangle.Height)
                       || kbState.IsKeyDown(Keys.L))
                    {
                        currentState = GameStates.Select;
                    }
                    break;

                case GameStates.Select:
                    //Goes to the Controls screen
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                        && mState.Position.X > controlRectangle.X && mState.Position.X < (controlRectangle.X + controlRectangle.Width)
                        && mState.Position.Y > controlRectangle.Y && mState.Position.Y < (controlRectangle.Y + controlRectangle.Height)
                        || kbState.IsKeyDown(Keys.C))
                    {
                        currentState = GameStates.Control;
                    }
                    //Goes to the stage. Each if statement is different depending on the level
                    //Level 1
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                          && mState.Position.X > gameRectangle.X && mState.Position.X < (gameRectangle.X + gameRectangle.Width)
                          && mState.Position.Y > gameRectangle.Y && mState.Position.Y < (gameRectangle.Y + gameRectangle.Height) ||
                          mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                          && mState.Position.X > numberRectangle.X && mState.Position.X < (numberRectangle.X + numberRectangle.Width)
                          && mState.Position.Y > numberRectangle.Y && mState.Position.Y < (numberRectangle.Y + numberRectangle.Height)
                          || (kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space)))
                    {
                        levelNumber = 1;
                        mirror.Level = levelNumber;
                        lasers.Level = levelNumber;
                        targets.Level = levelNumber;
                        walls.Level = levelNumber;
                        laserBeam.Level = levelNumber;
                        setup.Level = levelNumber;
                        life = Total_Life;
                        isLaserShoot = false;
                        laserAnimation = false;
                        laserBeam.HitMirrorSide = false;
                        currentState = GameStates.Stage;
                    }
                    //Level 2
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                          && mState.Position.X > numberRectangleTwo.X && mState.Position.X < (numberRectangleTwo.X + numberRectangleTwo.Width)
                          && mState.Position.Y > numberRectangleTwo.Y && mState.Position.Y < (numberRectangleTwo.Y + numberRectangleTwo.Height))
                    {
                        levelNumber = 2;
                        setup.Level = levelNumber;
                        mirror.Level = levelNumber;
                        lasers.Level = levelNumber;
                        targets.Level = levelNumber;
                        walls.Level = levelNumber;
                        laserBeam.Level = levelNumber;
                        life = Total_Life;
                        isLaserShoot = false;
                        laserAnimation = false;
                        laserBeam.HitMirrorSide = false;
                        currentState = GameStates.Stage;
                    }
                    //Level 3
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                          && mState.Position.X > numberRectangleThree.X && mState.Position.X < (numberRectangleThree.X + numberRectangleThree.Width)
                          && mState.Position.Y > numberRectangleThree.Y && mState.Position.Y < (numberRectangleThree.Y + numberRectangleThree.Height))
                    {
                        levelNumber = 3;
                        setup.Level = levelNumber;
                        mirror.Level = levelNumber;
                        lasers.Level = levelNumber;
                        targets.Level = levelNumber;
                        walls.Level = levelNumber;
                        laserBeam.Level = levelNumber;
                        life = Total_Life;
                        isLaserShoot = false;
                        laserAnimation = false;
                        laserBeam.HitMirrorSide = false;
                        currentState = GameStates.Stage;
                    }
                    //Level 4
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                          && mState.Position.X > numberRectangleFour.X && mState.Position.X < (numberRectangleFour.X + numberRectangleFour.Width)
                          && mState.Position.Y > numberRectangleFour.Y && mState.Position.Y < (numberRectangleFour.Y + numberRectangleFour.Height))
                    {
                        levelNumber = 4;
                        setup.Level = levelNumber;
                        mirror.Level = levelNumber;
                        lasers.Level = levelNumber;
                        targets.Level = levelNumber;
                        walls.Level = levelNumber;
                        laserBeam.Level = levelNumber;
                        life = Total_Life;
                        isLaserShoot = false;
                        laserAnimation = false;
                        laserBeam.HitMirrorSide = false;
                        currentState = GameStates.Stage;
                    }
                    //Level 5
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                          && mState.Position.X > numberRectangleFive.X && mState.Position.X < (numberRectangleFive.X + numberRectangleFive.Width)
                          && mState.Position.Y > numberRectangleFive.Y && mState.Position.Y < (numberRectangleFive.Y + numberRectangleFive.Height))
                    {
                        levelNumber = 5;
                        setup.Level = levelNumber;
                        mirror.Level = levelNumber;
                        lasers.Level = levelNumber;
                        targets.Level = levelNumber;
                        walls.Level = levelNumber;
                        laserBeam.Level = levelNumber;
                        life = Total_Life;
                        isLaserShoot = false;
                        laserAnimation = false;
                        laserBeam.HitMirrorSide = false;
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
                    //Game over if you run out of lives or there's no time left
                    if (life == 0 || timer <= 0)
                    {
                        life--;
                        currentState = GameStates.Failure;
                    }

                    //Rotate buttons turn the mirror
                    //1 = TopRight, 2 = BottomRight, 3 = BottomLeft, 4 = TopLeft
                    if (mState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed
                      && mState.Position.X > rotateCWRectangle.X && mState.Position.X < (rotateCWRectangle.X + rotateCWRectangle.Width)
                      && mState.Position.Y > rotateCWRectangle.Y && mState.Position.Y < (rotateCWRectangle.Y + rotateCWRectangle.Height))
                    {
                        if (mirror.Direction < 4)
                        {
                            mirror.Direction++;
                        }
                        else
                        {
                            mirror.Direction = 1;
                        }
                        if (mirror.Direction == 1)
                        {
                            mirror.Texture = mirrorTextureTR;
                        }
                        else if (mirror.Direction == 2)
                        {
                            mirror.Texture = mirrorTextureBR;
                        }
                        else if (mirror.Direction == 3)
                        {
                            mirror.Texture = mirrorTextureBL;
                        }
                        else if (mirror.Direction == 4)
                        {
                            mirror.Texture = mirrorTextureTL;
                        }
                    }
                    if (mState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Pressed
                          && mState.Position.X > rotateCCWRectangle.X && mState.Position.X < (rotateCCWRectangle.X + rotateCCWRectangle.Width)
                          && mState.Position.Y > rotateCCWRectangle.Y && mState.Position.Y < (rotateCCWRectangle.Y + rotateCCWRectangle.Height))
                    {
                        if (mirror.Direction > 1)
                        {
                            mirror.Direction--;
                        }
                        else
                        {
                            mirror.Direction = 4;
                        }
                        if (mirror.Direction == 1)
                        {
                            mirror.Texture = mirrorTextureTR;
                        }
                        else if (mirror.Direction == 2)
                        {
                            mirror.Texture = mirrorTextureBR;
                        }
                        else if (mirror.Direction == 3)
                        {
                            mirror.Texture = mirrorTextureBL;
                        }
                        else if (mirror.Direction == 4)
                        {
                            mirror.Texture = mirrorTextureTL;
                        }
                    }

                    //Shoots the laser
                    if (kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        if (isLaserShoot == false)
                        {
                            //Initializes the location of the laser and its direction
                            laserBeam.ShooterLocation = lasers.Position;
                            laserBeam.Location = lasers.Position;
                            laserBeam.ShooterDirection = lasers.Direction;
                            laserBeam.CurrentDirection = lasers.Direction;
                            isLaserShoot = true;
                            laserAnimation = true;
                            laserBeam.ShootLaser();
                            shootTimer = 1.500;
                        }
                    }

                    //If the mirror overlaps another object, it goes back to its original position
                    foreach (Rectangle wall in walls.ListOfWalls)
                    {
                        if ((mirror.Position.Intersects(lasers.Position)
                        || mirror.Position.Intersects(targets.Position)
                        || mirror.Position.Intersects(wall))
                        && (mState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Released))
                        {
                            mirror.Position = imageRectangle;
                        }
                    }
                    break;

                case GameStates.Failure:
                    Reset();
                    //Goes back to the stage select screen
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                        && mState.Position.X > levelRectangle.X && mState.Position.X < (levelRectangle.X + levelRectangle.Width)
                        && mState.Position.Y > levelRectangle.Y && mState.Position.Y < (levelRectangle.Y + levelRectangle.Height)
                        || kbState.IsKeyDown(Keys.L))
                    {
                        timer = Time; //Resets the timer
                        currentState = GameStates.Select;
                    }
                    //Goes back to the main menu
                    if (kbState.IsKeyDown(Keys.Escape))
                    {
                        currentState = GameStates.Menu;
                    }
                    //If the player still has some lives, they can restart the level
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

                case GameStates.Wins:
                    Reset();
                    //Goes back to the stage select screen
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                        && mState.Position.X > levelRectangle.X && mState.Position.X < (levelRectangle.X + levelRectangle.Width)
                        && mState.Position.Y > levelRectangle.Y && mState.Position.Y < (levelRectangle.Y + levelRectangle.Height)
                        || kbState.IsKeyDown(Keys.L))
                    {
                        timer = Time; //Resets the timer
                        currentState = GameStates.Select;
                    }
                    //Restarts the level
                    if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                        && mState.Position.X > gameRectangle.X && mState.Position.X < (gameRectangle.X + gameRectangle.Width)
                        && mState.Position.Y > gameRectangle.Y && mState.Position.Y < (gameRectangle.Y + gameRectangle.Height)
                        || kbState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        timer = Time; //Resets the timer
                        currentState = GameStates.Stage;
                    }
                    //Goes back to the main menu
                    if (kbState.IsKeyDown(Keys.Escape))
                    {
                        currentState = GameStates.Menu;
                    }
                    break;

            }
            previousKeyboardState = kbState;
            previousMouseState = mState;
        }
        #endregion

        #region Reset
        /// <summary>
        /// Resets everything back to their original poisiton and stage
        /// </summary>
        private void Reset()
        {
            laserBeam.ShooterLocation = lasers.Position;
            laserBeam.Location = lasers.Position;
            laserBeam.ShooterDirection = lasers.Direction;
            laserBeam.CurrentDirection = lasers.Direction;
            isLaserShoot = false;
            laserAnimation = false;
            laserBeam.HitMirrorSide = false;
            walls.Texture = wallBlue;
            targets.Texture = target;
            mirror.Position = imageRectangle;

        }
        #endregion

        #region LoadContent
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
            //Loading all of the fonts and images used in the game
            laser = Content.Load<Texture2D>("textures/laser/laser");
            laserH = Content.Load<Texture2D>("textures/laser/laserH");
            mirrorTextureBR = Content.Load<Texture2D>("textures/objects/mirrorBR");
            mirrorTextureBL = Content.Load<Texture2D>("textures/objects/mirrorBL");
            mirrorTextureTL = Content.Load<Texture2D>("textures/objects/mirrorTL");
            mirrorTextureTR = Content.Load<Texture2D>("textures/objects/mirror");
            background = Content.Load<Texture2D>("textures/Backgrounds/background_blue");
            backgroundRed = Content.Load<Texture2D>("textures/Backgrounds/background_red");//Background turns red when player beats the level.
            wallBlue = Content.Load<Texture2D>("textures/objects/wall_blue");
            wallRed = Content.Load<Texture2D>("textures/objects/wall_red");//wall turns red when player beats the level.
            startUp = Content.Load<Texture2D>("textures/objects/blaster");
            startDown = Content.Load<Texture2D>("textures/objects/blasterD");
            startLeft = Content.Load<Texture2D>("textures/objects/blasterL");
            startRight = Content.Load<Texture2D>("textures/objects/blasterR");
            target = Content.Load<Texture2D>("textures/objects/target_empty");
            targetFilled = Content.Load<Texture2D>("textures/objects/target_filled");
            gridTexture = Content.Load<Texture2D>("textures/Backgrounds/gridlines");
            box = Content.Load<Texture2D>("textures/gui/menu_button"); //Soon to be replaced with drawings
            boxSelected = Content.Load<Texture2D>("textures/gui/menu_button_selected");
            boxRed = Content.Load<Texture2D>("textures/gui/menu_button_red");
            boxRedSelected = Content.Load<Texture2D>("textures/gui/menu_button_red_selected");
            title = Content.Load<Texture2D>("textures/gui/title");
            rotateCW = Content.Load<Texture2D>("textures/gui/rotateCW");
            rotateCCW = Content.Load<Texture2D>("textures/gui/rotateCCW");
            mirror = new Mirror(mirrorTextureBR, imageRectangle);
            walls = new Wall(wallBlue);
            targets = new Target(target);
            lasers = new Laser(startUp);
            laserBeam = new LaserBeam(laser);

        }
        #endregion

        #region UnloadContent
        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        #endregion

        #region Update
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            ProcessInput();
            if (currentState == GameStates.Stage)
            {
                //Timer system
                if (isLaserShoot == false)
                {
                    timer -= gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    shootTimer -= gameTime.ElapsedGameTime.TotalSeconds;
                }
                //Loads in everything for each level
                if (loadLevel == false)
                {
                    int x = (screenSize_W - setup.windowWidth) / 2;
                    int y = (screenSize_H - setup.windowWidth) / 2;

                    rotateCWRectangle = new Rectangle(x + 300, y + 500, rotateCW.Width, rotateCW.Height);
                    rotateCCWRectangle = new Rectangle(x + 200, y + 500, rotateCCW.Width, rotateCCW.Height);
                    setup.LoadLevel();
                    imageRectangle.Width = setup.ShapeSize();
                    imageRectangle.Height = setup.ShapeSize();
                    walls.LoadLevel();
                    lasers.LoadLevel();
                    targets.LoadLevel();
                    mirror.LoadLevel();
                    laserBeam.LoadLevel();
                    loadLevel = true;
                }
                //Rotates the laser shooter according to the txt file.
                if (lasers.Direction == 1)
                {
                    lasers.Texture = startUp;
                }
                else if (lasers.Direction == 2)
                {
                    lasers.Texture = startDown;
                }
                else if (lasers.Direction == 3)
                {
                    lasers.Texture = startLeft;
                }
                else if (lasers.Direction == 4)
                {
                    lasers.Texture = startRight;
                }
                //Collision statements for the laser.
                if (isLaserShoot == true && laserBeam.Location.Intersects(mirror.Position))
                {
                    laserBeam.HitMirror(mirror.Direction);
                }
                if (laserBeam.CurrentDirection == 1 || laserBeam.CurrentDirection == 2)
                {
                    laserBeam.Texture = laser;
                }
                else if (laserBeam.CurrentDirection == 3 || laserBeam.CurrentDirection == 4)
                {
                    laserBeam.Texture = laserH;
                }
                if (isLaserShoot == true && laserBeam.HitMirrorSide == true)
                {
                    laserAnimation = false;
                    if (shootTimer <= 0)
                    {
                        life--;
                        currentState = GameStates.Failure;
                    }
                }
                //Hits the wall, fails the level
                if (isLaserShoot == true && laserBeam.Location.Intersects(walls.Position))
                {
                    laserAnimation = false;
                    walls.Texture = wallRed;
                    hitWall = true;
                    if (shootTimer <= 0)
                    {
                        life--;
                        currentState = GameStates.Failure;
                    }
                }
                //Hits the target, wins the level
                if (isLaserShoot == true && laserBeam.Location.Intersects(targets.Position))
                {
                    laserAnimation = false;
                    targets.Texture = targetFilled;
                    hitTarget = true;
                    if (shootTimer <= 0)
                    {
                        score += 10;
                        currentState = GameStates.Wins;
                    }
                }
                //Out of boundaries, fails the level
                if (isLaserShoot == true && (laserBeam.Location.X < 0 || laserBeam.Location.Y < 0
                    || laserBeam.Location.X > 500 || laserBeam.Location.Y > 500))
                {
                    laserAnimation = false;
                    if (shootTimer <= 0)
                    {
                        life--;
                        currentState = GameStates.Failure;
                    }
                }
                //Used for drag/drop
                mirror.Update(gameTime);
            }
            //Reset condition if the game isn't in the stage state
            else if (currentState != GameStates.Stage)
            {
                timer = 10.000;
                loadLevel = false;
            }
            base.Update(gameTime);
        }
        #endregion

        #region HoveringBoxes
        //Draws box based on if mouse is hovering over given rectangle or not
        private void DrawHoveringBoxes(Texture2D hovering, Texture2D notHovering, Rectangle Rect)
        {
            if (IsHovering(Mouse.GetState(), Rect))
            {
                spriteBatch.Draw(hovering, Rect, Color.White);
            }
            else
            {
                spriteBatch.Draw(notHovering, Rect, Color.White);
            }
        }
        #endregion

        #region Draw
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
                    spriteBatch.Draw(background, new Vector2(0f, 0f), Color.White);
                    spriteBatch.Draw(title, titleRectangle, Color.White);
                    DrawHoveringBoxes(boxSelected, box, controlRectangle);
                    DrawHoveringBoxes(boxSelected, box, levelRectangle);

                    spriteBatch.DrawString(mainFont, "(C)ontrols",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 - 390,
                        graphics.PreferredBackBufferHeight / 2 + 300), Color.White);

                    spriteBatch.DrawString(mainFont, "(L)evel \n select",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 + 205,
                        graphics.PreferredBackBufferHeight / 2 + 300), Color.White);

                    spriteBatch.DrawString(mainFont, "Press F1 to toggle fullscreen. Or press Backspace to quit.",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 - 480,
                        graphics.PreferredBackBufferHeight - 80), Color.White);
                    break;

                case GameStates.Control:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Draw(background, new Vector2(0f, 0f), Color.White);
                    spriteBatch.DrawString(mainFont, "Controls",
                        new Vector2((graphics.PreferredBackBufferWidth - 200) / 2, 100), Color.White);
                    spriteBatch.DrawString(mainFont, "Click and drag the mirror with the mouse," +
                        "\n\nshoot the laser with the space bar, and" +
                        " \n\nhit the target to clear the level!",
                        new Vector2(graphics.PreferredBackBufferWidth / 3, 150), Color.White);

                    DrawHoveringBoxes(boxSelected, box, levelRectangle);
                    spriteBatch.DrawString(mainFont, "(L)evel \n select",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 + 205,
                        graphics.PreferredBackBufferHeight / 2 + 300), Color.White);

                    DrawHoveringBoxes(boxSelected, box, gameRectangle);
                    spriteBatch.DrawString(mainFont, "Start level 1 \n (press space)",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 - 95,
                        graphics.PreferredBackBufferHeight / 2 + 300), Color.White);

                    //icon intro
                    spriteBatch.Draw(startUp, new Rectangle(graphics.PreferredBackBufferWidth / 3, 300, 50, 50), Color.White);
                    spriteBatch.DrawString(mainFont, "This is the Laser Shooter." + "\n" + "\n" +
                        "This will shoot the laser in the direction it is facing.",
                        new Vector2(graphics.PreferredBackBufferWidth / 3 + 100, 300), Color.White);

                    spriteBatch.Draw(target, new Rectangle(graphics.PreferredBackBufferWidth / 3, 400, 50, 50), Color.White);
                    spriteBatch.DrawString(mainFont, "This is the Target." + "\n" + "\n" +
                        "This is what the laser will hit. When hit, the level is cleared.",
                        new Vector2(graphics.PreferredBackBufferWidth / 3 + 100, 400), Color.White);

                    spriteBatch.Draw(wallBlue, new Rectangle(graphics.PreferredBackBufferWidth / 3, 500, 50, 50), Color.White);
                    spriteBatch.Draw(wallRed, new Rectangle(graphics.PreferredBackBufferWidth / 3, 550, 50, 50), Color.White);
                    spriteBatch.DrawString(mainFont, "This is the Wall.\n" + "\n" +
                        "This will block the laser and destroy it.",
                        new Vector2(graphics.PreferredBackBufferWidth / 3 + 100, 525), Color.White);

                    spriteBatch.Draw(mirrorTextureBR,
                        new Rectangle(graphics.PreferredBackBufferWidth / 3, 650, 50, 50), Color.White);
                    spriteBatch.DrawString(mainFont, "This is the Mirror.\n" + "\n" +
                        "This will reflect the laser in another direction.",
                        new Vector2(graphics.PreferredBackBufferWidth / 3 + 100, 650), Color.White);
                    spriteBatch.Draw(rotateCCW,
                        new Rectangle(graphics.PreferredBackBufferWidth / 3 - 30, 720, 50, 50), Color.White);
                    spriteBatch.Draw(rotateCW,
                        new Rectangle(graphics.PreferredBackBufferWidth / 3 + 30, 720, 50, 50), Color.White);
                    spriteBatch.DrawString(mainFont, "This can be rotated with the rotate buttons.",
                        new Vector2(graphics.PreferredBackBufferWidth / 3 + 100, 720), Color.White);
                    break;

                case GameStates.Select:
                    GraphicsDevice.Clear(Color.Black);
                    //Draws all of the buttons
                    spriteBatch.Draw(background, new Vector2(0f, 0f), Color.White);
                    spriteBatch.DrawString(mainFont, "Level select",
                        new Vector2((graphics.PreferredBackBufferWidth - 200) / 2,
                        graphics.PreferredBackBufferHeight / 3), Color.White);
                    DrawHoveringBoxes(boxSelected, box, gameRectangle);
                    spriteBatch.DrawString(mainFont, "Start level 1 \n (press space)",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 - 95,
                        graphics.PreferredBackBufferHeight / 2 + 300), Color.White);
                    DrawHoveringBoxes(boxSelected, box, levelRectangle);
                    spriteBatch.DrawString(mainFont, "(L)evel \n select",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 + 205,
                        graphics.PreferredBackBufferHeight / 2 + 300), Color.White);
                    DrawHoveringBoxes(boxSelected, box, numberRectangle);
                    spriteBatch.DrawString(mainFont, "1",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 - 130,
                        graphics.PreferredBackBufferHeight / 3 + 65), Color.White);
                    DrawHoveringBoxes(boxSelected, box, numberRectangleTwo);
                    spriteBatch.DrawString(mainFont, "2",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 - 70,
                        graphics.PreferredBackBufferHeight / 3 + 65), Color.White);
                    DrawHoveringBoxes(boxSelected, box, numberRectangleThree);
                    spriteBatch.DrawString(mainFont, "3",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 - 10,
                        graphics.PreferredBackBufferHeight / 3 + 65), Color.White);
                    DrawHoveringBoxes(boxSelected, box, numberRectangleFour);
                    spriteBatch.DrawString(mainFont, "4",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 + 50,
                        graphics.PreferredBackBufferHeight / 3 + 65), Color.White);
                    DrawHoveringBoxes(boxSelected, box, numberRectangleFive);
                    spriteBatch.DrawString(mainFont, "5",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 + 110,
                        graphics.PreferredBackBufferHeight / 3 + 65), Color.White);
                    break;

                case GameStates.Stage:
                    //Drawing the grid
                    spriteBatch.Draw(background, new Vector2(0f, 0f), Color.White);
                    setup.Draw(spriteBatch, gridTexture);
                    GraphicsDevice.Clear(Color.Aquamarine);
                    spriteBatch.DrawString(mainFont, String.Format("{0:0.000}", timer) + "\n"
                        + "Health: " + life.ToString(), new Vector2(10f, 510f), Color.Black);
                    //Draws the rotation buttons
                    if (IsHovering(Mouse.GetState(), rotateCCWRectangle))
                    {
                        spriteBatch.Draw(rotateCCW, rotateCCWRectangle, Color.Blue);
                    }
                    else
                    {
                        spriteBatch.Draw(rotateCCW, rotateCCWRectangle, Color.White);
                    }
                    if (IsHovering(Mouse.GetState(), rotateCWRectangle))
                    {
                        spriteBatch.Draw(rotateCW, rotateCWRectangle, Color.Blue);
                    }
                    else
                    {
                        spriteBatch.Draw(rotateCW, rotateCWRectangle, Color.White);
                    }
                    setup.Draw(spriteBatch, gridTexture);
                    GraphicsDevice.Clear(Color.Aquamarine);
                    int x = (screenSize_W - setup.windowWidth) / 2;
                    int y = (screenSize_H - setup.windowWidth) / 2;
                    spriteBatch.DrawString(mainFont, String.Format("{0:0.000}", timer) + "\n" + "Health: "+life.ToString(), new Vector2(x + 10f, y + 510f), Color.Black);
                    walls.Draw(spriteBatch);
                    targets.Draw(spriteBatch);
                    lasers.Draw(spriteBatch);
                    mirror.Draw(spriteBatch);
                    //Draws the laser when it's fired
                    if (isLaserShoot == true)
                    {
                        spriteBatch.DrawString(mainFont, "Shoot the laser!", new Vector2(100, 620), Color.Red);
                    }
                    if (laserAnimation == true)
                    {
                        laserBeam.DrawLaser(spriteBatch);
                    }

                    //Changes the texture of the object if the laser hits it
                    if (hitWall == true)
                    {
                        walls.Draw(spriteBatch);
                    }
                    if (hitTarget == true)
                    {
                        targets.Draw(spriteBatch);
                    }
                    break;

                case GameStates.Failure:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Draw(background, new Vector2(0f, 0f), Color.White);
                    spriteBatch.DrawString(mainFont, "FAILED!",
                        new Vector2((graphics.PreferredBackBufferWidth - 200) / 2,
                        graphics.PreferredBackBufferHeight / 3), Color.Red);
                    spriteBatch.DrawString(mainFont, "Score: " + score.ToString(),
                        new Vector2((graphics.PreferredBackBufferWidth - 200) / 2,
                        graphics.PreferredBackBufferHeight / 3 + 100), Color.Red);
                    DrawHoveringBoxes(boxSelected, box, levelRectangle);
                    spriteBatch.DrawString(mainFont, "(L)evel \n select",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 + 205,
                        graphics.PreferredBackBufferHeight / 2 + 300), Color.White);
                    if (life > 0)
                    {
                        DrawHoveringBoxes(boxSelected, box, gameRectangle);
                        spriteBatch.DrawString(mainFont, "Restart\n (press space)",
                            new Vector2(graphics.PreferredBackBufferWidth / 2 - 95,
                            graphics.PreferredBackBufferHeight / 2 + 300), Color.Red);
                    }
                    break;

                case GameStates.Wins:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Draw(backgroundRed, new Vector2(0f, 0f), Color.White);
                    spriteBatch.DrawString(mainFont, "You win!",
                        new Vector2((graphics.PreferredBackBufferWidth - 200) / 2,
                        graphics.PreferredBackBufferHeight / 3), Color.White);
                    spriteBatch.DrawString(mainFont, "Score: " + score.ToString(),
                        new Vector2((graphics.PreferredBackBufferWidth - 200) / 2,
                        graphics.PreferredBackBufferHeight / 3 + 100), Color.White);
                    DrawHoveringBoxes(boxRedSelected, boxRed, levelRectangle);
                    spriteBatch.DrawString(mainFont, "(L)evel \n select",
                        new Vector2(graphics.PreferredBackBufferWidth / 2 + 205,
                        graphics.PreferredBackBufferHeight / 2 + 300), Color.White);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
        #endregion
    }
}
