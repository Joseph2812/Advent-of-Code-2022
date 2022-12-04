namespace AdventOfCode2022.Puzzles
{
    public class Puzzle4
    {
        public Puzzle4()
        {
            string input = Program.ReadFile(@"Inputs\Puzzle4.txt");

            string[] pairs = input.Split("\r\n");
            int fullContainCount = 0;
            int partialContainCount = 0;

            foreach (string pair in pairs)
            {
                string[] timesStr = pair.Split(new char[] {',', '-'}); // "1-2,3-4" => {"1", 2", "3", "4"}

                int[] times = new int[timesStr.Length]; // {1, 2, 3, 4}
                for (int i = 0; i < times.Length; i++) { times[i] = int.Parse(timesStr[i]); }
                
                // Both time values within other's range //
                if ((times[0] >= times[2] && times[1] <= times[3]) || // [0]&[1] within [2]-[3]
                    (times[0] <= times[2] && times[1] >= times[3]))   // [2]&[3] within [0]-[1]
                {
                    fullContainCount++;
                    partialContainCount++;
                }
                // Only one time value within other's range //
                else if (!(times[0] > times[3] || times[1] < times[2]) || // NOT [0]OR[1] outside [2]-[3]
                         !(times[2] > times[1] || times[3] < times[0]))   // NOT [2]OR[3] outside [0]-[1]
                {
                    partialContainCount++;
                }
            }

            Console.Write(
                "=== Camp Cleanup: Count ===\n" +
                $"[Part 1] Fully contained count: {fullContainCount}\n" +
                $"[Part 2] Partially contained count: {partialContainCount}\n"
            );
        }
    }
}
