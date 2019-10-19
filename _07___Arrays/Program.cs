using System;

namespace _07___Arrays
{
    class Program
    {
        static void ProgramMain(string[] args)
        {
            int[] grades = new int[5];

            grades[0] = 20;
            grades[1] = 15;
            grades[2] = 12;
            grades[3] = 9;
            grades[4] = 7;

            string input = Console.ReadLine();
            //assign value to array grades at index 0
            grades[0] = int.Parse(input);
            Console.WriteLine($"grades at index 0 {grades[0]}");

            // another way of initializing an array
            int[] gradesOffMathStudentsA = { 20, 13, 12, 8, 8 };

            // third way of initializing an array
            int[] gradeOfMathStudentsB = new int[] { 15, 20, 3, 17, 18, 15 };

            Console.WriteLine(gradesOffMathStudentsA.Length);

            int[] nums = new int[10];

            for (int i = 0; i < 10; i++)
            {
                nums[i] = i;
            }

            for (int j = 0; j < nums.Length; j++)
            {
                Console.WriteLine($"Element[{j}] = {nums[j] = j}");
            }

            foreach (int j in nums)
            {
                Console.WriteLine($"Element[{j}] = {nums[j] = j}");
            }

            string[] friends = { "Henk", "Klaas", "Piet", "Kees" };
            foreach (string f in friends)
            {
                Console.WriteLine($"Hello {f}!");
            }

            // declare 2D array
            //string[,] matrix;

            // 3D array
            //int[,,] threeD;

            // two dimensional array
            int[,] array2D = new int[,]
            {
                {1,2,3 },
                {4,5,6 },
                {7,8,9 }
            };

            Console.WriteLine($"Central value is {array2D[1, 1]}");

            string[,,] array3D = new string[,,]
            {
                {
                    {"000", "001"},
                    {"010", "011"},
                    {"020", "021"}
                },
                {
                    {"100", "101"},
                    {"110", "111"},
                    {"120", "121"}
                }
            };

            Console.WriteLine($"Central value is {array3D[1, 1, 1]}");

            string[,] array2DString = new string[3, 2] { { "one", "two" }, { "three", "four" }, { "five", "six" } };

            array2DString[1, 1] = "chicken";

            int dimensions = array2DString.Rank;

            Console.WriteLine($"Central value is {array2DString[3, 0]} and total dimensions");

            int[,] array2D2 = { { 1, 2 }, { 3, 4 } };

        }
    }
}
