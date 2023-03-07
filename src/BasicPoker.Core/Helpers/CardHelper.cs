using BasicPoker.Core.Cards;

namespace BasicPoker.Core.Helpers;

public class CardHelper
{
    public static readonly List<CardSuit> AllSuits;
    public static readonly List<CardRank> AllRanks;
    private static readonly Dictionary<CardSuit, CardSuitMetadataAttribute> CardSuitMetaData;
    private static readonly Dictionary<CardRank, CardRankMetadataAttribute> CardRankMetaData;

    static CardHelper()
    {
        CardSuitMetaData = EnumHelper.GetEnumMembersAndAttributes<CardSuit, CardSuitMetadataAttribute>();
        CardRankMetaData = EnumHelper.GetEnumMembersAndAttributes<CardRank, CardRankMetadataAttribute>();

        AllSuits = CardSuitMetaData.Keys.ToList();
        AllRanks = CardRankMetaData.Keys.ToList();
    }

    public static CardMetadata GetMetadata(CardRank rank, CardSuit suit)
    {
        if (!CardSuitMetaData.TryGetValue(suit, out var suitData))
            throw new ArgumentException($"No data exists for suit {suit}");

        if (!CardRankMetaData.TryGetValue(rank, out var rankData))
            throw new ArgumentException($"No data exists for suit {suit}");

        return new CardMetadata
        {
            Rank = rank,
            Suit = suit,
            Color = suitData.Color,
            SuitSprite = suitData.Sprite,
            RankSprite = rankData.Sprite,
            Values = rankData.Values
        };
    }
}