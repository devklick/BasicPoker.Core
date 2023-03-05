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
            var playerCards = new List<Card> { Card.KingOfClubs, Card.AceOfClubs };
            var tableCards = new List<Card> { Card.TenOfClubs, Card.JackOfHearts, Card.QueenOfClubs };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var highCardResult = result.FirstOrDefault(r => r.Key == HandType.HighCard);
            highCardResult.Should().NotBeNull();
            var cardsInHand = highCardResult.Value.Hand.Cards;
            cardsInHand.Should().ContainSingle();
            cardsInHand.First().Should().Be(Card.AceOfClubs);
        }

        [Fact]
        public void FiveCards_HighCard_InTableCards()
        {
            // arrange
            var playerCards = new List<Card> { Card.KingOfClubs, Card.JackOfHearts };
            var tableCards = new List<Card> { Card.TenOfClubs, Card.AceOfClubs, Card.QueenOfClubs };
            // act
            var result = HandHelper.DetermineHandProbabilities(playerCards, tableCards);
            // assert
            var highCardResult = result.FirstOrDefault(r => r.Key == HandType.HighCard);
            highCardResult.Should().NotBeNull();
            var cardsInHand = highCardResult.Value.Hand.Cards;
            cardsInHand.Should().ContainSingle();
            cardsInHand.First().Should().Be(Card.AceOfClubs);
        }

        [Fact]
        public void FiveCards_Pair_InTableCards()
        {
            // arrange
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;

            var playerCards = new List<Card> { Card.KingOfClubs, Card.JackOfHearts };
            var tableCards = new List<Card> { expected1, expected2, Card.ThreeOfClubs };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;

            var playerCards = new List<Card> { expected1, expected2 };
            var tableCards = new List<Card> { Card.KingOfClubs, Card.JackOfHearts, Card.ThreeOfClubs };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;

            var playerCards = new List<Card> { expected1, Card.FourOfClubs };
            var tableCards = new List<Card> { Card.KingOfClubs, expected2, Card.ThreeOfClubs };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;
            var expected3 = Card.ThreeOfDiamonds;
            var expected4 = Card.ThreeOfSpades;

            var playerCards = new List<Card> { expected2, expected1 };
            var tableCards = new List<Card> { Card.KingOfClubs, expected4, expected3 };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;
            var expected3 = Card.ThreeOfDiamonds;
            var expected4 = Card.ThreeOfSpades;

            var playerCards = new List<Card> { expected2, Card.EightOfClubs };
            var tableCards = new List<Card> { expected4, expected1, expected3 };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;
            var expected3 = Card.TwoOfHearts;

            var playerCards = new List<Card> { Card.SixOfDiamonds, Card.FourOfClubs };
            var tableCards = new List<Card> { expected1, expected2, expected3 };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;
            var expected3 = Card.TwoOfHearts;

            var playerCards = new List<Card> { Card.SixOfDiamonds, expected1, };
            var tableCards = new List<Card> { Card.NineOfDiamonds, expected2, expected3 };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;
            var expected3 = Card.TwoOfHearts;
            var expected4 = Card.TenOfClubs;
            var expected5 = Card.TenOfHearts;

            var playerCards = new List<Card> { expected4, expected5 };
            var tableCards = new List<Card> { expected3, expected1, expected2 };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;
            var expected3 = Card.TwoOfHearts;
            var expected4 = Card.TenOfClubs;
            var expected5 = Card.TenOfHearts;

            var playerCards = new List<Card> { expected1, expected5 };
            var tableCards = new List<Card> { expected3, expected4, expected2 };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;
            var expected3 = Card.TwoOfHearts;
            var expected4 = Card.TwoOfSpades;

            var playerCards = new List<Card> { Card.SixOfDiamonds, expected1, };
            var tableCards = new List<Card> { expected2, expected3, expected4 };
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
            var expected1 = Card.TwoOfClubs;
            var expected2 = Card.TwoOfDiamonds;
            var expected3 = Card.TwoOfHearts;
            var expected4 = Card.TwoOfSpades;

            var playerCards = new List<Card> { expected2, expected1, };
            var tableCards = new List<Card> { Card.EightOfClubs, expected3, expected4 };
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
            var expected1 = Card.AceOfHearts;
            var expected2 = Card.SixOfHearts;
            var expected3 = Card.TenOfHearts;
            var expected4 = Card.FourOfHearts;
            var expected5 = Card.EightOfHearts;

            var playerCards = new List<Card> { expected1, expected2 };
            var tableCards = new List<Card> { expected3, expected4, expected5 };
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
            var expected1 = Card.AceOfHearts;
            var expected2 = Card.TwoOfClubs;
            var expected3 = Card.ThreeOfHearts;
            var expected4 = Card.FourOfHearts;
            var expected5 = Card.FiveOfSpades;

            var playerCards = new List<Card> { expected2, expected4 };
            var tableCards = new List<Card> { expected1, expected3, expected5 };
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
            var expected1 = Card.AceOfHearts;
            var expected2 = Card.KingOfDiamonds;
            var expected3 = Card.QueenOfHearts;
            var expected4 = Card.JackOfClubs;
            var expected5 = Card.TenOfSpades;

            var playerCards = new List<Card> { expected2, expected4 };
            var tableCards = new List<Card> { expected1, expected3, expected5 };
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
            var expected1 = Card.FourOfClubs;
            var expected2 = Card.FiveOfDiamonds;
            var expected3 = Card.SixOfDiamonds;
            var expected4 = Card.SevenOfSpades;
            var expected5 = Card.EightOfHearts;

            var playerCards = new List<Card> { expected2, expected4 };
            var tableCards = new List<Card> { expected1, expected3, expected5 };
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
            var expected1 = Card.FourOfClubs;
            var expected2 = Card.FiveOfClubs;
            var expected3 = Card.SixOfClubs;
            var expected4 = Card.SevenOfClubs;
            var expected5 = Card.EightOfClubs;

            var playerCards = new List<Card> { expected2, expected4 };
            var tableCards = new List<Card> { expected1, expected3, expected5 };
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
            var expected1 = Card.NineOfDiamonds;
            var expected2 = Card.TenOfDiamonds;
            var expected3 = Card.JackOfDiamonds;
            var expected4 = Card.QueenOfDiamonds;
            var expected5 = Card.KingOfDiamonds;

            var playerCards = new List<Card> { expected2, expected4 };
            var tableCards = new List<Card> { expected1, expected3, expected5 };
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
            var expected1 = Card.TenOfSpades;
            var expected2 = Card.JackOfSpades;
            var expected3 = Card.QueenOfSpades;
            var expected4 = Card.KingOfSpades;
            var expected5 = Card.AceOfSpades;

            var playerCards = new List<Card> { expected2, expected4 };
            var tableCards = new List<Card> { expected1, expected3, expected5 };
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