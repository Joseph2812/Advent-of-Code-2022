using System.Collections.ObjectModel;

namespace AdventOfCode2022.Puzzles
{
    public class Puzzle7
    {
        private const int TotalDiskSize = 70_000_000;
        private const int TargetDiskSize = 40_000_000;

        private struct File
        {
            public string Name;
            public int Size;
        }

        private class Directory
        {
            public Directory? PreviousDir; // Root can be null

            public string Name;
            public int Size;
            public readonly DirectoryCollection InnerDirs = new DirectoryCollection();

            private readonly List<File> Files = new List<File>();

            public Directory(string name, Directory? previousDir)
            { 
                Name = name;
                PreviousDir = previousDir;
            }

            public Directory this[string name]
            {
                get { return InnerDirs[name]; }
            }

            public void AddFile(File file) 
            { 
                Files.Add(file);
                Size += file.Size;

                Directory? nextPrevDir = PreviousDir;
                while (nextPrevDir != null)
                {
                    nextPrevDir.Size += file.Size;
                    nextPrevDir = nextPrevDir.PreviousDir;
                }   
            }
            public IEnumerator<File> GetFilesEnumerator() { return Files.GetEnumerator(); }
        }

        private class DirectoryCollection : KeyedCollection<string, Directory>
        {
            protected override string GetKeyForItem(Directory item) { return item.Name; }
        }

        private readonly Directory _rootDirectory = new Directory("/", null);
        private Directory _currentDirectory;

        private int _sumOfDirAtUnder100000; // Part 1
        private int _smallestRequiredDeletionSize = TotalDiskSize; // Part 2

        public Puzzle7()
        {
            string input = Program.ReadFile(@"Inputs\Puzzle7.txt");
            
            _currentDirectory = _rootDirectory;
            SetDirectories(input);

            PrintAllDirectoriesRecursively(_rootDirectory);
            Console.Write(
                "\n=== No Space Left On Device ===\n" +
                $"Sum of directories <= 100,000: {_sumOfDirAtUnder100000}\n" +
                $"Smallest directory deletion to create 30,000,000 of disk space: {_smallestRequiredDeletionSize}\n"         
            );
        }

        private void SetDirectories(string input)
        {
            string[] lines = input.Split("\r\n");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] segments = lines[i].Split(' ');
                if (!ExecuteCommand(segments)) { continue; }

                int newIdx = AddDirectorysAndFiles(lines, i + 1);
                if (newIdx == -1) { return; }
                else              { i = newIdx - 1; }
            }
        }

        private bool ExecuteCommand(string[] segments) // Returns whether the next files are being printed for this directory
        {
            switch (segments[1])
            {
                case "cd":
                    switch (segments[2])
                    {
                        case "/":
                            _currentDirectory = _rootDirectory;
                            break;

                        case "..":
                            if (_currentDirectory.PreviousDir != null)
                            {
                                _currentDirectory = _currentDirectory.PreviousDir;
                            }                         
                            break;

                        default:
                            _currentDirectory = _currentDirectory[segments[2]];
                            break;
                    }
                    break;

                case "ls":
                    return true;
            }
            return false;
        }

        private int AddDirectorysAndFiles(string[] lines, int startIdx) // Returns new index to start from
        {
            for (int i = startIdx; i < lines.Length; i++)
            {
                string[] segments = lines[i].Split(' ');
                if (segments[0] == "$") { return i; }

                if (segments[0] == "dir")
                {
                    Directory dir = new Directory(segments[1], _currentDirectory);
                    _currentDirectory.InnerDirs.Add(dir);
                }
                else // File
                {
                    _currentDirectory.AddFile(new File()
                    {
                        Name = segments[1],
                        Size = int.Parse(segments[0])
                    });
                }
            }
            return -1;
        }

        private void PrintAllDirectoriesRecursively(Directory dir, int depth = 0)
        {
            // Print all the contents //
            int indent = depth * 2;

            string dirStr = $"> {dir.Name} ({dir.Size})";
            Console.WriteLine(dirStr.PadLeft(dirStr.Length + indent));

            IEnumerator<File> files = dir.GetFilesEnumerator();
            while (files.MoveNext())
            {
                string fileStr = $"- {files.Current.Name} ({files.Current.Size})";
                Console.WriteLine(fileStr.PadLeft(fileStr.Length + (indent + 2)));
            }

            // Answers to both parts //
            if (dir.Size <= 100_000) { _sumOfDirAtUnder100000 += dir.Size; }
            if ((dir.Size >= _rootDirectory.Size - TargetDiskSize) && (dir.Size < _smallestRequiredDeletionSize))
            {
                _smallestRequiredDeletionSize = dir.Size;
            }

            // Look for more inner directories //
            IEnumerator<Directory> dirs = dir.InnerDirs.GetEnumerator();
            while (dirs.MoveNext())
            {
                PrintAllDirectoriesRecursively(dirs.Current, depth + 1);
            }
        }
    }
}
