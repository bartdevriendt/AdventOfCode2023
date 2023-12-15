using Spectre.Console;

namespace AOC2023.Puzzles;


public class Lens
{
    public string Label { get; set; }
    public int FocalLength { get; set; }
}

public class Puzzle15: PuzzleBase
{

    public int CalcHash(string word)
    {
        int h = 0;
        foreach (var c in word.AsSpan())
        {
            h += (int)c;
            h *= 17;
            h = h % 256;
        }

        return h;
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 15 part 1");
        AnsiConsole.WriteLine("Reading file");
        var contents = ReadFullFile("Data//puzzle15.txt").Replace("\n", "").Split(',');
        AnsiConsole.WriteLine("File read");

        long totalHash = 0;
        foreach (var c in contents)
        {
            totalHash += CalcHash(c);
        }
        
        AnsiConsole.WriteLine($"Total hash: {totalHash}");
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 15 part 2");
        AnsiConsole.WriteLine("Reading file");
        var contents = ReadFullFile("Data//puzzle15.txt").Replace("\n", "").Split(',');
        AnsiConsole.WriteLine("File read");

        List<List<Lens>> lenses = new List<List<Lens>>();
        for(int j = 0; j < 256; j++)
        {
            lenses.Add(new List<Lens>());
        }
        
        foreach (var c in contents)
        {
            int idx = c.IndexOfAny(new[] { '=', '-' });
            string label = c[0..idx];
            int box = CalcHash(label);
            
            if (c[idx] == '=')
            {
                int focalLength = Int32.Parse(c[(idx+1)..c.Length]);
                var lens = lenses[box].Find(x => x.Label == label);
                if(lens == null)
                    lenses[box].Add(new Lens {Label = label, FocalLength = focalLength});
                else
                    lens.FocalLength = focalLength;
                
            }
            else if (c[idx] == '-')
            {
                var lens = lenses[box].Find(x => x.Label == label);
                if (lens != null)
                    lenses[box].Remove(lens);
            }
            
            //AnsiConsole.WriteLine($"{label} = {box}");
        }
        
        
        long total = 0;
        for (int i = 0; i < 256; i++)
        {
            int slot = 1;
            foreach (var lens in lenses[i])
            {
                total += (i+1) * slot * lens.FocalLength;
                slot++;
            }
        }
        
        AnsiConsole.WriteLine($"Total focusing power: {total}");
        
    }
}