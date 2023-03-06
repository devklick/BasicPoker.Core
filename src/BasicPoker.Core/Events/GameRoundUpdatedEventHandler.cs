using BasicPoker.Core.Games;

namespace BasicPoker.Core.Events;

public delegate void GameRoundUpdatedEventHandler(object? sender, GameRoundUpdatedEventArgs args);

public class GameRoundUpdatedEventArgs
{
    public BettingRoundType OldValue { get; }
    public BettingRoundType NewValue { get; }

    public GameRoundUpdatedEventArgs(BettingRoundType oldValue, BettingRoundType newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }
}