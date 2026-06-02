using System;
using CardClasses;

namespace BJ
{
    public class BJHand : Hand
    {
        public BJHand() : base() { }

        public BJHand(Deck d, int numCards) : base(d, numCards) { }

        public bool HasAce => HasCard(1);
        public bool IsBusted => Score > 21;

        public int Score
        {
            get
            {
                int total = 0;
                int aceCount = 0;

                for (int i = 0; i < NumCards; i++)
                {
                    Card c = GetCard(i);

                    if (c.Value == 1)
                    {
                        aceCount++;
                        total += 11;
                    }
                    else if (c.Value >= 10)
                        total += 10;
                    else
                        total += c.Value;
                }

                while (total > 21 && aceCount > 0)
                {
                    total -= 10;
                    aceCount--;
                }

                return total;
            }
        }

        public override string ToString()
        {
            if (NumCards == 0)
                return "Hand is empty.";

            string result = $"Hand ({NumCards} card(s)):\n";
            for (int i = 0; i < NumCards; i++)
                result += $"  {GetCard(i)}\n";
            result += $"  Score: {Score}";
            return result;
        }
    }
}