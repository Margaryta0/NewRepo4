using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    internal class Log
    {
        private double Num { get; }
        public Log(double num)
        {
            Num = num;
        }

        public double Calculate()
        {
            if (Num <= 0)
            {
                throw new ArgumentException("Error. Argument for lg must be > 0!");
            }
            double result = Math.Log10(Num);

            return result;
        }

    }
}