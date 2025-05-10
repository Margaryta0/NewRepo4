
#pragma once
#include "String.h"
#include <iostream>
#include <vector>
using namespace std;

class Text
{
private:
	vector<String>strings;
public:
	Text(vector<String>& stringss);
	void addString(const String& string1);
	void deleteString(const String& string1);
	void deleteAllText();
	string TheLargestString();
	double getPercentOfDigits();
	int getNumbersOfSymbols();
};