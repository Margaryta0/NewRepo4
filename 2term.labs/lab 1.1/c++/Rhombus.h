#pragma once
#include <iostream>

struct Vertex {
	int x, y;
};

class Rhombus
{
private:
	Vertex A;
	Vertex B;
	Vertex C;
	Vertex D;

public:
	Rhombus(int Ax, int Ay, int Bx, int By, int Cx, int Cy, int Dx, int Dy);
	double getPerimeter();
	double getArea();
};