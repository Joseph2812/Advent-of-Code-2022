namespace AdventOfCode2022.Puzzles
{
    public class Puzzle2
    {
        private enum Move
        {
            Rock = 1,
            Paper,
            Scissors
        }

        public Puzzle2()
        {
            // Read input //
            string input;
            using (FileStream fs = new FileStream(@"Inputs\Puzzle2.txt", FileMode.Open))
            {
                input = new StreamReader(fs).ReadToEnd();
            }
            //

            string[] rounds = input.Split("\r\n"); // {"A Z", "B X"}
            int enemyScore = 0, myScore = 0;
            int enemyScoreP2 = 0, myScoreP2 = 0;

            // Calculate the score depending on the scenarios //
            for (int i = 0; i < rounds.Length; i++)
            {
                // Part 1 //
                Move enemyMove = GetMove(rounds[i][0]);
                Move myMove = GetMove(rounds[i][2]);

                (int enemyRoundScore, int myRoundScore) = GetResult(enemyMove, myMove);

                enemyScore += (int)enemyMove + enemyRoundScore;
                myScore += (int)myMove + myRoundScore;

                // Part 2 //
                myMove = GetOptimalMove(rounds[i][2], enemyMove);

                (enemyRoundScore, myRoundScore) = GetResult(enemyMove, myMove);

                enemyScoreP2 += (int)enemyMove + enemyRoundScore;
                myScoreP2 += (int)myMove + myRoundScore;
            }
            //

            Console.Write(
                "=== Rock, Paper, Scissors [Part 1] Results ===\n" +
                $"Enemy score: {enemyScore}\n" +
                $"My score: {myScore}\n" +

                "\n=== Rock, Paper, Scissors [Part 2] Results ===\n" +
                $"Enemy score: {enemyScoreP2}\n" +
                $"My score: {myScoreP2}\n"
            );
        }

        private Move GetMove(char move)
        {
            switch (move)
            {
                // Rock //
                case 'A':
                case 'X':
                    return Move.Rock;

                // Paper //
                case 'B':
                case 'Y':
                    return Move.Paper;

                // Scissors //
                case 'C':
                case 'Z':
                    return Move.Scissors;

                default: return Move.Rock;
            }
        }

        private Move GetOptimalMove(char outcome, Move enemyMove)
        {
            switch (outcome)
            {
                // Lose //
                case 'X':
                    switch (enemyMove)
                    {
                        case Move.Rock:
                            return Move.Scissors;
                        case Move.Paper:
                            return Move.Rock;
                        case Move.Scissors:
                            return Move.Paper;
                    }
                    break;

                // Draw //
                case 'Y':
                    return enemyMove;

                // Win //
                case 'Z':
                    switch (enemyMove)
                    {
                        case Move.Rock:
                            return Move.Paper;
                        case Move.Paper:
                            return Move.Scissors;
                        case Move.Scissors:
                            return Move.Rock;
                    }
                    break;
            }
            return Move.Rock;
        }

        private (int enemyPoints, int myPoints) GetResult(Move enemyMove, Move myMove)
        {
            switch (enemyMove)
            {
                case Move.Rock:
                    switch (myMove)
                    {
                        case Move.Rock:
                            return (3, 3);
                        case Move.Paper:
                            return (0, 6);
                        case Move.Scissors:
                            return (6, 0);
                    }                        
                    break;

                case Move.Paper:
                    switch (myMove)
                    {
                        case Move.Rock:
                            return (6, 0);
                        case Move.Paper:
                            return (3, 3);
                        case Move.Scissors:
                            return (0, 6);
                    }
                    break;

                case Move.Scissors:
                    switch (myMove)
                    {
                        case Move.Rock:
                            return (0, 6);
                        case Move.Paper:
                            return (6, 0);
                        case Move.Scissors:
                            return (3, 3);
                    }
                    break;
            }
            return (0, 0);
        }       
    }
}
