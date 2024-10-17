/**
 * Done by:
 * Student Name: Margaryta Smal
 * Student Group: 123
 * RGR
 */


 //ланцюжок містить два підланцюжки з послідовностей символів 0...9, які розподілені послідовністю символів */*

#include<iostream>

using namespace std;

bool isValidString(const string& str) {
	int len = str.length();

	int i = 0;
	while (i < len && str[i] >= '0' && str[i] <= '9') {  //перевіряємо перший підланцюжок і переходимо до наступних елементів
		i++;
	}

	if (i + 2 >= len || str.substr(i, 3) != "*/*") { // первіряємо чи наявні в ланцюжку символи */* і переходимо до наступних елементів
		return false;
	}

	i = i + 3;

	while (i < len && str[i] >= '0' && str[i] <= '9') { // перевіряємо другий підланцюжок 
		i++;
	}
	return true;
}

int main() {
	string input;

	cout << "Enter a string to check: ";
	cin >> input;

	if (isValidString(input)) {
		cout << "String belongs to language  L(V)." << endl;
	}
	else {
		cout << "String does not belong to language  L(V)." << endl;
	}

	return 0;
}