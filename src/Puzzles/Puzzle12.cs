using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle12 : PuzzleBase
{
    private int totalCombinations = 0;
    private void TestResults(string current, int[] groups)
    {
        //AnsiConsole.WriteLine("Testing string " + current);
        string[] parts = current.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if(parts.Length != groups.Length)
            return;
        int j = 0;
        while(j < groups.Length && j < parts.Length && parts[j].Length == groups[j])
        {
            j++;
        }

        if (j == groups.Length && j == parts.Length)
        {
            AnsiConsole.WriteLine("Match found: " + current);
            totalCombinations++;
        }
            
    }

    private bool CheckIntermediateResult(string current, int[] groups)
    {
        string[] parts = current.Split(".?".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        int j = 0;
        while (j < parts.Length && j < groups.Length)
        {
            if(parts[j].Length != groups[j])
                return false;
            j++;
        }

        return true;
    }
    
    private void BuildTestStrings(string current, string pattern, int index, int[] groups)
    {
        if(current.Length == pattern.Length)
        {
            TestResults(current, groups);
            return;
        }
        
        while(index < pattern.Length && (pattern[index] == '.' || pattern[index] == '#'))
        {

            current += pattern[index];
            if (pattern[index] == '.')
            {
                if (CheckIntermediateResult(current, groups))
                {
                    return;
                }
            }
            index++;
        }
        
        if(index == pattern.Length)
        {
            TestResults(current, groups);
            return;
        }

        BuildTestStrings(current + '#', pattern, index + 1, groups);
        BuildTestStrings(current + '.', pattern, index + 1, groups);
    }
    
    private void ProcessLine(string line)
    {
        string[] parts = line.Split(' ');
        string pattern = parts[0];
        int[] groups = parts[1].Split(',').Select(int.Parse).ToArray();
        AnsiConsole.WriteLine(line);
        BuildTestStrings("", pattern, 0, groups);
    }
    
    private void ProcessLinePart2(string line)
    {
        string[] parts = line.Split(' ');
        string pattern = parts[0];
        int[] groups = parts[1].Split(',').Select(int.Parse).ToArray();
        List<int> inter = new();
        inter.AddRange(groups);
        inter.AddRange(groups);
        inter.AddRange(groups);
        inter.AddRange(groups);
        inter.AddRange(groups);
        AnsiConsole.WriteLine(line);
        BuildTestStrings("", pattern + "?" + pattern + "?" + pattern + "?" + pattern + "?" + pattern, 0, inter.ToArray());
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 12 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle12.txt", ProcessLine);
        AnsiConsole.WriteLine("File read");
        AnsiConsole.WriteLine("Total combinations: " + totalCombinations);
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 12 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle12.txt", ProcessLinePart2);
        AnsiConsole.WriteLine("File read");
        AnsiConsole.WriteLine("Total combinations: " + totalCombinations);
    }
}