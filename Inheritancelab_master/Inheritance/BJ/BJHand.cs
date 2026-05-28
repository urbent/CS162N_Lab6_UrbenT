using System;
namespace CardClasses
public class BJHand : Hand
{
    public bool HasAce
    {
        get { return HasCard(1); }
    }

    public bool IsBusted
    {
        get { return Score > 21; }
    }

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

    public BJHand() : base()
    {
    }

    public BJHand(Deck d, int numCards) : base(d, numCards)
    {
    }
}
