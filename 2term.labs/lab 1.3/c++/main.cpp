#include "String.h"
#include<iostream>
#include<vector>

using namespace std;

void output(String& string) {
	vector<char>vec = string.display();
	for (char sym : vec) {
		cout << sym;
	}
	cout << endl;
}

int main() {
	vector<char>str = { '0', '2', '3', '4' };


	String CS1;
	String CS2(str);
	String CS3(CS2);
	output(CS3);

	CS3 -= '0';
	output(CS3);
	String CS4 = CS1 + CS2;
	output(CS4);
	cout << CS2.number(2) << endl;

	return 0;


}