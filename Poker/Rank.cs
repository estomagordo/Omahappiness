using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    public class Rank
    {
        public int Value { private set; get; }

        public Rank(int rawValue)
        {
            if (rawValue < 0 || rawValue > 12)
            {
                throw new ArgumentException("Legal values are between 0 and 12 (inclusive).");
            }

            Value = rawValue;
        }

        public override string ToString()
        {
            if (Value > 0 && Value < 9)
            {
                return (Value + 1).ToString();
            }

            if (Value == 9)
            {
                return "T";
            }

            if (Value == 10)
            {
                return "J";
            }

            if (Value == 11)
            {
                return "Q";
            }

            if (Value == 12)
            {
                return "K";
            }

            return "A";
        }
    }
}
