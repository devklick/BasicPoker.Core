using BasicPoker.Core.Cards;

namespace BasicPoker.Core.Players;

public class Dealer
{
    private readonly Deck _deck;
    private readonly CardCollection _burnPile;

    public Dealer(Deck deck, CardCollection burnPile)
    {
        _deck = deck;
        _burnPile = burnPile;
    }

    public Dealer()
    {
        _deck = new Deck();
        _burnPile = new(_deck.Size);
    }

    public void DealTo(IReadOnlyList<Player> players, int numberOfCards = 2)
    {
        EnsureRemainingCards();
        for (var i = 0; i < numberOfCards; i++)
        {
            foreach (var player in players)
            {
                player.DealCard(_deck.TakeNextCard());
            }
        }
    }

    public void DealTo(CardCollection cardCollection, int numberOfCards = 1)
    {
        EnsureRemainingCards();
        for (var i = 0; i < numberOfCards; i++)
        {
            cardCollection.Add(_deck.TakeNextCard());
        }
    }

    public void Burn(int numberOfCards = 1)
    {
        EnsureRemainingCards();
        for (var i = 0; i < numberOfCards; i++)
        {
            _burnPile.Add(_deck.TakeNextCard());
        }
    }

    public void Burn(params Card[] cards)
    {
        foreach (var card in cards)
        {
            _burnPile.Add(card);
        }
    }

    private void EnsureRemainingCards()
        => _deck.ThrowIfEmpty();
}