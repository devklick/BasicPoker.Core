using BasicPoker.Core.Extensions;
using BasicPoker.Core.Helpers;

namespace BasicPoker.Core.Cards;

public class Deck
{
    private readonly Queue<Card> _cards;

    public Deck(Queue<Card> cards)
    {
        _cards = cards;
    }

    public Deck()
    {
        _cards = new();

        foreach (var rank in CardHelper.AllRanks)
        {
            foreach (var suit in CardHelper.AllSuits)
            {
                _cards.Enqueue(new Card(suit, rank));
            }
        }

        _cards.Shuffle();
    }

    public void Shuffle() => _cards.Shuffle();

    public Card TakeCard() => _cards.Dequeue();

    public void Add(params Card[] cards)
    {
        foreach (var card in cards) _cards.Enqueue(card);
    }
}