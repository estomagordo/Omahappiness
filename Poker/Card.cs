using System;
using static Poker.Enums;

namespace Poker
{
    public class Card
    {
        public Rank Rank { private set; get; }
        public Suit Suit { private set; get; }
        public Card(int rawValue)
        {
            if (rawValue < 0 || rawValue > 51)
            {
                throw new ArgumentException("Legal values are between 0 and 51 (inclusive).");
            }

            Rank = new Rank(rawValue % 13);
            Suit = (Suit)(rawValue / 13);
        }
    }
}
