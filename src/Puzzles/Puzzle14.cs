using MathNet.Numerics.LinearAlgebra;
using Spectre.Console;

namespace AOC2023.Puzzles;

public enum Direction
{
    North = 1,
    West = 2,
    South = 3,
    East = 4
}

public class Puzzle14 : PuzzleBase
{
    private Matrix<float> platform;
    private float HandleChar(char c)
    {
        return c switch 
        {
            '.' => 0,
            '#' => 1,
            'O' => 2,
            _ => 999
        };
    }

    private void TiltPlatform(Direction d)
    {
        int x = 0;
        int y = 0;
        int rStart = 0;
        int rEnd = 0;
        int cStart = 0;
        int cEnd = 0;
        int rIncr = 1;
        int cIncr = 1;
        
        switch (d)
        {
            case Direction.North:
                y = -1;
                x = 0;
                cEnd = platform.ColumnCount;
                rEnd = platform.RowCount;
                break;
            case Direction.West:
                y = 0;
                x = -1;
                cEnd = platform.ColumnCount;
                rEnd = platform.RowCount;
                break;
            case Direction.South:
                y = 1;
                x = 0;
                rStart = platform.RowCount - 1;
                rEnd = -1;
                rIncr = -1;
                cEnd = platform.ColumnCount;
                break;
            case Direction.East:
                y = 0;
                x = 1;
                cStart = platform.ColumnCount - 1;
                cEnd = -1;
                cIncr = -1;
                rEnd = platform.RowCount;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(d), d, null);
        }

        
        
        for(int r=rStart; r != rEnd; r+=rIncr)
        {
            for(int c=cStart; c != cEnd; c+=cIncr)
            {
                
                int newR = r + y;
                int newC = c + x;
                if (newR < 0 || newR >= platform.RowCount || newC < 0 || newC >= platform.ColumnCount)
                {
                    continue;
                }

                if(platform[r, c] == 2f)
                {

                    int nextR = r;
                    int nextC = c;
                    while (platform[newR, newC] == 0)
                    {
                        //platform[newR - y, newC - x] = 0;
                        //platform[newR, newC] = 2;
                        nextR = newR;
                        nextC = newC;
                        newR += y;
                        newC += x;
                        if (newR < 0 || newR >= platform.RowCount || newC < 0 || newC >= platform.ColumnCount)
                        {
                            break;
                        }
                    }
                    
                    platform[r, c] = 0;
                    platform[nextR, nextC] = 2;
                }
                
            }
        }
        
        
        //AnsiConsole.WriteLine($"After tilting {d}:");
        //PrintMatrix(platform);
    }

    private void CalculateLoad()
    {

        int totalLoad = 0;
        
        for (int r = 0; r < platform.RowCount; r++)
        {
            for (int c = 0; c < platform.ColumnCount; c++)
            {
                if (platform[r, c] == 2f)
                    totalLoad += platform.RowCount - r;

            }
        }
        
        AnsiConsole.WriteLine("Total load is " + totalLoad);

    }
    
    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 14 part 2");
        AnsiConsole.WriteLine("Reading file");
        platform = ReadMatrixChar("Data//puzzle14.txt", handleChar: HandleChar);
        AnsiConsole.WriteLine("File read");

        
        var platformOrig = platform.Clone();
        var flat = platformOrig.ToColumnMajorArray();
        for (long i = 0; i < 1000000000; i++)
        {
            TiltPlatform(Direction.North);
            TiltPlatform(Direction.West);
            TiltPlatform(Direction.South);
            TiltPlatform(Direction.East);
            ;
            if (platform.Storage.Equals(platformOrig.Storage))
            {
                AnsiConsole.WriteLine($"Platform is the same as original after {i} iterations");
                break;
            }
        }

        List<int> test = new List<int>();
        test.Select((i, j) => (i, j));

        //PrintMatrix(platform);
        CalculateLoad();
    }

    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 14 part 1");
        AnsiConsole.WriteLine("Reading file");
        platform = ReadMatrixChar("Data//puzzle14.txt", handleChar: HandleChar);
        AnsiConsole.WriteLine("File read");
        TiltPlatform(Direction.North);
        PrintMatrix(platform);
        CalculateLoad();
    }
}