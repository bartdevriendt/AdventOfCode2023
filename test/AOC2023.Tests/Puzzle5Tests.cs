namespace AOC2023.Tests;
using AOC2023.Models;

public class Puzzle5Tests
{
    [SetUp]
    public void Setup()
    {
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