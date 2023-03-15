namespace BasicPoker.Core.Players.Events;

public delegate void StackUpdatedEventHandler(object sender, StackUpdatedEventArgs args);

public class StackUpdatedEventArgs
{
    public double Stack { get; }
    public StackUpdatedEventArgs(double stack)
    {
        Stack = stack;
    }
}