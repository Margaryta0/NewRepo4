#include "Circle.h"
#include <iostream>
#include <cmath>
#define PI 3.141592653589793

using namespace std;

Circle::Circle(double radius) : _radius(radius) {};

double Circle::getRadius() {
	return _radius;
}

double Circle::getArea() const {
	double area = PI * pow(_radius, 2);
	return area;
}

double Circle::getPerimeter() const {
	double perimeter = 2 * PI * _radius;
	return perimeter;
}