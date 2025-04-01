#include "String.h"
#include<iostream>
#include<vector>
using namespace std;

String::String() {
	_string = { '1', '@', '#', 'h' };
}

String::String(const vector<char>& string) {
	_string = string;
}

String::String(const String& string) {
	_string = string._string;
}

size_t String::getLength() {
	return _string.size();
}