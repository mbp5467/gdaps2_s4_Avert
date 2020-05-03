﻿using System;
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

    /* This is child class of stable shape
     * Laser is the start point of the game*/
    class Laser:StableShape
    {
        //Fields for the laser class, being 
        //a rectangle location, a data array for saving, 
        //and a new game object.
        Rectangle location;
        string[] data;
        GameConfig game = new GameConfig();
        //1 = up, 2 = down, 3 = left, 4 = right
        int direction = 1;

        public int Direction
        {
            get { return direction; }
        }

        public Rectangle Location
        {
            get { return location; }
            set { location = value; }
        }

        //Paramaterized constructor that sets
        //the position and calls the game's loadLevel method
        public Laser(Texture2D t)
            : base(t)
        {
            Position = location;
            game.LoadLevel();
        }

        //Method for LoadLevel which sets a fileName
        //and creates a FileInfo object. It checks if levels exists,
        //and if it does a new StreamReader is created. While the
        //created line is not null, it reads data from the file.
        //If the data is [/], active is set to true.
        public override void LoadLevel()
        {
            string filename = "Levels.txt";
            FileInfo levels = new FileInfo(filename);
            if (levels.Exists)
            {
                StreamReader levelReader = new StreamReader(filename);
                string line = null;
                while ((line = levelReader.ReadLine()) != null)
                {
                    data = line.Split(',');
                    if (data[0] == "/")
                    {
                        active = true;
                        break;
                    }
                }
                levelReader.Close();
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            int x = (Game1.screenSize_W - game.windowWidth) / 2;
            int y = (Game1.screenSize_H - game.windowWidth) / 2;

            if (active == true)
            {

                for (int i = 0; i < game.gridSize_W; i++)
                {
                    for (int j = 0; j < game.gridSize_H; j++)
                    {
                        int coordinate = (j + 1) * game.gridSize_W + i;
                        if (data[coordinate] == "11" || data[coordinate] == "12" || data[coordinate] == "13" || data[coordinate] == "14")
                        {
                            location = new Rectangle(x + i * game.tileSize, y + j * game.tileSize, game.ShapeSize(), game.ShapeSize());
                            if (data[coordinate] == "11")
                            {
                                direction = 1;
                            }
                            else if (data[coordinate] == "12")
                            {
                                direction = 2;
                            }
                            else if (data[coordinate] == "13")
                            {
                                direction = 3;
                            }
                            else if (data[coordinate] == "14")
                            {
                                direction = 4;
                            }
                            spriteBatch.Draw(texture, location, Color.White);
                        }

                    }
                }
            }
        }
    }
}
