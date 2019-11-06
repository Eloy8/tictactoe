using System;
namespace _07___Arrays
{
    class Game
    {
        private static readonly string[,] fields = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        private static readonly int[,] winPossibilities = { { 3, 2, 3 }, { 2, 4, 2 }, { 3, 2, 3 } };
        const string COMMAND_MULTIPLAYER = "multiplayer";
        const string COMMAND_COMPUTER = "computer";
        const string COMMAND_EXIT = "exit";
        const string Computer = "Computer";
        const string Nobody = "Nobody";
        const string Player1 = "Player 1";
        const string Player2 = "Player 2";
        const string Player1Mark = "X";
        const string Player2Mark = "O";

        private static string[,] gameFields = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        private static int[,] gameWinPossibilities = { { 3, 2, 3 }, { 2, 4, 2 }, { 3, 2, 3 } };
        private static int amountOfNonPossibilities;
        private static bool noWinners = false;
        private static string winner = null;
        private static string command = "";
        private static int currentTurn = 1;
        private static int maxTurns = 10;

        // TO DO:
        // Put regions inside of seperate classes!
        // refactoren horizontale en verticale check? (met for loop en tostring)
        // horizontalOrVerticalFinish dubbele code in een stoppen!
        // Beginnen vanuit de main ipv direct game (methodenaam startGame()?)

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
                    renderGame();
                    if (currentTurn % 2 == 0 && command == COMMAND_COMPUTER)
                    {
                        checkGame();
                        computerHandler();
                    }
                    else
                    {
                        playerHandler();
                    }
                    if (currentTurn > 4)
                    {
                        checkGame();
                    }
                }
                resetGame();
            }
        }

        #region playerhandler
        private static void playerHandler()
        {
            try
            {
                Console.WriteLine($"Player {(command == COMMAND_MULTIPLAYER && currentTurn % 2 == 0 ? $"2 ({Player2Mark})" : $"1 ({Player1Mark})")}, please enter your number:");
                int input = int.Parse(Console.ReadLine());
                if (input < 0 || input > 9)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a number between 0 and 9!");
                    return;
                }
                Coordinates coordinates = inputValidator(input);
                if (coordinates != null)
                {
                    currentTurn++;
                    gameFields[coordinates.Row, coordinates.Column] = command == COMMAND_MULTIPLAYER && currentTurn % 2 == 1 ? Player2Mark : Player1Mark;
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
        #endregion

        #region computer

        private static void computerHandler()
        {
            checkIfGameFinished();
            countWinPossibilities();
            computerTurn();
            gameWinPossibilities = (int[,])winPossibilities.Clone();
            currentTurn++;
        }

        private static void computerTurn()
        {
            Console.WriteLine($"Computer ({Player2Mark}) is entering its number...");
            for (int k = 5; k > 0; k--)
            {
                for (int i = 0; i < gameWinPossibilities.GetLength(0); i++)
                {
                    for (int j = 0; j < gameWinPossibilities.GetLength(1); j++)
                    {
                        if (gameWinPossibilities[i, j] == k && gameFields[i, j] != Player1Mark)
                        {
                            System.Threading.Thread.Sleep(2000);
                            gameFields[i, j] = Player2Mark;
                            Console.Clear();
                            return;
                        }
                    }
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
                    if (gameWinPossibilities[i, j] == 0)
                    {
                        amountOfNonPossibilities++;
                        if (amountOfNonPossibilities == gameWinPossibilities.Length)
                        {
                            noWinners = true;
                        }
                    }
                }
            }
        }

        private static int playerIsCloseToWinning(string[] fields)
        {
            int playerMarkOccurence = 0;
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i] == Player1Mark || fields[i] == Player2Mark)
                {
                    playerMarkOccurence++;
                }
                if (playerMarkOccurence == 2)
                {
                    for (int j = 0; j < fields.Length; j++)
                    {
                        if (fields[j] != Player1Mark && fields[j] != Player2Mark)
                        {
                            return j;
                        }
                    }
                }
            }
            return -1;
        }
        #endregion

        #region gameChecker
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
                for (int column = 0; column < gameFields.GetLength(0); column++)
                {
                    checks[column] = gameFields[row, column];
                }
                if (string.Equals(checks[0], checks[1]) && string.Equals(checks[0], checks[2]))
                {
                    winnerHandler(checks[0]);
                }
                else
                {
                    int column;
                    // No winner, so check the other possibilities
                    if (containsBothPlayerMarks(checks))
                    {
                        for (column = 0; column < gameFields.GetLength(0); column++)
                        {
                            gameWinPossibilities[row, column]--;
                        }
                    }
                    else
                    {
                        column = playerIsCloseToWinning(checks);
                        if (column > -1)
                        {
                            gameWinPossibilities[row, column] = 5;
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
                else
                {
                    int row;
                    // No winner, so check the other possibilities
                    if (containsBothPlayerMarks(checks))
                    {
                        for (row = 0; row < gameFields.GetLength(1); row++)
                        {
                            gameWinPossibilities[row, column]--;
                        }
                    }
                    else
                    {
                        row = playerIsCloseToWinning(checks);
                        if (row > -1)
                        {
                            gameWinPossibilities[row, column] = 5;
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
            return Array.IndexOf(fields, Player1Mark) > -1 && Array.IndexOf(fields, Player2Mark) > -1;
        }
        #endregion

        #region gameMechanics

        private static Coordinates inputValidator(int input)
        {
            for (int i = 0; i < gameFields.GetLength(0); i++)
            {
                for (int j = 0; j < gameFields.GetLength(1); j++)
                {
                    if (gameFields[i, j] == input.ToString())
                    {
                        Coordinates coordinates = new Coordinates(i, j);
                        return coordinates;
                    }
                }
            }
            return null;
        }

        private static void checkGame()
        {
            checkIfGameFinished();
            countWinPossibilities();
            gameWinPossibilities = (int[,])winPossibilities.Clone();
            if (currentTurn >= maxTurns || winner != null || noWinners)
            {
                if ((currentTurn >= maxTurns || noWinners) && winner == null)
                {
                    winner = Nobody;
                }
                renderGame();
                Console.WriteLine($"{(currentTurn == maxTurns || noWinners ? $"Game over. {winner} won!" : $"Game won by {winner}, {(winner != Computer ? "congratz!" : "too bad!")}")}");
                Console.WriteLine($"\nDo you want to play another game?");
            }
        }
        #endregion

        #region gameBasics

        private static void winnerHandler(string arraySymbol)
        {
            if (string.Equals(arraySymbol, Player2Mark))
            {
                winner = command == COMMAND_MULTIPLAYER ? Player2 : Computer;
            }
            else
            {
                winner = Player1;
            }
        }

        private static void renderGame()
        {
            string emptyField = "   |   |   ";
            string emptyFieldWithUnderline = "___|___|___";
            Console.WriteLine(emptyField);
            Console.WriteLine($" {gameFields[0, 0]} | {gameFields[0, 1]} | {gameFields[0, 2]} ");
            Console.WriteLine(emptyFieldWithUnderline);
            Console.WriteLine(emptyField);
            Console.WriteLine($" {gameFields[1, 0]} | {gameFields[1, 1]} | {gameFields[1, 2]} ");
            Console.WriteLine(emptyFieldWithUnderline);
            Console.WriteLine(emptyField);
            Console.WriteLine($" {gameFields[2, 0]} | {gameFields[2, 1]} | {gameFields[2, 2]} ");
            Console.WriteLine(emptyField);
        }

        private static void resetGame()
        {
            Random rand = new Random();
            currentTurn = rand.NextDouble() <= 0.5 ? 0 : 1;
            maxTurns = currentTurn + 9;
            gameFields = (string[,])fields.Clone();
            winner = null;
            command = "";
            amountOfNonPossibilities = 0;
            noWinners = false;
        }
        #endregion
    }
}
