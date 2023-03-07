namespace BasicPoker.Core.Cards.Events;

public delegate void CardRemovedEventHandler(object? sender, CardRemovedEventArgs args);

public class CardRemovedEventArgs
{
    public Card Card { get; }

    public CardRemovedEventArgs(Card card)
    {
        Card = card;
    }
}