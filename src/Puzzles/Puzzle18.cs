using System.Drawing.Drawing2D;
using AOC2023.Models;
using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle18 : PuzzleBase
{
    private List<(char dir, int steps, string color)> navigation = new();
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 18 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadFile();
        ProcessInput();
    }

    private void ProcessInput()
    {

        (double X, double Y) start = (0, 0);
        Polygon poly = new Polygon();
        poly.AddPoint(start);

        int circumference = 0;
        
        
        
        foreach (var (dir, steps, color) in navigation)
        {
            (double X, double Y) end = (0,0);
            switch (dir)
            {
                case 'U':
                    end = (start.X, start.Y - steps);
                    break;
                case 'D':
                    end = (start.X, start.Y + steps);
                    break;
                case 'L':
                    end = (start.X - steps, start.Y);
                    break;
                case 'R':
                    end = (start.X + steps, start.Y);
                    break;
            }
            poly.AddPoint(end);
            circumference += steps;
            start = end;
        }
        
        
        
        AnsiConsole.WriteLine($"Total holes: {poly.GetArea() + circumference / 2 + 1}");
    }
    private void ReadFile()
    {
        ReadFileLineByLine("Data//puzzle18.txt", HandleLine);
        
    }

    GraphicsPath p=new GraphicsPath();
    
    
    private void HandleLine(string line)
    {
        var parts = line.Split(' ');
        var dir = parts[0][0];
        var steps = int.Parse(parts[1]);
        var color = parts[2].Substring(1, 7);
        navigation.Add((dir, steps, color));
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 18 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadFile();
        ProcessInputPart2();
    }

    private void ProcessInputPart2()
    {

        (double X, double Y) start = (0, 0);
        Polygon poly = new Polygon();
        poly.AddPoint(start);

        double circumference = 0;

        foreach (var (d, s, color) in navigation)
        {

            var steps = Convert.ToInt64(color[1..6], 16);
            var dir = color[^1] switch
            {
                '0' => 'R',
                '1' => 'D',
                '2' => 'L',
                '3' => 'U',
            };

            AnsiConsole.WriteLine($"{dir} {steps}");

            (double X, double Y) end = (0,0);
            switch (dir)
            {
                case 'U':
                    end = (start.X, start.Y - steps);
                    break;
                case 'D':
                    end = (start.X, start.Y + steps);
                    break;
                case 'L':
                    end = (start.X - steps, start.Y);
                    break;
                case 'R':
                    end = (start.X + steps, start.Y);
                    break;
            }

            poly.AddPoint(end);
            circumference += steps;
            start = end;
        }

        double area = poly.GetArea();
        AnsiConsole.WriteLine($"Total holes: {area + circumference / 2 + 1}");
    }
}