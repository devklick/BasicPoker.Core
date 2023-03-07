namespace BasicPoker.Core.Cards;

[AttributeUsage(AttributeTargets.Field)]
public class CardRankMetadataAttribute : Attribute
{
    public string Sprite { get; }
    public int[] Values { get; }

    public CardRankMetadataAttribute(string sprite, params int[] values)
    {
        Sprite = sprite;
        Values = values;
    }
}
