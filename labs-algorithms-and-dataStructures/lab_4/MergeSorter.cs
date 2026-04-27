using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    static class MergeSorter
    {
        public static void Sort(Student[] arr)
        {
            int n = arr.Length;
            Student[] temp = new Student[n];   // буфер злиття

            for (int size = 1; size < n; size *= 2)
            {
                for (int left = 0; left < n - size; left += 2 * size)
                {
                    int mid = left + size - 1;
                    int right = Math.Min(left + 2 * size - 1, n - 1);
                    Merge(arr, temp, left, mid, right);
                }
            }
        }

        static void Merge(Student[] arr, Student[] temp, int left, int mid, int right)
        {
            // 1. Копіюємо відрізок у буфер
            for (int k = left; k <= right; k++) temp[k] = arr[k];

            int i = left;      // покажчик у лівому підмасиві
            int j = mid + 1;   // покажчик у правому підмасиві

            // 2. Зливаємо: беремо більший з двох голів (спадання)
            for (int k = left; k <= right; k++)
            {
                if (i > mid) arr[k] = temp[j++];
                else if (j > right) arr[k] = temp[i++];
                else if (temp[i].TaxCode >= temp[j].TaxCode) arr[k] = temp[i++];
                else arr[k] = temp[j++];
            }
        }
    }
}
