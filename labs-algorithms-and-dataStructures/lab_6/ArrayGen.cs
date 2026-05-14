using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    static class ArrayGen
    {
        static readonly Random rng = new Random(42);

        public static int[] Random(int n)
        {
            var arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = rng.Next(0, n * 10);
            return arr;
        }

        public static int[] Sorted(int n)
        {
            var arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = i;
            return arr;
        }

        public static int[] ReverseSorted(int n)
        {
            var arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = n - i;
            return arr;
        }

        public static int[] PartiallySorted(int n)
        {
            var arr = Sorted(n);
            for (int i = 0; i < n; i += 10)
            {
                int swapIdx = rng.Next(i, Math.Min(i + 10, n));
                int temp = arr[i]; arr[i] = arr[swapIdx]; arr[swapIdx] = temp;
            }
            return arr;
        }
    }
}
