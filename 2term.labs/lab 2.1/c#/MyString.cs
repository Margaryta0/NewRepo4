using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_2._1_c_
{
    public class MyString
    {
        protected string _string;

        public string String { get { return _string; } }

        public MyString()
        {
            _string = "Hola";
        }
        public MyString(string string1)
        {
            _string = string1;
        }

        public MyString(MyString other)
        {
            _string = other._string;
        }

        public int GetLength()
        {
            return _string.Length;
        }
    }
}