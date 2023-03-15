namespace BasicPoker.Core.Cards.Events;

public delegate void ManyCardsRemovedEventHandler(object? sender, ManyCardsRemovedEventArgs args);

public class ManyCardsRemovedEventArgs
{
    public IReadOnlyList<Card> Cards { get; }

    public ManyCardsRemovedEventArgs(IReadOnlyList<Card> allCards)
    {
        Cards = allCards;
    }
}