using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle17 : PuzzleBase
{
    private int[][] map;
    private Dictionary<(Direction, int), int>[][] visited;
    PriorityQueue<(int y, int x, Direction dir, int movesInDirection), int> queue = new();

    private void ReadMap()
    {
        string[] lines = File.ReadAllLines("Data//puzzle17.txt");
        map = new int[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            map[i] = new int[lines[i].Length];
            for (int j = 0; j < lines[i].Length; j++)
            {
                map[i][j] = lines[i][j] - '0';
            }
        }
    }

    private int TraverseMap(int minStep, int maxStep)
    {
        visited = new Dictionary<(Direction, int), int>[map.Length][];
        for (var y = 0; y < map.Length; y++)
        {
            visited[y] = new Dictionary<(Direction, int), int>[map[0].Length];
            for (var x = 0; x < map[0].Length; x++)
                visited[y][x] = [];
        }

        queue.Enqueue((0, 0, Direction.East, 0), 0);
        queue.Enqueue((0, 0, Direction.South, 0), 0);

        while (queue.Count > 0)
        {
            var (y, x, direction, movesInDirection) = queue.Dequeue();

            var heat = visited[y][x].GetValueOrDefault((direction, movesInDirection));

            if (movesInDirection < maxStep)
                Move(y, x, direction, heat, movesInDirection, minStep, maxStep);

            if (movesInDirection >= minStep)
            {
                Move(y, x, TurnLeft(direction), heat, 0, minStep, maxStep);
                Move(y, x, TurnRight(direction), heat, 0, minStep, maxStep);
            }
        }

        var maxY = map.Length - 1;
        var maxX = map[0].Length - 1;
        
        return visited[maxY][maxX].Min(x => x.Value);
    }

    private void Move(int y, int x, Direction direction, int heat, int movesInDirection, int minStep, int maxStep)
    {
        var dy = direction switch
        {
            Direction.North => -1,
            Direction.South => 1,
            _ => 0
        };

        var dx = direction switch
        {
            Direction.East => 1,
            Direction.West => -1,
            _ => 0
        };

        for (var i = 1; i <= maxStep; i++)
        {
            var newY = y + i * dy;
            var newX = x + i * dx;
            var newMovesInDirection = movesInDirection + i;

            if (newY < 0 || newY >= map.Length || newX < 0 || newX >= map[0].Length || newMovesInDirection > maxStep)
                return;

            heat += map[newY][newX];

            if (i < minStep) continue;

            var vlist = visited[newY][newX];

            if (vlist.TryGetValue((direction, newMovesInDirection), out var visitedHeat))
            {
                if (visitedHeat <= heat)
                    return;
            }

            queue.Enqueue((newY, newX, direction, newMovesInDirection), heat);
            vlist[(direction, newMovesInDirection)] = heat;
        }
    }

    private Direction TurnLeft(Direction direction) => direction switch
    {
        Direction.North => Direction.West,
        Direction.West => Direction.South,
        Direction.South => Direction.East,
        Direction.East => Direction.North,
        _ => throw new NotSupportedException()
    };

    private Direction TurnRight(Direction direction) => direction switch
    {
        Direction.North => Direction.East,
        Direction.East => Direction.South,
        Direction.South => Direction.West,
        Direction.West => Direction.North,
        _ => throw new NotSupportedException()
    };

    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 17 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadMap();
        AnsiConsole.WriteLine("File read");
        
        AnsiConsole.WriteLine(TraverseMap(1,3));
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 17 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadMap();
        AnsiConsole.WriteLine("File read");
        
        AnsiConsole.WriteLine(TraverseMap(4,10));
    }
}