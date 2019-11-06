using System;

namespace _07___Arrays.TicTacToe
{
    class Player
    {
        public static void Handler()
        {
            try
            {
                Console.WriteLine($"Player {(Game.command == Game.COMMAND_MULTIPLAYER && Game.currentTurn % 2 == 0 ? $"2 ({Game.Player2Mark})" : $"1 ({Game.Player1Mark})")}, please enter your number:");
                int input = int.Parse(Console.ReadLine());
                if (input < 1 || input > 9)
                {
                    Console.Clear();
                    Console.WriteLine("Please enter a number between 1 and 9!");
                    return;
                }
                Coordinates coordinates = Mechanics.inputValidator(input);
                if (coordinates != null)
                {
                    Game.currentTurn++;
                    Game.gameFields[coordinates.Row, coordinates.Column] = Game.command == Game.COMMAND_MULTIPLAYER && Game.currentTurn % 2 == 1 ? Game.Player2Mark : Game.Player1Mark;
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
    }
}
