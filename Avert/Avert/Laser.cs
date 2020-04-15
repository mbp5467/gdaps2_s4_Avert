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
     * Laser is the start point of the game*/
    class Laser:StableShape
    {
        //Fields for the laser class, bring a spriteBatch
        //for the laser, a rectangle location, a data
        //array for saving, and a new game object.
        SpriteBatch spriteBatch;
        Rectangle location;
        string[] data;
        GameConfig game = new GameConfig();

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

            location = new Rectangle(1 * game.tileSize, 1 * game.tileSize, game.ShapeSize(), game.ShapeSize());
            spriteBatch.Draw(texture, location, Color.White);
        }
    }
    
}
