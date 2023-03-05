namespace BasicPoker.Core.Cards;

public class CardMetadata
{
    public string Name => $"{Rank} of {Suit}";
    public CardSuit Suit { get; set; }
    public CardRank Rank { get; set; }
    public CardSuitColor Color { get; set; }
    public int[] Values { get; set; }
    public string SuitSprite { get; set; }
    public string RankSprite { get; set; }
}
