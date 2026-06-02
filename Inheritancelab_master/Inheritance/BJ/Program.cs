using System;
using CardClasses;

namespace BJ
{
    class Program
    {
        static void Main(string[] args)
        {
            HandTests();
            BJHandTests();
            Blackjack();
        }

        static void HandTests()
        {
            Console.WriteLine("Hand Tests");

            Deck deck = new Deck();
            deck.Shuffle();

            Hand emptyHand = new Hand();
            AssertEqual("Empty hand NumCards", 0, emptyHand.NumCards);
            Console.WriteLine("Empty hand ToString:\n" + emptyHand);

            Hand hand = new Hand(deck, 5);
            AssertEqual("Dealt hand NumCards", 5, hand.NumCards);
            Console.WriteLine("Dealt hand:\n" + hand);

            Card extra = deck.Deal();
            hand.AddCard(extra);
            AssertEqual("After AddCard NumCards", 6, hand.NumCards);

            Card first = hand.GetCard(0);
            Console.WriteLine($"GetCard(0): {first}");

            AssertEqual("HasCard(Card)        expect True", true, hand.HasCard(first));
            AssertEqual("HasCard(value, suit)  expect True", true, hand.HasCard(first.Value, first.Suit));
            AssertEqual("HasCard(value)        expect True", true, hand.HasCard(first.Value));

            AssertEqual("IndexOf(Card)        expect 0", 0, hand.IndexOf(first));
            AssertEqual("IndexOf(value, suit) expect 0", 0, hand.IndexOf(first.Value, first.Suit));
            AssertEqual("IndexOf(value)       expect >=0", true, hand.IndexOf(first.Value) >= 0);

            Card discarded = hand.Discard(0);
            AssertEqual("After Discard NumCards", 5, hand.NumCards);
            AssertEqual("HasCard after discard expect False", false, hand.HasCard(discarded));
            Console.WriteLine($"Discarded: {discarded}");
            Console.WriteLine("Hand after discard:\n" + hand);
            Console.WriteLine();
        }

        static void BJHandTests()
        {
            Console.WriteLine("BJHand Tests");

            BJHand empty = new BJHand();
            AssertEqual("Empty BJHand Score", 0, empty.Score);
            AssertEqual("Empty BJHand IsBusted", false, empty.IsBusted);
            AssertEqual("Empty BJHand HasAce", false, empty.HasAce);

            Deck d = new Deck();
            d.Shuffle();
            BJHand random = new BJHand(d, 2);
            Console.WriteLine("Random 2-card BJHand:\n" + random);

            BJHand bj = new BJHand();
            bj.AddCard(new Card(1, 1));
            bj.AddCard(new Card(13, 2));
            Console.WriteLine("Ace + King:\n" + bj);
            AssertEqual("Ace+King Score expect 21", 21, bj.Score);
            AssertEqual("Ace+King IsBusted", false, bj.IsBusted);
            AssertEqual("Ace+King HasAce", true, bj.HasAce);

            BJHand bust = new BJHand();
            bust.AddCard(new Card(10, 1));
            bust.AddCard(new Card(10, 2));
            bust.AddCard(new Card(5, 3));
            Console.WriteLine("10+10+5:\n" + bust);
            AssertEqual("10+10+5 Score expect 25", 25, bust.Score);
            AssertEqual("10+10+5 IsBusted", true, bust.IsBusted);

            BJHand soft = new BJHand();
            soft.AddCard(new Card(1, 1));
            soft.AddCard(new Card(10, 2));
            soft.AddCard(new Card(8, 3));
            Console.WriteLine("Ace+Ten+8:\n" + soft);
            AssertEqual("Ace+Ten+8 Score expect 19", 19, soft.Score);
            AssertEqual("Ace+Ten+8 IsBusted", false, soft.IsBusted);

            BJHand twoAces = new BJHand();
            twoAces.AddCard(new Card(1, 1));
            twoAces.AddCard(new Card(1, 2));
            Console.WriteLine("Ace+Ace:\n" + twoAces);
            AssertEqual("Ace+Ace Score expect 12", 12, twoAces.Score);
            Console.WriteLine();
        }

        static void Blackjack()
        {
            int playerWins = 0;
            int dealerWins = 0;
            bool playAgain = true;

            while (playAgain)
            {
                Deck deck = new Deck();
                deck.Shuffle();
                BJHand playerHand = new BJHand(deck, 2);
                BJHand dealerHand = new BJHand(deck, 2);

                Console.WriteLine($"Player: {playerWins}  Dealer: {dealerWins}\n");
                Console.WriteLine($"Dealer shows: {dealerHand.GetCard(0)}  [second card hidden]\n");
                ShowHand("Your", playerHand);

                if (playerHand.Score == 21)
                {
                    Console.WriteLine("Blackjack!");
                    ResolveHand(playerHand, dealerHand, deck, ref playerWins, ref dealerWins);
                }
                else
                {
                    bool playerDone = false;
                    while (!playerDone && !playerHand.IsBusted)
                    {
                        string action = PromptHitOrStand();

                        if (action == "H")
                        {
                            playerHand.AddCard(deck.Deal());
                            ShowHand("Your", playerHand);

                            if (playerHand.IsBusted)
                                playerDone = true;
                            else if (playerHand.Score == 21)
                                playerDone = true;
                        }
                        else
                        {
                            playerDone = true;
                        }
                    }

                    ResolveHand(playerHand, dealerHand, deck, ref playerWins, ref dealerWins);
                }

                Console.WriteLine($"\nPlayer: {playerWins}  Dealer: {dealerWins}");
                playAgain = PromptPlayAgain();
                Console.WriteLine();
            }

            Console.WriteLine($"Final  Player: {playerWins}  Dealer: {dealerWins}");
        }

        static void ResolveHand(BJHand player, BJHand dealer, Deck deck,
                                ref int playerWins, ref int dealerWins)
        {
            ShowHand("Dealer", dealer);

            while (!dealer.IsBusted && dealer.Score <= 16)
            {
                Console.WriteLine("Dealer hits...");
                dealer.AddCard(deck.Deal());
                ShowHand("Dealer", dealer);
            }

            Console.WriteLine();
            if (player.IsBusted)
            {
                Console.WriteLine("Dealer wins - you busted.");
                dealerWins++;
            }
            else if (dealer.IsBusted)
            {
                Console.WriteLine("You win - dealer busted!");
                playerWins++;
            }
            else if (player.Score > dealer.Score)
            {
                Console.WriteLine($"You win! ({player.Score} vs {dealer.Score})");
                playerWins++;
            }
            else if (dealer.Score > player.Score)
            {
                Console.WriteLine($"Dealer wins. ({dealer.Score} vs {player.Score})");
                dealerWins++;
            }
            else
            {
                Console.WriteLine($"Push - tie at {player.Score}.");
            }
        }

        static void ShowHand(string label, BJHand hand)
        {
            Console.WriteLine($"{label} hand (score: {hand.Score}):");
            for (int i = 0; i < hand.NumCards; i++)
                Console.WriteLine($"  {hand.GetCard(i)}");
            Console.WriteLine();
        }

        static string PromptHitOrStand()
        {
            while (true)
            {
                Console.Write("Hit (H) or Stand (S)? ");
                string input = Console.ReadLine()?.Trim().ToUpper();

                if (input == "H" || input == "HIT") return "H";
                if (input == "S" || input == "STAND") return "S";

                Console.WriteLine("Please enter H or S.");
            }
        }

        static bool PromptPlayAgain()
        {
            while (true)
            {
                Console.Write("Play again? (Y/N): ");
                string input = Console.ReadLine()?.Trim().ToUpper();

                if (input == "Y" || input == "YES") return true;
                if (input == "N" || input == "NO") return false;

                Console.WriteLine("Please enter Y or N.");
            }
        }

        static void AssertEqual<T>(string label, T expected, T actual)
        {
            bool pass = expected.Equals(actual);
            Console.WriteLine($"  [{(pass ? "PASS" : "FAIL")}] {label}");
            if (!pass)
                Console.WriteLine($"         Expected: {expected}   Got: {actual}");
        }
    }
}