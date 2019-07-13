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
        private int _pairCount = -1;
        private int _tripCount = -1;

        private int PairCount
        {
            get
            {
                if (_pairCount == -1)
                {
                    _pairCount = RankCounts.Values.Count(value => value == 2);
                }

                return _pairCount;
            }
        }

        private int TripCount
        {
            get
            {
                if (_tripCount == -1)
                {
                    _tripCount = RankCounts.Values.Count(value => value == 3);
                }

                return _tripCount;
            }
        }

        private List<int> OrderedKickers
        {
            get
            {
                if (_orderedKickers == null)
                {
                    _orderedKickers = RankCounts.Where(pair => pair.Value == 1).Select(pair => pair.Key).OrderBy(k => k).ToList();
                }

                return _orderedKickers;
            }
        }

        private Dictionary<int, int> RankCounts
        {
            get
            {
                if (_rankCounts == null)
                {
                    _rankCounts = new Dictionary<int, int>();

                    _cards.ForEach(card =>
                    {
                        if (!_rankCounts.ContainsKey(card.Rank.Value))
                        {
                            _rankCounts.Add(card.Rank.Value, 0);
                        }

                        _rankCounts[card.Rank.Value]++;
                    });
                }

                return _rankCounts;
            }
        }

        public List<Card> CardsSortedByRank
        {
            get
            {
                if (_cardsSortedByRank == null)
                {
                    _cardsSortedByRank = _cards.OrderBy(card => card.Rank.Value).ToList();
                }

                return _cardsSortedByRank;
            }
        }

        public Hand(Card card0, Card card1, Card card2, Card card3, Card card4)
        {
            _cards = new List<Card>() { card0, card1, card2, card3, card4 };                       
        }

        private bool IsOnePair()
        {
            return PairCount == 1 && TripCount == 0;
        }

        private bool IsTwoPair()
        {
            return PairCount == 2;
        }

        private bool IsTrips()
        {
            return TripCount == 1 && PairCount == 0;
        }

        private bool IsStraight()
        {
            if (RankCounts.Values.Any(value => value > 1))
            {
                return false;
            }

            if (CardsSortedByRank[4].Rank.Value - CardsSortedByRank[0].Rank.Value == 4)
            {
                return true;
            }

            return CardsSortedByRank[4].Rank.Value == 13 && CardsSortedByRank[3].Rank.Value == 4;
        }

        private bool IsFlush()
        {
            return _cards.Select(card => card.Suit).Distinct().Count() == 1;
        }

        private bool IsFullHouse()
        {
            return PairCount == 1 && TripCount == 1;
        }

        private bool IsQuads()
        {
            return RankCounts.Values.Count(value => value == 4) == 1;
        }

        private bool IsStraightFlush()
        {
            return IsFlush() && IsStraight();
        }

        private int HandType()
        {
            if (IsOnePair())
            {
                return 1;
            }

            if (IsTwoPair())
            {
                return 2;
            }

            if (IsTrips())
            {
                return 3;
            }

            if (IsStraight())
            {
                return 4;
            }

            if (IsFlush())
            {
                return 5;
            }

            if (IsFullHouse())
            {
                return 6;
            }

            if (IsQuads())
            {
                return 7;
            }

            if (IsStraightFlush())
            {
                return 8;
            }

            return 0;
        }

        private int CompareStraights(Hand other)
        {
            if (CardsSortedByRank[4].Rank.Value == 13)
            {
                if (other.CardsSortedByRank[4].Rank.Value == 13)
                {
                    if (CardsSortedByRank[3].Rank.Value == 12)
                    {
                        if (other.CardsSortedByRank[3].Rank.Value == 12)
                        {
                            return 0;
                        }

                        return 1;
                    }
                }

                if (CardsSortedByRank[0].Rank.Value == 1)
                {
                    return -1;
                }

                return 1;
            }

            if (other.CardsSortedByRank[4].Rank.Value == 13)
            {
                if (other.CardsSortedByRank[0].Rank.Value == 1)
                {
                    return 1;
                }

                return -1;
            }

            return CardsSortedByRank[0].Rank.Value.CompareTo(other.CardsSortedByRank[0].Rank.Value);
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
                var thisQuadRank = RankCounts.Where(pair => pair.Value == 4).Select(pair => pair.Key).First();
                var thisKickerRank = RankCounts.Where(pair => pair.Value == 1).Select(pair => pair.Key).First();
                var otherQuadRank = other.RankCounts.Where(pair => pair.Value == 4).Select(pair => pair.Key).First();
                var otherKickerRank = other.RankCounts.Where(pair => pair.Value == 1).Select(pair => pair.Key).First();

                if (thisQuadRank == otherQuadRank)
                {
                    return thisKickerRank.CompareTo(otherKickerRank);
                }

                return thisQuadRank.CompareTo(otherQuadRank);
            }

            if (IsFullHouse())
            {
                var thisTripRank = RankCounts.Where(pair => pair.Value == 3).Select(pair => pair.Key).First();
                var thisPairRank = RankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).First();
                var otherTripRank = other.RankCounts.Where(pair => pair.Value == 3).Select(pair => pair.Key).First();
                var otherPairRank = other.RankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).First();

                if (thisTripRank == otherTripRank)
                {
                    return thisPairRank.CompareTo(otherPairRank);
                }

                return thisTripRank.CompareTo(otherTripRank);
            }

            if (IsFlush())
            {
                for (var i = CardsSortedByRank.Count - 1; i >= 0; i--)
                {
                    if (CardsSortedByRank[i].Rank.Value != other.CardsSortedByRank[i].Rank.Value)
                    {
                        return CardsSortedByRank[i].Rank.Value.CompareTo(other.CardsSortedByRank[i].Rank.Value);
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
                var thisTripRank = RankCounts.Where(pair => pair.Value == 3).Select(pair => pair.Key).First();
                var otherTripRank = other.RankCounts.Where(pair => pair.Value == 3).Select(pair => pair.Key).First();

                if (thisTripRank != otherTripRank)
                {
                    return thisTripRank.CompareTo(otherTripRank);
                }

                if (OrderedKickers[1] != other.OrderedKickers[1])
                {
                    return OrderedKickers[1].CompareTo(other.OrderedKickers[1]);
                }

                return OrderedKickers[0].CompareTo(other.OrderedKickers[0]);
            }

            if (IsTwoPair())
            {
                var thisPairs = RankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).OrderBy(k => k).ToList();
                var otherPairs = other.RankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).OrderBy(k => k).ToList();

                if (thisPairs[1] != otherPairs[1])
                {
                    return thisPairs[1].CompareTo(otherPairs[1]);
                }

                if (thisPairs[0] != otherPairs[0])
                {
                    return thisPairs[0].CompareTo(otherPairs[0]);
                }

                return OrderedKickers[0].CompareTo(other.OrderedKickers[0]);
            }

            if (IsOnePair())
            {
                var thisPairRank = RankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).First();
                var otherPairRank = other.RankCounts.Where(pair => pair.Value == 2).Select(pair => pair.Key).First();

                if (thisPairRank != otherPairRank)
                {
                    return thisPairRank.CompareTo(otherPairRank);
                }

                if (OrderedKickers[2] != other.OrderedKickers[2])
                {
                    return OrderedKickers[2].CompareTo(other.OrderedKickers[2]);
                }

                if (OrderedKickers[1] != other.OrderedKickers[1])
                {
                    return OrderedKickers[1].CompareTo(other.OrderedKickers[1]);
                }

                return OrderedKickers[0].CompareTo(other.OrderedKickers[0]);
            }

            if (OrderedKickers[4] != other.OrderedKickers[4])
            {
                return OrderedKickers[4].CompareTo(other.OrderedKickers[4]);
            }

            if (OrderedKickers[3] != other.OrderedKickers[3])
            {
                return OrderedKickers[3].CompareTo(other.OrderedKickers[3]);
            }

            if (OrderedKickers[2] != other.OrderedKickers[2])
            {
                return OrderedKickers[2].CompareTo(other.OrderedKickers[2]);
            }

            if (OrderedKickers[1] != other.OrderedKickers[1])
            {
                return OrderedKickers[1].CompareTo(other.OrderedKickers[1]);
            }

            return OrderedKickers[0].CompareTo(other.OrderedKickers[0]);
        }
    }
}
