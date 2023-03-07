using BasicPoker.Core.Cards;
using BasicPoker.Core.Events;
using BasicPoker.Core.Games;
using BasicPoker.Core.Players;

namespace BasicPoker.Core;

public class Game
{
    public GameState CurrentState { get; private set; } = GameState.Initializing;
    public double Pot => Rounds.Sum(r => r.Value.Pot);
    public BettingRoundType CurrentBettingRound => Rounds.Keys.Last();
    public Dictionary<BettingRoundType, BettingRound> Rounds = new();
    public List<(BettingRoundType RoundType, Player Player, PlayerAction Action)> PlayerActivity
        => Rounds.SelectMany(r => r.Value.PlayerActivity.Select(a => (r.Key, a.Player, a.Action))).ToList();
    private readonly List<(Player Player, bool Out)> _players;
    private readonly CardCollection _communityCards;
    private readonly Dealer _dealer;

    public event GameRoundUpdatedEventHandler? GameRoundChanged;

    public Game(List<Player> players, Dealer dealer) : this(players, dealer, new CardCollection(5))
    { }

    public Game(List<Player> players, Dealer dealer, CardCollection communityCards)
    {
        _players = new List<(Player Player, bool Out)>(players.Select(p => (p, false)));
        _dealer = dealer;
        _dealer.DealTo(players);
        _communityCards = communityCards;
        Rounds.Add(BettingRoundType.PreFlop, new BettingRound(BettingRoundType.PreFlop));
    }

    public void NextRound()
    {
        if (CurrentState != GameState.InPlay)
            throw new InvalidOperationException("Game not active. No next round to progress to. Deal a new game");

        var nextRound = (BettingRoundType)(((int)CurrentBettingRound) + 1);

        if (!Enum.IsDefined(nextRound))
        {
            throw new InvalidOperationException("No next round to progress to. Deal a new game");
        }

        var oldValue = CurrentBettingRound;

        Rounds.Add(nextRound, new BettingRound(nextRound));

        OnGameRoundUpdated(oldValue, nextRound);
    }

    protected virtual void OnGameRoundUpdated(BettingRoundType oldValue, BettingRoundType newValue)
        => GameRoundChanged?.Invoke(this, new GameRoundUpdatedEventArgs(oldValue, newValue));
}