using System;
using System.Collections.Generic;

namespace Extensions
{
    public static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rand, IList<T> coll)
        {
            var n = coll.Count;

            for (var i = 0; i < n; i++)
            {
                var r = i + rand.Next(n - i);
                T t = coll[r];
                coll[r] = coll[i];
                coll[i] = t;
            }
        }
    }
}
