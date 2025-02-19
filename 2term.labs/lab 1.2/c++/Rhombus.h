#pragma once
#include <iostream>
#include <string>
#include <sstream>

using namespace std;


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
	Rhombus(Vertex A, Vertex B, Vertex C, Vertex D);
	Rhombus();
	Rhombus(const Rhombus& rhombus);
	Rhombus(const Rhombus& rhombus, bool deep);
	double getPerimeter();
	double getArea();
	string getA();
	string getB();
	string getC();
	string getD();
	~Rhombus();
};