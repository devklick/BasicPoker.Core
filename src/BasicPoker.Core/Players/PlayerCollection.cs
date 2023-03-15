namespace BasicPoker.Core.Players;

public partial class PlayerCollection
{
    private readonly List<(Player Player, bool IsOut)> _players;
    private int _currentIndex;
    public Player Current => _players[_currentIndex].Player;
    public Player BigBlind => _players.First(p => p.Player.Button == PlayerButtonType.BigBlind).Player;
    public Player SmallBlind => _players.First(p => p.Player.Button == PlayerButtonType.SmallBlind).Player;

    public PlayerCollection(List<Player> players)
    {
        _players = players.Select(p => (p, false)).ToList();

        // When the players are initialized, the small blind (left of the dealer) becomes
        // the "current" player, because they are first to make their move.
        _currentIndex = players.FindIndex(p => p.Button == PlayerButtonType.SmallBlind);
        if (_currentIndex < 0) throw new ArgumentException("No small blind player found");
    }

    public Player? MoveToNext()
    {
        var newIndex = _currentIndex + 1;
        if (newIndex > _players.Count - 1) newIndex = 0;

        // Current.Unfocus();

        _currentIndex = newIndex;

        return Current;
    }

    // public List<Player> StartingFrom(Player player)
    // {
    //     var players = new List<PlayerNode>(_players);
    //     var playerIndex = players.IndexOf(player);

    //     // Take all the players before the big blind and stick them on the end of the array.
    //     var removed = _players.GetRange(0, playerIndex);
    //     players.RemoveRange(0, playerIndex);
    //     players.AddRange(removed);

    //     return players;
    // }

    // public void UpdateButtons()
    // {
    //     _players.ForEach(p => p.SetPlayerButtonType(p.Button));
    // }

    // public void ForEach(Action<PlayerNode> action)
    // {
    //     _players.ForEach(action);
    // }
}