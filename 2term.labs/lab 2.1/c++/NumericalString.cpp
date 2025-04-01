#include "NumericalString.h"
#include <iostream>
#include <vector>

using namespace std;

NumericalString::NumericalString() : String() {
	_string = { '1', '2', '3' };
}

NumericalString::NumericalString(const vector<char>& string) : String(string) {}

NumericalString::NumericalString(const String& string) : String(string) {
	vector<char>newString;
	for (char val : _string) {
		if (val >= '0' && val <= '9') {
			newString.push_back(val);
		}
	}
	_string = newString;
	if (_string.empty()) {
		cout << "String doesn`t grow to the numerical string" << endl;
	}
}

vector<char> NumericalString::deleteSymbol(char s) {
	for (int i = 0; i < _string.size(); i++) {
		if (_string[i] == s) {
			_string.erase(_string.begin() + i);
			i--;
		}
	}
	return _string;
}

vector<char> NumericalString::getValue() {
	return _string;
}