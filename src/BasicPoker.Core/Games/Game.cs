using BasicPoker.Core.Cards;
using BasicPoker.Core.Events;
using BasicPoker.Core.Helpers;
using BasicPoker.Core.Players;

namespace BasicPoker.Core.Games;

public class Game
{
    public GameState CurrentState { get; private set; } = GameState.Setup;
    public double Pot => Rounds.Sum(r => r.Value.Pot);
    public Dealer Dealer { get; }
    public CardCollection CommunityCards { get; }
    public PlayerCollection Players { get; }
    public BettingRound CurrentBettingRound => Rounds.Last().Value;
    public Dictionary<BettingRoundType, BettingRound> Rounds = new();
    private readonly double _minimumBet;

    public event GameRoundUpdatedEventHandler? GameRoundChanged;

    public Game(List<Player> players, Dealer dealer, double minimumBet) : this(players, dealer, new CardCollection(5), minimumBet)
    { }

    public Game(List<Player> players, Dealer dealer, CardCollection communityCards, double minimumBet)
    {
        Dealer = dealer;
        Dealer.DealTo(players);
        CommunityCards = communityCards;
        Rounds.Add(BettingRoundType.PreFlop, new BettingRound(BettingRoundType.PreFlop));
        players.ForEach(player =>
        {
            communityCards.CardReceived += (s, e) => player.CalculateHandProbabilities(e.AllCards);
            communityCards.AllCardsRemoved += (s, e) => player.CalculateHandProbabilities(e.Cards);
        });
        Players = new(players);
        CurrentState = GameState.InPlay;
        _minimumBet = minimumBet;
        DeductBlindBets();
    }

    private void DeductBlindBets()
    {
        var bigBlindData = PlayerButtonHelper.GetMetadata(PlayerButtonType.BigBlind);
        var bigBlindBet = _minimumBet * bigBlindData.MinimumBetMultiplier;
        Players.BigBlind.DeductFunds(bigBlindBet);
        CurrentBettingRound.AddPlayerAction(Players.BigBlind, PlayerAction.Blind(bigBlindBet));

        var smallBlindData = PlayerButtonHelper.GetMetadata(PlayerButtonType.SmallBlind);
        var smallBlindBet = _minimumBet * smallBlindData.MinimumBetMultiplier;
        Players.SmallBlind.DeductFunds(smallBlindBet);
        CurrentBettingRound.AddPlayerAction(Players.SmallBlind, PlayerAction.Blind(smallBlindBet));
    }

    public void NextRound()
    {
        if (CurrentState != GameState.InPlay)
            throw new InvalidOperationException("Game not active. No next round to progress to. Deal a new game");

        var nextRound = (BettingRoundType)(((int)CurrentBettingRound.RoundType) + 1);

        if (!Enum.IsDefined(nextRound))
        {
            throw new InvalidOperationException("No next round to progress to. Deal a new game");
        }

        var oldValue = CurrentBettingRound.RoundType;

        Rounds.Add(nextRound, new BettingRound(nextRound));

        OnGameRoundUpdated(oldValue, nextRound);
    }

    protected virtual void OnGameRoundUpdated(BettingRoundType oldValue, BettingRoundType newValue)
        => GameRoundChanged?.Invoke(this, new GameRoundUpdatedEventArgs(oldValue, newValue));

    public void Fold(Player player, PlayerAction action)
    {
        CurrentBettingRound.AddPlayerAction(player, action);
        Dealer.Burn(player.TakeCards().ToArray());
    }

    public void Check(Player player, PlayerAction playerAction)
    {
        if (!CurrentBettingRound.CanPlayerCheck(player))
        {
            throw new InvalidOperationException("Player cannot check");
        }

        CurrentBettingRound.AddPlayerAction(player, playerAction);
    }

    public void Call(Player player, PlayerAction playerAction)
    {
        ThrowIfInsufficientFunds(player, playerAction.Bet);
        player.DeductFunds(CurrentBettingRound.AmountToCall);
        CurrentBettingRound.AddPlayerAction(player, playerAction);
    }

    public void Raise(Player player, PlayerAction playerAction)
    {
        ThrowIfInsufficientFunds(player, playerAction.Bet);
        player.DeductFunds(playerAction.Bet);
        CurrentBettingRound.AddPlayerAction(player, playerAction);
    }

    public void AllIn(Player player, PlayerAction playerAction)
    {
        ThrowIfInsufficientFunds(player, playerAction.Bet);
        player.DeductFunds(player.Stack);
        CurrentBettingRound.AddPlayerAction(player, playerAction);
    }

    private static void ThrowIfInsufficientFunds(Player player, double amount)
    {
        if (player.Stack < amount)
            throw new InvalidOperationException("Player does not have enough funds to call");
    }
}