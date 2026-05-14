using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    static class NaturalMergeSort
    {
        public static void Sort(int[] arr)
        {
            int n = arr.Length;
            if (n <= 1) return;

            bool sorted = false;
            while (!sorted)
            {
                sorted = true;
                int i = 0;

                while (i < n)
                {
                    int runEnd1 = FindRunEnd(arr, i, n);
                    if (runEnd1 >= n - 1) break; 

                    int runEnd2 = FindRunEnd(arr, runEnd1 + 1, n);

                    Merge(arr, i, runEnd1, runEnd2);
                    sorted = false;  

                    i = runEnd2 + 1;
                }
            }
        }
        static int FindRunEnd(int[] arr, int start, int n)
        {
            int i = start;
            while (i + 1 < n && arr[i] <= arr[i + 1])
                i++;
            return i;
        }
        static void Merge(int[] arr, int left, int mid, int right)
        {
            int[] temp = new int[right - left + 1];
            int i = left, j = mid + 1, k = 0;

            while (i <= mid && j <= right)
            {
                if (arr[i] <= arr[j]) temp[k++] = arr[i++];
                else temp[k++] = arr[j++];
            }
            while (i <= mid) temp[k++] = arr[i++];
            while (j <= right) temp[k++] = arr[j++];

            for (int x = 0; x < temp.Length; x++)
                arr[left + x] = temp[x];
        }
    }
}
