using BasicPoker.Core.Cards;
using BasicPoker.Core.Hands;
using BasicPoker.Core.Helpers;

using FluentAssertions;

namespace BasicPoker.Core.Test.Helpers
{
    public partial class HandHelperTest
    {
        [Fact]
        public void FiveCards_HighCard_InPlayerCards()
        {
            // arrange
            var playerCards = new List<CardDetails> { CardDetails.KingOfClubs, CardDetails.AceOfClubs };
            var tableCards = new List<CardDetails> { CardDetails.TenOfClubs, CardDetails.JackOfHearts, CardDetails.QueenOfClubs };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var highCardResult = result.FirstOrDefault(r => r.Key == HandType.HighCard);
            highCardResult.Should().NotBeNull();
            var cardsInHand = highCardResult.Value.Hand.Cards;
            cardsInHand.Should().ContainSingle();
            cardsInHand.First().Should().Be(CardDetails.AceOfClubs);
        }

        [Fact]
        public void FiveCards_HighCard_InTableCards()
        {
            // arrange
            var playerCards = new List<CardDetails> { CardDetails.KingOfClubs, CardDetails.JackOfHearts };
            var tableCards = new List<CardDetails> { CardDetails.TenOfClubs, CardDetails.AceOfClubs, CardDetails.QueenOfClubs };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var highCardResult = result.FirstOrDefault(r => r.Key == HandType.HighCard);
            highCardResult.Should().NotBeNull();
            var cardsInHand = highCardResult.Value.Hand.Cards;
            cardsInHand.Should().ContainSingle();
            cardsInHand.First().Should().Be(CardDetails.AceOfClubs);
        }

        [Fact]
        public void FiveCards_Pair_InTableCards()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;

            var playerCards = new List<CardDetails> { CardDetails.KingOfClubs, CardDetails.JackOfHearts };
            var tableCards = new List<CardDetails> { expected1, expected2, CardDetails.ThreeOfClubs };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.Pair);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(2);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
        }

        [Fact]
        public void FiveCards_Pair_InPlayerCards()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;

            var playerCards = new List<CardDetails> { expected1, expected2 };
            var tableCards = new List<CardDetails> { CardDetails.KingOfClubs, CardDetails.JackOfHearts, CardDetails.ThreeOfClubs };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.Pair);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(2);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
        }

        [Fact]
        public void FiveCards_Pair_InPlayerAndTableCards()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;

            var playerCards = new List<CardDetails> { expected1, CardDetails.FourOfClubs };
            var tableCards = new List<CardDetails> { CardDetails.KingOfClubs, expected2, CardDetails.ThreeOfClubs };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.Pair);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(2);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
        }

        [Fact]
        public void FiveCards_TwoPair_PairInPlayerCards_PairOnTable()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;
            var expected3 = CardDetails.ThreeOfDiamonds;
            var expected4 = CardDetails.ThreeOfSpades;

            var playerCards = new List<CardDetails> { expected2, expected1 };
            var tableCards = new List<CardDetails> { CardDetails.KingOfClubs, expected4, expected3 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.TwoPair);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(4);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
        }


        [Fact]
        public void FiveCards_TwoPair_PairOnTable_PairSplitBetweenPlayerAndTable()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;
            var expected3 = CardDetails.ThreeOfDiamonds;
            var expected4 = CardDetails.ThreeOfSpades;

            var playerCards = new List<CardDetails> { expected2, CardDetails.EightOfClubs };
            var tableCards = new List<CardDetails> { expected4, expected1, expected3 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.TwoPair);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(4);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
        }


        [Fact]
        public void FiveCards_ThreeOfAKind_TableCards()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;
            var expected3 = CardDetails.TwoOfHearts;

            var playerCards = new List<CardDetails> { CardDetails.SixOfDiamonds, CardDetails.FourOfClubs };
            var tableCards = new List<CardDetails> { expected1, expected2, expected3 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.ThreeOfAKind);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(3);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
        }

        [Fact]
        public void FiveCards_ThreeOfAKind_PlayerAndTableCards()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;
            var expected3 = CardDetails.TwoOfHearts;

            var playerCards = new List<CardDetails> { CardDetails.SixOfDiamonds, expected1, };
            var tableCards = new List<CardDetails> { CardDetails.NineOfDiamonds, expected2, expected3 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.ThreeOfAKind);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(3);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
        }


        [Fact]
        public void FiveCards_FullHouse_PairInPlayerHand_ThreeOnTable()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;
            var expected3 = CardDetails.TwoOfHearts;
            var expected4 = CardDetails.TenOfClubs;
            var expected5 = CardDetails.TenOfHearts;

            var playerCards = new List<CardDetails> { expected4, expected5 };
            var tableCards = new List<CardDetails> { expected3, expected1, expected2 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.FullHouse);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(5);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
            cardsInHand.Should().Contain(expected5);
        }

        [Fact]
        public void FiveCards_FullHouse_Split()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;
            var expected3 = CardDetails.TwoOfHearts;
            var expected4 = CardDetails.TenOfClubs;
            var expected5 = CardDetails.TenOfHearts;

            var playerCards = new List<CardDetails> { expected1, expected5 };
            var tableCards = new List<CardDetails> { expected3, expected4, expected2 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.FullHouse);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(5);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
            cardsInHand.Should().Contain(expected5);
        }

        [Fact]
        public void FiveCards_FourOfAKind_ThreeOnTable()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;
            var expected3 = CardDetails.TwoOfHearts;
            var expected4 = CardDetails.TwoOfSpades;

            var playerCards = new List<CardDetails> { CardDetails.SixOfDiamonds, expected1, };
            var tableCards = new List<CardDetails> { expected2, expected3, expected4 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.FourOfAKind);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(4);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
        }

        [Fact]
        public void FiveCards_FourOfAKind_PairInPlayerHand()
        {
            // arrange
            var expected1 = CardDetails.TwoOfClubs;
            var expected2 = CardDetails.TwoOfDiamonds;
            var expected3 = CardDetails.TwoOfHearts;
            var expected4 = CardDetails.TwoOfSpades;

            var playerCards = new List<CardDetails> { expected2, expected1, };
            var tableCards = new List<CardDetails> { CardDetails.EightOfClubs, expected3, expected4 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.FourOfAKind);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(4);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
        }

        [Fact]
        public void FiveCards_Flush()
        {
            // arrange
            var expected1 = CardDetails.AceOfHearts;
            var expected2 = CardDetails.SixOfHearts;
            var expected3 = CardDetails.TenOfHearts;
            var expected4 = CardDetails.FourOfHearts;
            var expected5 = CardDetails.EightOfHearts;

            var playerCards = new List<CardDetails> { expected1, expected2 };
            var tableCards = new List<CardDetails> { expected3, expected4, expected5 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.Flush);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(5);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
            cardsInHand.Should().Contain(expected5);
        }

        [Fact]
        public void FiveCards_Straight_AceLow()
        {
            // arrange
            var expected1 = CardDetails.AceOfHearts;
            var expected2 = CardDetails.TwoOfClubs;
            var expected3 = CardDetails.ThreeOfHearts;
            var expected4 = CardDetails.FourOfHearts;
            var expected5 = CardDetails.FiveOfSpades;

            var playerCards = new List<CardDetails> { expected2, expected4 };
            var tableCards = new List<CardDetails> { expected1, expected3, expected5 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.Straight);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(5);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
            cardsInHand.Should().Contain(expected5);
        }

        [Fact]
        public void FiveCards_Straight_AceHigh()
        {
            // arrange
            var expected1 = CardDetails.AceOfHearts;
            var expected2 = CardDetails.KingOfDiamonds;
            var expected3 = CardDetails.QueenOfHearts;
            var expected4 = CardDetails.JackOfClubs;
            var expected5 = CardDetails.TenOfSpades;

            var playerCards = new List<CardDetails> { expected2, expected4 };
            var tableCards = new List<CardDetails> { expected1, expected3, expected5 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.Straight);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(5);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
            cardsInHand.Should().Contain(expected5);
        }

        [Fact]
        public void FiveCards_Straight()
        {
            // arrange
            var expected1 = CardDetails.FourOfClubs;
            var expected2 = CardDetails.FiveOfDiamonds;
            var expected3 = CardDetails.SixOfDiamonds;
            var expected4 = CardDetails.SevenOfSpades;
            var expected5 = CardDetails.EightOfHearts;

            var playerCards = new List<CardDetails> { expected2, expected4 };
            var tableCards = new List<CardDetails> { expected1, expected3, expected5 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.Straight);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(5);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
            cardsInHand.Should().Contain(expected5);
        }

        [Fact]
        public void FiveCards_StraightFlush()
        {
            // arrange
            var expected1 = CardDetails.FourOfClubs;
            var expected2 = CardDetails.FiveOfClubs;
            var expected3 = CardDetails.SixOfClubs;
            var expected4 = CardDetails.SevenOfClubs;
            var expected5 = CardDetails.EightOfClubs;

            var playerCards = new List<CardDetails> { expected2, expected4 };
            var tableCards = new List<CardDetails> { expected1, expected3, expected5 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.StraightFlush);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(5);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
            cardsInHand.Should().Contain(expected5);
        }

        [Fact]
        public void FiveCards_StraightFlush_OneBelowRoyal()
        {
            // arrange
            var expected1 = CardDetails.NineOfDiamonds;
            var expected2 = CardDetails.TenOfDiamonds;
            var expected3 = CardDetails.JackOfDiamonds;
            var expected4 = CardDetails.QueenOfDiamonds;
            var expected5 = CardDetails.KingOfDiamonds;

            var playerCards = new List<CardDetails> { expected2, expected4 };
            var tableCards = new List<CardDetails> { expected1, expected3, expected5 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.StraightFlush);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(5);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
            cardsInHand.Should().Contain(expected5);
        }

        [Fact]
        public void FiveCards_RoyalFlush()
        {
            // arrange
            var expected1 = CardDetails.TenOfSpades;
            var expected2 = CardDetails.JackOfSpades;
            var expected3 = CardDetails.QueenOfSpades;
            var expected4 = CardDetails.KingOfSpades;
            var expected5 = CardDetails.AceOfSpades;

            var playerCards = new List<CardDetails> { expected2, expected4 };
            var tableCards = new List<CardDetails> { expected1, expected3, expected5 };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var pairResult = result.FirstOrDefault(r => r.Key == HandType.RoyalFlush);
            pairResult.Should().NotBeNull();
            var cardsInHand = pairResult.Value.Hand.Cards;
            cardsInHand.Should().HaveCount(5);
            cardsInHand.Should().Contain(expected1);
            cardsInHand.Should().Contain(expected2);
            cardsInHand.Should().Contain(expected3);
            cardsInHand.Should().Contain(expected4);
            cardsInHand.Should().Contain(expected5);
        }
    }
}