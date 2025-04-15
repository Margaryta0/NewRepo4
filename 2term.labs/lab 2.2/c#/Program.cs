using ClassLibrary;
using System;

namespace Project_c_
{
	internal class Program
	{
		static void GetData(Shape shape)
		{
			Console.WriteLine("Area: " + shape.GetArea());
			Console.WriteLine("Perimeter: " + shape.GetPerimeter());
		}
		static void Main(string[] args)
		{
			Vertex A = new Vertex(0, 0);
			Vertex B = new Vertex(1, 4);
			Vertex C = new Vertex(3, 4);
			Vertex D = new Vertex(5, 0);

			Shape s = new Circle(4.5);
			Console.WriteLine(s.GetPerimeter());
			Shape t = new Trapezoid(A, B, C, D);
			GetData(t);
			GetData(s);

		}
	}
}