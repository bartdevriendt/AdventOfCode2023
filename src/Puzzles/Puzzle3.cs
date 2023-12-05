using MathNet.Numerics.LinearAlgebra;
using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle3 : PuzzleBase
{
    private List<int> validNumbers = new List<int>();
    private float HandleChar(char c)
    {
        if (Char.IsDigit(c))
            return Convert.ToInt32(c.ToString());
        if(c == '.')
            return -1;
        return 999;
    }
    
    private float HandleCharPart2(char c)
    {
        if (Char.IsDigit(c))
            return Convert.ToInt32(c.ToString());
        if(c == '*')
            return 10;
        return 999;
    }
    
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 3 part 1");
        AnsiConsole.WriteLine("Reading file");
        Matrix<float> m = ReadMatrixChar("Data//puzzle3.txt", handleChar: HandleChar);
        AnsiConsole.WriteLine("File read");
        
        int currentNumber = 0;
        bool validNumber = false;
        
        for(int r=0; r < m.RowCount; r++)
        {
            currentNumber = 0;
            validNumber = false;
            for(int c=0; c < m.ColumnCount; c++)
            {
                if (m[r, c] == -1 || m[r, c] == 999)
                {
                    if (currentNumber > 0 && validNumber)
                    {
                        AnsiConsole.WriteLine("Found number " + currentNumber);
                        validNumbers.Add(currentNumber);
                    }

                    currentNumber = 0;
                    validNumber = false;
                }
                if(m[r, c]>=0 && m[r, c] <= 9)
                {
                    currentNumber = currentNumber * 10 + (int)m[r, c];
                    for(int i = -1; i < 2; i++)
                    {
                        for(int j = -1; j < 2; j++)
                        {
                            if (i == 0 && j == 0)
                                continue;
                            if (r + i < 0 || c + j < 0 || r + i >= m.RowCount || c + j >= m.ColumnCount)
                                continue;
                            if (m[r + i, c + j] == 999)
                                validNumber = true;
                        }
                    }
                }

               
            }
        }
        
        AnsiConsole.WriteLine("Sum is " + validNumbers.Sum());
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 3 part 2");
        AnsiConsole.WriteLine("Reading file");
        Matrix<float> m = ReadMatrixChar("Data//puzzle3.txt", handleChar: HandleCharPart2);
        AnsiConsole.WriteLine("File read");
        
        int currentNumber = 0;
        bool validNumber = false;

        (int, int) gearPos = (0,0);
        
        Dictionary<(int, int), List<int>> gearNumbers = new Dictionary<(int, int), List<int>>();
        
        for(int r=0; r < m.RowCount; r++)
        {
            currentNumber = 0;
            validNumber = false;
            for(int c=0; c < m.ColumnCount; c++)
            {
                if (m[r, c] == 999 || m[r,c] == 10)
                {
                    if (currentNumber > 0 && validNumber)
                    {
                        AnsiConsole.WriteLine("Found number " + currentNumber);
                        if (!gearNumbers.ContainsKey(gearPos))
                            gearNumbers[gearPos] = new List<int>();
                        gearNumbers[gearPos].Add(currentNumber);
                    }

                    currentNumber = 0;
                    validNumber = false;
                }
                if(m[r, c]>=0 && m[r, c] <= 9)
                {
                    currentNumber = currentNumber * 10 + (int)m[r, c];
                    for(int i = -1; i < 2; i++)
                    {
                        for(int j = -1; j < 2; j++)
                        {
                            if (i == 0 && j == 0)
                                continue;
                            if (r + i < 0 || c + j < 0 || r + i >= m.RowCount || c + j >= m.ColumnCount)
                                continue;
                            if (m[r + i, c + j] == 10)
                            {
                                validNumber = true;
                                gearPos = (r + i, c + j);
                            }
                        }
                    }
                }

               
            }
        }

        int sum = 0;
        foreach (var gearPosition in gearNumbers.Keys)
        {
            if (gearNumbers[gearPosition].Count == 2)
            {
                sum += gearNumbers[gearPosition][0] * gearNumbers[gearPosition][1];
            }
        }
        
        AnsiConsole.WriteLine("Sum is " + sum);
    }
}