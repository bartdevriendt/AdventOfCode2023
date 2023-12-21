using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle21 : PuzzleBase
{
    
    private Queue<(int r, int c, int steps) > _queue = new();
    private char[][] map;
    private Dictionary<(int r, int c), bool> visited;
    
    private bool IsVisited(int r, int c) => visited.ContainsKey((r, c));
    
    private void ReadMap()
    {
        string[] lines = File.ReadAllLines("Data//puzzle21.txt");
        map = new char[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            map[i] = new char[lines[i].Length];
            for (int j = 0; j < lines[i].Length; j++)
            {
                map[i][j] = lines[i][j];
                if (map[i][j] == 'S')
                {
                    _queue.Enqueue((i, j, 0));
                }
            }
        }
    }


    private long _cellCount = 0;

    private char GetCell(int rr, int cc)
    {
        // while(rr < 0) rr+= map.Length;
        // while(cc < 0) cc+= map[0].Length;
        // while(rr >= map.Length) rr-= map.Length;
        // while(cc >= map[0].Length) cc-= map[0].Length;
        
        int r = (rr % map.Length + map.Length) % map.Length;
        int c = (cc % map[0].Length + map[0].Length) % map[0].Length;

        return map[r][c];
    }


    private int Manhatten(int r1, int c1, int r2, int c2)
    {
        return Math.Abs(r1 - r2) + Math.Abs(c1 - c2);
    }
    
    // private void CalculateCells(int stepCount)
    // {
    //     var start = _queue.Dequeue();
    //     int mod = stepCount % 2;
    //     for(int r = start.r - stepCount; r <= start.r + stepCount; r++)
    //     {
    //         for(int c = start.c - stepCount; c <= start.c + stepCount; c++)
    //         {
    //             int d = Manhatten(r, c, start.r, start.c);
    //             if(d%2 != mod)
    //                 continue;
    //             if (GetCell(r, c) != '#' && (d % 2) == mod)
    //             {
    //                 _cellCount++;
    //             }
    //             
    //         }
    //     }
    // }
    
    private void MoveSteps(int stepCount = 64)
    {

        var start = _queue.Peek();

        int sr = start.r;
        int sc = start.c;
        int mod = stepCount % 2;
        
        while (_queue.Count > 0)
        {
            var entry = _queue.Dequeue();
            int r = entry.r;
            int c = entry.c;
            
            if(IsVisited(r,c)) continue;
            
            visited.Add((r, c), true);


            int d = Manhatten(sr, sc, r, c) % 2;
            
            if (d == mod)
            {
                _cellCount++;
            }
            if(entry.steps < stepCount)
            {
                if(GetCell(r-1,c) != '#' && !IsVisited(r-1,c))
                    _queue.Enqueue((r-1, c, entry.steps + 1));
                if(GetCell(r+1,c) != '#' && !IsVisited(r+1,c))
                    _queue.Enqueue((r+1, c, entry.steps + 1));
                if(GetCell(r,c-1) != '#' && !IsVisited(r,c-1))
                    _queue.Enqueue((r, c-1, entry.steps + 1));
                if(GetCell(r,c+1) != '#' && !IsVisited(r,c+1))
                    _queue.Enqueue((r, c+1, entry.steps + 1));
            }
        }
        
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 21 part 1");
        AnsiConsole.WriteLine("Reading file");
        visited = new();
        ReadMap();
        AnsiConsole.WriteLine("File read");
        MoveSteps(64);
        //CalculateCells(6);

        // for (int r = 0; r < map.Length; r++)
        // {
        //     for(int c = 0; c < map[0].Length; c++)
        //     {
        //         if (IsVisited(r, c))
        //         {
        //             AnsiConsole.Write("O");
        //         }
        //         else
        //         {
        //             AnsiConsole.Write(map[r][c]);
        //         }
        //     }
        //     AnsiConsole.WriteLine();
        // }
        
        
        AnsiConsole.WriteLine($"Number of cells visited: {_cellCount}");
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 21 part 2");
        AnsiConsole.WriteLine("Reading file");
        visited = new();
        ReadMap();
        AnsiConsole.WriteLine("File read");

        int steps = AnsiConsole.Ask<int>("Number of steps:");
        
        MoveSteps(steps);
        //CalculateCells(steps);

        // for (int r = 0; r < map.Length; r++)
        // {
        //     for(int c = 0; c < map[0].Length; c++)
        //     {
        //         if (IsVisited(r, c))
        //         {
        //             AnsiConsole.Write("O");
        //         }
        //         else
        //         {
        //             AnsiConsole.Write(map[r][c]);
        //         }
        //     }
        //     AnsiConsole.WriteLine();
        // }
        
        
        AnsiConsole.WriteLine($"Number of cells visited: {_cellCount}");
    }
}