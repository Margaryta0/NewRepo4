#pragma once
#include <iostream>
#include<vector>

using namespace std;

class String
{
protected:
	vector<char>_string;

public:
	String();
	String(const vector<char>& string);
	String(const String& string);

	size_t getLength();
};