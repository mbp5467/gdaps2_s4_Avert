using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avert
{
    /* This class is the user control shape's parent class
     * 
     */
    abstract class MoveableShape
    {
        //Field & properties
        protected Texture2D texture;
        protected Rectangle position;
        protected bool active = false;
        
        // Store the position in rectagle for collision later
        // the postion could be changed due to the movement
        public Rectangle Position 
        {
            get { return position; }

            set { position = value; }
        }

        //
        public bool Active 
        {
            get { return active; }
            set { active = value; }
        }

        //Constructor
        protected MoveableShape(Texture2D t, Rectangle r) 
        {
            this.texture = t;
            this.position = r;
        }

        //Load the number of moveable shape
        public abstract void LoadLevel();


        //Update the draw when laser hit
        public virtual void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        // moveable shape will have a update location when moving
        public abstract void Update(GameTime gameTime);

    }
}
