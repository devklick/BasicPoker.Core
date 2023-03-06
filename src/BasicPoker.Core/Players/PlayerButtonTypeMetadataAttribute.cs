namespace BasicPoker.Core.Players;

[AttributeUsage(AttributeTargets.Field)]
public class PlayerButtonTypeMetadataAttribute : Attribute
{
    public string Sprite { get; }
    public double MinBetMultiplier { get; }
    public PlayerButtonTypeMetadataAttribute(string sprite, double minBetMultiplier)
    {
        Sprite = sprite;
        MinBetMultiplier = minBetMultiplier;
    }
}