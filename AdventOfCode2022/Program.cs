using AdventOfCode2022.Puzzles;

namespace AdventOfCode2022
{
    internal static class Program
    {
        public static string ReadFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                return new StreamReader(fs).ReadToEnd();
            }
        }

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

                case 3:
                    new Puzzle3();
                    break;

                case 4:
                    new Puzzle4();
                    break;

                case 5:
                    new Puzzle5();
                    break;

                case 6:
                    new Puzzle6();
                    break;

                case 7:
                    new Puzzle7();
                    break;

                default:
                    Console.WriteLine($"Puzzle{puzzleNumber} does not exist");
                    break;
            }
        }
    }
}