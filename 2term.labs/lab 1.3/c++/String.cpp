#include "String.h"
#include<iostream>
#include<vector>

using namespace std;

String::String() {
	_string = { '@', '#', '1', '&' };
}

String::String(vector<char>string) {
	_string = string;
}

String::String(const String& string) {
	_string = string._string;
}

size_t String::getLength() {
	return _string.size();
}

vector<char> String::display() {
	return _string;
}

String String::operator+(String& string) {
	vector<char>newString = _string;
	newString.insert(newString.end(), string._string.begin(), string._string.end());
	return String(newString);
}

String& String::operator-=(char s) {
	for (int i = 0; i < _string.size(); i++) {
		if (_string[i] == s) {
			_string.erase(_string.begin() + i);
		}
	}
	return *this;
}

int String::number(int a, int b) {
	return a + b;
}