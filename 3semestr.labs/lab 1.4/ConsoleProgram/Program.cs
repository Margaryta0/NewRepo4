using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace ConsoleProgram
{
    internal class Program
    {
        static void OnWithdrawn(object sender, ClassArgEvent e)
        {
            Console.WriteLine(e.Message);
        }

        static void OnOverdraft(object sender, ClassArgEvent e)
        {
            Console.WriteLine(e.Message);
        }

        public delegate int SumOfRow(int[] row);
        static void Main(string[] args)
        {

            int[][] matrix = new int[][]
            {
                new int[] {1, 2, 3},
                new int[] {4, 5, 6},
                new int[] {7, 8, 9}
            };

            SumOfRow sum = row => row.Sum();

            int[] rowSums = matrix.Select(row => sum(row)).ToArray();
            Console.WriteLine(string.Join(", ", rowSums));

            //part 2

            BankAccount account = new BankAccount(200, 500);

            account.Withdrawn += OnWithdrawn;
            account.Overdraft += OnOverdraft;

            account.Withdraw(900);


        }

    }
}
