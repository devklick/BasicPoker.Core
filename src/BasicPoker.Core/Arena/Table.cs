using BasicPoker.Core.Games;
using BasicPoker.Core.Players;

namespace BasicPoker.Core.Arena;

public class Table
{
    public Game? ActiveGame => GetActiveGame();
    public bool HasActiveGame => ActiveGame != null;
    public bool RequiresMorePlayers => Players.Count < _minPlayers;
    public bool SupportsMorePlayers => Players.Count < _maxPlayers;
    public IReadOnlyList<Seat> Seats { get; }
    private readonly List<Game> _games = new();
    private List<Player> Players => Seats.Where(s => s.IsTaken).Select(s => s.Player!).ToList();
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

        if (!ButtonsAlreadyDistributed())
        {
            PlaceInitialButtons();
        }

        var players = PlayersLeftOfButton(PlayerButtonType.BigBlind);
        _games.Add(new Game(players, _dealer, 4));
    }

    private bool ButtonsAlreadyDistributed()
    {
        var allPresent = FindPlayerSeatWithButtonOrDefault(PlayerButtonType.Dealer) != null
            && FindPlayerSeatWithButtonOrDefault(PlayerButtonType.BigBlind) != null
            && FindPlayerSeatWithButtonOrDefault(PlayerButtonType.SmallBlind) != null;

        return allPresent;
    }

    private void PlaceInitialButtons()
    {
        Players.ForEach(p => p.TakeButton());

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

    private Seat? FindPlayerSeatWithButtonOrDefault(PlayerButtonType button)
        => Seats.FirstOrDefault(s => !s.IsAvailable && s.Player!.Button == button);

    private Seat FindPlayerSeatWithButton(PlayerButtonType button)
        => FindPlayerSeatWithButtonOrDefault(button)
        ?? throw new InvalidOperationException($"No player with ${button} button");

    private void GiveButtonToNextSeat(PlayerButtonType button, int currentSeatNo)
    {
        var nextSeatNo = currentSeatNo + 1;
        if (nextSeatNo > _maxPlayers) nextSeatNo = 0;

        Seats.Where(s => !s.IsAvailable && s.SeatNumber >= nextSeatNo)
            .OrderBy(s => s.SeatNumber)
            .First().Player!.GiveButton(button);
    }

    private List<Player> PlayersLeftOfButton(PlayerButtonType button)
    {
        var mutablePlayers = new List<Player>(Players);
        var sbIndex = mutablePlayers.FindIndex(p => p.Button == button);
        if (sbIndex < 0) throw new InvalidOperationException("No player with small blind. Unable to determine player to start dealing cards to");

        var afterSB = mutablePlayers.GetRange(0, sbIndex);
        mutablePlayers.RemoveRange(0, sbIndex);
        mutablePlayers.AddRange(afterSB);
        return mutablePlayers;
    }

    private Game? GetActiveGame()
    {
        var game = _games.LastOrDefault();
        return game != null && game.CurrentState == GameState.InPlay ? game : null;
    }
}

