using BasicPoker.Core.Cards;

namespace BasicPoker.Core.Players;

public class Player
{
    public CardCollection Cards { get; }
    public PlayerButtonType Button { get; private set; }

    public Player()
    {
        Cards = new CardCollection(2);
    }

    public void DealCard(Card card)
        => Cards.Add(card);

    public List<Card> TakeCards()
        => Cards.TakeAll();

    public void GiveButton(PlayerButtonType button)
        => Button = button;

    public PlayerButtonType TakeButton()
    {
        var button = Button;
        Button = PlayerButtonType.None;
        return button;
    }
}