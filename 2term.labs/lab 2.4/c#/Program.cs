using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace projectc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ClassLibrary1.Expression[] exps =
            {
                new ClassLibrary1.Expression(2, 6, 24),
                new ClassLibrary1.Expression(3, 7, 0)
            };
            try
            {
                for (int i = 0; i < exps.Length; i++)
                {
                    Console.WriteLine("Result: " + exps[i].CalculateExpression());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

}