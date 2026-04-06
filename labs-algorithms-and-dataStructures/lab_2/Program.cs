using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Random rng = new Random(42); // фіксоване зерно для відтворюваності

            // Генератор випадкового вектора
            // Координати від -10 до 10, але не нульовий вектор
            Vector GenerateVector()
            {
                Vector v;
                do
                {
                    double x = Math.Round(rng.NextDouble() * 20 - 10, 2);
                    double y = Math.Round(rng.NextDouble() * 20 - 10, 2);
                    v = new Vector(x, y);
                } while (!v.IsValid());
                return v;
            }


            // ════════════════════════════════════════════════
            // ЗАВДАННЯ 1 — Без колізій
            // ════════════════════════════════════════════════
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║  ЗАВДАННЯ 1 — Хеш-таблиця без колізій               ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("Метод хешування: ДІЛЕННЯ  →  hash = key % tableSize");
            Console.WriteLine("Ключ: кут вектора з віссю OY (в градусах, ціле число)");
            Console.WriteLine();

            // Розмір 11 — просте число (рекомендується для подвійного хешування)
            HashTable ht1 = new HashTable(11);

            // Генеруємо вектори так щоб не було колізій
            // (кожен має унікальний ключ % 11)
            Console.WriteLine("--- Вставка елементів (без колізій) ---");
            var usedHashes = new System.Collections.Generic.HashSet<int>();
            int inserted = 0;
            int attempts = 0;

            while (inserted < 7 && attempts < 1000)
            {
                attempts++;
                Vector v = GenerateVector();
                int h = v.Key() % 11;
                if (!usedHashes.Contains(h))
                {
                    usedHashes.Add(h);
                    ht1.Insert(v);
                    inserted++;
                }
            }

            Console.WriteLine("\nВміст хеш-таблиці після вставки:");
            ht1.Print();


            // ════════════════════════════════════════════════
            // ЗАВДАННЯ 2 — З колізіями, подвійне хешування
            // ════════════════════════════════════════════════
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║  ЗАВДАННЯ 2 — Подвійне хешування (колізії)          ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("h1(key) = key % tableSize");
            Console.WriteLine("h2(key) = 1 + key % (tableSize - 1)");
            Console.WriteLine("pos(i)  = (h1 + i * h2) % tableSize");
            Console.WriteLine();

            HashTable ht2 = new HashTable(11);

            // Спеціально створюємо вектори які дають колізії
            // Вектори з однаковим key % 11 викличуть колізію
            Console.WriteLine("--- Вставка елементів (з колізіями) ---");

            // Генеруємо 9 випадкових векторів — серед них точно будуть колізії
            Random rng2 = new Random(123);
            Vector GenVec2()
            {
                Vector v;
                do
                {
                    double x = Math.Round(rng2.NextDouble() * 20 - 10, 2);
                    double y = Math.Round(rng2.NextDouble() * 20 - 10, 2);
                    v = new Vector(x, y);
                } while (!v.IsValid());
                return v;
            }

            for (int i = 0; i < 9; i++)
            {
                ht2.Insert(GenVec2());
            }

            Console.WriteLine("\nВміст хеш-таблиці після вставки з колізіями:");
            ht2.Print();


            // ════════════════════════════════════════════════
            // ЗАВДАННЯ 3 — Видалення за критерієм
            // ════════════════════════════════════════════════
            Console.WriteLine("╔══════════════════════════════════════════════════════╗");
            Console.WriteLine("║  ЗАВДАННЯ 3 — Видалення за довжиною                 ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("Критерій: видалити вектори з довжиною, МЕНШОЮ від заданої");
            Console.WriteLine();

            // Використовуємо таблицю з завдання 2
            HashTable ht3 = new HashTable(11);
            Random rng3 = new Random(123); // той самий seed — ті самі вектори
            Vector GenVec3()
            {
                Vector v;
                do
                {
                    double x = Math.Round(rng3.NextDouble() * 20 - 10, 2);
                    double y = Math.Round(rng3.NextDouble() * 20 - 10, 2);
                    v = new Vector(x, y);
                } while (!v.IsValid());
                return v;
            }
            for (int i = 0; i < 9; i++) ht3.Insert(GenVec3());

            Console.WriteLine("\nВміст ДО видалення:");
            ht3.Print();

            // Задаємо порогову довжину
            double threshold = 8.0;
            Console.WriteLine($"--- Видаляємо вектори з довжиною < {threshold} ---");
            int deleted = ht3.DeleteByLength(threshold);
            Console.WriteLine($"  Видалено {deleted} елементів.");

            Console.WriteLine("\nВміст ПІСЛЯ видалення:");
            ht3.Print();

            Console.WriteLine("✓ Програму виконано успішно.");
            Console.ReadLine();
        }
    }

}
