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
}