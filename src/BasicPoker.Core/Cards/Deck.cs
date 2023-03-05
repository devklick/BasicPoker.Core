using BasicPoker.Core.Extensions;
using BasicPoker.Core.Helpers;

namespace BasicPoker.Core.Cards;

public class Deck
{
    private readonly Queue<CardDetails> _cards;

    public Deck(Queue<CardDetails> cards)
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
                _cards.Enqueue(new CardDetails(suit, rank));
            }
        }

        _cards.Shuffle();
    }

    public void Shuffle() => _cards.Shuffle();

    public CardDetails Take() => _cards.Dequeue();

    public void Add(params CardDetails[] cards)
    {
        foreach (var card in cards) _cards.Enqueue(card);
    }
}