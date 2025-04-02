Craps Game
Welcome to the Craps Game! This is a simple console-based implementation of the classic dice game Craps written in C#. The game tracks the player's balance, allows betting, and provides a fun way to play Craps in the terminal for now.

Features
Dice Rolling: Simulates rolling two six-sided dice.

Betting System: Allows the player to place bets before each round.

Currency Tracker: Tracks the player's balance and updates it based on wins and losses.

Game Logic: Implements standard Craps rules:

Win: Rolling a 7 or 11 on the first roll or matching your point on subsequent rolls.

Lose: Rolling a 2, 3, or 12 on the first roll, or rolling a 7 before matching the point.

Multiple Rounds: After each round, the player can choose whether to continue playing or quit.

Game Over: The game ends if the player runs out of money.

Requirements
.NET SDK: You need the .NET SDK installed to run this project. You can download it from here.

Setup Instructions
Clone or Download the Project:

Clone this repository or download the files and extract them to a folder on your machine.

Install .NET SDK:

If you haven’t already, download and install the .NET SDK from dotnet.microsoft.com/download/dotnet.

You can verify the installation by running dotnet --version in your terminal.

Run the Game:

To run the game, open a terminal or command prompt, navigate to the project directory, and execute:

bash
Copy
dotnet run
This will compile and start the game. Follow the on-screen instructions to place your bet and play.

Game Instructions
Place Your Bet: The game will prompt you to enter a bet before each round. You cannot bet more money than your current balance.

Roll the Dice: Press Enter to roll the dice. If you win, your bet is doubled and added to your balance. If you lose, your bet is subtracted from your balance.

Point System:

If your first roll is a 7 or 11, you win.

If your first roll is a 2, 3, or 12, you lose.

If you roll any other number, that becomes your "point". Keep rolling until you either match your point (win) or roll a 7 (lose).

Continue Playing: After each round, you can choose to play again or exit the game if you run out of money.

Example Output
vbnet
Copy
Welcome to Craps!
Your current balance: $1000
How much would you like to bet? 100
Press Enter to roll the dice...
You rolled: 5
Your point is now 5. Keep rolling until you get 5 to win or 7 to lose.
Press Enter to roll again...
You rolled: 7
You lose! Better luck next time.
Your new balance: $900
Do you want to play another round? (y/n): y
Game Rules
First Roll:

If you roll a 7 or 11 on the first roll, you win!

If you roll a 2, 3, or 12 on the first roll, you lose.

Any other roll (4, 5, 6, 8, 9, or 10) becomes your point.

Subsequent Rolls:

Keep rolling until you roll your point again (you win) or roll a 7 (you lose).

Betting: Players can bet any amount of their current balance, but the bet must be valid (greater than 0 and less than or equal to the current balance).

Notes
The starting balance is $1000, but you can change this value in the code if you wish.

This game runs in the terminal/console, and there are no graphical elements. It's designed to be simple and fun to play in a text-based environment.

