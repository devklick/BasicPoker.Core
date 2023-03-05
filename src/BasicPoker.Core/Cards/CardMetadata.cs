namespace BasicPoker.Core.Cards;

public class CardMetadata
{
    public string Name => $"{Rank} of {Suit}";
    public CardSuit Suit { get; set; }
    public CardRank Rank { get; set; }
    public CardSuitColor Color { get; set; }
    public int[] Values { get; set; } = Array.Empty<int>();
    public string SuitSprite { get; set; } = string.Empty;
    public string RankSprite { get; set; } = string.Empty;
}
