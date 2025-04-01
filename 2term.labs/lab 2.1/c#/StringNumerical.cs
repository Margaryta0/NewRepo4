using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace lab_2._1_c_
{
    public class StringNumerical : MyString
    {
        public StringNumerical()
        {
            _string = "1234";
        }
        public StringNumerical(string string1) : base(string1)
        {
        }

        public StringNumerical(MyString other) : base(other)
        {
            StringBuilder newString = new StringBuilder();
            foreach (char s in _string)
            {
                if (s >= '0' && s<= '9')
                {
                    newString.Append(s);
                }
            }
            _string = newString.ToString();

            if (_string.Length == 0)
            {
                Console.WriteLine("String doesn`t grow to the numerical string");
            }
        }
        public string GetValue()
        {
            return _string;
        }

        public string DeleteSymbol(char symbol)
        {
            _string = _string.Replace(symbol.ToString(), "");
            return _string;
        }
    }
}