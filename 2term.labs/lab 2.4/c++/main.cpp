#include "../StaticLib/Expression.h"
#include "../StaticLib/Log.h"
#include <iostream>
#include <string>
using namespace std;

int main() {
	try {
		Expression exps[] = {
			Expression(1, 2, 66),
			Expression()
		};
		for (auto& exp : exps) {
			cout << "Result: " << exp.calculateExpression() << endl;
		}
	}
	catch (const string& str) {
		cout << str << endl;
	}

	return 0;
}