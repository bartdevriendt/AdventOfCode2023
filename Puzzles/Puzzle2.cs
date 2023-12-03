using System.Text.RegularExpressions;
using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle2 : PuzzleBase
{

    private Dictionary<string, int> maxBoxes = new Dictionary<string, int>();
    private List<int> possibleGames = new List<int>();
    private void ProcessGamePart1(string line)
    {
        // regex to parse Game ID from line Game 86: 1 blue, 10 red; 2 blue, 5 red; 1 red, 2 blue, 2 green"
        var match = Regex.Match(line, @"Game (?<gameId>\d+): .*");
        if (!match.Success)
        {
            AnsiConsole.WriteLine("Line did not match regex: " + line);
            return;
        }
        int gameId = Convert.ToInt32(match.Groups["gameId"].Value);

        AnsiConsole.WriteLine("Processing game " + gameId);
        
        line = line.Substring(line.IndexOf(':') + 1).Trim();
        string[] parts = line.Split(';');

        bool impossible = false;
        
        foreach (string part in parts)
        {
            AnsiConsole.WriteLine("Part: " + part);
            var match2 = Regex.Matches(part, @"(?<count>\d+) (?<boxColor>\w+)");

            foreach (Match match3 in match2)
            {
                string color = match3.Groups["boxColor"].Value;
                int count = Convert.ToInt32(match3.Groups["count"].Value);
                if(maxBoxes[color]<count) impossible = true;
            }
            
        }

        if (!impossible)
        {
            AnsiConsole.WriteLine("Game " + gameId + " is possible");
            possibleGames.Add(gameId);
        }
        else AnsiConsole.WriteLine("Game " + gameId + " is impossible");
    }
    
    private void ProcessGamePart2(string line)
    {
        // regex to parse Game ID from line Game 86: 1 blue, 10 red; 2 blue, 5 red; 1 red, 2 blue, 2 green"
        var match = Regex.Match(line, @"Game (?<gameId>\d+): .*");
        if (!match.Success)
        {
            AnsiConsole.WriteLine("Line did not match regex: " + line);
            return;
        }
        int gameId = Convert.ToInt32(match.Groups["gameId"].Value);

        AnsiConsole.WriteLine("Processing game " + gameId);
        
        line = line.Substring(line.IndexOf(':') + 1).Trim();
        string[] parts = line.Split(';');

        Dictionary<string, int> boxMinimums = new Dictionary<string, int>();
        boxMinimums["red"] = 0;
        boxMinimums["blue"] = 0;
        boxMinimums["green"] = 0;
        
        
        
        foreach (string part in parts)
        {
            AnsiConsole.WriteLine("Part: " + part);
            var match2 = Regex.Matches(part, @"(?<count>\d+) (?<boxColor>\w+)");

            foreach (Match match3 in match2)
            {
                string color = match3.Groups["boxColor"].Value;
                int count = Convert.ToInt32(match3.Groups["count"].Value);
                if(boxMinimums[color]<count) boxMinimums[color] = count;
            }
            
        }

        possibleGames.Add(boxMinimums["red"] * boxMinimums["blue"] * boxMinimums["green"]);
        
        
    }
    
    public override void Part1()
    {
        maxBoxes["red"] = 12;
        maxBoxes["blue"] = 14;
        maxBoxes["green"] = 13;
        
        AnsiConsole.WriteLine("Puzzle 2 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle2.txt", ProcessGamePart1);
        AnsiConsole.WriteLine("File read");
        AnsiConsole.WriteLine("Sum is " + possibleGames.Sum());
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 2 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle2.txt", ProcessGamePart2);
        AnsiConsole.WriteLine("File read");
        AnsiConsole.WriteLine("Sum is " + possibleGames.Sum());
    }
}