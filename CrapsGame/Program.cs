using System;
using System.Collections.Generic;
class CrapsGame
{
    // Simulates rolling two six-sided dice and returns their sum.
    static int RollDice()
    {
        Random rand = new Random();
        int die1 = rand.Next(1, 7);  // Random number between 1 and 6
        int die2 = rand.Next(1, 7);  // Random number between 1 and 6
        return die1 + die2;
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
        Console.WriteLine("Welcome to Craps!");
        Console.WriteLine($"Your current balance is: ${balance}");

        // Use placeholder function to retrieve bet amount
        int bet = BetAmount(balance);
        if (bet == -1) return; // Exit if invalid bet

        // Deduct the bet from the player's balance
        balance -= bet;

        Console.WriteLine("Press Enter to roll the dice...");
        Console.ReadLine();

        // First roll
        int point = RollDice();
        Console.WriteLine($"You rolled: {point}");

        // First roll rules
        if (point == 7 || point == 11)
        {
            // If you win -> double your earnings (using dictionary; cleaner input and always easily changeable through dict)
           double multiplier = payouts.ContainsKey(point) ? payouts[point] : 1.0; 
           int winnings = (int)(bet*multiplier); 
           Console.WriteLine($"You won! You earned ${winnings}");
           balance += winnings;
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

                int roll = RollDice();
                Console.WriteLine($"You rolled: {roll}");

                if (roll == point)
                {
                    double multiplier = payouts.ContainsKey(point) ? payouts[point] : 2.0;
                    int winnings = (int)(bet * multiplier);
                    Console.WriteLine($"You won! You earned ${winnings}");
                    balance += winnings;
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