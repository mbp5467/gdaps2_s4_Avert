using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Avert
{
    class GameConfig
    {
        private int level;
        private int numberOfMirrors;
        //Set the grid size
        // the number can be changed depends on the level (not implemented)
        public int gridSize_W { get; set; }
        public int gridSize_H { get; set; }
        public int tileSize { get; set; }
        int windowWidth = 500;

        public string[] data { get; set; }
        //Receives a level number from Game1 and is used for everything
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public int NumberOfMirrors
        {
            get { return numberOfMirrors; }
            set { numberOfMirrors = value; }
        }

        /// <summary>
        /// Loads the level. Gets the size of the grid.
        /// </summary>
        public void LoadLevel()
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
                    if (data[0] == "/" && data[1] == level.ToString())
                    {
                        gridSize_W = int.Parse(data[2]);
                        gridSize_H = int.Parse(data[3]);
                        //set the size for each tile
                        tileSize = windowWidth / gridSize_W;
                        numberOfMirrors = int.Parse(data[4]);
                        break;
                    }
                }
                levelReader.Close();
            }
        }

        Rectangle gridPosition;

        //Draws the grid
        public void Draw(SpriteBatch spritebatch, Texture2D gridTexture)
        {
            for (int i = 0; i < gridSize_W; i++)
            {
                for (int j = 0; j < gridSize_H; j++)
                {
                    gridPosition = new Rectangle(i * tileSize, j * tileSize, tileSize, tileSize);
                    spritebatch.Draw(gridTexture, gridPosition, Color.White);
                }
            }
        }

        //Allows other classes to use the spots on the grid.
        public List<Rectangle> GridTiles()
        {
            List<Rectangle> spots = new List<Rectangle>();
            for (int i = 0; i < gridSize_W; i++)
            {
                for (int j = 0; j < gridSize_H; j++)
                {
                    gridPosition = new Rectangle(i * tileSize, j * tileSize, tileSize, tileSize);
                    spots.Add(gridPosition);
                }
            }
            return spots;
        }

        //Adjusts the size of the shapes according to the size of the grid
        public int ShapeSize()
        {
            return windowWidth / gridSize_W;
        }
    }
}
