using _07___Arrays.TicTacToe.Utils;
using System;

namespace _07___Arrays.TicTacToe
{
    class Mechanics
    {
        public static Coordinates inputValidator(int input)
        {
            for (int i = 0; i < Game.gameFields.GetLength(0); i++)
            {
                for (int j = 0; j < Game.gameFields.GetLength(1); j++)
                {
                    if (Game.gameFields[i, j] == input.ToString())
                    {
                        Coordinates coordinates = new Coordinates(i, j);
                        return coordinates;
                    }
                }
            }
            return null;
        }

        public static void checkGame()
        {
            Checker.checkIfGameFinished();
            Computer.countWinPossibilities();
            Game.gameWinPossibilities = (int[,])Game.winPossibilities.Clone();
            if (Game.currentTurn >= Game.maxTurns || Game.winner != null || Game.noWinners)
            {
                if ((Game.currentTurn >= Game.maxTurns || Game.noWinners) && Game.winner == null)
                {
                    Game.winner = Game.NobodyWon;
                }
                Basics.renderGame();
                Console.WriteLine($"{(Game.currentTurn == Game.maxTurns || Game.noWinners ? $"Game over. {Game.winner} won!" : $"Game won by {Game.winner}, {(Game.winner != Game.ComputerWon ? "congratz!" : "too bad!")}")}");
                Console.WriteLine($"\nDo you want to play another game?");
            }
        }
    }
}
