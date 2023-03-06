using BasicPoker.Core.Players;

namespace BasicPoker.Core.Games;

public class BettingRound
{
    public BettingRoundType RoundType { get; }
    public List<(Player Player, PlayerAction Action)> PlayerActivity { get; } = new();
    public double Pot => PlayerActivity.Sum(a => a.Action.Bet);

    public BettingRound(BettingRoundType roundType)
    {
        RoundType = roundType;
    }
}
