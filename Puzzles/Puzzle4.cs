using AOC2023.Models;
using Spectre.Console;

namespace AOC2023.Puzzles;

public class Puzzle4 : PuzzleBase
{
    private List<int> cardPoints = new();
    private void ProcessCard(string line)
    {
        AnsiConsole.Console.WriteLine("Processing card " + line);
        string[] parts = line.Substring(10).Split('|');
        List<int> cardNumbers = new List<int>(parts[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => Int32.Parse(x)));
        List<int> drawNumbers = new List<int>(parts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => Int32.Parse(x)));
        List<int> result = drawNumbers.FindAll(x => cardNumbers.Contains(x));
        
        cardPoints.Add((int)Math.Pow(2, result.Count - 1));

    }
    public override void Part1()
    {
        AnsiConsole.WriteLine("Puzzle 4 part 1");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle4.txt", ProcessCard);
        AnsiConsole.WriteLine("File read");
        AnsiConsole.WriteLine("Sum is " + cardPoints.Sum());
    }

    public override void Part2()
    {
        AnsiConsole.WriteLine("Puzzle 4 part 2");
        AnsiConsole.WriteLine("Reading file");
        ReadFileLineByLine("Data//puzzle4.txt", ProcessCardPart2);
        AnsiConsole.WriteLine("File read");
        CountCopies();
        AnsiConsole.WriteLine("Total scratch cards: " + scratchCards.Sum(x => x.Value.Copies));
    }
    
    private Dictionary<int, ScratchCard> scratchCards = new();

    private void CountCopies()
    {
        foreach(var cardNumber in scratchCards.Keys.OrderBy(x => x))
        {
            var card = scratchCards[cardNumber];
            List<int> result = card.DrawNumbers.FindAll(x => card.CardNumbers.Contains(x));
            int count = result.Count;
            for (int j = cardNumber + 1; j < cardNumber + count + 1; j++)
            {
                scratchCards[j].Copies += card.Copies;
            }
            AnsiConsole.WriteLine("Card " + cardNumber + " has " + card.Copies + " copies");
        }
    }
    
    private void ProcessCardPart2(string line)
    {
        AnsiConsole.Console.WriteLine("Processing card " + line);
        int CardId = Int32.Parse(line.Substring(5, 3).Trim());
        string[] parts = line.Substring(10).Split('|');
        List<int> cardNumbers = new List<int>(parts[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => Int32.Parse(x)));
        List<int> drawNumbers = new List<int>(parts[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList().Select(x => Int32.Parse(x)));
        scratchCards[CardId] = new ScratchCard(CardId, drawNumbers, cardNumbers);
    }
}