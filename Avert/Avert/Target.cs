using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avert
{
    /* Child class of StableShape.
     * Target is the ending point of the game.*/
    class Target : StableShape
    {
        //Creating the fields for the target
        GameConfig game = new GameConfig();

        //Parameterized constructor that receives the texture of the object
        public Target(Texture2D t)
            : base(t)
        {

        }

        //Method for LoadLevel which receives information about the layout of the level.
        public override void LoadLevel()
        {
            game.Level = level;
            game.LoadLevel();
        }

        //Method for drawing the target
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < game.gridSize_W; i++)
            {
                for (int j = 0; j < game.gridSize_H; j++)
                {
                    int coordinate = (j + 1) * game.gridSize_W + i;
                    if (game.data[coordinate] == "9")
                    {
                        position = new Rectangle(i * game.tileSize, j * game.tileSize, game.ShapeSize(), game.ShapeSize());
                        spriteBatch.Draw(texture, position, Color.White);
                    }
                }
            }
        }
    }
}
