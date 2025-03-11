using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace lab_1._3_c_
{
    internal class String
    {
        private char[] _string;

        public String()
        {
            _string = new char[] { '1', '2', '#' };
        }

        public String(char[] string1)
        {
            _string = string1;
        }

        public String(String string2)
        {
            _string = new char[string2._string.Length];
            Array.Copy(string2._string, _string, string2._string.Length);
        }

        public int GetLength()
        {
            return _string.Length;
        }

        public char[] GetArray()
        {
            return _string;
        }

        public static String operator +(String string1, String string2)
        {
            int len1 = string1.GetLength();
            int len2 = string2.GetLength();
            int totalLength = len1 + len2;

            char[] newString = new char[totalLength];


            for (int i = 0; i < len1; i++)
            {
                newString[i] = string1._string[i];
            }

            for (int i = 0; i < len2; i++)
            {
                newString[len1 + i] = string2._string[i];
            }

            return new String(newString);
        }

        public static String operator -(String string1, char s)
        {
            char[] newString = new char[string1._string.Length - 1];

            for (int i = 0, j = 0; i < string1._string.Length; i++)
            {
                if (string1._string[i] == s) continue;
                newString[j++] = string1._string[i];
            }
            return new String(newString);
        }

    }

}