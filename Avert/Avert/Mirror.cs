using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Avert
{
    /* Child class of moveableshape
     * Mirror will reflact the laser bean with X degree of angle
     */
    class Mirror:MoveableShape
    {
        public Mirror (Texture2D t, Rectangle r):base (t, r)
        {
            active = false;
        }
        int numberOfMirrors = 0;
        public override void LoadLevel()
        {
            string filename = "Levels.txt";
            FileInfo levels = new FileInfo(filename);
            if (levels.Exists)
            {
                StreamReader levelReader = new StreamReader(filename);
                string line = null;
                while ((line = levelReader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    if (data[0] == "/" && data[4] != "0")
                    {
                        active = true;
                        numberOfMirrors = int.Parse(data[4]);
                        break;
                    }
                }
                levelReader.Close();
            }
        }
    }
}
