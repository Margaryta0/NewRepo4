using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    static class Timer
    {
        const int REPEATS = 5;  // кількість повторень для усереднення

        public static long Measure(int[] original,
                                   Action<int[]> sortAction)
        {
            long total = 0;
            for (int r = 0; r < REPEATS; r++)
            {
                int[] copy = (int[])original.Clone();

                var sw = Stopwatch.StartNew();
                sortAction(copy);
                sw.Stop();

                total += sw.ElapsedTicks * (1_000_000_000L / Stopwatch.Frequency);
            }
            return total / REPEATS;
        }
    }
}
