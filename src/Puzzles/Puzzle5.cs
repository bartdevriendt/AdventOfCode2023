using System.Xml.XPath;
using AOC2023.Models;
using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle5 : PuzzleBase
{

    private List<long> seedsToTest = new List<long>();
    
    private List<SeedMapping> seedToSoil = new List<SeedMapping>();
    private List<SeedMapping> soilToFert = new List<SeedMapping>();
    private List<SeedMapping> fertToWater = new List<SeedMapping>();
    private List<SeedMapping> waterToLight = new List<SeedMapping>();
    private List<SeedMapping> lightToTemp = new List<SeedMapping>();
    private List<SeedMapping> tempToHumid = new List<SeedMapping>();
    private List<SeedMapping> humidToLoc = new List<SeedMapping>();
    private List<SeedMapping> compressed = new List<SeedMapping>();
    
    
    
    private void ProcessFile(string content)
    {
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        
        List<SeedMapping>? currentList = null;
        
        foreach (var line in lines)
        {
            AnsiConsole.WriteLine("Line: " + line);
            if (line.StartsWith("seeds:"))
            {
                seedsToTest.AddRange(line.Substring(7).Split(' ').ToList().Select(x => Int64.Parse(x)));
            }
            else if (line.StartsWith("seed-to-soil map:"))
            {
                currentList = seedToSoil;
            }
            else if (line.StartsWith("soil-to-fertilizer map:"))
            {
                currentList = soilToFert;
            }
            else if (line.StartsWith("fertilizer-to-water map:"))
            {
                currentList = fertToWater;
            }
            else if (line.StartsWith("water-to-light map:"))
            {
                currentList = waterToLight;
            }
            else if (line.StartsWith("light-to-temperature map:"))
            {
                currentList = lightToTemp;
            }
            else if (line.StartsWith("temperature-to-humidity map:"))
            {
                currentList = tempToHumid;
            }
            else if (line.StartsWith("humidity-to-location map:"))
            {
                currentList = humidToLoc;
            }
            else
            {
                long[] parts = line.Split(' ').Select(x => Int64.Parse(x)).ToArray();
                currentList?.Add(new SeedMapping(parts[1], parts[0], parts[2]));
            }
        }
        
        seedToSoil = seedToSoil.OrderBy(x => x.SourceStart).ToList();
        soilToFert = soilToFert.OrderBy(x => x.SourceStart).ToList();
        fertToWater = fertToWater.OrderBy(x => x.SourceStart).ToList();
        waterToLight = waterToLight.OrderBy(x => x.SourceStart).ToList();
        lightToTemp = lightToTemp.OrderBy(x => x.SourceStart).ToList();
        tempToHumid = tempToHumid.OrderBy(x => x.SourceStart).ToList();
        humidToLoc = humidToLoc.OrderBy(x => x.SourceStart).ToList();
        
    }

    private long TestSeed(long seed, List<SeedMapping> mappings)
    {
        //AnsiConsole.WriteLine("Searching seed " + seed + " for mapping");
        // foreach(var mapping in mappings)
        // {
        //     var result = mapping.MapValue(seed);
        //     if (result != Int64.MaxValue)
        //     {
        //         //AnsiConsole.WriteLine($"Mapping found ({mapping.SourceStart} - {mapping.DestinationStart} - {mapping.Range}), so returning " + result);
        //         return result;
        //     }
        //         
        // }
        
        var mapping = mappings.FirstOrDefault(x => x.SourceStart <= seed && x.SourceEnd >= seed);
        if(mapping != null)
        {
            //AnsiConsole.WriteLine($"Mapping found ({mapping.SourceStart} - {mapping.DestinationStart} - {mapping.Range}), so returning " + mapping.MapValue(seed));
            return mapping.MapValue(seed);
        }
        //AnsiConsole.WriteLine("Mapping not found, so returning seed " + seed);

        return seed;

    }
    
    private void SearchLowestLocation()
    {
        long lowest = Int64.MaxValue;
        
        foreach (var seed in seedsToTest)
        {
            //AnsiConsole.WriteLine("Testing seed " + seed + " for lowest location");
            var result = TestSeed(seed, seedToSoil);
            result = TestSeed(result, soilToFert);
            result = TestSeed(result, fertToWater);
            result = TestSeed(result, waterToLight);
            result = TestSeed(result, lightToTemp);
            result = TestSeed(result, tempToHumid);
            result = TestSeed(result, humidToLoc);
            if(result<lowest)
                lowest = result;
        }
        
        AnsiConsole.WriteLine("Lowest location is " + lowest);
    }
    
    
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 5 part 1");
        AnsiConsole.WriteLine("Reading file");
        var content = ReadFullFile("Data//puzzle5.txt");
        AnsiConsole.WriteLine("File read");
        ProcessFile(content);
        SearchLowestLocation();
        
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 5 part 1");
        AnsiConsole.WriteLine("Reading file");
        var content = ReadFullFile("Data//puzzle5.txt");
        AnsiConsole.WriteLine("File read");
        ProcessFile(content);
        //AggregateMappings();
        SearchLowestLocationPart2();
    }
    
    // private void AggregateMappings()
    // {
    //     var result = new List<SeedMapping>(seedToSoil);
    //     
    //
    //     List<List<SeedMapping>> lists = new List<List<SeedMapping>>()
    //     {
    //         soilToFert, fertToWater, waterToLight, lightToTemp, tempToHumid, humidToLoc
    //     };
    //
    //     foreach (var list in lists)
    //     {
    //         var currentList = new List<SeedMapping>();
    //         foreach (var mapping in result)
    //         {
    //             foreach (var mapping2 in list)
    //             {
    //                 var intersect = mapping.Intersect(mapping2);
    //                 if(intersect!= null)
    //                     currentList.Add(intersect);
    //             }    
    //         }
    //
    //         result = currentList;
    //     }
    //
    //     foreach (var maping in result)
    //     {
    //         if(maping.SourceEnd - maping.SourceStart != maping.DestinationEnd - maping.DestinationStart)
    //             throw new Exception("Mapping is not 1:1");
    //     }
    //     
    //     compressed = result.OrderBy(x => x.SourceStart).ToList();
    // }
    
    private void SearchLowestLocationPart2()
    {
        long lowest = Int64.MaxValue;

        List<SeedRange> ranges = new List<SeedRange>();

        for (var index = 0; index < seedsToTest.Count; index += 2)
        {
            
            var seedStart = seedsToTest[index];
            var seedEnd = seedsToTest[index + 1];
            
            ranges.Add(new SeedRange(seedStart, seedStart + seedEnd - 1));
        }
        
        List<List<SeedMapping>> lists = new List<List<SeedMapping>>()
        {
           seedToSoil, soilToFert, fertToWater, waterToLight, lightToTemp, tempToHumid, humidToLoc
        };


        
        
        foreach (var list in lists)
        {
            List<SeedRange> newRanges = new List<SeedRange>();
            foreach (var range in ranges)
            {
                range.Mapped = false;
            }
            foreach (var mapping in list)
            {
                foreach (var range in ranges)
                {
                    if (range.Mapped)
                    {
                        newRanges.Add(range);
                        continue;
                    }
                       
                    var result = mapping.Map(range);
                    newRanges.AddRange(result);
                }

                ranges = new List<SeedRange>(newRanges);
                newRanges.Clear();
            }
        }
        
        var sortedresult = ranges.OrderBy(x => x.Start).ToList();
        
        AnsiConsole.WriteLine("Lowest location is " + sortedresult[0].Start);

        // var result = TestSeed(seedStart, compressed);
            // if (result < lowest)
            //     lowest = result;
            
            // AnsiConsole.Progress().Start((ctx) =>
            // {
            //
            //     var task = ctx.AddTask("Searching seeds", new ProgressTaskSettings
            //     {
            //         AutoStart = false,
            //         MaxValue = seedEnd
            //     });
            //     
            //     for (long seed = seedStart; seed <= seedStart + seedEnd; seed++)
            //     {
            //         
            //         task.Increment(1);
            //         //if((seed % 1000) == 0) AnsiConsole.WriteLine("Another 1000");
            //         
            //         //AnsiConsole.WriteLine("Testing seed " + seed + " for lowest location");
            //         var result = TestSeed(seed, compressed);
            //         // result = TestSeed(result, soilToFert);
            //         // result = TestSeed(result, fertToWater);
            //         // result = TestSeed(result, waterToLight);
            //         // result = TestSeed(result, lightToTemp);
            //         // result = TestSeed(result, tempToHumid);
            //         // result = TestSeed(result, humidToLoc);
            //         if (result < lowest)
            //             lowest = result;
            //
            //     }
            // });
        //}
        //AnsiConsole.WriteLine("Lowest location is " + lowest);
    
        
    }
}