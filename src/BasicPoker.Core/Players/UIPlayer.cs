using BasicPoker.Core.Cards;
using BasicPoker.Core.Games;

namespace BasicPoker.Core.Players;

public class UIPlayer : Player
{
    public UIPlayer(string name, double stack) : base(name, stack)
    { }

    public override async Task<PlayerAction?> GetPlayerAction(IReadOnlyList<Card> communityCards, BettingRound bettingRound)
        => await Task.FromResult<PlayerAction?>(null);

}