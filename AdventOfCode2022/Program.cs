using AdventOfCode2022.Puzzles;

namespace AdventOfCode2022
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Enter a puzzle number: ");

            // Recieve requested puzzle number //
            int puzzleNumber;
            if (!int.TryParse(Console.ReadLine(), out puzzleNumber))
            {
                Console.WriteLine("Not a valid integer");
                return;
            }
            Console.WriteLine();

            // Puzzle selection //
            switch (puzzleNumber)
            {
                case 1:
                    new Puzzle1();
                    break;

                case 2:
                    new Puzzle2();
                    break;

                default:
                    Console.WriteLine($"Puzzle{puzzleNumber} does not exist");
                    break;
            }
        }
    }
}