using BasicPoker.Core.Cards;
using BasicPoker.Core.Games;

namespace BasicPoker.Core.Players;

public sealed class AIPlayerLevel1 : Player
{
    public AIPlayerLevel1(string name, double stack) : base(name, stack)
    { }

    public override async Task<PlayerAction?> GetPlayerAction(IReadOnlyList<Card> communityCards, BettingRound bettingRound)
    {
        // TODO: Replace this temporary implementation with something more sophisticated
        await Task.Delay(1000 * 2);
        return bettingRound.CanPlayerCheck(this) ? PlayerAction.Check() : PlayerAction.Fold();
    }
}