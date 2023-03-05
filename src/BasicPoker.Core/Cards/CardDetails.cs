namespace BasicPoker.Core.Cards;

public class CardDetails
{
    public string Name => $"{Rank} of {Suit}";
    public CardSuit Suit { get; }
    public CardRank Rank { get; }
    public Facing Facing { get; }

    public CardDetails(CardSuit suit, CardRank rank, Facing facing = Facing.Up)
    {
        Suit = suit;
        Rank = rank;
        Facing = facing;
    }

    public static readonly CardDetails TwoOfSpades = new(CardSuit.Spades, CardRank.Two);
    public static readonly CardDetails TwoOfClubs = new(CardSuit.Clubs, CardRank.Two);
    public static readonly CardDetails TwoOfHearts = new(CardSuit.Hearts, CardRank.Two);
    public static readonly CardDetails TwoOfDiamonds = new(CardSuit.Diamonds, CardRank.Two);
    public static readonly CardDetails ThreeOfSpades = new(CardSuit.Spades, CardRank.Three);
    public static readonly CardDetails ThreeOfClubs = new(CardSuit.Clubs, CardRank.Three);
    public static readonly CardDetails ThreeOfHearts = new(CardSuit.Hearts, CardRank.Three);
    public static readonly CardDetails ThreeOfDiamonds = new(CardSuit.Diamonds, CardRank.Three);
    public static readonly CardDetails FourOfSpades = new(CardSuit.Spades, CardRank.Four);
    public static readonly CardDetails FourOfClubs = new(CardSuit.Clubs, CardRank.Four);
    public static readonly CardDetails FourOfHearts = new(CardSuit.Hearts, CardRank.Four);
    public static readonly CardDetails FourOfDiamonds = new(CardSuit.Diamonds, CardRank.Four);
    public static readonly CardDetails FiveOfSpades = new(CardSuit.Spades, CardRank.Five);
    public static readonly CardDetails FiveOfClubs = new(CardSuit.Clubs, CardRank.Five);
    public static readonly CardDetails FiveOfHearts = new(CardSuit.Hearts, CardRank.Five);
    public static readonly CardDetails FiveOfDiamonds = new(CardSuit.Diamonds, CardRank.Five);
    public static readonly CardDetails SixOfSpades = new(CardSuit.Spades, CardRank.Six);
    public static readonly CardDetails SixOfClubs = new(CardSuit.Clubs, CardRank.Six);
    public static readonly CardDetails SixOfHearts = new(CardSuit.Hearts, CardRank.Six);
    public static readonly CardDetails SixOfDiamonds = new(CardSuit.Diamonds, CardRank.Six);
    public static readonly CardDetails SevenOfSpades = new(CardSuit.Spades, CardRank.Seven);
    public static readonly CardDetails SevenOfClubs = new(CardSuit.Clubs, CardRank.Seven);
    public static readonly CardDetails SevenOfHearts = new(CardSuit.Hearts, CardRank.Seven);
    public static readonly CardDetails SevenOfDiamonds = new(CardSuit.Diamonds, CardRank.Seven);
    public static readonly CardDetails EightOfSpades = new(CardSuit.Spades, CardRank.Eight);
    public static readonly CardDetails EightOfClubs = new(CardSuit.Clubs, CardRank.Eight);
    public static readonly CardDetails EightOfHearts = new(CardSuit.Hearts, CardRank.Eight);
    public static readonly CardDetails EightOfDiamonds = new(CardSuit.Diamonds, CardRank.Eight);
    public static readonly CardDetails NineOfSpades = new(CardSuit.Spades, CardRank.Nine);
    public static readonly CardDetails NineOfClubs = new(CardSuit.Clubs, CardRank.Nine);
    public static readonly CardDetails NineOfHearts = new(CardSuit.Hearts, CardRank.Nine);
    public static readonly CardDetails NineOfDiamonds = new(CardSuit.Diamonds, CardRank.Nine);
    public static readonly CardDetails TenOfSpades = new(CardSuit.Spades, CardRank.Ten);
    public static readonly CardDetails TenOfClubs = new(CardSuit.Clubs, CardRank.Ten);
    public static readonly CardDetails TenOfHearts = new(CardSuit.Hearts, CardRank.Ten);
    public static readonly CardDetails TenOfDiamonds = new(CardSuit.Diamonds, CardRank.Ten);
    public static readonly CardDetails JackOfSpades = new(CardSuit.Spades, CardRank.Jack);
    public static readonly CardDetails JackOfClubs = new(CardSuit.Clubs, CardRank.Jack);
    public static readonly CardDetails JackOfHearts = new(CardSuit.Hearts, CardRank.Jack);
    public static readonly CardDetails JackOfDiamonds = new(CardSuit.Diamonds, CardRank.Jack);
    public static readonly CardDetails QueenOfSpades = new(CardSuit.Spades, CardRank.Queen);
    public static readonly CardDetails QueenOfClubs = new(CardSuit.Clubs, CardRank.Queen);
    public static readonly CardDetails QueenOfHearts = new(CardSuit.Hearts, CardRank.Queen);
    public static readonly CardDetails QueenOfDiamonds = new(CardSuit.Diamonds, CardRank.Queen);
    public static readonly CardDetails KingOfSpades = new(CardSuit.Spades, CardRank.King);
    public static readonly CardDetails KingOfClubs = new(CardSuit.Clubs, CardRank.King);
    public static readonly CardDetails KingOfHearts = new(CardSuit.Hearts, CardRank.King);
    public static readonly CardDetails KingOfDiamonds = new(CardSuit.Diamonds, CardRank.King);
    public static readonly CardDetails AceOfSpades = new(CardSuit.Spades, CardRank.Ace);
    public static readonly CardDetails AceOfClubs = new(CardSuit.Clubs, CardRank.Ace);
    public static readonly CardDetails AceOfHearts = new(CardSuit.Hearts, CardRank.Ace);
    public static readonly CardDetails AceOfDiamonds = new(CardSuit.Diamonds, CardRank.Ace);
}
