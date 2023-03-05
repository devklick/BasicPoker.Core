namespace BasicPoker.Core.Players;

public enum PlayerButtonType
{
    [PlayerButtonTypeMetadata("", 0)]
    None,

    [PlayerButtonTypeMetadata("SB", 1)]
    SmallBlind,

    [PlayerButtonTypeMetadata("BB", 2)]
    BigBlind,

    [PlayerButtonTypeMetadata("D", 0)]
    Dealer
}