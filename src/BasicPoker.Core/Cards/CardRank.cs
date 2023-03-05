namespace BasicPoker.Core.Cards;

public enum CardRank
{
    [CardRankMetadata("2", 2)]
    Two,

    [CardRankMetadata("3", 3)]
    Three,

    [CardRankMetadata("4", 4)]
    Four,

    [CardRankMetadata("5", 5)]
    Five,

    [CardRankMetadata("6", 6)]
    Six,

    [CardRankMetadata("7", 7)]
    Seven,

    [CardRankMetadata("8", 8)]
    Eight,

    [CardRankMetadata("9", 9)]
    Nine,

    [CardRankMetadata("10", 10)]
    Ten,

    [CardRankMetadata("J", 10)]
    Jack,

    [CardRankMetadata("Q", 11)]
    Queen,

    [CardRankMetadata("K", 12)]
    King,

    [CardRankMetadata("A", 1, 13)]
    Ace,
}
