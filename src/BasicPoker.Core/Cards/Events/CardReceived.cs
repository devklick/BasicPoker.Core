namespace BasicPoker.Core.Cards.Events;

public delegate void CardReceivedEventHandler(object? sender, CardReceivedEventArgs args);

public class CardReceivedEventArgs
{
    public Card NewCard { get; }
    public IReadOnlyList<Card> AllCards { get; }

    public CardReceivedEventArgs(Card card, IReadOnlyList<Card> allCards)
    {
        NewCard = card;
        AllCards = allCards;
    }
}