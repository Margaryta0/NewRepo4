#include "Rhombus.h"
#include <iostream>
#include <cmath>

using namespace std;

Rhombus::Rhombus(int Ax, int Ay, int Bx, int By, int Cx, int Cy, int Dx, int Dy) {
	A = { Ax, Ay };
	B = { Bx, By };
	C = { Cx, Cy };
	D = { Dx, Dy };
};

double Rhombus::getPerimeter() {
	double side = sqrt(((B.x - A.x) * (B.x - A.x)) + ((B.y - A.y) * (B.y - A.y)));
	double perimetr = 4 * side;
	return perimetr;
}

double Rhombus::getArea() {
	double d1 = sqrt(((C.x - A.x) * (C.x - A.x)) + ((C.y - A.y) * (C.y - A.y)));
	double d2 = sqrt(((D.x - B.x) * (D.x - B.x)) + ((D.y - B.y) * (D.y - B.y)));
	double area = (d1 * d2) / 2;
	return area;

}
