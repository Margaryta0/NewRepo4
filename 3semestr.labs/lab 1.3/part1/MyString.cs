using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace part1
{
    [Serializable]
    public class MyString
    {
        public string Value { get; set; }
        public int Length
        {
            get
            {
                return Value?.Length ?? 0;
            }
        }

        public MyString() { }

        public MyString(string value)
        {
            Value = value;
        }

        public int FindSymbol(char c)
        {
            if (Value == null)
            {
                return -1;
            }
            int index = 0;
            foreach (char item in Value)
            {
                if (item == c)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public void Reverse()
        {
            if (Value == null) return;
            char[] array = Value.ToCharArray();
            Array.Reverse(array);
            Value = new string(array);
        }

        public void AddString(string str)
        {
            if (str == null) return;
            Value += str;
        }

        public string Output()
        {
            return Value;
        }
    }
}