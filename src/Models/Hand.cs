namespace AOC2023.Models;

public enum HandType
{
    FiveOfAKind = 0,
    FourOfAKind = 1,
    FullHouse = 2,
    ThreeOfAKind = 3,
    TwoPair = 4,
    OnePair = 5,
    HighCard = 6
}

public class HandComparator : Comparer<Hand>
{
    public override int Compare(Hand? x, Hand? y)
    {
        int handXType = (int)x!.HandType;
        int handYType = (int)y!.HandType;
        if (handXType != handYType)
            return handYType.CompareTo(handXType);
        
        int num = 0;
        while(x.Cards[num] == y.Cards[num] && num < 4)
        {
            num++;
        }
        
        return x.Cards[num].CompareTo(y.Cards[num]);
    }
}

public class Hand
{
    public List<int> Cards { get; set; } = new List<int>();
    public int Bid { get; set; }
    
    public HandType HandType { get; set; }

    public Hand(string hand, int bid, bool jIsJoker = false)
    {
        for(int j = 0; j < hand.Length; j++)
        {
            Cards.Add(DetermineCardValue(hand[j], jIsJoker));
        }

        Bid = bid;
        
        if(jIsJoker)
            DetermineHandTypeWithJoker();
        else
            DetermineHandType();
    }

    public void DetermineHandTypeWithJoker()
    {
        Dictionary<int, int> cardCounts = new Dictionary<int, int>();
        foreach (var card in Cards)
        {
            if (cardCounts.ContainsKey(card))
                cardCounts[card]++;
            else
                cardCounts.Add(card, 1);
        }
        
        HandType = HandType.HighCard;

        if (cardCounts.Values.Any(x => x == 5))
        {
            HandType = HandType.FiveOfAKind;

        }
        else if (cardCounts.Values.Any(x => x == 4))
        {
            HandType = HandType.FourOfAKind;

        }
        else if (cardCounts.Values.Any(x => x == 3) && cardCounts.Values.Any(x => x == 2))
        {
            HandType = HandType.FullHouse;

        }
        else if (cardCounts.Values.Any(x => x == 3))
        {
            HandType = HandType.ThreeOfAKind;

        }
        else if (cardCounts.Values.Count(x => x == 2) == 2)
        {
            HandType = HandType.TwoPair;

        }
        else if (cardCounts.Values.Any(x => x == 2))
        {
            HandType = HandType.OnePair;

        }
        
        if (cardCounts.TryGetValue(1, out var count))
        {
            if (HandType == HandType.FourOfAKind) HandType = HandType.FiveOfAKind;
            else if (HandType == HandType.FullHouse)
            {
                if (count == 1)
                    HandType = HandType.FourOfAKind;
                else
                    HandType = HandType.FiveOfAKind;
            }
            else if(HandType == HandType.ThreeOfAKind) HandType = HandType.FourOfAKind;
            else if(HandType == HandType.TwoPair)
            {
                if (count == 1)
                    HandType = HandType.FullHouse;
                else
                    HandType = HandType.FourOfAKind;
            }
            else if(HandType == HandType.OnePair) HandType = HandType.ThreeOfAKind;
            else if(HandType == HandType.HighCard) HandType = HandType.OnePair;
        }
    }
    public void DetermineHandType()
    {
        Dictionary<int, int> cardCounts = new Dictionary<int, int>();
        foreach (var card in Cards)
        {
            if (cardCounts.ContainsKey(card))
                cardCounts[card]++;
            else
                cardCounts.Add(card, 1);
        }

        if (cardCounts.Values.Any(x => x == 5))
        {
            HandType = HandType.FiveOfAKind;
            return;
        }
        if (cardCounts.Values.Any(x => x == 4))
        {
            HandType = HandType.FourOfAKind;
            return;
        }
        if (cardCounts.Values.Any(x => x == 3) && cardCounts.Values.Any(x => x == 2))
        {
            HandType = HandType.FullHouse;
            return;
        }
        if (cardCounts.Values.Any(x => x == 3))
        {
            HandType = HandType.ThreeOfAKind;
            return;
        }
        if (cardCounts.Values.Count(x => x == 2) == 2)
        {
            HandType = HandType.TwoPair;
            return;
        }
        if (cardCounts.Values.Any(x => x == 2))
        {
            HandType = HandType.OnePair;
            return;
        }
        HandType = HandType.HighCard;
        
    }
    private int DetermineCardValue(char card, bool jIsJoker = false)
    {
        switch (card)
        {
            case 'A':
                return 14;
            case 'K':
                return 13;
            case 'Q':
                return 12;
            case 'J':
                return  jIsJoker ? 1 : 11;
            case 'T':
                return 10;
            default:
                return Int32.Parse(card.ToString());
        }
    }

    public override string ToString()
    {
        return $"{Cards[0]} {Cards[1]} {Cards[2]} {Cards[3]} {Cards[4]} - {HandType}";
    }
}