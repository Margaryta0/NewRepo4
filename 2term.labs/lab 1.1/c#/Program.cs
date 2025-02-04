using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    class Program
    {
        static void GetData(Rhombus rhombus)
        {
            double perimetr = rhombus.GetPerimeter();
            double area = rhombus.GetArea();
            Console.WriteLine("Perimeter of rhombus: " + perimetr);
            Console.WriteLine("Area of rhombus: " + area);
        }
        static void Main(string[] args)
        {
            Rhombus rhombus1 = new Rhombus(1, 2, 3, 4, 5, 6, 7, 8);
            GetData(rhombus1);
        }
    }
}