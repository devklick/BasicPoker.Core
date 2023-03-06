using BasicPoker.Core.Cards;
using BasicPoker.Core.Events;
using BasicPoker.Core.Games;
using BasicPoker.Core.Players;

namespace BasicPoker.Core.Arena;

public class Table
{
    // TODO: Need to create a simplified HistoricGame object to hold onto rather than holding onto the history of whole game objects
    private readonly List<Game> _games = new();
    public Game? ActiveGame
    {
        get
        {
            var game = _games.LastOrDefault();
            return game != null && game.CurrentState == GameState.InPlay ? game : null;
        }
    }

    public bool RequiresMorePlayers => Players.Count < _minPlayers;
    public bool SupportsMorePlayers => Players.Count < _maxPlayers;
    public IReadOnlyList<Seat> Seats { get; }
    private List<Player> Players => Seats
        .Where(s => !s.IsAvailable).Select(s => s.Player!).ToList();
    private readonly Dealer _dealer;
    private readonly int _minPlayers;
    private readonly int _maxPlayers;

    private Seat FirstAvailableSeat => Seats.FirstOrDefault(s => s.IsAvailable)
        ?? throw new InvalidOperationException("No available seats at the table");

    public Table(int minPlayers = 3, int maxPlayers = 7)
        : this(Seat.CreateSequence(maxPlayers), new Dealer(), minPlayers, maxPlayers)
    { }

    public Table(List<Seat> seats, Dealer dealer, int minPlayers, int maxPlayers)
    {
        _minPlayers = minPlayers;
        _maxPlayers = maxPlayers;
        Seats = seats;
        _dealer = dealer;
    }

    public void SeatPlayer(Player player)
            => FirstAvailableSeat.SeatPlayer(player);

    public void DealNewGame()
    {
        if (ActiveGame != null)
            throw new InvalidOperationException("Cannot deal new game while another game is active");

        if (RequiresMorePlayers)
        {
            throw new InvalidOperationException(
                $"Currently {Players.Count} players at the table. Require minimum of {_minPlayers} to deal a game.");
        }

        DistributeButtons();

        var players = PlayersLeftOfDealer();
        _games.Add(new Game(players, _dealer));
    }

    private void DistributeButtons()
    {
        if (_games.Any()) RotateButtons();
        else PlaceInitialButtons();
    }

    private void PlaceInitialButtons()
    {
        GivePlayerButton(Players.FirstOrDefault(), PlayerButtonType.Dealer);
        GivePlayerButton(Players.ElementAtOrDefault(1), PlayerButtonType.SmallBlind);
        GivePlayerButton(Players.ElementAtOrDefault(2), PlayerButtonType.BigBlind);
    }

    private static void GivePlayerButton(Player? player, PlayerButtonType button)
    {
        if (player == null) throw new InvalidOperationException("Unable to find players to distribute buttons to");
        player.GiveButton(button);
    }

    private void RotateButtons()
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
        => Seats.FirstOrDefault(s => !s.IsAvailable && s.Player!.Button == button)
        ?? throw new InvalidOperationException($"No player with ${button} button");

    private void GiveButtonToNextSeat(PlayerButtonType button, int currentSeatNo)
    {
        var nextSeatNo = currentSeatNo + 1;
        if (nextSeatNo > _maxPlayers) nextSeatNo = 0;

        Seats.Where(s => !s.IsAvailable && s.SeatNumber >= nextSeatNo)
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

