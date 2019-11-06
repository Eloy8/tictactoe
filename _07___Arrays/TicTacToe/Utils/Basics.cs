using System;

namespace _07___Arrays.TicTacToe.Utils
{
    class Basics
    {
        public static void winnerHandler(string arraySymbol)
        {
            if (string.Equals(arraySymbol, Game.Player2Mark))
            {
                Game.winner = Game.command == Game.COMMAND_MULTIPLAYER ? Game.Player2 : Game.ComputerWon;
            }
            else
            {
                Game.winner = Game.Player1;
            }
        }

        public static void renderGame()
        {
            string emptyField = "   |   |   ";
            string emptyFieldWithUnderline = "___|___|___";
            Console.WriteLine(emptyField);
            Console.WriteLine($" {Game.gameFields[0, 0]} | {Game.gameFields[0, 1]} | {Game.gameFields[0, 2]} ");
            Console.WriteLine(emptyFieldWithUnderline);
            Console.WriteLine(emptyField);
            Console.WriteLine($" {Game.gameFields[1, 0]} | {Game.gameFields[1, 1]} | {Game.gameFields[1, 2]} ");
            Console.WriteLine(emptyFieldWithUnderline);
            Console.WriteLine(emptyField);
            Console.WriteLine($" {Game.gameFields[2, 0]} | {Game.gameFields[2, 1]} | {Game.gameFields[2, 2]} ");
            Console.WriteLine(emptyField);
        }

        public static void resetGame()
        {
            Random rand = new Random();
            Game.currentTurn = rand.NextDouble() <= 0.5 ? 0 : 1;
            Game.maxTurns = Game.currentTurn + 9;
            Game.gameFields = (string[,])Game.fields.Clone();
            Game.winner = null;
            Game.command = "";
            Game.amountOfNonPossibilities = 0;
            Game.noWinners = false;
        }
    }
}
