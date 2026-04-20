using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_3
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            //  РІВЕНЬ 1 — Побудова дерева та виведення паралельним обходом
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║       РІВЕНЬ 1 — Побудова дерева     ║");
            Console.WriteLine("╚══════════════════════════════════════╝\n");

            var tree = new BinaryTree();

            // Додаємо студентів (різні курси та мови для демонстрації)
            tree.Add(new Student("Іваненко", "Олег", 2, "КН-21", 1005, "Англійська"));
            tree.Add(new Student("Петренко", "Анна", 3, "КН-31", 1002, "Англійська"));
            tree.Add(new Student("Сидоренко", "Марко", 2, "КН-22", 1008, "Німецька"));
            tree.Add(new Student("Коваль", "Тетяна", 1, "КН-11", 1001, "Англійська"));
            tree.Add(new Student("Мороз", "Дмитро", 2, "КН-21", 1007, "Французька"));
            tree.Add(new Student("Бондар", "Ірина", 2, "КН-22", 1004, "Англійська"));
            tree.Add(new Student("Лисенко", "Сергій", 4, "КН-41", 1010, "Англійська"));
            tree.Add(new Student("Шевченко", "Олена", 2, "КН-21", 1003, "Англійська"));
            tree.Add(new Student("Гриценко", "Павло", 3, "КН-32", 1006, "Іспанська"));
            tree.Add(new Student("Тимченко", "Юлія", 2, "КН-22", 1009, "Англійська"));

            Console.WriteLine("Вміст дерева (паралельний обхід — по рівнях):");
            tree.PrintParallel();

            //  РІВЕНЬ 2 — Пошук студентів 2-го курсу з англійською мовою
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║       РІВЕНЬ 2 — Пошук вузлів        ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("Критерій: Студенти 2-го курсу, мова — Англійська\n");

            var found = tree.Search(2, "Англійська");

            if (found.Count > 0)
            {
                Console.WriteLine($"Знайдено {found.Count} запис(ів):");
                Console.WriteLine(new string('─', 72));
                Console.WriteLine($"{"ID",8} | {"Прізвище",-12} | {"Ім'я",-10} | " +
                                  $"{"Курс",5} | {"Група",-6} | Мова");
                Console.WriteLine(new string('─', 72));
                foreach (var s in found) Console.WriteLine(s);
                Console.WriteLine(new string('─', 72));
            }
            else
            {
                Console.WriteLine("Студентів, що відповідають критерію, не знайдено.");
            }

            //  РІВЕНЬ 3 — Видалення + виведення дерева до/після
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║      РІВЕНЬ 3 — Видалення вузлів     ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("Видаляємо: студентів 2-го курсу з англійською мовою\n");

            Console.WriteLine("Дерево ДО видалення (паралельний обхід):");
            tree.PrintParallel();

            tree.Delete(2, "Англійська");

            Console.WriteLine("\nДерево ПІСЛЯ видалення (паралельний обхід):");
            tree.PrintParallel();

            Console.WriteLine("\nГотово! Натисніть Enter для виходу...");
            Console.ReadLine();
        }
    }
}
