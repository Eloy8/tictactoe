using System;
using System.Collections.Generic;
using System.Text;

namespace _07___Arrays.TicTacToe.Utils
{
    class Checker
    {
        public static void checkIfGameFinished()
        {
            horizontalOrVerticalFinish(null, "0");
            horizontalOrVerticalFinish(null, "1");
            horizontalOrVerticalFinish(null, "2");
            horizontalOrVerticalFinish("0", null);
            horizontalOrVerticalFinish("1", null);
            horizontalOrVerticalFinish("2", null);
            diagonalFinish();
        }

        public static void horizontalOrVerticalFinish(string x, string y)
        {
            if (y == null)
            {
                // horizontal
                string[] checks = new string[3];
                int row = int.Parse(x);
                for (int column = 0; column < Game.gameFields.GetLength(0); column++)
                {
                    checks[column] = Game.gameFields[row, column];
                }
                if (string.Equals(checks[0], checks[1]) && string.Equals(checks[0], checks[2]))
                {
                    Basics.winnerHandler(checks[0]);
                }
                else
                {
                    int column;
                    // No winner, so check the other possibilities
                    if (containsBothPlayerMarks(checks))
                    {
                        for (column = 0; column < Game.gameFields.GetLength(0); column++)
                        {
                            Game.gameWinPossibilities[row, column]--;
                        }
                    }
                    else
                    {
                        column = Computer.playerIsCloseToWinning(checks);
                        if (column > -1)
                        {
                            Game.gameWinPossibilities[row, column] = 5;
                        }
                    }
                }
            }
            else
            {
                // vertical
                string[] checks = new string[3];
                int column = int.Parse(y);
                for (int row = 0; row < Game.gameFields.GetLength(1); row++)
                {
                    checks[row] = Game.gameFields[row, column];
                }
                if (string.Equals(checks[0], checks[1]) && string.Equals(checks[0], checks[2]))
                {
                    Basics.winnerHandler(checks[0]);
                }
                else
                {
                    int row;
                    // No winner, so check the other possibilities
                    if (containsBothPlayerMarks(checks))
                    {
                        for (row = 0; row < Game.gameFields.GetLength(1); row++)
                        {
                            Game.gameWinPossibilities[row, column]--;
                        }
                    }
                    else
                    {
                        row = Computer.playerIsCloseToWinning(checks);
                        if (row > -1)
                        {
                            Game.gameWinPossibilities[row, column] = 5;
                        }
                    }
                }
            }
        }

        public static void diagonalFinish()
        {
            string[] diagonalFields1 = { Game.gameFields[0, 0], Game.gameFields[1, 1], Game.gameFields[2, 2] };
            string[] diagonalFields2 = { Game.gameFields[2, 0], Game.gameFields[1, 1], Game.gameFields[0, 2] };
            if (string.Equals(Game.gameFields[0, 0], Game.gameFields[1, 1]) && string.Equals(Game.gameFields[0, 0], Game.gameFields[2, 2]))
            {
                Basics.winnerHandler(Game.gameFields[0, 0]);
            }
            else if (string.Equals(Game.gameFields[2, 0], Game.gameFields[1, 1]) && string.Equals(Game.gameFields[2, 0], Game.gameFields[0, 2]))
            {
                Basics.winnerHandler(Game.gameFields[2, 0]);
            }
            // Checks how many win posibilities are left
            else
            {
                if (containsBothPlayerMarks(diagonalFields1))
                {
                    Game.gameWinPossibilities[0, 0]--;
                    Game.gameWinPossibilities[1, 1]--;
                    Game.gameWinPossibilities[2, 2]--;
                }
                if (containsBothPlayerMarks(diagonalFields2))
                {
                    Game.gameWinPossibilities[2, 0]--;
                    Game.gameWinPossibilities[1, 1]--;
                    Game.gameWinPossibilities[0, 2]--;
                }
            }
        }

        public static bool containsBothPlayerMarks(string[] fields)
        {
            return Array.IndexOf(fields, Game.Player1Mark) > -1 && Array.IndexOf(fields, Game.Player2Mark) > -1;
        }
    }
}
