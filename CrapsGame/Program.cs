using System;
using System.Collections.Generic;
class CrapsGame
{
    // Simulates rolling two six-sided dice and returns their sum.
    static (int, int) RollDice()
    {
        Random rand = new Random();
        int die1 = rand.Next(1, 7);  // Random number between 1 and 6
        int die2 = rand.Next(1, 7);  // Random number between 1 and 6
        return (die1, die2);
    }

    // Placeholder function for bet amounts
    static int BetAmount(int currentBalance) {
        Console.Write($"How much money would you like to bet?: ");
        int bet;
        if (!int.TryParse(Console.ReadLine(), out bet) || bet <=0 || bet > currentBalance) {
            Console.WriteLine("Invalid bet size. Please enter a valid amount: ");
            return -1;
        }

        return bet;
    }

    // Runs a single game of Craps
    static void PlayCraps(ref int balance, Dictionary<int, double> payouts)
    {
        List<HardwayBet> hardwayBets = new List<HardwayBet>();
        
        Console.WriteLine("Welcome to Craps!");
        Console.WriteLine($"Your current balance is: ${balance}");

        // Use placeholder function to retrieve bet amount
        int bet = BetAmount(balance);
        if (bet == -1) return; // Exit if invalid bet

        //Hardway Bet prompt
        Console.Write("Do you want to set a Hardway Side Bet? (y/n)");
        if (Console.ReadLine().ToLower() == "y")
        {
            foreach (HardwayType type in Enum.GetValues(typeof(HardwayType)))
            {
                Console.Write($"Bet on Hard {((int)type)} (enter amount (0 to skip): ");
                if (int.TryParse(Console.ReadLine(), out int hwAmount) && hwAmount > 0 && hwAmount <= balance)
                {
                    hardwayBets.Add(new HardwayBet { Type = type, Amount = hwAmount });
                    balance -= hwAmount;
                }
            }
        }

        // Deduct the bet from the player's balance
        balance -= bet;

        Console.WriteLine("Press Enter to roll the dice...");
        Console.ReadLine();

        // First roll
        (int die1, int die2) = RollDice();
        int point = die1 + die2;
        Console.WriteLine($"You rolled: {die1} + {die2} = {point}");

        // First roll rules
        if (point == 7 || point == 11)
        {
            // If you win -> double your earnings (using dictionary; cleaner input and always easily changeable through dict)
           double multiplier = payouts.ContainsKey(point) ? payouts[point] : 1.0; 
           int winnings = (int)(bet*multiplier); 
           Console.WriteLine($"You won! You earned ${winnings}");
           balance += winnings + bet;
        }
        else if (point == 2 || point == 3 || point == 12)
        {
            // The player loses their bet (already deducted earlier)
            Console.WriteLine("Craps! You lose!");
        
        }
        else
        {
            // Point is set, now keep rolling until the player wins or loses
            Console.WriteLine($"Your point is now {point}. Keep rolling until you get {point} to win or 7 to lose.");

            while (true)
            {
                Console.WriteLine("Press Enter to roll again...");
                Console.ReadLine();

                (die1, die2) = RollDice();
                int roll = die1 + die2;
                Console.WriteLine($"You rolled: {die1} + {die2} = {roll}");

                // Check Hardway Bets
                for (int i = hardwayBets.Count - 1; i >= 0; i--)
                {
                    var hwBet = hardwayBets[i];
                    if (!hwBet.IsActive) continue;

                    int target = (int)hwBet.Type;
                    bool isHardway = (die1 == die2 && die1 * 2 == target);
                    bool isEasy = (die1 + die2 == target && die1 != die2);

                    if (isHardway)
                    {
                        int payout = hwBet.Amount * 7;  // Customize payout if needed
                        Console.WriteLine($"HARDWAY WIN! You hit Hard {target} and won ${payout}");
                        balance += payout;
                        hwBet.IsActive = false;
                    }
                    else if (roll == 7 || isEasy)
                    {
                        Console.WriteLine($"You lost your Hard {target} bet.");
                        hwBet.IsActive = false;
                    }
                }
                if (roll == point)
                {
                    double multiplier = payouts.ContainsKey(point) ? payouts[point] : 2.0;
                    int winnings = (int)(bet * multiplier);
                    Console.WriteLine($"You won! You earned ${winnings}");
                    balance += winnings + bet;
                    break;
                }
                else if (roll == 7)
                {
                    // The player loses their bet (already deducted earlier)
                    Console.WriteLine("You lose! Better luck next time.");
                    break;
                }
            }
        }

        Console.WriteLine($"Your new balance: ${balance}");
    }
    enum HardwayType {Hard4 = 4, Hard6 = 6, Hard8 = 8, Hard10 = 10}
    class HardwayBet
    {
        public HardwayType Type {get;set;}
        public int Amount { get; set; }
        public bool IsActive { get; set; } = true;
    }
    static void Main(string[] args)
    {
        // Direct user to starting amount of $
        Console.Write("How much money are you depositing?: $");
        int balance;
        if(!int.TryParse(Console.ReadLine(), out balance) || balance <= 0 ) {
            // Invalid amount -> set default amount to $1000
            Console.WriteLine("Invalid starting amount. Starting by $1000 by default!");
            balance = 1000; 
        }

        // Craps payouts by winning number (can be changed) ; 2,3,12 loses by default
        var payouts = new Dictionary<int, double>
        {
            {4, 2.0}, 
            {5, 1.5},
            {6, 1.2},
            {7, 1.0},
            {8, 1.2},
            {9, 1.5},
            {10, 2.0},
            {11, 1.0},
        };

        bool playing = true;
        while (playing)
        {
            Console.Clear();  // Clear the screen for the new round
            PlayCraps(ref balance, payouts); // Play using the new odds

            if (balance <= 0)
            {
                Console.WriteLine("You have no money left. Game over!");
                break;
            }

            // Ask if the player wants to keep playing -- updated to better handle breaks
            string choice;
            while (true) 
            {
                Console.Write("Do you want to play another round? (y/n): "); 
                choice = Console.ReadLine().ToLower();

                if (choice == "y") {
                    break; // Continue playing
                } else if (choice == "n") {
                    playing = false;
                    Console.WriteLine("Thanks for playing! Goodbye!");
                    break;
                } else {
                    Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                }
            }
        }
    }
}