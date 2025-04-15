using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Circle : Shape
    {
        public double Radius { get; private set; }
        public Circle(double radius)
        {
            Radius = radius;
        }

        public override double GetArea()
        {
            double area = Math.PI * Radius * Radius;
            return area;
        }

        public override double GetPerimeter()
        {
            double perimeter = Math.PI * Radius * 2;
            return perimeter;
        }
    }
}
