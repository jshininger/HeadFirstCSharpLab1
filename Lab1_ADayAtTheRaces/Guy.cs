using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_ADayAtTheRaces
{
    public class Guy
    {
        public string Name; // The guy's name
        public Bet MyBet;   // An instance of Bet that has his bet
        public int Cash;    // How much cash he has

        // The last two fields are the guy's GUI controls on the form
        public RadioButton MyRadioButton;   // My RadioButton
        public Label MyLabel;               // My Label

        public void UpdateLabels()
        {
            // Set my label to my bet's description, and the label on my
            // radio button to show my cash ("Joe has 43 bucks")
            if (MyBet != null)
            {
                MyLabel.Text = MyBet.GetDescription();
            }
            MyRadioButton.Text = Name + " has " + Cash + " bucks";
        }

        public void ClearBet()
        {
            // Reset my bet so it's zero
            MyBet.Amount = 0;
            UpdateLabels();
        }

        public bool PlaceBet(int Amount, int Dog)
        {
            // Place a new bet and store it in my bet field
            // Return true if the guy had enough money to bet
            if (Amount <= Cash)
            {
                MyBet = new Bet() { Amount = Amount, Dog = Dog, Bettor = this };
                UpdateLabels();
                return true;
            }
            else
            {
                MessageBox.Show(Name + " does not have enough cash to place this bet.", "Bet denied");
                return false;
            }
        }

        public void Collect(int Winner)
        {
            // Ask my bet to pay out
            Cash += MyBet.PayOut(Winner);
            UpdateLabels();
        }
    }

    public class Bet
    {
        public int Amount;  // The amount of cash that was bet
        public int Dog;     // The number of the dog the bet is on
        public Guy Bettor;  // The guy who placed the bet

        public string GetDescription()
        {
            // Return a string that says who placed the bet, how much
            // cash was bet, and which dog he bet on (“Joe bets 8 on
            // dog #4”). If the amount is zero, no bet was placed
            // (“Joe hasn’t placed a bet”).
            if (Amount == 0)
            {
                return Bettor.Name + " hasn't placed a bet";
            }
            else
            {
                return Bettor.Name + " bets " + Amount + " bucks on dog #" + (Dog + 1);
            }
        }

        public int PayOut(int Winner)
        {
            // The parameter is the winner of the race. If the dog won,
            // return the amount bet. Otherwise, return the negative of
            // the amount bet.
            if (Winner == Dog)
            {
                return Amount;
            }
            else
            {
                return -Amount;
            }
        }
    }
}
