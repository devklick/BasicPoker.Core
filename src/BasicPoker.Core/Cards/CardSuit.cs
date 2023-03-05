namespace BasicPoker.Core.Cards;

public enum CardSuitColor
{
    Red,
    Black
}

public enum CardSuit
{
    [CardSuitMetadata("♥", CardSuitColor.Red)]
    Hearts,

    [CardSuitMetadata("◆", CardSuitColor.Red)]
    Diamonds,

    [CardSuitMetadata("♣", CardSuitColor.Black)]
    Clubs,

    [CardSuitMetadata("♠", CardSuitColor.Black)]
    Spades
}
