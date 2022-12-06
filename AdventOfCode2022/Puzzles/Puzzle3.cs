namespace AdventOfCode2022.Puzzles
{
    public class Puzzle3
    {
        public Puzzle3()
        {
            string input = Program.ReadFile(@"Inputs\Puzzle3.txt");

            string[] rucksacks = input.Split("\r\n");

            // Part 1 //
            int allPriority = 0;
            foreach (string rucksack in rucksacks)
            {
                // Split string in half //
                int halfLength = rucksack.Length / 2;
                string compartment1 = rucksack.Substring(0, halfLength);
                string compartment2 = rucksack.Substring(halfLength);
                //

                char commonChar = compartment1.Intersect(compartment2).First();
                allPriority += GetPriority(commonChar);
            }

            // Part 2 //
            int allPriorityP2 = 0;
            for (int i = 0; i < rucksacks.Length; i += 3)
            {
                char commonChar = rucksacks[i].Intersect(rucksacks[i + 1]).Intersect(rucksacks[i + 2]).First();
                allPriorityP2 += GetPriority(commonChar);
            }
            //

            Console.Write(
                "=== Rucksack Reorganization: Priorities ===\n" +
                $"Part 1: {allPriority}\n" +
                $"Part 2: {allPriorityP2}\n"
            );
        }

        private int GetPriority(char c)
        {
            return (c < 97) ? (c - 38) : (c - 96);
        }
    }
}
