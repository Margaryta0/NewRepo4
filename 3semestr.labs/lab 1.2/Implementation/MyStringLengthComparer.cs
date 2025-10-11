using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation
{
    public class MyStringLengthComparer : IComparer<MyString>
    {
        public int Compare(MyString x, MyString y)
        {
            if (x == null || y == null)
            {
                return x == null ? (y == null ? 0 : -1) : 1;
            }
            return x.Length.CompareTo(y.Length);
        }
    }
}