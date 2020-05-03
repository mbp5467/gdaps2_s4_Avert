using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avert
{
    class LaserBeam
    {
        //Gets the location and orientation of the starting point
        private Rectangle shooterLocation;
        private int shooterDirection;
        private int currentDirection;
        private Rectangle location;
        GameConfig game = new GameConfig();
        private Texture2D texture;
        //Used to determine if the laser hit the side of the mirror instead of the diagonal
        private bool hitMirrorSide;
        private int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public Rectangle ShooterLocation
        {
            get { return shooterLocation; }
            set { shooterLocation = value; }
        }
        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }
        public int ShooterDirection
        {
            get { return shooterDirection; }
            set { shooterDirection = value; }
        }
        public int CurrentDirection
        {
            get { return currentDirection; }
            set { currentDirection = value; }
        }
        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }
        public bool HitMirrorSide
        {
            get { return hitMirrorSide; }
            set { hitMirrorSide = value; }
        }

        public LaserBeam(Texture2D t)
        {
            this.texture = t;
            hitMirrorSide = false;
        }

        //Method for LoadLevel which receives information about the layout of the level.
        public void LoadLevel()
        {
            game.Level = level;
            game.LoadLevel();
        }

        //Begins shooting the laser
        public void ShootLaser()
        {
            if (location.X == ShooterLocation.X && location.Y == ShooterLocation.Y)
            {
                //Initializes the laser's location and orientation
                if (shooterDirection == 1)
                {
                    location.Y -= game.ShapeSize() / 2;
                    currentDirection = 1;
                }
                else if (shooterDirection == 2)
                {
                    location.Y += game.ShapeSize() / 2;
                    currentDirection = 2;
                }
                else if (shooterDirection == 3)
                {
                    location.X -= game.ShapeSize() / 2;
                    currentDirection = 3;
                }
                else if (shooterDirection == 4)
                {
                    location.X += game.ShapeSize() / 2;
                    currentDirection = 4;
                }
            }
        }

        //The laser hits the mirror and bounces off of it accordingly
        public void HitMirror(int mirrorDirection)
        {
            switch (mirrorDirection)
            {
                case 1:
                    //Laser going down, bounce to the right
                    if (currentDirection == 2)
                    {
                        currentDirection = 4;
                        location.X += game.ShapeSize() / 2;
                        location.Y += game.ShapeSize() / 2;
                    }
                    //Laser going left, bounce up
                    else if (currentDirection == 3)
                    {
                        currentDirection = 1;
                        location.X -= game.ShapeSize() / 2;
                        location.Y -= game.ShapeSize() / 2;
                    }
                    else
                    {
                        hitMirrorSide = true;
                    }
                    break;

                case 2:
                    //Laser going up, bounce to the right
                    if (currentDirection == 1)
                    {
                        currentDirection = 4;
                        location.X += game.ShapeSize() / 2;
                        location.Y -= game.ShapeSize() / 2;
                    }
                    //Laser going to the left, bounce down
                    else if (currentDirection == 3)
                    {
                        currentDirection = 2;
                        location.X -= game.ShapeSize() / 2;
                        location.Y += game.ShapeSize() / 2;
                    }
                    else
                    {
                        hitMirrorSide = true;
                    }
                    break;

                case 3:
                    //Laser going up, bounce to the left
                    if (currentDirection == 1)
                    {
                        currentDirection = 3;
                        location.X -= game.ShapeSize() / 2;
                        location.Y -= game.ShapeSize() / 2;
                    }
                    //Laser going to the right, bounce down
                    else if (currentDirection == 4)
                    {
                        currentDirection = 2;
                        location.X += game.ShapeSize() / 2;
                        location.Y += game.ShapeSize() / 2;
                    }
                    else
                    {
                        hitMirrorSide = true;
                    }
                    break;

                case 4:
                    //Laser going down, bounce to the left
                    if (currentDirection == 2)
                    {
                        currentDirection = 3;
                        location.X -= game.ShapeSize() / 2;
                        location.Y += game.ShapeSize() / 2;
                    }
                    //Laser going right, bounce up
                    else if (currentDirection == 4)
                    {
                        currentDirection = 1;
                        location.X += game.ShapeSize() / 2;
                        location.Y -= game.ShapeSize() / 2;
                    }
                    else
                    {
                        hitMirrorSide = true;
                    }
                    break;
            }
        }

        //Draws the laser as it's being fired. Changes the position every time it's drawn
        //so it's actually moving.
        public void DrawLaser(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, Color.White);
            if (currentDirection == 1)
            {
                location.Y -= game.ShapeSize();
            }
            if (currentDirection == 2)
            {
                location.Y += game.ShapeSize();
            }
            if (currentDirection == 3)
            {
                location.X -= game.ShapeSize();
            }
            if (currentDirection == 4)
            {
                location.X += game.ShapeSize();
            }
        }
    }
}
