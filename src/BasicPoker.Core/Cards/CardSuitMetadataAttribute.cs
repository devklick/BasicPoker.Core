namespace BasicPoker.Core.Cards;

[AttributeUsage(AttributeTargets.Field)]
public class CardSuitMetadataAttribute : Attribute
{
    public string Sprite { get; }
    public CardSuitColor Color { get; }

    public CardSuitMetadataAttribute(string sprite, CardSuitColor color)
    {
        Sprite = sprite;
        Color = color;
    }
}
