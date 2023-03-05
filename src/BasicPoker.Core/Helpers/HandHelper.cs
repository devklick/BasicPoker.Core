using BasicPoker.Core.Cards;
using BasicPoker.Core.Extensions;
using BasicPoker.Core.Hands;

namespace BasicPoker.Core.Helpers;

public class HandHelper
{
    private static readonly Dictionary<HandType, HandTypeMetadataAttribute> HandMetadata =
        EnumHelper.GetEnumMembersAndAttributes<HandType, HandTypeMetadataAttribute>();

    private static readonly List<HandType> HandTypes;

    static HandHelper()
    {
        HandTypes = HandMetadata.Keys.ToList().OrderByDescending(x => x).ToList();
    }

    public static Dictionary<HandType, HandProbability> DetermineHandProbabilities(List<Card> playerCards, List<Card> tableCards)
    {
        var results = new Dictionary<HandType, HandProbability>();

        foreach (var handType in HandTypes)
        {
            var allCards = new List<Card>(playerCards).Concat(tableCards).ToList();
            var metadata = HandMetadata[handType];

            results.Add(handType, DetermineHandProbability(allCards, handType, metadata));
        }

        return results;
    }

    private static HandProbability DetermineHandProbability(List<Card> allCards, HandType handType, HandTypeMetadataAttribute metadata)
    {
        var cardsInHand = new List<Card>(allCards);
        var result = (double probability) => new HandProbability(new Hand(cardsInHand, handType), probability);

        // TODO: Determination of hand probability
        if (!MeetsCriteria(MeetsSameRankCriteria, metadata.SameRankCounts, ref cardsInHand))
            return result(0);

        if (!MeetsCriteria(MeetsSameSuitCriteria, metadata.SameSuitCount, ref cardsInHand))
            return result(0);

        if (!MeetsCriteria(MeetsSequentialRankCriteria, metadata.SequentialRank, ref cardsInHand))
            return result(0);

        if (!MeetsCriteria(MeetsHighestCardCriteria, metadata.HighestCard, ref cardsInHand))
            return result(0);

        return result(100);
    }

    private static bool MeetsCriteria<T>(
        Func<List<Card>, T, (bool Success, List<Card> Cards)> checker,
        T checkerParam,
        ref List<Card> candidates)
    {
        var (success, cardsInHand) = checker.Invoke(candidates, checkerParam);

        if (success)
        {
            var copy = new List<Card>(cardsInHand);
            candidates.Clear();
            foreach (var card in copy) candidates.Add(card);
        }

        return success;
    }

    private static (bool Success, List<Card> Cards) MeetsSameRankCriteria(List<Card> cards, int[] sameRankCounts)
    {
        // If we don't card about having a certain number of cards with the same rank, 
        // this check passes and all candidate cards are still candidates
        if (sameRankCounts.All(c => c == 0)) return (true, cards);
        if (cards.Count < sameRankCounts.Aggregate((sum, val) => sum + val)) return (false, new List<Card>());

        var groups = cards
            .GroupBy(c => c.Rank)
            .Select(g => new { Rank = g.Key, Cards = g.ToList(), g.ToList().Count })
            .OrderByDescending(c => (int)c.Rank);

        var ranksUsed = new List<CardRank>();
        var cardsUsed = new List<Card>();

        foreach (var rankCount in sameRankCounts)
        {
            var match = groups.FirstOrDefault(g => !ranksUsed.Contains(g.Rank) && g.Count == rankCount);

            if (match == null)
            {
                return (false, cardsUsed);
            }

            ranksUsed.Add(match.Rank);
            cardsUsed.AddRange(match.Cards);
        }

        return (true, cardsUsed);
    }

    private static (bool Success, List<Card> Cards) MeetsSameSuitCriteria(List<Card> cards, int sameSuitCount)
    {
        // If we don't care about having a certain number of cards with the same suit, 
        // this check passes and all candidate cards are still candidates
        if (sameSuitCount == 0) return (true, cards);

        var groups = cards
            .GroupBy(r => r.Suit)
            .Select(r => new { Suit = r.Key, Cards = r.ToList(), r.ToList().Count });

        var suitsUsed = new List<CardSuit>();
        var cardsUsed = new List<Card>();

        var match = groups.FirstOrDefault(g => !suitsUsed.Contains(g.Suit) && g.Count == sameSuitCount);

        if (match == null)
        {
            return (false, cardsUsed);
        }

        suitsUsed.Add(match.Suit);
        cardsUsed.AddRange(match.Cards);

        return (true, cardsUsed);
    }

    private static (bool Success, List<Card> Cards) MeetsSequentialRankCriteria(List<Card> cards, bool sequentialRank)
    {
        // If we don't care about sequential ranks, this check passes and all cards are still candidates
        if (!sequentialRank) return (true, cards);

        var metadata = cards.Select(c => CardHelper.GetMetadata(c.Rank, c.Suit));

        var ranges = metadata.Select(md => md.Values).Combinations().Distinct();

        foreach (var range in ranges.OrderByDescending(r => r.OrderByDescending(r => r)))
        {
            var result = Enumerable.Range(0, range.Length).All(i => range[i] == range[0] + i);
            if (result) return (true, cards);
        }

        return (false, new List<Card>());
    }

    private static (bool Success, List<Card> Cards) MeetsHighestCardCriteria(List<Card> cards, CardRank? highestCard)
    {
        if (!highestCard.HasValue) return (true, cards);

        var success = cards.OrderByDescending(c => (int)c.Rank).First().Rank == highestCard;

        return (success, success ? cards : new List<Card>());
    }
}
