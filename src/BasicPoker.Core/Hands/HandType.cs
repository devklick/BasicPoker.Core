using BasicPoker.Core.Cards;

namespace BasicPoker.Core.Hands;

public enum HandType
{
    [HandTypeMetadata(SameRankCounts = new[] { 1 })]
    HighCard,

    [HandTypeMetadata(SameRankCounts = new[] { 2 })]
    Pair,

    [HandTypeMetadata(SameRankCounts = new[] { 2, 2 })]
    TwoPair,

    [HandTypeMetadata(SameRankCounts = new[] { 3 })]
    ThreeOfAKind,

    [HandTypeMetadata(SequentialRank = true)]
    Straight,

    [HandTypeMetadata(SameSuitCount = 4)]
    Flush,

    [HandTypeMetadata(SameRankCounts = new[] { 2, 3 })]
    FullHouse,

    [HandTypeMetadata(SameRankCounts = new[] { 4 })]
    FourOfAKind,

    [HandTypeMetadata(SequentialRank = true, SameSuitCount = 5)]
    StraightFlush,

    [HandTypeMetadata(highestCard: CardRank.Ace, SequentialRank = true, SameSuitCount = 5)]
    RoyalFlush
}
