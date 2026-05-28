using System;
using System.Collections.Generic;
namespace CardClasses
public class Hand
{
    private List<Card> cards;

    public int NumCards
    {
        get { return cards.Count; }
    }

    public Hand()
    {
        cards = new List<Card>();
    }

    public Hand(Deck d, int numCards)
    {
        cards = new List<Card>();
        for (int i = 0; i < numCards; i++)
            cards.Add(d.Deal());
    }

    public void AddCard(Card c)
    {
        cards.Add(c);
    }

    public Card Discard(int index)
    {
        Card discarded = cards[index];
        cards.RemoveAt(index);
        return discarded;
    }

    public Card GetCard(int index)
    {
        return cards[index];
    }

    public bool HasCard(Card c)
    {
        return cards.Contains(c);
    }

    public bool HasCard(int value, int suit)
    {
        return IndexOf(value, suit) >= 0;
    }

    public bool HasCard(int value)
    {
        return IndexOf(value) >= 0;
    }

    public int IndexOf(Card c)
    {
        return cards.IndexOf(c);
    }

    public int IndexOf(int value, int suit)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Value == value && cards[i].Suit == suit)
                return i;
        }
        return -1;
    }

    public int IndexOf(int value)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Value == value)
                return i;
        }
        return -1;
    }

    public override string ToString()
    {
        if (cards.Count == 0)
            return "Hand is empty.";

        string result = $"Hand ({cards.Count} card(s)):\n";
        for (int i = 0; i < cards.Count; i++)
            result += $"  [{i}] {cards[i]}\n";

        return result;
    }
}
