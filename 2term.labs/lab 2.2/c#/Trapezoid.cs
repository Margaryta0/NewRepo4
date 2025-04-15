using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public struct Vertex
    {
        public int x, y;

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Trapezoid : Shape
    {
        public Vertex A { get; private set; }
        public Vertex B { get; private set; }
        public Vertex C { get; private set; }
        public Vertex D { get; private set; }

        public Trapezoid(Vertex A, Vertex B, Vertex C, Vertex D)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
        }

        public double Distance(Vertex A, Vertex B)
        {
            double distance = Math.Sqrt(Math.Pow(B.x - A.x, 2) + Math.Pow(B.y - A.y, 2));
            return distance;
        }

        public override double GetArea()
        {
            double a = D.y - A.y;
            double b = A.x - D.x;
            double c = D.x * A.y - A.x * D.y;

            double numerator = Math.Abs(a * B.x + b * B.y + c);
            double denominator = Math.Sqrt(a * a + b * b);
            double h = numerator / denominator;
            double l1 = Distance(A, D);
            double l2 = Distance(B, C);

            double area = ((l1 + l2) * h) / 2;
            return area;
        }

        public override double GetPerimeter()
        {
            double l1 = Distance(A, B);
            double l2 = Distance(B, C);
            double l3 = Distance(C, D);
            double l4 = Distance(D, A);
            double perimeter = l1 + l2 + l3 + l4;
            return perimeter;
        }

    }
}
