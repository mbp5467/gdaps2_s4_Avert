using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Level_Editor
{
    public partial class levelEditor : Form
    {
        int x;
        int y;
        int tileSize;

        List<ComboBox> gridSpots = new List<ComboBox>();
        List<ComboBox> directions = new List<ComboBox>();

        public levelEditor()
        {
            InitializeComponent();
        }

        private void ChangeGridSize_Click(object sender, EventArgs e)
        {
            //Once the grid changes, any old grid is removed
            foreach (ComboBox spot in gridSpots)
            {
                Controls.Remove(spot);
            }
            gridSpots.Clear();
            foreach (ComboBox direction in directions)
            {
                Controls.Remove(direction);
            }
            directions.Clear();

            //Gets the amount of spots in the x and y direction
            if (gridX.Items.Contains(gridX.Text))
            {
                x = int.Parse(gridX.Text);
            }
            if (gridY.Items.Contains(gridY.Text))
            {
                y = int.Parse(gridY.Text);
            }

            //Makes the size of each tile on the grid
            if (y < 6 && x < 6)
            {
                tileSize = 80;
            }
            else
            {
                if (x > y)
                {
                    tileSize = 400 / x;
                }
                else
                {
                    tileSize = 400 / y;
                }
            }

            //Creates each tile for the grid
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    ComboBox newGridSpot = new ComboBox();
                    //directions is used for the orientation of certain pieces
                    ComboBox direction = new ComboBox();
                    if (i == 0 && j == 0)
                    {
                        newGridSpot.Location = new Point(180, 25);
                    }
                    else
                    {
                        newGridSpot.Location = new Point(180 + tileSize * j, 25 + tileSize * i);
                    }
                    direction.Location = new Point(newGridSpot.Location.X, newGridSpot.Location.Y + newGridSpot.Height);

                    newGridSpot.AutoSize = false;
                    newGridSpot.Width = tileSize;
                    newGridSpot.Height = tileSize;
                    newGridSpot.Visible = true;
                    newGridSpot.Items.Add("Laser");
                    newGridSpot.Items.Add("Target");
                    newGridSpot.Items.Add("Wall");
                    Controls.Add(newGridSpot);

                    direction.AutoSize = false;
                    direction.Width = tileSize;
                    direction.Height = tileSize;
                    direction.Visible = true;
                    direction.Items.Add("↑");
                    direction.Items.Add("↓");
                    direction.Items.Add("←");
                    direction.Items.Add("→");
                    Controls.Add(direction);

                    //Puts all the tile spots and directions in their lists
                    gridSpots.Add(newGridSpot);
                    directions.Add(direction);
                }
            }
        }

        /// <summary>
        /// Used to manage changes in the code-generated grid comboboxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //If the spot is going to have a laser shooter, the user can
            //set the direction it's facing
            for (int i = 0; i < gridSpots.Count; i++)
            {
                if (gridSpots[i].Items.Contains(gridSpots[i].Text) && gridSpots[i].Text == "Laser")
                {
                    directions[i].Enabled = true;
                }
                else
                {
                    directions[i].SelectedIndex = 0;
                    directions[i].Text = "";
                    directions[i].Enabled = false;
                }
            }
        }

        /// <summary>
        /// Saves the game to the Levels file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button1_Click(object sender, EventArgs e)
        {
            //Checks for any possible errors
            bool levelExists = int.TryParse(levelNumber.Text, out int level);
            bool moveExists = int.TryParse(mirrorsAllowed.Text, out int move);
            bool laserExists = false;
            int numberOfLasers = 0;
            int numberOfTargets = 0;
            bool laserDirectionSet = false;
            bool targetExists = false;
            int gridSizeX = 5;
            int gridSizeY = 5;
            if (gridX.Items.Contains(gridX.Text))
            {
                gridSizeX = int.Parse(gridX.Text);
            }
            if (gridY.Items.Contains(gridY.Text))
            {
                gridSizeY = int.Parse(gridY.Text);
            }
            for (int i = 0; i < gridSpots.Count; i++)
            {
                if (gridSpots[i].Items.Contains(gridSpots[i].Text) && gridSpots[i].Text == "Laser")
                {
                    laserExists = true;
                    numberOfLasers++;
                    if (directions[i].Items.Contains(directions[i].Text) && directions[i].Text != "")
                    {
                        laserDirectionSet = true;
                    }
                }
                if (gridSpots[i].Items.Contains(gridSpots[i].Text) && gridSpots[i].Text == "Target")
                {
                    numberOfTargets++;
                }
            }
            foreach (ComboBox gridSpot in gridSpots)
            {
                if (gridSpot.Items.Contains(gridSpot.Text) && gridSpot.Text == "Target")
                {
                    targetExists = true;
                }
            }
            if (!levelExists || levelNumber.Text == "" || level <= 0
                || laserExists == false
                || gridSizeX != 5 || gridSizeY != 5
                || numberOfLasers > 1
                || numberOfTargets > 1
                || laserDirectionSet == false
                || targetExists == false
                || !moveExists || mirrorsAllowed.Text == "" || move <= 0)
            {
                string errorMessage = "Couldn't save the level. Contains the following errors:\n";
                string lvlError = "- Level number must be from 1 to 5\n";
                string gridError = "- Grid size must be 5 x 5\n";
                string laserError = "- No laser on board\n";
                string targetError = "- No target on board\n";
                string placeError = "- Number of movable objects must be 1";
                string tooManyLasers = "- More than one laser is set\n";
                string tooManyTargets = "- More than one larget is set\n";
                string noDirectionSet = "- Laser doesn't have a set direction\n";

                if (!levelExists || levelNumber.Text == "" || level <= 0 || level > 5)
                {
                    errorMessage = errorMessage + lvlError;
                }
                if (gridSizeX != 5 || gridSizeY != 5)
                {
                    errorMessage = errorMessage + gridError;
                }
                if (laserExists == false)
                {
                    errorMessage = errorMessage + laserError;
                }
                else if (numberOfLasers > 1)
                {
                    errorMessage = errorMessage + tooManyLasers;
                }
                else if (numberOfTargets > 1)
                {
                    errorMessage = errorMessage + tooManyTargets;
                }
                else if (laserDirectionSet == false)
                {
                    errorMessage = errorMessage + noDirectionSet;
                }
                if (targetExists == false)
                {
                    errorMessage = errorMessage + targetError;
                }
                if (!moveExists || mirrorsAllowed.Text == "" || move <= 0 || move > 1)
                {
                    errorMessage = errorMessage + placeError;
                }
                MessageBox.Show(errorMessage, "Error");
            }




            else
            {
                //KEY:
                //0 = empty space
                //1 = laser shooter; number following is different directions
                //1 = up, 2 = down, 3 = left, 4 = right
                //9 = target
                //- = wall
                //This was made with the idea that we should use 
                //multiple files for the levels so it's easier to load them in
                string filename = "..\\..\\..\\..\\..\\Avert\\Avert\\Levels.txt";

                List<string> levelList = File.ReadAllLines(filename).ToList();
                string levelToRemove = "";
                foreach (string line in levelList)
                {
                    if (line.Contains("/," + levelNumber.Text))
                    {
                        levelToRemove = line;
                    }
                }
                levelList.Remove(levelToRemove);
                File.WriteAllLines(filename, levelList.ToArray());

                string levelWriter = "";
                levelWriter = ("/," + levelNumber.Text + "," + x + "," + y + "," + mirrorsAllowed.Text + ",");
                for (int i = 0; i < (x * y - 1); i++)
                {
                    if (gridSpots[i].Text == "Laser")
                    {
                        levelWriter += "1";
                        if (directions[i].Text == "↑")
                        {
                            levelWriter += "1";
                        }
                        else if (directions[i].Text == "↓")
                        {
                            levelWriter += "2";
                        }
                        else if (directions[i].Text == "←")
                        {
                            levelWriter += "3";
                        }
                        else if (directions[i].Text == "→")
                        {
                            levelWriter += "4";
                        }
                    }
                    else if (gridSpots[i].Text == "Target")
                    {
                        levelWriter += "9";
                    }
                    else if (gridSpots[i].Text == "Wall")
                    {
                        levelWriter += "-";
                    }
                    else
                    {
                        levelWriter += "0";
                    }
                    levelWriter += ",";
                }

                if (gridSpots[x * y - 1].Text == "Laser")
                {
                    levelWriter += "1";
                    if (directions[x * y - 1].Text == "↑")
                    {
                        levelWriter += "1";
                    }
                    else if (directions[x * y - 1].Text == "↓")
                    {
                        levelWriter += "2";
                    }
                    else if (directions[x * y - 1].Text == "←")
                    {
                        levelWriter += "3";
                    }
                    else if (directions[x * y - 1].Text == "→")
                    {
                        levelWriter += "4";
                    }
                }
                else if (gridSpots[x * y - 1].Text == "Target")
                {
                    levelWriter += "9";
                }
                else if (gridSpots[x * y - 1].Text == "Wall")
                {
                    levelWriter += "-";
                }
                else
                {
                    levelWriter += "0";
                }
                levelList.Add(levelWriter);
                File.WriteAllLines(filename, levelList.ToArray());
            }
        }
    }
}
