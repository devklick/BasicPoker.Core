using BasicPoker.Core.Cards;
using BasicPoker.Core.Hands;
using BasicPoker.Core.Helpers;

using FluentAssertions;

namespace BasicPoker.Core.Test.Helpers
{
    public partial class HandHelperTest
    {
        [Fact]
        public void TwoCards_HighCard_KingAce()
        {
            // arrange
            var cards = new List<CardDetails> { CardDetails.KingOfClubs, CardDetails.AceOfClubs };
            // act
            var result = HandHelper.DetermineHandProbabilities(cards, new List<CardDetails>());
            // assert
            var highCardResult = result.FirstOrDefault(r => r.Key == HandType.HighCard);
            highCardResult.Should().NotBeNull();
            var cardsInHand = highCardResult.Value.Hand.Cards;
            cardsInHand.Should().ContainSingle();
            cardsInHand.First().Should().Be(CardDetails.AceOfClubs);
        }

        [Fact]
        public void TwoCards_HighCard_TwoAce()
        {
            // arrange
            var cards = new List<CardDetails> { CardDetails.TwoOfClubs, CardDetails.AceOfSpades };
            // act
            var result = HandHelper.DetermineHandProbabilities(cards, new List<CardDetails>());
            // assert
            var highCardResult = result.FirstOrDefault(r => r.Key == HandType.HighCard);
            highCardResult.Should().NotBeNull();
            var cardsInHand = highCardResult.Value.Hand.Cards;
            cardsInHand.Should().ContainSingle();
            cardsInHand.First().Should().Be(CardDetails.AceOfSpades);
        }


        [Fact]
        public void TwoCards_Pair_AceAce()
        {
            // arrange
            var cards = new List<CardDetails> { CardDetails.AceOfClubs, CardDetails.AceOfDiamonds };
            // act
            var result = HandHelper.DetermineHandProbabilities(cards, new List<CardDetails>());
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.Pair);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(2);
            cardsInHand.Should().Contain(cards.First());
            cardsInHand.Should().Contain(cards.Last());
        }

        [Fact]
        public void TwoCards_Pair_KingKing()
        {
            // arrange
            var cards = new List<CardDetails> { CardDetails.KingOfClubs, CardDetails.KingOfHearts };
            // act
            var result = HandHelper.DetermineHandProbabilities(cards, new List<CardDetails>());
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.Pair);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(2);
            cardsInHand.Should().Contain(cards.First());
            cardsInHand.Should().Contain(cards.Last());
        }
    }
}