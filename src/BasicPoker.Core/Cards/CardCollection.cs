using BasicPoker.Core.Cards.Events;

namespace BasicPoker.Core.Cards;

public class CardCollection
{
    public event CardReceivedEventHandler? CardReceived;
    public event CardRemovedEventHandler? CardRemoved;
    private readonly List<Card> _cards;
    public CardCollection(int max)
    {
        _cards = new(max);
    }

    public void Add(Card card)
    {
        _cards.Add(card);
        OnCardReceived(card);
    }

    public List<Card> TakeAll()
    {
        var cards = new List<Card>(_cards);
        _cards.Clear();
        cards.ForEach(OnCardRemoved);
        return cards;
    }

    protected virtual void OnCardReceived(Card card)
    {
        CardReceived?.Invoke(this, new CardReceivedEventArgs(card));
    }
    protected virtual void OnCardRemoved(Card card)
    {
        CardRemoved?.Invoke(this, new CardRemovedEventArgs(card));
    }
}