using BasicPoker.Core.Extensions;
using BasicPoker.Core.Helpers;

namespace BasicPoker.Core.Cards;

public class Deck
{
    public int Size { get; }
    private Queue<Card> _cards;
    public bool IsEmpty => !_cards.Any();

    public Deck(Queue<Card> cards)
    {
        _cards = cards;
    }

    public Deck()
    {
        var cards = new List<Card>();
        foreach (var rank in CardHelper.AllRanks)
        {
            foreach (var suit in CardHelper.AllSuits)
            {
                cards.Add(new Card(suit, rank));
            }
        }

        cards.Shuffle().Shuffle();

        _cards = new Queue<Card>(cards);
        Size = cards.Count;
    }

    public void Shuffle()
    {
        // TODO: May be better to get rid of queue and just store a readonly list
        var cards = new List<Card>(_cards);
        cards.Shuffle();
        _cards = new Queue<Card>(cards);
    }

    public Card TakeNextCard() => _cards.Dequeue();

    public void AddToDeck(params Card[] cards)
    {
        foreach (var card in cards) _cards.Enqueue(card);
    }

    public void ThrowIfEmpty()
    {
        if (!_cards.Any()) throw new ArgumentOutOfRangeException("No cards in the deck");
    }
}