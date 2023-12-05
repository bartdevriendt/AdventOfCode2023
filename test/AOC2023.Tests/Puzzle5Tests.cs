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


    [Test]
    public void Test_1()
    {
        SeedMapping map = new SeedMapping(50, 90, 10);

        SeedRange sr = new SeedRange(40, 55);
        
        var result = map.Map(sr);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(40, result[0].Start);
        Assert.AreEqual(49, result[0].End);
        Assert.AreEqual(90, result[1].Start);
        Assert.AreEqual(95, result[1].End);
    }
    
    [Test]
    public void Test_2()
    {
        SeedMapping map = new SeedMapping(50, 90, 10);

        SeedRange sr = new SeedRange(40, 65);
        
        var result = map.Map(sr);
        Assert.AreEqual(3, result.Count);
        Assert.AreEqual(40, result[0].Start);
        Assert.AreEqual(49, result[0].End);
        Assert.AreEqual(90, result[1].Start);
        Assert.AreEqual(99, result[1].End);
        Assert.AreEqual(60, result[2].Start);
        Assert.AreEqual(65, result[2].End);
    }
    
    
    [Test]
    public void Test_3()
    {
        SeedMapping map = new SeedMapping(50, 90, 10);

        SeedRange sr = new SeedRange(55, 65);
        
        var result = map.Map(sr);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(95, result[1].Start);
        Assert.AreEqual(99, result[1].End);
        Assert.AreEqual(60, result[0].Start);
        Assert.AreEqual(65, result[0].End);
    }
    
    [Test]
    public void Test_4()
    {
        SeedMapping map = new SeedMapping(50, 90, 10);

        SeedRange sr = new SeedRange(50, 65);
        
        var result = map.Map(sr);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(90, result[1].Start);
        Assert.AreEqual(99, result[1].End);
        Assert.AreEqual(60, result[0].Start);
        Assert.AreEqual(65, result[0].End);
    }
    
    [Test]
    public void Test_5()
    {
        SeedMapping map = new SeedMapping(50, 90, 10);

        SeedRange sr = new SeedRange(40, 59);
        
        var result = map.Map(sr);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(90, result[1].Start);
        Assert.AreEqual(99, result[1].End);
        Assert.AreEqual(40, result[0].Start);
        Assert.AreEqual(49, result[0].End);
    }
    
    [Test]
    public void Test_6()
    {
        SeedMapping map = new SeedMapping(50, 90, 10);

        SeedRange sr = new SeedRange(40, 50);
        
        var result = map.Map(sr);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(90, result[1].Start);
        Assert.AreEqual(90, result[1].End);
        Assert.AreEqual(40, result[0].Start);
        Assert.AreEqual(49, result[0].End);
    }
    
    [Test]
    public void Test_7()
    {
        SeedMapping map = new SeedMapping(50, 90, 10);

        SeedRange sr = new SeedRange(59, 65);
        
        var result = map.Map(sr);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(99, result[1].Start);
        Assert.AreEqual(99, result[1].End);
        Assert.AreEqual(60, result[0].Start);
        Assert.AreEqual(65, result[0].End);
    }
    
    [Test]
    public void Test_8()
    {
        SeedMapping map = new SeedMapping(50, 90, 10);

        SeedRange sr = new SeedRange(53, 57);
        
        var result = map.Map(sr);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(93, result[0].Start);
        Assert.AreEqual(97, result[0].End);
    }
    
    [Test]
    public void Test_9()
    {
        SeedMapping map = new SeedMapping(50, 52, 48);

        SeedRange sr = new SeedRange(55, 67);
        
        var result = map.Map(sr);
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(57, result[0].Start);
        Assert.AreEqual(69, result[0].End);
    }
}