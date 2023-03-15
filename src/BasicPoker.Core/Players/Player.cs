using BasicPoker.Core.Cards;
using BasicPoker.Core.Games;
using BasicPoker.Core.Hands;
using BasicPoker.Core.Helpers;
using BasicPoker.Core.Players.Events;

namespace BasicPoker.Core.Players;

public abstract class Player
{
    public string Name { get; set; }
    public CardCollection Cards { get; }
    public PlayerButtonType Button { get; private set; }
    public double Stack { get; private set; }
    public IReadOnlyDictionary<HandType, HandProbability> HandProbabilities { get; private set; }

    public StackUpdatedEventHandler? StackUpdated;
    public ButtonUpdatedEventHandler? ButtonUpdated;

    public Player(string name, double stack)
    {
        Name = name;
        Cards = new CardCollection(2);
        Stack = stack;
        HandProbabilities = new Dictionary<HandType, HandProbability>();
    }

    public void DealCard(Card card)
    {
        Cards.Add(card);
        HandProbabilities = HandHelper.DetermineHandProbabilities(Cards.ToReadOnlyList(), new List<Card>());
    }

    public List<Card> TakeCards()
    {
        HandProbabilities = new Dictionary<HandType, HandProbability>();
        return Cards.TakeAll();
    }

    public void GiveButton(PlayerButtonType button)
    {
        Button = button;
        OnButtonUpdated(Button);
    }

    public PlayerButtonType TakeButton()
    {
        var button = Button;
        Button = PlayerButtonType.None;
        OnButtonUpdated(Button);
        return button;
    }

    public void CalculateHandProbabilities(IReadOnlyList<Card> communityCards)
        => HandProbabilities = HandHelper.DetermineHandProbabilities(Cards.ToReadOnlyList(), communityCards);

    public abstract Task<PlayerAction?> GetPlayerAction(IReadOnlyList<Card> communityCards, BettingRound bettingRound);

    public void DeductFunds(double amount)
    {
        Stack -= amount;
        OnStackUpdated(Stack);
    }

    protected virtual void OnButtonUpdated(PlayerButtonType button)
    {
        ButtonUpdated?.Invoke(this, new(button));
    }

    protected virtual void OnStackUpdated(double stack)
    {
        StackUpdated?.Invoke(this, new(stack));
    }
}