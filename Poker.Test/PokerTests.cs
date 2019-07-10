using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Poker.Test
{
    [TestClass]
    public class PokerTests
    {
        private Hand _royalFlush;
        private Hand _middlingStraightFlush;
        private Hand _wheelStraightFlush;
        private Hand _aceQuadsK;
        private Hand _aceQuadsJ;
        private Hand _middlingQuads;
        private Hand _aceKingBoat;
        private Hand _aceDeuceBoat;
        private Hand _kingAceBoat;        
        private Hand _nuttyFlush;
        private Hand _aceHighFlush;
        private Hand _kingHighFlush;
        private Hand _babyFlush;
        private Hand _broadwayStraight;
        private Hand _middlingStraight;
        private Hand _wheelStraight;
        private Hand _aceKingQueenTrips;
        private Hand _eightSevenSeuceTrips;
        private Hand _eightSixFiveTrips;
        private Hand _queenTenFourTwoPairs;
        private Hand _queenTenThreeTwoPairs;
        private Hand _kingSevenFourThreePair;
        private Hand _queenAceKingJackPair;
        private Hand _royalSampler;
        private Hand _nutLow;

        private List<Hand> _allHands;

        [TestInitialize]
        public void Setup()
        {
            _allHands = new List<Hand>();

            _royalFlush = new Hand
                (
                    new Card(0),
                    new Card(9),
                    new Card(10),
                    new Card(11),
                    new Card(12)
                );
            _allHands.Add(_royalFlush);

            _middlingStraightFlush = new Hand
                (
                    new Card(17),
                    new Card(18),
                    new Card(19),
                    new Card(20),
                    new Card(21)
                );
            _allHands.Add(_middlingStraightFlush);

            _wheelStraightFlush = new Hand
                (
                    new Card(27),
                    new Card(28),
                    new Card(29),
                    new Card(30),
                    new Card(31)
                );
            _allHands.Add(_wheelStraightFlush);

            _aceQuadsK = new Hand
                (
                    new Card(0),
                    new Card(13),
                    new Card(26),
                    new Card(39),
                    new Card(12)
                );
            _allHands.Add(_aceQuadsK);

            _aceQuadsJ = new Hand
                (
                    new Card(0),
                    new Card(13),
                    new Card(26),
                    new Card(39),
                    new Card(23)
                );
            _allHands.Add(_aceQuadsJ);

            _middlingQuads = new Hand
                (
                    new Card(4),
                    new Card(17),
                    new Card(30),
                    new Card(43),
                    new Card(15)
                );
            _allHands.Add(_middlingQuads);

            _aceKingBoat = new Hand
                (
                    new Card(0),
                    new Card(26),
                    new Card(39),
                    new Card(38),
                    new Card(51)
                );
            _allHands.Add(_aceKingBoat);

            _aceDeuceBoat = new Hand
                (
                    new Card(0),
                    new Card(13),
                    new Card(26),
                    new Card(1),
                    new Card(40)
                );
            _allHands.Add(_aceDeuceBoat);

            _kingAceBoat = new Hand
                (
                    new Card(0),
                    new Card(26),
                    new Card(12),
                    new Card(38),
                    new Card(51)
                );
            _allHands.Add(_kingAceBoat);            

            _nuttyFlush = new Hand
                (
                    new Card(0),
                    new Card(8),
                    new Card(10),
                    new Card(11),
                    new Card(12)
                );
            _allHands.Add(_nuttyFlush);

            _aceHighFlush = new Hand
                (
                    new Card(0),
                    new Card(4),
                    new Card(6),
                    new Card(7),
                    new Card(11)
                );
            _allHands.Add(_aceHighFlush);

            _kingHighFlush = new Hand
                (
                    new Card(3),
                    new Card(4),
                    new Card(7),
                    new Card(9),
                    new Card(12)
                );
            _allHands.Add(_kingHighFlush);

            _babyFlush = new Hand
                (
                    new Card(1),
                    new Card(2),
                    new Card(3),
                    new Card(4),
                    new Card(6)
                );
            _allHands.Add(_babyFlush);

            _broadwayStraight = new Hand
                (
                    new Card(0),
                    new Card(9),
                    new Card(10),
                    new Card(11),
                    new Card(38)
                );
            _allHands.Add(_broadwayStraight);

            _middlingStraight = new Hand
                (
                    new Card(31),
                    new Card(45),
                    new Card(46),
                    new Card(47),
                    new Card(48)
                );
            _allHands.Add(_middlingStraight);

            _wheelStraight = new Hand
                (
                    new Card(14),
                    new Card(15),
                    new Card(16),
                    new Card(30),
                    new Card(44)
                );
            _allHands.Add(_wheelStraight);

            _aceKingQueenTrips = new Hand
                (
                    new Card(0),
                    new Card(13),
                    new Card(39),
                    new Card(50),
                    new Card(51)
                );
            _allHands.Add(_aceKingQueenTrips);

            _eightSevenSeuceTrips = new Hand
                (
                    new Card(7),
                    new Card(20),
                    new Card(33),
                    new Card(40),
                    new Card(45)
                );
            _allHands.Add(_eightSevenSeuceTrips);

            _eightSixFiveTrips = new Hand
                (
                    new Card(7),
                    new Card(20),
                    new Card(33),
                    new Card(43),
                    new Card(44)
                );
            _allHands.Add(_eightSixFiveTrips);

            _queenTenFourTwoPairs = new Hand
                (
                    new Card(11),
                    new Card(35),
                    new Card(37),
                    new Card(42),
                    new Card(48)
                );
            _allHands.Add(_queenTenFourTwoPairs);

            _queenTenThreeTwoPairs = new Hand
                (
                    new Card(11),
                    new Card(35),
                    new Card(37),
                    new Card(41),
                    new Card(48)
                );
            _allHands.Add(_queenTenThreeTwoPairs);

            _kingSevenFourThreePair = new Hand
                (
                    new Card(12),
                    new Card(19),
                    new Card(29),
                    new Card(38),
                    new Card(41)
                );
            _allHands.Add(_kingSevenFourThreePair);

            _queenAceKingJackPair = new Hand
                (
                    new Card(0),
                    new Card(11),
                    new Card(24),
                    new Card(38),
                    new Card(49)
                );
            _allHands.Add(_queenAceKingJackPair);

            _royalSampler = new Hand
                (
                    new Card(0),
                    new Card(9),
                    new Card(25),
                    new Card(29),
                    new Card(40)
                );
            _allHands.Add(_royalSampler);

            _nutLow = new Hand
                (
                    new Card(1),
                    new Card(2),
                    new Card(3),
                    new Card(4),
                    new Card(19)
                );
            _allHands.Add(_nutLow);
        }

        [TestMethod]
        public void HandsOrderedCorrectly()
        {
            for (var i = 0; i < _allHands.Count - 1; i++)
            {
                for (var j = i + 1; j < _allHands.Count; j++)
                {
                    Assert.IsTrue(_allHands[i] > _allHands[j]);
                }
            }
        }

        [TestMethod]
        public void HandsOfEqualWorthTie()
        {
            var otherRoyalFlush = new Hand
            (
                new Card(26),
                new Card(35),
                new Card(36),
                new Card(37),
                new Card(38)
            );

            var otherAceKingBoat = new Hand
            (
                new Card(13),
                new Card(26),
                new Card(39),
                new Card(38),
                new Card(25)
            );

            var otherRoyalSampler = new Hand
            (
                new Card(26),
                new Card(9),
                new Card(12),
                new Card(29),
                new Card(14)
            );

            var royalStraightComparison = _royalFlush.CompareTo(otherRoyalFlush);
            var aceKingBoatComparison = _aceKingBoat.CompareTo(otherAceKingBoat);
            var royalSamplerComparison = _royalSampler.CompareTo(otherRoyalSampler);

            Assert.AreEqual(0, royalStraightComparison);
            Assert.AreEqual(0, aceKingBoatComparison);
            Assert.AreEqual(0, royalSamplerComparison);
        }

        [TestMethod]
        public void OmahaRoyal()
        {
            var board = new List<Card>()
            {
                new Card(14),
                new Card(22),
                new Card(39),
                new Card(49),
                new Card(50)
            };

            var holeCards = new List<Card>()
            {
                new Card(4),
                new Card(21),
                new Card(48),
                new Card(51)
            };

            var royalComparison = _royalFlush.CompareTo((new Omaha()).GetBestHand(holeCards, board));

            Assert.AreEqual(0, royalComparison);
        }

        [TestMethod]
        public void NotOmahaOneCardRoyal()
        {
            var board = new List<Card>()
            {
                new Card(48),
                new Card(22),
                new Card(39),
                new Card(49),
                new Card(50)
            };

            var holeCards = new List<Card>()
            {
                new Card(4),
                new Card(14),
                new Card(21),
                new Card(51)
            };

            var royalComparison = _royalFlush.CompareTo((new Omaha()).GetBestHand(holeCards, board));

            Assert.AreEqual(1, royalComparison);
        }

        [TestMethod]
        public void NotOmahaRoyalBoard()
        {
            var board = new List<Card>()
            {
                new Card(39),
                new Card(48),
                new Card(49),
                new Card(50),
                new Card(51)
            };

            var holeCards = new List<Card>()
            {
                new Card(4),
                new Card(14),
                new Card(21),
                new Card(22)                
            };

            var royalComparison = _royalFlush.CompareTo((new Omaha()).GetBestHand(holeCards, board));

            Assert.AreEqual(1, royalComparison);
        }

        [TestMethod]
        public void OmahaKingHighFlush()
        {
            var board = new List<Card>()
            {
                new Card(2),
                new Card(3),
                new Card(4),
                new Card(9),
                new Card(25)
            };

            var holeCards = new List<Card>()
            {
                new Card(7),
                new Card(12),
                new Card(21),
                new Card(44)
            };

            var kingHighFlushComparison = _kingHighFlush.CompareTo((new Omaha()).GetBestHand(holeCards, board));

            Assert.AreEqual(0, kingHighFlushComparison);
        }
    }
}
