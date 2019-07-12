using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Poker;

namespace PokerPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            Console.WriteLine("Welcome!");
            Random rnd = new Random();

            for (var i = 0; i < 1; i++)
            {
                var deck = Enumerable.Range(0, 52).ToList();
                rnd.Shuffle(deck);

                var myCards = deck.Take(4).Select(num => new Card(num)).ToList();
                var opponentCards = deck.Skip(4).Take(4).Select(num => new Card(num)).ToList();

                var myWins = 0;
                var opponentWins = 0;

                for (var a = 8; a < 48; a++)
                {                    
                    for (var b = a + 1; b < 49; b++)
                    {
                        Console.WriteLine("a");
                        Console.WriteLine(a);
                        Console.WriteLine("b");
                        Console.WriteLine(b);
                        for (var c = b + 1; c < 50; c++)
                        {
                            for (var d = c + 1; d < 51; d++)
                            {
                                for (var e = d + 1; e < 52; e++)
                                {
                                    var board = new List<Card>() { new Card(deck[a]), new Card(deck[b]), new Card(deck[c]), new Card(deck[d]), new Card(deck[e]) };
                                    var myHand = new Omaha().GetBestHand(myCards, board);
                                    var opponentHand = new Omaha().GetBestHand(opponentCards, board);

                                    var result = myHand.CompareTo(opponentHand);

                                    if (result == -1)
                                    {
                                        opponentWins += 2;
                                    }
                                    if (result == 0)
                                    {
                                        myWins += 1;
                                        opponentWins += 1;
                                    }
                                    if (result == 1)
                                    {
                                        myWins += 2;
                                    }
                                }                                
                            }
                        }
                    }
                }

                Console.WriteLine("My hand: ");
                myCards.ForEach(card => Console.WriteLine(card.ToString()));

                Console.WriteLine("");

                Console.WriteLine("Opponent hand: ");
                opponentCards.ForEach(card => Console.WriteLine(card.ToString()));

                Console.WriteLine("");
                Console.WriteLine("Results: ");

                Console.WriteLine(((float)myWins * 100.0) / (float)(myWins + opponentWins));
            }

            sw.Stop();
            Console.WriteLine("Elapsed time: " + (double)sw.ElapsedMilliseconds/1000.0 + " seconds");
        }
    }
}
