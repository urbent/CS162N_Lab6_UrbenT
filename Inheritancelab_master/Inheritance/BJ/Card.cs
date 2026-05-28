using System;

namespace CardClasses
{
    public class Card
    {
        private static string[] values = { "", "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "Ten", "Jack", "Queen", "King" };
        private static string[] suits = { "", "Clubs", "Diamonds", "Hearts", "Spades" };

        private int value;
        private int suit;

        public int Value
        {
            get { return value; }
            set
            {
                if (value >= 1 && value <= 13)
                    this.value = value;
                else
                    throw new ArgumentException("Value must be between 1 (Ace) and 13 (King).");
            }
        }
        public int Suit
        {
            get { return suit; }
            set
            {
                if (value >= 1 && value <= 4)
                    this.suit = value;
                else
                    throw new ArgumentException("Suit must be between 1 (Clubs) and 4 (Spades).");
            }
        }

        public Card()
        {
            Value = 1;
            Suit = 1;
        }

        public Card(int value, int suit)
        {
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return values[value] + " of " + suits[suit];
        }

        public bool IsClub() => suit == 1;
        public bool IsDiamond() => suit == 2;
        public bool IsHeart() => suit == 3;
        public bool IsSpade() => suit == 4;
        public bool IsBlack() => IsClub() || IsSpade();
        public bool IsRed() => IsHeart() || IsDiamond();
        public bool IsAce() => value == 1;
        public bool IsFaceCard() => value >= 11 && value <= 13;

        public bool SuitMatches(Card other) => this.suit == other.suit;
        public bool ValueMatches(Card other) => this.value == other.value;
    }
}
