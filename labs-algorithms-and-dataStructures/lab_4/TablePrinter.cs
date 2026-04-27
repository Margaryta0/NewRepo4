using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_4
{
    static class TablePrinter
    {
        const int W = 58;

        public static void Header()
        {
            Console.WriteLine(new string('-', W));
            Console.WriteLine($"{"ІД КОД",12} | {"Прізвище",-12} | {"Ім'я",-10} | Місто");
            Console.WriteLine(new string('-', W));
        }

        public static void Footer()
            => Console.WriteLine(new string('-', W));

        public static void Array(Student[] arr)
        {
            Header();
            foreach (var s in arr) Console.WriteLine(s);
            Footer();
        }
    }
}
