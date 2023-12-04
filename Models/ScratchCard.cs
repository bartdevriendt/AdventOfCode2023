namespace AOC2023.Models;

public class ScratchCard
{
    public int Number { get; set; }
    public List<int> DrawNumbers { get; set; }
    public List<int> CardNumbers { get; set; }
    public int Copies { get; set; }

    public ScratchCard(int number, List<int> drawNumbers, List<int> cardNumbers)
    {
        Number = number;
        DrawNumbers = drawNumbers;
        CardNumbers = cardNumbers;
        Copies = 1;
    }
}