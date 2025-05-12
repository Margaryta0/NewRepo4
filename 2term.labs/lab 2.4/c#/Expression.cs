using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary1
{
    public class Expression
    {
        public double A { get; private set; }
        public double B { get; private set; }
        public double C { get; private set; }

        public Expression(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public double CalculateExpression()
        {
            Log log = new Log(B - 1);
            double numerator = 8 * log.Calculate() - C;
            double denominator = A * 2 + B / C;

            if (C == 0 || denominator == 0)
            {
                throw new DivideByZeroException("Error. Divide by zero!");
            }

            double result = numerator / denominator;
            return result;
        }
    }
}