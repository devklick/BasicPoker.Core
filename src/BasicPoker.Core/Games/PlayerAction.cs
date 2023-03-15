namespace BasicPoker.Core.Games;

public class PlayerAction
{
    public PlayerActionType PlayerActionType { get; }
    public double Bet { get; }

    public PlayerAction(PlayerActionType playerActionType, double bet = 0)
    {
        PlayerActionType = playerActionType;
        Bet = bet;
    }

    public static PlayerAction Check() => new(PlayerActionType.Check);
    public static PlayerAction Fold() => new(PlayerActionType.Fold);
    public static PlayerAction Call() => new(PlayerActionType.Fold);
    public static PlayerAction Raise(double bet) => new(PlayerActionType.Raise, bet);
    public static PlayerAction AllIn(double bet) => new(PlayerActionType.AllIn, bet);
    public static PlayerAction Blind(double bet) => new(PlayerActionType.Blind, bet);
}