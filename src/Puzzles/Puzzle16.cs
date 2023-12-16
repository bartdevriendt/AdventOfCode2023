using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using Spectre.Console;
using DenseMatrix = MathNet.Numerics.LinearAlgebra.Single.DenseMatrix;

namespace AOC2023.Puzzles;



public class Beam
{
    public int R { get; set; }
    public int C { get; set; }
    public Direction Direction { get; set; }
}

public class Puzzle16: PuzzleBase
{

    private Matrix<float> contraption;
    private Matrix<float> energized;
    private Stack<Beam> beams = new Stack<Beam>();
    private float HandleChar(char c)
    {
        return c switch 
        {
            '.' => 0,
            '|' => 1,
            '-' => 2,
            '/' => 3,
            '\\' => 4,
            _ => 999
        };
    }

    private int YOffset(Direction dir)
    {
        return dir switch
        {
            Direction.East => 0,
            Direction.West => 0,
            Direction.North => -1,
            Direction.South => 1,
            _ => 0
        };
    }
    
    private int XOffset(Direction dir)
    {
        return dir switch
        {
            Direction.East => 1,
            Direction.West => -1,
            Direction.North => 0,
            Direction.South => 0,
            _ => 0
        };
    }

    private void TraceBeams()
    {
        while (beams.Count > 0)
        {
            var beam = beams.Pop();
            int r = beam.R;
            int c = beam.C;
            
            if(r < 0 || r >= contraption.RowCount || c < 0 || c >= contraption.ColumnCount)
            {
                continue;
            }
            
            energized[r, c] += 1;

            
            
            // AnsiConsole.WriteLine($"Beam at {r}, {c} going {beam.Direction}");
            // PrintMatrix(energized);
            
            Direction dir = beam.Direction;
            if(contraption[r, c] == 0)
            {
                
                beams.Push(new Beam() { R = r + YOffset(dir), C = c + XOffset(dir), Direction = dir });
            }
            else if (contraption[r, c] == 1)
            {
                if (energized[r, c] > 1)
                {
                    continue;
                }
                if (dir == Direction.East || dir == Direction.West)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.North), C = c + XOffset(Direction.North), Direction = Direction.North });
                    beams.Push(new Beam() { R = r + YOffset(Direction.South), C = c + XOffset(Direction.South), Direction = Direction.South });
                }
                else
                {
                    beams.Push(new Beam() { R = r + YOffset(dir), C = c + XOffset(dir), Direction = dir });
                }
            }
            else if (contraption[r, c] == 2)
            {
                if (energized[r, c] > 1)
                {
                    continue;
                }
                if (dir == Direction.North || dir == Direction.South)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.East), C = c + XOffset(Direction.East), Direction = Direction.East });
                    beams.Push(new Beam() { R = r + YOffset(Direction.West), C = c + XOffset(Direction.West), Direction = Direction.West });
                }
                else
                {
                    beams.Push(new Beam() { R = r + YOffset(dir), C = c + XOffset(dir), Direction = dir });
                }
                
            }
            else if (contraption[r, c] == 3)  //  /
            {
                if (dir == Direction.North)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.East), C = c + XOffset(Direction.East), Direction = Direction.East });
                }
                else if (dir == Direction.East)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.North), C = c + XOffset(Direction.North), Direction = Direction.North });
                }
                else if (dir == Direction.South)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.West), C = c + XOffset(Direction.West), Direction = Direction.West });
                }
                else if (dir == Direction.West)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.South), C = c + XOffset(Direction.South), Direction = Direction.South });
                }
            }
            else if (contraption[r, c] == 4)  //  \
            {
                if (dir == Direction.North)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.West), C = c + XOffset(Direction.West), Direction = Direction.West });
                }
                else if (dir == Direction.East)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.South), C = c + XOffset(Direction.South), Direction = Direction.South });
                }
                else if (dir == Direction.South)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.East), C = c + XOffset(Direction.East), Direction = Direction.East });
                }
                else if (dir == Direction.West)
                {
                    beams.Push(new Beam() { R = r + YOffset(Direction.North), C = c + XOffset(Direction.North), Direction = Direction.North });
                }
            }

        }
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 16 part 1");
        AnsiConsole.WriteLine("Reading file");
        contraption = ReadMatrixChar("Data//puzzle16.txt", handleChar: HandleChar);
        energized = new DenseMatrix(contraption.RowCount, contraption.ColumnCount);
        AnsiConsole.WriteLine("File read");
        
        beams.Push(new Beam() { R = 0, C = 0, Direction = Direction.East });
        
        TraceBeams();

        AnsiConsole.WriteLine($"{energized.Storage.EnumerateNonZero().Count()}");

    }

    public override void Part2()
    {
        int maxEnergized = 0;
        
        AnsiConsole.WriteLine("Puzzle 16 part 2");
        AnsiConsole.WriteLine("Reading file");
        contraption = ReadMatrixChar("Data//puzzle16.txt", handleChar: HandleChar);
        
        AnsiConsole.WriteLine("File read");
        
        
        for(int r = 0; r < contraption.RowCount; r++)
        {
            beams.Push(new Beam() { R = r, C = 0, Direction = Direction.East });
            energized = new DenseMatrix(contraption.RowCount, contraption.ColumnCount);
            TraceBeams();
            
            int energizedCount = energized.Storage.EnumerateNonZero().Count();
            if (energizedCount > maxEnergized)
            {
                maxEnergized = energizedCount;
                //AnsiConsole.WriteLine($"Max energized: {maxEnergized} at {r}");
            }
            
            beams.Push(new Beam() { R = r, C = contraption.ColumnCount - 1, Direction = Direction.West });
            energized = new DenseMatrix(contraption.RowCount, contraption.ColumnCount);
            TraceBeams();
            
            energizedCount = energized.Storage.EnumerateNonZero().Count();
            if (energizedCount > maxEnergized)
            {
                maxEnergized = energizedCount;
                //AnsiConsole.WriteLine($"Max energized: {maxEnergized} at {r}");
            }
        }
        
        for(int c = 0; c < contraption.ColumnCount; c++)
        {
            beams.Push(new Beam() { R = 0, C = c, Direction = Direction.South });
            energized = new DenseMatrix(contraption.RowCount, contraption.ColumnCount);
            TraceBeams();
            
            int energizedCount = energized.Storage.EnumerateNonZero().Count();
            if (energizedCount > maxEnergized)
            {
                maxEnergized = energizedCount;
                //AnsiConsole.WriteLine($"Max energized: {maxEnergized} at {r}");
            }
            
            beams.Push(new Beam() { R = contraption.RowCount - 1, C = c, Direction = Direction.North });
            energized = new DenseMatrix(contraption.RowCount, contraption.ColumnCount);
            TraceBeams();
            
            energizedCount = energized.Storage.EnumerateNonZero().Count();
            if (energizedCount > maxEnergized)
            {
                maxEnergized = energizedCount;
                //AnsiConsole.WriteLine($"Max energized: {maxEnergized} at {r}");
            }
        }
        
        AnsiConsole.WriteLine($"{maxEnergized}");
    }
}