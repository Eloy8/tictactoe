using System;

namespace _07___Arrays
{
    class Game
    {
        private static string[,] fields = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        private static string[,] gameFields = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        private static bool finishedGame = false;
        private static int currentTurn = 1;
        private static int maxTurns = 9;
        private static string Player1Mark = "X";
        private static string Player2Mark = "O";
        static void Main(string[] args)
        {
            while (!finishedGame)
            {
                Game game = new Game();
                game.renderGame();
                game.inputHandler();
                if (currentTurn > 4)
                {
                    game.checkGame();
                }
                currentTurn++;
            }
        }

        private void checkGame()
        {
            string weHaveAWinner = checkIfGameFinished();
            if (maxTurns > currentTurn && weHaveAWinner != null)
            {
                Console.WriteLine($"{(currentTurn == 9 ? "Game over, nobody won!" : $"Game won by {weHaveAWinner}, congratz!")}");
                finishedGame = true;
            }
        }

        private string checkIfGameFinished()
        {
            // CHECKS:
            horizontalOrVerticalFinish(null, "0");
            horizontalOrVerticalFinish(null, "1");
            horizontalOrVerticalFinish(null, "2");
            horizontalOrVerticalFinish("0", null);
            horizontalOrVerticalFinish("1", null);
            horizontalOrVerticalFinish("2", null);
            //diagonalFinish();
            return null;

        }

        private void horizontalOrVerticalFinish(string x, string y)
        {
            if (y == null)
            {
                // horizontal
                string[] checks = new string[3];
                int row = int.Parse(x);
                for (int column = 0; column < gameFields.GetLength(0); column++)
                {
                    checks[row] = gameFields[row, column];
                    Console.WriteLine($"Horizontal {gameFields[row, column]} on {row} and column {column}");
                }
                Console.WriteLine($"{checks[0]} {checks[1]} {checks[2]}");
                if (checks[0].Equals(checks[1]) && checks[0].Equals(checks[2]))
                {
                    finishedGame = true;
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
                    Console.WriteLine($"Vertical {gameFields[row, column]} on {row} and column {column}");
                }

                Console.WriteLine($"{checks[0]} {checks[1]} {checks[2]}");
                if (string.Equals(checks[0], checks[1]) && string.Equals(checks[0], checks[2]))
                {
                    finishedGame = true;
                }
            }
        }

        private void diagonalFinish()
        {
            throw new NotImplementedException();
        }

        private void inputHandler()
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
                Console.WriteLine($"coordinates {coordinates}");
                //Console.Clear();
                if (coordinates != null)
                {
                    gameFields[coordinates.Row, coordinates.Column] = currentTurn % 2 == 0 ? Player1Mark : Player2Mark;
                }
                else
                {
                    Console.WriteLine($"Number {input} is not available, please try again!");
                    return;
                }
            }
            catch (FormatException e)
            {
                Console.Clear();
                Console.WriteLine("Please enter a valid number!", e);
            }
        }

        private static Coordinates inputValidator(int input)
        {
            for (int i = 0; i < gameFields.GetLength(0); i++)
            {
                Console.WriteLine($"Row {i}");
                for (int j = 0; j < gameFields.GetLength(1); j++)
                {
                    Console.WriteLine($"Column {j}, value {gameFields[i, j]}");
                    if (gameFields[i, j] == input.ToString())
                    {
                        Console.WriteLine($"test {gameFields[i, j] == input.ToString()}");
                        Coordinates coordinates = new Coordinates(i, j);
                        return coordinates;
                    }
                }
            }
            return null;
        }

        private void renderGame()
        {
            //Console.Clear();
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
    }
}
