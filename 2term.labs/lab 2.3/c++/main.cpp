#include "../StaticLib/IMyInterface.h"
#include "../StaticLib/String.h"
#include "../StaticLib/Text.h"
#include <iostream>
#include <vector>
using namespace std;

int main()
{
	String string1("bveb3bb");
	String string2("jcb233j");

	vector<String> strings = {
		string1,
		string2
	};

	Text text(strings);
	String string3("Hello2");

	cout << "Total symbols: " << text.getNumbersOfSymbols() << endl;

	cout << "Percent of digits: " << text.getPercentOfDigits() << "%" << endl;

	cout << "The largest string: " << text.TheLargestString() << endl;

	text.deleteString(string2);

	cout << "Total symbols after deletion: " << text.getNumbersOfSymbols() << endl;

	return 0;
}