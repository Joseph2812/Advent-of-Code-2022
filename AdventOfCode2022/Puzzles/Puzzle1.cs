namespace AdventOfCode2022.Puzzles
{
    public class Puzzle1
    {
        public Puzzle1()
        {
            // Read input //
            string input;          
            using (FileStream fs = new FileStream(@"Inputs\Puzzle1.txt", FileMode.Open))
            {
                input = new StreamReader(fs).ReadToEnd(); 
            }
            //

            string[] itemCaloriesPerElf = input.Split("\r\n\r\n");          // {"2000\r\n3000", "1000\r\n3000"}
            int[] totalCaloriesPerElf = new int[itemCaloriesPerElf.Length]; // {5000          , 4000          }

            // Add each elf's calorie items up for the total //
            for (int i = 0; i < itemCaloriesPerElf.Length; i++)
            {
                string[] calories = itemCaloriesPerElf[i].Split("\r\n"); // {"2000", "3000"}, {"1000", "3000"}
                for (int j = 0; j < calories.Length; j++)
                {
                    totalCaloriesPerElf[i] += int.Parse(calories[j]);
                }
            }

            // Find the top 3 calorie holding elfs, and get the grand total //
            Array.Sort(totalCaloriesPerElf);
            Console.Write(
                "=== Top 3 Calorie Holding Elfs ===\n" +
                $"1st: {totalCaloriesPerElf[totalCaloriesPerElf.Length - 1]}\n" +
                $"2nd: {totalCaloriesPerElf[totalCaloriesPerElf.Length - 2]}\n" +
                $"3rd: {totalCaloriesPerElf[totalCaloriesPerElf.Length - 3]}\n" +

                string.Format("\nTotal: {0}",
                    totalCaloriesPerElf[totalCaloriesPerElf.Length - 1] +
                    totalCaloriesPerElf[totalCaloriesPerElf.Length - 2] +
                    totalCaloriesPerElf[totalCaloriesPerElf.Length - 3]
                )
            );
        }
    }
}
