#include "Expression.h"
#include "Log.h"
#include <iostream>
#include <string>
using namespace std;

Expression::Expression() {
	a = 3;
	b = 4;
	c = 3.2;
}

Expression::Expression(double _a, double _b, double _c) : a(_a), b(_b), c(_c) {};

double Expression::getA() {
	return a;
}

double Expression::getB() {
	return b;
}

double Expression::getC() {
	return c;
}

double Expression::calculateExpression() {
	Log log(b - 1);

	double numerator = 8 * log.calculate() - c;
	double denominator = a * 2 + b / c;

	if (c == 0 || denominator == 0) {
		string str = "Error. Divided by zero.";
		throw str;
	}

	return numerator / denominator;
}
