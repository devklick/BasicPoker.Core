namespace BasicPoker.Core.Cards;

public class CardCollection
{
    private readonly List<Card> _cards;
    public CardCollection(int max)
    {
        _cards = new(max);
    }

    public void Add(Card card)
    {
        _cards.Add(card);
    }

    public List<Card> TakeAll()
    {
        var cards = new List<Card>(_cards);
        _cards.Clear();
        return cards;
    }
}