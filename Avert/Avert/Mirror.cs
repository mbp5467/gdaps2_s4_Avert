using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avert
{
    /* Child class of MoveableShape
     * Mirror will reflect the laser beam at 90 degree angles
     */
    class Mirror : MoveableShape
    {
        //New GameConfig for size of the shape
        GameConfig shapeSize = new GameConfig();
        //1 = TopRight, 2 = BottomRight, 3 = BottomLeft, 4 = TopLeft
        int direction = 2;
        private int level;

        public Rectangle Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public int Level
        {
            get { return level; }
            set { level = value; }
        }

        //Parameterized constructor that receives the texture of the object
        //and its current position
        public Mirror(Texture2D t, Rectangle r) : base(t, r)
        {

        }

        //This is for future levels that require more
        //than one mirror; never implemented
        int numberOfMirrors = 0;

        public int NumberOfMirrors
        {
            get { return numberOfMirrors; }
        }

        //Method for LoadLevel which receives information about the layout of the level.
        public override void LoadLevel()
        {
            shapeSize.Level = level;
            shapeSize.LoadLevel();
            numberOfMirrors = shapeSize.NumberOfMirrors;
        }

        //Update method which change the width and height and allows
        //user input
        public override void Update(GameTime gameTime)
        {
            position.Width = shapeSize.ShapeSize();
            position.Height = shapeSize.ShapeSize();
            ProcessInput(level);
        }
    }
}
