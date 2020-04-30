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
    class LaserBean
    {
        Rectangle location;
        
        public Rectangle Location 
        {
            get { return location; }
            set { location = value; }
        }


    }
}
