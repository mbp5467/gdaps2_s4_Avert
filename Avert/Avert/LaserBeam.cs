using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Avert
{
    class LaserBeam
    {
        //Gets the location of the starting point
        private Rectangle shooterLocation;
        private int shooterDirection;
        private int currentDirection;
        private Rectangle location;
        GameConfig game = new GameConfig();

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

        public LaserBeam()
        {
            location = shooterLocation;
        }

        public void ShootLaser()
        {
            if (location == ShooterLocation)
            {
                if (shooterDirection == 1)
                {
                    location.Y -= game.ShapeSize() / 2;
                }
                else if (shooterDirection == 2)
                {
                    location.Y += game.ShapeSize() / 2;
                }
                else if (shooterDirection == 3)
                {
                    location.X -= game.ShapeSize() / 2;
                }
                else if (shooterDirection == 4)
                {
                    location.X += game.ShapeSize() / 2;
                }
            }
        }

        public void HitMirror(int mirrorDirection)
        {
            switch (mirrorDirection)
            {

            }
        }

        public void DrawLaser(SpriteBatch spriteBatch)
        {

        }
    }
}
