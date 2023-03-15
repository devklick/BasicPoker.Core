namespace BasicPoker.Core.Players;

public class PlayerButtonMetadata
{
    public string Sprite { get; }
    public double MinimumBetMultiplier { get; }
    public PlayerButtonMetadata(string sprite, double minimumBetMultiplier)
    {
        Sprite = sprite;
        MinimumBetMultiplier = minimumBetMultiplier;
    }
}