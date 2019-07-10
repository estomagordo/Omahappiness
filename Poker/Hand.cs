using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    public class Hand
    {
        private List<Card> _cards;
        private Dictionary<int, int> _rankCounts;

        public Hand(Card card0, Card card1, Card card2, Card card3, Card card4)
        {
            _cards = new List<Card>() { card0, card1, card2, card3, card4 };
            CountRanks();
        }

        private void CountRanks()
        {
            var counts = new Dictionary<int, int>();

            _cards.ForEach(card =>
            {
                if (!counts.ContainsKey(card.Rank.Value))
                {
                    counts.Add(card.Rank.Value, 0);
                }

                counts[card.Rank.Value]++;
            });

            _rankCounts = counts;
        }

        private bool IsOnePair()
        {
            return _rankCounts.Values.Count(value => value == 2) == 1;
        }

        private bool IsTwoPair()
        {
            return _rankCounts.Values.Count(value => value == 2) == 2;
        }

        private bool IsTrips()
        {
            return _rankCounts.Values.Count(value => value == 3) == 1;
        }

        private bool IsStraight()
        {
            if (_rankCounts.Values.Any(value => value > 1))
            {
                return false;
            }

            var sortedCards = _cards.OrderBy(card => card.Rank).ToList();

            if (sortedCards[4].Rank.Value - sortedCards[0].Rank.Value == 4)
            {
                return true;
            }

            return sortedCards[4].Rank.Value == 13 && sortedCards[3].Rank.Value == 4;
        }

        private bool IsFlush()
        {
            return _cards.Select(card => card.Suit).Distinct().Count() == 1;
        }

        private bool IsFullHouse()
        {
            return IsOnePair() & IsTrips();
        }

        private bool IsQuads()
        {
            return _rankCounts.Values.Count(value => value == 4) == 1;
        }

        private bool IsStraightFlush()
        {
            return IsFlush() && IsStraight();
        }
    }
}
