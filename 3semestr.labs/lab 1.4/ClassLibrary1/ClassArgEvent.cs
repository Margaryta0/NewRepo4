using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class ClassArgEvent : EventArgs
    {
        public int Amount { get; }
        public string Message { get; }

        public ClassArgEvent(int amount, string message)
        {
            Amount = amount;
            Message = message;
        }
    }
}

