namespace BasicPoker.Core.Cards;

[AttributeUsage(AttributeTargets.Field)]
public class CardSuitMetadataAttribute : Attribute
{
    public string Sprite2D { get; }
    public CardSuitColor Color { get; }

    public CardSuitMetadataAttribute(string sprite, CardSuitColor color)
    {
        Sprite2D = sprite;
        Color = color;
    }
}
