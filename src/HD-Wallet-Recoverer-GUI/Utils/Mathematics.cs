using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HD_Wallet_Recoverer_GUI.Utils;

public static class Mathematics
{
    /// <summary>
    /// EO: 2016-04-14
    /// Generator of all permutations of an array of anything.
    /// Base on Heap's Algorithm. See: https://en.wikipedia.org/wiki/Heap%27s_algorithm#cite_note-3
    /// </summary>
    public static class Permutations
    {
        /// <summary>
        /// Heap's algorithm to find all pmermutations. Non recursive, more efficient.
        /// </summary>
        /// <param name="items">Items to permute in each possible ways</param>
        /// <param name="funcExecuteAndTellIfShouldStop"></param>
        /// <returns>Return true if cancelled</returns> 
        public static bool ForAllPermutation<T>(T[] items, Func<T[], bool> funcExecuteAndTellIfShouldStop)
        {
            var countOfItem = items.Length;

            if (countOfItem <= 1)
                return funcExecuteAndTellIfShouldStop(items);

            var indexes = new int[countOfItem];

            if (funcExecuteAndTellIfShouldStop(items))
                return true;

            for (var i = 1; i < countOfItem;)
                if (indexes[i] < i)
                {
                    if ((i & 1) == 1)
                        Swap(ref items[i], ref items[indexes[i]]);
                    else
                        Swap(ref items[i], ref items[0]);

                    if (funcExecuteAndTellIfShouldStop(items))
                        return true;

                    indexes[i]++;
                    i = 1;
                }
                else
                    indexes[i++] = 0;

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void Swap<T>(ref T a, ref T b) => (a, b) = (b, a);
    }
}
