using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_1
{
    struct Vertex
    {
        public int x, y;

        public Vertex(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    };
    internal class Rhombus
    {
        private Vertex A;
        private Vertex B;
        private Vertex C;
        private Vertex D;

        public Rhombus(int Ax, int Ay, int Bx, int By, int Cx, int Cy, int Dx, int Dy)
        {
            A = new Vertex(Ax, Ay);
            B = new Vertex(Bx, By);
            C = new Vertex(Cx, Cy);
            D = new Vertex(Dx, Dy);
        }

        public double GetPerimeter()
        {
            double side = Math.Sqrt(((B.x - A.x) * (B.x - A.x)) + ((B.y - A.y) * (B.y - A.y)));
            return 4 * side;
        }

        public double GetArea()
        {
            double d1 = Math.Sqrt(((C.x - A.x) * (C.x - A.x)) + ((C.y - A.y) * (C.y - A.y)));
            double d2 = Math.Sqrt(((D.x - B.x) * (D.x - B.x)) + ((D.y - B.y) * (D.y - B.y)));
            return (d1 * d2) / 2;
        }
    }
}