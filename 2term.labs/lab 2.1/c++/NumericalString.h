#pragma once
#include<iostream>
#include "String.h"
#include <vector>

class NumericalString : public String
{
public:
	NumericalString();
	NumericalString(const vector<char>& string);
	NumericalString(const String& string);

	vector<char> deleteSymbol(char s);
	vector<char> getValue();
};