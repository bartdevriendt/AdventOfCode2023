using AOC2023.Models;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle11 : PuzzleBase
{
    private List<Galaxy> _galaxies = new();

    private int currentRow = 0;
    private List<int> _columnsWithGalaxies = new();
    private int rowExpansion = 0;
    private int EXPANSION_FACTOR = 1;
    private int _origColumns = 0;
    private int GetDistance(Galaxy p1, Galaxy p2)
    {
        return Math.Abs(p1.r - p2.r) + Math.Abs(p1.c - p2.c);
    }

    private long FindTotalDistance()
    {
        int count = 0;
        long total = 0;
        for(int j=0;j<_galaxies.Count-1;j++)
        {
            for(int k=j+1;k<_galaxies.Count;k++)
            {
                count++;
                var distance = GetDistance(_galaxies[j], _galaxies[k]);
                //AnsiConsole.WriteLine($"Distance between {_galaxies[j]} and {_galaxies[k]}: {distance}");
                total += distance;
            }
        }
        
        AnsiConsole.WriteLine($"Number of pairs: {count}");

        return total;

    }

    private void HandleLine(string line)
    {
        _origColumns = line.Length;
        if (!line.Contains('#'))
        {
            rowExpansion += EXPANSION_FACTOR;
        }
        else
        {
            int col = 0;
            foreach(var c in line.AsSpan())
            {
                if (c == '#')
                {
                    _galaxies.Add(new Galaxy(currentRow + rowExpansion, col));
                    if(!_columnsWithGalaxies.Contains(col))
                    {
                        _columnsWithGalaxies.Add(col);
                    }
                }
                col++;
            }
        }

        currentRow++;
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 11 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle11.txt", HandleLine);
        AnsiConsole.WriteLine("File read");
        ExpandUniverse();
        //PrintMatrix(_universe);
        //_galaxies = FindGalaxies();
        AnsiConsole.WriteLine($"Number of galaxies: {_galaxies.Count}");
        
        foreach(var galaxy in _galaxies)
        {
            AnsiConsole.WriteLine($"Galaxy: {galaxy.r} {galaxy.c}");
        }
        
        AnsiConsole.WriteLine($"Total distance: {FindTotalDistance()}");
    }

    private void ExpandUniverse()
    {
        int colExpansion = 0;
        for(int j = 0; j < _origColumns; j++)
        {
            if (!_columnsWithGalaxies.Contains(j))
            {
                foreach(var galaxy in _galaxies)
                {
                    if (galaxy.c >= j + colExpansion)
                    {
                        galaxy.c += EXPANSION_FACTOR;
                    }
                }
                colExpansion += EXPANSION_FACTOR;
            }
        }
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 11 part 2");
        AnsiConsole.WriteLine("Reading file");
        // add 999999 rows/columns for each empty row/column
        EXPANSION_FACTOR = 999999;
        ReadFileLineByLine("Data//puzzle11.txt", HandleLine);
        AnsiConsole.WriteLine("File read");
        ExpandUniverse();
        AnsiConsole.WriteLine($"Number of galaxies: {_galaxies.Count}");
        
        foreach(var galaxy in _galaxies)
        {
            AnsiConsole.WriteLine($"Galaxy: {galaxy.r} {galaxy.c}");
        }
        
        AnsiConsole.WriteLine($"Total distance: {FindTotalDistance()}");
    }
}