namespace BasicPoker.Core.Players;

[AttributeUsage(AttributeTargets.Field)]
public class PlayerButtonTypeMetadataAttribute : Attribute
{
    public string Sprite { get; }
    public double SmallBlindMultiplier { get; }
    public PlayerButtonTypeMetadataAttribute(string sprite, double smallBlindMultiplier)
    {
        Sprite = sprite;
        SmallBlindMultiplier = smallBlindMultiplier;
    }
}