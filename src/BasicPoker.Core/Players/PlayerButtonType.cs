namespace BasicPoker.Core.Players;

public enum PlayerButtonType
{
    [PlayerButtonTypeMetadata("")]
    None,

    [PlayerButtonTypeMetadata("SB", 0.5)]
    SmallBlind,

    [PlayerButtonTypeMetadata("BB", 1)]
    BigBlind,

    [PlayerButtonTypeMetadata("D")]
    Dealer
}