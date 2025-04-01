using System;

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

    // Runs a single game of Craps
    static void PlayCraps(ref int balance)
    {
        Console.WriteLine("Welcome to Craps!");

        // Ask the player to enter their bet
        Console.WriteLine($"Your current balance: ${balance}");
        Console.Write("How much would you like to bet? ");
        int bet = int.Parse(Console.ReadLine());

        // Check if the bet is valid
        if (bet <= 0 || bet > balance)
        {
            Console.WriteLine("Invalid bet amount. Please enter a valid bet.");
            return;
        }

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
            Console.WriteLine("Congratulations! You win!");
            balance += bet * 2;  // Win double the bet
        }
        else if (point == 2 || point == 3 || point == 12)
        {
            Console.WriteLine("Craps! You lose!");
            // The player loses their bet (already deducted earlier)
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
                    Console.WriteLine("You win!");
                    balance += bet * 2;  // Win double the bet
                    break;
                }
                else if (roll == 7)
                {
                    Console.WriteLine("You lose! Better luck next time.");
                    // The player loses their bet (already deducted earlier)
                    break;
                }
            }
        }

        Console.WriteLine($"Your new balance: ${balance}");
    }

    static void Main(string[] args)
    {
        int balance = 1000;  // Starting balance
        bool playing = true;

        while (playing)
        {
            Console.Clear();  // Clear the screen for the new round
            PlayCraps(ref balance);

            if (balance <= 0)
            {
                Console.WriteLine("You have no money left. Game over!");
                break;
            }

            // Ask if the player wants to keep playing
            Console.Write("Do you want to play another round? (y/n): ");
            string choice = Console.ReadLine().ToLower();
            if (choice != "y")
            {
                playing = false;
                Console.WriteLine("Thanks for playing! Goodbye!");
            }
        }
    }
}