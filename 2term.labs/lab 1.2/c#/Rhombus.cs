using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{

	public struct Vertex
	{
		public int x { get; private set; }
		public int y { get; private set; }


		public Vertex(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	};
	public class Rhombus
	{
		private Vertex A;
		private Vertex B;
		private Vertex C;
		private Vertex D;

		public Rhombus()
		{
			A = new Vertex(2, 1);
			B = new Vertex(4, 3);
			C = new Vertex(6, 5);
			D = new Vertex(8, 7);
		}
		public Rhombus(Vertex A, Vertex B, Vertex C, Vertex D)
		{
			this.A = A;
			this.B = B;
			this.C = C;
			this.D = D;
		}

		public Rhombus(Rhombus rhombus)
		{
			this.A = new Vertex(rhombus.A.x, rhombus.A.y);
			this.B = new Vertex(rhombus.B.x, rhombus.B.y);
			this.C = new Vertex(rhombus.C.x, rhombus.C.y);
			this.D = new Vertex(rhombus.D.x, rhombus.D.y);
		}

		public Rhombus(ref Rhombus rhombus1)
		{
			this.A = rhombus1.A;
			this.B = rhombus1.B;
			this.C = rhombus1.C;
			this.D = rhombus1.D;

			rhombus1 = null;
		}

		~Rhombus()
		{
			Console.WriteLine("The program is over!");
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

		public string GetA()
		{
			return $"({A.x}, {A.y})";
		}

		public string GetB()
		{
			return $"({B.x}, {B.y})";
		}

		public string GetC()
		{
			return $"({C.x}, {C.y})";
		}

		public string GetD()
		{
			return $"({D.x}, {D.y})";
		}
	}

}