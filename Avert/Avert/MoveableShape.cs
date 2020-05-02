using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Avert
{
    // This class is the user control shape's parent class

    abstract class MoveableShape
    {
        //Fields & properties
        protected Texture2D texture;
        protected Rectangle position;
        protected bool active = false;

        private MouseState previousMouseState;
        private bool isDragAndDropping;

        protected StableShape stableShape;
        // Store the position in rectagle for collision later
        // the postion could be changed due to the movement
        public Rectangle Position 
        {
            get { return position; }

            set { position = value; }
        }

        public bool Active 
        {
            get { return active; }
            set { active = value; }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        //Constructor
        protected MoveableShape(Texture2D texture, Rectangle position) 
        {
            this.texture = texture;
            this.position = position;
            previousMouseState = Mouse.GetState();
            isDragAndDropping = false;
        }

        //Loads each movable shape
        public abstract void LoadLevel();

        public virtual void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        // moveable shape will have a update location when moving
        public abstract void Update(GameTime gameTime);

        //Method for processing input
        public virtual void ProcessInput()
        {
            MouseState mState = Mouse.GetState();
            GameConfig setup = new GameConfig();
            setup.LoadLevel();

            //Determines if the mouse button is being held down and if the mouse is hovering over the object.
            if (mState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Pressed
                && mState.Position.X > position.X && mState.Position.X < (position.X + position.Width)
                && mState.Position.Y > position.Y && mState.Position.Y < (position.Y + position.Height))
            {
                isDragAndDropping = true;
            }

            //Drags the object in accordance to the mouse's changing position.
            //The dragging effect is set up so the object moves in the direction of the mouse's current position from the mouse's previous position.
            if (isDragAndDropping)
            {
                int xDifference = mState.Position.X - previousMouseState.Position.X;
                int yDifference = mState.Position.Y - previousMouseState.Position.Y;
                position.X += xDifference;
                position.Y += yDifference;
            }

            //Turns off the dragging effect if the mouse button is released and snaps moving shapes onto the spots.
            if (mState.LeftButton == ButtonState.Released && previousMouseState.LeftButton == ButtonState.Released
                && isDragAndDropping == true)
            {
                foreach (Rectangle gridSpot in setup.GridTiles())
                {
                    if (mState.X >= gridSpot.X && mState.X < gridSpot.X + gridSpot.Width
                        && mState.Y >= gridSpot.Y && mState.Y < gridSpot.Y + gridSpot.Height)
                    {
                        position.X = gridSpot.X + 5;
                        position.Y = gridSpot.Y + 5;
                    }
                }
                isDragAndDropping = false;
            }
            previousMouseState = mState;
        }
    }
}
