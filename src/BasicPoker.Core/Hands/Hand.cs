using BasicPoker.Core.Cards;

namespace BasicPoker.Core.Hands;

public class Hand
{
    public List<CardDetails> Cards { get; }
    public HandType HandType { get; }

    public Hand(List<CardDetails> cards, HandType handType)
    {
        Cards = cards;
        HandType = handType;
    }
}
