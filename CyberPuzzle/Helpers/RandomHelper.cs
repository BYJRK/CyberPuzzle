using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberPuzzle.Helpers
{
    static class RandomHelper
    {
        private static Random rnd;
        public static Random Rnd
        {
            get
            {
                if (rnd == null)
                    rnd = new Random(0);
                return rnd;
            }
            set => rnd = value;
        }

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

        /// <summary>
        /// randomly select one element inside the list
        /// </summary>
        public static T Choice<T>(IEnumerable<T> list)
        {
            return list.ElementAt(Rnd.Next(list.Count()));
        }

        public static int NextInteger(int maxValue) => Rnd.Next(maxValue);
        public static int NextInteger(int minValue, int maxValue) => Rnd.Next(minValue, maxValue + 1);

        #endregion
    }
}
