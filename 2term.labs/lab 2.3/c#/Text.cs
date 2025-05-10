using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Text
    {
        private List<MyString> strings;

        public Text(List<MyString> stringss)
        {
            strings = new List<MyString>(stringss);
        }

        public void AddString(MyString string1)
        {
            strings.Add(string1);
        }

        public void DeleteString(MyString string1)
        {
            strings.RemoveAll(s => s.GetString() == string1.GetString());
        }

        public void DeleteAllText()
        {
            strings.Clear();
        }

        public string TheLargestString()
        {
            if (strings.Count == 0) return "";

            return strings.OrderByDescending(s => s.GetString().Length).First().GetString();
        }

        public double GetPercentOfDigits()
        {
            int totalDigits = 0;
            int totalChars = 0;

            foreach (var str in strings)
            {
                totalDigits += str.NumberOfDigits();
                totalChars += str.GetString().Length;
            }

            if (totalChars == 0) return 0.0;
            return 100.0 * totalDigits / totalChars;
        }

        public int GetNumbersOfSymbols()
        {
            int total = 0;
            foreach (var str in strings)
            {
                total += str.GetString().Length;
            }
            return total;
        }
    }
}