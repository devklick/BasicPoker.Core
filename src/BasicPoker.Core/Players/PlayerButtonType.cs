namespace BasicPoker.Core.Players;

public enum PlayerButtonType
{
    [PlayerButtonTypeMetadata("", 0)]
    None,

    [PlayerButtonTypeMetadata("SB", 0.5)]
    SmallBlind,

    [PlayerButtonTypeMetadata("BB", 1)]
    BigBlind,

    [PlayerButtonTypeMetadata("D", 0)]
    Dealer
}