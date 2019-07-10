using System;
using System.Collections.Generic;
using System.Text;

namespace Poker
{
    public interface IRule
    {
        Hand GetBestHand(IList<Card> holeCards, IList<Card> board);
    }
}
