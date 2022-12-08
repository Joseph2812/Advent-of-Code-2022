namespace AdventOfCode2022.Puzzles
{
    public class Puzzle8
    {
        private string[] _rows;

        public Puzzle8()
        {
            string input = Program.ReadFile(@"Inputs\Puzzle8.txt");
            _rows = input.Split("\r\n");

            bool[][] visibleTrees = GetPart1Visibility();
            int[][] scenicValues = GetPart2ScenicValues();

            Console.Write(
                "=== Treetop Tree House: Visible trees ===\n" +
                $"Part 1: {visibleTrees.Sum(x => x.Sum(x => (x ? 1 : 0)))}\n" +
                $"Part 2: {scenicValues.Max(x => x.Max())}\n"
            );
        }

        private bool[][] GetPart1Visibility()
        {
            bool[][] visibleTrees = new bool[_rows.Length][];

            // Horizontal check //
            for (int y = 0; y < _rows.Length; y++)
            {
                visibleTrees[y] = new bool[_rows[y].Length];

                // One side //
                int highestTree = -1;
                for (int x = 0; x < _rows[y].Length; x++)
                {
                    if (highestTree < _rows[y][x])
                    {
                        highestTree = _rows[y][x];
                        visibleTrees[y][x] = true;
                    }
                }
                // Other side //
                highestTree = -1;
                for (int x = _rows[y].Length - 1; x >= 0; x--)
                {
                    if (highestTree < _rows[y][x])
                    {
                        highestTree = _rows[y][x];
                        visibleTrees[y][x] = true;
                    }
                }
            }

            // Vertical check //
            for (int x = 0; x < _rows[0].Length; x++)
            {
                // One side //
                int highestTree = -1;
                for (int y = 0; y < _rows.Length; y++)
                {
                    if (highestTree < _rows[y][x])
                    {
                        highestTree = _rows[y][x];
                        visibleTrees[y][x] = true;
                    }
                }
                // Other side //
                highestTree = -1;
                for (int y = _rows.Length - 1; y >= 0; y--)
                {
                    if (highestTree < _rows[y][x])
                    {
                        highestTree = _rows[y][x];
                        visibleTrees[y][x] = true;
                    }
                }
            }
            return visibleTrees;
        }

        private int[][] GetPart2ScenicValues()
        {
            int[][] scenicValues = new int[_rows.Length][];

            for (int y = 0; y < _rows.Length; y++)
            {
                scenicValues[y] = new int[_rows[y].Length];
                for (int x = 0; x < _rows[y].Length; x++)
                {
                    int north = 0, south = 0, east = 0, west = 0;

                    // North //
                    for (int i = 1; (y - i) >= 0; i++)
                    {
                        north = i;
                        if (_rows[y - i][x] >= _rows[y][x]) { break; }
                    }
                    // South //
                    for (int i = 1; (y + i) < _rows.Length; i++)
                    {
                        south = i;
                        if (_rows[y + i][x] >= _rows[y][x]) { break; }
                    }
                    // East //
                    for (int i = 1; (x + i) < _rows[y].Length; i++)
                    {
                        east = i;
                        if (_rows[y][x + i] >= _rows[y][x]) { break; }
                    }
                    // West //
                    for (int i = 1; (x - i) >= 0; i++)
                    {
                        west = i;
                        if (_rows[y][x - i] >= _rows[y][x]) { break; }
                    }

                    scenicValues[y][x] = north * south * east * west;
                }
            }
            return scenicValues;
        }
    }
}
