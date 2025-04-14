#include "../StaticLib/Shape.h"
#include "../StaticLib/Circle.h"
#include "../StaticLib/Trapezoid.h"
#include <iostream>
using namespace std;

void getData(Shape& shape) {
	cout << "Area: " << shape.getArea() << endl;
	cout << "Perimeter: " << shape.getPerimeter() << endl;
}

int main()
{
	Vertex A = { 2, 3 };
	Vertex B = { 4, 5 };
	Vertex C = { 6, 7 };
	Vertex D = { 2, 8 };

	Shape* s = new Circle(3.2);
	//cout << s->getArea() << endl;
	Circle c(4);
	getData(c);
	getData(*s);

	delete s;
}