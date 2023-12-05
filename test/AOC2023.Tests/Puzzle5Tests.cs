namespace AOC2023.Tests;
using AOC2023.Models;

public class Puzzle5Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        SeedMapping map1 = new(50, 90, 10);
        SeedMapping map2 = new SeedMapping(80, 30, 12);
        
        
        var result = map1.Intersect(map2);
        
        Assert.AreEqual(50, result.SourceStart);
        Assert.AreEqual(51, result.SourceEnd);
        Assert.AreEqual(40, result.DestinationStart);
        Assert.AreEqual(41, result.DestinationEnd);

    }
    
    [Test]
    public void Test2()
    {
        SeedMapping map1 = new(50, 90, 10);
        SeedMapping map2 = new SeedMapping(97, 30, 12);
        
        
        var result = map1.Intersect(map2);
        
        Assert.AreEqual(57, result.SourceStart);
        Assert.AreEqual(59, result.SourceEnd);
        Assert.AreEqual(30, result.DestinationStart);
        Assert.AreEqual(32, result.DestinationEnd);

    }
    
    [Test]
    public void Test3()
    {
        SeedMapping map1 = new(50, 90, 10);
        SeedMapping map2 = new SeedMapping(92, 30, 5);
        
        
        var result = map1.Intersect(map2);
        
        Assert.AreEqual(52, result.SourceStart);
        Assert.AreEqual(56, result.SourceEnd);
        Assert.AreEqual(30, result.DestinationStart);
        Assert.AreEqual(34, result.DestinationEnd);

    }
    
    [Test]
    public void Test4()
    {
        SeedMapping map1 = new(50, 90, 10);
        SeedMapping map2 = new SeedMapping(80, 30, 30);
        
        
        var result = map1.Intersect(map2);
        
        Assert.AreEqual(50, result.SourceStart);
        Assert.AreEqual(59, result.SourceEnd);
        Assert.AreEqual(40, result.DestinationStart);
        Assert.AreEqual(49, result.DestinationEnd);

    }
    
    [Test]
    public void Test5()
    {
        SeedMapping map1 = new(50, 90, 10);
        SeedMapping map2 = new SeedMapping(80, 30, 20);
        
        
        var result = map1.Intersect(map2);
        
        Assert.AreEqual(50, result.SourceStart);
        Assert.AreEqual(59, result.SourceEnd);
        Assert.AreEqual(40, result.DestinationStart);
        Assert.AreEqual(49, result.DestinationEnd);

    }
}