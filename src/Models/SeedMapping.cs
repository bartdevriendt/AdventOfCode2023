using Spectre.Console;

namespace AOC2023.Models;

public class SeedMapping(long sourceStart, long destinationStart, long range)
{
    public long SourceStart { get; set; } = sourceStart;
    public long DestinationStart { get; set; } = destinationStart;
    public long Range { get; set; } = range;

    public long DestinationEnd { get; set; } = destinationStart + range - 1;

    public long SourceEnd { get; set; } = sourceStart + range - 1;
    
    public long MapValue(long sourceValue)
    {
        if(sourceValue < SourceStart || sourceValue > SourceEnd)
            return Int64.MaxValue;
        return sourceValue - SourceStart + DestinationStart;
    }

    public override string ToString()
    {
        return $"Source: {SourceStart} - {SourceEnd} - Destination: {DestinationStart} - {DestinationEnd}";
    }
    
    public List<SeedRange> Map(SeedRange source)
    {
        var result = new List<SeedRange>();

        if(source.End < SourceStart)
        {
            result.Add(source);
            return result;
        }

        if (source.Start > SourceEnd)
        {
            result.Add(source);
            return result;
        }
        
        
        if (source.Start < SourceStart && source.End >= SourceStart)
        {
            result.Add(new SeedRange(source.Start, SourceStart - 1));
            if (source.End > SourceEnd)
            {
                result.Add(new SeedRange(DestinationStart, DestinationEnd, true));
                result.Add(new SeedRange(SourceEnd + 1, source.End));
            }
            else
            {
                result.Add(new SeedRange(DestinationStart, DestinationStart + source.End - SourceStart, true));
            }
        }
        else if (source.Start >= SourceStart && source.End <= SourceEnd)
        {
            result.Add(new SeedRange(DestinationStart + (source.Start - SourceStart), DestinationEnd - (SourceEnd - source.End), true));
        }
        if (source.Start >= SourceStart && source.End > SourceEnd)
        {
            
            result.Add(new SeedRange(SourceEnd + 1, source.End));
            result.Add(new SeedRange(DestinationEnd - (SourceEnd - source.Start), DestinationEnd, true));
        }
        
        return result;
    }

    public SeedMapping? Intersect(SeedMapping target)
    {
        var result = new SeedMapping(0, 0, 0);

        if(DestinationStart >= target.SourceStart && DestinationStart <= target.SourceEnd && DestinationEnd > target.SourceEnd)
        {
            result.SourceStart = SourceStart;
            result.DestinationStart = target.DestinationEnd - (target.SourceEnd - DestinationStart);
            result.SourceEnd = SourceStart +  target.SourceEnd - DestinationStart ;
            result.DestinationEnd = target.DestinationEnd;
        }
        else if(DestinationStart <= target.SourceStart && DestinationEnd >= target.SourceStart && DestinationEnd < target.SourceEnd)
        {
            result.SourceStart = SourceEnd - (DestinationEnd - target.SourceStart);
            result.DestinationStart = target.DestinationStart;
            result.SourceEnd = SourceEnd;
            result.DestinationEnd = DestinationEnd - target.SourceStart + target.DestinationStart;
        }
        else if(DestinationStart >= target.SourceStart && DestinationEnd <= target.SourceEnd)
        {
            result.SourceStart = SourceStart;
            result.DestinationStart = target.DestinationStart + DestinationStart - target.SourceStart;
            result.SourceEnd = SourceEnd;
            result.DestinationEnd = DestinationEnd - DestinationStart + result.DestinationStart;
        }
        else if(DestinationStart <= target.SourceStart && DestinationEnd >= target.SourceEnd)
        {
            result.SourceStart = SourceStart + target.SourceStart - DestinationStart;
            result.DestinationStart = target.DestinationStart;
            result.SourceEnd = SourceEnd - DestinationEnd + target.SourceEnd;
            result.DestinationEnd = target.DestinationEnd;
        }
        else
        {
            
            AnsiConsole.Markup($"[red]No intersection found for {this} and {target}[/]\n");
            
            return null;
        }
        
        return result;
    }
}