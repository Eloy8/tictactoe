using System;
using _07___Arrays.TicTacToe;
using _07___Arrays.TicTacToe.Utils;

namespace _07___Arrays
{
    class Game
    {
        public static readonly string[,] fields = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        public static readonly int[,] winPossibilities = { { 3, 2, 3 }, { 2, 4, 2 }, { 3, 2, 3 } };
        public const string COMMAND_MULTIPLAYER = "multiplayer";
        public const string COMMAND_COMPUTER = "computer";
        public const string COMMAND_EXIT = "exit";
        public const string ComputerWon = "Computer";
        public const string NobodyWon = "Nobody";
        public const string Player1 = "Player 1";
        public const string Player2 = "Player 2";
        public const string Player1Mark = "X";
        public const string Player2Mark = "O";

        public static string[,] gameFields = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        public static int[,] gameWinPossibilities = { { 3, 2, 3 }, { 2, 4, 2 }, { 3, 2, 3 } };
        public static int amountOfNonPossibilities;
        public static bool noWinners = false;
        public static string winner = null;
        public static string command = "";
        public static int currentTurn = 1;
        public static int maxTurns = 10;

        // TO DO:
        // refactoren horizontale en verticale check? (met for loop en tostring)
        // horizontalOrVerticalFinish dubbele code in een stoppen!
        // Added getters and setters and decrease the amount of cross-class calls to variables!

        public static void start()
        {
            Console.WriteLine("Hello, welcome to Tic Tac Toe. Good luck and have fun! ;)");
            while (command != COMMAND_EXIT)
            {
                commandInput();
                commandHandler();
            }
        }

        private static void commandInput()
        {
            Console.WriteLine($"Type \"{COMMAND_MULTIPLAYER}\" or type \"{COMMAND_COMPUTER}\" to go against the computer! If you want to quit, type \"{COMMAND_EXIT}\".");
            if (command != "")
            {
                Console.WriteLine($"Error: {command} is not a command! Please try again.");
            }
            command = Console.ReadLine().ToLower();
            Console.Clear();
        }

        private static void commandHandler()
        {
            if (command == COMMAND_MULTIPLAYER || command == COMMAND_COMPUTER)
            {
                while (winner == null)
                {
                    Basics.renderGame();
                    if (currentTurn % 2 == 0 && command == COMMAND_COMPUTER)
                    {
                        Mechanics.checkGame();
                        Computer.Handler();
                    }
                    else
                    {
                        Player.Handler();
                    }
                    if (currentTurn > 4)
                    {
                        Mechanics.checkGame();
                    }
                }
                Basics.resetGame();
            }
        }
    }
}
