using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    public class Omaha : IRule
    {
        public Hand GetBestHand(IList<Card> holeCards, IList<Card> board)
        {
            Hand best = new Hand(holeCards[0], holeCards[1], board[0], board[1], board[2]);

            for (var firstHole = 0; firstHole < 3; firstHole++)
            {
                for (var secondHole = firstHole + 1; secondHole < 4; secondHole++)
                {
                    for (var firstBoard = 0; firstBoard < 3; firstBoard++)
                    {
                        for (var secondBoard = firstBoard + 1; secondBoard < 4; secondBoard++)
                        {
                            for (var thirdBoard = secondBoard + 1; thirdBoard < 5; thirdBoard++)
                            {
                                var hand = new Hand(holeCards[firstHole], holeCards[secondHole], board[firstBoard], board[secondBoard], board[thirdBoard]);
                                if (hand > best)
                                {
                                    best = hand;
                                }
                            }
                        }
                    }
                }
            }

            return best;
        }
    }
}
