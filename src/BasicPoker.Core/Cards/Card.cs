namespace BasicPoker.Core.Cards;

public class Card
{
    public string Name => $"{Rank} of {Suit}";
    public CardSuit Suit { get; }
    public CardRank Rank { get; }
    public Facing Facing { get; }

    public Card(CardSuit suit, CardRank rank, Facing facing = Facing.Up)
    {
        Suit = suit;
        Rank = rank;
        Facing = facing;
    }

    public static readonly Card TwoOfSpades = new(CardSuit.Spades, CardRank.Two);
    public static readonly Card TwoOfClubs = new(CardSuit.Clubs, CardRank.Two);
    public static readonly Card TwoOfHearts = new(CardSuit.Hearts, CardRank.Two);
    public static readonly Card TwoOfDiamonds = new(CardSuit.Diamonds, CardRank.Two);
    public static readonly Card ThreeOfSpades = new(CardSuit.Spades, CardRank.Three);
    public static readonly Card ThreeOfClubs = new(CardSuit.Clubs, CardRank.Three);
    public static readonly Card ThreeOfHearts = new(CardSuit.Hearts, CardRank.Three);
    public static readonly Card ThreeOfDiamonds = new(CardSuit.Diamonds, CardRank.Three);
    public static readonly Card FourOfSpades = new(CardSuit.Spades, CardRank.Four);
    public static readonly Card FourOfClubs = new(CardSuit.Clubs, CardRank.Four);
    public static readonly Card FourOfHearts = new(CardSuit.Hearts, CardRank.Four);
    public static readonly Card FourOfDiamonds = new(CardSuit.Diamonds, CardRank.Four);
    public static readonly Card FiveOfSpades = new(CardSuit.Spades, CardRank.Five);
    public static readonly Card FiveOfClubs = new(CardSuit.Clubs, CardRank.Five);
    public static readonly Card FiveOfHearts = new(CardSuit.Hearts, CardRank.Five);
    public static readonly Card FiveOfDiamonds = new(CardSuit.Diamonds, CardRank.Five);
    public static readonly Card SixOfSpades = new(CardSuit.Spades, CardRank.Six);
    public static readonly Card SixOfClubs = new(CardSuit.Clubs, CardRank.Six);
    public static readonly Card SixOfHearts = new(CardSuit.Hearts, CardRank.Six);
    public static readonly Card SixOfDiamonds = new(CardSuit.Diamonds, CardRank.Six);
    public static readonly Card SevenOfSpades = new(CardSuit.Spades, CardRank.Seven);
    public static readonly Card SevenOfClubs = new(CardSuit.Clubs, CardRank.Seven);
    public static readonly Card SevenOfHearts = new(CardSuit.Hearts, CardRank.Seven);
    public static readonly Card SevenOfDiamonds = new(CardSuit.Diamonds, CardRank.Seven);
    public static readonly Card EightOfSpades = new(CardSuit.Spades, CardRank.Eight);
    public static readonly Card EightOfClubs = new(CardSuit.Clubs, CardRank.Eight);
    public static readonly Card EightOfHearts = new(CardSuit.Hearts, CardRank.Eight);
    public static readonly Card EightOfDiamonds = new(CardSuit.Diamonds, CardRank.Eight);
    public static readonly Card NineOfSpades = new(CardSuit.Spades, CardRank.Nine);
    public static readonly Card NineOfClubs = new(CardSuit.Clubs, CardRank.Nine);
    public static readonly Card NineOfHearts = new(CardSuit.Hearts, CardRank.Nine);
    public static readonly Card NineOfDiamonds = new(CardSuit.Diamonds, CardRank.Nine);
    public static readonly Card TenOfSpades = new(CardSuit.Spades, CardRank.Ten);
    public static readonly Card TenOfClubs = new(CardSuit.Clubs, CardRank.Ten);
    public static readonly Card TenOfHearts = new(CardSuit.Hearts, CardRank.Ten);
    public static readonly Card TenOfDiamonds = new(CardSuit.Diamonds, CardRank.Ten);
    public static readonly Card JackOfSpades = new(CardSuit.Spades, CardRank.Jack);
    public static readonly Card JackOfClubs = new(CardSuit.Clubs, CardRank.Jack);
    public static readonly Card JackOfHearts = new(CardSuit.Hearts, CardRank.Jack);
    public static readonly Card JackOfDiamonds = new(CardSuit.Diamonds, CardRank.Jack);
    public static readonly Card QueenOfSpades = new(CardSuit.Spades, CardRank.Queen);
    public static readonly Card QueenOfClubs = new(CardSuit.Clubs, CardRank.Queen);
    public static readonly Card QueenOfHearts = new(CardSuit.Hearts, CardRank.Queen);
    public static readonly Card QueenOfDiamonds = new(CardSuit.Diamonds, CardRank.Queen);
    public static readonly Card KingOfSpades = new(CardSuit.Spades, CardRank.King);
    public static readonly Card KingOfClubs = new(CardSuit.Clubs, CardRank.King);
    public static readonly Card KingOfHearts = new(CardSuit.Hearts, CardRank.King);
    public static readonly Card KingOfDiamonds = new(CardSuit.Diamonds, CardRank.King);
    public static readonly Card AceOfSpades = new(CardSuit.Spades, CardRank.Ace);
    public static readonly Card AceOfClubs = new(CardSuit.Clubs, CardRank.Ace);
    public static readonly Card AceOfHearts = new(CardSuit.Hearts, CardRank.Ace);
    public static readonly Card AceOfDiamonds = new(CardSuit.Diamonds, CardRank.Ace);
}
