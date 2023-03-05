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
        for (var i = 0; i < numberOfCards; i++)
        {
            foreach (var player in players)
            {
                player.DealCard(_deck.TakeCard());
            }
        }
    }
}