using System.Diagnostics;
using AOC2023.Puzzles;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AOC2023;

public class PuzzleCommandSettings : CommandSettings
{
    [CommandArgument(0, "[puzzleNumber]")]
    public int? PuzzleNumber { get; set; }
    
    [CommandArgument(1, "[part]")]
    public int? Part { get; set; }
}

public class PuzzleCommand : Command<PuzzleCommandSettings>
{
    public override int Execute(CommandContext context, PuzzleCommandSettings settings)
    {
        if(settings.PuzzleNumber == null)
        {
            settings.PuzzleNumber = AnsiConsole.Ask<int>("Which puzzle do you want to run?");
        }
        
        if(settings.Part == null)
        {
            settings.Part = AnsiConsole.Ask<int>("Which part do you want to run?");
        }
        
        Type? t = Type.GetType("AOC2023.Puzzles.Puzzle" + settings.PuzzleNumber);
        if (t == null)
        {
            AnsiConsole.WriteLine("[red]Puzzle not found[/]");
            return -1;
        }

        PuzzleBase? b = Activator.CreateInstance(t) as PuzzleBase;
        
        if (b == null)
        {
            AnsiConsole.WriteLine("[red]Puzzle not found[/]");
            return -1;
        }
        
        Stopwatch sw = Stopwatch.StartNew();
        
        AnsiConsole.MarkupLine($"[green]Start {DateTime.Now}[/]");
        
        switch (settings.Part)
        {
            case 1:
                b.Part1();
                break;
            case 2:
                b.Part2();
                break;
            default:
                AnsiConsole.WriteLine("[red]Part not found[/]");
                return -1;
        }
        sw.Stop();
        AnsiConsole.MarkupLine($"[green]End {DateTime.Now} in {sw.Elapsed.TotalSeconds}[/]");

        return 0;
    }
}