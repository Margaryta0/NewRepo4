using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // ЗАВДАННЯ 1 — СТЕК (векторний, елементи double)
            Console.WriteLine(" ЗАВДАННЯ 1 — СТЕК ");

            VectorStack stack = new VectorStack(7); 

            Console.WriteLine("\nВставка елементів");
            double[] valuesToPush = { 3.14, -2.71, 0.5, 100.0, -50.25, 7.77, 1.0 };
            foreach (double v in valuesToPush)
            {
                bool ok = stack.Push(v);
                Console.WriteLine($"  Push({v:F2}) → {(ok ? "OK" : "FAILED")}");
            }

            Console.WriteLine($"Push в повний стек");
            stack.Push(999.0); 

            Console.WriteLine("\nПісля вставки:");
            stack.Print();

            Console.WriteLine("\nВидалення елементів");
            for (int i = 0; i < 3; i++)
            {
                double removed = stack.Pop();
                Console.WriteLine($"{removed:F2}");
            }

            Console.WriteLine("\nПісля видалення 3 елементів:");
            stack.Print();

            // ЗАВДАННЯ 2 — ОДНОСПРЯМОВАНИЙ СПИСОК (зв'язний)
            Console.WriteLine("\nЗАВДАННЯ 2 — СПИСОК (зв'язний, рядкові числа)");

            LinkedList list = new LinkedList();

            Console.WriteLine("\nВставка елементів:");
            string[] strValues = { "-10", "-5", "0", "3", "7", "10", "-2", "1" };
            foreach (string s in strValues)
            {
                list.AddLast(s);
                Console.WriteLine($"  AddLast(\"{s}\") → OK");
            }

            Console.WriteLine("\nПісля вставки:");
            list.Print();

            Console.WriteLine("\n- Видалення елементів -");

            // Видалення першого
            string r1 = list.RemoveFirst();
            Console.WriteLine($"  RemoveFirst() → \"{r1}\"");

            // Видалення за значенням
            bool r2 = list.RemoveByValue("7");
            Console.WriteLine($"  RemoveByValue(\"7\") → {(r2 ? "видалено" : "не знайдено")}");

            bool r3 = list.RemoveByValue("0");
            Console.WriteLine($"  RemoveByValue(\"0\") → {(r3 ? "видалено" : "не знайдено")}");

            Console.WriteLine("\nПісля видалення:");
            list.Print();

            // ЗАВДАННЯ 3 — СПИСОК → СТЕПЕНІ → 10^n → СТЕК
            Console.WriteLine("\nЗАВДАННЯ 3 — Перетворення: список → степені → 10^n → стек");
            Console.WriteLine();
            Console.WriteLine("  Алгоритм:");
            Console.WriteLine("  1) Беремо кожен елемент зі списку (рядок)");
            Console.WriteLine("  2) Перетворюємо його в int (степінь)");
            Console.WriteLine("  3) Обчислюємо 10^степінь = результат (double)");
            Console.WriteLine("  4) Кладемо результат у стек");

            LinkedList list3 = new LinkedList();
            string[] list3Values = { "-3", "-1", "0", "2", "4", "-2", "1" };
            foreach (string s in list3Values)
                list3.AddLast(s);

            VectorStack stack3 = new VectorStack(7);

            Console.WriteLine("\nВміст списку:");
            list3.Print();

            Console.WriteLine("\n--- Формування стека ---");
            ListNode current = list3.GetHead();
            while (current != null)
            {
                int exponent = int.Parse(current.Data); // перетворюємо рядок → int
                double result = Math.Pow(10, exponent);  // 10 ^ степінь
                Console.WriteLine($"  Елемент: \"{current.Data}\" → int: {exponent} → 10^{exponent} = {result}");
                stack3.Push(result);
                current = current.Next;
            }

            Console.WriteLine("\nВміст стека (після формування):");
            stack3.Print();
        }
    }
}
