using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

using BasicPoker.Core.Cards;
using BasicPoker.Core.Players;

namespace BasicPoker.Core.Arena;

public class Table
{
    public bool RequiresMorePlayers => Players.Count < _minPlayers;
    public bool SupportsMorePlayers => Players.Count < _maxPlayers;
    private readonly List<Seat> _seats;
    private List<Player> Players => _seats
        .Where(s => !s.IsAvailable).Select(s => s.Player!).ToList();
    private readonly Dealer _dealer;
    private readonly int _minPlayers;
    private readonly int _maxPlayers;
    private readonly CardCollection _communityCards;
    private GameRound _round = GameRound.WaitingForDeal;
    private Seat FirstAvailableSeat => _seats.FirstOrDefault(s => s.IsAvailable)
        ?? throw new InvalidOperationException("No available seats at the table");

    public Table(List<Seat> seats, Dealer dealer, CardCollection communityCards)
    {
        _seats = seats;
        _dealer = dealer;
        _communityCards = communityCards;
    }

    public Table(int minPlayers = 3, int maxPlayers = 7)
    {
        _minPlayers = minPlayers;
        _maxPlayers = maxPlayers;
        _dealer = new Dealer();
        _communityCards = new CardCollection(5);
        _seats = Seat.CreateSequence(_maxPlayers);
    }

    public void DealNewGame()
    {
        var players = PlayersLeftOfDealer();
        _dealer.DealTo(players);
        _round = GameRound.PreFlop;
    }

    public void NextRound()
    {
        if (_round == GameRound.WaitingForDeal)
        {
            throw new InvalidOperationException("No active game. Deal new game before progressing to next round.");
        }

        var nextRound = (GameRound)(((int)_round) + 1);

        if (!Enum.IsDefined(nextRound))
        {
            nextRound = GameRound.WaitingForDeal;
        }

        _round = nextRound;
    }

    public void SeatPlayer(Player player)
        => FirstAvailableSeat.SeatPlayer(player);

    public void RotateButtons()
    {
        var smallBlind = FindPlayerSeatWithButton(PlayerButtonType.SmallBlind);
        var bigBlind = FindPlayerSeatWithButton(PlayerButtonType.BigBlind);
        var dealer = FindPlayerSeatWithButton(PlayerButtonType.Dealer);

        smallBlind.Player!.TakeButton();
        bigBlind.Player!.TakeButton();
        dealer.Player!.TakeButton();

        GiveButtonToNextSeat(smallBlind.Player.Button, smallBlind.SeatNumber);
        GiveButtonToNextSeat(bigBlind.Player.Button, bigBlind.SeatNumber);
        GiveButtonToNextSeat(dealer.Player.Button, dealer.SeatNumber);
    }

    private Seat FindPlayerSeatWithButton(PlayerButtonType button)
        => _seats.FirstOrDefault(s => !s.IsAvailable && s.Player!.Button == button)
        ?? throw new InvalidOperationException($"No player with ${button} button");

    private void GiveButtonToNextSeat(PlayerButtonType button, int currentSeatNo)
    {
        var nextSeatNo = currentSeatNo + 1;
        if (nextSeatNo > _maxPlayers) nextSeatNo = 0;

        _seats.Where(s => !s.IsAvailable && s.SeatNumber >= nextSeatNo)
            .OrderBy(s => s.SeatNumber)
            .First().Player!.GiveButton(button);
    }

    private List<Player> PlayersLeftOfDealer()
    {
        var mutablePlayers = new List<Player>(Players);
        var sbIndex = mutablePlayers.FindIndex(p => p.Button == PlayerButtonType.SmallBlind);
        if (sbIndex < 0) throw new InvalidOperationException("No player with small blind. Unable to determine player to start dealing cards to");

        var afterSB = mutablePlayers.GetRange(0, sbIndex);
        mutablePlayers.RemoveRange(0, sbIndex);
        mutablePlayers.AddRange(afterSB);
        return mutablePlayers;
    }
}

