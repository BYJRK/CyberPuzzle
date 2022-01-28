using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPuzzle.Helpers
{
    static class RandomHelper
    {
        public static Random Rnd { get; private set; }

        /// <summary>
        /// initialize the random generator with a given seed
        /// </summary>
        /// <param name="seed"></param>
        public static void Init(int? seed = null)
        {
            if (seed.HasValue)
                Rnd = new Random(seed.Value);
            else
                Rnd = new Random();
        }

        #region Helper functions

        /// <summary>
        /// randomly sample given count elements
        /// </summary>
        public static IEnumerable<T> Sample<T>(IEnumerable<T> list, int count)
        {
            var copy = list.ToList();
            Shuffle(copy);
            return copy.Take(count);
        }

        /// <summary>
        /// shuffle a list using Fisher–Yates algorithm
        /// </summary>
        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        #endregion
    }
}
