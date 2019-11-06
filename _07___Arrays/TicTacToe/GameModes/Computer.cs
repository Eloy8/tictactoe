using _07___Arrays.TicTacToe.Utils;
using System;

namespace _07___Arrays.TicTacToe
{
    class Computer
    {
        public static void Handler()
        {
            Checker.checkIfGameFinished();
            countWinPossibilities();
            Turn();
            Game.gameWinPossibilities = (int[,])Game.winPossibilities.Clone();
            Game.currentTurn++;
        }

        public static void Turn()
        {
            Console.WriteLine($"Computer ({Game.Player2Mark}) is entering its number...");
            for (int k = 5; k > 0; k--)
            {
                for (int i = 0; i < Game.gameWinPossibilities.GetLength(0); i++)
                {
                    for (int j = 0; j < Game.gameWinPossibilities.GetLength(1); j++)
                    {
                        if (Game.gameWinPossibilities[i, j] == k && Game.gameFields[i, j] != Game.Player1Mark)
                        {
                            System.Threading.Thread.Sleep(2000);
                            Game.gameFields[i, j] = Game.Player2Mark;
                            Console.Clear();
                            return;
                        }
                    }
                }
            }
        }

        public static void countWinPossibilities()
        {
            Game.amountOfNonPossibilities = 0;
            for (int i = 0; i < Game.gameFields.GetLength(0); i++)
            {
                for (int j = 0; j < Game.gameFields.GetLength(1); j++)
                {
                    if (Game.gameWinPossibilities[i, j] == 0)
                    {
                        Game.amountOfNonPossibilities++;
                        if (Game.amountOfNonPossibilities == Game.gameWinPossibilities.Length)
                        {
                            Game.noWinners = true;
                        }
                    }
                }
            }
        }

        public static int playerIsCloseToWinning(string[] fields)
        {
            int playerMarkOccurence = 0;
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i] == Game.Player1Mark || fields[i] == Game.Player2Mark)
                {
                    playerMarkOccurence++;
                }
                if (playerMarkOccurence == 2)
                {
                    for (int j = 0; j < fields.Length; j++)
                    {
                        if (fields[j] != Game.Player1Mark && fields[j] != Game.Player2Mark)
                        {
                            return j;
                        }
                    }
                }
            }
            return -1;
        }
    }
}
