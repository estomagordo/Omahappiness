using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker
{
    public class Hand : IComparable<Hand>
    {
        private List<Card> _cards;
        private List<Card> _cardsSortedByRank;
        private Dictionary<int, int> _rankCounts;
        private List<int> _orderedKickers;

        public Hand(Card card0, Card card1, Card card2, Card card3, Card card4)
        {
            _cards = new List<Card>() { card0, card1, card2, card3, card4 };
            CountRanks();
            _cardsSortedByRank = _cards.OrderBy(card => card.Rank.Value).ToList();
            _orderedKickers = _rankCounts.Where(pair => pair.Value == 1).Select(pair => pair.Key).OrderBy(k => k).ToList();
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

            if (_cardsSortedByRank[4].Rank.Value - _cardsSortedByRank[0].Rank.Value == 4)
            {
                return true;
            }

            return _cardsSortedByRank[4].Rank.Value == 13 && _cardsSortedByRank[3].Rank.Value == 4;
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

        private int HandType()
        {
            if (IsStraightFlush())
            {
                return 8;
            }

            if (IsQuads())
            {
                return 7;
            }

            if (IsFullHouse())
            {
                return 6;
            }

            if (IsFlush())
            {
                return 5;
            }

            if (IsStraight())
            {
                return 4;
            }

            if (IsTrips())
            {
                return 3;
            }

            if (IsTwoPair())
            {
                return 2;
            }

            if (IsOnePair())
            {
                return 1;
            }

            return 0;
        }

        private int CompareStraights(Hand other)
        {
            if (_cardsSortedByRank[4].Rank.Value == 13)
            {
                if (other._cardsSortedByRank[4].Rank.Value == 13)
                {
                    if (_cardsSortedByRank[3].Rank.Value == 12)
                    {
                        if (other._cardsSortedByRank[3].Rank.Value == 12)
                        {
                            return 0;
                        }

                        return 1;
                    }
                }

                if (_cardsSortedByRank[0].Rank.Value == 1)
                {
                    return -1;
                }

                return 1;
            }

            if (other._cardsSortedByRank[4].Rank.Value == 13)
            {
                if (other._cardsSortedByRank[0].Rank.Value == 1)
                {
                    return 1;
                }

                return -1;
            }

            return _cardsSortedByRank[0].Rank.Value.CompareTo(other._cardsSortedByRank[0].Rank.Value);
        }

        public static bool operator < (Hand first, Hand second)
        {
            return first.CompareTo(second) == -1;
        }

        public static bool operator > (Hand first, Hand second)
        {
            return first.CompareTo(second) == 1;
        }

        public int CompareTo(Hand other)
        {
            if (HandType() != other.HandType())
            {
                return HandType().CompareTo(other.HandType());
            }

            if (IsStraightFlush())
            {
                return CompareStraights(other);
            }

            if (IsQuads())
            {
                var thisQuadRank = _rankCounts.Where(pair => pair.Value == 4).Select(pair => pair.Key).First();
                var thisKickerRank = _rankCounts.Where(pair => pair.Value == 1).Select(pair => pair.Key).First();
                var otherQuadRank = other._rankCounts.Where(pair => pair.Value == 4).Select(pair => pair.Key).First();
                var otherKickerRank = other._rankCounts.Where(pair => pair.Value == 1).Select(pair => pair.Key).First();

                if (thisQuadRank == otherQuadRank)
                {
                    return thisKickerRank.CompareTo(otherKickerRank);
                }

                return thisQuadRank.CompareTo(otherQuadRank);
            }

            if (IsFullHouse())
            {
                var thisTripRank = _rankCounts.Where(pair => pair.Value == 3).Select(pair => pair.Key).First();
                var thisPairRank = _rankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).First();
                var otherTripRank = other._rankCounts.Where(pair => pair.Value == 3).Select(pair => pair.Key).First();
                var otherPairRank = other._rankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).First();

                if (thisTripRank == otherTripRank)
                {
                    return thisPairRank.CompareTo(otherPairRank);
                }

                return thisTripRank.CompareTo(otherTripRank);
            }

            if (IsFlush())
            {
                for (var i = _cardsSortedByRank.Count - 1; i >= 0; i--)
                {
                    if (_cardsSortedByRank[i].Rank.Value != other._cardsSortedByRank[i].Rank.Value)
                    {
                        return _cardsSortedByRank[i].Rank.Value.CompareTo(other._cardsSortedByRank[i].Rank.Value);
                    }
                }

                return 0;
            }

            if (IsStraight())
            {
                return CompareStraights(other);
            }

            if (IsTrips())
            {
                var thisTripRank = _rankCounts.Where(pair => pair.Value == 3).Select(pair => pair.Key).First();
                var otherTripRank = other._rankCounts.Where(pair => pair.Value == 3).Select(pair => pair.Key).First();

                if (thisTripRank != otherTripRank)
                {
                    return thisTripRank.CompareTo(otherTripRank);
                }

                if (_orderedKickers[1] != other._orderedKickers[1])
                {
                    return _orderedKickers[1].CompareTo(other._orderedKickers[1]);
                }

                return _orderedKickers[0].CompareTo(other._orderedKickers[0]);
            }

            if (IsTwoPair())
            {
                var thisPairs = _rankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).OrderBy(k => k).ToList();
                var otherPairs = other._rankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).OrderBy(k => k).ToList();

                if (thisPairs[1] != otherPairs[1])
                {
                    return thisPairs[1].CompareTo(otherPairs[1]);
                }

                if (thisPairs[0] != otherPairs[0])
                {
                    return thisPairs[0].CompareTo(otherPairs[0]);
                }

                return _orderedKickers[0].CompareTo(other._orderedKickers[0]);
            }

            if (IsOnePair())
            {
                var thisPairRank = _rankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).First();
                var otherPairRank = other._rankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).First();

                if (thisPairRank != otherPairRank)
                {
                    return thisPairRank.CompareTo(otherPairRank);
                }

                if (_orderedKickers[2] != other._orderedKickers[2])
                {
                    return _orderedKickers[2].CompareTo(other._orderedKickers[2]);
                }

                if (_orderedKickers[1] != other._orderedKickers[1])
                {
                    return _orderedKickers[1].CompareTo(other._orderedKickers[1]);
                }

                return _orderedKickers[0].CompareTo(other._orderedKickers[0]);
            }

            if (_orderedKickers[4] != other._orderedKickers[4])
            {
                return _orderedKickers[4].CompareTo(other._orderedKickers[4]);
            }

            if (_orderedKickers[3] != other._orderedKickers[3])
            {
                return _orderedKickers[3].CompareTo(other._orderedKickers[3]);
            }

            if (_orderedKickers[2] != other._orderedKickers[2])
            {
                return _orderedKickers[2].CompareTo(other._orderedKickers[2]);
            }

            if (_orderedKickers[1] != other._orderedKickers[1])
            {
                return _orderedKickers[1].CompareTo(other._orderedKickers[1]);
            }

            return _orderedKickers[0].CompareTo(other._orderedKickers[0]);
        }
    }
}
