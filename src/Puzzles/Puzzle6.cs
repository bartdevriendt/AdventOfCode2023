using AOC2023.Models;
using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle6 : PuzzleBase
{
    List<BoatInfo> boatInfos = new List<BoatInfo>();
    private Func<BoatInfo, long, bool> _distanceRecord = (boatInfo, hold) => (boatInfo.Time - hold) * hold > boatInfo.RecordDistance;

    public Puzzle6()
    {
        SetupInput();
    }
    
    private void SetupInput()
    {
        boatInfos.Add(new BoatInfo() { Time = 40, RecordDistance = 277 });
        boatInfos.Add(new BoatInfo() { Time = 82, RecordDistance = 1338 });
        boatInfos.Add(new BoatInfo() { Time = 91, RecordDistance = 1349 });
        boatInfos.Add(new BoatInfo() { Time = 66, RecordDistance = 1063 });
        boatInfos.Add(new BoatInfo() { Time = 40829166, RecordDistance = 277133813491063 });
    }
    public override void Part1()
    {
        int result = 1;
        foreach(var boatInfo in boatInfos.Take(4))
        {
            result *= Enumerable.Range(0, boatInfo.Time + 1).Count(x => _distanceRecord.Invoke(boatInfo, x));
        }
        
        AnsiConsole.WriteLine($"Result: {result}");
    }

    public override void Part2()
    {
        int result = 1;
        var boatInfo = boatInfos[4];
        result = Enumerable.Range(0, boatInfo.Time + 1).Count(x => _distanceRecord.Invoke(boatInfo, x));
        
        AnsiConsole.WriteLine($"Result: {result}");
    }
}