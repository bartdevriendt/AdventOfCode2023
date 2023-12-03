using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle1 : PuzzleBase
{

    private List<string> numbers = new List<string>();
    private void StripNumber(string line)
    {
        string number = "";
        
        for(int j= 0; j < line.Length; j++)
        {
            if (Char.IsDigit(line[j]))
            {
                number += line[j];
            }
        }
        AnsiConsole.WriteLine(line + " -> " + number + " -> " + number[0].ToString() + number[^1].ToString()    );
        
        numbers.Add(number[0].ToString() + number[^1].ToString());
        
    }

    private void ReplaceNumber(string line)
    {
        string[] words = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        string[] wordNumbers = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string newLine = line;
        for(int j = 0; j< newLine.Length; j++)
        {
            for (int i = 0; i < words.Length; i++)
            {
                if(newLine.Length < j + words[i].Length)
                    continue;
                if(newLine.Substring(j, words[i].Length) == words[i])
                    newLine = newLine.Substring(0, j) + wordNumbers[i] + newLine.Substring(j + words[i].Length);
            }
        }
        AnsiConsole.WriteLine(line + " -> " + newLine);
        
        StripNumber(newLine);
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 1 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle1.txt", StripNumber);
        AnsiConsole.WriteLine("File read");
        int sum = 0;
        foreach (string number in numbers)
        {
            sum += Int32.Parse(number);
        }
        AnsiConsole.WriteLine("Sum is " + sum);
    }
    
    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 1 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle1.txt", ReplaceNumber);
        AnsiConsole.WriteLine("File read");
        int sum = 0;
        foreach (string number in numbers)
        {
            sum += Int32.Parse(number);
        }
        AnsiConsole.WriteLine("Sum is " + sum);
    }
}