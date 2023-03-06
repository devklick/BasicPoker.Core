namespace BasicPoker.Core.Games;

public class PlayerAction
{
    public PlayerActionType PlayerActionType { get; }
    public double Bet { get; set; }

    public PlayerAction(PlayerActionType playerActionType, double bet)
    {
        PlayerActionType = playerActionType;
        Bet = bet;
    }
}