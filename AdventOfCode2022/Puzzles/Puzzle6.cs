namespace AdventOfCode2022.Puzzles
{
    public class Puzzle6
    {
        private readonly string _signal;

        public Puzzle6()
        {
            _signal = Program.ReadFile(@"Inputs\Puzzle6.txt");

            int endOfMarker = GetEndOfMarker(4);
            int endOfMarkerP2 = GetEndOfMarker(14);

            Console.Write(
                "=== Tuning Trouble: End of marker ===\n" +
                $"Part 1: {endOfMarker}\n" +
                $"Part 2: {endOfMarkerP2}\n"
            );
        }

        private int GetEndOfMarker(int markerLength)
        {
            List<char> uniqueChars = new List<char>(markerLength);
            int iterCount = 0;

            foreach (char c in _signal)
            {
                iterCount++;

                int firstIndex = uniqueChars.IndexOf(c);
                if (firstIndex != -1) { uniqueChars.RemoveRange(0, firstIndex + 1); }

                uniqueChars.Add(c);
                if (uniqueChars.Count == markerLength) { break; }
            }
            return iterCount;
        }
    }
}
