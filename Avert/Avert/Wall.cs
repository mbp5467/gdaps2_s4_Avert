using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Avert
{
    /* Child class of StableShape
     * Wall will block the laser*/
    class Wall : StableShape
    {
        //Fields for the wall
        GameConfig game = new GameConfig();
        //List used in case there's multiple walls in a level
        List<Rectangle> walls = new List<Rectangle>();
        public List<Rectangle> ListOfWalls
        {
            get { return walls; }
        }

        //Parameterized constructor that receives the texture of the object
        public Wall(Texture2D t)
            : base(t)
        {

        }

        //Method for LoadLevel which receives information about the layout of the level.
        public override void LoadLevel()
        {
            game.Level = level;
            game.LoadLevel();
        }

        //Method for drawing the wall
        public override void Draw(SpriteBatch spriteBatch)
        {
            //Used to draw the wall if it's in the level
            //Clears the walls list first so drawn walls don't interfere with
            //other levels later on
            walls.Clear();
            for (int i = 0; i < game.gridSize_W; i++)
            {

                for (int j = 0; j < game.gridSize_H; j++)
                {
                    int coordinate = (j + 1) * game.gridSize_W + i;
                    if (game.data[coordinate] == "-")
                    {
                        position = new Rectangle(i * game.tileSize, j * game.tileSize, game.ShapeSize(), game.ShapeSize());
                        walls.Add(position);
                        spriteBatch.Draw(texture, position, Color.White);
                    }
                }
            }
        }
    }
}

