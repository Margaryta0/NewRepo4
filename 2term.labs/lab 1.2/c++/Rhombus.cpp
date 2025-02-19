#include "Rhombus.h"
#include <iostream>
#include <cmath>
#include <string>

using namespace std;

Rhombus::Rhombus(Vertex A, Vertex B, Vertex C, Vertex D) {
	this->A = A;
	this->B = B;
	this->C = C;
	this->D = D;
};

Rhombus::Rhombus() {
	A = { 1, 2 };
	B = { 3, 4 };
	C = { 5, 6 };
	D = { 7, 8 };
}

Rhombus::Rhombus(const Rhombus& rhombus, bool deep) {
	if (deep) {
		this->A = { rhombus.A.x, rhombus.A.y };
		this->B = { rhombus.B.x, rhombus.B.y };
		this->C = { rhombus.C.x, rhombus.C.y };
		this->D = { rhombus.D.x, rhombus.D.y };
	}
}

Rhombus::Rhombus(const Rhombus& rhombus) {
	this->A = rhombus.A;
	this->B = rhombus.B;
	this->C = rhombus.C;
	this->D = rhombus.D;
}

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

string Rhombus::getA() {
	return "(" + to_string(A.x) + ", " + to_string(A.y) + ")";
}

string Rhombus::getB() {
	return "(" + to_string(B.x) + ", " + to_string(B.y) + ")";
}

string Rhombus::getC() {
	return "(" + to_string(C.x) + ", " + to_string(C.y) + ")";
}

string Rhombus::getD() {
	return "(" + to_string(D.x) + ", " + to_string(D.y) + ")";
}

Rhombus::~Rhombus() {
	cout << "The program is over!" << endl;
}