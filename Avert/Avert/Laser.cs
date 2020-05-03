using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avert
{

    /* Child class of StableShape
     * Laser is the start point of the game*/
    class Laser : StableShape
    {
        //Fields for the laser class
        GameConfig game = new GameConfig();
        //1 = up, 2 = down, 3 = left, 4 = right
        int direction = 1;

        public int Direction
        {
            get { return direction; }
        }

        //Parameterized constructor that receives the texture of the object
        public Laser(Texture2D t)
            : base(t)
        {

        }

        //Method for LoadLevel which receives information about the layout of the level.
        public override void LoadLevel()
        {
            game.Level = level;
            game.LoadLevel();
        }

        //Method for drawing the image and getting the direction of the laser shooter
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < game.gridSize_W; i++)
            {
                for (int j = 0; j < game.gridSize_H; j++)
                {
                    int coordinate = (j + 1) * game.gridSize_W + i;
                    if (game.data[coordinate] == "11" || game.data[coordinate] == "12" || game.data[coordinate] == "13" || game.data[coordinate] == "14")
                    {
                        if (game.data[coordinate] == "11")
                        {
                            direction = 1;
                        }
                        else if (game.data[coordinate] == "12")
                        {
                            direction = 2;
                        }
                        else if (game.data[coordinate] == "13")
                        {
                            direction = 3;
                        }
                        else if (game.data[coordinate] == "14")
                        {
                            direction = 4;
                        }
                        position = new Rectangle(i * game.tileSize, j * game.tileSize, game.ShapeSize(), game.ShapeSize());
                        spriteBatch.Draw(texture, position, Color.White);
                    }

                }
            }
        }
    }
}
