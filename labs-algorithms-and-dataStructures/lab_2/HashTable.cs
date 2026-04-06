using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2
{
    public class HashTable
    {
        private Vector[] table;      // масив елементів
        private bool[] isDeleted;    // маркери видалених комірок
        private int size;            // розмір таблиці

        // Конструктор — розмір задається користувачем
        public HashTable(int size)
        {
            this.size = size;
            table = new Vector[size];
            isDeleted = new bool[size];
            // null = порожня комірка, isDeleted[i] = false за замовчуванням
        }

        // ─── ХЕШ-ФУНКЦІЇ ───────────────────────────────────

        // Перша хеш-функція: метод ділення
        // h1(key) = key % size
        private int Hash1(int key)
        {
            return key % size;
        }

        // Друга хеш-функція: для подвійного хешування
        // h2(key) = 1 + key % (size - 1)
        // Результат завжди >= 1, ніколи не = 0
        private int Hash2(int key)
        {
            return 1 + key % (size - 1);
        }

        // Позиція при i-й спробі (подвійне хешування)
        // pos = (h1 + i * h2) % size
        private int Probe(int key, int attempt)
        {
            return (Hash1(key) + attempt * Hash2(key)) % size;
        }

        // ─── ОПЕРАЦІЯ ВСТАВКИ ──────────────────────────────

        /// <summary>
        /// Вставити вектор у хеш-таблицю.
        /// Повертає true якщо успішно, false якщо таблиця повна.
        /// </summary>
        public bool Insert(Vector v)
        {
            int key = v.Key();

            for (int i = 0; i < size; i++)
            {
                int pos = Probe(key, i);

                // Комірка вільна (порожня або видалена) — вставляємо
                if (table[pos] == null || isDeleted[pos])
                {
                    table[pos] = v;
                    isDeleted[pos] = false;

                    if (i == 0)
                        Console.WriteLine($"  Вставка: ключ={key,4} → позиція {pos,3} (без колізії)");
                    else
                        Console.WriteLine($"  Вставка: ключ={key,4} → колізія! Спроба {i+1}, позиція {pos,3} " +
                                          $"[h1={Hash1(key)}, h2={Hash2(key)}]");
                    return true;
                }
            }

            // Не знайшли вільного місця — таблиця повна
            Console.WriteLine($"  [!] Вставка не вдалась: таблиця повна! (ключ={key})");
            return false;
        }

        // ─── ОПЕРАЦІЯ ПОШУКУ ───────────────────────────────

        /// <summary>
        /// Знайти вектор за ключем.
        /// Повертає вектор або null якщо не знайдено.
        /// </summary>
        public Vector Search(int key)
        {
            for (int i = 0; i < size; i++)
            {
                int pos = Probe(key, i);

                // Порожня і НЕ видалена — значить далі точно немає
                if (table[pos] == null && !isDeleted[pos])
                    return null;

                // Знайшли і не видалений
                if (table[pos] != null && !isDeleted[pos] && table[pos].Key() == key)
                    return table[pos];
            }
            return null;
        }

        // ─── ЗАВДАННЯ 3: Видалення за критерієм ────────────

        /// <summary>
        /// Видалити всі вектори з довжиною, МЕНШОЮ від заданої.
        /// При видаленні ставимо маркер isDeleted = true.
        /// Самі дані (table[pos]) залишаємо для коректної роботи зондування.
        /// </summary>
        public int DeleteByLength(double maxLength)
        {
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                if (table[i] != null && !isDeleted[i])
                {
                    if (table[i].Length() < maxLength)
                    {
                        Console.WriteLine($"  Видалення позиції {i,3}: довжина {table[i].Length():F2} < {maxLength:F2}");
                        isDeleted[i] = true;
                        count++;
                    }
                }
            }
            return count;
        }

        // ─── ВИВЕДЕННЯ ─────────────────────────────────────

        /// <summary>
        /// Вивести хеш-таблицю:
        /// для кожної позиції — номер, ключ, елемент або "порожньо".
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"\n  {"Поз",4} | {"Ключ",6} | Елемент");
            Console.WriteLine($"  {new string('─', 80)}");

            for (int i = 0; i < size; i++)
            {
                if (table[i] == null || isDeleted[i])
                {
                    string status = isDeleted[i] ? "[ВИДАЛЕНО]" : "[порожньо]";
                    Console.WriteLine($"  {i,4} | {"—",6} | {status}");
                }
                else
                {
                    Console.WriteLine($"  {i,4} | {table[i].Key(),6} | {table[i]}");
                }
            }
            Console.WriteLine();
        }
    }
}
