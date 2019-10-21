using System;
namespace _07___Arrays
{
    class Game
    {
        private static string[,] fields = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        private static string[,] gameFields = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        private static int[,] winPossibilities = { { 3, 2, 3 }, { 2, 4, 2 }, { 3, 2, 3 } };
        private static int[,] gameWinPossibilities = { { 3, 2, 3 }, { 2, 4, 2 }, { 3, 2, 3 } };
        private static string winner = null;
        private static int amountOfNonPossibilities = 0;
        private static int currentTurn = 1;
        private static int maxTurns = 10;
        private static string Player1Mark = "X";
        private static string Player2Mark = "O";
        private static string COMMAND_MULTIPLAYER = "multiplayer";
        private static string COMMAND_COMPUTER = "computer";
        private static string COMMAND_EXIT = "exit";
        private static string command = "";
        private static bool noWinners = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to Tic Tac Toe. Good luck and have fun! ;)");
            while (command != COMMAND_EXIT)
            {
                Console.WriteLine($"Type \"{COMMAND_MULTIPLAYER}\" or type \"{COMMAND_COMPUTER}\" to go against the computer! If you want to quit, type \"{COMMAND_EXIT}\".");
                if (command != "")
                {
                    Console.WriteLine($"Error: {command} is not a command! Please try again.");
                }
                command = Console.ReadLine().ToLower();
                Console.Clear();
                if (command == COMMAND_MULTIPLAYER)
                {
                    while (winner == null)
                    {
                        renderGame();
                        inputHandler();
                        // Minimum 5 turns to win
                        if (currentTurn > 4)
                        {
                            countWinPossibilities();
                            gameWinPossibilities = (int[,])winPossibilities.Clone();
                            checkGame();
                        }
                    }
                    resetGame();
                }
                else if (command == COMMAND_COMPUTER)
                {
                    throw new NotImplementedException();
                }
            }
        }

        private static void countWinPossibilities()
        {
            amountOfNonPossibilities = 0;
            for (int i = 0; i < gameFields.GetLength(0); i++)
            {
                for (int j = 0; j < gameFields.GetLength(1); j++)
                {
                    Console.WriteLine(gameWinPossibilities[i, j]);
                    if (gameWinPossibilities[i, j] == 0)
                    {
                        amountOfNonPossibilities++;
                        Console.WriteLine($"ZERO {amountOfNonPossibilities}");
                        if (amountOfNonPossibilities == gameWinPossibilities.Length)
                        {
                            noWinners = true;
                        }
                    }
                }
            }
        }

        private static void checkGame()
        {
            // Is it still possible to win?!
            checkIfGameFinished();
            if (currentTurn >= maxTurns || winner != null || noWinners)
            {
                if ((currentTurn >= maxTurns || noWinners) && winner == null)
                {
                    winner = "Nobody";
                }
                renderGame();
                Console.WriteLine($"{(currentTurn == maxTurns || noWinners ? $"Game over. {winner} won!" : $"Game won by {winner}, congratz!")}");
                Console.WriteLine($"\nDo you want to play another game?");
            }
        }

        private static void checkIfGameFinished()
        {
            horizontalOrVerticalFinish(null, "0");
            horizontalOrVerticalFinish(null, "1");
            horizontalOrVerticalFinish(null, "2");
            horizontalOrVerticalFinish("0", null);
            horizontalOrVerticalFinish("1", null);
            horizontalOrVerticalFinish("2", null);
            diagonalFinish();
        }

        private static void horizontalOrVerticalFinish(string x, string y)
        {
            if (y == null)
            {
                // horizontal
                string[] checks = new string[3];
                int row = int.Parse(x);
                //Console.WriteLine($"LENGTH {gameFields.GetLength(0)}, {row}");
                for (int column = 0; column < gameFields.GetLength(0); column++)
                {
                    checks[column] = gameFields[row, column];
                    //Console.WriteLine($"Horizontal {gameFields[row, column]} on {row} and column {column}");
                }
                //Console.WriteLine($"{checks[0]} {checks[1]} {checks[2]}");
                if (string.Equals(checks[0], checks[1]) && string.Equals(checks[0], checks[2]))
                {
                    winnerHandler(checks[0]);
                }
                else
                {
                    // No winner, so check the other possibilities
                    if (containsBothPlayerMarks(checks))
                    {
                        for (int column = 0; column < gameFields.GetLength(0); column++)
                        {
                            gameWinPossibilities[row, column]--;
                        }
                    }
                }
            }
            else
            {
                // vertical
                string[] checks = new string[3];
                int column = int.Parse(y);
                for (int row = 0; row < gameFields.GetLength(1); row++)
                {
                    checks[row] = gameFields[row, column];
                }
                if (string.Equals(checks[0], checks[1]) && string.Equals(checks[0], checks[2]))
                {
                    winnerHandler(checks[0]);
                }
                // BUG: GAAT NOG NIET HELEMAAL LEKKER
                else
                {
                    // No winner, so check the other possibilities
                    if (containsBothPlayerMarks(checks))
                    {
                        for (int row = 0; row < gameFields.GetLength(1); row++)
                        {
                            gameWinPossibilities[row, column]--;
                        }
                    }
                }
            }
        }

        private static void diagonalFinish()
        {
            string[] diagonalFields1 = { gameFields[0, 0], gameFields[1, 1], gameFields[2, 2] };
            string[] diagonalFields2 = { gameFields[2, 0], gameFields[1, 1], gameFields[0, 2] };
            if (string.Equals(gameFields[0, 0], gameFields[1, 1]) && string.Equals(gameFields[0, 0], gameFields[2, 2]))
            {
                winnerHandler(gameFields[0, 0]);
            }
            else if (string.Equals(gameFields[2, 0], gameFields[1, 1]) && string.Equals(gameFields[2, 0], gameFields[0, 2]))
            {
                winnerHandler(gameFields[2, 0]);
            }
            // Checks how many win posibilities are left
            else
            {
                if (containsBothPlayerMarks(diagonalFields1))
                {
                    gameWinPossibilities[0, 0]--;
                    gameWinPossibilities[1, 1]--;
                    gameWinPossibilities[2, 2]--;
                }
                if (containsBothPlayerMarks(diagonalFields2))
                {
                    gameWinPossibilities[2, 0]--;
                    gameWinPossibilities[1, 1]--;
                    gameWinPossibilities[0, 2]--;
                }
            }
        }

        private static bool containsBothPlayerMarks(string[] fields)
        {
            if (Array.IndexOf(fields, Player1Mark) > -1 && Array.IndexOf(fields, Player2Mark) > -1)
            {
                return true;
            }
            return false;
        }

        private static void inputHandler()
        {
            try
            {
                Console.WriteLine($"Player {(currentTurn % 2 == 1 ? $"1 ({Player1Mark})" : $"2 ({Player2Mark})")}, please enter your number:");
                int input = int.Parse(Console.ReadLine());
                if (input < 0 || input > 9)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a number between 0 and 9!");
                    return;
                }
                Coordinates coordinates = inputValidator(input);
                //Console.WriteLine($"coordinates {coordinates}");
                //Console.Clear();
                if (coordinates != null)
                {
                    currentTurn++;
                    gameFields[coordinates.Row, coordinates.Column] = currentTurn % 2 == 0 ? Player1Mark : Player2Mark;
                    Console.Clear();
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($"Number {input} is already taken, please try again!");
                }
            }
            catch (FormatException e)
            {
                Console.Clear();
                Console.WriteLine("Please enter a valid number!", e);
            }
        }

        private static void winnerHandler(string arraySymbol)
        {
            winner = string.Equals(arraySymbol, Player1Mark) ? "Player 1" : "Player 2";
        }

        private static Coordinates inputValidator(int input)
        {
            for (int i = 0; i < gameFields.GetLength(0); i++)
            {
                //Console.WriteLine($"Row {i}");
                for (int j = 0; j < gameFields.GetLength(1); j++)
                {
                    //Console.WriteLine($"Column {j}, value {gameFields[i, j]}");
                    if (gameFields[i, j] == input.ToString())
                    {
                        //Console.WriteLine($"test {gameFields[i, j] == input.ToString()}");
                        Coordinates coordinates = new Coordinates(i, j);
                        return coordinates;
                    }
                }
            }
            return null;
        }

        private static void renderGame()
        {
            Console.WriteLine("   |   |   ");
            Console.WriteLine($" {gameFields[0, 0]} | {gameFields[0, 1]} | {gameFields[0, 2]} ");
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine($" {gameFields[1, 0]} | {gameFields[1, 1]} | {gameFields[1, 2]} ");
            Console.WriteLine("___|___|___");
            Console.WriteLine("   |   |   ");
            Console.WriteLine($" {gameFields[2, 0]} | {gameFields[2, 1]} | {gameFields[2, 2]} ");
            Console.WriteLine("   |   |   ");
        }

        private static void resetGame()
        {
            currentTurn = 0;
            gameFields = fields;
            winner = null;
            command = "";
            amountOfNonPossibilities = 0;
            noWinners = false;
        }
    }
}
