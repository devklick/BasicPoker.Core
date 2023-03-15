using BasicPoker.Core.Cards.Events;

namespace BasicPoker.Core.Cards;

public class CardCollection
{
    public event CardReceivedEventHandler? CardReceived;
    public event CardRemovedEventHandler? CardRemoved;
    public event ManyCardsRemovedEventHandler? AllCardsRemoved;
    private readonly List<Card> _cards;

    public CardCollection(int max)
    {
        _cards = new(max);
    }

    public void Add(Card card)
    {
        _cards.Add(card);
        OnCardReceived(card, _cards);
    }

    public List<Card> TakeAll()
    {
        var cards = new List<Card>(_cards);
        _cards.Clear();
        cards.ForEach(OnCardRemoved);
        OnAllCardsRemoved(cards);
        return cards;
    }

    public IReadOnlyList<Card> ToReadOnlyList() => _cards.AsReadOnly();

    protected virtual void OnCardReceived(Card card, List<Card> allCards)
    {
        CardReceived?.Invoke(this, new CardReceivedEventArgs(card, allCards));
    }
    protected virtual void OnCardRemoved(Card card)
    {
        CardRemoved?.Invoke(this, new CardRemovedEventArgs(card));
    }
    protected virtual void OnAllCardsRemoved(List<Card> cardsRemoved)
    {
        AllCardsRemoved?.Invoke(this, new ManyCardsRemovedEventArgs(cardsRemoved));
    }
}