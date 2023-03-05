using BasicPoker.Core.Cards;

namespace BasicPoker.Core.Hands;

[AttributeUsage(AttributeTargets.Field)]
public class HandTypeMetadataAttribute : Attribute
{
    public int[] SameRankCounts { get; set; } = new[] { 0 };
    public int SameSuitCount { get; set; } = 0;
    public bool SequentialRank { get; set; } = false;
    public CardRank? HighestCard { get; set; } = null;
    public HandTypeMetadataAttribute() { }
    public HandTypeMetadataAttribute(CardRank highestCard)
    {
        HighestCard = highestCard;
    }
}
