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
    class GameConfig
    {

        //Set the grid size
        // the number can be changed depends on the level(future)
        int gridSize_W = 10;
        int gridSize_H = 10;

        //set the size for each tile
        int tileSize = 45;

        Rectangle gridPosition;

        //Draw the grid
        public void Draw(SpriteBatch spritebatch,Texture2D gridTexture) 
        {
            /* 
             */
            for (int i = 0; i < gridSize_W; i++) 
            {
                for (int j = 0; j < gridSize_H; j++) 
                {
                   
                    gridPosition = new Rectangle(i * tileSize, j * tileSize,tileSize,tileSize);
                    spritebatch.Draw(gridTexture,gridPosition,Color.White);
                }
            }
        }


        
    }
}
