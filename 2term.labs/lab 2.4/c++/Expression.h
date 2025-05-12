#pragma once
class Expression
{
private:
	double a, b, c;
public:
	Expression();
	Expression(double _a, double _b, double _c);
	double getA();
	double getB();
	double getC();
	double calculateExpression();
};