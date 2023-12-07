using AOC2023.Models;
using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle7 : PuzzleBase
{
    private List<Hand> hands = new List<Hand>();
    private bool jIsJoker = false;
    private void ProcessLine(string line)
    {
        string[] parts = line.Split(' ');
        hands.Add(new Hand(parts[0], int.Parse(parts[1]), jIsJoker));
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 7 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle7.txt", ProcessLine);
        AnsiConsole.WriteLine("File read");

        try
        {
            hands.Sort(new HandComparator());
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            throw;
        }
        

        int bidTotal = 0;
        for (var index = 0; index < hands.Count; index++)
        {
            var hand = hands[index];
            bidTotal += hand.Bid * (index + 1);
        }
        
        AnsiConsole.WriteLine("Bid total is " + bidTotal);
    }

    public override void Part2()
    {
        jIsJoker = true;
        AnsiConsole.WriteLine("Puzzle 7 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle7.txt", ProcessLine);
        AnsiConsole.WriteLine("File read");

        foreach (var hand in hands)
        {
            AnsiConsole.WriteLine(hand.ToString());
        }

        try
        {
            hands.Sort(new HandComparator());
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            throw;
        }
        

        int bidTotal = 0;
        for (var index = 0; index < hands.Count; index++)
        {
            var hand = hands[index];
            bidTotal += hand.Bid * (index + 1);
        }
        
        AnsiConsole.WriteLine("Bid total is " + bidTotal);
    }
}