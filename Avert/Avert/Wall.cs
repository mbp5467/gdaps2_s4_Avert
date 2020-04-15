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
    /* This is child class of stable shape
     * Wall will block the laser*/
    class Wall : StableShape
    {
        //Fields for the wall, being a spriteBatch object,
        //a rectangle for location, an array of strings, and 
        //a new Game object
        SpriteBatch spriteBatch;
        Rectangle location;
        string[] data;
        GameConfig game = new GameConfig();

        //Paramterized construcor that sets the position property to a
        //location and calls LoadLevel on the created game object
        public Wall(Texture2D t)
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

        //Method for drawing the wall
        public override void Draw(SpriteBatch spriteBatch)
        {

            location = new Rectangle(3 * game.tileSize, 1 * game.tileSize, game.ShapeSize(), game.ShapeSize());
            spriteBatch.Draw(texture, location, Color.White);

            if (active == true)
            {

                for (int k = 0; k < data.Length; k++)
                {
                    for (int i = 0; i < game.gridSize_W; i++)
                    {
                        for (int j = 0; j < game.gridSize_H; j++)
                        {
                            location = new Rectangle(i * game.tileSize, j * game.tileSize, game.ShapeSize(), game.ShapeSize());
                            spriteBatch.Draw(texture, location, Color.White);
                        }

                    }

                }


                /*foreach (string s in data)
                {
                    for (int i = 0; i < game.gridSize_W; i++)
                    {
                        for (int j = 0; j < game.gridSize_H; j++)
                        {
                            if ( == "-")
                            {
                                location = new Rectangle(i * game.tileSize, j * game.tileSize, game.ShapeSize(), game.ShapeSize());
                                spriteBatch.Draw(texture, location, Color.White);
                            }
                        }
                    }

                }*/
            }
        }
    }
}
    
