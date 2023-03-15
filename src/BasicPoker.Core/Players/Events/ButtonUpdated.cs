namespace BasicPoker.Core.Players.Events;

public delegate void ButtonUpdatedEventHandler(object sender, ButtonUpdatedEventArgs args);

public class ButtonUpdatedEventArgs
{
    public PlayerButtonType Button { get; }

    public ButtonUpdatedEventArgs(PlayerButtonType button)
    {
        Button = button;
    }
}