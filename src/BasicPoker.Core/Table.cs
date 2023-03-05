using BasicPoker.Core.Cards;
using BasicPoker.Core.Players;

namespace BasicPoker.Core;

public class Table
{
    public bool RequiresMorePlayers => _players.Count < _minPlayers;
    public bool SupportsMorePlayers => _players.Count < _maxPlayers;
    private readonly List<Player> _players;
    private readonly Dealer _dealer;
    private readonly int _minPlayers;
    private readonly int _maxPlayers;
    private readonly CardCollection _communityCards;

    public Table(List<Player> players, Dealer dealer, CardCollection communityCards)
    {
        _players = players;
        _dealer = dealer;
        _communityCards = communityCards;
    }

    public Table(int minPlayers = 3, int maxPlayers = 7)
    {
        _minPlayers = minPlayers;
        _maxPlayers = maxPlayers;
        _dealer = new Dealer();
        _players = new List<Player>(maxPlayers);
        _communityCards = new CardCollection(5);
    }

    public void RotateButtons()
    {
        var sb = FindPlayerWithButton(PlayerButtonType.SmallBlind);
        var bb = FindPlayerWithButton(PlayerButtonType.BigBlind);
        var d = FindPlayerWithButton(PlayerButtonType.Dealer);

        sb.TakeButton();
        sb.TakeButton();
        d.TakeButton();

        GiveButtonToNextSeat(sb.Button, sb.SeatNumber);
        GiveButtonToNextSeat(bb.Button, bb.SeatNumber);
        GiveButtonToNextSeat(d.Button, d.SeatNumber);
    }

    private Player FindPlayerWithButton(PlayerButtonType button)
        => _players.FirstOrDefault(p => p.Button == button)
        ?? throw new InvalidOperationException($"No player with ${button} button");

    private void GiveButtonToNextSeat(PlayerButtonType button, int previousSeatNo)
    {
        var nextSeatNo = previousSeatNo + 1;
        if (nextSeatNo > _maxPlayers) nextSeatNo = 0;
        _players.First(p => p.SeatNumber == nextSeatNo).GiveButton(button);
    }
}