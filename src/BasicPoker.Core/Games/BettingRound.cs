using BasicPoker.Core.Players;

namespace BasicPoker.Core.Games;

public class BettingRound
{
    public BettingRoundType RoundType { get; }
    private readonly List<(Player Player, PlayerAction Action)> _playerActivity = new();
    public IReadOnlyList<(Player Player, PlayerAction Action)> PlayerActivity => _playerActivity.AsReadOnly();
    public double Pot => PlayerActivity.Sum(a => a.Action.Bet);
    public double Price { get; private set; }
    public double AmountToCall { get; private set; }
    private readonly object _playerActivityLock = new();

    public BettingRound(BettingRoundType roundType)
    {
        RoundType = roundType;
    }

    public void AddPlayerAction(Player player, PlayerAction playerAction)
    {
        switch (playerAction.PlayerActionType)
        {
            case PlayerActionType.Blind:
                Price = Math.Max(Price, playerAction.Bet);
                break;
            case PlayerActionType.Raise:
            case PlayerActionType.AllIn:
                Price = playerAction.Bet;
                break;
        }

        lock (_playerActivityLock)
        {
            _playerActivity.Add((player, playerAction));
        }
    }

    public bool CanPlayerCheck(Player player)
    {
        lock (_playerActivityLock)
        {
            var totalPlayerBets = _playerActivity.Where(a => a.Player == player).Sum(x => x.Action.Bet);
            return totalPlayerBets >= Price;
        }
    }
}
