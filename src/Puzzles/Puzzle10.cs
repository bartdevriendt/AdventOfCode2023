using System.Drawing;
using System.Drawing.Drawing2D;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Single;
using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle10 : PuzzleBase
{

    private Matrix<float> _loopMatrix;
    private List<PointF> _points = new();
    private float HandleChar(char arg)
    {
        // codes:
        // 0 = start
        // 1 = |
        // 2 = -
        // 3 = L    
        // 4 = F
        // 5 = 7
        // 6 = J
        // -1 = .    
        
        return arg switch
        {
            'S' => 0,
            '|' => 1,
            '-' => 2,
            'L' => 3,
            'F' => 4,
            '7' => 5,
            'J' => 6,
            _ => -1
        };
        
    }

    private int FindPath(Matrix<float> m)
    {
        Tuple<int, int, float> startPos = m.Find(x => x == 0, Zeros.Include);
        int count = 1;
        
        AnsiConsole.WriteLine("Start position: " + startPos);

        List<PointF> points1 = new();
        List<PointF> points2 = new();
        
        _loopMatrix[startPos.Item1, startPos.Item2] = 9f;
        
        points1.Add(new PointF(startPos.Item2, startPos.Item1));
        points2.Add(new PointF(startPos.Item2, startPos.Item1));
        
        // directions:   1 = up, 2 = right, 3 = down, 4 = left
        
        List<(int x, int y, int d)> startPositions = new List<(int x, int y, int d)>();
        
        // we know from the input that we have to go up & down 
        if (m[startPos.Item1 - 1, startPos.Item2] == 1f || m[startPos.Item1 - 1, startPos.Item2] == 4f || m[startPos.Item1 - 1, startPos.Item2] == 5f)
        {
            startPositions.Add((startPos.Item2, startPos.Item1 - 1, 1));
        }
        if (m[startPos.Item1 + 1, startPos.Item2] == 1f || m[startPos.Item1 + 1, startPos.Item2] == 3f || m[startPos.Item1 + 1, startPos.Item2] == 6f)
        {
            startPositions.Add((startPos.Item2, startPos.Item1 + 1, 3));
        }
        if (m[startPos.Item1, startPos.Item2 - 1] == 2f || m[startPos.Item1, startPos.Item2 - 1] == 3f || m[startPos.Item1, startPos.Item2 - 1] == 4f)
        {
            startPositions.Add((startPos.Item2 - 1, startPos.Item1, 4));
        }
        if (m[startPos.Item1, startPos.Item2 + 1] == 2f || m[startPos.Item1, startPos.Item2 + 1] == 5f || m[startPos.Item1, startPos.Item2 + 1] == 6f)
        {
            startPositions.Add((startPos.Item2 + 1, startPos.Item1, 2));
        }

        (int x, int y, int d) currPos1 = startPositions[0];
        (int x, int y, int d) currPos2 = startPositions[1];

        _loopMatrix[currPos1.Item2, currPos1.Item1] = 9f;
        _loopMatrix[currPos2.Item2, currPos2.Item1] = 9f;
        
        points1.Add(new PointF(currPos1.x, currPos1.y));
        points2.Add(new PointF(currPos2.x, currPos2.y));
        
        while (currPos1.x != currPos2.x || currPos1.y != currPos2.y)
        {
            currPos1 = MoveNext(currPos1, m);
            currPos2 = MoveNext(currPos2, m);
            
            // AnsiConsole.WriteLine("currPos1: " + currPos1);
            // AnsiConsole.WriteLine("currPos2: " + currPos2);
        
            _loopMatrix[currPos1.Item2, currPos1.Item1] = 9f;
            _loopMatrix[currPos2.Item2, currPos2.Item1] = 9f;
            
            points1.Add(new PointF(currPos1.x, currPos1.y));
            points2.Add(new PointF(currPos2.x, currPos2.y));
            
            count++;
        }

        points2.Reverse();
        _points.AddRange(points1);
        _points.AddRange(points2);
        
        return count;
    }

    private (int x, int y, int d) MoveNext((int x, int y, int d) currPos, Matrix<float> m)
    {

        float currVal = m[currPos.y, currPos.x];
        
        if (currPos.d == 1)
        {
            if (currVal == 1f)
            {
                return (currPos.x, currPos.y - 1, 1);
            }
            if (currVal == 4f)
            {
                return (currPos.x + 1, currPos.y, 2);
            }
            if (currVal == 5f)
            {
                return (currPos.x - 1, currPos.y, 4);
            }
        }
        if (currPos.d == 2)
        {
            if (currVal == 2f)
            {
                return (currPos.x + 1, currPos.y, 2);
            }
            if (currVal == 5f)
            {
                return (currPos.x, currPos.y + 1, 3);
            }
            if (currVal == 6f)
            {
                return (currPos.x, currPos.y - 1, 1);
            }
        }
        if (currPos.d == 3)
        {
            if (currVal == 1f)
            {
                return (currPos.x, currPos.y + 1, 3);
            }
            if (currVal == 3f)
            {
                return (currPos.x + 1, currPos.y, 2);
            }
            if (currVal == 6f)
            {
                return (currPos.x - 1, currPos.y, 4);
            }
        }
        if (currPos.d == 4)
        {
            if (currVal == 2f)
            {
                return (currPos.x - 1, currPos.y, 4);
            }
            if (currVal == 3f)
            {
                return (currPos.x, currPos.y - 1, 1);
            }
            if (currVal == 4f)
            {
                return (currPos.x, currPos.y + 1, 3);
            }
        }
        
        throw new Exception($"Invalid state currPos: {currPos}: {currVal}");
    }

    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 10 part 1");
        AnsiConsole.WriteLine("Reading file");
        Matrix<float> m = ReadMatrixChar("Data//puzzle10.txt", handleChar: HandleChar);
        _loopMatrix = new DenseMatrix(m.RowCount, m.ColumnCount);
        _loopMatrix.Clear();
        AnsiConsole.WriteLine("File read");

        AnsiConsole.WriteLine($"Number of steps {FindPath(m)}");
    }

    private int CountHorizontal(int x, int y, int direction)
    {
        int count = 0;
        bool onEdge = false;
        if (direction == -1)
        {
            while (x >= 0)
            {
                if (_loopMatrix[y, x] == 9f && !onEdge)
                {
                    onEdge = true;
                    count++;
                }
                else if(_loopMatrix[y, x] != 9f)
                {
                    onEdge = false;
                }

                x--;
            }
        }
        else
        {
            while (x < _loopMatrix.ColumnCount)
            {
                if (_loopMatrix[y, x] == 9f && !onEdge)
                {
                    onEdge = true;
                    count++;
                }
                else if(_loopMatrix[y, x] != 9f)
                {
                    onEdge = false;
                }
                x++;
            }
        }

        return count;
    }
    private int CountVertical(int x, int y, int direction)
    {
        int count = 0;
        bool onEdge = false;
        if (direction == -1)
        {
            while (y >= 0)
            {
                if (_loopMatrix[y, x] == 9f && !onEdge)
                {
                    onEdge = true;
                    count++;
                }
                else if(_loopMatrix[y, x] != 9f)
                {
                    onEdge = false;
                }

                y--;
            }
        }
        else
        {
            while (y < _loopMatrix.RowCount)
            {
                if (_loopMatrix[y, x] == 9f && !onEdge)
                {
                    onEdge = true;
                    count++;
                }
                else if(_loopMatrix[y, x] != 9f)
                {
                    onEdge = false;
                }
                
                y++;
            }
            
        }

        return count;
    }
    private void MarkInsideOutside()
    {
        // foreach (var entry in _loopMatrix.EnumerateIndexed())
        // {
        //     if (entry.Item3 == 0)
        //     {
        //         int left = CountHorizontal(entry.Item2, entry.Item1, -1);
        //         int right = CountHorizontal(entry.Item2, entry.Item1, 1);
        //         int up = CountVertical(entry.Item2, entry.Item1, -1);
        //         int down = CountVertical(entry.Item2, entry.Item1, 1);
        //
        //         // if(left % 2 == 1 && right % 2 == 1 && up % 2 == 1 && down % 2 == 1)
        //         //      _loopMatrix[entry.Item1, entry.Item2] = 2; // + up + down;
        //         _loopMatrix[entry.Item1, entry.Item2] = left;
        //     }
        // }

        byte[] types = new byte[_points.Count];
        Array.Fill(types, (byte)1);
        types[0] = 0;

        GraphicsPath path = new GraphicsPath(_points.ToArray(), types);
        System.Drawing.Region region = new System.Drawing.Region(path);
        
        foreach (var entry in _loopMatrix.EnumerateIndexed())
        {
            if (entry.Item3 == 0)
            {
                if (region.IsVisible(entry.Item2, entry.Item1))
                {
                    _loopMatrix[entry.Item1, entry.Item2] = 2;
                }
            }
        }
    }
    
    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 10 part 2");
        AnsiConsole.WriteLine("Reading file");
        Matrix<float> m = ReadMatrixChar("Data//puzzle10.txt", handleChar: HandleChar);
        AnsiConsole.WriteLine("File read");

        _loopMatrix = new DenseMatrix(m.RowCount, m.ColumnCount);
        _loopMatrix.Clear();

        FindPath(m);
        MarkInsideOutside();
        PrintMatrix(_loopMatrix);

        AnsiConsole.WriteLine(_loopMatrix.Enumerate().Count(t => t == 2));

    }
}