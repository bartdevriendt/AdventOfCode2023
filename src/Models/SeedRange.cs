namespace AOC2023.Models;

public class SeedRange(long start, long end, bool mapped = false)
{
    public long Start { get; set; } = start;
    public long End { get; set; } = end;
    
    public bool Mapped { get; set; } = mapped;
}