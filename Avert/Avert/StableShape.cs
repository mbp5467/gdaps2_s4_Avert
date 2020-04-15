using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avert
{
    /*This class is the parent class for pre-position shape
     */
    abstract class StableShape
    {
        //Field & properties
        protected Texture2D texture;
        protected Rectangle position;
        protected bool active = false;

        // Store the position in rectagle for collision later
        //and changing the active value using getter and
        //setter properties
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

        //Load the position of stableShape in each level
        public abstract void LoadLevel();

        
    }
}
