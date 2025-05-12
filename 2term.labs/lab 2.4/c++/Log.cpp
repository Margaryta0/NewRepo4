#include "Log.h"
#include <iostream>
#include <cmath>
#include <string>
using namespace std;

Log::Log(double num1) : num(num1) {};

double Log::calculate() {
	if (num <= 0) {
		string str = "Error. Argument for lg must be > 0!";
		throw str;
	}
	return log10(num);
}