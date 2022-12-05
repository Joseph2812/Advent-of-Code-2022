namespace AdventOfCode2022.Puzzles
{
    public class Puzzle5
    {
        private const int StackCount = 9;

        public Puzzle5()
        {
            string input = Program.ReadFile(@"Inputs\Puzzle5.txt");

            // [0] = Initial crates, [1] = Commands
            string[] sections = input.Split("\r\n\r\n");

            Stack<char>[] stacks = LoadStacks(sections[0]);
            Stack<char>[] stacksP2 = LoadStacks(sections[0]);
            
            Console.Write(
                "=== Supply Stacks ===\n" +
                "Before:\n"
            );
            PrintStacks(stacks);

            // Part 1 //
            RunCommandsOnStacks(stacks, sections[1], false);

            Console.WriteLine("\nPart 1 After:");
            PrintStacks(stacks);

            // Part 2 //
            RunCommandsOnStacks(stacksP2, sections[1], true);

            Console.WriteLine("\nPart 2 After:");
            PrintStacks(stacksP2);
        }

        private Stack<char>[] LoadStacks(string crates)
        {
            Stack<char>[] stacks = new Stack<char>[StackCount];
            for (int i = 0; i < stacks.Length; i++) { stacks[i] = new Stack<char>(); }

            string[] row = crates.Split("\r\n");
            for (int i = row.Length - 2; i >= 0; i--)
            {
                for (int j = 0; j < stacks.Length; j++)
                {
                    char crate = row[i][1 + (4 * j)];
                    if (crate == ' ') { continue; }

                    stacks[j].Push(crate);
                }
            }
            return stacks;
        }

        private void PrintStacks(Stack<char>[] stacks)
        {
            // Print each stack (Left => Right | Bottom => Top) //
            for (int i = 0; i < stacks.Length; i++)
            {
                Console.Write($"{i + 1} => ");

                char[] crates = stacks[i].ToArray();
                for (int j = crates.Length - 1; j >= 0; j--)
                {
                    Console.Write($"[{crates[j]}] ");
                }
                Console.WriteLine();
            }

            // Print the top of each stack //
            Console.Write("Top: ");
            foreach (Stack<char> stack in stacks)
            {
                Console.Write(stack.Peek());
            }
            Console.WriteLine();
        }

        private void RunCommandsOnStacks(Stack<char>[] stacks, string commandsStr, bool part2)
        {
            string[] commands = commandsStr.Split("\r\n");

            foreach (string command in commands)
            {
                string[] segments = command.Split(' ');

                int amount = int.Parse(segments[1]);
                int source = int.Parse(segments[3]) - 1;
                int destination = int.Parse(segments[5]) - 1;

                char[] cratesToReverse = new char[amount];
                for (int i = 0; i < amount; i++)
                {
                    if (part2) { cratesToReverse[i] = stacks[source].Pop(); }
                    else       { stacks[destination].Push(stacks[source].Pop()); }
                }
                if (!part2) { continue; }

                // Keep in the same order as previous stack by reversing (for part 2) //
                for (int i = cratesToReverse.Length - 1; i >= 0; i--)
                {
                    stacks[destination].Push(cratesToReverse[i]);
                }
            }
        }
    }
}
