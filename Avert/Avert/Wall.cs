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
    class Wall :StableShape
    {
        SpriteBatch spriteBatch;
        string[] data;
        string line = null;
        Rectangle location;
        GameConfig game;

        public Wall(Texture2D t)
            :base(t)
        {
            Position = location;
        }

        public override void LoadLevel()
        {
            string filename = "Levels.txt";
            FileInfo levels = new FileInfo(filename);
            if (levels.Exists) 
            {
                StreamReader levelReader = new StreamReader(filename);
                while ((line = levelReader.ReadLine()) != null)
                {
                    data = line.Split(',');
                    if (data[0] == "/" && data[4] != "0")
                    {
                        active = true;
                        Draw(spriteBatch);
                        break;
                    }
                }
                levelReader.Close();
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (active == true)
            {
                
                foreach (string s in data) 
                {
                    for (int i = 0; i < game.gridSize_W; i++)
                    {
                        for (int j = 0; j < game.gridSize_H; j++)
                        {
                            if (s == "-")
                            {
                                location = new Rectangle(i * game.tileSize, j * game.tileSize, game.ShapeSize(), game.ShapeSize());
                                spriteBatch.Draw(texture, location, Color.White);
                            }
                        }
                    }
                    
                }
            }

        }

        
    }
    
}
