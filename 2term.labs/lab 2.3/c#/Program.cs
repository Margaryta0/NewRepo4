using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace projectc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var string1 = new MyString("bveb3bb");
            var string2 = new MyString("jcb233j");

            var strings = new List<MyString> { string1, string2 };
            var text = new Text(strings);

            var string3 = new MyString("Hello2");
            text.AddString(string3);

            Console.WriteLine("Total symbols: " + text.GetNumbersOfSymbols());
            Console.WriteLine("Percent of digits: " + text.GetPercentOfDigits() + "%");
            Console.WriteLine("Largest string: " + text.TheLargestString());
        }
    }

}
