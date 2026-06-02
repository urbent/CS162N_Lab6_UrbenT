using System;
using System.Collections.Generic;

namespace CardClasses
{
    public class Deck
    {
        private List<Card> cards = new List<Card>();

        public Deck()
        {
            for (int value = 1; value <= 13; value++)
                for (int suit = 1; suit <= 4; suit++)
                    cards.Add(new Card(value, suit));
        }

        public int NumCards => cards.Count;
        public bool IsEmpty => cards.Count == 0;

        public Card this[int i] => cards[i];

        public Card Deal()
        {
            if (!IsEmpty)
            {
                Card c = cards[0];
                cards.Remove(c);
                return c;
            }
            return null;
        }

        public void Shuffle()
        {
            Random gen = new Random();
            for (int i = 0; i < NumCards; i++)
            {
                int rnd = gen.Next(0, NumCards);
                Card c = cards[rnd];
                cards[rnd] = cards[i];
                cards[i] = c;
            }
        }

        public override string ToString()
        {
            string output = "";
            foreach (Card c in cards)
                output += c.ToString() + "\n";
            return output;
        }
    }
}