namespace BasicPoker.Core.Cards;

[AttributeUsage(AttributeTargets.Field)]
public class CardRankMetadataAttribute : Attribute
{
    public string Sprite2D { get; }
    public int[] Values { get; }

    public CardRankMetadataAttribute(string sprite, params int[] values)
    {
        Sprite2D = sprite;
        Values = values;
    }
}
