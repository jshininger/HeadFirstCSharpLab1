using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Lab1_ADayAtTheRaces
{
    public class Greyhound
    {
        public int StartingPosition;        // Where my PictureBox starts
        static public int RacetrackLength;      // How long the racetrack is
        public PictureBox MyPictureBox = null;  // My PictureBox object
        public int Location = 0;            // My Location on the racetrack
        static public Random Randomizer;    // An instance of Random - Made static to ensure
                                            // I only have one instance of random

        public bool Run()
        {
            // Move forward either 1, 2, 3, or 4 spaces at random
            // Update the position of my PictureBox on the form
            // Return true if I won the race
            int distance = Randomizer.Next(1, 5);
            
            Location += distance;

            // Ensures the dog's nose doesn't go over the finish line, even if the dog does.
            if (Location > (RacetrackLength - StartingPosition))
            {
                distance -= Location - (RacetrackLength - StartingPosition);
            }
            MoveMyPictureBox(distance);

            if (Location >= (RacetrackLength - StartingPosition))
            {
                return true;
            }
            return false;
        }

        public void TakeStartingPosition()
        {
            // Reset my location to the start line
            MoveMyPictureBox(-Location);
            Location = 0;
        }

        public void MoveMyPictureBox(int Distance)
        {
            Point p = MyPictureBox.Location;
            p.X += Distance;
            MyPictureBox.Location = p;
        }
    }
}
