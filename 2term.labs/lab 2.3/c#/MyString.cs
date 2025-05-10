using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class MyString : IMyInterface
    {
        private string _string;

        public MyString(string string1)
        {
            _string = string1;
        }

        public string GetString()
        {
            return _string;
        }

        public int NumberOfDigits()
        {
            int count = 0;
            foreach (char ch in _string)
            {
                if (char.IsDigit(ch)) count++;
            }
            return count;
        }

        public int SumOfDigits()
        {
            int sum = 0;
            foreach (char ch in _string)
            {
                if (char.IsDigit(ch)) sum += ch - '0';
            }
            return sum;
        }

        public double PercentOfDigits()
        {
            if (_string.Length == 0) return 0.0;

            int digitCount = 0;
            foreach (char ch in _string)
            {
                if (char.IsDigit(ch)) digitCount++;
            }

            return 100.0 * digitCount / _string.Length;
        }
    }
}