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
            for (int i = 0; i < rucksacks.Length; i++)
            {
                int halfLength = rucksacks[i].Length / 2;
                string compartment1 = rucksacks[i].Substring(0, halfLength);
                string compartment2 = rucksacks[i].Substring(halfLength);

                for (int j = 0; j < compartment1.Length; j++)
                {
                    if (compartment2.Contains(compartment1[j]))
                    {
                        allPriority += GetPriority(compartment1[j]);
                        break;
                    }
                }              
            }

            // Part 2 //
            int allPriorityP2 = 0;
            for (int i = 0; i < rucksacks.Length; i += 3)
            {
                // Check what 1st & 2nd have in common //
                List<char> commonChar1st2nd = new List<char>();
                for (int j = 0; j < rucksacks[i].Length; j++)
                {
                    if (rucksacks[i + 1].Contains(rucksacks[i][j]))
                    {
                        commonChar1st2nd.Add(rucksacks[i][j]);
                    }
                }

                // Check what 1st & 2nd have in common WITH the 3rd //
                for (int j = 0; j < commonChar1st2nd.Count; j++)
                {
                    if (rucksacks[i + 2].Contains(commonChar1st2nd[j]))
                    {
                        allPriorityP2 += GetPriority(commonChar1st2nd[j]);
                        break;
                    }
                }
            }
            //

            Console.Write(
                "=== Rucksack Reorganization Priorities ===\n" +
                $"Part 1: {allPriority}\n" +
                $"Part 2: {allPriorityP2}\n"
            );
        }

        private int GetPriority(char a)
        {
            return (a < 97) ? (a - 38) : (a - 96);
        }
    }
}
