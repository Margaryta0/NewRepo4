using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1._3_c_
{
    internal class Program
    {
        static void display(String string1)
        {
            char[] str = string1.GetArray();
            for (int i = 0; i< str.Length; i++)
            {
                Console.WriteLine(str[i]);
            }
        }
        static void Main(string[] args)
        {
            char[] vec = { '3', '4', '5', '0' };
            String CS1 = new String(vec);
            String CS2 = new String();
            String CS3 = CS1 + CS2;
            display(CS3);
            String CS4 = CS1 - '0';
            display(CS4);

        }
    }
}