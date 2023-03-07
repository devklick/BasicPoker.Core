namespace BasicPoker.Core.Cards.Events;

public delegate void CardReceivedEventHandler(object? sender, CardReceivedEventArgs args);

public class CardReceivedEventArgs
{
    public Card Card { get; }

    public CardReceivedEventArgs(Card card)
    {
        Card = card;
    }
}