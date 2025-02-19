using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1;

namespace lab
{
    internal class Program
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
            Vertex _A = new Vertex(1, 2);
            Vertex _B = new Vertex(3, 4);
            Vertex _C = new Vertex(4, 5);
            Vertex _D = new Vertex(5, 6);

            ClassLibrary1.Rhombus rhombus = new Rhombus(_A, _B, _C, _D);
            Console.WriteLine("Your A : " + rhombus.GetA() + ", Your B: " + rhombus.GetB() + ", Your C: " + rhombus.GetC() + ", Your D: " + rhombus.GetD());
            GetData(rhombus);
            ClassLibrary1.Rhombus rhombus1 = new Rhombus();
            ClassLibrary1.Rhombus rhombus2 = new Rhombus(rhombus1);
            ClassLibrary1.Rhombus rhombus3 = new Rhombus(ref rhombus1);
        }
    }
}