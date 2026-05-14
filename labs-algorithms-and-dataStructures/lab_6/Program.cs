using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_6
{
    class Program
    {
        static void PrintHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"  {title}");
            Console.WriteLine(new string('═', 60));
        }

        static void PrintTableHeader()
        {
            Console.WriteLine($"\n{"N",10} | {"Двоколійне (нс)",18} | {"Обмінне (нс)",15}");
            Console.WriteLine(new string('-', 50));
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int N = 100;
            int[] sizes = { N, N * N, N * N * N };  // 100, 10000, 1000000

            //  РІВЕНЬ 1: Двоколійне злиття для N, N², N³
            PrintHeader("РІВЕНЬ 1: Двоколійне злиття — час для N, N², N³");
            Console.WriteLine($"\n{"N",10} | {"Час (нс)",15} | {"Час (мс)",12}");
            Console.WriteLine(new string('-', 42));

            foreach (int n in sizes)
            {
                var arr = ArrayGen.Random(n);
                long time = Timer.Measure(arr, a => TwoWayMergeSort.Sort(a));
                Console.WriteLine($"{n,10} | {time,15:N0} | {time/1_000_000.0,12:F3}");
            }

            Console.WriteLine("N\tЧас(нс)");
            foreach (int n in sizes)
            {
                var arr = ArrayGen.Random(n);
                long time = Timer.Measure(arr, a => TwoWayMergeSort.Sort(a));
                Console.WriteLine($"{n}\t{time}");
            }

            //  РІВЕНЬ 2: Двоколійне vs Обмінне злиття
            PrintHeader("РІВЕНЬ 2: Двоколійне vs Обмінне злиття");
            PrintTableHeader();

            foreach (int n in sizes)
            {
                // однакові вхідні дані для обох алгоритмів
                var arr = ArrayGen.Random(n);
                long timeTW = Timer.Measure(arr, a => TwoWayMergeSort.Sort(a));
                long timeNM = Timer.Measure(arr, a => NaturalMergeSort.Sort(a));
                Console.WriteLine($"{n,10} | {timeTW,18:N0} | {timeNM,15:N0}");
            }

            Console.WriteLine("N\tДвоколійне(нс)\tОбмінне(нс)");
            foreach (int n in sizes)
            {
                var arr = ArrayGen.Random(n);
                long timeTW = Timer.Measure(arr, a => TwoWayMergeSort.Sort(a));
                long timeNM = Timer.Measure(arr, a => NaturalMergeSort.Sort(a));
                Console.WriteLine($"{n}\t{timeTW}\t{timeNM}");
            }

            //  РІВЕНЬ 3: Найкращий / Найгірший / Середній випадок
            //  Масив розміром 10000 елементів
            PrintHeader("РІВЕНЬ 3: Вплив структури даних (N=10000)");

            int bigN = 10000;
            string[] caseNames = { "Відсортований (найкращий)", "Зворотній (найгірший)", "Випадковий (середній)", "Частково відс." };
            int[][] testArrays = {
                ArrayGen.Sorted(bigN),
                ArrayGen.ReverseSorted(bigN),
                ArrayGen.Random(bigN),
                ArrayGen.PartiallySorted(bigN),
            };

            Console.WriteLine($"\n{"Випадок",-28} | {"Двоколійне (нс)",18} | {"Обмінне (нс)",15}");
            Console.WriteLine(new string('-', 68));

            Console.WriteLine("Випадок\tДвоколійне(нс)\tОбмінне(нс)");

            for (int i = 0; i < caseNames.Length; i++)
            {
                long timeTW = Timer.Measure(testArrays[i], a => TwoWayMergeSort.Sort(a));
                long timeNM = Timer.Measure(testArrays[i], a => NaturalMergeSort.Sort(a));
                Console.WriteLine($"{caseNames[i],-28} | {timeTW,18:N0} | {timeNM,15:N0}");
            }

            Console.WriteLine();
            for (int i = 0; i < caseNames.Length; i++)
            {
                long timeTW = Timer.Measure(testArrays[i], a => TwoWayMergeSort.Sort(a));
                long timeNM = Timer.Measure(testArrays[i], a => NaturalMergeSort.Sort(a));
                Console.WriteLine($"{caseNames[i]}\t{timeTW}\t{timeNM}");
            }
            Console.ReadLine();
        }
    }
}
