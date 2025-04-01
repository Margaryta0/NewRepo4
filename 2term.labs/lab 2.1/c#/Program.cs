using lab_2._1_c_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace lab_2._1_class
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyString instance = new MyString();
            MyString string1 = new MyString("Its123і");
            StringNumerical numberString = new StringNumerical("Hi");

            Console.WriteLine(instance.GetLength());
            Console.WriteLine(numberString.GetLength());

            numberString.DeleteSymbol('H');
            Console.WriteLine(numberString.GetValue());

            StringNumerical string2 = new StringNumerical(string1);
            Console.WriteLine("String update:" + string2.GetValue());

            Console.ReadKey();


        }
    }
}