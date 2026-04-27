using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    class Program
    {
        static Student[] GetData() => new Student[]
        {
            new Student("Іваненко",  "Олег",    3456789012u, "Київ"),
            new Student("Петренко",  "Анна",    1234567890u, "Львів"),
            new Student("Сидоренко", "Марко",   3876543210u, "Одеса"),
            new Student("Коваль",    "Тетяна",  3567890123u, "Харків"),
            new Student("Мороз",     "Дмитро",  2345678901u, "Дніпро"),
            new Student("Бондар",    "Ірина",   3890123456u, "Запоріжжя"),
            new Student("Лисенко",   "Сергій",  2789012345u, "Миколаїв"),
            new Student("Шевченко",  "Олена",   1678901234u, "Вінниця"),
        };

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //  РІВЕНЬ 1
            Console.WriteLine("=== РІВЕНЬ 1: Двоспрямований бульбашковий (масив) ===\n");

            Student[] arr1 = GetData();
            Console.WriteLine("До сортування:");
            TablePrinter.Array(arr1);

            CocktailSorter.Sort(arr1);

            Console.WriteLine("\nПісля сортування (за спаданням ід. коду):");
            TablePrinter.Array(arr1);

            //  РІВЕНЬ 2
            Console.WriteLine("\n=== РІВЕНЬ 2: Двоспрямований бульбашковий (DLL) ===\n");

            var list = new DoublyLinkedList();
            foreach (var s in GetData()) list.Add(s);

            Console.WriteLine("До сортування:");
            list.PrintList();   

            list.CocktailSort();

            Console.WriteLine("\nПісля сортування (за спаданням ід. коду):");
            list.PrintList();   

            //  РІВЕНЬ 3
            Console.WriteLine("\n=== РІВЕНЬ 3: Висхідне злиття (масив) ===\n");

            Student[] arr3 = GetData();
            Console.WriteLine("До сортування:");
            TablePrinter.Array(arr3);

            MergeSorter.Sort(arr3);

            Console.WriteLine("\nПісля сортування (за спаданням ід. коду):");
            TablePrinter.Array(arr3);

            Console.WriteLine("\nГотово! Натисніть Enter...");
            Console.ReadLine();
        }
    }
}
