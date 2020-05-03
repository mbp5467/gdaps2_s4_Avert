using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avert
{
    //This class is the parent class for pre-position shape

    abstract class StableShape
    {
        //Field & properties
        protected Texture2D texture;
        protected Rectangle position;
        GameConfig game = new GameConfig();
        protected int level;
        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        // Store the position in rectagle for collision later
        //and changing the active value using getter and
        //setter properties
        public Rectangle Position
        {
            get { return position; }

            set { position = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        //Constuctor
        protected StableShape(Texture2D t)
        {
            this.texture = t;
        }

        //Update the draw when laser hit
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Black);
        }

        //Load the positions of each StableShape in each level
        public virtual void LoadLevel()
        {
            game.Level = level;
            game.LoadLevel();
        }
    }
}
