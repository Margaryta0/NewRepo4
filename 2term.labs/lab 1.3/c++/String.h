#pragma once
#include <iostream>
#include <vector>

using namespace std;

class String
{
private:
	vector<char>_string;

public:
	String();
	String(vector<char>string);
	String(const String& string);
	size_t getLength();
	vector<char> display();
	String operator+(String& string);
	String& operator-=(char s);
	int number(int a, int b = 1);

};