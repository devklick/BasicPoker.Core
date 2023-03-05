using BasicPoker.Core.Cards;

namespace BasicPoker.Core.Players;

public class Dealer
{
    private readonly Deck _deck;

    public Dealer(Deck deck)
    {
        _deck = deck;
    }

    public Dealer()
    {
        _deck = new Deck();
    }

    public void DealTo(IReadOnlyList<Player> players, int numberOfCards = 2)
    {
        var mutablePlayers = new List<Player>(players);
        var sbIndex = mutablePlayers.FindIndex(p => p.Button == PlayerButtonType.SmallBlind);
        if (sbIndex < 0) throw new InvalidOperationException("No player with small blind. Unable to determine player to start dealing cards to");

        var afterSB = mutablePlayers.GetRange(0, sbIndex);
        mutablePlayers.RemoveRange(0, sbIndex);
        mutablePlayers.AddRange(afterSB);

        for (var i = 0; i < numberOfCards; i++)
        {
            foreach (var player in mutablePlayers)
            {
                player.DealCard(_deck.TakeCard());
            }
        }
    }
}