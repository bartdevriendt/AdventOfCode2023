using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle8 : PuzzleBase
{
    
    private string _instructions = string.Empty;
    private Dictionary<string, (string L, string R)> _rules = new();
    
    private void LoadFile(string content)
    {
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        bool first = true;
        
        foreach (var line in lines)
        {
            AnsiConsole.WriteLine("Line: " + line);
            if(first)
            {
                _instructions = line;
                first = false;
                continue;
            }

            var from = line.Substring(0, 3);
            var toLeft = line.Substring(7, 3);
            var toRight = line.Substring(12, 3);
        
            _rules.Add(from, (toLeft, toRight));
            
        }
    }

    private int NavigatePart1(string start = "AAA", string endsWith = "ZZZ")
    {
        string currPos = start;
        int instrPos = 0;
        int steps = 0;
        while (!currPos.EndsWith(endsWith))
        {
            var rule = _rules[currPos];
            if (_instructions[instrPos] == 'L')
            {
                currPos = rule.L;
            }
            else if (_instructions[instrPos] == 'R')
            {
                currPos = rule.R;
            }

            steps++;
            instrPos++;
            if(instrPos == _instructions.Length)
                instrPos = 0;
        }

        return steps;
    }
    
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 8 part 1");
        AnsiConsole.WriteLine("Reading file");
        var contents = ReadFullFile("Data//puzzle8.txt");
        LoadFile(contents);
        AnsiConsole.WriteLine("File read");
        
        AnsiConsole.WriteLine($"Number of steps: {NavigatePart1()}"); 
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 8 part 2");
        AnsiConsole.WriteLine("Reading file");
        var contents = ReadFullFile("Data//puzzle8.txt");
        LoadFile(contents);
        AnsiConsole.WriteLine("File read");
        
        AnsiConsole.WriteLine($"Number of steps: {NavigatePart2()}");
    }

    private bool IsEndPos(List<string> positions)
    {
        return positions.All(x  => x.EndsWith("Z"));
    }
    
    private long NavigatePart2()
    {
        List<string> startPos = _rules.Keys.Where(k => k.EndsWith("A")).ToList();
        List<int> stepCounts = new List<int>();

        foreach (var start in startPos)
        {
            stepCounts.Add(NavigatePart1(start, "Z"));
        }
        
        // calculate lease common multiplier of stepCounts
        long lcm = LCM(stepCounts);

        return lcm;
    }
    
    public long GCD(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public long LCM(long a, long b)
    {
        return (a / GCD(a, b)) * b;
    }

    public long LCM(List<int> numbers)
    {
        long lcm = numbers[0];
        for (int i = 1; i < numbers.Count; i++)
        {
            lcm = LCM(lcm, numbers[i]);
        }
        return lcm;
    }
}