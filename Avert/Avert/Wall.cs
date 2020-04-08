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
        int level = 1;
        int gridHeight;
        int gridWidth;
        int numberOfWalls = 0;
        int count = 0;

        public Wall(Texture2D t)
            :base(t)
        { 

        }

        public override void LoadLevel()
        {
            string filename = "Levels.txt";
            FileInfo levels = new FileInfo(filename);
            if (levels.Exists) 
            {
                StreamReader levelReader = new StreamReader(filename);
                string line = null;
                int[,] coordinates;
                while ((line = levelReader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    if (data[0] == "/" && data[1] == level.ToString())
                    {
                        gridWidth = int.Parse(data[2]);
                        gridHeight = int.Parse(data[3]);
                    }
                    coordinates = new int[gridWidth, gridHeight];
                    if (data[0] != "/" && gridHeight > 0)
                    {
                        for (int i = 0; i < gridWidth; i++)
                        {
                            if (data[i] == "-")
                            {
                                coordinates[int.Parse(data[i]), count] = 1;
                            }
                        }
                        count++;
                    }
                    if (count == gridHeight)
                    {
                        break;
                    }
                }
                levelReader.Close();
            }
        }
    }
}
