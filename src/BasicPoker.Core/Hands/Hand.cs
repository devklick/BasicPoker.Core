using BasicPoker.Core.Cards;

namespace BasicPoker.Core.Hands;

public class Hand
{
    public IReadOnlyList<Card> Cards { get; }
    public HandType HandType { get; }

    public Hand(List<Card> cards, HandType handType)
    {
        Cards = cards;
        HandType = handType;
    }
}
