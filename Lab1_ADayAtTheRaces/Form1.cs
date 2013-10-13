using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_ADayAtTheRaces
{
    public partial class Form1 : Form
    {
        Greyhound[] dogs;
        Guy[] guys;
        Guy Bettor;

        public Form1()
        {
            InitializeComponent();
            setupRaceTrack();
        }

        private void setupRaceTrack()
        {
            dogs = new Greyhound[4];
            guys = new Guy[3];

            int startingPosition = dog1PictureBox.Right - racetrackPictureBox.Left;

            dogs[0] = new Greyhound { MyPictureBox = dog1PictureBox, StartingPosition = startingPosition };
            dogs[1] = new Greyhound { MyPictureBox = dog2PictureBox, StartingPosition = startingPosition };
            dogs[2] = new Greyhound { MyPictureBox = dog3PictureBox, StartingPosition = startingPosition };
            dogs[3] = new Greyhound { MyPictureBox = dog4PictureBox, StartingPosition = startingPosition };

            guys[0] = new Guy { Name = "Joe", Cash = 50, MyBet = null, MyLabel = joeBetLabel,
                MyRadioButton = joeRadio };
            guys[0].UpdateLabels();
            guys[1] = new Guy { Name = "Bob", Cash = 75, MyBet = null, MyLabel = bobBetLabel,
                MyRadioButton = bobRadio };
            guys[1].UpdateLabels();
            guys[2] = new Guy { Name = "Al", Cash = 45, MyBet = null, MyLabel = alBetLabel,
                MyRadioButton = alRadio };
            guys[2].UpdateLabels();

            // This ensures that when the dog's nose hits the end of the track, it wins
//            Greyhound.RacetrackLength = racetrackPictureBox.Width - dog1PictureBox.Width;
            Greyhound.RacetrackLength = racetrackPictureBox.Width;
            Greyhound.Randomizer = new Random();
        }

        private void raceButton_Click(object sender, EventArgs e)
        {
            bool RaceOver = false;
            int Winner = -1;
            
            raceButton.Enabled = false;
            betButton.Enabled = false;

            while (!RaceOver)
            {
                racetrackPictureBox.Refresh();      // Added to redraw the racetrack, removing dog artifacts
                System.Threading.Thread.Sleep(10);
                for (int d = 0; d < dogs.Length; d++)
                {
                    if (dogs[d].Run())
                    {
                        RaceOver = true;
                        Winner = d;
                        MessageBox.Show("We have a winner - dog #" + (d + 1) + "!");
                        break;
                    }
                }
            }

            // Reset everything after the race
            raceButton.Enabled = true;
            betButton.Enabled = true;

            for (int i = 0; i < dogs.Length; i++)
            {
                dogs[i].TakeStartingPosition();
            }

            for (int j = 0; j < guys.Length; j++)
            {
                if (guys[j].MyBet != null)
                {
                    guys[j].Collect(Winner);
                    guys[j].ClearBet();
                }
            }
        }

        private void betButton_Click(object sender, EventArgs e)
        {
            // determine which guy this is
            // call that guy's placebet
            // subtract 1 from numericUpDown2.Value because the array index starts at 0
            int dogBet = (int)numericUpDown2.Value;
            if (Bettor.PlaceBet((int)numericUpDown1.Value, --dogBet))
            {
                numericUpDown1.Value = 5;
                numericUpDown2.Value = 1;
            }
        }

        private void joeRadio_CheckedChanged(object sender, EventArgs e)
        {
            Bettor = guys[0];
            bettorLabel.Text = Bettor.Name;
        }

        private void bobRadio_CheckedChanged(object sender, EventArgs e)
        {
            Bettor = guys[1];
            bettorLabel.Text = Bettor.Name;
        }

        private void alRadio_CheckedChanged(object sender, EventArgs e)
        {
            Bettor = guys[2];
            bettorLabel.Text = Bettor.Name;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
