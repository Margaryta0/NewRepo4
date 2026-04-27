using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    static class CocktailSorter
    {
        public static void Sort(Student[] arr)
        {
            int left = 0;
            int right = arr.Length - 1;
            bool swapped;

            while (left < right)
            {
                swapped = false;

                //зліва направо
                // Найменший елемент у поточному відрізку пливе
                // до правого краю
                for (int i = left; i < right; i++)
                {
                    if (arr[i].TaxCode < arr[i + 1].TaxCode)
                    {
                        Swap(arr, i, i + 1);
                        swapped = true;
                    }
                }
                right--;                   // правий кінець зафіксовано

                if (!swapped) break;
                swapped = false;

                // Найбільший елемент у поточному відрізку пливе до лівого краю
                for (int i = right; i > left; i--)
                {
                    if (arr[i].TaxCode > arr[i - 1].TaxCode)
                    {
                        Swap(arr, i, i - 1);
                        swapped = true;
                    }
                }
                left++;                    // лівий кінець зафіксовано

                if (!swapped) break;
            }
        }

        static void Swap(Student[] arr, int i, int j)
        {
            Student t = arr[i]; arr[i] = arr[j]; arr[j] = t;
        }
    }
}
