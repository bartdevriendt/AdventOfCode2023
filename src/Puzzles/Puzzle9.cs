using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle9 : PuzzleBase
{
    private long sum = 0;
    private bool backFill = true;
    
    private void FindNextValue(string line)
    {
        List<int> numbers = new List<int>(line.Split(' ').Select(x => Int32.Parse(x)).ToList());
        List<List<int>> deltas = new List<List<int>>();
        deltas.Add(numbers);

        bool allSame = false;
        
        while(!allSame)
        {
            var last = deltas.Last();
            var next = new List<int>();
            for (int i = 0; i < last.Count - 1; i++)
            {
                next.Add(last[i + 1] - last[i]);
            }

            deltas.Add(next);
            allSame = next.All(x => x == next[0]);
        }

        if (backFill)
        {

            for (int j = deltas.Count - 1; j > 0; j--)
            {
                int delta = deltas[j].Last();
                deltas[j - 1].Add(deltas[j - 1].Last() + delta);
            }
            sum += deltas[0].Last();
        }
        else
        {
            for (int j = deltas.Count - 1; j > 0 ; j--)
            {
                int delta = deltas[j].First();
                deltas[j - 1].Insert(0, deltas[j - 1].First() - delta);
            }
            
            sum += deltas[0].First();
        }


    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 9 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle9.txt", FindNextValue);
        AnsiConsole.WriteLine("File read");
        
        AnsiConsole.WriteLine($"Sum is {sum}");
    }

    public override void Part2()
    {
        backFill = false;
        AnsiConsole.WriteLine("Puzzle 9 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle9.txt", FindNextValue);
        AnsiConsole.WriteLine("File read");
        
        AnsiConsole.WriteLine($"Sum is {sum}");
    }
}